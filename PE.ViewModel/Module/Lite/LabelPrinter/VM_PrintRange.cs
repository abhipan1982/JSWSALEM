using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.LabelPrinter
{
  public class VM_PrintRange : VM_Base
  {
    public VM_PrintRange() { }

    public VM_PrintRange(long workOrderId)
    {
      this.WorkOrderId = workOrderId;
    }

    public VM_PrintRange(long workOrderId, short orderSeqMin, short orderSeqMax)
    {
      this.WorkOrderId = workOrderId;
      this.OrderSeqMin = orderSeqMin;
      this.OrderSeqMax = orderSeqMax;
    }

    public long WorkOrderId { get; set; }
    public short OrderSeqMin { get; set; }
    public short OrderSeqMax { get; set; }
  }
}
