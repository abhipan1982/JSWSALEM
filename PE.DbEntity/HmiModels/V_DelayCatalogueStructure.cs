using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DelayCatalogueStructure
    {
        [StringLength(10)]
        public string DelayCategoryGroupCode { get; set; }
        [StringLength(50)]
        public string DelayCategoryGroupName { get; set; }
        [StringLength(10)]
        public string DelayCatalogueCategoryCode { get; set; }
        [StringLength(50)]
        public string DelayCatalogueCategoryName { get; set; }
        public bool? IsDefaultCategory { get; set; }
        public short EnumAssignmentType { get; set; }
        [StringLength(50)]
        public string AssignmentType { get; set; }
        [Required]
        [StringLength(10)]
        public string DelayCatalogueCode { get; set; }
        [Required]
        [StringLength(50)]
        public string DelayCatalogueName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefaultCatalogue { get; set; }
        public bool IsPlanned { get; set; }
        public double StdDelayTime { get; set; }
        public long? ParentDelayCatalogueId { get; set; }
        [StringLength(10)]
        public string ParentDelayCatalogueCode { get; set; }
        [StringLength(50)]
        public string ParentDelayCatalogueName { get; set; }
    }
}
