using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(ShapeCode), Name = "UQ_ShapeSymbol", IsUnique = true)]
    public partial class PRMShape
    {
        public PRMShape()
        {
            PRMMaterialCatalogues = new HashSet<PRMMaterialCatalogue>();
            PRMProductCatalogues = new HashSet<PRMProductCatalogue>();
        }

        [Key]
        public long ShapeId { get; set; }
        [Required]
        [StringLength(10)]
        public string ShapeCode { get; set; }
        [StringLength(50)]
        public string ShapeName { get; set; }
        [StringLength(200)]
        public string ShapeDescription { get; set; }
        public bool IsDefault { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(PRMMaterialCatalogue.FKShape))]
        public virtual ICollection<PRMMaterialCatalogue> PRMMaterialCatalogues { get; set; }
        [InverseProperty(nameof(PRMProductCatalogue.FKShape))]
        public virtual ICollection<PRMProductCatalogue> PRMProductCatalogues { get; set; }
    }
}
