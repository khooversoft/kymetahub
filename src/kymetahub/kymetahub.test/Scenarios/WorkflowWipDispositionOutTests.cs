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
    //private const int _workOrderId = 1551;
    private const int _workOrderId = 1517;

    [Fact]
    public async Task GivenWorkOrder_WhenWorkFlowRun_ShouldPass()
    {
        KymetaHubApiClient client = TestApplication.GetKymetaHubApiClient();

        var creationDate = DateTime.UtcNow;
        var response = await client.WipDispositionOut(_workOrderId, creationDate);
        response.Should().NotBeNull();
    }
}
