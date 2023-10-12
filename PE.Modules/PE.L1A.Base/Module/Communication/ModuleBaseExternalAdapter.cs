using System.Threading.Tasks;
using PE.BaseInterfaces.Modules;
using PE.BaseModels.DataContracts.Internal.L1A;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.L1A.Base.Module.Communication
{
  public abstract class ModuleBaseExternalAdapter<T> : ExternalAdapterBase<T>, IL1AdapterBase where T : class, IL1AdapterBase
  {
    protected readonly ModuleBaseExternalAdapterHandler Handler;

    #region ctor

    public ModuleBaseExternalAdapter(ModuleBaseExternalAdapterHandler handler) : base()
    {
      Handler = handler;
    }

    #endregion

    public virtual Task<DataContractBase> ProcessMeasurementRequestAsync(DcRelatedToMaterialMeasurementRequest data)
    {
      return HandleIncommingMethod(Handler.ProcessMeasurementRequestAsync, data);
    }

    public virtual Task<DataContractBase> ProcessAggregatedMeasurementRequestAsync(DcAggregatedMeasurementRequest data)
    {
      return HandleIncommingMethod(Handler.ProcessAggregatedMeasurementRequestAsync, data);
    }

    public virtual Task<DcRawMeasurementResponse> ProcessGetRawMeasurementsAsync(DcAggregatedMeasurementRequest data)
    {
      return HandleIncommingMethod(Handler.ProcessGetRawMeasurementsAsync, data);
    }

    public virtual Task<DcNdrMeasurementResponse> ProcessNdrMeasurementRequestAsync(DcNdrMeasurementRequest data)
    {
      return HandleIncommingMethod(Handler.ProcessNdrMeasurementRequestAsync, data);
    }

    public virtual Task<DcMeasurementResponse> ProcessGetMeasurementValueAsync(DcMeasurementRequest data)
    {
      return HandleIncommingMethod(Handler.ProcessGetMeasurementValueAsync, data);
    }

    public Task<DataContractBase> SendMillControlData(DCMillControlMessage data)
    {
      return HandleIncommingMethod(Handler.SendSendMillControlDataAsync, data);
    }

    public virtual Task<DataContractBase> ImportBypasses(DataContractBase dc)
    {
      return HandleIncommingMethod(Handler.ImportBypasses, dc);
    }

    public virtual Task<DataContractBase> ResendTrackingPointSignals(DataContractBase dc)
    {
      return HandleIncommingMethod(Handler.ResendTrackingPointSignals, dc);
    }
  }
}
