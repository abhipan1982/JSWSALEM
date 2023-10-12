using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Steelgrade
{
  public class VM_SteelGroup : VM_Base
  {
    public VM_SteelGroup()
    {
    }

    public VM_SteelGroup(PRMSteelGroup sg)
    {
      Id = sg.SteelGroupId;
      Name = sg.SteelGroupName;
      Description = sg.SteelGroupDescription;
      Code = sg.SteelGroupCode;
      IsDefault = sg.IsDefault;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long Id { get; set; }

    [SmfDisplay(typeof(VM_SteelGroup), "KeyName", "NAME_Name")]
    [SmfRequired]
    public string Name { get; set; }

    [SmfDisplay(typeof(VM_SteelGroup), "KeyDescription", "NAME_Description")]
    public string Description { get; set; }

    [SmfDisplay(typeof(VM_SteelGroup), "KeyCode", "NAME_Code")]
    [SmfRequired]
    public string Code { get; set; }

    [SmfDisplay(typeof(VM_SteelGroup), "KeySymbol", "NAME_Symbol")]
    public string Symbol { get; set; }

    [SmfDisplay(typeof(VM_SteelGroup), "KeyName", "NAME_IsDefault")]
    public bool IsDefault { get; set; }
  }
}
