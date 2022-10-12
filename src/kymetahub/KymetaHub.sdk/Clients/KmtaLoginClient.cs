using KymetaHub.sdk.Extensions;
using KymetaHub.sdk.Tools;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Clients;

public class KmtaLoginClient
{
    private readonly HttpClient _client;
    private readonly ILogger<KmtaLoginClient> _logger;

    public KmtaLoginClient(HttpClient client, ILogger<KmtaLoginClient> logger)
    {
        _client = client.NotNull();
        _logger = logger.NotNull();
    }

    public async Task<string> Login(string userName, string password, CancellationToken token = default)
    {
        userName.NotEmpty();
        password.NotEmpty();
        _logger.LogEntryExit();

        var body = new
        {
            userName = userName,
            password = password,
        };

        HttpResponseMessage response = await _client.PostAsJsonAsync("user/login", body, token);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var data = Json.Default.Deserialize<AuthData>(content).NotNull();
        return data.AuthToken;
    }

    private record AuthData
    {
        public string AuthToken { get; init; } = null!;
    }
}
