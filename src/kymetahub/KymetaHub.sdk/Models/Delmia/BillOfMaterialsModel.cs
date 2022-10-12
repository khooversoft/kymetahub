using KymetaHub.sdk.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Models.Delmia;

public record BillOfMaterialsModel
{
    public BillOfMaterialsData Data { get; init; } = null!;
}

public class BillOfMaterialsData
{
    public string? MfgType { get; set; }
}

public static class BillOfMaterialsDataExtensions
{
    public static bool IsValid(this BillOfMaterialsModel subject)
    {
        return subject != null &&
            subject.Data.IsValid();
    }

    public static bool IsValid(this BillOfMaterialsData subject)
    {
        return subject != null &&
            !subject.MfgType.IsEmpty();
    }
}