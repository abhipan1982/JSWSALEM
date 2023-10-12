using System;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.RLS
{
  public class DCCassetteData : DataContractBase
  {
    [DataMember]
    public long? Id
    {
      get;
      set;
    }
    [DataMember]
    public string CassetteName
    {
      get;
      set;
    }
    [DataMember]
    public CassetteStatus? Status
    {
      get;
      set;
    }
    [DataMember]
    public long? CassetteTypeId
    {
      get;
      set;
    }
    [DataMember]
    public short? NumberOfPositions
    {
      get;
      set;
    }
    [DataMember]
    public long? StandId
    {
      get;
      set;
    }
    [DataMember]
    public CassetteArrangement? Arrangement
    {
      get;
      set;
    }
    [DataMember]
    public DateTime? MountedDate
    {
      get;
      set;
    }
    [DataMember]
    public DateTime? DismountedDate
    {
      get;
      set;
    }
  }
}
