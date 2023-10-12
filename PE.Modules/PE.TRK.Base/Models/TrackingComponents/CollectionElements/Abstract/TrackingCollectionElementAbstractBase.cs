using PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;

namespace PE.TRK.Base.Models.TrackingComponents.CollectionElements.Abstract
{
  public abstract class TrackingCollectionElementAbstractBase : ITrackingInstructionDataContractBase
  {
    public MaterialInfoCollection MaterialInfoCollection { get; } = new MaterialInfoCollection();
  }
}
