using System;
using System.Collections.Generic;
using System.Text;
using PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Abstract;

namespace PE.TRK.Base.Models.TrackingComponents.CollectionElements.Concrete
{
  public class TrackingGreyElementBase : TrackingCollectionElementBase
  {
    public int? CtrAreaAssetCode { get; set; }

    public TrackingGreyElementBase()
    {

    }

    public TrackingGreyElementBase(MaterialInfoBase materialInfo)
      :base(materialInfo)
    {

    }
  }
}
