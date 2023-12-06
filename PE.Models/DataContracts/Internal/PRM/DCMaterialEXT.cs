using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.Models.DataContracts.Internal.PRM
{
  [DataContract]
  [Serializable]
  public class DCMaterialEXT : DataContractBase
  {
    [DataMember] public string FKWorkOrderIdRef { get; set; }

    [DataMember] public double Weight { get; set; }

    [DataMember] public long MaterialsNumber { get; set; }

    [DataMember] public long FKHeatId { get; set; }

    [DataMember] public bool IsTestOrder { get; set; }

    [DataMember] public long FKMaterialCatalogueId { get; set; }
  }
}
