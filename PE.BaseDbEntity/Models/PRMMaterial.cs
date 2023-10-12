using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKHeatId), Name = "NCI_FKHeatId")]
    [Index(nameof(FKWorkOrderId), Name = "NCI_FKWorkOrderId")]
    [Index(nameof(MaterialName), Name = "UQ_MaterialName", IsUnique = true)]
    public partial class PRMMaterial
    {
        public PRMMaterial()
        {
            PRMMaterialSteps = new HashSet<PRMMaterialStep>();
            QTYQualityInspections = new HashSet<QTYQualityInspection>();
            TRKRawMaterials = new HashSet<TRKRawMaterial>();
        }

        [Key]
        public long MaterialId { get; set; }
        public short SeqNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime MaterialCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MaterialStartTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MaterialEndTs { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialName { get; set; }
        public double MaterialWeight { get; set; }
        public double MaterialThickness { get; set; }
        public double? MaterialWidth { get; set; }
        public double? MaterialLength { get; set; }
        public bool IsDummy { get; set; }
        public bool IsAssigned { get; set; }
        public long FKHeatId { get; set; }
        public long? FKWorkOrderId { get; set; }
        public long? FKMaterialCatalogueId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKHeatId))]
        [InverseProperty(nameof(PRMHeat.PRMMaterials))]
        public virtual PRMHeat FKHeat { get; set; }
        [ForeignKey(nameof(FKMaterialCatalogueId))]
        [InverseProperty(nameof(PRMMaterialCatalogue.PRMMaterials))]
        public virtual PRMMaterialCatalogue FKMaterialCatalogue { get; set; }
        [ForeignKey(nameof(FKWorkOrderId))]
        [InverseProperty(nameof(PRMWorkOrder.PRMMaterials))]
        public virtual PRMWorkOrder FKWorkOrder { get; set; }
        [InverseProperty(nameof(PRMMaterialStep.FKMaterial))]
        public virtual ICollection<PRMMaterialStep> PRMMaterialSteps { get; set; }
        [InverseProperty(nameof(QTYQualityInspection.FKMaterial))]
        public virtual ICollection<QTYQualityInspection> QTYQualityInspections { get; set; }
        [InverseProperty(nameof(TRKRawMaterial.FKMaterial))]
        public virtual ICollection<TRKRawMaterial> TRKRawMaterials { get; set; }
    }
}
