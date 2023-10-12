using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Defect
{
  public class VM_DefectGroupsCatalogue : VM_Base
  {
    public VM_DefectGroupsCatalogue()
    {
    }

    public VM_DefectGroupsCatalogue(QTYDefectCategoryGroup dLS)
    {
      DefectGroupId = dLS.DefectCategoryGroupId;
      DefectGroupCode = dLS.DefectCategoryGroupCode;
      DefectGroupName = dLS.DefectCategoryGroupName;
      UnitConverterHelper.ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_DefectGroupsCatalogue), "DefectGroupId", "NAME_DefectGroupId")]
    public long DefectGroupId { get; set; }

    [SmfDisplay(typeof(VM_DefectGroupsCatalogue), "DefectGroupName", "NAME_DefectGroupName")]
    public string DefectGroupName { get; set; }

    [SmfDisplay(typeof(VM_DefectGroupsCatalogue), "DefectGroupCode", "NAME_DefectGroupCode")]
    public string DefectGroupCode { get; set; }
  }
}
