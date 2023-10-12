using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.Core;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.WorkOrder
{
  public class VM_WorkOrderSummary : VM_Base
  {
    #region ctor

    public VM_WorkOrderSummary() { }

    public VM_WorkOrderSummary(V_WorkOrderSummary data)
    {
      WorkOrderId = data.WorkOrderId;
      ScheduleId = data.ScheduleId;
      WorkOrderName = data.WorkOrderName;
      IsTestOrder = data.IsTestOrder;
      EnumWorkOrderStatus = data.EnumWorkOrderStatus;
      ProductCatalogueName = data.ProductCatalogueName;
      MaterialCatalogueName = data.MaterialCatalogueName;
      ProductCatalogueTypeCode = data.ProductCatalogueTypeCode;
      HeatName = data.HeatName;
      SteelgradeCode = data.SteelgradeCode;
      WorkOrderStart = data.WorkOrderStartTs;
      TargetOrderWeight = data.TargetOrderWeight;
      ProductsWeight = data.ProductsWeight;
      ProductsNumber = data.ProductsNumber;
      MaterialsPlanned = data.MaterialsPlanned;
      MaterialsAssigned = data.RawMaterialsNumber;
      MaterialsRejected = data.MaterialsRejected;
      MaterialsScrapped = data.MaterialsScrapped;
      ScheduleOrderSeq = data.ScheduleOrderSeq;
      MaterialsWeight = data.MaterialsWeight;
      ProductThickness = data.ProductThickness;
      SteelgradeName = data.SteelgradeName;
      MaterialShapeName = data.MaterialShapeName;
      WorkOrderCreatedInL3Ts = data.WorkOrderCreatedInL3Ts;
      ToBeCompletedBeforeTs = data.ToBeCompletedBeforeTs;
      WorkOrderCreatedTs = data.WorkOrderCreatedTs;
      WorkOrderStartTs = data.WorkOrderStartTs;
      WorkOrderEndTs = data.WorkOrderEndTs;
      if (data.WorkOrderStartTs.HasValue && data.WorkOrderEndTs.HasValue)
      {
        DurationHMS = TimeSpan.FromSeconds((data.WorkOrderEndTs.Value - data.WorkOrderStartTs.Value).TotalSeconds).ToString(@"hh\:mm\:ss");
      }
      //TODOMN - check this
      PlannedTime = data.PlannedDuration != null ? new TimeSpan(0, 0, (int)data.PlannedDuration) : new TimeSpan(0, 0, 0);
      Progress = data.RawMaterialsNumber + "/" + data.MaterialsPlanned;
      MaterialsRejectedWeight = data.MaterialsRejectedWeight;
      MaterialsRejectedWeightAfterFurnace = data.MaterialsRejectedWeightAfterFurnace;
      MaterialsScrapped = data.MaterialsScrapped;
      MaterialsScrappedWeight = data.MaterialsScrappedWeight;
      MaterialsLossWeight = data.MaterialsLossWeight;
      IsScheduledStatus = data.EnumWorkOrderStatus == WorkOrderStatus.Scheduled;
      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_WorkOrderSummary(V_WorkOrderSearchGrid data)
    {
      WorkOrderId = data.WorkOrderId;
      WorkOrderName = data.WorkOrderName;
      EnumWorkOrderStatus = data.EnumWorkOrderStatus;
      ProductCatalogueName = data.ProductCatalogueName;
      MaterialCatalogueName = data.MaterialCatalogueName;
      SteelgradeCode = data.SteelgradeCode;
      HeatName = data.HeatName;
      IsTestOrder = data.IsTestOrder;
      IsBlocked = data.IsBlocked;
      IsNewStatus = EnumWorkOrderStatus == WorkOrderStatus.New;
      UnCancellable = EnumWorkOrderStatus == WorkOrderStatus.Cancelled;
      UnBlockable = IsBlocked;
      WorkOrderCreatedInL3Ts = data.WorkOrderCreatedInL3Ts;
      ToBeCompletedBeforeTs = data.ToBeCompletedBeforeTs;
      WorkOrderCreatedTs = data.WorkOrderCreatedTs;
      WorkOrderStartTs = data.WorkOrderStartTs;
      WorkOrderEndTs = data.WorkOrderEndTs;
      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region props
    public long WorkOrderId { get; set; }

    public long? ScheduleId { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "WorkOrderName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "IsTestOrder", "NAME_IsTestOrder")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsTestOrder { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "WorkOrderStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumWorkOrderStatus { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "ProductCatalogueName", "NAME_ProductCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "ProductCatalogueTypeCode", "NAME_ProductCatalogueTypeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueTypeCode { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "MaterialCatalogueName", "NAME_MaterialCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "SteelgradeName", "NAME_Steelgrade")]
    public string SteelgradeName { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "SteelgradeCode", "NAME_SteelgradeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "CreatedInL3", "NAME_CreatedInL3")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime WorkOrderCreatedInL3Ts { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "ToBeCompletedBefore", "NAME_ToBeCompletedBefore")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime ToBeCompletedBeforeTs { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "WorkOrderStart", "NAME_DateTimeProductionStarted")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? WorkOrderStart { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "WorkOrderCreatedTs", "NAME_WorkOrderCreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime WorkOrderCreatedTs { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "WorkOrderStartTs", "NAME_WorkOrderStartTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? WorkOrderStartTs { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "WorkOrderEndTs", "NAME_WorkOrderEndTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? WorkOrderEndTs { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "TargetOrderWeight", "NAME_TargetOrderWeight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double TargetOrderWeight { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "ProductsWeight", "NAME_ProductWeight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double ProductsWeight { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "MaterialsWeight", "NAME_RawMaterialWeight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double? MaterialsWeight { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "ProductsNumber", "NAME_ProductNumber")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int ProductsNumber { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "ProductsNumber", "NAME_MaterialsPlanned")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? MaterialsPlanned { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "MaterialsAssigned", "NAME_Assigned")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? MaterialsAssigned { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "MaterialsRejected", "NAME_Rejected")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? MaterialsRejected { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "MaterialsScrapped", "NAME_Scrapped")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? MaterialsScrapped { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "Progress", "NAME_Progress")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Progress { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "ScheduleOrderSeq", "NAME_SEQ")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? ScheduleOrderSeq { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "IsBlocked", "NAME_IsBlocked")]
    public bool IsBlocked { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "PlannedTime", "NAME_PlannedTime")]
    public TimeSpan PlannedTime { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "ProductThickness", "NAME_Thickness")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-")]
    [SmfUnit("UNIT_Diameter")]
    public double ProductThickness { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "DurationHMS", "NAME_Duration")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DurationHMS { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "MaterialShapeName", "NAME_Shape")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialShapeName { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "MaterialsRejectedWeight", "NAME_Rejects")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double MaterialsRejectedWeight { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "MaterialsRejectedWeightAfterFurnace", "NAME_Rejects")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double MaterialsRejectedWeightAfterFurnace { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "MaterialsScrappedWeight", "NAME_Scrap")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double MaterialsScrappedWeight { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderSummary), "MaterialsLossWeight", "NAME_LossWeight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double MaterialsLossWeight { get; set; }

    public bool IsNewStatus { get; set; }
    public bool IsScheduledStatus { get; set; }
    public bool UnCancellable { get; set; }
    public bool UnBlockable { get; set; }

    #endregion
  }
}
