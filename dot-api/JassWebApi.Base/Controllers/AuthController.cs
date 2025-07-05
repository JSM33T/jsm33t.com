using JassWebApi.Data;
using JassWebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using JassWebApi.Helpers.Mappers;
using Microsoft.EntityFrameworkCore;
using JassWebApi.Infra;

namespace JassWebApi.Base.Controllers
{

    [Route("api/[controller]")]
    public class AuthController(IAuthRepository authRepo, AppDbContext context,IJwtService jwtService) : JsmtBaseController
    {
        private readonly IAuthRepository _authRepo = authRepo;
        private readonly AppDbContext _context = context;
        private readonly IJwtService _jwtService = jwtService;

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserSignUpRequest request)
        {
            try
            {
                var (user, verificationToken) = await _authRepo.SignupAsync(request.ToUserEntity(), request.Password);

                // TODO: send verification email with verificationToken.Token

                return RESP_Success(new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.UserName,
                    user.Email
                }, "Signup successful, please verify your email.");
            }
            catch (Exception ex)
            {
                return RESP_BadRequestResponse(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var sessionId = Request.Cookies["sessionId"];
                if (string.IsNullOrEmpty(sessionId))
                    return RESP_BadRequestResponse("Session ID cookie missing");

                var userAgent = Request.Headers["User-Agent"].ToString();
                var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

                var (user, refreshToken) = await _authRepo.LoginAsync(
                    request.Email,
                    request.Password,
                    sessionId,
                    userAgent,
                    ip
                );

                if (!user.IsVerified)
                    throw new Exception("User email not verified");

                // ✅ Generate JWT token
                var jwtToken = _jwtService.GenerateJwtToken(user); // Ensure IJwtService is injected

                // ✅ Set refresh token cookie
                Response.Cookies.Append("refreshToken", refreshToken.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = refreshToken.ExpiresAt,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                Response.Cookies.Append("sessionId", sessionId, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddYears(1),
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                // ✅ Return user details + JWT token in response
                return RESP_Success(new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.UserName,
                    user.Email,
                    token = jwtToken
                }, "Login successful");
            }
            catch (Exception ex)
            {
                return RESP_BadRequestResponse(ex.Message);
            }
        }


        [HttpPost("request-verification")]
        public async Task<IActionResult> RequestVerification([FromBody] EmailRequest request)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (user == null)
                    return RESP_BadRequestResponse("User not found");

                var verification = await _authRepo.CreateVerificationTokenAsync(user.Id);

                // TODO: send verification email with verification.Token

                return RESP_Success("Verification email sent");
            }
            catch (Exception ex)
            {
                return RESP_BadRequestResponse(ex.Message);
            }
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] TokenRequest request)
        {
            try
            {
                await _authRepo.VerifyUserAsync(request.Token);
                return RESP_Success("User verified successfully");
            }
            catch (Exception ex)
            {
                return RESP_BadRequestResponse(ex.Message);
            }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            try
            {
                var sessionId = Request.Cookies["sessionId"];
                if (string.IsNullOrEmpty(sessionId))
                    return RESP_BadRequestResponse("Session ID cookie missing");

                var userAgent = Request.Headers["User-Agent"].ToString();
                var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

                var (user, refreshToken) = await _authRepo.GoogleLoginAsync(
                    request.GoogleUserId,
                    request.Email,
                    request.FirstName,
                    request.LastName,
                    request.Avatar,
                    sessionId,
                    userAgent,
                    ip
                );

                Response.Cookies.Append("refreshToken", refreshToken.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = refreshToken.ExpiresAt,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                Response.Cookies.Append("sessionId", sessionId, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddYears(1),
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return RESP_Success(new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.UserName,
                    user.Email
                }, "Google login successful");
            }
            catch (Exception ex)
            {
                return RESP_BadRequestResponse(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var refreshTokenStr = Request.Cookies["refreshToken"];
                var sessionId = Request.Cookies["sessionId"];

                if (string.IsNullOrEmpty(refreshTokenStr) || string.IsNullOrEmpty(sessionId))
                    return RESP_BadRequestResponse("Missing refresh token or session ID");

                var newRefreshToken = await _authRepo.RefreshTokenAsync(refreshTokenStr, sessionId);

                var user = await _context.Users
                    .Include(u => u.UserSessions)
                    .ThenInclude(us => us.RefreshTokens)
                    .FirstOrDefaultAsync(u => u.UserSessions.Any(us => us.Id == newRefreshToken.UserSessionId));

                if (user == null)
                    return RESP_BadRequestResponse("User not found for this session");

                var jwtToken = _jwtService.GenerateJwtToken(user);

                // Update refresh token cookie
                Response.Cookies.Append("refreshToken", newRefreshToken.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = newRefreshToken.ExpiresAt,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return RESP_Success(new
                {
                    token = jwtToken
                }, "Token refreshed");
            }
            catch (Exception ex)
            {
                return RESP_BadRequestResponse(ex.Message);
            }
        }


        [HttpGet("sessions")]
        public async Task<IActionResult> GetSessions()
        {
            try
            {
                var userId = GetUserId(); // Implement as previously discussed
                var currentSessionId = Request.Cookies["sessionId"];

                var sessions = await _authRepo.GetSessionsAsync(userId, currentSessionId);

                return RESP_Success(sessions, "Sessions retrieved successfully");
            }
            catch (Exception ex)
            {
                return RESP_BadRequestResponse(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userId = GetUserId(); // your helper to extract userId from JWT claims
                var sessionId = Request.Cookies["sessionId"];

                if (string.IsNullOrEmpty(sessionId))
                    return RESP_BadRequestResponse("Session ID cookie missing");

                // Fetch the current session with its refresh tokens
                var session = await _context.UserSessions
                    .Include(s => s.RefreshTokens)
                    .FirstOrDefaultAsync(s => s.UserId == userId && s.SessionId == sessionId);

                if (session == null)
                    return RESP_BadRequestResponse("Session not found");

                // Delete only refresh tokens linked to this session
                _context.RefreshTokens.RemoveRange(session.RefreshTokens);

                // Delete the current session
                _context.UserSessions.Remove(session);

                await _context.SaveChangesAsync();

                // Clear cookies for current session
                Response.Cookies.Delete("refreshToken");
                Response.Cookies.Delete("sessionId");

                return RESP_Success("Logged out from current session only");
            }
            catch (Exception ex)
            {
                return RESP_BadRequestResponse(ex.Message);
            }
        }


        [HttpPost("logout-all")]
        public async Task<IActionResult> LogoutAll()
        {
            try
            {
                var userId = GetUserId(); // your helper to extract userId from JWT claims

                var sessions = await _context.UserSessions
                    .Include(s => s.RefreshTokens)
                    .Where(s => s.UserId == userId)
                    .ToListAsync();

                if (!sessions.Any())
                    return RESP_BadRequestResponse("No sessions found");

                // Delete all refresh tokens for all sessions
                foreach (var session in sessions)
                {
                    _context.RefreshTokens.RemoveRange(session.RefreshTokens);
                }

                // Delete all sessions
                _context.UserSessions.RemoveRange(sessions);

                await _context.SaveChangesAsync();

                // Clear cookies
                Response.Cookies.Delete("refreshToken");
                Response.Cookies.Delete("sessionId");

                return RESP_Success("Logged out from all sessions");
            }
            catch (Exception ex)
            {
                return RESP_BadRequestResponse(ex.Message);
            }
        }


        [HttpDelete("sessions/{sessionId}")]
        public async Task<IActionResult> RemoveSession(string sessionId)
        {
            try
            {
                var userId = GetUserId(); // Implement as previously discussed

                await _authRepo.RemoveSessionAsync(userId, sessionId);

                return RESP_Success("Session removed successfully");
            }
            catch (Exception ex)
            {
                return RESP_BadRequestResponse(ex.Message);
            }
        }

        [HttpPost("request-password-recovery")]
        public async Task<IActionResult> RequestPasswordRecovery([FromBody] PasswordRecoveryRequest request)
        {
            try
            {
                // Check if user exists
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (user == null)
                    return RESP_BadRequestResponse("User not found");

                var recovery = await _authRepo.CreatePasswordRecoveryAsync(user.Id);

                // TODO: send recovery email using MailService with recovery.Token

                return RESP_Success("Password recovery initiated");
            }
            catch (Exception ex)
            {
                return RESP_BadRequestResponse(ex.Message);
            }
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                await _authRepo.ResetPasswordAsync(request.Token, request.NewPassword);
                return RESP_Success( "Password reset successful");
            }
            catch (Exception ex)
            {
                return RESP_BadRequestResponse(ex.Message);
            }
        }

        // Helper to get userId from JWT claims
        private Guid GetUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId" || c.Type == "sub");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
                throw new Exception("Invalid user ID claim");
            return userId;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashed)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashed);
        }
    }

    public class SignupRequest
    {
        public User User { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class GoogleLoginRequest
    {
        public string GoogleUserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
    }
    public class EmailRequest
    {
        public string Email { get; set; }
    }

    public class TokenRequest
    {
        public string Token { get; set; }
    }
    public class PasswordRecoveryRequest
    {
        public string Email { get; set; }
    }

    public class ResetPasswordRequest
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
