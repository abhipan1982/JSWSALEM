using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.MNT
{
  public class DCDevice : DataContractBase
  {
    [DataMember]
    public long DeviceId
    {
      get;
      set;
    }

    [DataMember]
    public string DeviceName
    {
      get;
      set;
    }
  }
}
