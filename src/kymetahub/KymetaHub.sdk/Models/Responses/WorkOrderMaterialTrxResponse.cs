using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Models.Responses;

public record WorkOrderMaterialTrxResponse
{
    public string WorkOrderId { get; init; } = null!;
    public bool Success { get; init; }
    public string? Message { get; init; }
}
