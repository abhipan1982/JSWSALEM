using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Schedule
{
  public class VM_Schedule : VM_Base
  {
    #region props

    [SmfDisplay(typeof(VM_Schedule), "ScheduleId", "NAME_ScheduleId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? ScheduleId { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "FKWorkOrderId", "NAME_FKWorkOrderId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKWorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "OrderSeq", "NAME_OrderSeq")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? OrderSeq { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "ScheduleStatus", "NAME_ScheduleStatus")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? ScheduleStatus { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "PlannedWeight", "NAME_Weight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double? PlannedWeight { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "PlannedTime", "NAME_PlannedDuration")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string PlannedTime { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "StartTime", "NAME_StartTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? StartTime { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "EndTime", "NAME_EndTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? EndTime { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "PlannedStartTime", "NAME_StartTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? PlannedStartTime { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "PlannedEndTime", "NAME_PlannedEndTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? PlannedEndTime { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "FKAssetId", "NAME_FKAssetId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKAssetId { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "NoOfmaterials", "NAME_NoOfmaterials")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    public long? NoOfmaterials { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "MaterialsNo", "NAME_MaterialsNo")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? MaterialsNo { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "IsTest", "NAME_IsTest")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsTest { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "ProductCatalogueName", "PS_ProductCatalogueName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "BilletCatalogueName", "NAME_BilletCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string BilletCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "Steelgrade", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Steelgrade { get; set; }

    [SmfDisplay(typeof(VM_Schedule), "BilletsToBeRolled", "NAME_BilletsToBeRolled")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string BilletsToBeRolled { get; set; }

    public int? WorkOrderStatus { get; set; }

    #endregion

    #region ctor

    public VM_Schedule(PPLSchedule schedule)
    {
      ScheduleId = schedule.ScheduleId;
      FKWorkOrderId = schedule.FKWorkOrderId;
      OrderSeq = schedule.OrderSeq;
      PlannedStartTime = schedule.PlannedStartTime;
      PlannedEndTime = schedule.PlannedEndTime;
      //TODOMN - refactor this
      //CreatedTs = schedule.CreatedTs;
      PlannedWeight = schedule.FKWorkOrder?.TargetOrderWeight;
      HeatName = schedule.FKWorkOrder.FKHeat?.HeatName;
      IsTest = schedule.FKWorkOrder?.IsTestOrder;
      ProductCatalogueName = schedule.FKWorkOrder?.FKProductCatalogue?.ProductCatalogueName;
      BilletCatalogueName = schedule.FKWorkOrder?.FKMaterialCatalogue?.MaterialCatalogueName;
      Steelgrade = schedule.FKWorkOrder.FKSteelgrade?.SteelgradeCode;

      WorkOrderName = schedule?.FKWorkOrder?.WorkOrderName;
      MaterialsNo = schedule?.FKWorkOrder?.PRMMaterials?.Count;
      WorkOrderStatus = Convert.ToInt32(schedule?.FKWorkOrder?.EnumWorkOrderStatus);

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_Schedule()
    {
    }

    #endregion
  }
}
