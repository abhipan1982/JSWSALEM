using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.ProductYard
{
  public class VM_Location : VM_Base
  {
    public VM_Location(V_AssetsLocationOverview location)
    {
      AreaId = location.ParentAssetId;
      AreaDescription = location.ParentAssetDescription;
      AssetDescription = location.AssetDescription;
      AssetId = location.AssetId;
      CountProduct = location.CountProducts;
      WeightProducts = location.WeightProducts;
      WeightMaxCapacity = location.WeightMaxCapacity;
      WeightFree = WeightMaxCapacity - WeightProducts;
      FillOrderSeq = location.FillOrderSeq;
      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_Location(V_WorkOrdersOnYardLocation location)
    {
      AreaId = location.AreaId;
      AreaDescription = location.AreaDescription;
      AssetDescription = location.AssetDescription;
      AssetId = location.AssetId;
      CountProduct = location.ProductsOnAsset;
      WeightProducts = location.WeightOnAsset;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long? AreaId { get; set; }

    [SmfDisplay(typeof(VM_Location), "AreaDescription", "NAME_Yard")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AreaDescription { get; set; }

    public long AssetId { get; set; }

    [SmfDisplay(typeof(VM_Location), "AssetDescription", "NAME_Location")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetDescription { get; set; }

    [SmfDisplay(typeof(VM_ProductLocationWorkOrder), "CountProduct", "NAME_ProductsCount")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? CountProduct { get; set; }

    [SmfDisplay(typeof(VM_Location), "WeightProducts", "NAME_Weight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double? WeightProducts { get; set; }

    [SmfDisplay(typeof(VM_Location), "WeightMaxCapacity", "NAME_WeightMax")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double? WeightMaxCapacity { get; set; }

    [SmfDisplay(typeof(VM_Location), "WeightMaxCapacity", "NAME_Free")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double? WeightFree { get; set; }


    public string ParentAssetDescription { get; set; }

    public short FillOrderSeq { get; set; }
  }
}
