using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.TransferModels
{
    [Keyless]
    [Table("HPS_L2_TcpProduct", Schema = "xfr")]
    public partial class HPS_L2_TcpProduct
    {
        [StringLength(4)]
        public string Length { get; set; }
        [StringLength(4)]
        public string Id { get; set; }
        [StringLength(50)]
        public string BatchId { get; set; }
        [StringLength(50)]
        public string ShapeTxt { get; set; }
        public short? Shape { get; set; }
        [StringLength(50)]
        public string QualityTxt { get; set; }
        public double? K1 { get; set; }
        public double? K2 { get; set; }
        public int? RefTemp { get; set; }
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
        public int? BarId { get; set; }
        [StringLength(10)]
        public string TolDiagDiff { get; set; }
        [StringLength(10)]
        public string TolUMax { get; set; }
        [StringLength(14)]
        public string TolCentPos { get; set; }
        [StringLength(2)]
        public string NewBatch { get; set; }
        [StringLength(1)]
        public string Source { get; set; }
        [StringLength(20)]
        public string GroveRadius_Upper { get; set; }
        [StringLength(20)]
        public string GroveRadius_Lower { get; set; }
        [StringLength(1)]
        public string LSBUnit { get; set; }
        [StringLength(5)]
        public string Spare { get; set; }
    }
}
