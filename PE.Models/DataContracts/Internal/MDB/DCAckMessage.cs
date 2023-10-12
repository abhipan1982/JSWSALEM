using System.Runtime.Serialization;
using SMF.Core.DC;


namespace PE.Models.DataContracts.Internal.MDB 
{ 
  public class DCAckMessage : DataContractBase
  {
    [DataMember] public bool Ok { get; set; }
  }
}
