using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.MNT
{
  public class DCDeviceGroup : DataContractBase
  {
    [DataMember]
    public long DeviceGroupId
    {
      get;
      set;
    }

    [DataMember]
    public string DeviceGroupName
    {
      get;
      set;
    }

    [DataMember]
    public string DeviceGroupCode
    {
      get;
      set;
    }

    [DataMember]
    public string DeviceGroupDescription
    {
      get;
      set;
    }

    [DataMember]
    public long? ParentDeviceGroupId
    {
      get;
      set;
    }

    [DataMember]
    public DateTime? CreatedTs
    {
      get;
      set;
    }

    [DataMember]
    public DateTime? LastUpdateTs
    {
      get;
      set;
    }
  }
}
