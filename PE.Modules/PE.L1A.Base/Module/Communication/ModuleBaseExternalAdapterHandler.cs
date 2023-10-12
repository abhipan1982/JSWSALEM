using System;
using System.Threading.Tasks;
using PE.BaseInterfaces.Managers.L1A;
using PE.BaseModels.DataContracts.Internal.L1A;
using SMF.Core.DC;

namespace PE.L1A.Base.Module.Communication
{
  public class ModuleBaseExternalAdapterHandler
  {
    protected readonly IL1SignalManagerBase L1SignalManager;
    protected readonly IMeasurementStorageManagerBase MeasurementStorageManager;
    protected readonly IL1MillControlDataManagerBase MillControlDataManager;
    protected readonly IBypassManagerBase BypassManager;    

    public ModuleBaseExternalAdapterHandler(IL1SignalManagerBase l1SignalManager, IMeasurementStorageManagerBase measurementStorageManager,
      IL1MillControlDataManagerBase millControlDataManager, IBypassManagerBase bypassManager)
    {
      MeasurementStorageManager = measurementStorageManager;
      MillControlDataManager = millControlDataManager;
      BypassManager = bypassManager;
      L1SignalManager = l1SignalManager;
    }

    public virtual Task<DataContractBase> ProcessMeasurementRequestAsync(DcRelatedToMaterialMeasurementRequest data)
    {
      _ = MeasurementStorageManager.ProcessMeasurementsAsync(data);

      return Task.FromResult(new DataContractBase());
    }

    public virtual async Task<DataContractBase> ProcessAggregatedMeasurementRequestAsync(DcAggregatedMeasurementRequest data)
    {
      return await MeasurementStorageManager.ProcessAggregatedMeasurementRequestAsync(data);
    }

    public virtual async Task<DcRawMeasurementResponse> ProcessGetRawMeasurementsAsync(DcAggregatedMeasurementRequest data)
    {
      return await MeasurementStorageManager.ProcessGetRawMeasurementsAsync(data);
    }

    public virtual async Task<DcNdrMeasurementResponse> ProcessNdrMeasurementRequestAsync(DcNdrMeasurementRequest data)
    {
      return await MeasurementStorageManager.ProcessNdrMeasurementRequestAsync(data);
    }

    public virtual async Task<DcMeasurementResponse> ProcessGetMeasurementValueAsync(DcMeasurementRequest data)
    {
      return await MeasurementStorageManager.ProcessGetMeasurementValueAsync(data);
    }

    public virtual async Task<DataContractBase> SendSendMillControlDataAsync(DCMillControlMessage data)
    {
      return await MillControlDataManager.SendMillControlDataMessage(data);
    }

    public virtual Task<DataContractBase> ImportBypasses(DataContractBase dc)
    {
      BypassManager.ImportBypasses();

      return Task.FromResult(new DataContractBase());
    }

    public virtual Task<DataContractBase> ResendTrackingPointSignals(DataContractBase dc)
    {
      return L1SignalManager.ResendTrackingPointSignals();
    }
  }
}
