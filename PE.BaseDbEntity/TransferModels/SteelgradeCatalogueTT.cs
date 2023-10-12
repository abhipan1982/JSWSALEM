﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.BaseDbEntity.TransferModels
{
    [Keyless]
    [Table("SteelgradeCatalogueTT", Schema = "xfr")]
    public partial class SteelgradeCatalogueTT
    {
        [StringLength(255)]
        public string SteelgradeCode { get; set; }
        [StringLength(255)]
        public string ParentSteelgradeCode { get; set; }
        public short? IsDefault { get; set; }
        [StringLength(255)]
        public string SteelgradeName { get; set; }
        [StringLength(255)]
        public string SteelgradeDescription { get; set; }
        public double? Density { get; set; }
        public double? OvenRecipeTemperature { get; set; }
        [StringLength(255)]
        public string QualitySpecification { get; set; }
        [StringLength(255)]
        public string CommercialGroup { get; set; }
        [StringLength(255)]
        public string CustomerUseCode { get; set; }
        [StringLength(255)]
        public string CustomerUseDescription { get; set; }
        [StringLength(255)]
        public string SteelGroupCode { get; set; }
        [StringLength(255)]
        public string SteelGroupName { get; set; }
        [StringLength(255)]
        public string SteelGroupDescription { get; set; }
        public short? SteelGroupIsDefault { get; set; }
        [StringLength(255)]
        public string ParentSteelGroupCode { get; set; }
        public double? FeMin { get; set; }
        public double? FeMax { get; set; }
        public double? CMin { get; set; }
        public double? CMax { get; set; }
        public double? MnMin { get; set; }
        public double? MnMax { get; set; }
        public double? CrMin { get; set; }
        public double? CrMax { get; set; }
        public double? MoMin { get; set; }
        public double? MoMax { get; set; }
        public double? VMin { get; set; }
        public double? VMax { get; set; }
        public double? NiMin { get; set; }
        public double? NiMax { get; set; }
        public double? CoMin { get; set; }
        public double? CoMax { get; set; }
        public double? SiMin { get; set; }
        public double? SiMax { get; set; }
        public double? PMin { get; set; }
        public double? PMax { get; set; }
        public double? SMin { get; set; }
        public double? SMax { get; set; }
        public double? CuMin { get; set; }
        public double? CuMax { get; set; }
        public double? NbMin { get; set; }
        public double? NbMax { get; set; }
        public double? AlMin { get; set; }
        public double? AlMax { get; set; }
        public double? NMin { get; set; }
        public double? NMax { get; set; }
        public double? CaMin { get; set; }
        public double? CaMax { get; set; }
        public double? BMin { get; set; }
        public double? BMax { get; set; }
        public double? TiMin { get; set; }
        public double? TiMax { get; set; }
        public double? SnMin { get; set; }
        public double? SnMax { get; set; }
        public double? OMin { get; set; }
        public double? OMax { get; set; }
        public double? HMin { get; set; }
        public double? HMax { get; set; }
        public double? WMin { get; set; }
        public double? WMax { get; set; }
        public double? PbMin { get; set; }
        public double? PbMax { get; set; }
        public double? ZnMin { get; set; }
        public double? ZnMax { get; set; }
        public double? AsMin { get; set; }
        public double? AsMax { get; set; }
        public double? MgMin { get; set; }
        public double? MgMax { get; set; }
        public double? SbMin { get; set; }
        public double? SbMax { get; set; }
        public double? BiMin { get; set; }
        public double? BiMax { get; set; }
        public double? TaMin { get; set; }
        public double? TaMax { get; set; }
        public double? ZrMin { get; set; }
        public double? ZrMax { get; set; }
        public double? CeMin { get; set; }
        public double? CeMax { get; set; }
        public double? TeMin { get; set; }
        public double? TeMax { get; set; }
    }
}
