using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimHeat
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimHeatRow { get; set; }
        public bool DimHeatIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimHeatHash { get; set; }
        public long DimHeatKey { get; set; }
        public long? DimSteelgradeKey { get; set; }
        public long? DimHeatSupplierKey { get; set; }
        [Required]
        [StringLength(50)]
        public string HeatName { get; set; }
        public double? HeatWeight { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HeatCreated { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        [StringLength(50)]
        public string HeatSupplierName { get; set; }
        [StringLength(200)]
        public string HeatSupplierDescription { get; set; }
        public double? HeatChFe { get; set; }
        public double? HeatChC { get; set; }
        public double? HeatChMn { get; set; }
        public double? HeatChCr { get; set; }
        public double? HeatChMo { get; set; }
        public double? HeatChV { get; set; }
        public double? HeatChNi { get; set; }
        public double? HeatChCo { get; set; }
        public double? HeatChSi { get; set; }
        public double? HeatChP { get; set; }
        public double? HeatChS { get; set; }
        public double? HeatChCu { get; set; }
        public double? HeatChNb { get; set; }
        public double? HeatChAl { get; set; }
        public double? HeatChN { get; set; }
        public double? HeatChCa { get; set; }
        public double? HeatChB { get; set; }
        public double? HeatChTi { get; set; }
        public double? HeatChSn { get; set; }
        public double? HeatChO { get; set; }
        public double? HeatChH { get; set; }
        public double? HeatChW { get; set; }
        public double? HeatChPb { get; set; }
        public double? HeatChZn { get; set; }
        public double? HeatChAs { get; set; }
        public double? HeatChMg { get; set; }
        public double? HeatChSb { get; set; }
        public double? HeatChBi { get; set; }
        public double? HeatChTa { get; set; }
        public double? HeatChZr { get; set; }
        public double? HeatChCe { get; set; }
        public double? HeatChTe { get; set; }
    }
}
