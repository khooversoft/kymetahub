using KymetaHub.sdk.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Models;

public record SalesOrderForWorkOrderModel
{
    public IReadOnlyList<SalesOrderForWorkOrderData> Data { get; init; } = Array.Empty<SalesOrderForWorkOrderData>();
}

public record SalesOrderForWorkOrderData
{
    public int EPlantId { get; init; }
    public string? OrderNumber { get; init; }
    public int OrdDetailId { get; init; }
}


public static class SalesOrderForWorkOrderModelExtensions
{
    public static bool IsValid(this SalesOrderForWorkOrderModel subject)
    {
        return subject != null &&
            subject.Data.Count > 0 &&
            subject.Data.All(x => x.IsValid());
    }

    public static bool IsValid(this SalesOrderForWorkOrderData subject)
    {
        return subject != null &&
            !subject.OrderNumber.IsEmpty();
    }
}
