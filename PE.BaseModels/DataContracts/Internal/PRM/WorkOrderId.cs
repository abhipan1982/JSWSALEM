using PE.BaseModels.DataContracts.Internal._Base;

namespace PE.BaseModels.DataContracts.Internal.PRM
{
  public class WorkOrderId : IdBase<long>
  {
    public WorkOrderId() { }

    public WorkOrderId(long id) 
      : base(id)
    {
    }
  }
}
