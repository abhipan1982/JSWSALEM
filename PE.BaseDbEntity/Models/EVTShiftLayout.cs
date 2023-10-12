using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class EVTShiftLayout
    {
        public EVTShiftLayout()
        {
            EVTDaysOfYears = new HashSet<EVTDaysOfYear>();
            EVTShiftDefinitions = new HashSet<EVTShiftDefinition>();
        }

        [Key]
        public long ShiftLayoutId { get; set; }
        [Required]
        [StringLength(10)]
        public string ShiftLayoutCode { get; set; }
        [StringLength(50)]
        public string ShiftLayoutName { get; set; }
        [StringLength(255)]
        public string ShiftLayoutDescription { get; set; }
        public bool IsDefaultLayout { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(EVTDaysOfYear.FKShiftLayout))]
        public virtual ICollection<EVTDaysOfYear> EVTDaysOfYears { get; set; }
        [InverseProperty(nameof(EVTShiftDefinition.FKShiftLayout))]
        public virtual ICollection<EVTShiftDefinition> EVTShiftDefinitions { get; set; }
    }
}
