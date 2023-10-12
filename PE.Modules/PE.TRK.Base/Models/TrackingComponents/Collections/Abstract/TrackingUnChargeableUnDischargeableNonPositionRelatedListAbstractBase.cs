using System;
using System.Collections.Generic;
using System.Linq;
using PE.BaseDbEntity.EnumClasses;
using PE.Helpers;
using PE.TRK.Base.Models._Base;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Abstract
{
  public abstract class TrackingUnChargeableUnDischargeableNonPositionRelatedListAbstractBase
    : TrackingNonPositionRelatedListAbstractBase,
    IUnChargeableCollectionAreaBase,
    IUnDischargeableCollectionAreaBase
  {
    protected TrackingUnChargeableUnDischargeableNonPositionRelatedListAbstractBase(
      ITrackingEventStorageProviderBase trackingEventStorageProvider,
      int areaAssetCode,
      List<QueuePosition> positionsToBeInitialized,
      int positionsAmount,
      int virtualPositionsAmount)
      : base(
          trackingEventStorageProvider,
          areaAssetCode,
          positionsToBeInitialized,
          positionsAmount,
          virtualPositionsAmount)
    {
    }

    public virtual ITrackingInstructionDataContractBase UnChargeElement(DateTime operationDate)
    {
      lock (LockObject)
      {
        if (!Elements.Any())
          throw new ArgumentException($"There is nothing to uncharge for {AreaAssetCode}");

        var elementToRemove = Elements.First();

        Elements.RemoveAt(0);

        TaskHelper.FireAndForget(QueuePositionChange);

        TaskHelper.FireAndForget(() => TriggerTrackingEvent(elementToRemove.MaterialInfoCollection.MaterialInfos.FirstOrDefault().MaterialId,
          TrackingEventType.UnEnter,
          operationDate));

        return elementToRemove;
      }
    }

    public virtual bool TryUnDischargeVirtualElement(DateTime operationDate)
    {
      lock (LockObject)
      {
        if (VirtualElements.Any())
        {
          var virtualElement = VirtualElements.First();

          Elements.Add(virtualElement);

          VirtualElements.Remove(virtualElement);

          TaskHelper.FireAndForget(QueuePositionChange);

          TaskHelper.FireAndForget(() => TriggerTrackingEvent(virtualElement.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.MaterialId ?? 0,
            TrackingEventType.UnExit,
            operationDate));

          return true;
        }

        return false;
      }
    }

    public virtual void UnDischargeElement(ITrackingInstructionDataContractBase elementAbstract, DateTime operationDate)
    {
      lock (LockObject)
      {
        var element = elementAbstract as TrackingCollectionElementAbstractBase;

        Elements.Add(element);

        TaskHelper.FireAndForget(QueuePositionChange);

        TaskHelper.FireAndForget(() => TriggerTrackingEvent(element.MaterialInfoCollection.MaterialInfos.FirstOrDefault().MaterialId,
          TrackingEventType.UnExit,
          operationDate));
      }
    }
  }
}
