using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Shift
{
  public class VM_ShiftWorkOrderSimpleData : VM_Base
  {
    public VM_ShiftWorkOrderSimpleData(PRMWorkOrder workOrder)
    {
      WorkOrderId = workOrder.WorkOrderId;
      WorkOrderStatus = ResxHelper.GetResxByKey((WorkOrderStatus)workOrder.EnumWorkOrderStatus);
      WorkOrderName = workOrder.WorkOrderName;
      //TODOMN - refactor this
      //CreatedTs = workOrder.CreatedTs;
      //LastUpdateTs = workOrder.LastUpdateTs;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_ShiftWorkOrderSimpleData(PRMWorkOrder workOrder, long shiftId, bool delaysShouldBeUpdated)
    {
      ShiftId = shiftId;
      WorkOrderId = workOrder.WorkOrderId;
      WorkOrderStatus = ResxHelper.GetResxByKey((WorkOrderStatus)workOrder.EnumWorkOrderStatus);
      WorkOrderName = workOrder.WorkOrderName;
      //TODOMN - refactor this
      //CreatedTs = workOrder.CreatedTs;
      //LastUpdateTs = workOrder.LastUpdateTs;
      CanBeSent = !delaysShouldBeUpdated && workOrder.EnumWorkOrderStatus == BaseDbEntity.EnumClasses.WorkOrderStatus.Finished.Value ? true : false;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long ShiftId { get; set; }

    [SmfDisplay(typeof(VM_ShiftWorkOrderSimpleData), "WorkOrderId", "NAME_WorkOrderId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long WorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_ShiftWorkOrderSimpleData), "WorkOrderStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderStatus { get; set; }

    [SmfDisplay(typeof(VM_ShiftWorkOrderSimpleData), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_ShiftWorkOrderSimpleData), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_ShiftWorkOrderSimpleData), "LastUpdateTs", "NAME_LastUpdateTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplay(typeof(VM_ShiftWorkOrderSimpleData), "CanBeSent", "NAME_CanBeSent")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool CanBeSent { get; set; }
  }
}
