using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.QualityInspection
{
  public class VM_QualityInspection : VM_Base
  {
    public VM_QualityInspection()
    {
      CreatedTs = DateTime.Now;
      LastUpdateTs = DateTime.Now;
    }

    public VM_QualityInspection(long rawMaterialId)
    {
      this.RawMaterialId = rawMaterialId;
    }

    public VM_QualityInspection(QTYQualityInspection data)
    {
      RawMaterialId = data.FKRawMaterialId.Value;
      DiameterMin = data.DiameterMin;
      DiameterMax = data.DiameterMax;
      VisualInspection = data.VisualInspection;
      CrashTestEnum = BaseDbEntity.EnumClasses.CrashTest.GetValue(data.EnumCrashTest);
      InspectionResultEnum = BaseDbEntity.EnumClasses.InspectionResult.GetValue(data.EnumInspectionResult);
      CrashTest = ResxHelper.GetResxByKey("NAME_InspectionResult_" + CrashTestEnum.Name);
      InspectionResult = ResxHelper.GetResxByKey("NAME_InspectionResult_" + InspectionResultEnum.Name);
      EnumCrashTest = (data.EnumCrashTest);
      EnumInspectionResult = (data.EnumInspectionResult);

      if (InspectionResultEnum == BaseDbEntity.EnumClasses.InspectionResult.Undefined)
        InspectionResultImageSRC = "result_warn_blue";
      else if (InspectionResultEnum == BaseDbEntity.EnumClasses.InspectionResult.Good)
        InspectionResultImageSRC = "result_true";
      else if (InspectionResultEnum == BaseDbEntity.EnumClasses.InspectionResult.Bad)
        InspectionResultImageSRC = "result_false";
      else if (InspectionResultEnum == BaseDbEntity.EnumClasses.InspectionResult.Doubtful)
        InspectionResultImageSRC = "result_warn";

      if (CrashTestEnum == BaseDbEntity.EnumClasses.CrashTest.Undefined)
        CrashTestImageSRC = "result_warn_blue";
      else if (CrashTestEnum == BaseDbEntity.EnumClasses.CrashTest.Good)
        CrashTestImageSRC = "result_true";
      else if (CrashTestEnum == BaseDbEntity.EnumClasses.CrashTest.Bad)
        CrashTestImageSRC = "result_false";
      else if (CrashTestEnum == BaseDbEntity.EnumClasses.CrashTest.Doubtful)
        CrashTestImageSRC = "result_warn";

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_QualityInspection(V_QualityInspectionSearchGrid material)
    {
      if (material != null)
      {
        RawMaterialId = material.RawMaterialId;
        RawMaterialName = material.RawMaterialName;
        MaterialName = material.MaterialName;
        HeatName = material.HeatName;
        WorkOrderName = material.WorkOrderName;
        FKMaterialId = material.MaterialId;
        DefectsNumber = material.DefectsNumber;
        HasDefects = material.DefectsNumber > 0;
        HasDefectsText = HasDefects ? ResxHelper.GetResxByKey("NAME_Yes") : ResxHelper.GetResxByKey("NAME_No");
        SteelgradeCode = material.SteelgradeName;
        SteelgradeName = material.SteelgradeName;
        QualityResultEnum = BaseDbEntity.EnumClasses.InspectionResult.GetValue(material.EnumInspectionResult);
        EnumInspectionResult = (material.EnumInspectionResult);
        InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + QualityResultEnum.Name);
        MaterialCreatedTs = material.MaterialCreatedTs;
        MaterialStartTs = material.MaterialStartTs;
        MaterialEndTs = material.MaterialEndTs;
        ProductCreatedTs = material.ProductCreatedTs;
        RollingStartTs = material.RollingStartTs;
        RollingEndTs = material.RollingEndTs;

        if (QualityResultEnum == BaseDbEntity.EnumClasses.InspectionResult.Undefined)
        {
          InspectionResultImageSRC = "result_warn_blue";
          InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + BaseDbEntity.EnumClasses.InspectionResult.Undefined.Name);
        }
        else if (QualityResultEnum == BaseDbEntity.EnumClasses.InspectionResult.Good)
        {
          InspectionResultImageSRC = "result_true";
          InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + BaseDbEntity.EnumClasses.InspectionResult.Good.Name);
        }
        else if (QualityResultEnum == BaseDbEntity.EnumClasses.InspectionResult.Bad)
        {
          InspectionResultImageSRC = "result_false";
          InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + BaseDbEntity.EnumClasses.InspectionResult.Bad.Name);
        }
        else if (QualityResultEnum == BaseDbEntity.EnumClasses.InspectionResult.Doubtful)
        {
          InspectionResultImageSRC = "result_warn";
          InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + BaseDbEntity.EnumClasses.InspectionResult.Doubtful.Name);
        }

        if (HasDefects)
          HasDefectImageSRC = "result_false";
        else
          HasDefectImageSRC = "result_true";
      }

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_QualityInspection(V_QualityBundlesInspectionSearchGrid material)
    {
      if (material != null)
      {
        RawMaterialId = material.RawMaterialId;
        RawMaterialName = material.RawMaterialName;
        HeatName = material.HeatName;
        WorkOrderName = material.WorkOrderName;
        DefectsNumber = material.DefectsNumber;
        HasDefects = material.DefectsNumber > 0;
        HasDefectsText = HasDefects ? ResxHelper.GetResxByKey("NAME_Yes") : ResxHelper.GetResxByKey("NAME_No");
        SteelgradeCode = material.SteelgradeName;
        SteelgradeName = material.SteelgradeName;
        QualityResultEnum = BaseDbEntity.EnumClasses.InspectionResult.GetValue(material.EnumInspectionResult);
        EnumInspectionResult = (material.EnumInspectionResult);
        InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + QualityResultEnum.Name);
        ProductCreatedTs = material.ProductCreatedTs;
        RollingStartTs = material.RollingStartTs;
        RollingEndTs = material.RollingEndTs;

        if (QualityResultEnum == BaseDbEntity.EnumClasses.InspectionResult.Undefined)
        {
          InspectionResultImageSRC = "result_warn_blue";
          InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + BaseDbEntity.EnumClasses.InspectionResult.Undefined.Name);
        }
        else if (QualityResultEnum == BaseDbEntity.EnumClasses.InspectionResult.Good)
        {
          InspectionResultImageSRC = "result_true";
          InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + BaseDbEntity.EnumClasses.InspectionResult.Good.Name);
        }
        else if (QualityResultEnum == BaseDbEntity.EnumClasses.InspectionResult.Bad)
        {
          InspectionResultImageSRC = "result_false";
          InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + BaseDbEntity.EnumClasses.InspectionResult.Bad.Name);
        }
        else if (QualityResultEnum == BaseDbEntity.EnumClasses.InspectionResult.Doubtful)
        {
          InspectionResultImageSRC = "result_warn";
          InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + BaseDbEntity.EnumClasses.InspectionResult.Doubtful.Name);
        }

        if (HasDefects)
          HasDefectImageSRC = "result_false";
        else
          HasDefectImageSRC = "result_true";
      }

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_QualityInspection(V_RawMaterialOverview material)
    {
      RawMaterialId = material.RawMaterialId;
      RawMaterialName = material.RawMaterialName;
      MaterialName = material.MaterialName;
      HeatName = material.HeatName;
      WorkOrderName = material.WorkOrderName;
      DefectsNumber = material.DefectsNumber;
      HasDefects = material.DefectsNumber > 0;
      HasDefectsText = HasDefects ? ResxHelper.GetResxByKey("NAME_Yes") : ResxHelper.GetResxByKey("NAME_No");
      SteelgradeCode = material.SteelgradeName;
      SteelgradeName = material.SteelgradeName;
      QualityResultEnum = BaseDbEntity.EnumClasses.InspectionResult.GetValue(material.EnumInspectionResult);
      EnumInspectionResult = material.EnumInspectionResult;
      InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + QualityResultEnum.Name);
      CrashTestEnum = BaseDbEntity.EnumClasses.CrashTest.GetValue(material.EnumCrashTest);

      if (QualityResultEnum == BaseDbEntity.EnumClasses.InspectionResult.Undefined)
      {
        InspectionResultImageSRC = "result_warn_blue";
        InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + BaseDbEntity.EnumClasses.InspectionResult.Undefined.Name);
      }
      else if (QualityResultEnum == BaseDbEntity.EnumClasses.InspectionResult.Good)
      {
        InspectionResultImageSRC = "result_true";
        InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + BaseDbEntity.EnumClasses.InspectionResult.Good.Name);
      }
      else if (QualityResultEnum == BaseDbEntity.EnumClasses.InspectionResult.Bad)
      {
        InspectionResultImageSRC = "result_false";
        InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + BaseDbEntity.EnumClasses.InspectionResult.Bad.Name);
      }
      else if (QualityResultEnum == BaseDbEntity.EnumClasses.InspectionResult.Doubtful)
      {
        InspectionResultImageSRC = "result_warn";
        InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + BaseDbEntity.EnumClasses.InspectionResult.Doubtful.Name);
      }

      if (CrashTestEnum == BaseDbEntity.EnumClasses.CrashTest.Undefined)
      {
        CrashTestImageSRC = "result_warn_blue";
        CrashTestText = ResxHelper.GetResxByKey("ENUM_CrashTest_" + BaseDbEntity.EnumClasses.CrashTest.Undefined.Name);
      }
      else if (CrashTestEnum == BaseDbEntity.EnumClasses.CrashTest.Good)
      {
        CrashTestImageSRC = "result_true";
        CrashTestText = ResxHelper.GetResxByKey("ENUM_CrashTest_" + BaseDbEntity.EnumClasses.CrashTest.Good.Name);
      }
      else if (CrashTestEnum == BaseDbEntity.EnumClasses.CrashTest.Bad)
      {
        CrashTestImageSRC = "result_false";
        CrashTestText = ResxHelper.GetResxByKey("ENUM_CrashTest_" + BaseDbEntity.EnumClasses.CrashTest.Bad.Name);
      }
      else if (CrashTestEnum == BaseDbEntity.EnumClasses.CrashTest.Doubtful)
      {
        CrashTestImageSRC = "result_warn";
        CrashTestText = ResxHelper.GetResxByKey("ENUM_CrashTest_" + BaseDbEntity.EnumClasses.CrashTest.Doubtful.Name);
      }

      if (HasDefects)
        HasDefectImageSRC = "result_false";
      else
        HasDefectImageSRC = "result_true";

      UnitConverterHelper.ConvertToLocal(this);
    }

    public DateTime CreatedTs { get; set; }
    public DateTime LastUpdateTs { get; set; }
    public long RawMaterialId { get; set; }
    public long FKMaterialId { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "MaterialName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "RawMaterialName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string RawMaterialName { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "WorkOrderName", "NAME_WorkOrder")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "DiameterMin", "NAME_DiameterMinium")]
    [SmfFormat("FORMAT_Length", NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Length")]
    public double? DiameterMin { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "DiameterMax", "NAME_DiameterMaximum")]
    [SmfFormat("FORMAT_Length", NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Length")]
    public double? DiameterMax { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "CrashTestEnum", "NAME_CrashTest")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public CrashTest CrashTestEnum { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "EnumCrashTest", "NAME_CrashTest")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumCrashTest { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "CrashTest", "NAME_CrashTest")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string CrashTest { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "VisualInspection", "NAME_VisualInspection")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string VisualInspection { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "FKInspectionResult", "NAME_InspectionResult")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public short FKInspectionResult { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "InspectionResultEnum", "NAME_InspectionResult")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public InspectionResult InspectionResultEnum { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "InspectionResult", "NAME_InspectionResult")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string InspectionResult { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "EnumInspectionResult", "NAME_QualityResult")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public short EnumInspectionResult { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "QualityResultEnum", "NAME_QualityResult")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public InspectionResult QualityResultEnum { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "QualityResult", "NAME_QualityResult")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string QualityResult { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "DefectsNumber", "NAME_Defects")]
    public int DefectsNumber { get; set; }

    public bool HasDefects { get; set; }
    public string HasDefectsText { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "SteelgradeCode", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }

    public string SteelgradeName { get; set; }
    public string InspectionResultImageSRC { get; set; }
    public string CrashTestImageSRC { get; set; }
    public string HasDefectImageSRC { get; set; }
    public string InspectionResultText { get; set; }
    public string CrashTestText { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "MaterialCreatedTs", "NAME_MaterialCreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime MaterialCreatedTs { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "MaterialStartTs", "NAME_MaterialStartTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? MaterialStartTs { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "MaterialEndTs", "NAME_MaterialEndTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? MaterialEndTs { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "ProductCreatedTs", "NAME_ProductCreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? ProductCreatedTs { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "RollingStartTs", "NAME_RollingStartTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? RollingStartTs { get; set; }

    [SmfDisplay(typeof(VM_QualityInspection), "RollingEndTs", "NAME_RollingEndTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? RollingEndTs { get; set; }
  }
}
