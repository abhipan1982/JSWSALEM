using PE.TRK.Base.Models.TrackingComponents.CollectionPositions.Abstract;

namespace PE.TRK.Base.Models.TrackingComponents.CollectionPositions.Concrete
{
  public class TrackingCollectionPosition : TrackingCollectionPositionBase
  {
    #region ctor

    public TrackingCollectionPosition(int positionId, int assetCode, bool isVirtual)
    : base(positionId, assetCode, isVirtual)
    {
    }

    #endregion
  }
}
