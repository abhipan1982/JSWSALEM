using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.L1A
{
  [DataContract]
  public class DCL1Response : DataContractBase
  {
    [DataMember] public bool SuccessResponse { get; set; }
  }
}
