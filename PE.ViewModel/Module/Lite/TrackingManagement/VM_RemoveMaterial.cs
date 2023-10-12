using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.TrackingManagement
{
  public class VM_RemoveMaterial : VM_Base
  {
    #region ctor

    #endregion

    #region prop

    [SmfDisplay(typeof(VM_RemoveMaterial), "RawMaterialId", "NAME_RawMaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long RawMaterialId { get; set; }

    [SmfDisplay(typeof(VM_RemoveMaterial), "AreaCode", "NAME_AreaCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int AreaCode { get; set; }

    public bool HardRemove { get; set; }

    #endregion
  }
}
