using System.Collections.Generic;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.RLS
{
  public class DCRollSetToCassetteAction : DataContractBase
  {
    [DataMember]
    public long? CassetteId
    {
      get;
      set;
    }

    [DataMember]
    public long? RollSetId
    {
      get;
      set;
    }

    [DataMember]
    public RollSetCassetteAction Action
    {
      get;
      set;
    }

    [DataMember]
    public short? Postion
    {
      get;
      set;
    }

    [DataMember]
    public List<long?> RollSetIdList
    {
      get;
      set;
    }

    [DataMember]
    public long? InterCassetteId
    {
      get;
      set;
    }
  }
}
