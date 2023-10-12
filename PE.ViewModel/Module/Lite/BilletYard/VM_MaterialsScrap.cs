using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.BilletYard
{
  public class VM_MaterialsScrap : VM_Base
  {
    public long HeatId { get; set; }
    public int MaterialsNumber { get; set; }
    public long SourceYardId { get; set; }
    public long? SourceLocationId { get; set; }
  }
}
