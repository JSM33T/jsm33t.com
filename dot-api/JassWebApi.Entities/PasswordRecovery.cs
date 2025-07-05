using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JassWebApi.Entities
{
    public class PasswordRecovery
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool IsUsed { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation to User
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
