namespace KymetaHub.sdk.Models;

public record WipDispositionOutResponse
{
    public WorkOrderModel WorkOrder { get; init; } = null!;
    public WorkOrderPartForWorkOrderModel WorkOrderPartForWorkOrder { get; init; } = null!;
    public WorkOrderPartsModel WorkOrderParts { get; init; } = null!;
    public SalesOrderForWorkOrderModel SalesOrderForWorkOrder { get; init; } = null!;
    public EplantsModel Eplants { get; init; } = null!;
    public BillOfMaterialsModel BillOfMaterials { get; init; } = null!;
}