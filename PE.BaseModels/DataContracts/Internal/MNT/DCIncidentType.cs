using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.MNT
{
  public class DCIncidentType : DataContractBase
  {
    [DataMember]
    public long IncidentTypeId
    {
      get;
      set;
    }

    [DataMember]
    public string IncidentTypeName
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
    public string IncidentTypeCode
    {
      get;
      set;
    }

    [DataMember]
    public string IncidentTypeDescription
    {
      get;
      set;
    }

    [DataMember]
    public short? DefaultEnumSeverity
    {
      get;
      set;
    }
  }
}
