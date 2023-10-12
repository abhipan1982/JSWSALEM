using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.ProductYard
{
  public class VM_WorOrdersOnYards : VM_Base
  {
    public VM_WorOrdersOnYards(V_WorkOrdersOnYard data)
    {
      HeatId = data.HeatId;
      HeatName = data.HeatName;
      WorkOrderId = data.WorkOrderId;
      WorkOrderName = data.WorkOrderName;
      SteelgradeId = data.SteelgradeId;
      SteelgradeCode = data.SteelgradeName;
      ProductsNumber = data.ProductsNumber;
      IsOverrun = data.IsOverrun ?? default;
      ToBeCompletedBeforeTs = data.ToBeCompletedBeforeTs;
      ProductsWeight = data.ProductsWeight;
    }

    public VM_WorOrdersOnYards(V_WorkOrdersOnProductYard data)
    {
      YardId = data.AreaId;
      YardName = data.AreaDescription;
      HeatId = data.HeatId;
      HeatName = data.HeatName;
      WorkOrderId = data.WorkOrderId;
      WorkOrderName = data.WorkOrderName;
      SteelgradeId = data.SteelgradeId;
      SteelgradeCode = data.SteelgradeName;
      ProductsNumber = data.ProductsOnArea;
      IsOverrun = data.IsOverrun ?? default;
      ToBeCompletedBeforeTs = data.ToBeCompletedBeforeTs;
      ProductsWeight = data.WeightOnArea;
      HeatWeight = data.HeatWeight;
    }

    public long? HeatId { get; set; }

    [SmfDisplay(typeof(VM_WorOrdersOnYards), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    public long? WorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_WorOrdersOnYards), "HeatName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }


    public long? SteelgradeId { get; set; }

    [SmfDisplay(typeof(VM_WorOrdersOnYards), "SteelgradeCode", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }

    public long? YardId { get; set; }

    [SmfDisplay(typeof(VM_WorOrdersOnYards), "YardName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string YardName { get; set; }

    [SmfDisplay(typeof(VM_WorOrdersOnYards), "ProductsOnYard", "NAME_ProductShort")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? ProductsNumber { get; set; }

    [SmfDisplay(typeof(VM_WorOrdersOnYards), "HeatWeight", "NAME_ProductsOnYard")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? HeatWeight { get; set; }

    [SmfDisplay(typeof(VM_WorOrdersOnYards), "IsOverrun", "NAME_IsOverrun")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsOverrun { get; set; }

    [SmfDisplay(typeof(VM_WorOrdersOnYards), "ToBeCompletedBefore", "NAME_ToBeCompletedBefore")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime ToBeCompletedBeforeTs { get; set; }

    [SmfDisplay(typeof(VM_WorOrdersOnYards), "WeightOnArea", "NAME_ProductsWeight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? ProductsWeight { get; set; }
  }
}
