using System.Runtime.Serialization;
using PE.BaseModels.DataContracts.Internal._Base;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  [DataContract]
  public class DCUpdateArea : IdBase<int>
  {
    private DCUpdateArea(){ }

    public DCUpdateArea(int id)
      :base(id)
    {
    }
  }
}
