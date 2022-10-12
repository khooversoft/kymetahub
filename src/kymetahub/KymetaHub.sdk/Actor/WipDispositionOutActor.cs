using KymetaHub.sdk.Clients;
using KymetaHub.sdk.Models;
using KymetaHub.sdk.Tools;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Services;

public class WipDispositionOutActor
{
    private readonly KmtaClient _client;
    private readonly ILogger<WipDispositionOutActor> _logger;

    public WipDispositionOutActor(KmtaClient kmtaClient, ILogger<WipDispositionOutActor> logger)
    {
        _client = kmtaClient.NotNull();
        _logger = logger.NotNull();
    }

    public async Task<bool> Run(int workOrderId, CancellationToken token)
    {
        try
        {
            WorkOrderModel workOrderModel = await _client.GetWorkOrder(workOrderId, token);
            WorkOrderPartForWorkOrderModel workOrderPartForWorkOrderModel = await _client.GetWorkOrderPartForWorkOrderModel(workOrderId, token);
            WorkOrderPartsModel workOrderPartsModel = await _client.GetWorkOrderParts(workOrderId, token);

            SalesOrderForWorkOrderModel salesOrderForWorkOrderModel = await _client.GetSalesOrderForWorkOrder(workOrderId, workOrderPartsModel.Data.First().PartNoId, token);
            EplantsModel eplantsModel = await _client.GetEplantsModel(workOrderModel.Data.First().EplantID, token);

            BillOfMaterialsModel billOfMaterialsModel = await _client.GetBillOfMaterials(workOrderModel.Data.First().StandardID, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Workflow for workOrderId={workOrderId} failed", workOrderId);
            return false;
        }

        return true;
    }
}
