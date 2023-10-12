using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.ScrapGroup
{
  public class VM_ScrapGroup : VM_Base
  {
    public VM_ScrapGroup()
    {
    }

    public VM_ScrapGroup(PRMScrapGroup sg)
    {
      ScrapGroupId = sg.ScrapGroupId;
      ScrapGroupName = sg.ScrapGroupName;
      ScrapGroupDescription = sg.ScrapGroupDescription;
      ScrapGroupCode = sg.ScrapGroupCode;

      UnitConverterHelper.ConvertToLocal(this);
    }

    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfDisplay(typeof(VM_ScrapGroup), "ScrapGroupId", "NAME_ScrapGroupId")]
    public long ScrapGroupId { get; set; }

    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfDisplay(typeof(VM_ScrapGroup), "ScrapGroupName", "NAME_ScrapGroupName")]
    [SmfStringLength(50)]
    [SmfRequired]
    public string ScrapGroupName { get; set; }

    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfDisplay(typeof(VM_ScrapGroup), "ScrapGroupDescription", "NAME_ScrapGroupDescription")]
    [SmfStringLength(200)]
    public string ScrapGroupDescription { get; set; }

    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfDisplay(typeof(VM_ScrapGroup), "ScrapGroupCode", "NAME_ScrapGroupCode")]
    [SmfStringLength(10)]
    [SmfRequired]
    public string ScrapGroupCode { get; set; }
  }
}
