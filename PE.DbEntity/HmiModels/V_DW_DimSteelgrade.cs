using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimSteelgrade
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long DimSteelgradeKey { get; set; }
        public long? DimParentSteelgradeKey { get; set; }
        [Required]
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        [StringLength(200)]
        public string SteelgradeDescription { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeDensity { get; set; }
        [StringLength(10)]
        public string SteelGroupCode { get; set; }
        [StringLength(50)]
        public string SteelGroupName { get; set; }
        [StringLength(200)]
        public string SteelGroupDescription { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeFeMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeFeMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeCMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeCMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeMnMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeMnMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeCrMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeCrMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeMoMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeMoMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeVMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeVMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeNiMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeNiMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeCoMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeCoMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeSiMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeSiMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradePMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradePMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeSMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeSMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeCuMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeCuMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeNbMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeNbMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeAlMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeAlMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeNMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeNMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeCaMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeCaMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeBMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeBMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeTiMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeTiMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeSnMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeSnMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeOMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeOMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeHMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeHMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeWMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeWMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradePbMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradePbMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeZnMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeZnMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeAsMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeAsMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeMgMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeMgMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeSbMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeSbMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeBiMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeBiMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeTaMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeTaMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeZrMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeZrMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeCeMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeCeMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeTeMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? SteelgradeTeMax { get; set; }
    }
}
