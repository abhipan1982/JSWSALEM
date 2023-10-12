using System.Collections.Generic;
using System.Linq;

namespace PE.BaseModels.AbstractionModels.L1A
{
  public abstract class TrackingDataBase
  {
    #region ctor

    public TrackingDataBase()
    {
      TrackingAreas = new List<TrackingAreaBase>();
    }

    #endregion ctor

    #region properties

    public List<TrackingAreaBase> TrackingAreas { get; protected set; }

    #endregion properties

    public TrackingAreaBase GetAreaById(int id)
    {
      return TrackingAreas.Where(x => x.TrackArea == id).FirstOrDefault();
    }

    public T GetArea<T>() where T : TrackingAreaBase
    {
      return (T)TrackingAreas.Where(x => x is T).FirstOrDefault();
    }
  }
}
