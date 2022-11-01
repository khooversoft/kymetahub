using KymetaHub.sdk.Clients;
using KymetaHub.sdk.Extensions;
using KymetaHub.sdk.Models.Delmia;
using KymetaHub.sdk.Models.Orcale;
using KymetaHub.sdk.Models.Requests;
using KymetaHub.sdk.Models.Responses;
using KymetaHub.sdk.Services;
using KymetaHub.sdk.Tools;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Actor;

public class WorkOrderUpdateActor
{
    private readonly KmtaClient _client;
    private readonly ILogger<WorkOrderCreateActor> _logger;
    private readonly OracleClient _oracleClient;

    public WorkOrderUpdateActor(KmtaClient kmtaClient, OracleClient oracleClient, ILogger<WorkOrderCreateActor> logger)
    {
        _client = kmtaClient.NotNull();
        _oracleClient = oracleClient.NotNull();
        _logger = logger.NotNull();
    }

    public async Task<WorkOrderUpdateResponse> Run(WorkOrderUpdateRequest request, CancellationToken token)
    {
        _logger.LogEntryExit();

        int workOrderId = int.Parse(request.WORKORDER_ID);
        _logger.LogInformation("Updating workorder for workOrderId={workOrderId} (data={data}", workOrderId, request.WORKORDER_ID);

        WipDispositionOutResponse? wipResponse = await CollectData(workOrderId, token);
        if (wipResponse == null) return new WorkOrderUpdateResponse();

        (bool success, string? response) updateReponse = await UpdateSync(wipResponse, workOrderId, token);

        return new WorkOrderUpdateResponse
        {
            Success = updateReponse.success,
            WorkOrderId = request.WORKORDER_ID,
            Message = updateReponse.response,
            Wip = wipResponse,
        };
    }

    private async Task<WipDispositionOutResponse?> CollectData(int workOrderId, CancellationToken token)
    {
        try
        {
            WorkOrderModel workOrderModel = await _client.GetWorkOrder(workOrderId, token);
            WorkOrderPartsModel workOrderPartsModel = await _client.GetWorkOrderParts(workOrderId, token);

            SalesOrderForWorkOrderModel salesOrderForWorkOrderModel = await _client.GetSalesOrderForWorkOrder(workOrderId, workOrderPartsModel.Data.First().PartNoId, token);
            EplantsModel eplantsModel = await _client.GetEplantsModel(workOrderModel.Data.First().EplantID, token);

            BillOfMaterialsModel billOfMaterialsModel = await _client.GetBillOfMaterials(workOrderModel.Data.First().StandardID, token);

            return new WipDispositionOutResponse
            {
                WorkOrder = workOrderModel,
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

    private async Task<(bool success, string? response)> UpdateSync(WipDispositionOutResponse wip, int workOrderId, CancellationToken token)
    {
        _logger.LogEntryExit();
        _logger.LogInformation("Posting to sync: object={wip}", wip.ToJson());

        var request = new WorkOrderUpdateModel
        {
            WorkOrderNumber = workOrderId.ToString(),
            SourceHeaderReference = wip.SalesOrderForWorkOrder.Data.First().OrderNumber,
            SourceLineReferenceId = wip.SalesOrderForWorkOrder.Data.First().OrdDetailId,
            SourceSystemId = wip.SalesOrderForWorkOrder.Data.First().EPlantId,
            OrganizationCode = wip.Eplants.Data.First().PlantName,
            OrganizationName = wip.Eplants.Data.First().CompanyName,
            WorkOrderType = wip.BillOfMaterials.Data.MfgType,

        }.Assert(x => x.IsValid(), "Invalid work order require");

        (bool success, string? response) response = await _oracleClient.UpdateWorkOrder(request, token);
        if (response.success) _logger.LogInformation("Posting success, response={response}", response);
        return response;
    }
}
