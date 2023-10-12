using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.Measurements
{
  public class VM_RawMaterialInArea : VM_Base
  {
    public VM_RawMaterialInArea()
    {
    }

    public VM_RawMaterialInArea(long rawmMaterialId, MVHAsset area)
    {
      RawmMaterialId = rawmMaterialId;
      AreaDescription = area.AssetDescription;
      AreaCode = area.AssetCode;
      AreaName = area.AssetName;
    }

    public long RawmMaterialId { get; set; }
    public string AreaDescription { get; set; }
    public int AreaCode { get; set; }
    public string AreaName { get; set; }
  }
}
