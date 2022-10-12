using KymetaHub.sdk.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace kymetahub.test.Application;

internal static class TestApplication
{
    private static bool _initialized = false;
    private static WebApplicationFactory<Program> _host = null!;
    private static KmtaClient? _client;
    private static KymetaHubApiClient? _apiClient;
    private static object _lock = new object();

    public static void StartHost()
    {
        lock (_lock)
        {
            if (_initialized) return;

            ILogger logger = LoggerFactory.Create(builder =>
            {
                builder.AddDebug();
                builder.AddFilter(x => true);
            }).CreateLogger<Program>();

            _host = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(service =>
                    {
                        ConfigureModelBindingExceptionHandling(service, logger);
                    });
                });

            _initialized = true;
        }
    }

    public static KmtaClient GetKmtaClient()
    {
        lock (_lock)
        {
            if (_client != null) return _client;

            StartHost();
            return _client = _host.Services.GetRequiredService<KmtaClient>();
        }
    }

    public static KymetaHubApiClient GetKymetaHubApiClient()
    {
        lock (_lock)
        {
            if (_apiClient != null) return _apiClient;

            StartHost();

            HttpClient client = _host.CreateClient();
            return _apiClient = new KymetaHubApiClient(client, _host.Services.GetRequiredService<ILoggerFactory>().CreateLogger<KymetaHubApiClient>());
        }
    }

    private static void ConfigureModelBindingExceptionHandling(IServiceCollection services, ILogger logger)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                ValidationProblemDetails? error = actionContext.ModelState
                    .Where(e => e.Value?.Errors.Count > 0)
                    .Select(e => new ValidationProblemDetails(actionContext.ModelState))
                    .FirstOrDefault();

                logger.LogError("ApiBehaviorOption error");

                // Here you can add logging to you log file or to your Application Insights.
                // For example, using Serilog:
                // Log.Error($"{{@RequestPath}} received invalid message format: {{@Exception}}", 
                //   actionContext.HttpContext.Request.Path.Value, 
                //   error.Errors.Values);
                return new BadRequestObjectResult(error);
            };
        });
    }
}
