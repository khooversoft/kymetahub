using KymetaHub.sdk.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Models.Delmia;

public record WorkOrderModel
{
    public IReadOnlyList<WorkOrderData> Data { get; init; } = null!;
}


public record WorkOrderData
{
    public int Id { get; set; }
    public int StandardID { get; set; }
    public string MfgNumber { get; set; } = null!;
    public int EplantID { get; set; }
    public DateTime StartDate { get; set; }
}


public static class WorkOrderModelExtensions
{
    public static bool IsValid(this WorkOrderModel subject)
    {
        return subject != null &&
            subject.Data.Count > 0 &&
            subject.Data.All(x => x.IsValid());
    }

    public static bool IsValid(this WorkOrderData subject)
    {
        return subject != null &&
            !subject.MfgNumber.IsEmpty();
    }
}