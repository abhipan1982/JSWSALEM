using System.Runtime.Serialization;
using PE.BaseModels.DataContracts.Internal._Base;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  [DataContract]
  public class DCHardRemoveMaterial : IdBase<long>
  {
    private DCHardRemoveMaterial() { }
    public DCHardRemoveMaterial(long id) : base(id)
    {

    }
  }
}
