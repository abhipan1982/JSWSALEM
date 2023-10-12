using System;
using System.Collections.Generic;
using System.Linq;
using PE.BaseDbEntity.EnumClasses;
using PE.Helpers;
using PE.TRK.Base.Models._Base;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Abstract;
using PE.TRK.Base.Models.TrackingComponents.Collections.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;
using SMF.Core.Notification;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Concrete
{
  public class FurnaceBase : TrackingUnChargeableUnDischargeableNonPositionRelatedListAbstractBase, IChargeableOnExitCollectionAreaBase
  {
    public FurnaceBase(ITrackingEventStorageProviderBase trackingEventStorageProvider,
      int areaAssetCode,
      List<QueuePosition> positionsToBeInitialized,
      int positionsAmount,
      int virtualPositionsAmount)
      : base(trackingEventStorageProvider, areaAssetCode, positionsToBeInitialized, positionsAmount, virtualPositionsAmount)
    {

    }

    public void ChargeElementOnExit(ITrackingInstructionDataContractBase elementAbstract, DateTime operationDate)
    {
      lock (LockObject)
      {
        var element = elementAbstract as TrackingCollectionElementAbstractBase;
        if (Elements.Any(x => x.MaterialInfoCollection.Equals(element.MaterialInfoCollection)))
        {
          NotificationController.Warn($"Material: {element.MaterialInfoCollection.MaterialInfos.FirstOrDefault().MaterialId} already exist in AreaAssetCode: {AreaAssetCode}");
          return;
        }

        Elements.Add(element);

        TaskHelper.FireAndForget(QueuePositionChange);

        TaskHelper.FireAndForget(() => TriggerTrackingEvent(element.MaterialInfoCollection.MaterialInfos.FirstOrDefault().MaterialId,
          TrackingEventType.Enter,
          operationDate));
      }
    }

  }
}
