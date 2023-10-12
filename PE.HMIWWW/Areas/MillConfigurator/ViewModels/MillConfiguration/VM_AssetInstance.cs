using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.Areas.MillConfigurator.ViewModels.MillConfiguration
{
  public class VM_AssetInstance : VM_Base
  {
    public VM_AssetInstance() { }

    public VM_AssetInstance(MVHAsset data)
    {
      CheckAssetId = data.AssetId.ToString();
      AssetId = data.AssetId;
      OrderSeq = data.OrderSeq;
      AssetCode = data.AssetCode;
      AssetName = data.AssetName;
      AssetDescription = data.AssetDescription;
      FKParentAssetId = data.FKParentAssetId;
      FKAssetTypeId = data.FKAssetTypeId;
      IsActive = data.IsActive.Value;
      IsArea = data.IsArea;
      IsZone = data.IsZone;
      IsTrackingPoint = data.IsTrackingPoint.Value;
      IsDelayCheckpoint = data.IsDelayCheckpoint;
      IsReversible = data.IsReversible;
      EnumTrackingAreaType = data.EnumTrackingAreaType;
      IsPositionBased = data.IsPositionBased;
      PositionsNumber = data.PositionsNumber;
      VirtualPositionsNumber = data.VirtualPositionsNumber;
      IsVisibleOnMVH = data.IsVisibleOnMVH;

      EnumTrackingAreaTypeText = ResxHelper.GetResxByKey(data.EnumTrackingAreaType);
    }

    public VM_AssetInstance(V_Asset data)
    {
      CheckAssetId = data.AssetId.ToString();
      AssetId = data.AssetId;
      OrderSeq = (int)data.OrderSeq;
      AssetCode = data.AssetCode;
      AssetName = data.AssetName;
      AssetDescription = data.AssetDescription;
      ParentAssetId = data.ParentAssetId;
      IsActive = data.IsActive;
      IsArea = data.IsArea;
      IsZone = data.IsZone;
      IsTrackingPoint = data.IsTrackingPoint;
      IsDelayCheckpoint = data.IsDelayCheckpoint;
      EnumTrackingAreaType = data.EnumTrackingAreaType;
      IsPositionBased = data.IsPositionBased;
      PositionsNumber = data.PositionsNumber;
      VirtualPositionsNumber = data.VirtualPositionsNumber;
      L1Features = data.L1Features;
      ValidFeatures = data.ValidFeatures;
      AllFeatures = data.AllFeatures;
      FeaturesAreValid = data.FeaturesAreValid.Value;
    }

    [ScaffoldColumn(false)]
    public long AssetId { get; set; }

    public string CheckAssetId { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "OrderSeq", "NAME_SEQ")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int OrderSeq { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "AssetCode", "NAME_AssetCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int AssetCode { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    [SmfStringLength(50)]
    public string AssetName { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "AssetDescription", "NAME_AssetDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(100)]
    [SmfRequired]
    public string AssetDescription { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "FKParentAssetId", "NAME_ParentAsset")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [ScaffoldColumn(false)]
    public long? FKParentAssetId { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "ParentAssetId", "NAME_ParentAsset")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [ScaffoldColumn(false)]
    public long? ParentAssetId { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "ParentAssetName", "NAME_ParentAsset")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ParentAssetName { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "FKAssetTypeId", "NAME_AssetType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKAssetTypeId { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "AssetType", "NAME_AssetType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetType { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "IsActive", "NAME_IsActive")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public bool IsActive { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "IsArea", "NAME_IsArea")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsArea { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "IsZone", "NAME_IsZone")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsZone { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "IsTrackingPoint", "NAME_IsTrackingPoint")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public bool IsTrackingPoint { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "IsDelayCheckpoint", "NAME_IsDelayCheckpoint")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsDelayCheckpoint { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "IsReversible", "NAME_IsReversible")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsReversible { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "EnumTrackingAreaType", "NAME_TrackingAreaType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumTrackingAreaType { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "EnumTrackingAreaType", "NAME_TrackingAreaType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string EnumTrackingAreaTypeText { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "IsPositionBased", "NAME_IsPositionBased")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsPositionBased { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "PositionsNumber", "NAME_PositionsNumber")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    public short? PositionsNumber { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "VirtualPositionsNumber", "NAME_VirtualPositionsNumber")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    public short? VirtualPositionsNumber { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "AssetTemplateId", "NAME_AssetTemplate")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? AssetTemplateId { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "IsVisibleOnMVH", "NAME_IsVisibleOnMVH")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsVisibleOnMVH { get; set; }

    [SmfDisplay(typeof(VM_AssetInstance), "FeaturesAreValid", "NAME_Valid")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool FeaturesAreValid { get; set; }

    public int ValidFeatures { get; set; }

    public int L1Features { get; set; }

    public int AllFeatures { get; set; }
  }
}
