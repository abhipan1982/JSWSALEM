using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(MaterialCatalogueTypeName), Name = "UQ_MaterialCatalogueTypeName", IsUnique = true)]
    [Index(nameof(MaterialCatalogueTypeCode), Name = "UQ_MaterialCatalogueTypeSymbol", IsUnique = true)]
    public partial class PRMMaterialCatalogueType
    {
        public PRMMaterialCatalogueType()
        {
            PRMMaterialCatalogues = new HashSet<PRMMaterialCatalogue>();
        }

        [Key]
        public long MaterialCatalogueTypeId { get; set; }
        [Required]
        [StringLength(10)]
        public string MaterialCatalogueTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialCatalogueTypeName { get; set; }
        [StringLength(200)]
        public string MaterialCatalogueTypeDescription { get; set; }
        public bool IsDefault { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(PRMMaterialCatalogue.FKMaterialCatalogueType))]
        public virtual ICollection<PRMMaterialCatalogue> PRMMaterialCatalogues { get; set; }
    }
}
