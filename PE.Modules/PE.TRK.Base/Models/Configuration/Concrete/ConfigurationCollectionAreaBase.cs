using System.Collections.Generic;
using PE.TRK.Base.Models._Base;
using PE.TRK.Base.Models.Configuration.Abstract;

namespace PE.TRK.Base.Models.Configuration.Concrete
{
  public class ConfigurationCollectionAreaBase : TrackingSimpleAreaBase, ITrackingSimpleCollectionAreaBase
  {
    public bool IsPositionBasedCollection { get; protected set; }
    public int PositionsAmount { get; protected set; }
    public int VirtualPositionsAmount { get; protected set; }

    public ConfigurationCollectionAreaBase(int areaAssetCode, 
      bool isPositionBasedCollection,
      int positionsAmount,
      int virtualPositionsAmount) 
      : base(areaAssetCode)
    {
      IsPositionBasedCollection = isPositionBasedCollection;
      PositionsAmount = positionsAmount;
      VirtualPositionsAmount = virtualPositionsAmount;
    }
  }
}
