using JassWebApi.Entities.Shared;
using Microsoft.AspNetCore.Mvc;

namespace JassWebApi.Base.Controllers
{
    [ApiController]
    [Route("/")]
    public class SystemController(ILogger<SystemController> logger, JsmtConfig jsmtConfig) : JsmtBaseController
    {
        private readonly ILogger<SystemController> _logger = logger;
        private readonly JsmtConfig _jsmtConfig = jsmtConfig;

        [HttpGet]
        public IActionResult Get()
        {
            var smtpServer = _jsmtConfig.SMTP.Server;
            var smtpPort = _jsmtConfig.SMTP.Port;
            var smtpUsername = _jsmtConfig.SMTP.UserName;
            var smtpPassword = _jsmtConfig.SMTP.Password;
            var useSSL = _jsmtConfig.SMTP.UseSSL;

            var msSqlConnectionString = _jsmtConfig.ConnectionSettings.MsSqlConstr;

            // Return all configurations
            var rr = new
            {
                SmtpServer = smtpServer,
                SmtpPort = smtpPort,
                SmtpUsername = smtpUsername,
                SmtpPassword = smtpPassword,
                UseSSL = useSSL,
                MsSqlConnectionString = msSqlConnectionString
            };

            return RESP_Success(rr);
        }
    }
}
