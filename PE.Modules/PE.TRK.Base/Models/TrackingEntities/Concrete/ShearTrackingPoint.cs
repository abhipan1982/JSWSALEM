using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Base.Models.TrackingEntities.Concrete
{
  public class ShearTrackingPoint : TrackingPoint
  {
    public ShearTrackingPoint(int occupiedFeatureCode, 
      int assetCode, 
      int stepAssetCode, 
      int sequence,
      ITrackingEventStorageProviderBase eventStorageProvider) 
      : base(occupiedFeatureCode, assetCode, stepAssetCode, sequence, eventStorageProvider)
    {
    }

    public DateTime? AutoChoppingStartDate { get; set; }
    public DateTime? AutoChoppingEndDate { get; set; }
    public DateTime? ManualChoppingStartDate { get; set; }
    public DateTime? ManualChoppingEndDate { get; set; }
  }
}
