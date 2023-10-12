using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  public class DCRejectMaterialData : DataContractBase
  {
    /// <summary>
    ///   Billet unique identification
    /// </summary>
    [DataMember]
    public long RawMaterialId { get; set; }

    /// <summary>
    ///   Location of billet reject action
    /// </summary>
    [DataMember]
    public RejectLocation RejectLocation { get; set; }

    /// <summary>
    ///   Number of output pieces
    /// </summary>
    [DataMember]
    public short OutputPieces { get; set; }
  }
}
