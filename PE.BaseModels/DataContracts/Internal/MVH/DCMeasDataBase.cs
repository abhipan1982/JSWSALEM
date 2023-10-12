using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.MVH
{
  public class DCMeasDataBase : DataContractBase
  {
    /// <summary>
    ///   Billet unique identification
    /// </summary>
    [DataMember]
    public long RawMaterialId { get; set; }
  }
}
