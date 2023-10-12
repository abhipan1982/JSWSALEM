using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKWorkOrderId), Name = "NCI_WorkOrderId")]
    [Index(nameof(ProductName), Name = "UQ_ProductName", IsUnique = true)]
    public partial class PRMProduct
    {
        public PRMProduct()
        {
            PRMProductSteps = new HashSet<PRMProductStep>();
            QTYDefects = new HashSet<QTYDefect>();
            QTYQualityInspections = new HashSet<QTYQualityInspection>();
            TRKRawMaterials = new HashSet<TRKRawMaterial>();
        }

        [Key]
        public long ProductId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ProductCreatedTs { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        public double ProductWeight { get; set; }
        public short BarsCounter { get; set; }
        public WeightSource EnumWeightSource { get; set; }
        public bool IsDummy { get; set; }
        public bool IsAssigned { get; set; }
        public long? FKWorkOrderId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKWorkOrderId))]
        [InverseProperty(nameof(PRMWorkOrder.PRMProducts))]
        public virtual PRMWorkOrder FKWorkOrder { get; set; }
        [InverseProperty(nameof(PRMProductStep.FKProduct))]
        public virtual ICollection<PRMProductStep> PRMProductSteps { get; set; }
        [InverseProperty(nameof(QTYDefect.FKProduct))]
        public virtual ICollection<QTYDefect> QTYDefects { get; set; }
        [InverseProperty(nameof(QTYQualityInspection.FKProduct))]
        public virtual ICollection<QTYQualityInspection> QTYQualityInspections { get; set; }
        [InverseProperty(nameof(TRKRawMaterial.FKProduct))]
        public virtual ICollection<TRKRawMaterial> TRKRawMaterials { get; set; }
    }
}
