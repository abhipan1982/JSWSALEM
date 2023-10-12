using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_RawMaterialHistory
    {
        public long RawMaterialId { get; set; }
        [Required]
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        public long ShiftCalendarId { get; set; }
        public double? RawMaterialLastWeight { get; set; }
        public double? RawMaterialLastLength { get; set; }
        public long RawMaterialStepId { get; set; }
        public long AssetId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ProcessingStepTs { get; set; }
        public short ProcessingStepNo { get; set; }
        public short RawMaterialPassNo { get; set; }
        public bool RawMaterialIsReversed { get; set; }
        public bool IsAssetExit { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
    }
}
