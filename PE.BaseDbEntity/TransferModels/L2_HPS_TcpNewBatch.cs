using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.TransferModels
{
    [Keyless]
    [Table("L2_HPS_TcpNewBatch", Schema = "xfr")]
    public partial class L2_HPS_TcpNewBatch
    {
        [StringLength(4)]
        public string Length { get; set; }
        [StringLength(4)]
        public string Id { get; set; }
        [StringLength(50)]
        public string BatchId { get; set; }
        public short? Shape { get; set; }
        [StringLength(50)]
        public string SteelType { get; set; }
        [StringLength(10)]
        public string NomHeight { get; set; }
        [StringLength(10)]
        public string NomWidth { get; set; }
        [StringLength(10)]
        public string TolMaxVert { get; set; }
        [StringLength(10)]
        public string TolMinVert { get; set; }
        [StringLength(10)]
        public string TolMaxHor { get; set; }
        [StringLength(10)]
        public string TolMinHor { get; set; }
        [StringLength(4)]
        public string CornerRadius { get; set; }
        [StringLength(10)]
        public string RefTemp { get; set; }
        public int? BarId { get; set; }
        [StringLength(10)]
        public string TolDiagDiff { get; set; }
        [StringLength(10)]
        public string TolUMax { get; set; }
        [StringLength(14)]
        public string TolCentPos { get; set; }
        [StringLength(20)]
        public string GroveRadius_Upper { get; set; }
        [StringLength(20)]
        public string GroveRadius_Lower { get; set; }
    }
}
