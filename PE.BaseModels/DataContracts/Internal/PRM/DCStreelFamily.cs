using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.PRM
{
  public class DCSteelFamily : DataContractBase
  {
    [DataMember] public long SteelFamilyId { get; set; }

    [DataMember] public string SteelFamilyCode { get; set; }

    [DataMember] public string SteelFamilyName { get; set; }

    [DataMember] public string SteelFamilyDescription { get; set; }

    [DataMember] public double WearCoefficient { get; set; }

    [DataMember] public bool IsDefault { get; set; }
  }
}
