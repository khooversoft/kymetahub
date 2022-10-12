using KymetaHub.sdk.Extensions;
using KymetaHub.sdk.Models.Delmia;
using KymetaHub.sdk.Models.Ping;
using KymetaHub.sdk.Tools;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Clients;

public class KymetaHubApiClient
{
    private readonly HttpClient _client;
    private readonly ILogger<KymetaHubApiClient> _logger;

    public KymetaHubApiClient(HttpClient client, ILogger<KymetaHubApiClient> logger)
    {
        _client = client.NotNull();
        _logger = logger.NotNull();
    }

    public async Task<WipDispositionOutResponse> WipDispositionOut(int workOrderId, CancellationToken token = default)
    {
        _logger.LogEntryExit();
        _logger.LogInformation("WipDispositionOut for workOrderId={workOrderId}", workOrderId);

        HttpResponseMessage response = await _client.PostAsync($"api/Workflow/WipDispositionOut/{workOrderId}", null);
        response.EnsureSuccessStatusCode();

        string content = await response.Content.ReadAsStringAsync();
        return Json.Default.Deserialize<WipDispositionOutResponse>(content).NotNull();
    }

    public async Task<PingResponse> Ping(CancellationToken token = default)
    {
        return (await _client.GetFromJsonAsync<PingResponse>("api/ping", token)).NotNull();
    }
}
