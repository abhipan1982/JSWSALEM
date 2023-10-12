using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.BaseDbEntity.EnumClasses;

namespace PE.TRK.Base.Models._Base
{
  public class AssetBase
  {
    public long AssetId { get; set; }
    public string AssetName { get; set; }
    public int AssetCode { get; set; }

    public Dictionary<int, FeatureBase> FeaturesDictionary = new Dictionary<int, FeatureBase>();
  }

  public class FeatureBase
  {
    public long FeatureId { get; set; }
    public int FeatureCode { get; set; }
    public FeatureType FeatureType { get; set; }
  }
}
