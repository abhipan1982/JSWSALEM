using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_EventsStructureSearchGrid
    {
        public long EventTypeId { get; set; }
        public short EventTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EventTypeName { get; set; }
        public long? ParentEventTypeId { get; set; }
        public short? ParentEventTypeCode { get; set; }
        [StringLength(50)]
        public string ParentEventTypeName { get; set; }
        public long? EventCategoryGroupId { get; set; }
        [StringLength(10)]
        public string EventCategoryGroupCode { get; set; }
        [StringLength(50)]
        public string EventCategoryGroupName { get; set; }
        public long? EventCatalogueCategoryId { get; set; }
        [StringLength(10)]
        public string EventCatalogueCategoryCode { get; set; }
        [StringLength(50)]
        public string EventCatalogueCategoryName { get; set; }
        public bool? IsDefaultCategory { get; set; }
        public long? EventCatalogueId { get; set; }
        [StringLength(10)]
        public string EventCatalogueCode { get; set; }
        [StringLength(50)]
        public string EventCatalogueName { get; set; }
        public bool? IsActiveCatalogue { get; set; }
        public bool? IsDefaultCatalogue { get; set; }
        public long? ParentEventCatalogueId { get; set; }
        [StringLength(10)]
        public string ParentEventCatalogueCode { get; set; }
        [StringLength(50)]
        public string ParentEventCatalogueName { get; set; }
    }
}
