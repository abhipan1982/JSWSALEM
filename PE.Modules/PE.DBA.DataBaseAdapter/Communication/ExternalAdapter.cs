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
    //Added by Abhishek
    public virtual Task<DCL3L2BatchData> CreateBatchDataAsync(DCL3L2BatchData dcBatchData)
    {
      return HandleIncommingMethod(_handler.CreateBatchDataAsync, dcBatchData);
    }

    public virtual Task<DCL3L2BatchData> UpdateBatchDataAsync(DCL3L2BatchData dcBatchData)
    {
      return HandleIncommingMethod(_handler.UpdateBatchDataAsync, dcBatchData);
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
