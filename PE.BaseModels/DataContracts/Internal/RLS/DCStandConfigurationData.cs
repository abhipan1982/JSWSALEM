using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.RLS
{
  public class DCStandConfigurationData : DataContractBase
  {
    [DataMember]
    public long? Id
    {
      get;
      set;
    }
    [DataMember]
    public short? StandNo
    {
      get;
      set;
    }
    [DataMember]
    public short Status
    {
      get;
      set;
    }
    [DataMember]
    public long? MountedCassette
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
    [DataMember]
    public short? Arrangement
    {
      get;
      set;
    }
    [DataMember]
    public string StandBlockName
    {
      get;
      set;
    }

    [DataMember]
    public short? NumberOfRolls
    {
      get;
      set;
    }
    [DataMember]
    public bool? IsCalibrated
    {
      get;
      set;
    }
  }
}
