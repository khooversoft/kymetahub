using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Models.Requests;

public class UpdateWorkOrderRequest
{
    public string WORKORDER_ID { get; set; } = null!;
    public string ACTUAL_COMPLETE_DATE { get; set; } = null!;
    public string START_DATE { get; set; } = null!;
    public string CLOSED_DATE { get; set; } = null!;
    public string COMPLETED_QTY { get; set; } = null!;
    public string COMPLETED_LOCATION { get; set; } = null!;
    public string REJECT_QTY { get; set; } = null!;
    public string SCRAP_QTY { get; set; } = null!;
    public string WO_STATUS { get; set; } = null!;
}
