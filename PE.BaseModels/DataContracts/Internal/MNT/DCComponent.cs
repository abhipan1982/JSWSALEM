using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.MNT
{
  public class DCComponent : DataContractBase
  {
    [DataMember]
    public long ComponentId
    {
      get;
      set;
    }

    [DataMember]
    public string ComponentName
    {
      get;
      set;
    }
  }
}
