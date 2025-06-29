using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JassWebApi.Entities
{
    public class ChangeLog
    {
        public int Id { get; set; }
        public string Version { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Added, Fixed, Removed, etc.
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class AddChangeLogRequest
    {
        public string Version { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class ChangeLogDto
    {
        public string Version { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
