using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.Models.DataContracts.Internal.MDA
{
  public class DCHelloMessage : DataContractBase
  {
    [DataMember] public string Text { get; set; }
  }
}
