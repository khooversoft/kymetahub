using KymetaHub.sdk.Extensions;
using KymetaHub.sdk.Models.Delmia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Models.Orcale;

public class WorkOrderResponse
{
    public IReadOnlyList<WorkOrderResponseData> Data { get; set; } = Array.Empty<WorkOrderResponseData>();

}

public class WorkOrderResponseData
{
    public string OrganizationCode { get; set; } = null!;
    public string ItemNumber { get; set; } = null!;
    public int PlannedStartQuantity { get; set; }
    public DateTime PlannedStartDate { get; set; }
}

public static class WorkOrderResponseExtensions
{
    public static bool IsValid(this WorkOrderResponse subject)
    {
        return subject != null &&
            subject.Data.Count > 0 &&
            subject.Data.All(x => x.IsValid());
    }

    public static bool IsValid(this WorkOrderResponseData subject)
    {
        return subject != null &&
            !subject.OrganizationCode.IsEmpty() &&
            !subject.ItemNumber.IsEmpty();
    }
}