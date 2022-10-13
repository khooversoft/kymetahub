using KymetaHub.sdk.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Extensions;

public static class ByteExtensions
{
    public static byte[] ToBytes(this string? subject)
    {
        return subject switch
        {
            null => Array.Empty<byte>(),
            not null => Encoding.UTF8.GetBytes(subject),
        };
    }

    public static string ToJson<T>(this T subject) => Json.Default.Serialize(subject);
    public static string ToJsonPascal<T>(this T subject) => Json.Default.SerializePascal(subject);

    public static T? ToObject<T>(this string json)
    {
        return json switch
        {
            string v when v.IsEmpty() => default,
            _ => Json.Default.Deserialize<T>(json),
        };
    }
}
