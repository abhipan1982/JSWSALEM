using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.MNT
{
  public class DCActionType : DataContractBase
  {
    [DataMember]
    public long ActionTypeId
    {
      get;
      set;
    }

    [DataMember]
    public string ActionTypeName
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

    [DataMember]
    public string ActionTypeCode
    {
      get;
      set;
    }

    [DataMember]
    public string ActionTypeDescription
    {
      get;
      set;
    }
  }
}
