﻿using KymetaHub.sdk.Tools;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task WipDispositionOut(int workOrderId, CancellationToken token = default)
    {
        HttpResponseMessage response = await _client.PostAsync($"api/Workflow/WipDispositionOut/{workOrderId}", null);
        response.EnsureSuccessStatusCode();
    }
}
