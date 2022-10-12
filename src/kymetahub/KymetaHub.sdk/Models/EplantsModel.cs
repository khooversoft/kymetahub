using KymetaHub.sdk.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Models;

public record EplantsModel
{
    public IReadOnlyList<EplantsData> Data { get; init; } = Array.Empty<EplantsData>();
}

public record EplantsData
{
    public int ID { get; set; }
    public string? PlantName { get; set; }
    public string? CompanyName { get; set; }
}

public static class EplantsModelExtensions
{
    public static bool IsValid(this EplantsModel subject)
    {
        return subject != null &&
            subject.Data.Count > 0 &&
            subject.Data.All(x => x.IsValid());
    }

    public static bool IsValid(this EplantsData subject)
    {
        return subject != null &&
            !subject.PlantName.IsEmpty() &&
            !subject.CompanyName.IsEmpty();
    }
}