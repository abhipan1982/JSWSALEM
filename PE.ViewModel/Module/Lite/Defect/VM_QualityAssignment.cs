using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Defect
{
  public class VM_QualityAssignment : VM_Base
  {
    public VM_QualityAssignment() { }

    public VM_QualityAssignment(long id)
    {
      this.Id = id;
    }

    public VM_QualityAssignment(long id, short? quality, List<long> defects)
    {
      this.Id = id;
      Quality = quality;

      if (defects != null)
      {
        foreach (long defect in defects)
        {
          DefectIds.Add(defect);
        }
      }
    }

    public long Id { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_QualityAssignment), "AssetId", "NAME_Asset")]
    public long? AssetId { get; set; }

    [SmfDisplay(typeof(VM_QualityAssignment), "Quality", "NAME_Quality")]
    public short? Quality { get; set; }

    [SmfDisplay(typeof(VM_QualityAssignment), "Remark", "NAME_Remark")]
    public string Remark { get; set; }

    [SmfDisplay(typeof(VM_QualityAssignment), "DefectIds", "NAME_Defects")]
    [SmfRequired]
    public List<long> DefectIds { get; set; }
  }
}
