using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.STP
{
  public class DCFurnaceSetup : DataContractBase
  {
    /// <summary>
    ///   Target heating temperature for billet
    /// </summary>
    [DataMember]
    public double? SetpointTemperature { get; set; }

    /// <summary>
    ///   Heating time at target temperature for billet thickness more or equals than threshold from param (default 136)
    /// </summary>
    [DataMember]
    public int HoldingTime { get; set; }

    /// <summary>
    ///   Heating time at target temperature for billet thickness less than threshold from param (default 136)
    /// </summary>
    [DataMember]
    public int HoldingTimeThin { get; set; }

    /// <summary>
    ///   Is oxidation calculation has to be performed
    /// </summary>
    [DataMember]
    public bool CaclulateOxidation { get; set; }
  }
}
