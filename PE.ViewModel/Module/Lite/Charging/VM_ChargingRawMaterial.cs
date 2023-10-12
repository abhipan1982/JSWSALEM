using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.Charging
{
  public class VM_ChargingRawMaterial : VM_Base
  {
    public VM_ChargingRawMaterial(TRKRawMaterialLocation data)
    {
      RawMaterialId = data.FKRawMaterialId;
      OrderSeq = data.OrderSeq;
      RawMaterialName = data.FKRawMaterial.RawMaterialName;
      //this.Weight = data.TRKRawMaterial.TRKRawMaterialsSteps?.Weight;
    }

    public long? RawMaterialId { get; set; }
    public string RawMaterialName { get; set; }
    public double? Weight { get; set; }
    public double? Length { get; set; }
    public short OrderSeq { get; set; }
    public string HeatQRID { get; set; }
  }
}
