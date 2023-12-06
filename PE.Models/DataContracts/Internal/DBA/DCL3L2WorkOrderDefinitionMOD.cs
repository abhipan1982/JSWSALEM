using System;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.Models.DataContracts.Internal.DBA
{
  public class DCL3L2WorkOrderDefinitionMOD : DataContractBase
  {
    /// <summary>
    ///   Primary Key in transfer table
    /// </summary>
    [DataMember]
    public long Counter { get; set; }

    /// <summary>
    ///   0 - new record, 1 - processed and OK, 2 - processed and Error, 3 - processed and rejected
    /// </summary>
    [DataMember]
    public CommStatus CommStatus { get; set; }

    /// <summary>
    ///   Unique work order name
    /// </summary>
    [DataMember]
    public string WorkOrderName { get; set; }

    /// <summary>
    ///   Customer name
    /// </summary>
    [DataMember]
    public string CustomerName { get; set; }

    /// <summary>
    ///   Heat name
    /// </summary>
    [DataMember]
    public string HeatName { get; set; }

    /// <summary>
    ///   temp flag to simulator L3 - can and should be removed
    /// </summary>
    [DataMember]
    public bool AmISimulated { get; set; }

    [DataMember] public DateTime CreatedTs { get; set; }

    [DataMember] public string ExternalWorkOrderName { get; set; }
    [DataMember] public string PreviousWorkOrderName { get; set; }
    [DataMember] public string OrderDeadline { get; set; }
    [DataMember] public string BilletWeight { get; set; }
    [DataMember] public string NumberOfBillets { get; set; }
    [DataMember] public string BundleWeightMin { get; set; }
    [DataMember] public string BundleWeightMax { get; set; }
    [DataMember] public string TargetWorkOrderWeight { get; set; }
    [DataMember] public string TargetWorkOrderWeightMin { get; set; }
    [DataMember] public string TargetWorkOrderWeightMax { get; set; }
    [DataMember] public string MaterialCatalogueName { get; set; }
    [DataMember] public string ProductCatalogueName { get; set; }
    [DataMember] public string SteelgradeCode { get; set; }
    [DataMember] public string InputThickness { get; set; }
    [DataMember] public string InputWidth { get; set; }
    [DataMember] public string BilletLength { get; set; }
    [DataMember] public string InputShapeSymbol { get; set; }
    [DataMember] public string OutputThickness { get; set; }
    [DataMember] public string OutputWidth { get; set; }
    [DataMember] public string OutputShapeSymbol { get; set; }
  }
}
