using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimEventCatalogue
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimEventCatalogueRow { get; set; }
        public bool DimEventCatalogueIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimEventCatalogueHash { get; set; }
        public long DimEventCatalogueKey { get; set; }
        public long DimEventTypeKey { get; set; }
        public long DimEventCategoryKey { get; set; }
        public long? DimEventGroupKey { get; set; }
        public long? DimEventCatalogueKeyParent { get; set; }
        public int EventIsDelay { get; set; }
        public short EventTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EventTypeName { get; set; }
        [StringLength(100)]
        public string EventTypeDescription { get; set; }
        [Required]
        [StringLength(10)]
        public string EventCatalogueCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EventCatalogueName { get; set; }
        [StringLength(100)]
        public string EventCatalogueDescription { get; set; }
        public double EventStdTime { get; set; }
        public bool EventIsPlanned { get; set; }
        [Required]
        [StringLength(10)]
        public string EventCategoryCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EventCategoryName { get; set; }
        [StringLength(100)]
        public string EventCategoryDescription { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string EventCategoryAssignmentType { get; set; }
        [StringLength(10)]
        public string EventGroupCode { get; set; }
        [StringLength(50)]
        public string EventGroupName { get; set; }
        [StringLength(10)]
        public string EventCatalogueCodeParent { get; set; }
        [StringLength(50)]
        public string EventCatalogueNameParent { get; set; }
        [StringLength(100)]
        public string EventCatalogueDescriptionParent { get; set; }
    }
}
