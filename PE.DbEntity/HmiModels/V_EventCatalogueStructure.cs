using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_EventCatalogueStructure
    {
        [StringLength(10)]
        public string EventCategoryGroupCode { get; set; }
        [StringLength(50)]
        public string EventCategoryGroupName { get; set; }
        [StringLength(10)]
        public string EventCatalogueCategoryCode { get; set; }
        [StringLength(50)]
        public string EventCatalogueCategoryName { get; set; }
        public bool? IsDefaultCategory { get; set; }
        public short EnumAssignmentType { get; set; }
        [StringLength(50)]
        public string AssignmentType { get; set; }
        [Required]
        [StringLength(10)]
        public string EventCatalogueCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EventCatalogueName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefaultCatalogue { get; set; }
        public bool IsPlanned { get; set; }
        public double StdEventTime { get; set; }
        public long? ParentEventCatalogueId { get; set; }
        [StringLength(10)]
        public string ParentDelayCatalogueCode { get; set; }
        [StringLength(50)]
        public string ParentDelayCatalogueName { get; set; }
    }
}
