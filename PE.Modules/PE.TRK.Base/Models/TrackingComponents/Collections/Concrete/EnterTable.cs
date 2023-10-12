using System;
using System.Collections.Generic;
using PE.TRK.Base.Models.TrackingComponents.Collections.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;
using SMF.Core.Notification;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Concrete
{
  public class EnterTable : TrackingNonPositionRelatedQueueBase
  {
    #region ctor

    public EnterTable(ITrackingEventStorageProviderBase trackingEventStorageProvider,
      int assetCode, 
      List<QueuePosition> positionsToBeInitialized) 
      : base(trackingEventStorageProvider, assetCode, positionsToBeInitialized)
    {
    }

    #endregion

    #region public methods


    // public void SetMaterialAfterDivide(long rawMaterialId, long parentRawMaterialId)
    // {
    //   TrackingCollectionElementAbstractBase element =
    //     Elements.FirstOrDefault(e => e.MaterialInfo.MaterialRequested && !e.MaterialInfo.MaterialId.HasValue);
    //
    //   if (element == null)
    //   {
    //     NotificationController.Error("There is no position without Material on EnterTableET");
    //     return;
    //   }
    //
    //   element.MaterialInfo.ChangeMaterialId(rawMaterialId);
    //   element.MaterialInfo.ChangeParentMaterialId(parentRawMaterialId);
    //   element.MaterialInfo.ChangeMaterialRequested(false);
    // }

    public override ITrackingInstructionDataContractBase GetFirstVirtualPosition()
    {
      throw new NotImplementedException();
    }
    public override ITrackingInstructionDataContractBase GetLastVirtualPosition()
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
