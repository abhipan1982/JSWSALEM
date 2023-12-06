using System.Threading.Tasks;
using SMF.Core.DC;
using PE.Interfaces.Modules;
using PE.Models.DataContracts.Internal.DBA;
using PE.DBA.Base.Module.Communication;
using SMF.Module.Core;
using PE.BaseInterfaces.Modules;

namespace PE.DBA.DataBaseAdapter.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<IDBAdapter>, IDBAdapter
  {
    protected readonly ExternalAdapterHandler _handler;

    #region ctor

    public ExternalAdapter(ExternalAdapterHandler handler) : base(handler)
    {
      _handler = handler;
    }

    #endregion
    //Added by Abhishek 12/10/2023 also add 07122023
    public virtual Task<DCL3L2BatchDataDefinition> CreateBatchDataAsync(DCL3L2BatchDataDefinition dcBatchData)
    {
      return HandleIncommingMethod(_handler.CreateBatchDataAsync, dcBatchData);
    }

    public virtual Task<DCL3L2BatchDataDefinition> UpdateBatchDataAsync(DCL3L2BatchDataDefinition dcBatchData)
    {
      return HandleIncommingMethod(_handler.UpdateBatchDataAsync, dcBatchData);
    }

    //Av@
    public virtual Task<DataContractBase> CreateWorkOrderDefinitionAsyncEXT(DCL3L2WorkOrderDefinitionMOD dcWorkOrderDefinition)
    {
      return HandleIncommingMethod(_handler.CreateWorkOrderDefinitionAsyncEXT, dcWorkOrderDefinition);
    }

    public virtual Task<DataContractBase> UpdateWorkOrderDefinitionAsyncEXT(DCL3L2WorkOrderDefinitionMOD dcWorkOrderDefinition)
    {
      return HandleIncommingMethod(_handler.UpdateWorkOrderDefinitionAsyncEXT, dcWorkOrderDefinition);
    }

    public virtual Task<DataContractBase> DeleteWorkOrderDefinitionAsyncEXT(DCL3L2WorkOrderDefinitionMOD dcWorkOrderDefinition)
    {
      return HandleIncommingMethod(_handler.DeleteWorkOrderDefinitionAsyncEXT, dcWorkOrderDefinition);
    }

    //public virtual Task<DataContractBase> DeleteWorkOrderDefinitionAsync(DCL3L2BatchData dcBatchData)
    //{
    //  return HandleIncommingMethod(_handler.DeleteWorkOrderDefinitionAsync, dcBatchData);
    //}

    //public virtual Task<DataContractBase> ResetWorkOrderReportAsync(DCL3L2BatchData dc)
    //{
    //  return HandleIncommingMethod(_handler.ResetWorkOrderReportAsync, dc);
    //}

    //public virtual Task<DataContractBase> ResetProductReportAsync(DCL3L2BatchData dc)
    //{
    //  return HandleIncommingMethod(_handler.ResetProductReportAsync, dc);
    //}

    //public virtual Task<DataContractBase> CreateWorkOrderReport(DCL3L2BatchData dc)
    //{
    //  return HandleIncommingMethod(_handler.CreateWorkOrderReport, dc);
    //}

    //public virtual Task<DataContractBase> CreateProductReport(DCL3L2BatchData dc)
    //{
    //  return HandleIncommingMethod(_handler.CreateProductReport, dc);
    //}    
  }
}
