using System.Runtime.Serialization;
using PE.BaseModels.DataContracts.Internal._Base;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  [DataContract]
  public class DCRawMaterial : IdBase<long>
  {
    private DCRawMaterial() { }
    public DCRawMaterial(long id) : base(id)
    {

    }
    [DataMember] public int AssetCode { get; set; }
  }
}
