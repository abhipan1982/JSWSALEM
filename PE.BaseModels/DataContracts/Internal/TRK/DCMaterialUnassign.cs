using System.Runtime.Serialization;
using PE.BaseModels.DataContracts.Internal._Base;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  [DataContract]
  public class DCMaterialUnassign : IdBase<long>
  {
    private DCMaterialUnassign() { }
    public DCMaterialUnassign(long id) : base(id)
    {

    }
  }
}
