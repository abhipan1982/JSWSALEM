using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Shift
{
  public class VM_ShiftOverview
  {
    public VM_ShiftOverview(EVTShiftCalendar shift)
    {
      ShiftId = shift.ShiftCalendarId;
      PlannedStartTime = shift.PlannedStartTime;
      PlannedEndTime = shift.PlannedEndTime;
      StartTime = shift.StartTime;
      EndTime = shift.EndTime;
      IsActualShift = shift.IsActualShift;
      ShiftCode = shift.FKShiftDefinition.ShiftCode;
    }

    [SmfDisplay(typeof(VM_ShiftOverview), "ShiftId", "NAME_ShiftId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long ShiftId { get; set; }

    [SmfDisplay(typeof(VM_ShiftOverview), "PlannedStartTime", "NAME_PlannedStartTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime PlannedStartTime { get; set; }

    [SmfDisplay(typeof(VM_ShiftOverview), "PlannedEndTime", "NAME_PlannedEndTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime PlannedEndTime { get; set; }

    [SmfDisplay(typeof(VM_ShiftOverview), "StartTime", "NAME_StartTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? StartTime { get; set; }

    [SmfDisplay(typeof(VM_ShiftOverview), "EndTime", "NAME_EndTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? EndTime { get; set; }

    [SmfDisplay(typeof(VM_ShiftOverview), "IsActualShift", "NAME_IsActualShift")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsActualShift { get; set; }

    [SmfDisplay(typeof(VM_ShiftOverview), "ShiftCode", "NAME_ShiftCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ShiftCode { get; set; }
  }
}
