using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PE.BaseModels.DataContracts.Internal.HMI;
using PE.Helpers;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;
using SMF.Core.Notification;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Abstract
{
  public abstract class TrackingNonPositionRelatedQueueWithMultipleMaterialsBase : TrackingNonPositionRelatedQueueBase
  {
    #region ctor

    protected TrackingNonPositionRelatedQueueWithMultipleMaterialsBase(ITrackingEventStorageProviderBase trackingEventStorageProvider, 
      short areaAssetCode,
      List<QueuePosition> positionsToBeInitialized)
    : base(trackingEventStorageProvider, areaAssetCode, positionsToBeInitialized)
    {
    }

    #endregion ctor

    #region protected methods

    public override List<MaterialPosition> GetMaterials()
    {
      var result = new List<MaterialPosition>();
      int order = 0;

      foreach (var element in Elements)
      {
        int materialOrder = 0;
        int positionOrder = ++order;
        foreach (var materialInfo in element.MaterialInfoCollection.MaterialInfos)
        {
          result.Add(new MaterialPosition()
          {
            RawMaterialId = materialInfo.MaterialId, PositionOrder = positionOrder, Order = ++materialOrder
          });
        }
      }

      return result;
    }
    

    public override void RemoveMaterial(long rawMaterialId)
    {
      lock (LockObject)
      {
        TrackingCollectionElementAbstractBase elementAbstract = Elements.FirstOrDefault();

        if (elementAbstract == null)
        {
          throw new Exception($"Storage: {AreaAssetCode} is empty");
        }

        if (elementAbstract.MaterialInfoCollection[rawMaterialId] == null)
        {
          throw new Exception($"Storage: {AreaAssetCode} does not have rawMaterial: {rawMaterialId} at first position");
        }

        Elements.Dequeue();

        NotificationController.Info(
          $"Material: {rawMaterialId} has been successfully removed from storage {AreaAssetCode}");

        TaskHelper.FireAndForget(QueuePositionChange);
      }
    }


    #endregion protected methods
  }
}
