using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Visualization
{
  public class VM_TrackingMaterialOverview : VM_Base
  {
    #region ctor

    public VM_TrackingMaterialOverview()
    {
      //IsHeatConsistent = string.Equals(HeatQR, HeatName);

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region prop

    [SmfDisplay(typeof(VM_TrackingMaterialOverview), "MaterialName", "NAME_L3MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }

    [SmfDisplay(typeof(VM_TrackingMaterialOverview), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_TrackingMaterialOverview), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_TrackingMaterialOverview), "SteelgradeName", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeName { get; set; }

    [SmfDisplay(typeof(VM_TrackingMaterialOverview), "SteelgradeCode", "NAME_SteelgradeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }

    [SmfDisplay(typeof(VM_TrackingMaterialOverview), "SlittingFactor", "NAME_Slitting")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? SlittingFactor { get; set; }

    public long? MaterialId { get; set; }
    public long? WorkOrderId { get; set; }
    public long? HeatId { get; set; }
    public long? SteelgradeId { get; set; }

    #endregion
  }
}
