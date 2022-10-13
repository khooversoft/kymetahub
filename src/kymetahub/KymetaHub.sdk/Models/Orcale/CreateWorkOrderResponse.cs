using KymetaHub.sdk.Models.Delmia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Models.Orcale;

public record CreateWorkOrderResponse
{
    public WipDispositionOutResponse? WipResponse { get; init; }
    public bool Success { get; init; }
    public string? OrcaleResponse { get; init; }
}
