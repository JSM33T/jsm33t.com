using Microsoft.AspNetCore.Mvc;

namespace JassWebApi.Base.Controllers
{
	[ApiController]
	[Route("/")]
	public class SystemController : ControllerBase
	{
		private readonly ILogger<SystemController> _logger;

		public SystemController(ILogger<SystemController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok("Under Rennovation | Server Up and Running");
		}
	}
}
