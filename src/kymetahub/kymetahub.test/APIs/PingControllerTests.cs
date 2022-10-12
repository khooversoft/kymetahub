using KymetaHub.sdk.Clients;
using kymetahub.test.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using KymetaHub.sdk.Services;
using KymetaHub.sdk.Models.Ping;

namespace kymetahub.test.APIs;

public class PingControllerTests
{
    [Fact]
    public async Task GivenPing_ShouldPass()
    {
        KymetaHubApiClient client = TestApplication.GetKymetaHubApiClient();

        PingResponse response = await client.Ping();
        response.Should().NotBeNull();
        response.Status.Should().Be(ServiceStatusLevel.Running.ToString());
    }
}
