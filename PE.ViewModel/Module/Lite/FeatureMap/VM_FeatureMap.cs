using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.UnitConverter;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.FeatureMap
{
  public class VM_FeatureMap : VM_Base
  {
    public VM_FeatureMap() { }

    public VM_FeatureMap(V_Feature table)
    {
      FeatureId = table.FeatureId;
      OrderSeq = table.OrderSeq;
      AssetCode = table.AssetCode;
      AssetName = table.AssetName;
      AreaName = table.AreaName;
      IsDelayCheckpoint = table.IsDelayCheckpoint;
      FeatureCode = table.FeatureCode;
      FeatureName = table.FeatureName;
      UnitSymbol = table.UnitSymbol;
      DataTypeName = table.DataTypeName;
      IsMaterialRelated = table.IsMaterialRelated;
      IsLengthRelated = table.IsLengthRelated;
      IsActive = table.IsActive;
      IsSampledFeature = table.IsSampledFeature;
      UnitId = table.UnitId;
      MinValue = table.MinValue;
      MaxValue = table.MaxValue;

      FeatureUnitConverterHelper.ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_FeatureMap), "SeqNo", "NAME_SeqNo")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? OrderSeq { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "AssetCode", "NAME_AssetCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int AssetCode { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "AreaName", "NAME_AreaName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AreaName { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "AreaCode", "NAME_AreaCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short AreaCode { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "IsCheckpoint", "NAME_IsCheckpoint")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsDelayCheckpoint { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "FeatureCode", "NAME_FeatureCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int FeatureCode { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "FeatureName", "NAME_FeatureName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string FeatureName { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "IsNewProcessingStep", "NAME_IsNewProcessingStep")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsNewProcessingStep { get; set; }

    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsSampledFeature { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "UnitSymbol", "NAME_UnitSymbol")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [FeatureUnitSymbol(nameof(UnitId), nameof(FeatureId), false)]
    public string UnitSymbol { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "DataTypeName", "NAME_DataTypeName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DataTypeName { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "IsMaterialRelated", "NAME_IsMaterialRelated")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsMaterialRelated { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "IsLengthRelated", "NAME_IsLengthRelated")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsLengthRelated { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "IsActive", "NAME_IsActive")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsActive { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "IsTrigger", "NAME_IsTrigger")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsTrackingTrigger { get; set; }

    public long FeatureId { get; set; }

    public long UnitId { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "MinValue", "NAME_Min")]
    [SmfFormat("FORMAT_Plain3", NullDisplayText = "-", HtmlEncode = false)]
    public double? MinValue { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "MaxValue", "NAME_Max")]
    [SmfFormat("FORMAT_Plain3", NullDisplayText = "-", HtmlEncode = false)]
    public double? MaxValue { get; set; }
  }
}
