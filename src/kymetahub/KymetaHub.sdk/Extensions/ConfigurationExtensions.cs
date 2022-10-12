using KymetaHub.sdk.Tools;
using Microsoft.Extensions.Configuration;

namespace KymetaHub.sdk.Extensions;

public static class ConfigurationExtensions
{
    public static T Bind<T>(this IConfiguration configuration) where T : new()
    {
        configuration.NotNull();

        var option = new T();
        configuration.Bind(option, x => x.BindNonPublicProperties = true);
        return option;
    }
}