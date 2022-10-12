using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Models.Ping;

public class PingResponse
{
    public string? Message { get; set; }
    public string? Status { get; set; }
    public string? Version { get; set; }
}
