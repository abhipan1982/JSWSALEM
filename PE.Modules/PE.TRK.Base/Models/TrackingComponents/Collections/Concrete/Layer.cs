using System;
using System.Collections.Generic;
using System.Linq;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseModels.DataContracts.Internal.HMI;
using PE.Helpers;
using PE.TRK.Base.Managers;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Concrete;
using PE.TRK.Base.Models.TrackingComponents.CollectionPositions.Concrete;
using PE.TRK.Base.Models.TrackingComponents.Collections.Abstract;
using PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Concrete;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;
using SMF.Core.Notification;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Concrete
{
  public class Layer : TrackingPositionRelatedCollectionAbstractBase
  {
    public bool HasChanged { get; protected set; }
    protected bool ExistingPositionsImported { get; set; }

    public Layer(ITrackingEventStorageProviderBase trackingEventStorageProvider,
      int areaAssetCode,
      int positionsAmount,
      int virtualPositionsAmount,
      List<QueuePosition> positionsToBeInitialized,
      Dictionary<long, short> layerWithMaterialsConterMap,
      bool existingPositionsImported = true)
      : base(trackingEventStorageProvider,
          areaAssetCode,
          positionsAmount,
          virtualPositionsAmount,
          positionsToBeInitialized,
          layerWithMaterialsConterMap)
    {
      ExistingPositionsImported = existingPositionsImported;
    }

    protected override void InitPositions(List<QueuePosition> positionsToBeInitialized)
    {
      base.InitPositions(positionsToBeInitialized);

      if (!ExistingPositionsImported)
        QueuePositionChange();
    }

    protected override void AddPosition(List<QueuePosition> positionsToBeInitialized, int index, bool isVirtual)
    {
      var positionInDatabase =
                positionsToBeInitialized.FirstOrDefault(q => q.PositionSeq == index && q.IsVirtualPosition == isVirtual);

      var position = new TrackingCollectionPosition(index, AreaAssetCode, isVirtual);

      if (positionInDatabase != null && !positionInDatabase.IsEmpty && positionInDatabase.RawMaterialId.HasValue)
      {
        position.SetElement(new TrackingCollectionElementBase(new LayerMaterialInfo(positionInDatabase.RawMaterialId.Value,
          LayerWithMaterialsCounterMap.ContainsKey(positionInDatabase.RawMaterialId.Value) ?
          LayerWithMaterialsCounterMap[positionInDatabase.RawMaterialId.Value] : (short)0)));
      }

      Positions.Add(position);
    }

    public ITrackingInstructionDataContractBase DischargePositionByLayerId(long layerId)
    {
      lock (LockObject)
      {
        var operationDate = DateTime.Now;
        var position = Positions
          .Where(x => x.Element != null && x.Element.MaterialInfoCollection[layerId] != null)
          .FirstOrDefault();

        if (position == null)
        {
          throw new Exception($"For DischargePositionByLayerId operation there is no position with LayerId: {layerId}");
        }

        var result = position.Element;

        position.SetElement(null);

        TaskHelper.FireAndForget(QueuePositionChange);
        TaskHelper.FireAndForget(() => TriggerTrackingEvent(layerId,
          TrackingEventType.Exit,
          operationDate));

        return result;
      }
    }

    public List<LayerElement> GetLayers()
    {
      return (
        from position in Positions
        where position.Element != null
        let material = position.Element.MaterialInfoCollection.MaterialInfos.FirstOrDefault()
        select new LayerElement()
        {
          Id = material.MaterialId,
          HasChanged = HasChanged,
          PositionOrder = position.PositionId,
          MaterialsSum = (material as LayerMaterialInfo)?.MaterialsSum ?? 0
        })
      .ToList();
    }

    public override void MoveForward(DateTime operationDate)
    {
      lock (LockObject)
      {
        var emptyPosition = Positions.Where(x => x.Element == null).FirstOrDefault();
        if (emptyPosition == null)
          throw new Exception("For MoveForward operation in Layer there should be at least one position empty");

        int emptyPositionIndex = emptyPosition.PositionId - 1;

        while (emptyPositionIndex > 0)
        {
          Positions[emptyPositionIndex].SetElement(Positions[emptyPositionIndex - 1].Element);

          emptyPositionIndex--;
        }

        Positions[0].SetElement(null);

        TaskHelper.FireAndForget(QueuePositionChange);
      }
    }

    public void SetHasChanged(bool hasChanged)
    {
      HasChanged = hasChanged;
    }

    public void AssignMaterialsSumByRawMaterialId(long materialId, short materialsSum)
    {
      var position = Positions
        .FirstOrDefault(x => x.Element is not null && x.Element.MaterialInfoCollection[materialId] is not null);
      if (position is null)
        NotificationController.RegisterAlarm(AlarmDefsBase.LayerWithMaterialNotFound, "Cannot find layer with material to assign materials sum.");
      else
      {
        var materialInfo = position.Element.MaterialInfoCollection[materialId];
        if (materialInfo is LayerMaterialInfo layerMaterialInfo)
          layerMaterialInfo.SetMaterialsSum(materialsSum);
        else
          NotificationController.RegisterAlarm(AlarmDefsBase.LayerInfoNotInValidType, "Layer info is not in LayerMaterialInfo type.");

        HasChanged = true;
      }
    }

    protected override void QueuePositionChange()
    {
      HasChanged = true;

      base.QueuePositionChange();
    }
  }
}
