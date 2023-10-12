using System;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.PRM
{
  public class DCWorkOrder : DataContractBase
  {
    [DataMember] public long WorkOrderId { get; set; }

    [DataMember] public string WorkOrderName { get; set; }

    [DataMember] public bool IsTestOrder { get; set; }

    [DataMember] public double TargetOrderWeight { get; set; }

    [DataMember] public double? TargetOrderWeightMin { get; set; }

    [DataMember] public double? TargetOrderWeightMax { get; set; }

    [DataMember] public DateTime CreatedInL3Ts { get; set; }

    [DataMember] public DateTime ToBeCompletedBeforeTs { get; set; }


    [DataMember] public long? FKHeatId { get; set; }

    [DataMember] public long FKSteelgradeId { get; set; }

    [DataMember] public long FKProductCatalogueId { get; set; }

    [DataMember] public long FKMaterialCatalogueId { get; set; }

    [DataMember] public long? FKCustomerId { get; set; }

    [DataMember] public long? MaterialsNumber { get; set; }

    [DataMember] public WorkOrderStatus WorkOrderStatus { get; set; }
    [DataMember] public double? BundleWeightMin { get; set; }
    [DataMember] public double? BundleWeightMax { get; set; }
  }
}
