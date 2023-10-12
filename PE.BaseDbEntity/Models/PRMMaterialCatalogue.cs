using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("PRMMaterialCatalogue")]
    [Index(nameof(FKMaterialCatalogueTypeId), Name = "NCI_MaterialCatalogueTypeId")]
    [Index(nameof(FKShapeId), Name = "NCI_ShapeId")]
    [Index(nameof(MaterialCatalogueName), Name = "UQ_MaterialCatalogueName", IsUnique = true)]
    public partial class PRMMaterialCatalogue
    {
        public PRMMaterialCatalogue()
        {
            PRMMaterials = new HashSet<PRMMaterial>();
            PRMWorkOrders = new HashSet<PRMWorkOrder>();
        }

        [Key]
        public long MaterialCatalogueId { get; set; }
        public long FKMaterialCatalogueTypeId { get; set; }
        public long FKShapeId { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public bool IsDefault { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialCatalogueName { get; set; }
        [StringLength(200)]
        public string MaterialCatalogueDescription { get; set; }
        [StringLength(50)]
        public string ExternalMaterialCatalogueName { get; set; }
        public double? LengthMin { get; set; }
        public double? LengthMax { get; set; }
        public double ThicknessMin { get; set; }
        public double ThicknessMax { get; set; }
        public double? WidthMin { get; set; }
        public double? WidthMax { get; set; }
        public double? WeightMin { get; set; }
        public double? WeightMax { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKMaterialCatalogueTypeId))]
        [InverseProperty(nameof(PRMMaterialCatalogueType.PRMMaterialCatalogues))]
        public virtual PRMMaterialCatalogueType FKMaterialCatalogueType { get; set; }
        [ForeignKey(nameof(FKShapeId))]
        [InverseProperty(nameof(PRMShape.PRMMaterialCatalogues))]
        public virtual PRMShape FKShape { get; set; }
        [InverseProperty(nameof(PRMMaterial.FKMaterialCatalogue))]
        public virtual ICollection<PRMMaterial> PRMMaterials { get; set; }
        [InverseProperty(nameof(PRMWorkOrder.FKMaterialCatalogue))]
        public virtual ICollection<PRMWorkOrder> PRMWorkOrders { get; set; }
    }
}
