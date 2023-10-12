using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.L1A
{
  [DataContract]
  public class DCMillControlMessage : DataContractBase
  {
    [DataMember] public object Value { get; set; }
    [DataMember] public int MillControlDataCode { get; set; }
  }
}
