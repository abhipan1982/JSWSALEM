using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.TrackingManagement
{
  public class VM_MoveMaterials : VM_Base
  {
    #region ctor

    #endregion

    #region prop

    [SmfDisplay(typeof(VM_MoveMaterials), "AssetCode", "NAME_AssetCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int AssetCode { get; set; }

    #endregion
  }
}
