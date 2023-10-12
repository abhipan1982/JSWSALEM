using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.BilletYard
{
  public class VM_BilletLocationDetails : VM_Base
  {
    public long LocationId { get; set; }
    public string Name { get; set; }
    public List<VM_BilletLocationMaterial> Materials { get; set; }

    [SmfDisplay(typeof(VM_BilletLocationDetails), "FillDirection", "NAME_FillDirection")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string FillDirection { get; set; }

    public int SizeX { get; set; }
    public int SizeY { get; set; }

    [SmfDisplay(typeof(VM_BilletLocationDetails), "MaterialsNumber", "LABEL_Materials")]
    public int MaterialsNumber { get; set; }

    [SmfDisplay(typeof(VM_BilletLocationDetails), "Capacity", "NAME_Capacity")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int Capacity { get; set; }
  }

  public class VM_BilletLocationMaterial : VM_Base
  {
    public long MaterialId { get; set; }
    public long HeatId { get; set; }
    public string MaterialName { get; set; }
    public string HeatName { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public short GroupNo { get; set; }
    public long LocationId { get; set; }
  }
}
