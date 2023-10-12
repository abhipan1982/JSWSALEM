using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.Areas.MillConfigurator.ViewModels.MillConfiguration
{
  public class VM_FeatureInstance : VM_Base
  {
    public VM_FeatureInstance() { }

    public VM_FeatureInstance(MVHFeature data)
    {
      FeatureId = data.FeatureId;
      FKAssetId = data.FKAssetId;
      AssetName = data.FKAsset?.AssetName;
      FKUnitOfMeasureId = data.FKUnitOfMeasureId;
      FKExtUnitOfMeasureId = data.FKExtUnitOfMeasureId;
      FKDataTypeId = data.FKDataTypeId;
      DataTypeName = data?.FKDataType?.DataTypeName;
      DotNetDataType = data?.FKDataType?.DataTypeNameDotNet;
      FKParentFeatureId = data.FKParentFeatureId;
      ParentFeatureName = data?.FKParentFeature?.FeatureName;
      FeatureCode = data.FeatureCode;
      FeatureName = data.FeatureName;
      FeatureDescription = data.FeatureDescription;
      IsSampledFeature = data.IsSampledFeature;
      IsConsumptionFeature = data.IsConsumptionPoint;
      IsMaterialRelated = data.IsMaterialRelated.Value;
      IsLengthRelated = data.IsLengthRelated;
      IsQETrigger = data.IsQETrigger;
      IsDigital = data.IsDigital.Value;
      IsActive = data.IsActive.Value;
      IsOnHMI = data.IsOnHMI;
      IsTrackingPoint = data.IsTrackingPoint;
      IsMeasurementPoint = data.IsMeasurementPoint;
      SampleOffsetTime = data.SampleOffsetTime;
      ConsumptionAggregationTime = data.ConsumptionAggregationTime;
      MinValue = data.MinValue;
      MaxValue = data.MaxValue;
      RetentionFactor = data.RetentionFactor;
      EnumFeatureType = data.EnumFeatureType;
      EnumCommChannelType = data.EnumCommChannelType;
      EnumAggregationStrategy = data.EnumAggregationStrategy;
      EnumFeatureProvider = data.EnumFeatureProvider;
      EnumTagValidationResult = data.EnumTagValidationResult;
      CommAttr1 = data.CommAttr1;
      CommAttr2 = data.CommAttr2;
      CommAttr3 = data.CommAttr3;

      FeatureTypeText = ResxHelper.GetResxByKey(data.EnumFeatureType);
      CommChannelTypeText = ResxHelper.GetResxByKey(data.EnumCommChannelType);
      AggregationStrategyText = ResxHelper.GetResxByKey(data.EnumAggregationStrategy);
      FeatureProviderText = ResxHelper.GetResxByKey(data.EnumFeatureProvider);
      TagValidationResultText = ResxHelper.GetResxByKey(data.EnumTagValidationResult);
    }

    public VM_FeatureInstance(V_Feature data)
    {
      FeatureId = data.FeatureId;
      AssetName = data.AssetName;
      AssetId = data.AssetId;
      AreaName = data.AreaName;
      DataTypeName = data.DataTypeName;
      ParentFeatureName = data.ParentFeatureName;
      FeatureCode = data.FeatureCode;
      AssetCode = data.AssetCode;
      FeatureName = data.FeatureName;
      FeatureDescription = data.FeatureDescription;
      IsSampledFeature = data.IsSampledFeature;
      IsConsumptionFeature = data.IsConsumptionPoint;
      IsMaterialRelated = data.IsMaterialRelated;
      IsLengthRelated = data.IsLengthRelated;
      IsQETrigger = data.IsQETrigger;
      IsDigital = data.IsDigital;
      IsActive = data.IsActive;
      IsOnHMI = data.IsOnHMI;
      IsTrackingPoint = data.IsTrackingPoint;
      IsMeasurementPoint = data.IsMeasurementPoint;
      SampleOffsetTime = data.SampleOffsetTime;
      ConsumptionAggregationTime = data.ConsumptionAggregationTime;
      MinValue = data.MinValue;
      MaxValue = data.MaxValue;
      RetentionFactor = data.RetentionFactor;
      CommAttr1 = data.CommAddress;
      CommAttr2 = data.CommTagNameSpace;
      CommAttr3 = data.CommAttr3;
      FeatureTypeText = data.FeatureType;
      CommChannelTypeText = data.CommChannelType;
      EnumFeatureType = data.EnumFeatureType;
      EnumCommChannelType = data.EnumCommChannelType;
      EnumAggregationStrategy = data.EnumAggregationStrategy;
      AggregationStrategyText = data.AggregationStrategy;
      Shading = data.AssetSeq % 2 == 0 ? (short)1 : (short)0;
      OrderSeq = data.OrderSeq;
      UnitOfMeasureSymbol = data.UnitSymbol;
      UnitCategoryName = data.UnitCategoryName;
      IsDelayCheckpoint = data.IsDelayCheckpoint;
      EnumFeatureProvider = data.EnumFeatureProvider;
      FeatureProviderText = data.FeatureProvider;
      EnumTagValidationResult = data.EnumTagValidationResult;
      TagValidationResultText = data.TagValidationResult;
      MaxLength = data.MaxLength;
    }

    public long FeatureId { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "FKAssetId", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long FKAssetId { get; set; }
    public long AssetId { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "AreaName", "NAME_AreaName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AreaName { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "FKUnitOfMeasureId", "NAME_UnitOfMeasure")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long FKUnitOfMeasureId { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "UnitOfMeasure", "NAME_UnitOfMeasure")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string UnitOfMeasureSymbol { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "UnitCategoryName", "NAME_UnitCategoryName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string UnitCategoryName { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "FKExtUnitOfMeasureId", "NAME_ExternalUnitOfMeasure")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long FKExtUnitOfMeasureId { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "ExtUnitOfMeasureSymbol", "NAME_ExternalUnitOfMeasure")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ExtUnitOfMeasureSymbol { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "FKDataTypeId", "NAME_L1DataType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long FKDataTypeId { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "DataType", "NAME_DataType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DataTypeName { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "DotNetDataType", "NAME_DotNetDataType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DotNetDataType { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "FKParentFeatureId", "NAME_ParentFeature")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKParentFeatureId { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "ParentFeatureName", "NAME_ParentFeature")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ParentFeatureName { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "FeatureCode", "NAME_FeatureCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int FeatureCode { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "AssetCode", "NAME_AssetCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int AssetCode { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "FeatureName", "NAME_FeatureName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    [SmfStringLength(75)]
    public string FeatureName { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "FeatureDescription", "NAME_FeatureDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    [SmfStringLength(100)]
    public string FeatureDescription { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "IsSampledFeature", "NAME_IsSampledFeature")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsSampledFeature { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "IsConsumptionFeature", "NAME_IsConsumptionFeature")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsConsumptionFeature { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "IsMaterialRelated", "NAME_IsMaterialRelated")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public bool IsMaterialRelated { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "IsLengthRelated", "NAME_IsLengthRelated")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsLengthRelated { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "IsQETrigger", "NAME_IsQETrigger")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsQETrigger { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "IsDigital", "NAME_IsDigital")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public bool IsDigital { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "IsActive", "NAME_IsActive")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public bool IsActive { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "AssetName", "NAME_IsOnHMI")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsOnHMI { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "IsTrackingPoint", "NAME_IsTrackingPoint")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsTrackingPoint { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "IsMeasurementPoint", "NAME_IsMeasurementPoint")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsMeasurementPoint { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "SampleOffsetTime", "NAME_SampleOffsetTime")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    public double? SampleOffsetTime { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "ConsumptionAggregationTime", "NAME_ConsumptionAggregationTime")]
    [SmfFormat("FORMAT_Plain2", NullDisplayText = "-", HtmlEncode = false)]
    public double? ConsumptionAggregationTime { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "MinValue", "NAME_MinValue")]
    [SmfFormat("FORMAT_Plain2", NullDisplayText = "-", HtmlEncode = false)]
    public double? MinValue { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "MaxValue", "NAME_MaxValue")]
    [SmfFormat("FORMAT_Plain2", NullDisplayText = "-", HtmlEncode = false)]
    public double? MaxValue { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "RetentionFactor", "NAME_RetentionFactor")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    public int? RetentionFactor { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "EnumFeatureType", "NAME_FeatureType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumFeatureType { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "FeatureTypeText", "NAME_FeatureType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string FeatureTypeText { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "EnumCommChannelType", "NAME_CommChannelType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumCommChannelType { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "CommChannelTypeText", "NAME_CommChannelType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string CommChannelTypeText { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "EnumAggregationStrategy", "NAME_AggregationStrategy")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumAggregationStrategy { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "AggregationStrategyText", "NAME_AggregationStrategy")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AggregationStrategyText { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "CommAttr1", "NAME_CommAttr1")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(350)]
    public string CommAttr1 { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "CommAttr2", "NAME_CommAttr2")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(350)]
    public string CommAttr2 { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "CommAttr3", "NAME_CommAttr3")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(350)]
    public string CommAttr3 { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "OrderSeq", "NAME_SEQ")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long OrderSeq { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "EnumFeatureProvider", "NAME_FeatureProvider")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumFeatureProvider { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "FeatureProviderText", "NAME_FeatureProvider")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string FeatureProviderText { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "EnumTagValidationResult", "NAME_TagValidationResult")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumTagValidationResult { get; set; }

    [SmfDisplay(typeof(VM_FeatureInstance), "TagValidationResultText", "NAME_TagValidationResult")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string TagValidationResultText { get; set; }

    public short Shading { get; set; }

    public bool IsDelayCheckpoint { get; set; }

    public short? MaxLength { get; set; }
  }
}
