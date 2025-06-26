using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JassWebApi.Base.Controllers
{
    [ApiController]
    [Route("/")]
    public class SystemController : ControllerBase
    {
        private readonly ILogger<SystemController> _logger;
        private readonly IConfiguration _configuration;

        // Injecting IConfiguration to read from appsettings.json and environment variables
        public SystemController(ILogger<SystemController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Read SMTP server address from appsettings.json -> SMTP -> Server
            var smtpServer = _configuration.GetValue<string>("SMTP:Server");

            // If it's null or empty, set a default message
            if (string.IsNullOrEmpty(smtpServer))
            {
                smtpServer = "SMTP Server not set.";
            }

            // Return the SMTP server address
            return Ok($"SMTP Server: {smtpServer}");
        }
    }
}
