using System;
using System.Collections.Generic;
using System.Linq;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Abstract;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Concrete;
using PE.TRK.Base.Models.TrackingComponents.Collections.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;
using SMF.Core.Notification;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Concrete
{
  public class TransportListArea : TrackingNonPositionRelatedListAbstractBase
  {
    public TransportListArea(ITrackingEventStorageProviderBase trackingEventStorageProvider,
      int areaAssetCode,
      List<QueuePosition> positionsToBeInitialized,
      int positionsAmount,
      int virtualPositionsAmount)
      : base(trackingEventStorageProvider, areaAssetCode, positionsToBeInitialized, positionsAmount, virtualPositionsAmount)
    {

    }

    protected override void QueuePositionChange()
    {
      try
      {
        TrackingCollectionElementAbstractBase[] elementsArray = Elements.ToArray();

        List<QueuePosition> list = new List<QueuePosition>();
        for (int i = 0; i < elementsArray.Length; i++)
        {
          var element = elementsArray[i] as TrackingGreyElementBase;
          var materialInfo = element?.MaterialInfoCollection.MaterialInfos.FirstOrDefault();
          if (materialInfo != null)
          {
            list.Add(new QueuePosition(i + 1, 1, AreaAssetCode, materialInfo.MaterialId, false,
              false,
              element.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.GetHistoryItemDateByAreaCodeAndHistoryType(AreaAssetCode, TrackingHistoryTypeEnum.Charge),
              element.CtrAreaAssetCode, ((materialInfo.CorrelationId as IntCorrelationId)?.Value)?.ToString()));
          }
        }

        TrackingEventStorageProvider.TrackingQueuePositionChangeEvents.Enqueue(
          new TrackingQueuePositionChangeEventArgs(list, AreaAssetCode));
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex,
          $"Something went wrong while Enqueue to TrackingQueuePositionChangeEvents with parameters: AssetCode: {AreaAssetCode}");
      }
    }
  }
}
