using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.RLS
{
  public class DCCassetteTypeData : DataContractBase
  {
    [DataMember]
    public long? Id
    {
      get;
      set;
    }

    [DataMember]
    public string CassetteTypeName
    {
      get;
      set;
    }

    [DataMember]
    public string Description
    {
      get;
      set;
    }

    [DataMember]
    public short? NumberOfRolls
    {
      get;
      set;
    }
  }
}
