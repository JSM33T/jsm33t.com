using BCrypt.Net;
using JassWebApi.Data;
using JassWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JassWebApi.Data
{

    public interface IAuthRepository
    {
        Task<(User user, VerificationToken verificationToken)> SignupAsync(User userInput, string password);

        Task<(User user, RefreshToken refreshToken)> LoginAsync(string email, string password, string sessionId, string userAgent, string ip);

        Task<(User user, RefreshToken refreshToken)> GoogleLoginAsync(string googleUserId, string email, string firstName, string lastName, string avatar, string sessionId, string userAgent, string ip);

        Task<RefreshToken> RefreshTokenAsync(string refreshTokenStr, string sessionId);

        Task<List<UserSessionDto>> GetSessionsAsync(Guid userId, string currentSessionId);

        Task RemoveSessionAsync(Guid userId, string sessionId);

        Task<PasswordRecovery> CreatePasswordRecoveryAsync(Guid userId);

        Task ResetPasswordAsync(string token, string newPassword);
        Task<VerificationToken> CreateVerificationTokenAsync(Guid userId);
        Task VerifyUserAsync(string token);

    }
}
public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(User user, VerificationToken verificationToken)> SignupAsync(User userInput, string password)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userInput.Email);
        if (existingUser != null)
            throw new Exception("User already exists");

        var hashedPassword = HashPassword(password);

        var user = new User
        {
            FirstName = userInput.FirstName,
            LastName = userInput.LastName,
            Avatar = userInput.Avatar,
            UserName = string.IsNullOrWhiteSpace(userInput.UserName) ? userInput.Email.Split('@')[0] : userInput.UserName,
            Role = "user",
            Email = userInput.Email,
            PasswordHash = hashedPassword,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsVerified = false
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var verificationToken = await CreateVerificationTokenAsync(user.Id);

        return (user, verificationToken);
    }


    public async Task<VerificationToken> CreateVerificationTokenAsync(Guid userId)
    {
        // Optional: Check if user exists
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new Exception("User not found");

        var token = new VerificationToken
        {
            UserId = userId,
            Token = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N"),
            ExpiresAt = DateTime.UtcNow.AddHours(24),
            IsUsed = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.VerificationTokens.Add(token);
        await _context.SaveChangesAsync();

        return token;
    }

    public async Task<(User user, RefreshToken refreshToken)> LoginAsync(string email, string password, string sessionId, string userAgent, string ip)
    {
        var user = await _context.Users
            .Include(u => u.UserSessions)
            .ThenInclude(us => us.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null || !VerifyPassword(password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        // ✅ Check if session exists
        var session = user.UserSessions.FirstOrDefault(s => s.SessionId == sessionId);
        if (session == null)
        {
            // Create new session for this device/browser
            session = new UserSession
            {
                SessionId = sessionId,
                UserAgent = userAgent,
                Ip = ip,
                UserId = user.Id,
                IsLoggedIn = true,
                CreatedAt = DateTime.UtcNow,
                LastUsedAt = DateTime.UtcNow,
                ValidUntil = DateTime.UtcNow.AddDays(7)
            };
            _context.UserSessions.Add(session);
        }
        else
        {
            // Update existing session
            session.LastUsedAt = DateTime.UtcNow;
            session.IsLoggedIn = true;
            _context.UserSessions.Update(session);

            // 🔥 Delete any existing refresh tokens for this session
            if (session.RefreshTokens.Any())
            {
                _context.RefreshTokens.RemoveRange(session.RefreshTokens);
            }
        }

        // ✅ Generate new refresh token for this session
        var refreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N"),
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow,
            UserSession = session
        };

        _context.RefreshTokens.Add(refreshToken);

        await _context.SaveChangesAsync();

        return (user, refreshToken);
    }


    public async Task<(User user, RefreshToken refreshToken)> GoogleLoginAsync(string googleUserId, string email, string firstName, string lastName, string avatar, string sessionId, string userAgent, string ip)
    {
        var user = await _context.Users.Include(u => u.UserSessions).FirstOrDefaultAsync(u => u.GoogleUserId == googleUserId);

        if (user == null)
        {
            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Avatar = avatar,
                UserName = email.Split('@')[0],
                Role = "user",
                Email = email,
                GoogleUserId = googleUserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        var session = await CreateOrUpdateSession(user, sessionId, userAgent, ip);

        var refreshToken = GenerateRefreshToken(session);
        _context.RefreshTokens.Add(refreshToken);

        await _context.SaveChangesAsync();

        return (user, refreshToken);
    }

    public async Task<RefreshToken> RefreshTokenAsync(string refreshTokenStr, string sessionId)
    {
        var refreshToken = await _context.RefreshTokens
            .Include(r => r.UserSession)
            .ThenInclude(us => us.User)
            .FirstOrDefaultAsync(r => r.Token == refreshTokenStr && r.UserSession.SessionId == sessionId);

        if (refreshToken == null || refreshToken.IsRevoked || refreshToken.ExpiresAt < DateTime.UtcNow)
            throw new Exception("Invalid refresh token");

        refreshToken.IsRevoked = true;

        var newRefreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N"),
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow,
            UserSessionId = refreshToken.UserSessionId
        };

        _context.RefreshTokens.Add(newRefreshToken);
        await _context.SaveChangesAsync();

        return newRefreshToken;
    }


    public async Task<List<UserSessionDto>> GetSessionsAsync(Guid userId, string currentSessionId)
    {
        var sessions = await _context.UserSessions
            .Where(s => s.UserId == userId)
            .Select(s => new UserSessionDto
            {
                SessionId = s.SessionId,
                UserAgent = s.UserAgent,
                Ip = s.Ip,
                IsCurrent = s.SessionId == currentSessionId,
                IsLoggedIn = s.IsLoggedIn,
                ValidUntil = s.ValidUntil
            })
            .ToListAsync();

        return sessions;
    }

    public async Task RemoveSessionAsync(Guid userId, string sessionId)
    {
        var session = await _context.UserSessions.FirstOrDefaultAsync(s => s.UserId == userId && s.SessionId == sessionId);
        if (session == null)
            throw new Exception("Session not found");

        _context.UserSessions.Remove(session);
        await _context.SaveChangesAsync();
    }


    public async Task VerifyUserAsync(string token)
    {
        var verification = await _context.VerificationTokens.Include(v => v.User)
            .FirstOrDefaultAsync(v => v.Token == token);

        if (verification == null || verification.IsUsed || verification.ExpiresAt < DateTime.UtcNow)
            throw new Exception("Invalid or expired verification token");

        verification.IsUsed = true;
        verification.User.IsVerified = true;

        _context.VerificationTokens.Update(verification);
        _context.Users.Update(verification.User);

        await _context.SaveChangesAsync();
    }

    public async Task<PasswordRecovery> CreatePasswordRecoveryAsync(Guid userId)
    {
        var recovery = new PasswordRecovery
        {
            UserId = userId,
            Token = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N"),
            ExpiresAt = DateTime.UtcNow.AddMinutes(30),
            IsUsed = false
        };

        _context.PasswordRecoveries.Add(recovery);
        await _context.SaveChangesAsync();

        return recovery;
    }

    public async Task ResetPasswordAsync(string token, string newPassword)
    {
        var recovery = await _context.PasswordRecoveries.Include(r => r.User).FirstOrDefaultAsync(r => r.Token == token);
        if (recovery == null || recovery.IsUsed || recovery.ExpiresAt < DateTime.UtcNow)
            throw new Exception("Invalid or expired token");

        recovery.IsUsed = true;
        recovery.User.PasswordHash = HashPassword(newPassword);
        recovery.User.UpdatedAt = DateTime.UtcNow;

        _context.PasswordRecoveries.Update(recovery);
        _context.Users.Update(recovery.User);

        await _context.SaveChangesAsync();
    }

    private async Task<UserSession> CreateOrUpdateSession(User user, string sessionId, string userAgent, string ip)
    {
        var session = user.UserSessions.FirstOrDefault(s => s.SessionId == sessionId);
        if (session == null)
        {
            session = new UserSession
            {
                SessionId = sessionId,
                UserAgent = userAgent,
                Ip = ip,
                UserId = user.Id,
                IsLoggedIn = true,
                ValidUntil = DateTime.UtcNow.AddDays(7)
            };
            _context.UserSessions.Add(session);
        }
        else
        {
            session.LastUsedAt = DateTime.UtcNow;
            session.IsLoggedIn = true;
            _context.UserSessions.Update(session);
        }
        return session;
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashed)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashed);
    }

    private RefreshToken GenerateRefreshToken(UserSession session)
    {
        return new RefreshToken
        {
            Token = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N"),
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            UserSession = session
        };
    }
}
