using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class RLSGrooveTemplate
    {
        public RLSGrooveTemplate()
        {
            RLSRollGroovesHistories = new HashSet<RLSRollGroovesHistory>();
        }

        [Key]
        public long GrooveTemplateId { get; set; }
        [Required]
        [StringLength(5)]
        public string Shape { get; set; }
        [Required]
        [StringLength(20)]
        public string GrooveTemplateCode { get; set; }
        [Required]
        [StringLength(50)]
        public string GrooveTemplateName { get; set; }
        [StringLength(100)]
        public string GrooveTemplateDescription { get; set; }
        [StringLength(50)]
        public string GrindingProgramName { get; set; }
        public GrooveTemplateStatus EnumGrooveTemplateStatus { get; set; }
        public GrooveSetting EnumGrooveSetting { get; set; }
        /// <summary>
        /// Radius 1
        /// </summary>
        public double? R1 { get; set; }
        /// <summary>
        /// Radius 2
        /// </summary>
        public double? R2 { get; set; }
        /// <summary>
        /// Radius 3
        /// </summary>
        public double? R3 { get; set; }
        /// <summary>
        /// Depth 1
        /// </summary>
        public double? D1 { get; set; }
        /// <summary>
        /// Depth 2
        /// </summary>
        public double? D2 { get; set; }
        /// <summary>
        /// Width 1
        /// </summary>
        public double? W1 { get; set; }
        /// <summary>
        /// Width 2
        /// </summary>
        public double? W2 { get; set; }
        /// <summary>
        /// Angle 1
        /// </summary>
        public double? Angle1 { get; set; }
        /// <summary>
        /// Angle 2
        /// </summary>
        public double? Angle2 { get; set; }
        public double? Ds { get; set; }
        public double? Dw { get; set; }
        public double? SpreadFactor { get; set; }
        /// <summary>
        /// m2
        /// </summary>
        public double? Plane { get; set; }
        public bool IsDefault { get; set; }
        public long? AccBilletCntLimit { get; set; }
        public double? AccWeightLimit { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(RLSRollGroovesHistory.FKGrooveTemplate))]
        public virtual ICollection<RLSRollGroovesHistory> RLSRollGroovesHistories { get; set; }
    }
}
