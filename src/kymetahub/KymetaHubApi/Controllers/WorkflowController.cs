using KymetaHub.sdk.Clients;
using KymetaHub.sdk.Models.Delmia;
using KymetaHub.sdk.Services;
using KymetaHub.sdk.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KymetaHub.sdk.Models.Requests;
using KymetaHub.sdk.Models.Responses;
using Newtonsoft.Json.Linq;
using KymetaHub.sdk.Actor;
using KymetaHub.sdk.Extensions;

namespace KymetaHubApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkflowController : ControllerBase
{
    private readonly WorkOrderCreateActor _workOrderCreateActor;
    private readonly WorkOrderUpdateActor _workOrderUpdateActor;
    private readonly ILogger<WorkflowController> _logger;

    public WorkflowController(
        WorkOrderCreateActor workOrderCreateActor,
        WorkOrderUpdateActor workOrderUpdateActor,
        ILogger<WorkflowController> logger)
    {
        _workOrderCreateActor = workOrderCreateActor.NotNull();
        _workOrderUpdateActor = workOrderUpdateActor.NotNull();
        _logger = logger.NotNull();
    }

    [HttpGet("createWorkOrder/{workOrderId}/{creationDate}")]
    public async Task<IActionResult> CreateWorkOrder(int workOrderId, DateTime creationDate, CancellationToken token)
    {
        using var lc = _logger.LogEntryExit();
        if (workOrderId < 0)
        {
            _logger.LogError("workOrderId={workOrderId} number is invalid", workOrderId);
            return BadRequest("Work order invalid");
        }

        _logger.LogInformation("Processing workOrderId={workOrderId}", workOrderId);
        CreateWorkOrderResponse? response = await _workOrderCreateActor.Run(workOrderId, creationDate, token);
        return Ok(response);
    }

    [HttpPost("workOrderUpdate")]
    public async Task<IActionResult> WorkOrderUpdate([FromBody] WorkOrderUpdateRequest request, CancellationToken token)
    {
        using var lc = _logger.LogEntryExit();
        if (!int.TryParse(request.WORKORDER_ID, out int workOrderId) && workOrderId > 0) return BadRequest("Work order invalid");

        _logger.LogInformation("Processing workOrderId={workOrderId}", workOrderId);
        WorkOrderUpdateResponse? response = await _workOrderUpdateActor.Run(request, token);
        return Ok(response);

    }

    [HttpPost("workOrderMaterialTrx")]
    public Task<IActionResult> WorkOrderMaterialTrxResponse([FromBody] WorkOrderMaterialTrxRequest request)
    {
        WorkOrderMaterialTrxResponse response = new WorkOrderMaterialTrxResponse
        {
            Success = true,
            WorkOrderId = request.WORKORDER_ID.ToString(),
            Message = "Material trx has been updated",
        };

        return Task.FromResult<IActionResult>(Ok(response));
    }
}
