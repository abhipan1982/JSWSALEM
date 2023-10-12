using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKCustomerId), Name = "NCI_CustomerId")]
    [Index(nameof(FKMaterialCatalogueId), Name = "NCI_MaterialCatalogueId")]
    [Index(nameof(FKProductCatalogueId), Name = "NCI_ProductCatalogueId")]
    [Index(nameof(EnumWorkOrderStatus), Name = "NCI_WorkOrderStatus")]
    [Index(nameof(WorkOrderName), Name = "UQ_WorkOrderName", IsUnique = true)]
    public partial class PRMWorkOrder
    {
        public PRMWorkOrder()
        {
            EVTEvents = new HashSet<EVTEvent>();
            InverseFKParentWorkOrder = new HashSet<PRMWorkOrder>();
            PRFKPIValues = new HashSet<PRFKPIValue>();
            PRMMaterials = new HashSet<PRMMaterial>();
            PRMProducts = new HashSet<PRMProduct>();
            STPSetupSents = new HashSet<STPSetupSent>();
            STPSetupWorkOrders = new HashSet<STPSetupWorkOrder>();
        }

        [Key]
        public long WorkOrderId { get; set; }
        public long FKProductCatalogueId { get; set; }
        public long FKMaterialCatalogueId { get; set; }
        public long FKSteelgradeId { get; set; }
        public long? FKHeatId { get; set; }
        public long? FKCustomerId { get; set; }
        public long? FKParentWorkOrderId { get; set; }
        public WorkOrderStatus EnumWorkOrderStatus { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WorkOrderCreatedInL3Ts { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WorkOrderCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WorkOrderStartTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WorkOrderEndTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ToBeCompletedBeforeTs { get; set; }
        public bool IsTestOrder { get; set; }
        public bool IsSentToL3 { get; set; }
        public bool IsBlocked { get; set; }
        public double TargetOrderWeight { get; set; }
        public double? TargetOrderWeightMin { get; set; }
        public double? TargetOrderWeightMax { get; set; }
        public double? BundleWeightMin { get; set; }
        public double? BundleWeightMax { get; set; }
        public short L3NumberOfBillets { get; set; }
        [StringLength(50)]
        public string ExternalWorkOrderName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKCustomerId))]
        [InverseProperty(nameof(PRMCustomer.PRMWorkOrders))]
        public virtual PRMCustomer FKCustomer { get; set; }
        [ForeignKey(nameof(FKHeatId))]
        [InverseProperty(nameof(PRMHeat.PRMWorkOrders))]
        public virtual PRMHeat FKHeat { get; set; }
        [ForeignKey(nameof(FKMaterialCatalogueId))]
        [InverseProperty(nameof(PRMMaterialCatalogue.PRMWorkOrders))]
        public virtual PRMMaterialCatalogue FKMaterialCatalogue { get; set; }
        [ForeignKey(nameof(FKParentWorkOrderId))]
        [InverseProperty(nameof(PRMWorkOrder.InverseFKParentWorkOrder))]
        public virtual PRMWorkOrder FKParentWorkOrder { get; set; }
        [ForeignKey(nameof(FKProductCatalogueId))]
        [InverseProperty(nameof(PRMProductCatalogue.PRMWorkOrders))]
        public virtual PRMProductCatalogue FKProductCatalogue { get; set; }
        [ForeignKey(nameof(FKSteelgradeId))]
        [InverseProperty(nameof(PRMSteelgrade.PRMWorkOrders))]
        public virtual PRMSteelgrade FKSteelgrade { get; set; }
        [InverseProperty("FKWorkOrder")]
        public virtual PPLSchedule PPLSchedule { get; set; }
        [InverseProperty(nameof(EVTEvent.FKWorkOrder))]
        public virtual ICollection<EVTEvent> EVTEvents { get; set; }
        [InverseProperty(nameof(PRMWorkOrder.FKParentWorkOrder))]
        public virtual ICollection<PRMWorkOrder> InverseFKParentWorkOrder { get; set; }
        [InverseProperty(nameof(PRFKPIValue.FKWorkOrder))]
        public virtual ICollection<PRFKPIValue> PRFKPIValues { get; set; }
        [InverseProperty(nameof(PRMMaterial.FKWorkOrder))]
        public virtual ICollection<PRMMaterial> PRMMaterials { get; set; }
        [InverseProperty(nameof(PRMProduct.FKWorkOrder))]
        public virtual ICollection<PRMProduct> PRMProducts { get; set; }
        [InverseProperty(nameof(STPSetupSent.FKWorkOrder))]
        public virtual ICollection<STPSetupSent> STPSetupSents { get; set; }
        [InverseProperty(nameof(STPSetupWorkOrder.FKWorkOrder))]
        public virtual ICollection<STPSetupWorkOrder> STPSetupWorkOrders { get; set; }
    }
}
