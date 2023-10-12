namespace PE.TRK.Base.Models._Base
{
  public abstract class TrackingPointBase : PointBase
  {
    public int OccupiedFeatureCode { get; private set; }
    public bool IsShear { get; private set; }

    protected TrackingPointBase(int occupiedFeatureCode, int trackingPointAssetCode, int orderSeq, bool isShear)
      : base(trackingPointAssetCode, orderSeq)
    {
      OccupiedFeatureCode = occupiedFeatureCode;
      IsShear = isShear;
    }
  }

  public abstract class PointBase
  {
    public int AssetCode { get; private set; }
    public int OrderSeq { get; private set; }

    protected PointBase(int assetCode, int orderSeq)
    {
      AssetCode = assetCode;
      OrderSeq = orderSeq;
    }
  }
}
