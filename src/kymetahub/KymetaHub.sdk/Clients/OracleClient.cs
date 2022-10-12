using KymetaHub.sdk.Extensions;
using KymetaHub.sdk.Models.Orcale;
using KymetaHub.sdk.Tools;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Clients;

public class OracleClient
{
    private readonly HttpClient _client;
    private readonly ILogger<OracleClient> _logger;

    public OracleClient(HttpClient client, ILogger<OracleClient> logger)
    {
        _client = client.NotNull();
        _logger = logger.NotNull();
    }

    public async Task<bool> CreateWorkOrder(CreateWorkOrderRequest createWorkOrderRequest, CancellationToken token = default)
    {
        createWorkOrderRequest.NotNull();
        _logger.LogEntryExit();
        _logger.LogInformation("Posting UpdateWorkOrder for object={object}", createWorkOrderRequest.ToJson());

        HttpResponseMessage response = await _client.PostAsJsonAsync("workOrders", createWorkOrderRequest, token);
        response.EnsureSuccessStatusCode();

        return true;
    }

    public async Task<WorkOrderResponse> GetWorkOrder(string itemNumber, CancellationToken token = default)
    {
        itemNumber.NotEmpty();
        _logger.LogEntryExit();
        _logger.LogInformation("Posting UpdateWorkOrder for itemNumber={itemNumber}", itemNumber);

        WorkOrderResponse response = (await _client.GetFromJsonAsync<WorkOrderResponse>("workOrders", token)).NotNull();

    }
}
