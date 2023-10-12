using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(AssetTypeCode), Name = "UQ_AssetTypeCode", IsUnique = true)]
    public partial class MVHAssetType
    {
        public MVHAssetType()
        {
            MVHAssets = new HashSet<MVHAsset>();
        }

        [Key]
        public long AssetTypeId { get; set; }
        [Required]
        [StringLength(10)]
        public string AssetTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetTypeName { get; set; }
        [StringLength(200)]
        public string AssetTypeDescription { get; set; }
        public YardType EnumYardType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(MVHAsset.FKAssetType))]
        public virtual ICollection<MVHAsset> MVHAssets { get; set; }
    }
}
