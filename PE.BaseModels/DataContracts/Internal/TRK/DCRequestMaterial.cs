using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  public class DCRequestMaterial : DataContractBase
  {
    /// <summary>
    ///   Material id for basic automation
    /// </summary>
    [DataMember]
    public long Id { get; set; }

    /// <summary>
    ///   Random token sent by L1 in request telegram
    /// </summary>
    [DataMember]
    public int RequestToken { get; set; }

    /// <summary>
    ///   divide flag
    /// </summary>
    [DataMember]
    public bool IsDivide { get; set; }
  }
}
