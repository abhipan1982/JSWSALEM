using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.RLS
{
  public class DCRollsAccu : DataContractBase
  {
    [DataMember]
    public double MaterialWeight
    {
      get;
      set;
    }

    public double MaterialWeightWithCoeff
    {
      get;
      set;
    }
  }
}
