using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimEventType
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimEventTypeRow { get; set; }
        public bool DimEventTypeIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimEventTypeHash { get; set; }
        public long DimEventTypeKey { get; set; }
        public long? DimEventTypeKeyParent { get; set; }
        public int EventIsDelay { get; set; }
        public short EventTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EventTypeName { get; set; }
        [StringLength(100)]
        public string EventTypeDescription { get; set; }
        public short? EventTypeCodeParent { get; set; }
        [StringLength(50)]
        public string EventTypeNameParent { get; set; }
        [StringLength(100)]
        public string EventTypeDescriptionParent { get; set; }
    }
}
