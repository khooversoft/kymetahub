using KymetaHub.sdk.Extensions;
using KymetaHub.sdk.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Models.Orcale;

public record CreateWorkOrderRequest
{
    public string WorkOrderNumber { get; init; } = null!;
    public string OrganizationCode { get; set; } = "MFG";   // TODO: Needs mapping
    public string ItemNumber { get; set; } = null!;
    public int PlannedStartQuantity { get; set; }
    public DateTime PlannedStartDate { get; set; }
    public string WorkOrderStatusCode { get; set; } = "ORA_RELEASED";   // TODO: Needs mapping
    public bool ExplosionFlag { get; init; } = true;
}


//{  This works

//    "WorkOrderNumber": "1560",
//    "OrganizationCode": "MFG",
//    "ItemNumber": "U8ACC-00001-0",
//    "PlannedStartQuantity": 20,
//    "PlannedStartDate": "2022-11-16T12:00:00-05:00",
//    "WorkOrderStatusCode": "ORA_RELEASED",
//    "ExplosionFlag": true
//}

public static class CreateWorkOrderRequestExtensions
{
    public static bool IsValid(this CreateWorkOrderRequest subject)
    {
        return subject != null &&
            !subject.ItemNumber.IsEmpty();
    }
}
