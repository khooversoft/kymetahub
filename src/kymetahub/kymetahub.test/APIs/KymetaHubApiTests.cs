//using FluentAssertions;
//using kymetahub.test.Application;
//using KymetaHub.sdk.Clients;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace kymetahub.test.APIs;

//public class KymetaHubApiTests
//{
//    private const int _workOrderId = 1551;

//    [Fact]
//    public async Task GivenWipDispositionOut_ShouldPass()
//    {
//        KymetaHubApiClient client = TestApplication.GetKymetaHubApiClient();

//        var response = await client.WipDispositionOut(_workOrderId);
//        response.Should().NotBeNull();
//    }
//}
