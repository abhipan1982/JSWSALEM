using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.Attributes;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.WorkOrder
{
  public class VM_WorkOrder : VM_Base
  {
    public VM_WorkOrder() { }

    public VM_WorkOrder(PRMWorkOrder data)
    {
      WorkOrderId = data.WorkOrderId;
      WorkOrderName = data.WorkOrderName;
      FKHeatIdRef = data.FKHeat?.HeatName;
      FKSteelgradeIdRef = data.FKSteelgrade?.SteelgradeCode;
      FKHeatId = data.FKHeatId;
      FKProductCatalogueId = data.FKProductCatalogueId;
      FKMaterialCatalogueId = data.FKMaterialCatalogue.MaterialCatalogueId;
      IsTestOrder = data.IsTestOrder;
      TargetOrderWeight = data.TargetOrderWeight;
      TargetOrderWeightMin = data.TargetOrderWeightMin;
      TargetOrderWeightMax = data.TargetOrderWeightMax;
      WorkOrderCreatedInL3Ts = data.WorkOrderCreatedInL3Ts;
      ToBeCompletedBeforeTs = data.ToBeCompletedBeforeTs;
      FKCustomerId = data.FKCustomerId;
      WorkOrderStatus = data.EnumWorkOrderStatus;
      FKSteelgradeId = data.FKSteelgradeId;

      UnitConverterHelper.ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_WorkOrder), "WorkOrderId", "NAME_WorkOrderId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long WorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "WorkOrderName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    [SmfStringLength(50)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "FKHeatIdRef", "NAME_HeatName")]
    [DisplayFormat(HtmlEncode = false, ConvertEmptyStringToNull = true)]
    [SmfRequired]
    public long? FKHeatId { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "SteelgradeId", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public long? FKSteelgradeId { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "FKProductCatalogueId", "NAME_ProductCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public long FKProductCatalogueId { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "WorkOrderStatus", "NAME_WorkOrderStatus")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public short WorkOrderStatus { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "FKMaterialCatalogueId", "NAME_MaterialCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public long FKMaterialCatalogueId { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "IsTestOrder", "NAME_IsTestOrder")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsTestOrder { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "TargetOrderWeight", "NAME_TargetWorkOrderWeight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    [SmfRequired]
    [CompareTo(nameof(TargetOrderWeightMin), ComparisonType.GreaterThanOrEqual)]
    [CompareTo(nameof(TargetOrderWeightMax), ComparisonType.SmallerThanOrEqual)]
    public double TargetOrderWeight { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "TargetOrderWeightMin", "NAME_TargetOrderWeightMin")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? TargetOrderWeightMin { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "TargetOrderWeightMax", "NAME_TargetOrderWeightMax")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? TargetOrderWeightMax { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "BundleWeightMin", "NAME_BundleWeightMin")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? BundleWeightMin { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "BundleWeightMax", "NAME_BundleWeightMax")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? BundleWeightMax { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "CreatedInL3", "NAME_CreatedInL3")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public DateTime WorkOrderCreatedInL3Ts { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "ToBeCompletedBefore", "NAME_ToBeCompletedBefore")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public DateTime ToBeCompletedBeforeTs { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "FKCustomerId", "NAME_CustomerName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKCustomerId { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "MaterialNumber", "NAME_NumberOfBillets")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public long MaterialsNumber { get; set; }

    public string FKHeatIdRef { get; set; }
    public string FKSteelgradeIdRef { get; set; }


    [SmfDisplay(typeof(VM_WorkOrder), "BilletWeight", "NAME_BilletWeight")]
    [SmfUnit("UNIT_Weight")]
    [CompareTo(nameof(BilletCatalogueWeightMin), ComparisonType.GreaterThanOrEqual)]
    [CompareTo(nameof(BilletCatalogueWeightMax), ComparisonType.SmallerThanOrEqual)]
    public double? BilletWeight { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "BilletCatalogueWeightMin", "NAME_WeightMin")]
    [SmfUnit("UNIT_Weight")]
    public double? BilletCatalogueWeightMin { get; set; }

    [SmfDisplay(typeof(VM_WorkOrder), "BilletCatalogueWeightMax", "NAME_WeightMax")]
    [SmfUnit("UNIT_Weight")]
    public double? BilletCatalogueWeightMax { get; set; }
  }
}
