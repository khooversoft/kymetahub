using KymetaHub.sdk.Clients;
using KymetaHub.sdk.Models;
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

        [HttpPost("WipDispositionOut/{workOrderId}")]
        public async Task<IActionResult> WipDispositionOut(int workOrderId, CancellationToken token)
        {
            if (workOrderId < 0) return BadRequest("Work order invalid");

            WipDispositionOutResponse? response = await _wipDispositionOutActor.Run(workOrderId, token);

            return response switch
            {
                null => BadRequest(),
                not null => Ok(response),
            };
        }
    }
}
