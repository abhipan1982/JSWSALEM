using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.Core;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Schedule
{
  public class VM_ScheduleSummary : VM_Base
  {
    #region ctor

    public VM_ScheduleSummary() { }

    public VM_ScheduleSummary(V_ScheduleSummary data)
    {
      ScheduleOrderSeq = data.ScheduleOrderSeq;
      WorkOrderId = data.WorkOrderId;
      ScheduleId = data.ScheduleId;
      WorkOrderName = data.WorkOrderName;
      EnumWorkOrderStatus = data.EnumWorkOrderStatus;
      ProductCatalogueName = data.ProductCatalogueName;
      MaterialCatalogueName = data.MaterialCatalogueName;
      ProductCatalogueTypeCode = data.ProductCatalogueTypeCode;
      HeatId = data.HeatId;
      HeatName = data.HeatName;
      SteelgradeCode = data.SteelgradeCode;
      SteelgradeId = data.SteelgradeId;
      WorkOrderStartTs = data.WorkOrderStartTs;
      WorkOrderEndTs = data.WorkOrderEndTs;
      MaterialsPlanned = data.MaterialsPlanned;
      RawMaterialsNumber = data.RawMaterialsNumber;
      MaterialsNumber = data.MaterialsNumber;
      SteelgradeName = data.SteelgradeName;
      PlannedTime = new TimeSpan(0, 0, (int)data.PlannedDuration);
      Progress = data.RawMaterialsParents + "/" + data.MaterialsNumber;
      RawMaterialsParents = data.RawMaterialsParents;
      IsTestOrder = data.IsTestOrder;
      IsScheduledStatus = data.EnumWorkOrderStatus == WorkOrderStatus.Scheduled;
      if (!data.IsTestOrder)
      {
        IsWorkOrderReportSendPossible = data.EnumWorkOrderStatus == WorkOrderStatus.Finished;
      }
      IsEndOfWorkOrderPossible = ProductCatalogueTypeCode.ToUpper().Equals(Constants.Bar.ToUpper())
        && data.LowestRawMaterialStatus.HasValue
        && data.LowestRawMaterialStatus > RawMaterialStatus.OnCoolingBed
        && data.MaterialsNumber == data.RawMaterialsParents
        && data.EnumWorkOrderStatus != WorkOrderStatus.Finished;
      if (data.WorkOrderStartTs.HasValue && data.WorkOrderEndTs.HasValue)
      {
        DurationHMS = TimeSpan.FromSeconds((data.WorkOrderEndTs.Value - data.WorkOrderStartTs.Value).TotalSeconds).ToString(@"hh\:mm\:ss");
      }

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region props

    public long WorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "EnumWorkOrderStatus", "NAME_WorkOrderStatus")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumWorkOrderStatus { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "WorkOrderStartTs", "NAME_DateTimeProductionStarted")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? WorkOrderStartTs { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "WorkOrderEndTs", "NAME_DateTimeProductionFinished")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? WorkOrderEndTs { get; set; }

    public long MaterialCatalogueId { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "MaterialCatalogueName", "NAME_MaterialCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialCatalogueName { get; set; }

    public long ProductCatalogueId { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "ProductCatalogueName", "NAME_ProductCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "ProductCatalogueTypeCode", "NAME_ProductCatalogueTypeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueTypeCode { get; set; }

    public long SteelgradeId { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "SteelgradeCode", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "SteelgradeName", "NAME_SteelgradeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeName { get; set; }

    public long? HeatId { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    public long ScheduleId { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "ScheduleOrderSeq", "NAME_SEQ")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short ScheduleOrderSeq { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "PlannedTime", "NAME_PlannedTime")]
    public TimeSpan PlannedTime { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "MaterialsPlanned", "NAME_MaterialsPlanned")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short MaterialsPlanned { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "MaterialsNumber", "NAME_MaterialsNumber")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int MaterialsNumber { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "RawMaterialsNumber", "NAME_Assigned")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int RawMaterialsNumber { get; set; }

    public int RawMaterialsParents { get; set; }

    public int RawMaterialsKids { get; set; }

    public int RawMaterialsInvalid { get; set; }

    public int RawMaterialsUnassigned { get; set; }

    public int RawMaterialsAssigned { get; set; }

    public int RawMaterialsCharged { get; set; }

    public int RawMaterialsDischarged { get; set; }

    public int RawMaterialsInStorage { get; set; }

    public int RawMaterialsInMill { get; set; }

    public int RawMaterialsInFinalProduction { get; set; }

    public int RawMaterialsOnCoolingBed { get; set; }

    public int RawMaterialsRolled { get; set; }

    public int RawMaterialsInTransport { get; set; }

    public int RawMaterialsFinished { get; set; }

    public int RawMaterialsScrapped { get; set; }

    public int RawMaterialsRejected { get; set; }

    public int RawMaterialsDivided { get; set; }

    public short? LowestRawMaterialStatus { get; set; }

    public short? HighestRawMaterialStatus { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "DurationHMS", "NAME_Duration")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DurationHMS { get; set; }

    [SmfDisplay(typeof(VM_ScheduleSummary), "Progress", "NAME_Progress")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Progress { get; set; }

    public bool IsWorkOrderReportSendPossible { get; set; }

    public bool IsEndOfWorkOrderPossible { get; set; }

    public bool IsTestOrder { get; set; }

    public bool IsScheduledStatus { get; set; }

    #endregion
  }
}
