using KymetaHub.sdk.Tools;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Extensions;

public static class LoggingExtensions
{
    public static IDisposable LogEntryExit(
    this ILogger logger,
    [CallerMemberName] string function = "",
    [CallerFilePath] string path = "",
    [CallerLineNumber] int lineNumber = 0
    )
    {
        logger
            .NotNull()
            .LogTrace("Enter: Method={method}, path={path}, line={lineNumber}", function, path, lineNumber);

        var sw = Stopwatch.StartNew();

        return new FinalizeScope<ILogger>(logger, x =>
            x.LogTrace("Exit: ms={ms} Method={method}, path={path}, line={lineNumber}", sw.ElapsedMilliseconds, function, path, lineNumber)
            );
    }
}
