using FluentAssertions;
using kymetahub.test.Application;
using KymetaHub.sdk.Clients;
using KymetaHub.sdk.Models.Requests;

namespace kymetahub.test.Scenarios;

public class WorkOrderTests
{
    [Fact]
    public async Task GivenWorkOrder_WhenWorkFlowRun_ShouldPass()
    {
        const int workOrderId = 1517;
        KymetaHubApiClient client = TestApplication.GetKymetaHubApiClient();

        var creationDate = DateTime.UtcNow;
        var response = await client.CreateWorkOrder(workOrderId, creationDate);
        response.Should().NotBeNull();
    }

    [Fact]
    public async Task GivenWorkOrderUpdate_WhenRun_ShouldPass()
    {
        KymetaHubApiClient client = TestApplication.GetKymetaHubApiClient();

        var request = new WorkOrderUpdateRequest
        {
            WORKORDER_ID = "1517",
            ACTUAL_COMPLETE_DATE = "",
            START_DATE = "09/21/2022 16:22:12",
            CLOSED_DATE = "",
            COMPLETED_QTY = "",
            COMPLETED_LOCATION = "",
            REJECT_QTY = "0",
            SCRAP_QTY = "0",
            WO_STATUS = "OPEN",
        };

        var creationDate = DateTime.UtcNow;
        var response = await client.UpdateWorkOrder(request);
        response.Should().NotBeNull();
    }

    [Fact]
    public async Task WorkOrderMaterialTrxRequest_WhenRun_ShouldPass()
    {
        KymetaHubApiClient client = TestApplication.GetKymetaHubApiClient();

        var request = new WorkOrderMaterialTrxRequest
        {
            WORKORDER_ID = 1533,
            EPLANT_ID = 10,
            FG_LOTNO = "",
            ITEMNO = "820-00597-000",
            LOCATION = "FEED",
            MFG_TYPE = "ASSY1",
            SEQ = 1,
            SERIAL = "000000134",
            TRANS_DATE = "09/30/2022 12:23:07",
            TRANS_QTY = 1,
            TRANS_TYPE = "FINISH PROCESS WIP",
            UNIT = "EACH",
        };

        var creationDate = DateTime.UtcNow;
        var response = await client.WorkOrderMaterialTrx(request);
        response.Should().NotBeNull();
    }
}
