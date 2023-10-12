using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Products
{
  public class VM_Bundle : VM_Base
  {
    #region ctor

    public VM_Bundle() { }

    #endregion

    #region props

    public long WorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_Bundle), "Weight", "NAME_Weight")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    [SmfRequired]
    public double Weight { get; set; }

    [SmfDisplay(typeof(VM_Bundle), "KeepInTracking", "NAME_KeepInTracking")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool KeepInTracking { get; set; }

    [SmfDisplay(typeof(VM_Bundle), "BarsNumber", "NAME_BarsNumber")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public short BarsNumber { get; set; }

    #endregion
  }
}
