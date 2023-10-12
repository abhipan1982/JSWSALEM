using System.Runtime.Serialization;
using PE.BaseModels.DataContracts.Internal._Base;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  [DataContract]
  public class DCLayer : IdBase<long>
  {
    private DCLayer() { }
    public DCLayer(long id) : base(id)
    {

    }

    [DataMember] public int AreaCode { get; set; }
  }
}
