using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimDefectCatalogue
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimDefectCatalogueRow { get; set; }
        public bool DimDefectCatalogueIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimDefectCatalogueHash { get; set; }
        public long DimDefectCatalogueKey { get; set; }
        public long? DimDefectCatalogueKeyParent { get; set; }
        [Required]
        [StringLength(10)]
        public string DefectCatalogueCode { get; set; }
        [Required]
        [StringLength(50)]
        public string DefectCatalogueName { get; set; }
        [StringLength(200)]
        public string DefectCatalogueDescription { get; set; }
        [Required]
        [StringLength(10)]
        public string DefectCategoryCode { get; set; }
        [StringLength(50)]
        public string DefectCategoryName { get; set; }
        [StringLength(200)]
        public string DefectCategoryDescription { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string DefectCategoryAssignmentType { get; set; }
        [StringLength(10)]
        public string DefectGroupCode { get; set; }
        [StringLength(50)]
        public string DefectGroupName { get; set; }
        [StringLength(2000)]
        public string DefectGroupDescription { get; set; }
        [StringLength(10)]
        public string DefectCatalogueCodeParent { get; set; }
        [StringLength(50)]
        public string DefectCatalogueNameParent { get; set; }
        [StringLength(200)]
        public string DefectCatalogueDescriptionParent { get; set; }
    }
}
