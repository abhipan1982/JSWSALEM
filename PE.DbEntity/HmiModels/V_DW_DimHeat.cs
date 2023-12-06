using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimHeat
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long DimHeatKey { get; set; }
        public long? DimSteelgradeKey { get; set; }
        [Required]
        [StringLength(50)]
        public string HeatName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HeatCreated { get; set; }
        [StringLength(50)]
        public string HeatSupplierName { get; set; }
        [StringLength(200)]
        public string HeatSupplierDescription { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChFe { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChC { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChMn { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChCr { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChMo { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChV { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChNi { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChCo { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChSi { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChP { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChS { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChCu { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChNb { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChAl { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChN { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChCa { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChB { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChTi { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChSn { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChO { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChH { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChW { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChPb { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChZn { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChAs { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChMg { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChSb { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChBi { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChTa { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChZr { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChCe { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? HeatChTe { get; set; }
    }
}
