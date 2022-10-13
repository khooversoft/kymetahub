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

    public async Task<(bool success, string? response)> CreateWorkOrder(CreateWorkOrderRequest createWorkOrderRequest, CancellationToken token = default)
    {
        createWorkOrderRequest.NotNull();
        _logger.LogEntryExit();
        _logger.LogInformation("Posting CreateWorkOrder for object={object}", createWorkOrderRequest.ToJsonPascal());

        string json = createWorkOrderRequest.ToJsonPascal();
        var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync("workOrders", requestContent, token);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed call to Oracle, message={message}", responseContent);
            return (false, responseContent);
        }

        return (true, responseContent);
    }
}
