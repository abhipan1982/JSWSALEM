using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.RLS
{
  public class DCRollGroovesHistoryStatusData : DataContractBase
  {
    [DataMember]
    public long Id
    {
      get;
      set;
    }

    [DataMember]
    public short GrooveNumber
    {
      get;
      set;
    }

    [DataMember]
    public long RollId
    {
      get;
      set;
    }

    //[DataMember]
    //public long GrooveTemplateId
    //{
    //	get;
    //	set;
    //}
    [DataMember]
    public short GrooveHistoryStatus
    {
      get;
      set;
    }

    //[DataMember]
    //public float AccWeight
    //{
    //	get;
    //	set;
    //}
    //[DataMember]
    //public long AccBilletCnt
    //{
    //	get;
    //	set;
    //}
    [DataMember]
    public long RollSetHistoryId
    {
      get;
      set;
    }
    //[DataMember]
    //public float ActDiameter
    //{
    //	get;
    //	set;
    //}
  }
}
