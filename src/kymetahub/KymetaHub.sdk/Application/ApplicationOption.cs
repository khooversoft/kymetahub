using KymetaHub.sdk.Tools;

namespace KymetaHub.sdk.Application;

public record ApplicationOption
{
    public string KmtaUrl { get; init; } = null!;
    public string OracleUrl { get; init; } = null!;
    public LoginOption KmtaLogin { get; init; } = null!;
    public LoginOption OracleLogin { get; init; } = null!;
}

public record LoginOption
{
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
}


public static class ApplicationOptionExtensions
{
    public static ApplicationOption Verify(this ApplicationOption option)
    {
        option.NotNull();
        option.KmtaUrl.NotEmpty();
        option.KmtaLogin.Verify();
        option.OracleLogin.Verify();

        return option;
    }

    public static void Verify(this LoginOption option)
    {
        option.NotNull();
        option.UserName.NotEmpty();
        option.Password.NotEmpty();
    }
}