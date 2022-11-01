using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KymetaHub.sdk.Models.Delmia;

namespace KymetaHub.sdk.Models.Responses;

public record WorkOrderUpdateResponse
{
    public string WorkOrderId { get; init; } = null!;
    public bool Success { get; init; }
    public string? Message { get; init; }
    public WipDispositionOutResponse? Wip { get; init; }
}
