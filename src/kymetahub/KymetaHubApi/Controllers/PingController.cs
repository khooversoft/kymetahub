using KymetaHub.sdk.Models.Ping;
using KymetaHub.sdk.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;
using System.Reflection;

namespace KymetaHubApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PingController : ControllerBase
{
    [HttpGet]
    public ActionResult<PingResponse> Ping()
    {
        var response = new PingResponse
        {
            Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown",
            Status = ServiceStatusLevel.Running.ToString(),
            Message = "Service is running",
        };

        return Ok(response);
    }
}
