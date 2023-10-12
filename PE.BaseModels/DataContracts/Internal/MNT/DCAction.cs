using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.MNT
{
  public class DCAction : DataContractBase
  {
    [DataMember]
    public long ActionId
    {
      get;
      set;
    }

    [DataMember]
    public string ActionName
    {
      get;
      set;
    }
  }
}
