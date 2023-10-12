using System.Collections.Generic;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.QTY
{
  public class DCQualityAssignment : DataContractBase
  {
    public DCQualityAssignment()
    {
      ProductIds = new List<long>();
      RawMaterialIds = new List<long>();
      DefectCatalogueElementIds = new List<long>();
    }

    [DataMember]
    public List<long> ProductIds
    {
      get;
      set;
    }

    [DataMember]
    public List<long> RawMaterialIds
    {
      get;
      set;
    }

    [DataMember]
    public List<long> DefectCatalogueElementIds
    {
      get;
      set;
    }

    [DataMember]
    public ProductQuality QualityFlag
    {
      get;
      set;
    }

    [DataMember]
    public string Remark
    {
      get;
      set;
    }

    [DataMember]
    public long AssetId
    {
      get;
      set;
    }
  }
}
