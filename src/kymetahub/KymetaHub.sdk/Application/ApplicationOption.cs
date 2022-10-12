using KymetaHub.sdk.Tools;

namespace KymetaHub.sdk.Application;

public record ApplicationOption
{
    public string KmtaUrl { get; init; } = null!;
    public KmtaLoginOption KmtaLogin { get; init; } = null!;
}

public record KmtaLoginOption
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

        return option;
    }

    public static void Verify(this KmtaLoginOption option)
    {
        option.NotNull();
        option.UserName.NotEmpty();
        option.Password.NotEmpty();
    }
}