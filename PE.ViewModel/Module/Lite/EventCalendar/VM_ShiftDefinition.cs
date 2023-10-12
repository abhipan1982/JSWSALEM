using System;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.EventCalendar
{
  public class VM_ShiftDefinition : VM_Base
  {
    public VM_ShiftDefinition(EVTShiftDefinition entity)
    {
      ShiftCode = entity.ShiftCode;
      ShiftStart = entity.DefaultStartTime;
      ShiftEnd = entity.DefaultEndTime;
      EndNextDay = entity.ShiftEndsNextDay;
    }

    public string ShiftCode { get; set; }
    public TimeSpan ShiftStart { get; set; }
    public TimeSpan ShiftEnd { get; set; }
    public bool EndNextDay { get; set; }
  }
}
