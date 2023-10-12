using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.MNT
{
  public class DCQuantityType : DataContractBase
  {
    [DataMember]
    public long QuantityTypeId
    {
      get;
      set;
    }

    [DataMember]
    public string QuantityTypeName
    {
      get;
      set;
    }

    [DataMember]
    public DateTime? CreatedTs
    {
      get;
      set;
    }

    [DataMember]
    public DateTime? LastUpdateTs
    {
      get;
      set;
    }

    [DataMember]
    public string QuantityTypeCode
    {
      get;
      set;
    }

    [DataMember]
    public long? FkUnitId
    {
      get;
      set;
    }

    [DataMember]
    public long? FKExtUnitCategoryId
    {
      get;
      set;
    }
  }
}
