using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("PRMProductCatalogue")]
    [Index(nameof(FKProductCatalogueTypeId), Name = "NCI_ProductCatalogueTypeId")]
    [Index(nameof(FKShapeId), Name = "NCI_ShapeId")]
    [Index(nameof(ProductCatalogueName), Name = "UQ_ProductCatalogueName", IsUnique = true)]
    public partial class PRMProductCatalogue
    {
        public PRMProductCatalogue()
        {
            PRMWorkOrders = new HashSet<PRMWorkOrder>();
            STPProductLayouts = new HashSet<STPProductLayout>();
        }

        [Key]
        public long ProductCatalogueId { get; set; }
        public long FKProductCatalogueTypeId { get; set; }
        public long FKShapeId { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public bool IsDefault { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        [StringLength(200)]
        public string ProductCatalogueDescription { get; set; }
        [StringLength(50)]
        public string ExternalProductCatalogueName { get; set; }
        public double? Length { get; set; }
        public double? LengthMin { get; set; }
        public double? LengthMax { get; set; }
        public double Thickness { get; set; }
        public double ThicknessMin { get; set; }
        public double ThicknessMax { get; set; }
        public double? Width { get; set; }
        public double? WidthMin { get; set; }
        public double? WidthMax { get; set; }
        public double? Weight { get; set; }
        public double? WeightMin { get; set; }
        public double? WeightMax { get; set; }
        public double? MaxOvality { get; set; }
        public double StdProductivity { get; set; }
        public double StdMetallicYield { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKProductCatalogueTypeId))]
        [InverseProperty(nameof(PRMProductCatalogueType.PRMProductCatalogues))]
        public virtual PRMProductCatalogueType FKProductCatalogueType { get; set; }
        [ForeignKey(nameof(FKShapeId))]
        [InverseProperty(nameof(PRMShape.PRMProductCatalogues))]
        public virtual PRMShape FKShape { get; set; }
        [InverseProperty(nameof(PRMWorkOrder.FKProductCatalogue))]
        public virtual ICollection<PRMWorkOrder> PRMWorkOrders { get; set; }
        [InverseProperty(nameof(STPProductLayout.FKProductCatalogue))]
        public virtual ICollection<STPProductLayout> STPProductLayouts { get; set; }
    }
}
