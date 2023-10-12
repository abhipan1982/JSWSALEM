using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.RLS
{
  public class DCRollSetAccumulationData : DataContractBase
  {
    [DataMember]
    public long BilletId
    {
      get;
      set;
    }

    [DataMember]
    public string StandIds
    {
      get;
      set;
    }

    [DataMember]
    public short GrooveNo
    {
      get;
      set;
    }
  }
}
