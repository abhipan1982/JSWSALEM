using System.Threading.Tasks;
using PE.Interfaces.Managers.DBA;
using PE.BaseInterfaces.Managers.DBA;
using PE.BaseModels.DataContracts.External.DBA;
using PE.BaseModels.DataContracts.Internal.DBA;
using SMF.Core.DC;
using PE.Models.DataContracts.Internal.DBA;
using PE.DBA.Base.Module.Communication;
using PE.DBA.DataBaseAdapter.Managers;
using PE.Models.DataContracts.External.DBA;

namespace PE.DBA.DataBaseAdapter.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    protected readonly IL3DBCommunicationManager _L3DbCommunicationManager;
    //protected readonly IL3DBCommunicationBaseManager _L3DbCommunicationBaseManager;

    public ExternalAdapterHandler(IL3DBCommunicationBaseManager l3DbCommunicationBaseManager, IL3DBCommunicationManager l3DbCommunicationManager) :base(l3DbCommunicationBaseManager)
    {
       _L3DbCommunicationManager = l3DbCommunicationManager;
    }

    //public ExternalAdapterHandler(IL3DBCommunicationManager l3DbCommunicationManager)
    //{
    //  _L3DbCommunicationManager = l3DbCommunicationManager;
    //}

    public virtual Task<DCL3L2BatchDataDefinition> CreateBatchDataAsync(DCL3L2BatchDataDefinition batchData)
    {
      DCL3L2BatchDataDefinitionExt internalDc = new DCL3L2BatchDataDefinitionExt();
      internalDc.ToExternal(batchData);

      return _L3DbCommunicationManager.CreateBatchDataAsync(internalDc);
    }

    public virtual Task<DCL3L2BatchDataDefinition> UpdateBatchDataAsync(DCL3L2BatchDataDefinition batchData)
    {
      DCL3L2BatchDataDefinitionExt externalDc = new DCL3L2BatchDataDefinitionExt();
      externalDc.ToExternal(batchData);

      return _L3DbCommunicationManager.UpdateBatchDataDefinitionAsync(externalDc);
    }

    //Av@

    public virtual Task<DataContractBase> CreateWorkOrderDefinitionAsyncEXT(DCL3L2WorkOrderDefinitionMOD workOrderDefinition)
    {
      DCL3L2WorkOrderDefinitionExtMOD externalDc = new DCL3L2WorkOrderDefinitionExtMOD();
      externalDc.ToExternal(workOrderDefinition);

      return _L3DbCommunicationManager.CreateWorkOrderDefinitionAsyncEXT(externalDc);
    }

    public virtual Task<DataContractBase> UpdateWorkOrderDefinitionAsyncEXT(DCL3L2WorkOrderDefinitionMOD workOrderDefinition)
    {
      DCL3L2WorkOrderDefinitionExtMOD externalDc = new DCL3L2WorkOrderDefinitionExtMOD();
      externalDc.ToExternal(workOrderDefinition);

      return _L3DbCommunicationManager.CreateWorkOrderDefinitionAsyncEXT(externalDc);
    }


    public virtual Task<DataContractBase> DeleteWorkOrderDefinitionAsyncEXT(DCL3L2WorkOrderDefinitionMOD workOrderDefinition)
    {
      DCL3L2WorkOrderDefinitionExtMOD externalDc = new DCL3L2WorkOrderDefinitionExtMOD();
      externalDc.ToExternal(workOrderDefinition);

      return _L3DbCommunicationManager.DeleteWorkOrderDefinitionAsyncEXT(externalDc);
    }

    //public virtual Task<DataContractBase> DeleteWorkOrderDefinitionAsync(DCL3L2BatchData workOrderDefinition)
    //{
    //  DCL3L2WorkOrderDefinitionExt externalDc = new DCL3L2WorkOrderDefinitionExt();
    //  externalDc.ToExternal(workOrderDefinition);

    //  return L3DbCommunicationManager.DeleteWorkOrderDefinitionAsync(externalDc);
    //}

    //public virtual Task<DataContractBase> ResetWorkOrderReportAsync(DCL2L3WorkOrderReport dc)
    //{
    //  DCL2L3WorkOrderReportExt externalDc = new DCL2L3WorkOrderReportExt();
    //  externalDc.ToExternal(dc);

    //  return L3DbCommunicationManager.ResetWorkOrderReportAsync(externalDc);
    //}

    //public virtual Task<DataContractBase> ResetProductReportAsync(DCL2L3ProductReport dc)
    //{
    //  DCL2L3ProductReportExt externalDc = new DCL2L3ProductReportExt();
    //  externalDc.ToExternal(dc);

    //  return L3DbCommunicationManager.ResetProductReportAsync(externalDc);
    //}

    //public virtual Task<DataContractBase> CreateWorkOrderReport(DCL2L3WorkOrderReport dc)
    //{
    //  return L3DbCommunicationManager.CreateWorkOrderReport(dc);
    //}

    //public virtual Task<DataContractBase> CreateProductReport(DCL2L3ProductReport dc)
    //{
    //  return L3DbCommunicationManager.CreateProductReport(dc);
    //}
  }
}
