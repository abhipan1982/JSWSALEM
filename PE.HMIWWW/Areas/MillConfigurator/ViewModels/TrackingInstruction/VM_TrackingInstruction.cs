using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.Areas.MillConfigurator.ViewModels.TrackingInstruction
{
  public class VM_TrackingInstruction : VM_Base
  {
    public VM_TrackingInstruction() { }

    public VM_TrackingInstruction(TRKTrackingInstruction data)
    {
      TrackingInstructionId = data.TrackingInstructionId;
      FKFeatureId = data.FKFeatureId;
      FeatureName = data.FKFeature.FeatureName;
      FeatureCode = data.FKFeature.FeatureCode;
      FKAreaAssetId = data.FKAreaAssetId;
      AreaName = data.FKAreaAsset?.AssetName;
      FKPointAssetId = data.FKPointAssetId;
      FKParentTrackingInstructionId = data.FKParentTrackingInstructionId;
      SeqNo = data.SeqNo;
      TrackingInstructionValue = data.TrackingInstructionValue;
      EnumTrackingInstructionType = data.EnumTrackingInstructionType;
      ChannelId = data.ChannelId;
      TimeFilter = data.TimeFilter;
      IsAsync = data.IsAsync;
      IsIgnoredIfSimulation = data.IsIgnoredIfSimulation;

      PointName = data.FKPointAsset?.AssetName;
      EnumTrackingInstructionTypeText = data.EnumTrackingInstructionType.Name;
    }

    public VM_TrackingInstruction(TRKTrackingInstruction data, TRKTrackingInstruction parent)
    {
      TrackingInstructionId = data.TrackingInstructionId;
      FKFeatureId = data.FKFeatureId;
      FeatureName = data.FKFeature.FeatureName;
      FeatureCode = data.FKFeature.FeatureCode;
      FKAreaAssetId = data.FKAreaAssetId;
      AreaName = data.FKAreaAsset?.AssetName;
      FKPointAssetId = data.FKPointAssetId;
      FKParentTrackingInstructionId = data.FKParentTrackingInstructionId;
      SeqNo = data.SeqNo;
      TrackingInstructionValue = data.TrackingInstructionValue;
      EnumTrackingInstructionType = data.EnumTrackingInstructionType;
      ChannelId = data.ChannelId;
      TimeFilter = data.TimeFilter;
      IsAsync = data.IsAsync;
      IsIgnoredIfSimulation = data.IsIgnoredIfSimulation;

      PointName = data.FKPointAsset?.AssetName;
      EnumTrackingInstructionTypeText = data.EnumTrackingInstructionType.Name;
      ParentTrackingInstructionName = $"{parent.FKFeature.FeatureName} {VM_Resources.NAME_Area}: {parent.FKAreaAsset.AssetName} {VM_Resources.NAME_SEQ}: {parent.SeqNo} {VM_Resources.NAME_Value}: {data.TrackingInstructionValue}";
    }

    public VM_TrackingInstruction(V_TrackingInstruction data)
    {
      TrackingInstructionId = data.TrackingInstructionId;
      FeatureName = data.FeatureName;
      AreaName = data.AreaName;
      PointName = data.PointName;
      SeqNo = data.SeqNo;
      TrackingInstructionValue = data.TrackingInstructionValue;
      EnumTrackingInstructionType = data.EnumTrackingInstructionType;
      EnumTrackingInstructionTypeText = TrackingInstructionType.GetValue(data.EnumTrackingInstructionType).Name;
      FeatureCode = data.FeatureCode;
    }

    public long TrackingInstructionId { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "FKFeatureId", "NAME_Feature")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public long FKFeatureId { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "FeatureName", "NAME_Feature")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string FeatureName { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "FKAreaAssetId", "NAME_Area")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public long FKAreaAssetId { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "AreaName", "NAME_Area")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AreaName { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "FKPointAssetId", "NAME_Point")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKPointAssetId { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "PointName", "NAME_Point")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string PointName { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "FKParentTrackingInstructionId", "NAME_ParentTrackingInstruction")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKParentTrackingInstructionId { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "ParentTrackingInstructionName", "NAME_ParentTrackingInstruction")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ParentTrackingInstructionName { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "SeqNo", "NAME_SEQ")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    public short SeqNo { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "TrackingInstructionValue", "NAME_Value")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    public short? TrackingInstructionValue { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "EnumTrackingInstructionType", "NAME_Type")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumTrackingInstructionType { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "EnumTrackingInstructionTypeText", "NAME_Type")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string EnumTrackingInstructionTypeText { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "ChannelId", "NAME_Channel")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public short ChannelId { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "TimeFilter", "NAME_TimeFilter")]
    [SmfFormat("FORMAT_Plain2", NullDisplayText = "-", HtmlEncode = false)]
    public double? TimeFilter { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "IsAsync", "NAME_IsAsync")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsAsync { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "IsIgnoredIfSimulation", "NAME_IsIgnoredIfSimulation")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsIgnoredIfSimulation { get; set; }

    [SmfDisplay(typeof(VM_TrackingInstruction), "FeatureCode", "NAME_FeatureCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int FeatureCode { get; set; }
  }
}
