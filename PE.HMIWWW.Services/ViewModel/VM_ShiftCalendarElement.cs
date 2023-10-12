using System;
using Kendo.Mvc.UI;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.Services.ViewModel
{
  public class VM_ShiftCalendarElement : VM_Base, ISchedulerEvent
  {
    public VM_ShiftCalendarElement()
    {
    }

    public VM_ShiftCalendarElement(V_ShiftCalendar rec)
    {
      ShiftCalendarId = rec.ShiftCalendarId;
      ShiftCode = rec.ShiftCode;
      if (rec.PlannedStartTime <= DateTime.Now && rec.PlannedEndTime >= DateTime.Now)
      {
        IsActualShift = true;
      }
      else
      {
        IsActualShift = false;
      }

      ShiftDefinitionId = rec.ShiftDefinitionId;
      CrewId = rec.CrewId;

      //IScheduler interdace implementation
      Start = rec.PlannedStartTime;
      End = rec.PlannedEndTime;
      CrewName = rec.CrewName;
      Title = rec.CrewName;
      IsAllDay = false;
      IsActive = rec.IsActive;
    }

    public VM_ShiftCalendarElement(EVTShiftCalendar rec)
    {
      ShiftCalendarId = rec.ShiftCalendarId;
      IsActualShift = rec.IsActualShift;

      ShiftDefinitionId = rec.FKShiftDefinitionId;

      //IScheduler interdace implementation
      Start = rec.PlannedStartTime;
      End = rec.PlannedEndTime;
      IsAllDay = false;
      IsActive = rec.IsActive;
    }


    public long? ShiftCalendarId { get; set; } // ShiftCalendarId
    public string ShiftCode { get; set; } // ShiftCode

    [SmfDisplayAttribute(typeof(VM_ShiftCalendarElement), "CrewName", "NAME_CrewName")]
    public string CrewName { get; set; } // CrewName

    [SmfDisplayAttribute(typeof(VM_ShiftCalendarElement), "IsActualShift", "NAME_IsActiveShift")]
    public bool IsActualShift { get; set; } // IsActualShift

    [SmfDisplayAttribute(typeof(VM_ShiftCalendarElement), "IsActive", "NAME_IsActive")]
    public bool? IsActive { get; set; }


    public long ShiftDefinitionId { get; set; } // ShiftDefinitionId

    [SmfDisplayAttribute(typeof(VM_ShiftCalendarElement), "CrewId", "NAME_Shift")]
    public long CrewId { get; set; } // CrewId

    [SmfDisplayAttribute(typeof(VM_ShiftCalendarElement), "IsActualWorkingShift", "NAME_IsWorkingShift")]
    public bool IsActualWorkingShift { get; set; } // IsActualWorkingShift

    public long DaysOfYearId { get; set; }

    public string Description { get; set; }


    //IScheduler interdace implementation
    public string EndTimezone { get; set; }
    public bool IsAllDay { get; set; }

    [SmfDisplayAttribute(typeof(VM_ShiftCalendarElement), "Start", "NAME_From")]
    public DateTime Start { get; set; } // DelayStart

    [SmfDisplayAttribute(typeof(VM_ShiftCalendarElement), "End", "NAME_To")]
    public DateTime End { get; set; } // DelayEnd

    public string RecurrenceException { get; set; }
    public string RecurrenceRule { get; set; }
    public string StartTimezone { get; set; }
    public string Title { get; set; }
  }
  //public class VM_ShiftCalendarElementList : List<VM_ShiftCalendarElement>
  //{
  //	public VM_ShiftCalendarElementList()
  //	{
  //	}
  //	//public VM_DADelayElementList(IList<VDelaysSummaryPivotData> dbClass)
  //	//{
  //	//	foreach (VDelaysSummaryPivotData item in dbClass)
  //	//	{
  //	//		this.Add(new VM_DADelayElement(item));
  //	//	}
  //	//}
  //}
}
