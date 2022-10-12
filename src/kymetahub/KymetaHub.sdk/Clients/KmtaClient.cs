using KymetaHub.sdk.Extensions;
using KymetaHub.sdk.Models;
using KymetaHub.sdk.Tools;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Clients;

public class KmtaClient
{
    private readonly HttpClient _client;
    private readonly ILogger<KmtaClient> _logger;

    public KmtaClient(HttpClient client, ILogger<KmtaClient> logger)
    {
        _client = client.NotNull();
        _logger = logger.NotNull();
    }

    public async Task<WorkOrderModel> GetWorkOrder(int workOrderId, CancellationToken token = default)
    {
        _logger.LogEntryExit();
        _logger.LogInformation("Getting work order orderId={orderId}", workOrderId);

        var response = await _client.GetFromJsonAsync<WorkOrderModel>($"Manufacturing/WorkOrders/WorkOrders?Filter=(ID.eq~{workOrderId}~)", token);
        response.NotNull();
        response.IsValid().Assert(x => x == true, $"Model for orderId={workOrderId}");

        return response;
    }

    public async Task<WorkOrderPartForWorkOrderModel> GetWorkOrderPartForWorkOrderModel(int workOrderId, CancellationToken token = default)
    {
        _logger.LogEntryExit();
        _logger.LogInformation("Getting work order parts for work order orderId={orderId}", workOrderId);

        var response = await _client.GetFromJsonAsync<WorkOrderPartForWorkOrderModel>($"Manufacturing/WorkOrders/WorkOrderPartsForWorkOrder/{workOrderId}", token);
        response.NotNull();
        response.IsValid().Assert(x => x == true, $"Model for orderId={workOrderId}");

        return response;
    }

    public async Task<WorkOrderPartsModel> GetWorkOrderParts(int workOrderId, CancellationToken token = default)
    {
        _logger.LogEntryExit();
        _logger.LogInformation("Getting work order parts orderId={orderId}", workOrderId);

        string requestUri = $"Manufacturing/WorkOrders/WorkOrderParts?Filter=(WorkOrderID.eq~{workOrderId}~)";
        var response = await _client.GetFromJsonAsync<WorkOrderPartsModel>(requestUri, token);
        response.NotNull();
        response.IsValid().Assert(x => x == true, $"Model for orderId={workOrderId}");

        return response;
    }

    public async Task<SalesOrderForWorkOrderModel> GetSalesOrderForWorkOrder(int workOrderId, int partNoId, CancellationToken token = default)
    {
        _logger.LogEntryExit();
        _logger.LogInformation("Getting sales order for work order, orderId={orderId}, partNoId={partNoId}", workOrderId, partNoId);

        string requestUri = $"Manufacturing/WorkOrders/SalesOrdersForWorkOrder?workOrderId={workOrderId}&partNoId={partNoId}";
        var response = await _client.GetFromJsonAsync<SalesOrderForWorkOrderModel>(requestUri, token);
        response.NotNull();
        response.IsValid().Assert(x => x == true, $"Model for orderId={workOrderId}, partNoId={partNoId}");

        return response;
    }
    
    public async Task<EplantsModel> GetEplantsModel(int eplantID, CancellationToken token = default)
    {
        _logger.LogEntryExit();
        _logger.LogInformation("Getting EPlants, eplantID={eplantID}", eplantID);

        string requestUri = $"AssemblyData/FinalAssembly/GetEplants?Filter=(ID.eq~{eplantID}~)";
        var response = await _client.GetFromJsonAsync<EplantsModel>(requestUri, token);
        response.NotNull();
        response.IsValid().Assert(x => x == true, $"Model for eplantID={eplantID}");

        return response;
    }

    public async Task<BillOfMaterialsModel> GetBillOfMaterials(int standardId, CancellationToken token = default)
    {
        _logger.LogEntryExit();
        _logger.LogInformation("Getting bill of materials, standardId={standardId}", standardId);

        string requestUri = $"Manufacturing/BOM/BillOfMaterial/{standardId}";
        var response = await _client.GetFromJsonAsync<BillOfMaterialsModel>(requestUri, token);
        response.NotNull();
        response.IsValid().Assert(x => x == true, $"Model for standardId={standardId}");

        return response;
    }
}
