using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PE.Helpers;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Abstract;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Concrete;
using PE.TRK.Base.Models.TrackingComponents.Collections.Abstract;
using PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Abstract;
using PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Concrete;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;
using SMF.Core.Notification;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Concrete
{
  public class CtrGreyListArea : TrackingNonPositionRelatedListAbstractBase
  {
    public CtrGreyListArea(ITrackingEventStorageProviderBase trackingEventStorageProvider,
      int areaAssetCode,
      List<QueuePosition> positionsToBeInitialized,
      int positionsAmount,
      int virtualPositionsAmount)
      : base(trackingEventStorageProvider, areaAssetCode, positionsToBeInitialized, positionsAmount, virtualPositionsAmount)
    {

    }

    protected override void InitPositions(List<QueuePosition> positionsToBeInitialized)
    {
      if (positionsToBeInitialized.Any())
      {
        foreach (QueuePosition positionToBeInitialized in
          positionsToBeInitialized.OrderByDescending(p => p.PositionSeq))
        {
          if (positionToBeInitialized?.RawMaterialId == null)
          {
            continue;
          }

          TrackingGreyElementBase materialElement = new TrackingGreyElementBase();

          materialElement.MaterialInfoCollection.MaterialInfos.Add(new MaterialInfo(positionToBeInitialized.RawMaterialId.Value));
          materialElement.CtrAreaAssetCode = positionToBeInitialized.CtrAssetCode;

          if (positionToBeInitialized.IsVirtualPosition)
          {
            VirtualElements.Add(materialElement);
          }
          else
          {
            ChargeElement(materialElement, DateTime.Now, true);
          }
        }
      }
    }

    public void SetCTRAssetCodeForMaterialId(long rawMaterialId, int assetCode)
    {
      try
      {
        var element = Elements.FirstOrDefault(x => x.MaterialInfoCollection[rawMaterialId] != null);

        if (element == null)
        {
          ChargeElement(new TrackingGreyElementBase(new MaterialInfo(rawMaterialId)), DateTime.Now, true);
        }
        else if (element is TrackingGreyElementBase greyElement && greyElement.CtrAreaAssetCode != assetCode)
        {
          greyElement.CtrAreaAssetCode = assetCode;

          TaskHelper.FireAndForget(QueuePositionChange);
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, "Something went wrong while SetCTRAssetCodeForMaterialId");
      }
    }

    protected override void QueuePositionChange()
    {
      try
      {
        TrackingCollectionElementAbstractBase[] elementsArray = Elements.ToArray();

        List<QueuePosition> list = new List<QueuePosition>();
        for (int i = 0; i < elementsArray.Length; i++)
        {
          var element = elementsArray[i];
          var materialInfo = element.MaterialInfoCollection.MaterialInfos.FirstOrDefault();
          if (materialInfo != null)
          {
            list.Add(new QueuePosition(i + 1, 1, AreaAssetCode, materialInfo.MaterialId, false,
              false, 
              element.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.GetHistoryItemDateByAreaCodeAndHistoryType(AreaAssetCode, TrackingHistoryTypeEnum.Charge),
              element is TrackingGreyElementBase greyElement ? greyElement.CtrAreaAssetCode : (int?)null,
              ((materialInfo.CorrelationId as IntCorrelationId)?.Value)?.ToString()));
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
