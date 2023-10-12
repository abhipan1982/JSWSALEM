using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.ProductYard
{
  public class VM_ProductYardDetails : VM_Base
  {
    public long? ProductYardId { get; set; }

    [SmfDisplay(typeof(VM_ProductYardDetails), "YardName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string YardName { get; set; }

    public List<VM_ProductLocation> Locations { get; set; }

    public int GridRows => Locations?.Max(x => (int?)x.GridRowEnd) ?? 0;

    public int GridColumns => Locations?.Max(x => (int?)x.GridColumnEnd) ?? 0;
  }

  public class VM_ProductLocation
  {
    public long LocationId { get; set; }
    public string Name { get; set; }
    public int NumberOfProducts { get; set; }
    public double WeightOfProducts { get; set; }
    public int Capacity { get; set; }
    public short LocationX { get; set; }
    public short LocationY { get; set; }
    public short SizeX { get; set; }
    public short SizeY { get; set; }

    public double FreeSpace => Capacity - WeightOfProducts;

    public int GridColumnStart => LocationX;

    public int GridColumnEnd => LocationX + SizeX;

    public int GridRowStart => LocationY;

    public int GridRowEnd => LocationY + SizeY;

    public int fillPercentage => (int)(WeightOfProducts / Capacity * 100);
  }
}
