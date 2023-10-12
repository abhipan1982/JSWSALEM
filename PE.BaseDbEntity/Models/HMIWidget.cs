using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class HMIWidget
    {
        public HMIWidget()
        {
            HMIWidgetConfigurations = new HashSet<HMIWidgetConfiguration>();
        }

        [Key]
        public long WidgetId { get; set; }
        [Required]
        [StringLength(50)]
        public string WidgetName { get; set; }
        [Required]
        [StringLength(250)]
        public string WidgetFileName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(HMIWidgetConfiguration.FKWidget))]
        public virtual ICollection<HMIWidgetConfiguration> HMIWidgetConfigurations { get; set; }
    }
}
