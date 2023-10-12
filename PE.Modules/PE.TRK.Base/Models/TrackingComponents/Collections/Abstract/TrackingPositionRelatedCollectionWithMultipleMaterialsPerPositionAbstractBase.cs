using System;
using System.Collections.Generic;
using System.Linq;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseModels.DataContracts.Internal.HMI;
using PE.Helpers;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Abstract;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Concrete;
using PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Concrete;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Abstract
{
  public abstract class TrackingPositionRelatedCollectionWithMultipleMaterialsPerPositionAbstractBase
  : TrackingPositionRelatedCollectionAbstractBase
  {
    #region ctor

    protected TrackingPositionRelatedCollectionWithMultipleMaterialsPerPositionAbstractBase(
      ITrackingEventStorageProviderBase trackingEventStorageProvider,
      int areaAssetCode,
      int positionsAmount,
      int virtualPositionsAmount,
      List<QueuePosition> positionsToBeInitialized)
      : base(trackingEventStorageProvider, areaAssetCode, positionsAmount, virtualPositionsAmount, positionsToBeInitialized)
    {
    }

    #endregion ctor

    #region methods

    public override void Drag(long rawMaterialId, int dragSeq, bool isPreviousDragForTheSameAsset, bool dragFromVirtualPosition = false)
    {
      lock (LockObject)
      {
        if (dragSeq > Positions.Count)
          throw new InvalidOperationException($"For area: {AreaAssetCode} dragSeq: {dragSeq} is OutOfRange. Amount of elements: {Positions.Count}");

        var position = Positions.First(x => x.PositionId == dragSeq);

        if (position.Element == null || position.Element.MaterialInfoCollection.MaterialInfos.Count == 0)
          throw new InvalidOperationException($"For area: {AreaAssetCode} dragSeq: {dragSeq} nothing to drag");

        position.Element.MaterialInfoCollection.RemoveMaterial(rawMaterialId);

        if (position.Element.MaterialInfoCollection.MaterialInfos.Count == 0)
          position.SetElement(null);

        TaskHelper.FireAndForget(QueuePositionChange);

        if (!isPreviousDragForTheSameAsset)
          TaskHelper.FireAndForget(() => TriggerTrackingEvent(
            rawMaterialId, BaseDbEntity.EnumClasses.TrackingEventType.Exit, DateTime.Now.ExcludeMiliseconds(), true, AreaAssetCode));
      }
    }

    public override void Drop(long rawMaterialId, int dropSeq, int? dragSeq)
    {
      lock (LockObject)
      {
        var positionsToCheck = Positions;

        if (dragSeq.HasValue && dragSeq <= Positions.Count)
        {
          var positionToExcludeCheck = Positions.First(x => x.PositionId == dragSeq.Value);
          positionsToCheck = Positions.Where(x => x != positionToExcludeCheck).ToList();
        }

        if (dropSeq > Positions.Count)
          throw new InvalidOperationException($"For area: {AreaAssetCode} dragSeq: {dropSeq} is OutOfRange. Amount of elements: {Positions.Count}");


        var position = Positions.First(x => x.PositionId == dropSeq);

        if (positionsToCheck.Where(x => x != position).Any(x => x.Element?.MaterialInfoCollection[rawMaterialId] != null))
        {
          throw new InvalidOperationException($"For area: {AreaAssetCode} material with id: {rawMaterialId} already exist");
        }

        if (position.Element != null && position.Element.MaterialInfoCollection[rawMaterialId] != null)
          throw new InvalidOperationException($"For area: {AreaAssetCode} material with id: {rawMaterialId} already exist");


        if (position.Element != null)
          position.Element.MaterialInfoCollection.MaterialInfos.Add(new MaterialInfo(rawMaterialId));
        else
        {
          position.SetElement(new TrackingCollectionElementBase(new MaterialInfo(rawMaterialId)));
        }

        TaskHelper.FireAndForget(QueuePositionChange);

        if (dragSeq != dropSeq)
          TaskHelper.FireAndForget(() => TriggerTrackingEvent(
            rawMaterialId, BaseDbEntity.EnumClasses.TrackingEventType.Enter, DateTime.Now.ExcludeMiliseconds(), true, AreaAssetCode));
      }
    }

    public override List<MaterialPosition> GetMaterials()
    {
      var result = new List<MaterialPosition>();

      var positionsToAnalyze = Positions
        .Where(p => p.Element != null)
        .ToList();

      foreach (var p in positionsToAnalyze)
      {
        int materialOrder = 0;
        foreach (var materialInfo in p.Element.MaterialInfoCollection.MaterialInfos)
        {
          result.Add(new MaterialPosition()
          {
            RawMaterialId = materialInfo.MaterialId,
            PositionOrder = p.PositionId,
            Order = ++materialOrder,
            IsVirtual = p.IsVirtual
          });
        }
      }

      return result;
    }

    public override ITrackingInstructionDataContractBase ChargeElement(ITrackingInstructionDataContractBase elementAbstract, DateTime operationDate, bool ignoreEvents = false)
    {
      lock (LockObject)
      {
        if (elementAbstract is not TrackingCollectionElementAbstractBase element)
          throw new Exception("Element is not TrackingCollectionElementAbstractBase");

        if (Positions[0].Element != null)
          Positions[0].Element.MaterialInfoCollection.MaterialInfos.Add(element.MaterialInfoCollection.MaterialInfos.FirstOrDefault());
        else
        {
          Positions[0].SetElement(element);
        }

        if (element.MaterialInfoCollection != null)
          element.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.AddHistoryItem(AreaAssetCode, operationDate, TrackingHistoryTypeEnum.Charge);

        if (!ignoreEvents)
        {
          TaskHelper.FireAndForget(QueuePositionChange);

          TaskHelper.FireAndForget(() => TriggerTrackingEvent(element.MaterialInfoCollection.MaterialInfos.FirstOrDefault().MaterialId,
            TrackingEventType.Enter,
            operationDate));
        }

        return element;
      }
    }

    #endregion methods
  }
}
