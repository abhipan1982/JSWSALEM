using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.BilletYard
{
  public class VM_BilletYardDetails : VM_Base
  {
    public long? BilletYardId { get; set; }

    [SmfDisplay(typeof(VM_BilletYardDetails), "YardName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string YardName { get; set; }

    public List<VM_BilletLocation> Locations { get; set; }

    public int GridRows => Locations?.Max(x => (int?)x.GridRowEnd) ?? 0;

    public int GridColumns => Locations?.Max(x => (int?)x.GridColumnEnd) ?? 0;
  }

  public class VM_BilletLocation
  {
    public long LocationId { get; set; }
    public string Name { get; set; }
    public int NumberOfMaterials { get; set; }
    public int Capacity { get; set; }
    public short LocationX { get; set; }
    public short LocationY { get; set; }
    public short SizeX { get; set; }
    public short SizeY { get; set; }

    public int FreeSpace => Capacity - NumberOfMaterials;

    public int GridColumnStart => LocationX;

    public int GridColumnEnd => LocationX + SizeX;

    public int GridRowStart => LocationY;

    public int GridRowEnd => LocationY + SizeY;

    public int fillPercentage => (int)(NumberOfMaterials / (double)Capacity * 100);
  }
}
