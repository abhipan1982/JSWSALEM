using System;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.QTY
{
  public class DCRawMaterialQuality : DataContractBase
  {
    [DataMember] public long RawMaterialId { get; set; }

    [DataMember] public double? DiameterMin { get; set; }

    [DataMember] public double? DiameterMax { get; set; }

    [DataMember] public CrashTest CrashTest { get; set; }

    [DataMember] public string VisualInspection { get; set; }

    [DataMember] public InspectionResult InspectionResult { get; set; }

  }
}
