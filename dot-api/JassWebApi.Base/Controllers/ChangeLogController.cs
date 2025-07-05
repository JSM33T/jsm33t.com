using System.Threading.Tasks;
using JassWebApi.Base.Filters;
using JassWebApi.Data;
using JassWebApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JassWebApi.Base.Controllers
{
    [Route("api/changelog")]
    public class ChangeLogController(IChangeLogRepository changeLogRepository) : JsmtBaseController
    {
        private readonly IChangeLogRepository _changeLogRepository = changeLogRepository;

        // GET: api/changelog
        [Persist(5)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var logs = _changeLogRepository.GetAll();
            return RESP_Success(logs);
        }

        // GET: api/changelog/version/{version}
        [HttpGet("version/{version}")]
        public IActionResult GetByVersion(string version)
        {
            var log = _changeLogRepository.GetByVersion(version);
            if (log == null)
                return RESP_NotFoundResponse($"ChangeLog with version {version} not found ");
            return RESP_Success(log);
        }

        // POST: api/changelog
        [HttpPost]
        public IActionResult Insert([FromBody] ChangeLog changeLog)
        {
            var inserted = _changeLogRepository.Insert(changeLog);
            return RESP_Success(inserted, "Inserted successfully");
        }

        // POST: api/changelog/delete-by-version
        [HttpPost("delete-by-version")]
        public IActionResult DeleteByVersion([FromBody] DeleteByVersionRequest request)
        {
            var deleted = _changeLogRepository.DeleteByVersion(request.Version);
            if (deleted)
                return RESP_Success<object>(null, $"Deleted ChangeLog with version {request.Version}");
            return RESP_NotFoundResponse($"ChangeLog with version {request.Version} not found");
        }
    }

    public class DeleteByVersionRequest
    {
        public required string Version { get; set; }
    }
}
