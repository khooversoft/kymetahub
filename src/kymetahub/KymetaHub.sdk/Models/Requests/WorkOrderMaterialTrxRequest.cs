using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KymetaHub.sdk.Models.Requests;

public record WorkOrderMaterialTrxRequest
{
    public int WORKORDER_ID { get; set; }
    public int EPLANT_ID { get; set; }
    public string FG_LOTNO { get; set; } = null!;
    public string ITEMNO { get; set; } = null!;
    public string LOCATION { get; set; } = null!;
    public string MFG_TYPE { get; set; } = null!;
    public int SEQ { get; set; }
    public string SERIAL { get; set; } = null!;
    public string TRANS_DATE { get; set; } = null!;
    public int TRANS_QTY { get; set; }
    public string TRANS_TYPE { get; set; } = null!;
    public string UNIT { get; set; } = null!;
}
