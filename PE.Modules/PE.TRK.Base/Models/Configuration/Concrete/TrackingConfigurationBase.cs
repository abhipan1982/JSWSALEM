using System.Collections.Generic;
using PE.TRK.Base.Models.Configuration.Abstract;

namespace PE.TRK.Base.Models.Configuration.Concrete
{
  public class TrackingConfigurationBase
  {
    public TrackingConfigurationBase()
    {
      TrackingCollectionAreas = new List<ConfigurationCollectionAreaBase>();
      TrackingCtrAreas = new List<ConfigurationCtrAreaBase>();
    }
    public List<ConfigurationCollectionAreaBase> TrackingCollectionAreas { get; set; }
    public List<ConfigurationCtrAreaBase> TrackingCtrAreas { get; set; }
  }
}
