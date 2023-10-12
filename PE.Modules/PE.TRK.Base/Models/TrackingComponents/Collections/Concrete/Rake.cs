using System;
using System.Collections.Generic;
using PE.Helpers;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Concrete
{
  public class Rake : TrackingPositionRelatedCollectionWithMultipleMaterialsPerPositionBase
  {
    #region ctor

    public Rake(
      ITrackingEventStorageProviderBase trackingEventStorageProvider,
      int areaAssetCode,
      int positionsAmount,
      int virtualPositionsAmount,
      List<QueuePosition> positionsToBeInitialized) : base(trackingEventStorageProvider, areaAssetCode, positionsAmount, virtualPositionsAmount, positionsToBeInitialized) { }

    #endregion

    #region methods

    public override void MoveForward(DateTime operationDate)
    {
      lock (LockObject)
      {
        if (Positions[PositionsAmount - 1].Element != null)
        {
          DischargeElement(operationDate);
        }

        for (int i = PositionsAmount - 1; i >= 0; i--)
        {
          if (i != 0)
          {
            Positions[i].SetElement(Positions[i - 1].Element);
          }
          else
          {
            // for move forward operation first element should be empty
            Positions[i].SetElement(null);
          }
        }

        TaskHelper.FireAndForget(QueuePositionChange);
      }
    }

    #endregion
  }
}
