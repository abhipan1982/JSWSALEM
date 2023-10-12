using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKUserId), Name = "NCI_UserId")]
    public partial class HMIWidgetConfiguration
    {
        [Key]
        public long WidgetConfigurationId { get; set; }
        public string FKUserId { get; set; }
        public long FKWidgetId { get; set; }
        [Required]
        [StringLength(50)]
        public string WidgetName { get; set; }
        [Required]
        [StringLength(250)]
        public string WidgetFileName { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public short OrderSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKWidgetId))]
        [InverseProperty(nameof(HMIWidget.HMIWidgetConfigurations))]
        public virtual HMIWidget FKWidget { get; set; }
    }
}
