using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollsManagement
{
  public class VM_RollTypeDiameterLimit : VM_Base
  {
    public long RollTypeId { get; set; }

    [SmfUnit("UNIT_SmallDiameter")]
    public double Min { get; set; }

    [SmfUnit("UNIT_SmallDiameter")]
    public double Max { get; set; }

    public VM_RollTypeDiameterLimit(long rollTypeId, double min, double max)
    {
      RollTypeId = rollTypeId;
      Min = min;
      Max = max;

      UnitConverterHelper.ConvertToLocal(this);
    }
  }
}
