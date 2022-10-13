using KymetaHub.sdk.Clients;
using KymetaHub.sdk.Extensions;
using KymetaHub.sdk.Models.Delmia;
using KymetaHub.sdk.Models.Orcale;
using KymetaHub.sdk.Tools;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Services;

public class WipDispositionOutActor
{
    private readonly KmtaClient _client;
    private readonly ILogger<WipDispositionOutActor> _logger;
    private readonly OracleClient _oracleClient;

    public WipDispositionOutActor(KmtaClient kmtaClient, OracleClient oracleClient, ILogger<WipDispositionOutActor> logger)
    {
        _client = kmtaClient.NotNull();
        _oracleClient = oracleClient.NotNull();
        _logger = logger.NotNull();
    }

    public async Task<CreateWorkOrderResponse> Run(int workOrderId, DateTime creationDate, CancellationToken token)
    {
        _logger.LogEntryExit();
        _logger.LogInformation("Running for workOrderId={workOrderId}", workOrderId);

        WipDispositionOutResponse? wipResponse = await CollectData(workOrderId, token);
        if (wipResponse == null) return new CreateWorkOrderResponse();

        (bool success, string? response) updateReponse = await UpdateSync(wipResponse, workOrderId, creationDate, token);

        return new CreateWorkOrderResponse
        {
            WipResponse = wipResponse,
            Success = updateReponse.success,
            OrcaleResponse = updateReponse.response,
        };
    }

    private async Task<WipDispositionOutResponse?> CollectData(int workOrderId, CancellationToken token)
    {
        try
        {
            WorkOrderModel workOrderModel = await _client.GetWorkOrder(workOrderId, token);
            WorkOrderPartForWorkOrderModel workOrderPartForWorkOrderModel = await _client.GetWorkOrderPartForWorkOrderModel(workOrderId, token);
            WorkOrderPartsModel workOrderPartsModel = await _client.GetWorkOrderParts(workOrderId, token);

            SalesOrderForWorkOrderModel salesOrderForWorkOrderModel = await _client.GetSalesOrderForWorkOrder(workOrderId, workOrderPartsModel.Data.First().PartNoId, token);
            EplantsModel eplantsModel = await _client.GetEplantsModel(workOrderModel.Data.First().EplantID, token);

            BillOfMaterialsModel billOfMaterialsModel = await _client.GetBillOfMaterials(workOrderModel.Data.First().StandardID, token);

            return new WipDispositionOutResponse
            {
                WorkOrder = workOrderModel,
                WorkOrderPartForWorkOrder = workOrderPartForWorkOrderModel,
                WorkOrderParts = workOrderPartsModel,
                SalesOrderForWorkOrder = salesOrderForWorkOrderModel,
                Eplants = eplantsModel,
                BillOfMaterials = billOfMaterialsModel,
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Workflow for workOrderId={workOrderId} failed", workOrderId);
            return null;
        }
    }

    private async Task<(bool success, string? response)> UpdateSync(WipDispositionOutResponse wip, int workOrderId, DateTime creationDate, CancellationToken token)
    {
        _logger.LogEntryExit();
        _logger.LogInformation("Posting to sync: object={wip}", wip.ToJson());

        var request = new CreateWorkOrderRequest
        {
            WorkOrderNumber = workOrderId.ToString(),
            ItemNumber = wip.WorkOrderPartForWorkOrder.Data.First().ItemNo,
            PlannedStartQuantity = wip.WorkOrderPartForWorkOrder.Data.First().Quantity,
            PlannedStartDate = wip.WorkOrder.Data.First().StartDate,

        }.Assert(x => x.IsValid(), "Invalid work order require");

        (bool success, string? response) response = await _oracleClient.CreateWorkOrder(request, token);
        if (response.success) _logger.LogInformation("Posting success, response={response}", response);
        return response;
    }
}
