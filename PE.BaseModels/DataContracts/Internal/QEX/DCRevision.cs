using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.QEX
{
  public class DCRevision : DataContractBase
  {
    [DataMember] public string Revision { get; set; }
  }
}
