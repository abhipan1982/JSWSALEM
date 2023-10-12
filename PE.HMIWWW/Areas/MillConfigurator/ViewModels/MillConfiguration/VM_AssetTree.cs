using System.Linq;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.Areas.MillConfigurator.ViewModels.MillConfiguration
{
  public class VM_AssetTree : VM_Base
  {
    public VM_AssetTree() { }

    public VM_AssetTree(V_Asset data)
    {
      AssetId = data.AssetId;
      AssetName = data.AssetName;
      ParentAssetId = data.ParentAssetId;
      Level = (short)data.Path.Count(x => x == '/');
      OrderSeq = data.OrderSeq;
    }

    public long AssetId { get; set; }
    public string AssetName { get; set; }
    public long? ParentAssetId { get; set; }
    public short Level { get; set; }
    public long OrderSeq { get; set; }
  }
}
