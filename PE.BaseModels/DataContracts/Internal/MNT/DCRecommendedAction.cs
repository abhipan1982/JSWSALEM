using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.MNT
{
  public class DCRecommendedAction : DataContractBase
  {
    [DataMember]
    public long RecommendedActionId
    {
      get;
      set;
    }

    [DataMember]
    public string ActionDescription
    {
      get;
      set;
    }

    [DataMember]
    public long? FkActionTypeId
    {
      get;
      set;
    }

    [DataMember]
    public long? FkIncidentTypeId
    {
      get;
      set;
    }
  }
}
