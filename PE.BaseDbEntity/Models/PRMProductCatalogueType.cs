using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(ProductCatalogueTypeCode), Name = "UQ_ProductCatalogueTypeSymbol", IsUnique = true)]
    [Index(nameof(ProductCatalogueTypeName), Name = "UQ_ProductCatalogueTypesName", IsUnique = true)]
    public partial class PRMProductCatalogueType
    {
        public PRMProductCatalogueType()
        {
            PRMProductCatalogues = new HashSet<PRMProductCatalogue>();
        }

        [Key]
        public long ProductCatalogueTypeId { get; set; }
        [Required]
        [StringLength(10)]
        public string ProductCatalogueTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCatalogueTypeName { get; set; }
        [StringLength(200)]
        public string ProductCatalogueTypeDescription { get; set; }
        public bool IsDefault { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(PRMProductCatalogue.FKProductCatalogueType))]
        public virtual ICollection<PRMProductCatalogue> PRMProductCatalogues { get; set; }
    }
}
