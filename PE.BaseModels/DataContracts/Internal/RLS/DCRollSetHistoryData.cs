using System;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.RLS
{
  public class DCRollSetHistoryData : DataContractBase
  {
    [DataMember]
    public long? Id
    {
      get;
      set;
    }

    [DataMember]
    public DateTime? Mounted
    {
      get;
      set;
    }

    [DataMember]
    public DateTime? Dismounted
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
    public long? CassetteId
    {
      get;
      set;
    }

    [DataMember]
    public RollSetHistoryStatus Status
    {
      get;
      set;
    }

    [DataMember]
    public short? PositionInCassette
    {
      get;
      set;
    }

    [DataMember]
    public short? IsThirdRoll
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

    [DataMember]
    public double? AccWeightLimit
    {
      get;
      set;
    }
  }
}
