using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_Scrap : VM_Base
  {
    public VM_Scrap()
    {
    }

    public VM_Scrap(long rawMaterialId)
    {
      this.RawMaterialId = rawMaterialId;
    }

    public VM_Scrap(TRKRawMaterial data)
    {
      RawMaterialId = data.RawMaterialId;
      ScrapPercent = data.ScrapPercent;
      ScrapRemark = data.ScrapRemarks;
      AssetId = data.FKScrapAssetId;
      AssetName = data.FKScrapAsset?.AssetName;
      Percent = data.ScrapPercent;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long RawMaterialId { get; set; }

    [SmfDisplay(typeof(VM_Scrap), "AssetId", "NAME_Asset")]
    //[SmfRequired]
    public long? AssetId { get; set; }

    [SmfDisplay(typeof(VM_Scrap), "ScrapPercent", "NAME_Percent")]
    [SmfFormat("FORMAT_Percent_Scrap", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_Scrap), "ScrapPercent", "Percent")]
    [SmfUnit("UNIT_Percent")]
    public double? ScrapPercent { get; set; }

    [SmfDisplay(typeof(VM_Scrap), "ScrapPercent", "NAME_Percent")]
    [SmfFormat("FORMAT_Percent_Scrap", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_Scrap), "Percent", "Percent")]
    [SmfUnit("UNIT_Percent")]
    public double? Percent { get; set; }

    [SmfDisplay(typeof(VM_Scrap), "ScrapRemark", "NAME_Remark")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ScrapRemark { get; set; }

    [SmfDisplay(typeof(VM_Scrap), "AssetName", "NAME_AssetName")]
    public string AssetName { get; set; }
  }
}
