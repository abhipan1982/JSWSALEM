using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.ProductYard
{
  public class VM_ProductOnYard : VM_Base
  {
    public VM_ProductOnYard() { }

    public VM_ProductOnYard(V_ProductsOnYard data)
    {
      ProductId = data.ProductId;
      ProductName = data.ProductName;
      SteelgradeId = data.SteelgradeId;
      SteelgradeCode = data.SteelgradeCode;
      SteelgradeName = data.SteelgradeName;
      WorkOrderId = data.WorkOrderId;
      WorkOrderName = data.WorkOrderName;
      AssetId = data.AssetId;
      AssetName = data.AssetName;
      AreaId = data.AreaId;
      AreaName = data.AreaName;
      AreaDescription = data.AreaDescription;
      HeatId = data.HeatId;
      HeatName = data.HeatName;
      HeatWeight = data.HeatWeight;
      ProductWeight = data.ProductWeight;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long ProductId { get; set; }


    [SmfDisplay(typeof(VM_ProductOnYard), "ProductName", "NAME_Product")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductName { get; set; }

    public long? SteelgradeId { get; set; }

    [SmfDisplay(typeof(VM_ProductOnYard), "SteelgradeCode", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }

    [SmfDisplay(typeof(VM_ProductOnYard), "SteelgradeName", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeName { get; set; }

    public long? WorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_ProductOnYard), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }


    public long AssetId { get; set; }

    [SmfDisplay(typeof(VM_ProductOnYard), "AssetName", "NAME_Location")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }

    public long? AreaId { get; set; }

    [SmfDisplay(typeof(VM_ProductOnYard), "AreaName", "NAME_Yard")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AreaName { get; set; }

    [SmfDisplay(typeof(VM_ProductOnYard), "AreaDescription", "NAME_Yard")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AreaDescription { get; set; }

    public long? HeatId { get; set; }

    [SmfDisplay(typeof(VM_ProductOnYard), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    public double? HeatWeight { get; set; }

    [SmfDisplay(typeof(VM_ProductOnYard), "Weight", "NAME_Weight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double ProductWeight { get; set; }
  }
}
