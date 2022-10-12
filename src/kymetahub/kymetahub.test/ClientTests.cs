using FluentAssertions;
using kymetahub.test.Application;
using KymetaHub.sdk.Clients;
using KymetaHub.sdk.Extensions;
using KymetaHub.sdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kymetahub.test;

public class ClientTests
{
    private const int _workOrderId = 1551;

    [Fact]
    public async Task WhenGetWorkOrder_ShouldPass()
    {
        KmtaClient client = TestApplication.GetKmtaClient();

        WorkOrderModel response = await client.GetWorkOrder(_workOrderId);
        response.IsValid().Should().BeTrue();
        response.Data.Count.Should().BeGreaterOrEqualTo(1);
        response.Data[0].Action(x =>
        {
            x.Id.Should().Be(_workOrderId);
            x.StandardID.Should().Be(63956);
            x.MfgNumber.Should().Be("U8ACC-00001-0_A_CO-003893");
            x.EplantID.Should().Be(10);
            x.StartDate.Should().Be(DateTime.Parse("2022-09-21T00:00:00"));
        });
    }
}
