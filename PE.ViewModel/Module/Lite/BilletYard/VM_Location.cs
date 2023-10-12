using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.BilletYard
{
  public class VM_Location : VM_Base
  {
    public long ParentAssetId { get; set; }

    [SmfDisplay(typeof(VM_Location), "ParentAssetDescription", "NAME_Yard")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ParentAssetDescription { get; set; }

    public long AssetId { get; set; }

    [SmfDisplay(typeof(VM_Location), "AssetDescription", "NAME_Location")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetDescription { get; set; }

    public long? HeatIdInLastGroup { get; set; }

    [SmfDisplay(typeof(VM_Location), "HeatNameInLastGroup", "NAME_LastHeat")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatNameInLastGroup { get; set; }

    [SmfDisplay(typeof(VM_Location), "CountMaterials", "NAME_MaterialsCount")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int CountMaterials { get; set; }

    [SmfDisplay(typeof(VM_Location), "PieceMaxCapacity", "NAME_Capacity")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int PieceMaxCapacity { get; set; }

    [SmfDisplay(typeof(VM_Location), "FreeSpace", "NAME_Free")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int FreeSpace => PieceMaxCapacity - CountMaterials;
  }
}
