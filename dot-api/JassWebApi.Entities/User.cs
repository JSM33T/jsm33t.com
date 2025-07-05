using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JassWebApi.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Avatar { get; set; } = "";

        public string UserName { get; set; } = "";

        public string Role { get; set; } = "user";

        public string Email { get; set; }

        public string? GoogleUserId { get; set; }

        public string? PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsVerified { get; set; } = false;

        public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
    }
    public class VerificationToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool IsUsed { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class UserSignUpRequest
    {
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Avatar { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; } = "user";

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
