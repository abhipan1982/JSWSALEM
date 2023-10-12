using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Furnace
{
  public class VM_FurnaceTemperatures
  {
    public int Position1 { get; set; }
    public int Position2 { get; set; }
    public int Position3 { get; set; }
    public int Position4 { get; set; }

    [SmfUnit("UNIT_Temperature")]
    [SmfFormat("FORMAT_Temperature", NullDisplayText = "-")]
    public double? Temperature1 { get; set; }

    [SmfUnit("UNIT_Temperature")]
    [SmfFormat("FORMAT_Temperature", NullDisplayText = "-")]
    public double? Temperature2 { get; set; }

    [SmfUnit("UNIT_Temperature")]
    [SmfFormat("FORMAT_Temperature", NullDisplayText = "-")]
    public double? Temperature3 { get; set; }

    [SmfUnit("UNIT_Temperature")]
    [SmfFormat("FORMAT_Temperature", NullDisplayText = "-")]
    public double? Temperature4 { get; set; }
  }
}
