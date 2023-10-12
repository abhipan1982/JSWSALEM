using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("PRMHeatChemicalAnalysis")]
    [Index(nameof(FKHeatId), Name = "NCI_HeatId")]
    public partial class PRMHeatChemicalAnalysis
    {
        [Key]
        public long HeatChemAnalysisId { get; set; }
        public long FKHeatId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SampleTakenTs { get; set; }
        public short? Laboratory { get; set; }
        public double? Fe { get; set; }
        public double? C { get; set; }
        public double? Mn { get; set; }
        public double? Cr { get; set; }
        public double? Mo { get; set; }
        public double? V { get; set; }
        public double? Ni { get; set; }
        public double? Co { get; set; }
        public double? Si { get; set; }
        public double? P { get; set; }
        public double? S { get; set; }
        public double? Cu { get; set; }
        public double? Nb { get; set; }
        public double? Al { get; set; }
        public double? N { get; set; }
        public double? Ca { get; set; }
        public double? B { get; set; }
        public double? Ti { get; set; }
        public double? Sn { get; set; }
        public double? O { get; set; }
        public double? H { get; set; }
        public double? W { get; set; }
        public double? Pb { get; set; }
        public double? Zn { get; set; }
        public double? As { get; set; }
        public double? Mg { get; set; }
        public double? Sb { get; set; }
        public double? Bi { get; set; }
        public double? Ta { get; set; }
        public double? Zr { get; set; }
        public double? Ce { get; set; }
        public double? Te { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKHeatId))]
        [InverseProperty(nameof(PRMHeat.PRMHeatChemicalAnalyses))]
        public virtual PRMHeat FKHeat { get; set; }
    }
}
