using System.Collections.Generic;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseModels.Structures.RLS;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.RLS
{
  public class DCRollSetGrooveSetup : DataContractBase
  {
    [DataMember]
    public long? Id
    {
      get;
      set;
    }

    [DataMember]
    public GrindingTurningAction Action
    {
      get;
      set;
    }

    [DataMember]
    public short? RollSetType
    {
      get;
      set;
    }

    [DataMember]
    public List<SingleGrooveSetup> GrooveSetupList
    {
      get;
      set;
    }

    [DataMember]
    public List<SingleRollDataInfo> RollDataInfoList
    {
      get;
      set;
    }
  }
}
