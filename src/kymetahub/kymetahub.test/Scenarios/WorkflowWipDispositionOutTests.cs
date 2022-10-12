using KymetaHub.sdk.Clients;
using kymetahub.test.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace kymetahub.test.Scenarios;

public class WorkflowWipDispositionOutTests
{
    private const int _workOrderId = 1551;

    [Fact]
    public async Task GivenWorkOrder_WhenWorkFlowRun_ShouldPass()
    {
        KymetaHubApiClient client = TestApplication.GetKymetaHubApiClient();

        var response = await client.WipDispositionOut(_workOrderId);
        response.Should().NotBeNull();



    }
}
