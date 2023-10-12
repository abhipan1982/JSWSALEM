using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Base.Models._Base
{
  public abstract class TrackingSimpleAreaBase
  { 
    public int AreaAssetCode { get; private set; }

    public TrackingSimpleAreaBase(int areaAssetCode)
    {
      AreaAssetCode = areaAssetCode;
    }
  }
}
