using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.L1A
{
  [DataContract]
  public class DCL1Message : DataContractBase
  {
    [DataMember] public bool HeatChanged { get; set; }
  }
}
