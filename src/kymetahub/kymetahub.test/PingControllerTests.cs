using KymetaHub.sdk.Clients;
using kymetahub.test.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using KymetaHub.sdk.Models;
using KymetaHub.sdk.Services;

namespace kymetahub.test;

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
