using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.RLS
{
  public class DCRollGroovesHistoryData : DataContractBase
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
    [DataMember]
    public long GrooveTemplateId
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
    public double AccWeight
    {
      get;
      set;
    }

    [DataMember]
    public double AccWeightWithCoeff
    {
      get;
      set;
    }

    [DataMember]
    public long AccBilletCnt
    {
      get;
      set;
    }
    [DataMember]
    public long RollSetHistoryId
    {
      get;
      set;
    }
    [DataMember]
    public double ActDiameter
    {
      get;
      set;
    }

    [DataMember]
    public string GrooveRemark
    {
      get;
      set;
    }

    [DataMember]
    public short GrooveCondition
    {
      get;
      set;
    }

  }
}
