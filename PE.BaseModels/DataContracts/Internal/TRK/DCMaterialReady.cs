using System.Runtime.Serialization;
using PE.BaseModels.DataContracts.Internal._Base;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  [DataContract]
  public class DCMaterialReady : IdBase<long>
  {
    private DCMaterialReady() { }
    public DCMaterialReady(long id) : base(id) { }

    [DataMember] public long ProductType { get; set; }

    [DataMember] public short ChildsNo { get; set; }

    [DataMember] public bool KeepInTracking { get; set; }

    [DataMember] public int TargetAreaAssetCode { get; set; }
  }
}
