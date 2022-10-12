using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KymetaHub.sdk.Tools;

namespace KymetaHub.sdk.Extensions;

public static class FunctionExtensions
{
    /// <summary>
    /// Execute function
    /// </summary>
    /// <typeparam name="T">subject type</typeparam>
    /// <typeparam name="TResult">return type</typeparam>
    /// <param name="subject">subject</param>
    /// <param name="function">lambda execute</param>
    /// <returns>return from lambda</returns>
    public static TResult Func<T, TResult>(this T subject, Func<T, TResult> function) => function.NotNull()(subject);

    /// <summary>
    /// Execute action
    /// </summary>
    /// <typeparam name="T">any type</typeparam>
    /// <param name="subject">subject</param>
    /// <param name="action">action</param>
    /// <returns>subject</returns>
    public static T Action<T>(this T subject, Action<T> action)
    {
        subject.NotNull();
        action.NotNull();

        action(subject);
        return subject;
    }
}
