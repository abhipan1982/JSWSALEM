using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  public class DCL1CutData : DataContractBase
  {
    /// <summary>
    ///   Billet unique identification
    /// </summary>
    [DataMember]
    public long RawMaterialId { get; set; }

    /// <summary>
    ///   Type of cut:
    ///   6: head cut
    ///   8: tail cut
    ///   ??: sample cut
    /// </summary>
    [DataMember]
    public TypeOfCut TypeOfCut { get; set; }

    /// <summary>
    ///   Length of cut
    /// </summary>
    [DataMember]
    public double CutLength { get; set; }

    /// <summary>
    ///   Unique location Id
    /// </summary>
    [DataMember]
    public int AssetCode { get; set; }

    /// <summary>
    ///   EventType
    /// </summary>
    [DataMember]
    public EventType EventType { get; set; }
  }
}
