using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.EnumClasses;
using PE.DbEntity.PEContext;
using PE.DbEntity.Models;
using PE.DbEntity;

namespace PE.HMIWWW.ViewModel.Module.Lite.WorkOrder
{
  public class VM_WorkOrderOverview : VM_Base
  {
    public VM_WorkOrderOverview() { }

    public VM_WorkOrderOverview(PRMWorkOrder order)
    //public VM_WorkOrderOverview(PRMWorkOrdersEXT order)
    {
      WorkOrderId = order.WorkOrderId;
      WorkOrderStatus = ResxHelper.GetResxByKey((WorkOrderStatus)order.EnumWorkOrderStatus);
      WorkOrderName = order.WorkOrderName;
      IsTestOrder = order.IsTestOrder;
      TargetOrderWeight = order.TargetOrderWeight;
      FKProductCatalogueId = order.FKProductCatalogueId;
      WorkOrderCreatedInL3Ts = order.WorkOrderCreatedInL3Ts;
      ToBeCompletedBeforeTs = order.ToBeCompletedBeforeTs;
      TargetOrderWeightMin = order.TargetOrderWeightMin;
      TargetOrderWeightMax = order.TargetOrderWeightMax;
      FKCustomerId = order.FKCustomerId;
      TargetMaterialNumber = order.L3NumberOfBillets;
      if (order.FKHeat != null)
      {
        Heat = new VM_HeatOverview(order.FKHeat);
        HeatName = order.FKHeat.HeatName;
        FKHeatIdRef = order.FKHeat.HeatId;
      }

      if (order.FKSteelgrade != null)
      {
        Steelgrade = order.FKSteelgrade.SteelgradeCode;
      }

      FKMaterialCatalogueId = order.FKMaterialCatalogue?.MaterialCatalogueId;
      Shape = order.FKMaterialCatalogue?.FKShape?.ShapeName;

      if (order.FKMaterialCatalogue != null)
      {
        MC = new VM_MaterialCatalogue(order.FKMaterialCatalogue);
      }

      if (order.FKProductCatalogue != null)
      {
        PC = new VM_ProductCatalogue(order.FKProductCatalogue);
      }

      

      if (order.FKSteelgrade != null)
      {
        SC = new VM_Steelgrade(order.FKSteelgrade);
      }

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_WorkOrderOverview(PRMWorkOrder order, double? metallicYield)
    {
      WorkOrderId = order.WorkOrderId;
      //TODOMN exclude this
      //CreatedTs = order.CreatedTs;
      //LastUpdateTs = order.LastUpdateTs;
      WorkOrderStatus = ResxHelper.GetResxByKey((WorkOrderStatus)order.EnumWorkOrderStatus);
      WorkOrderName = order.WorkOrderName;
      IsTestOrder = order.IsTestOrder;
      TargetOrderWeight = order.TargetOrderWeight;
      FKProductCatalogueId = order.FKProductCatalogueId;
      WorkOrderCreatedInL3Ts = order.WorkOrderCreatedInL3Ts;
      ToBeCompletedBeforeTs = order.ToBeCompletedBeforeTs;
      TargetOrderWeightMin = order.TargetOrderWeightMin;
      TargetOrderWeightMax = order.TargetOrderWeightMax;
      FKCustomerId = order.FKCustomerId;
      HeatName = order.FKHeat.HeatName;
      Steelgrade = order.FKSteelgrade.SteelgradeCode;
      FKHeatIdRef = order.FKHeat.HeatId;
      FKMaterialCatalogueId = order.FKMaterialCatalogue?.MaterialCatalogueId;
      MetallicYield = metallicYield;
      TargetMaterialNumber = order.L3NumberOfBillets;

      if (order.FKHeat != null)
      {
        Heat = new VM_HeatOverview(order.FKHeat);
      }

      if (order.FKMaterialCatalogue != null)
      {
        MC = new VM_MaterialCatalogue(order.FKMaterialCatalogue);
      }

      if (order.FKProductCatalogue != null)
      {
        PC = new VM_ProductCatalogue(order.FKProductCatalogue);
      }

      if (order?.FKSteelgrade != null)
      {
        SC = new VM_Steelgrade(order.FKSteelgrade);
      }

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_WorkOrderOverview(long workOrderId, WorkOrderStatus workOrderStatus, string workOrderName,
      DateTime? toBeCompletedBeforeTs)
    {
      WorkOrderId = workOrderId;
      WorkOrderName = workOrderName;
      ToBeCompletedBeforeTs = toBeCompletedBeforeTs;
      WorkOrderStatus = ResxHelper.GetResxByKey((WorkOrderStatus)workOrderStatus);

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_WorkOrderOverview(long workOrderId, string workOrderName, DateTime createdTs, double targetOrderWeight,
      WorkOrderStatus workOrderStatus)
    {
      WorkOrderId = workOrderId;
      WorkOrderName = workOrderName;
      CreatedTs = createdTs;
      TargetOrderWeight = targetOrderWeight;
      WorkOrderStatus = ResxHelper.GetResxByKey((WorkOrderStatus)workOrderStatus);

      UnitConverterHelper.ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "WorkOrderId", "NAME_WorkOrderId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? WorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "LastUpdateTs", "NAME_LastUpdateTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "WorkOrderStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderStatus { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "WorkOrderName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "IsTestOrder", "NAME_IsTestOrder")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsTestOrder { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "TargetOrderWeight", "NAME_Weight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double? TargetOrderWeight { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "FKProductCatalogueId", "NAME_FKProductCatalogueId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKProductCatalogueId { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "CreatedInL3", "NAME_CreatedInL3")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? WorkOrderCreatedInL3Ts { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "ToBeCompletedBefore", "NAME_ToBeCompletedBefore")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? ToBeCompletedBeforeTs { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "TargetOrderWeightMin", "NAME_TargetOrderWeightMin")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double? TargetOrderWeightMin { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "TargetOrderWeightMax", "NAME_TargetOrderWeightMax")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double? TargetOrderWeightMax { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "FKCustomerId", "NAME_FKCustomerId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKCustomerId { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "Shape", "NAME_Shape")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Shape { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "SteelGrade", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Steelgrade { get; set; }


    [SmfDisplay(typeof(VM_WorkOrderOverview), "IsScheduled", "NAME_IsScheduled")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsScheduled { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "TargetMaterialNumber", "NAME_TargetMaterialNumber")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short TargetMaterialNumber { get; set; }

    public long? FKHeatIdRef { get; set; }
    public long? FKMaterialCatalogueId { get; set; }

    public VM_MaterialCatalogue MC { get; set; }

    public VM_ProductCatalogue PC { get; set; }

    public VM_Steelgrade SC { get; set; }

    public VM_HeatOverview Heat { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "MetallicYield", "NAME_MetallicYield")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? MetallicYield { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "Completion", "NAME_Completion")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double Completion { get; set; }
  }
}
