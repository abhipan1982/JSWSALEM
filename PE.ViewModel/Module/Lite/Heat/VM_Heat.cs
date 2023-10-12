using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Heat
{
  public class VM_Heat : VM_Base
  {
    public VM_Heat() { }

    public VM_Heat(PRMHeat heat)
    {
      HeatId = heat.HeatId;
      HeatName = heat.HeatName;
      FKSteelgradeId = heat.FKSteelgradeId;
      FKHeatSupplierId = heat.FKHeatSupplierId;
      HeatWeight = heat.HeatWeight;
      IsDummy = heat.IsDummy;
      SteelgradeCode = heat.FKSteelgrade?.SteelgradeCode;

      UnitConverterHelper.ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_Heat), "HeatId", "NAME_HeatId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long HeatId { get; set; }

    [SmfDisplay(typeof(VM_Heat), "HeatName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    [SmfStringLength(50)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_Heat), "FKSteelgradeId", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public long? FKSteelgradeId { get; set; }

    [SmfDisplay(typeof(VM_Heat), "FKHeatSupplierId", "NAME_Supplier")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKHeatSupplierId { get; set; }

    [SmfDisplay(typeof(VM_Heat), "HeatWeight", "NAME_HeatWeight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? HeatWeight { get; set; }

    [SmfDisplay(typeof(VM_Heat), "IsDummy", "NAME_IsDummy")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsDummy { get; set; }

    [SmfDisplay(typeof(VM_HeatOverview), "SteelgradeCode", "NAME_SteelgradeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }

    [SmfDisplay(typeof(VM_HeatOverview), "MaterialsNumber", "NAME_MaterialsNumber")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    public int MaterialsNumber { get; set; }

    [SmfDisplay(typeof(VM_HeatOverview), "MaterialCatalogueId", "NAME_MaterialCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long MaterialCatalogueId { get; set; }
  }
}
