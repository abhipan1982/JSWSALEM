using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class PRFKPIValue
    {
        [Key]
        public long KPIValueId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime KPITime { get; set; }
        public double KPIValue { get; set; }
        public long FKKPIDefinitionId { get; set; }
        public long? FKWorkOrderId { get; set; }
        public long? FKShiftCalendarId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKKPIDefinitionId))]
        [InverseProperty(nameof(PRFKPIDefinition.PRFKPIValues))]
        public virtual PRFKPIDefinition FKKPIDefinition { get; set; }
        [ForeignKey(nameof(FKShiftCalendarId))]
        [InverseProperty(nameof(EVTShiftCalendar.PRFKPIValues))]
        public virtual EVTShiftCalendar FKShiftCalendar { get; set; }
        [ForeignKey(nameof(FKWorkOrderId))]
        [InverseProperty(nameof(PRMWorkOrder.PRFKPIValues))]
        public virtual PRMWorkOrder FKWorkOrder { get; set; }
    }
}
