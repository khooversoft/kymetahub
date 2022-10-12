using KymetaHub.sdk.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Models;

public record WorkOrderPartForWorkOrderModel
{
    public IReadOnlyList<WorkOrderPartsForWorkOrderData> Data { get; init; } = Array.Empty<WorkOrderPartsForWorkOrderData>();
}

public record WorkOrderPartsForWorkOrderData
{
    public string? ItemNo { get; init; } = null!;
    public int Id { get; init; }
    public int Quantity { get; init; }
    public string? Rev { get; init; }
    public string? Unit { get; init; }
}

public static class WorkOrderPartForWorkOrderModelExtensions
{
    public static bool IsValid(this WorkOrderPartForWorkOrderModel subject)
    {
        return subject != null &&
            subject.Data.Count > 0 &&
            subject.Data.All(x => x.IsValid());
    }

    public static bool IsValid(this WorkOrderPartsForWorkOrderData subject)
    {
        return subject != null &&
            !subject.ItemNo.IsEmpty() &&
            !subject.Rev.IsEmpty() &&
            !subject.Unit.IsEmpty();
    }
}
