using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JassWebApi.Entities.Shared
{
    public class ApiResponse<T>(int status, string message, T data, List<string> hints = null)
    {
        public int Status { get; set; } = status;
        public string Message { get; set; } = message;
        public T Data { get; set; } = data;
        public List<string> Hints { get; set; } = hints ?? [];
    }
}
