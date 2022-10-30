using KymetaHub.sdk.Clients;
using KymetaHub.sdk.Models.Delmia;
using KymetaHub.sdk.Services;
using KymetaHub.sdk.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KymetaHub.sdk.Models.Requests;
using KymetaHub.sdk.Models.Responses;

namespace KymetaHubApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkflowController : ControllerBase
{
    private readonly WorkOrderCreateActor _wipDispositionOutActor;
    private readonly ILogger<WorkflowController> _logger;

    public WorkflowController(WorkOrderCreateActor wipDispositionOutService, ILogger<WorkflowController> logger)
    {
        _wipDispositionOutActor = wipDispositionOutService;
        _logger = logger;
    }

    [HttpGet("createWorkOrder/{workOrderId}/{creationDate}")]
    public async Task<IActionResult> CreateWorkOrder(int workOrderId, DateTime creationDate, CancellationToken token)
    {
        if (workOrderId < 0) return BadRequest("Work order invalid");

        CreateWorkOrderResponse? response = await _wipDispositionOutActor.Run(workOrderId, creationDate, token);
        return Ok(response);
    }

    [HttpPost("updateWorkOrder")]
    public Task<IActionResult> WorkOrderUpdate([FromBody] UpdateWorkOrderRequest request)
    {
        UpdateWorkOrderResponse response = new UpdateWorkOrderResponse
        {
            Success = true,
            WorkOrderId = request.WORKORDER_ID,
            Message = "Workorder updated",
        };

        return Task.FromResult<IActionResult>(Ok(response));
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
