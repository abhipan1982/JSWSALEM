using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.Areas.MillConfigurator.ViewModels.MillConfiguration
{
  public class VM_AssetType : VM_Base
  {
    public VM_AssetType() { }

    public VM_AssetType(MVHAssetType data)
    {
      AssetTypeId = data.AssetTypeId;
      AssetTypeCode = data.AssetTypeCode;
      AssetTypeName = data.AssetTypeName;
      AssetTypeDescription = data.AssetTypeDescription;
      EnumYardType = data.EnumYardType;

      YardTypeText = ResxHelper.GetResxByKey(data.EnumYardType);
    }

    public long AssetTypeId { get; set; }

    [SmfDisplay(typeof(VM_AssetType), "AssetTypeCode", "NAME_AssetTypeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(10)]
    [SmfRequired]
    public string AssetTypeCode { get; set; }

    [SmfDisplay(typeof(VM_AssetType), "AssetTypeName", "NAME_AssetTypeName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(50)]
    [SmfRequired]
    public string AssetTypeName { get; set; }

    [SmfDisplay(typeof(VM_AssetType), "AssetTypeDescription", "NAME_AssetTypeDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(200)]
    [SmfRequired]
    public string AssetTypeDescription { get; set; }

    [SmfDisplay(typeof(VM_AssetType), "EnumYardType", "NAME_YardType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumYardType { get; set; }

    [SmfDisplay(typeof(VM_AssetType), "YardTypeText", "NAME_YardType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string YardTypeText { get; set; }
  }
}
