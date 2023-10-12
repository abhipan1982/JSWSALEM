using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;


namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_RawMaterialOverview : VM_Base
  {
    public VM_RawMaterialOverview(DbEntity.SPModels.SPL3L1MaterialInArea material)
    {
      OrderSeq = material.OrderSeq;
      RawMaterialName = material.RawMaterialName;
      Weight = material.LastWeight;
      Length = material.LastLength;
      WorkOrderName = material.WorkOrderName;
      SteelgradeCode = material.SteelgradeCode;
      SteelgradeName = material.SteelgradeCode;
      HeatName = material.HeatName;
      RawMaterialId = material.RawMaterialId;
      AreaCode = material.AreaCode;
      WorkOrderId = material.WorkOrderId;
      HeatId = material.HeatId;
      SteelgradeId = material.SteelgradeId;
    }

    public VM_RawMaterialOverview(DbEntity.SPModels.SPL3L1MaterialInArea material, short orderSeq) : this(material)
    {
      OrderSeq = orderSeq;
    }

    public VM_RawMaterialOverview(TRKRawMaterial material)
    {
      RawMaterialId = material.RawMaterialId;
      IsDummy = material.IsDummy;
      IsVirtual = material.IsVirtual;
      RawMaterialName = material.RawMaterialName;
      FKMaterialId = material.FKMaterialId;
      FKProductId = material.FKProductId;
      ProductName = material?.FKProduct?.ProductName;
      Status = ResxHelper.GetResxByKey(material.EnumRawMaterialStatus);
      CuttingSeqNo = material.CuttingSeqNo;
      ChildsNo = material.ChildsNo;
      SlittingFactor = material.SlittingFactor;
      Weight = material.FKMaterial?.MaterialWeight;
      LayerStatus = ResxHelper.GetResxByKey(material.EnumLayerStatus);
      RawMaterialCreatedTs = material.RawMaterialCreatedTs;
      WorkOrderId = material?.FKMaterial?.FKWorkOrderId ?? material.FKProduct?.FKWorkOrderId;
      WorkOrderName = material?.FKMaterial?.FKWorkOrder?.WorkOrderName ?? material?.FKProduct?.FKWorkOrder?.WorkOrderName;
      HeatId = material?.FKMaterial?.FKWorkOrder?.FKHeatId ?? material?.FKProduct?.FKWorkOrder?.FKHeatId;
      HeatName = material?.FKMaterial?.FKWorkOrder?.FKHeat?.HeatName ?? material?.FKProduct?.FKWorkOrder?.FKHeat?.HeatName;
      SteelgradeId = material?.FKMaterial?.FKWorkOrder?.FKHeat?.FKSteelgradeId ?? material?.FKProduct?.FKWorkOrder?.FKHeat?.FKSteelgradeId;
      SteelgradeCode = material?.FKMaterial?.FKWorkOrder?.FKHeat?.FKSteelgrade?.SteelgradeCode ?? material?.FKProduct?.FKWorkOrder?.FKHeat?.FKSteelgrade?.SteelgradeCode;
      Length = material.LastLength;
      RejectLocationText = ResxHelper.GetResxByKey(material.EnumRejectLocation);
      TypeOfScrapText = ResxHelper.GetResxByKey(material.EnumTypeOfScrap);
      Weight = material?.LastWeight;
      EnumInspectionResult = material?.QTYQualityInspection?.EnumInspectionResult;
      EnumRawMaterialType = RawMaterialType.GetValue(material.EnumRawMaterialType);

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_RawMaterialOverview(TRKRawMaterial material, TRKRawMaterial parentMaterial)
    {
      RawMaterialId = material.RawMaterialId;
      IsDummy = material.IsDummy;
      IsVirtual = material.IsVirtual;
      RawMaterialName = material.RawMaterialName;
      FKMaterialId = material.FKMaterialId;
      FKProductId = material.FKProductId;
      ProductName = material?.FKProduct?.ProductName;
      ParentRawMaterial = parentMaterial?.RawMaterialName;
      Status = ResxHelper.GetResxByKey(material.EnumRawMaterialStatus);
      CuttingSeqNo = material.CuttingSeqNo;
      ChildsNo = material.ChildsNo;
      SlittingFactor = material.SlittingFactor;
      Weight = material.FKMaterial?.MaterialWeight;
      LayerStatus = ResxHelper.GetResxByKey(material.EnumLayerStatus);
      RawMaterialCreatedTs = material.RawMaterialCreatedTs;
      WorkOrderId = material?.FKMaterial?.FKWorkOrderId ?? material.FKProduct?.FKWorkOrderId;
      WorkOrderName = material?.FKMaterial?.FKWorkOrder?.WorkOrderName ?? material?.FKProduct?.FKWorkOrder?.WorkOrderName;
      HeatId = material?.FKMaterial?.FKWorkOrder?.FKHeatId ?? material?.FKProduct?.FKWorkOrder?.FKHeatId;
      HeatName = material?.FKMaterial?.FKWorkOrder?.FKHeat?.HeatName ?? material?.FKProduct?.FKWorkOrder?.FKHeat?.HeatName;
      SteelgradeId = material?.FKMaterial?.FKWorkOrder?.FKHeat?.FKSteelgradeId ?? material?.FKProduct?.FKWorkOrder?.FKHeat?.FKSteelgradeId;
      SteelgradeCode = material?.FKMaterial?.FKWorkOrder?.FKHeat?.FKSteelgrade?.SteelgradeCode ?? material?.FKProduct?.FKWorkOrder?.FKHeat?.FKSteelgrade?.SteelgradeCode;
      Length = material.LastLength;
      RejectLocationText = ResxHelper.GetResxByKey(material.EnumRejectLocation);
      TypeOfScrapText = ResxHelper.GetResxByKey(material.EnumTypeOfScrap);
      Weight = material?.LastWeight;
      EnumInspectionResult = material?.QTYQualityInspection?.EnumInspectionResult;
      EnumRawMaterialType = RawMaterialType.GetValue(material.EnumRawMaterialType);

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_RawMaterialOverview(V_RawMaterialOverview material)
    {
      if (material != null)
      {
        RawMaterialId = material.RawMaterialId;
        IsVirtual = material.RawMaterialIsVirtual;
        DisplayedMaterialName = material.DisplayedMaterialName;
        RawMaterialName = material.RawMaterialName;
        Weight = material.WeighingStationWeight;
        HeatName = material.HeatName;
        WorkOrderName = material.WorkOrderName;
        FKMaterialId = material.MaterialId;
        ShiftCode = material.ShiftCode;
        DefectsNumber = material.DefectsNumber;
        HasDefects = material.DefectsNumber > 0;
        SteelgradeCode = material.SteelgradeName;
        SteelgradeName = material.SteelgradeName;
        EnumInspectionResult = material.EnumInspectionResult;
        QualityResultEnum = InspectionResult.GetValue(EnumInspectionResult ?? 0);
        Status = material.RawMaterialStatus;
        ScrapRemarks = material.ScrapRemarks;
        ScrapPercent = material.ScrapPercent;
        EnumTypeOfScrap = material.EnumTypeOfScrap;
        OutputPieces = material.OutputPieces;
        EnumRejectLocation = material.EnumRejectLocation;
        EnumRawMaterialType = material.EnumRawMaterialType;
      }

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_RawMaterialOverview() { }
    public long? Sorting { get; set; }

    public long? WorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "RawMaterialId", "NAME_MaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? RawMaterialId { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "IsDummy", "NAME_IsDummyMaterial")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsDummy { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "IsVirtual", "NAME_IsVirtual")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsVirtual { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "RawMaterialName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string RawMaterialName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "DisplayedMaterialName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DisplayedMaterialName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "FKMaterialId", "NAME_FKMaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKMaterialId { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "FKProductId", "NAME_FKProductId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKProductId { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "ProductName", "NAME_ProductName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "ParentRawMaterial", "NAME_ParentName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ParentRawMaterial { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "Status", "NAME_MaterialStatus")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public virtual string Status { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "LastProcessingStepNo", "NAME_LastStepNo")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short LastProcessingStepNo { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "CuttingSeqNo", "NAME_CuttingSeqNo")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? CuttingSeqNo { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "ChildsNo", "NAME_ChildsNo")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? ChildsNo { get; set; }

    public bool AfterL3Assign { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "Weight", "NAME_Weight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? Weight { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "Length", "NAME_Length")]
    [SmfFormat("FORMAT_Length", NullDisplayText = "-")]
    [SmfUnit("UNIT_Length")]
    public double? Length { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "WorkOrderName", "NAME_WorkOrder")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    public string ShiftCode { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "SteelgradeCode", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }

    public string SteelgradeName { get; set; }

    public short OrderSeq { get; set; }

    public short? SlittingFactor { get; set; }

    public bool HasDefects { get; set; }
    public InspectionResult QualityResultEnum { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "QualityResult", "NAME_QualityResult")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? EnumInspectionResult { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "CountDefects", "NAME_Defects")]
    public int DefectsNumber { get; set; }

    public VM_RawMaterialMeasurements Measurements { get; set; }
    public VM_RawMaterialHistory History { get; set; }


    [SmfDisplay(typeof(VM_RawMaterialOverview), "ScrapRemarks", "NAME_Remark")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ScrapRemarks { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "ScrapPercent", "NAME_Percent")]
    [SmfFormat("FORMAT_Percent", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? ScrapPercent { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "EnumTypeOfScrap", "NAME_Type")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? EnumTypeOfScrap { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "TypeOfScrapText", "NAME_TypeOfScrap")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string TypeOfScrapText { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "EnumRejectLocation", "NAME_RejectLocation")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? EnumRejectLocation { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "RejectLocationText", "NAME_RejectLocation")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string RejectLocationText { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "OutputPieces", "NAME_NumberOfOutputPieces")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short OutputPieces { get; set; }

    public int AreaCode { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "LayerStatus", "NAME_LayerStatus")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string LayerStatus { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "RawMaterialCreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime RawMaterialCreatedTs { get; set; }
    public long? HeatId { get; set; }
    public long? SteelgradeId { get; set; }

    public bool LayerSelected { get; set; }

    public short EnumRawMaterialType { get; set; }
  }
}
