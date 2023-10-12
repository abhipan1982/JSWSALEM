using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_RawMaterialRejection : VM_Base
  {
    public VM_RawMaterialRejection() { }

    public VM_RawMaterialRejection(long rawMaterialId)
    {
      this.RawMaterialId = rawMaterialId;
    }

    public VM_RawMaterialRejection(TRKRawMaterial data)
    {
      RawMaterialId = data.RawMaterialId;
      EnumRejectLocation = data.EnumRejectLocation;
    }

    public long RawMaterialId { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialRejection), "EnumRejectLocation", "NAME_RejectLocation")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public short EnumRejectLocation { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialRejection), "OutputPieces", "NAME_NumberOfOutputPieces")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public short OutputPieces { get; set; }
  }
}
