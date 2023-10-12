using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_FeatureCustom
    {
        public long FeatureId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long ExtUnitOfMeasureId { get; set; }
        public long LanguageId { get; set; }
        public long CustomUnitOfMeasureId { get; set; }
        public long? CustomUnitOfMeasureFormatId { get; set; }
        public int FeatureCode { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitSymbol { get; set; }
        [Required]
        [StringLength(50)]
        public string ExtUnitSymbol { get; set; }
        [Required]
        [StringLength(10)]
        public string LanguageCode { get; set; }
        [Required]
        [StringLength(50)]
        public string CustomUnitSymbol { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string CustomUnitFormat { get; set; }
    }
}
