using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKDefectCatalogueId), Name = "NCI_DefectCatalogueId")]
    [Index(nameof(FKRawMaterialId), Name = "NCI_RawMaterialId")]
    public partial class QTYDefect
    {
        [Key]
        public long DefectId { get; set; }
        public long FKDefectCatalogueId { get; set; }
        public long? FKRawMaterialId { get; set; }
        public long? FKProductId { get; set; }
        public long? FKAssetId { get; set; }
        [StringLength(50)]
        public string DefectName { get; set; }
        [StringLength(200)]
        public string DefectDescription { get; set; }
        public short? DefectPosition { get; set; }
        public short? DefectFrequency { get; set; }
        public short? DefectScale { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DefectCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKAssetId))]
        [InverseProperty(nameof(MVHAsset.QTYDefects))]
        public virtual MVHAsset FKAsset { get; set; }
        [ForeignKey(nameof(FKDefectCatalogueId))]
        [InverseProperty(nameof(QTYDefectCatalogue.QTYDefects))]
        public virtual QTYDefectCatalogue FKDefectCatalogue { get; set; }
        [ForeignKey(nameof(FKProductId))]
        [InverseProperty(nameof(PRMProduct.QTYDefects))]
        public virtual PRMProduct FKProduct { get; set; }
        [ForeignKey(nameof(FKRawMaterialId))]
        [InverseProperty(nameof(TRKRawMaterial.QTYDefects))]
        public virtual TRKRawMaterial FKRawMaterial { get; set; }
    }
}
