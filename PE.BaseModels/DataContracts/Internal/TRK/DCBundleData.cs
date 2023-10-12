using System.Runtime.Serialization;
using System;
using SMF.Core.DC;
using PE.BaseDbEntity.EnumClasses;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  public class DCBundleData : DataContractBase
  {
    [DataMember] public long? WorkOrderId { get; set; }

    [DataMember] public double OverallWeight { get; set; }

    [DataMember] public short BarsCounter { get; set; }

    [DataMember] public WeightSource BundleWeightSource { get; set; }

    [DataMember] public DateTime Date { get; set; }

    [DataMember] public bool KeepInTracking { get; set; }
  }
}
