using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimSteelgrade
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimSteelgradeRow { get; set; }
        public bool DimSteelgradeIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimSteelgradeHash { get; set; }
        public long DimSteelgradeKey { get; set; }
        public long? DimSteelgradeKeyParent { get; set; }
        public long? DimSteelGroupKey { get; set; }
        public long? DimScrapGroupKey { get; set; }
        [Required]
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        [StringLength(200)]
        public string SteelgradeDescription { get; set; }
        [StringLength(10)]
        public string SteelgradeCustomCode { get; set; }
        [StringLength(50)]
        public string SteelgradeCustomName { get; set; }
        [StringLength(200)]
        public string SteelgradeCustomDescription { get; set; }
        public double? SteelgradeDensity { get; set; }
        [StringLength(10)]
        public string SteelGroupCode { get; set; }
        [StringLength(50)]
        public string SteelGroupName { get; set; }
        [StringLength(200)]
        public string SteelGroupDescription { get; set; }
        [StringLength(10)]
        public string ScrapGroupCode { get; set; }
        [StringLength(50)]
        public string ScrapGroupName { get; set; }
        [StringLength(200)]
        public string ScrapGroupDescription { get; set; }
        [StringLength(10)]
        public string SteelgradeCodeParent { get; set; }
        [StringLength(50)]
        public string SteelgradeNameParent { get; set; }
        [StringLength(200)]
        public string SteelgradeDescriptionParent { get; set; }
        public double? SteelgradeFeMin { get; set; }
        public double? SteelgradeFeMax { get; set; }
        public double? SteelgradeCMin { get; set; }
        public double? SteelgradeCMax { get; set; }
        public double? SteelgradeMnMin { get; set; }
        public double? SteelgradeMnMax { get; set; }
        public double? SteelgradeCrMin { get; set; }
        public double? SteelgradeCrMax { get; set; }
        public double? SteelgradeMoMin { get; set; }
        public double? SteelgradeMoMax { get; set; }
        public double? SteelgradeVMin { get; set; }
        public double? SteelgradeVMax { get; set; }
        public double? SteelgradeNiMin { get; set; }
        public double? SteelgradeNiMax { get; set; }
        public double? SteelgradeCoMin { get; set; }
        public double? SteelgradeCoMax { get; set; }
        public double? SteelgradeSiMin { get; set; }
        public double? SteelgradeSiMax { get; set; }
        public double? SteelgradePMin { get; set; }
        public double? SteelgradePMax { get; set; }
        public double? SteelgradeSMin { get; set; }
        public double? SteelgradeSMax { get; set; }
        public double? SteelgradeCuMin { get; set; }
        public double? SteelgradeCuMax { get; set; }
        public double? SteelgradeNbMin { get; set; }
        public double? SteelgradeNbMax { get; set; }
        public double? SteelgradeAlMin { get; set; }
        public double? SteelgradeAlMax { get; set; }
        public double? SteelgradeNMin { get; set; }
        public double? SteelgradeNMax { get; set; }
        public double? SteelgradeCaMin { get; set; }
        public double? SteelgradeCaMax { get; set; }
        public double? SteelgradeBMin { get; set; }
        public double? SteelgradeBMax { get; set; }
        public double? SteelgradeTiMin { get; set; }
        public double? SteelgradeTiMax { get; set; }
        public double? SteelgradeSnMin { get; set; }
        public double? SteelgradeSnMax { get; set; }
        public double? SteelgradeOMin { get; set; }
        public double? SteelgradeOMax { get; set; }
        public double? SteelgradeHMin { get; set; }
        public double? SteelgradeHMax { get; set; }
        public double? SteelgradeWMin { get; set; }
        public double? SteelgradeWMax { get; set; }
        public double? SteelgradePbMin { get; set; }
        public double? SteelgradePbMax { get; set; }
        public double? SteelgradeZnMin { get; set; }
        public double? SteelgradeZnMax { get; set; }
        public double? SteelgradeAsMin { get; set; }
        public double? SteelgradeAsMax { get; set; }
        public double? SteelgradeMgMin { get; set; }
        public double? SteelgradeMgMax { get; set; }
        public double? SteelgradeSbMin { get; set; }
        public double? SteelgradeSbMax { get; set; }
        public double? SteelgradeBiMin { get; set; }
        public double? SteelgradeBiMax { get; set; }
        public double? SteelgradeTaMin { get; set; }
        public double? SteelgradeTaMax { get; set; }
        public double? SteelgradeZrMin { get; set; }
        public double? SteelgradeZrMax { get; set; }
        public double? SteelgradeCeMin { get; set; }
        public double? SteelgradeCeMax { get; set; }
        public double? SteelgradeTeMin { get; set; }
        public double? SteelgradeTeMax { get; set; }
    }
}
