using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.MNT
{
  public class DCIncident : DataContractBase
  {
    [DataMember]
    public long IncidentId
    {
      get;
      set;
    }

    [DataMember]
    public string IncidentName
    {
      get;
      set;
    }
  }
}
