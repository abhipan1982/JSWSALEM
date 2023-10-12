using System.Runtime.Serialization;
using PE.BaseModels.DataContracts.Internal._Base;

namespace PE.BaseModels.DataContracts.Internal.PRM
{
  [DataContract]
  public class DCWorkOrderConfirmation : IdBase<long>
  {
    private DCWorkOrderConfirmation() { }
    public DCWorkOrderConfirmation(long id):base(id) { }

    public bool IsEndOfWorkShop { get; set; }
  }
}
