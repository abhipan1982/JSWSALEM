using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_RawMaterialEXT : VM_Base
  {
    public VM_RawMaterialEXT() { }

    public VM_RawMaterialEXT(TRKRawMaterial ramMat)
    {
      FKRawMaterialId = ramMat.RawMaterialId;

      UnitConverterHelper.ConvertToLocal(this);
    }

    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long FKRawMaterialId { get; set; }

    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }
  }
}
