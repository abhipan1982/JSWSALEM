using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.SteelFamily
{
  public class VM_SteelFamily : VM_Base
  {
    //public VM_SteelFamily ParentSteelGroup { get; set; }

    public VM_SteelFamily()
    {
    }

    public VM_SteelFamily(PRMSteelGroup sg)
    {
      Id = sg.SteelGroupId;
      SteelGroupName = sg.SteelGroupName;
      Description = sg.SteelGroupDescription;
      SteelGroupCode = sg.SteelGroupCode;
      IsDefault = sg.IsDefault;
      //ParentSteelGroup = new VM_SteelFamily(sg.PRMSteelGroup1);
      WearCoefficient = sg.WearCoefficient;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long Id { get; set; }

    [SmfDisplay(typeof(VM_SteelFamily), "KeyName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(50)]
    [SmfRequired]
    public string SteelGroupName { get; set; }

    [SmfDisplay(typeof(VM_SteelFamily), "KeyDescription", "NAME_Description")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(200)]
    public string Description { get; set; }

    [SmfDisplay(typeof(VM_SteelFamily), "KeyCode", "NAME_Code")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(10)]
    [SmfRequired]
    public string SteelGroupCode { get; set; }

    [SmfDisplay(typeof(VM_SteelFamily), "KeyName", "NAME_IsDefault")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsDefault { get; set; }

    [SmfDisplay(typeof(VM_SteelFamily), "WearCoefficient", "NAME_WearCoefficient")]
    [SmfFormat("FORMAT_Plain2", NullDisplayText = "-", HtmlEncode = false)]
    public double WearCoefficient { get; set; }
  }
}
