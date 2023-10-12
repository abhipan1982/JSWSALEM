using System;
using System.Threading.Tasks;
using PE.BaseInterfaces.Modules;
using PE.BaseModels.DataContracts.Internal.DBA;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.DBA.Base.Module.Communication
{
  public abstract class ModuleBaseExternalAdapter<T> : ExternalAdapterBase<T>, IDBAdapterBase where T : class, IDBAdapterBase
  {
    protected readonly ModuleBaseExternalAdapterHandler Handler;

    #region ctor

    public ModuleBaseExternalAdapter(ModuleBaseExternalAdapterHandler handler) : base()
    {
      Handler = handler;
    }

    #endregion

    public virtual Task<DataContractBase> CreateWorkOrderDefinitionAsync(DCL3L2WorkOrderDefinition dcWorkOrderDefinition)
    {
      return HandleIncommingMethod(Handler.CreateWorkOrderDefinitionAsync, dcWorkOrderDefinition);
    }

    public virtual Task<DataContractBase> UpdateWorkOrderDefinitionAsync(DCL3L2WorkOrderDefinition dcWorkOrderDefinition)
    {
      return HandleIncommingMethod(Handler.UpdateWorkOrderDefinitionAsync, dcWorkOrderDefinition);
    }

    public virtual Task<DataContractBase> DeleteWorkOrderDefinitionAsync(DCL3L2WorkOrderDefinition dcWorkOrderDefinition)
    {
      return HandleIncommingMethod(Handler.DeleteWorkOrderDefinitionAsync, dcWorkOrderDefinition);
    }

    public virtual Task<DataContractBase> ResetWorkOrderReportAsync(DCL2L3WorkOrderReport dc)
    {
      return HandleIncommingMethod(Handler.ResetWorkOrderReportAsync, dc);
    }

    public virtual Task<DataContractBase> ResetProductReportAsync(DCL2L3ProductReport dc)
    {
      return HandleIncommingMethod(Handler.ResetProductReportAsync, dc);
    }

    public virtual Task<DataContractBase> CreateWorkOrderReport(DCL2L3WorkOrderReport dc)
    {
      return HandleIncommingMethod(Handler.CreateWorkOrderReport, dc);
    }

    public virtual Task<DataContractBase> CreateProductReport(DCL2L3ProductReport dc)
    {
      return HandleIncommingMethod(Handler.CreateProductReport, dc);
    }
  }
}
