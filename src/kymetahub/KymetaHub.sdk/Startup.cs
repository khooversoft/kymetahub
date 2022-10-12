using KymetaHub.sdk.Application;
using KymetaHub.sdk.Clients;
using KymetaHub.sdk.Extensions;
using KymetaHub.sdk.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk;

public static class Startup
{
    public static IServiceCollection ConfigureKymeta(this IServiceCollection service)
    {
        service.AddSingleton<WipDispositionOutActor>();

        service.AddHttpClient<KmtaLoginClient>((service, httpClient) =>
        {
            var option = service.GetRequiredService<ApplicationOption>();
            httpClient.BaseAddress = new Uri(option.KmtaUrl);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        service.AddHttpClient<KmtaClient>((service, httpClient) =>
        {
            var option = service.GetRequiredService<ApplicationOption>();
            var client = service.GetRequiredService<KmtaLoginClient>();

            string authToken = client.Login(option.KmtaLogin.UserName, option.KmtaLogin.Password).Result;

            httpClient.BaseAddress = new Uri(option.KmtaUrl);
            httpClient.DefaultRequestHeaders.Add("AuthToken", authToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        service.AddHttpClient<OracleClient>((service, httpClient) =>
        {
            var option = service.GetRequiredService<ApplicationOption>();

            httpClient.BaseAddress = new Uri(option.OracleUrl);

            string basicAuth = $"{option.OracleLogin.UserName}:{option.OracleLogin.Password}"
                .ToBytes()
                .Func(x => Convert.ToBase64String(x));

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basicAuth);
        });

        return service;
    }
}
