using Microsoft.AspNetCore.Mvc;

namespace JassWebApi.Base.Controllers
{
	[ApiController]
	[Route("/")]
	public class SystemController : ControllerBase
	{
		private readonly ILogger<SystemController> _logger;

		// Injecting the logger and using Environment.GetEnvironmentVariable to fetch the SMTP_SERVER
		public SystemController(ILogger<SystemController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			// Read SMTP_SERVER from the environment variables
			var smtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER");

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
