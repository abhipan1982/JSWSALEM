using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.Models.DataContracts.Internal.PRM
{
  [DataContract]
  [Serializable]
  public class DCSteelFamilyEXT : DataContractBase
  {
    [DataMember] public long SteelFamilyId { get; set; }

    [DataMember] public string SteelFamilyCode { get; set; }

    [DataMember] public string SteelFamilyName { get; set; }

    [DataMember] public string SteelFamilyDescription { get; set; }

    [DataMember] public double WearCoefficient { get; set; }

    [DataMember] public bool IsDefault { get; set; }
  }
}
