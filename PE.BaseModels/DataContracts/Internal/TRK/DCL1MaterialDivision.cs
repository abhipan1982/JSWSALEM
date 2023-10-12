using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  public class DCL1MaterialDivision : DataContractBase
  {
    /// <summary>
    ///   Billet basic automation ID
    /// </summary>
    [DataMember]
    public long ParentBasId { get; set; }

    /// <summary>
    ///   Random token for return message identification
    /// </summary>
    [DataMember]
    public int RequestToken { get; set; }

    /// <summary>
    ///   Length of new material
    ///   Unit meters
    /// </summary>
    [DataMember]
    public double NewMaterialLength { get; set; }

    /// <summary>
    ///   1st cut = 1
    ///   2nd cut = 2
    ///   ...
    /// </summary>
    [DataMember]
    public int CutNumberInParent { get; set; }

    /// <summary>
    ///   Unique location Id
    /// </summary>
    [DataMember]
    public int LocationId { get; set; }

    [DataMember] public short SlittingFactor { get; set; }
  }
}
