using PE.BaseDbEntity.EnumClasses;
using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Base.Models.TrackingEntities.Abstract
{
  public abstract class TrackingStepBase
  {
    public TrackingStepBase(int assetCode, ITrackingEventStorageProviderBase eventStorageProvider)
    {
      AssetCode = assetCode;
      EventStorageProvider = eventStorageProvider;
    }

    public int AssetCode { get; }
    
    protected readonly ITrackingEventStorageProviderBase EventStorageProvider;
  }
}
