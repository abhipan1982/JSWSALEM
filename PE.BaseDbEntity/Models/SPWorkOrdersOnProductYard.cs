namespace PE.BaseDbEntity.Models
{
  public class SPWorkOrdersOnProductYard
  {
    public long WorkOrderId { get; set; }
    public long AssetId { get; set; }
    public string AssetName { get; set; }
    public int PieceMaxCapacity { get; set; }
    public int WeightMaxCapacity { get; set; }
    public int ProductNumber { get; set; }
    public double ProductWeight { get; set; }
    public double FillIndex { get; set; }
  }
}
