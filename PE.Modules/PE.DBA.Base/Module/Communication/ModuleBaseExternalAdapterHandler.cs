using System.Threading.Tasks;
using PE.BaseInterfaces.Managers.DBA;
using PE.BaseModels.DataContracts.External.DBA;
using PE.BaseModels.DataContracts.Internal.DBA;
using SMF.Core.DC;

namespace PE.DBA.Base.Module.Communication
{
  public class ModuleBaseExternalAdapterHandler
  {
    protected readonly IL3DBCommunicationBaseManager L3DbCommunicationManager;


    public ModuleBaseExternalAdapterHandler(IL3DBCommunicationBaseManager l3DbCommunicationManager)
    {
      L3DbCommunicationManager = l3DbCommunicationManager;
    }

    public virtual Task<DataContractBase> CreateWorkOrderDefinitionAsync(DCL3L2WorkOrderDefinition workOrderDefinition)
    {
      DCL3L2WorkOrderDefinitionExt externalDc = new DCL3L2WorkOrderDefinitionExt();
      externalDc.ToExternal(workOrderDefinition);

      return L3DbCommunicationManager.CreateWorkOrderDefinitionAsync(externalDc);
    }

    public virtual Task<DataContractBase> UpdateWorkOrderDefinitionAsync(DCL3L2WorkOrderDefinition workOrderDefinition)
    {
      DCL3L2WorkOrderDefinitionExt externalDc = new DCL3L2WorkOrderDefinitionExt();
      externalDc.ToExternal(workOrderDefinition);

      return L3DbCommunicationManager.UpdateWorkOrderDefinitionAsync(externalDc);
    }

    public virtual Task<DataContractBase> DeleteWorkOrderDefinitionAsync(DCL3L2WorkOrderDefinition workOrderDefinition)
    {
      DCL3L2WorkOrderDefinitionExt externalDc = new DCL3L2WorkOrderDefinitionExt();
      externalDc.ToExternal(workOrderDefinition);

      return L3DbCommunicationManager.DeleteWorkOrderDefinitionAsync(externalDc);
    }

    public virtual Task<DataContractBase> ResetWorkOrderReportAsync(DCL2L3WorkOrderReport dc)
    {
      DCL2L3WorkOrderReportExt externalDc = new DCL2L3WorkOrderReportExt();
      externalDc.ToExternal(dc);

      return L3DbCommunicationManager.ResetWorkOrderReportAsync(externalDc);
    }

    public virtual Task<DataContractBase> ResetProductReportAsync(DCL2L3ProductReport dc)
    {
      DCL2L3ProductReportExt externalDc = new DCL2L3ProductReportExt();
      externalDc.ToExternal(dc);

      return L3DbCommunicationManager.ResetProductReportAsync(externalDc);
    }

    public virtual Task<DataContractBase> CreateWorkOrderReport(DCL2L3WorkOrderReport dc)
    {
      return L3DbCommunicationManager.CreateWorkOrderReport(dc);
    }

    public virtual Task<DataContractBase> CreateProductReport(DCL2L3ProductReport dc)
    {
      return L3DbCommunicationManager.CreateProductReport(dc);
    }
  }
}
