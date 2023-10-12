namespace PE.HMIWWW.ViewModel.Module.Lite.Shift
{
  public class VM_ShiftWorkOrderModel
  {
    public VM_ShiftWorkOrderModel(long shiftId, long? workOrderId)
    {
      ShiftId = shiftId;
      WorkOrderId = workOrderId;
    }

    public long ShiftId { get; set; }
    public long? WorkOrderId { get; set; }
  }
}
