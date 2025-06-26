using JassWebApi.Entities.Shared;
using Microsoft.AspNetCore.Mvc;

namespace JassWebApi.Base.Controllers
{
    [ApiController]
    [Route("/")]
    public class SystemController : ControllerBase
    {
        private readonly ILogger<SystemController> _logger;
        private readonly JsmtConfig _jsmtConfig;

        // Injecting JsmtConfig directly
        public SystemController(ILogger<SystemController> logger, JsmtConfig jsmtConfig)
        {
            _logger = logger;
            _jsmtConfig = jsmtConfig;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Access SMTP configuration values
            var smtpServer = _jsmtConfig.SMTP.Server;
            var smtpPort = _jsmtConfig.SMTP.Port;
            var smtpUsername = _jsmtConfig.SMTP.UserName;
            var smtpPassword = _jsmtConfig.SMTP.Password;
            var useSSL = _jsmtConfig.SMTP.UseSSL;

            // Access ConnectionSettings (SQL connection string)
            var msSqlConnectionString = _jsmtConfig.ConnectionSettings.MsSqlConstr;

            // Return all configurations
            return Ok(new
            {
                SmtpServer = smtpServer,
                SmtpPort = smtpPort,
                SmtpUsername = smtpUsername,
                SmtpPassword = smtpPassword,
                UseSSL = useSSL,
                MsSqlConnectionString = msSqlConnectionString
            });
        }
    }
}
