using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JassWebApi.Entities
{
    public class UserSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string SessionId { get; set; } // client-generated UUID

        public string UserAgent { get; set; }

        public string Ip { get; set; }

        public bool IsLoggedIn { get; set; } = true; // true when session is active

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime LastUsedAt { get; set; } = DateTime.UtcNow;

        public DateTime ValidUntil { get; set; } // session expiry

        // Navigation to User
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        // Navigation to RefreshToken
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }

    public class UserSessionDto
    {
        public string SessionId { get; set; }
        public string UserAgent { get; set; }
        public string Ip { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsLoggedIn { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}
