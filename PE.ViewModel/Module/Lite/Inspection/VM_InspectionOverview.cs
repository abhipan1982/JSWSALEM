using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Inspection
{
  public class VM_InspectionOverview : VM_Base
  {
    public VM_InspectionOverview(long? rawMaterialId, long? inspectionId = null)
    {
      RawMaterialId = rawMaterialId;
      InspectionId = inspectionId;
    }

    [SmfDisplay(typeof(VM_InspectionOverview), "RawMaterialId", "NAME_MaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? RawMaterialId { get; set; }

    [SmfDisplay(typeof(VM_InspectionOverview), "InspectionId", "NAME_InspectionId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? InspectionId { get; set; }
  }
}
