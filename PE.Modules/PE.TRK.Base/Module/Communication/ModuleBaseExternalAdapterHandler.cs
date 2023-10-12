using System;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.Helpers;
using PE.TRK.Base.Managers.Abstract;
using PE.TRK.Base.Providers.Abstract;
using SMF.Core.DC;
using SMF.Core.Notification;

namespace PE.TRK.Base.Module.Communication
{
  public class ModuleBaseExternalAdapterHandler
  {
    protected readonly ITrackingStorageProviderBase StorageProvider;
    protected readonly ITrackingEventStorageProviderBase EventStorageProvider;
    protected readonly ITrackingManagerBase TrackingManager;
    protected readonly ITrackingDispatcherManagerBase DispatcherManager;

    public ModuleBaseExternalAdapterHandler(ITrackingStorageProviderBase storageProvider,
      ITrackingEventStorageProviderBase eventStorageProvider,
      ITrackingManagerBase trackingManager,
      ITrackingDispatcherManagerBase dispatcherManager)
    {
      StorageProvider = storageProvider;
      EventStorageProvider = eventStorageProvider;
      TrackingManager = trackingManager;
      DispatcherManager = dispatcherManager;
    }

    public virtual Task<DataContractBase> ProcessTrackingPointSignalAsync(DcTrackingPointSignal dc)
    {
      var result = EventStorageProvider.TrackingPointSignalsToBeProcessed.TryAdd(Guid.NewGuid(), dc);
      if (!result)
        NotificationController.Error($"Cannot add Signal for processing {dc.FeatureCode}");

      return Task.FromResult(new DataContractBase());
    }

    public virtual Task<DataContractBase> SendAggregatedL1TrackingPointSignalsAsync(DcAggregatedTrackingPointSignal arg)
    {
      foreach (var dc in arg.TrackingPointSignals)
      {
        var result = EventStorageProvider.TrackingPointSignalsToBeProcessed.TryAdd(Guid.NewGuid(), dc);
        if (!result)
          NotificationController.Error($"Cannot add Signal for processing {dc.FeatureCode}");
      }

      return Task.FromResult(new DataContractBase());
    }

    public virtual Task<DataContractBase> HardRemoveMaterialFromTracking(DCHardRemoveMaterial message)
    {
      return TrackingManager.HardRemoveMaterialFromTracking(message);
    }

    public virtual Task<DataContractBase> ProcessScrapMessageAsync(DCL1ScrapData message)
    {
      return TrackingManager.ProcessScrapMessageAsync(message);
    }

    public virtual Task<DataContractBase> ProductUndo(DCProductUndo message)
    {
      return TrackingManager.ProductUndo(message);
    }

    public virtual Task<DataContractBase> CollectionMoveForward(DCUpdateArea arg)
    {
      return TrackingManager.CollectionMoveForward(arg);
    }

    public virtual Task<DataContractBase> CollectionMoveBackward(DCUpdateArea arg)
    {
      return TrackingManager.CollectionMoveBackward(arg);
    }

    public virtual Task<DataContractBase> FurnaceUnCharge(DataContractBase message)
    {
      return TrackingManager.FurnaceUnCharge(message);
    }

    public virtual Task<DataContractBase> MarkAsReady(DCMaterialReady message)
    {
      return TrackingManager.MarkAsReady(message);
    }

    public virtual Task<DataContractBase> RemoveMaterialFromArea(DCRemoveMaterial arg)
    {
      return TrackingManager.RemoveMaterialFromArea(arg);
    }

    public virtual Task<DataContractBase> FurnaceCharge(DataContractBase message)
    {
      return TrackingManager.FurnaceCharge(message);
    }

    public virtual Task<DataContractBase> ChargeWOOnFurnaceLastPosition(DCChargeMaterialOnFurnaceExit message)
    {
      return TrackingManager.FurnaceChargeOnExit(message);
    }

    public virtual async Task<DataContractBase> FinishLayerAsync(DCLayer message)
    {
      DataContractBase result = new DataContractBase();

      await DispatcherManager.ProcessLayerFormFinishedEventAsync(message.AreaCode, message.Id);

      return result;
    }

    public virtual async Task<DataContractBase> TransferLayerAsync(DCLayer message)
    {
      DataContractBase result = new DataContractBase();

      await DispatcherManager.ProcessLayerTransferredEventAsync(DateTime.Now.ExcludeMiliseconds(), message.AreaCode, message.Id);

      return result;
    }

    public virtual Task<DataContractBase> FurnaceUnDischarge(DataContractBase message)
    {
      return TrackingManager.FurnaceUnDischarge(message);
    }

    public virtual Task<DataContractBase> FurnaceDischarge(DataContractBase message)
    {
      return TrackingManager.FurnaceDischarge(message);
    }

    public virtual Task<DataContractBase> ReplaceMaterialPosition(DCMoveMaterial arg)
    {
      return TrackingManager.ReplaceMaterialPosition(arg);
    }

    public virtual Task<DataContractBase> ChargingGridCharge(DataContractBase arg)
    {
      return TrackingManager.ChargingGridCharge(arg);
    }

    public virtual Task<DataContractBase> ChargingGridUnCharge(DataContractBase arg)
    {
      return TrackingManager.ChargingGridUnCharge(arg);
    }

    public virtual Task<DataContractBase> RejectRawMaterial(DCRejectMaterialData message)
    {
      return DispatcherManager.RejectRawMaterialAsync(message);
    }

    public virtual Task<DataContractBase> UnassignMaterial(DCMaterialUnassign message)
    {
      return TrackingManager.UnassignMaterial(message);
    }

    public virtual Task<DataContractBase> AssignMaterial(DCMaterialAssign message)
    {
      return TrackingManager.AssignMaterial(message);
    }

    public virtual Task<DataContractBase> CreateBundleAsync(DCBundleData message)
    {
      return TrackingManager.CreateBundleAsync(message);
    }
  }
}
