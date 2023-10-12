using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Heat
{
  public class VM_HeatSummary : VM_Base
  {
    public VM_HeatSummary() { }

    public VM_HeatSummary(V_Heat data)
    {
      HeatId = data.HeatId;
      //TODOMN - exclude this
      //CreatedTs = data.CreatedTs;
      //LastUpdateTs = data.LastUpdateTs;
      HeatName = data.HeatName;
      SteelgradeCode = data.SteelgradeCode;
      SteelgradeName = data.SteelgradeName;
      SteelGradeDensity = data.Density;
      HeatSupplierName = data.HeatSupplierName;

      UnitConverterHelper.ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_HeatSummary), "HeatId", "NAME_HeatId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long HeatId { get; set; }

    [SmfDisplay(typeof(VM_HeatSummary), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_HeatSummary), "LastUpdatedTs", "NAME_LastUpdateTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplay(typeof(VM_HeatSummary), "HeatName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_HeatSummary), "SupplierName", "NAME_Supplier")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatSupplierName { get; set; }

    [SmfDisplay(typeof(VM_HeatSummary), "IsDummy", "NAME_IsDummy")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsDummy { get; set; }

    [SmfDisplay(typeof(VM_HeatSummary), "SteelgradeCode", "NAME_SteelgradeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }

    [SmfDisplay(typeof(VM_HeatSummary), "SteelgradeName", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeName { get; set; }

    [SmfDisplay(typeof(VM_HeatSummary), "SteelGradeDensity", "NAME_SteelDensity")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? SteelGradeDensity { get; set; }

    [SmfDisplay(typeof(VM_HeatSummary), "Weigth", "NAME_Weigth")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double Weigth { get; set; }

    [SmfDisplay(typeof(VM_HeatSummary), "HeatWeight", "NAME_HeatWeightRef")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? HeatWeight { get; set; }
  }
}
