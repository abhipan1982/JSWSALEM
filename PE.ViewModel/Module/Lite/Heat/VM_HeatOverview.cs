using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Heat
{
  public class VM_HeatOverview : VM_Base
  {
    public VM_HeatOverview() { }

    public VM_HeatOverview(PRMHeat heat)
    {
      HeatId = heat.HeatId;
      //TODOMN exclude this
      //CreatedTs = heat.CreatedTs;
      //LastUpdateTs = heat.LastUpdateTs;
      HeatName = heat.HeatName;
      SupplierName = heat.FKHeatSupplier?.HeatSupplierName;
      IsDummy = heat.IsDummy;
      SupplierDescription = heat.FKHeatSupplier?.HeatSupplierDescription;
      SteelgradeCode = heat.FKSteelgrade?.SteelgradeCode;
      SteelGradeDensity = heat.FKSteelgrade?.Density;
      HeatWeight = heat.HeatWeight;

      if (heat.FKSteelgrade != null)
      {
        SC = new VM_Steelgrade(heat.FKSteelgrade);
      }

      if (heat.PRMHeatChemicalAnalyses != null && heat.PRMHeatChemicalAnalyses.Count() > 0)
      {
        HC = new VM_HeatChemAnalysis(heat.PRMHeatChemicalAnalyses.FirstOrDefault());
      }

      UnitConverterHelper.ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_HeatOverview), "HeatId", "NAME_HeatId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long HeatId { get; set; }

    [SmfDisplay(typeof(VM_HeatOverview), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_HeatOverview), "LastUpdatedTs", "NAME_LastUpdateTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplay(typeof(VM_HeatOverview), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_HeatOverview), "SupplierName", "NAME_SupplierName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SupplierName { get; set; }

    [SmfDisplay(typeof(VM_HeatOverview), "IsDummy", "NAME_IsDummy")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsDummy { get; set; }

    [SmfDisplay(typeof(VM_HeatOverview), "Description", "NAME_SupplierDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SupplierDescription { get; set; }

    [SmfDisplay(typeof(VM_HeatOverview), "SteelgradeCode", "NAME_SteelgradeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }

    [SmfDisplay(typeof(VM_HeatOverview), "SteelGradeDensity", "NAME_SteelDensity")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? SteelGradeDensity { get; set; }

    [SmfDisplay(typeof(VM_HeatOverview), "Weigth", "NAME_Weigth")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double Weigth { get; set; }

    [SmfDisplay(typeof(VM_HeatOverview), "HeatWeight", "NAME_HeatWeightRef")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? HeatWeight { get; set; }

    public VM_Steelgrade SC { get; set; }

    public VM_HeatChemAnalysis HC { get; set; }
  }
}
