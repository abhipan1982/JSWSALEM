using System.Collections.Generic;
using PE.BaseDbEntity.EnumClasses;
using PE.TRK.Base.Models._Base;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Abstract;

namespace PE.TRK.Base.Models.TrackingComponents.CollectionPositions.Abstract
{
  public abstract class TrackingCollectionPositionBase : PointBase
  {
    public int PositionId { get; private set; }
    
    public bool IsVirtual { get; private set; }
    
    public TrackingCollectionElementAbstractBase Element { get; private set; }
    
    public TrackingCollectionPositionBase(int positionId, int assetCode, bool isVirtual) 
      : base(assetCode, 0)
    {
      PositionId = positionId;
      IsVirtual = isVirtual;
    }
    
    public void SetElement(TrackingCollectionElementAbstractBase elementAbstract)
    {
      Element = elementAbstract;
    }
  }
}
