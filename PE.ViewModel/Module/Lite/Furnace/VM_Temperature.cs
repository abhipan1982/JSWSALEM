using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Furnace
{
  public class VM_Temperature : VM_Base
  {
    public VM_Temperature() { }

    public VM_Temperature(double? temperature)
    {
      Temperature = temperature;
      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_Temperature(V_RawMaterialInFurnace data)
    {
      Temperature = data.Temperature;
      OrderSeq = (short)(data.OrderSeq ?? 0);
      UnitConverterHelper.ConvertToLocal(this);
    }

    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Temperature")]
    public double? Temperature { get; set; }

    public short OrderSeq { get; set; }
  }
}
