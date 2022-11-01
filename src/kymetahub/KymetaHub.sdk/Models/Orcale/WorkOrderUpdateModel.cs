using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KymetaHub.sdk.Extensions;

namespace KymetaHub.sdk.Models.Orcale;

public record WorkOrderUpdateModel
{
    public string WorkOrderNumber { get; init; } = null!;
    public string SourceHeaderReference { get; init; } = null!;
    public long SourceLineReferenceId { get; init; }
    public int SourceSystemId { get; init; }
    public string OrganizationCode { get; init; } = null!;
    public string OrganizationName { get; init; } = null!;
    public string WorkOrderType { get; init; } = null!;
}


public static class WorkOrderUpdateModelExtensions
{
    public static bool IsValid(this WorkOrderUpdateModel subject)
    {
        return subject != null;
    }
}