using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.MNT
{
  public class DCComponentGroup : DataContractBase
  {
    [DataMember]
    public long ComponentGroupId
    {
      get;
      set;
    }

    [DataMember]
    public string ComponentGroupName
    {
      get;
      set;
    }

    [DataMember]
    public string ComponentGroupCode
    {
      get;
      set;
    }

    [DataMember]
    public string ComponentGroupDescription
    {
      get;
      set;
    }

    [DataMember]
    public long? ParentComponentGroupId
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
