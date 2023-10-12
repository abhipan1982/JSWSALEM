using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseModels.DataContracts.Internal._Base;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  [DataContract]
  public class DCProductUndo : IdBase<long>
  {
    private DCProductUndo() { }
    public DCProductUndo(long id) : base(id)
    {

    }
  }
}
