using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.TrackingManagement
{
  public class VM_ReplaceMaterialPosition : VM_Base
  {
    #region ctor

    #endregion

    #region prop

    public int DragAssetCode { get; set; }

    public int DragOrderSeq { get; set; }

    [SmfDisplay(typeof(VM_ReplaceMaterialPosition), "DropAssetCode", "NAME_DropAssetCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int DropAssetCode { get; set; }

    [SmfDisplay(typeof(VM_ReplaceMaterialPosition), "DropOrderSeq", "NAME_DropOrderSeq")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int DropOrderSeq { get; set; }

    [SmfDisplay(typeof(VM_ReplaceMaterialPosition), "RawMaterialId", "NAME_RawMaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long RawMaterialId { get; set; }

    #endregion
  }
}
