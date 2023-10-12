using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Abstract;
using PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Abstract;

namespace PE.TRK.Base.Models.TrackingComponents.CollectionElements.Concrete
{
  public class TrackingCollectionElementBase : TrackingCollectionElementAbstractBase
  {
    public TrackingCollectionElementBase()
    {
    }

    public TrackingCollectionElementBase(MaterialInfoBase materialInfo)
    {
      MaterialInfoCollection.MaterialInfos.Add(materialInfo);
    }

  }
}
