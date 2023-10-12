using PE.TRK.Base.Managers.Abstract;
using PE.TRK.Base.Module.Communication;
using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Tracking.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    public ExternalAdapterHandler(ITrackingStorageProviderBase storageProvider,
      ITrackingEventStorageProviderBase eventStorageProvider,
      ITrackingManagerBase trackingManager,
      ITrackingDispatcherManagerBase dispatcherManager) 
      : base(storageProvider, eventStorageProvider, trackingManager, dispatcherManager)
    {
    }

    //private readonly ITrackingRawMaterialManager _trackingRawMaterialManager;

    //internal Task<DataContractBase> ProcessL1TrackingEventMessageAsync(DCTrackingEvent message)
    //{
    //  return _trackingRawMaterialManager.ProcessTrackingEventAsync(message);
    //}

    //internal Task<DCRequestMaterial> ProcessL1BaseIdRequestAsync(DCRawMaterialRequest message)
    //{
    //  return _trackingRawMaterialManager.ProcessL1BaseIdRequestAsync(message);
    //}

    //internal Task<DataContractBase> ProcessScrapMessageAsync(DCL1ScrapData message)
    //{
    //  return _trackingRawMaterialManager.ProcessScrapMessageAsync(message);
    //}

    //internal Task<DataContractBase> ProcessCutDataMessageAsync(DCL1CutData message)
    //{
    //  return _trackingRawMaterialManager.ProcessCutDataMessageAsync(message);
    //}

    //internal Task<DCRequestMaterial> ProcessDivisionMaterialMessageAsync(DCL1MaterialDivision message)
    //{
    //  return _trackingRawMaterialManager.ProcessDivisionMaterialMessageAsync(message);
    //}

    //internal Task<DCRequestMaterial> ProcessLayerCreationRequestAsync(DCRawMaterialRequest message)
    //{
    //  return _trackingRawMaterialManager.ProcessLayerCreationRequestAsync(message);
    //}

    //internal Task<DCMaterialsCountResult> ProcessGetMaterialsCountByLayerIdAsync(DCLongIdRequest message)
    //{
    //  return _trackingRawMaterialManager.ProcessGetMaterialsCountByLayerIdAsync(message);
    //}

    //internal Task<DataContractBase> ProcessSetLayerForMaterialsByMaterialIds(DCAppendRawMaterialsToLayerRequest message)
    //{
    //  return _trackingRawMaterialManager.ProcessSetLayerForMaterialsByMaterialIds(message);
    //}

    //internal Task<DataContractBase> ProcessLayerFormFinished(DCLongIdRequest message)
    //{
    //  return _trackingRawMaterialManager.ProcessLayerFormFinished(message);
    //}

    //internal Task<DataContractBase> ProcessLayerTransferredEventAsync(DCLongIdRequest message)
    //{
    //  return _trackingRawMaterialManager.ProcessLayerTransferredEventAsync(message);
    //}

    //internal Task<DataContractBase> AssignMaterialsAsync(DCMaterialAssign message)
    //{
    //  return _trackingRawMaterialManager.AssignMaterialsAsync(message);
    //}

    //internal Task<DataContractBase> UnassignMaterialAsync(DCMaterialAssign message)
    //{
    //  return _trackingRawMaterialManager.UnassignMaterialAsync(message);
    //}

    //internal Task<DataContractBase> RejectRawMaterial(DCRejectMaterialData message)
    //{
    //  return _trackingRawMaterialManager.RejectRawMaterial(message);
    //}

    ////internal Task<DataContractBase> ProcessFinishedMessageAsync(DCL1MaterialFinished message)
    ////{
    ////  return _rawMaterialManager.ProcessFinishedMessageAsync(message);
    ////}

    ////internal Task<DataContractBase> ChangeMaterialStatusAsync(DCNewMaterialStatus message)
    ////{
    ////  return _rawMaterialManager.ChangeMaterialStatusAsync(message);
    ////}


    //internal Task<DataContractBase> MarkMaterialAsScrapAsync(DCMaterialMarkedAsScrap message)
    //{
    //  return _trackingRawMaterialManager.MarkMaterialAsScrapAsync(message);
    //}

    ////internal Task<DataContractBase> ProcessProductionEndAsync(DCMaterialProductionEnd dataToSend)
    ////{
    ////  return _rawMaterialManager.MaterialProductionEndAsync(dataToSend);
    ////}

    ////internal Task<DataContractBase> ConnectRawMaterialWithProductAsync(DCProductData dataToSend)
    ////{
    ////  return _rawMaterialManager.ConnectRawMaterialWithProductAsync(dataToSend);
    ////}
  }
}
