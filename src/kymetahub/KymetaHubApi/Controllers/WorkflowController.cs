using KymetaHub.sdk.Clients;
using KymetaHub.sdk.Models.Delmia;
using KymetaHub.sdk.Models.Orcale;
using KymetaHub.sdk.Services;
using KymetaHub.sdk.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KymetaHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly WipDispositionOutActor _wipDispositionOutActor;
        private readonly ILogger<WorkflowController> _logger;

        public WorkflowController(WipDispositionOutActor wipDispositionOutService, ILogger<WorkflowController> logger)
        {
            _wipDispositionOutActor = wipDispositionOutService;
            _logger = logger;
        }

        [HttpGet("WipDispositionOut/{workOrderId}/{creationDate}")]
        public async Task<IActionResult> WipDispositionOut(int workOrderId, DateTime creationDate, CancellationToken token)
        {
            if (workOrderId < 0) return BadRequest("Work order invalid");

            CreateWorkOrderResponse? response = await _wipDispositionOutActor.Run(workOrderId, creationDate, token);
            return Ok(response);
        }
    }
}
