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
    public string OrganizationCode { get; set; } = null!;
    public string ItemNumber { get; set; } = null!;
    public int PlannedStartQuantity { get; set; }
    public DateTime PlannedStartDate { get; set; }
}


public static class CreateWorkOrderRequestExtensions
{
    public static bool IsValid(this CreateWorkOrderRequest subject)
    {
        return subject != null &&
            !subject.ItemNumber.IsEmpty();
    }
}
