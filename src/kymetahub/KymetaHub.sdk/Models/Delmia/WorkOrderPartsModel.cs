using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Models.Delmia;

public record WorkOrderPartsModel
{
    public IReadOnlyList<WorkOrderPartsData> Data { get; init; } = Array.Empty<WorkOrderPartsData>();
}


public record WorkOrderPartsData
{
    public int Id { get; set; }
    public int WorkOrderId { get; set; }
    public int PartNoId { get; set; }
    public int QuantitySinceScheduled { get; set; }
}

public static class WorkOrderPartsModelExtensions
{
    public static bool IsValid(this WorkOrderPartsModel subject)
    {
        return subject != null &&
            subject.Data.Count > 0 &&
            subject.Data.All(x => x.IsValid());
    }

    public static bool IsValid(this WorkOrderPartsData subject)
    {
        return subject != null;
    }
}
