using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.Shift
{
  public class VM_ShiftWorkOrders : VM_Base
  {
    public VM_ShiftWorkOrders()
    {
      ShiftWorkOrders = new List<VM_ShiftWorkOrderSimpleData>();
    }

    public long ShiftId { get; set; }

    public List<VM_ShiftWorkOrderSimpleData> ShiftWorkOrders { get; set; }
  }
}
