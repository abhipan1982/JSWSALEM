using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.WorkOrder
{
  public class VM_WorkOrderConfirmation : VM_Base
  {
    public VM_WorkOrderConfirmation() { }
    public long? WorkOrderId { get; set; }
    public bool IsEndOfWorkShop { get; set; }
  }
}
