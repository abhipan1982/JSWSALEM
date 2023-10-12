using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.ProductYard
{
  public class VM_ProductLocationDetails : VM_Base
  {
    public long LocationId { get; set; }
    public string Name { get; set; }
    public List<VM_ProductLocationWorkOrder> WorkOrders { get; set; }

    [SmfDisplay(typeof(VM_ProductLocationDetails), "MaterialsNumber", "LABEL_Materials")]
    public int MaterialsNumber { get; set; }

    public int Capacity { get; set; }
  }

  public class VM_ProductLocationWorkOrder : VM_Base
  {
    public VM_ProductLocationWorkOrder() { }

    public VM_ProductLocationWorkOrder(V_WorkOrdersOnYardLocation data)
    {
      HeatId = data.HeatId;
      HeatName = data.HeatName;
      LocationId = data.AssetId;
      LocationName = data.AssetDescription;
      WorkOrderId = data.WorkOrderId;
      WorkOrderName = data.WorkOrderName;
      SteelgradeId = data.SteelgradeId;
      SteelgradeName = data.SteelgradeName;
      ProductCount = data.ProductsOnAsset;
      ProductWeight = data.WeightOnAsset;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long? WorkOrderId { get; set; }
    public long? HeatId { get; set; }
    public long? SteelgradeId { get; set; }
    public long LocationId { get; set; }

    [SmfDisplay(typeof(VM_ProductLocationWorkOrder), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_ProductLocationWorkOrder), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_ProductLocationWorkOrder), "SteelgradeName", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeName { get; set; }

    [SmfDisplay(typeof(VM_ProductLocationWorkOrder), "AssetDescription", "NAME_Location")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string LocationName { get; set; }

    [SmfDisplay(typeof(VM_ProductLocationWorkOrder), "ProductCount", "NAME_ProductsCount")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? ProductCount { get; set; }

    [SmfDisplay(typeof(VM_ProductLocationWorkOrder), "ProductWeight", "NAME_Weight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? ProductWeight { get; set; }
  }
}
