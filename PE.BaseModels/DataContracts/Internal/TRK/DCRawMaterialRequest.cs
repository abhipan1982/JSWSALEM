using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  public class DCRawMaterialRequest : DataContractBase
  {
    /// <summary>
    ///   Random token for return message identification
    /// </summary>
    [DataMember]
    public int RequestToken { get; set; }

    [DataMember] public bool IsSimnu { get; set; }
  }
}
