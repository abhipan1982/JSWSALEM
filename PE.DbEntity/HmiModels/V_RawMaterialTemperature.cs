using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_RawMaterialTemperature
    {
        public long RawMaterialId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        public short? OrderSeq { get; set; }
        public double? Temperature { get; set; }
        public int EnumFurnaceMaterialState { get; set; }
        public int EnumOxidationState { get; set; }
        public int OxidationCoeff { get; set; }
        [StringLength(128)]
        public string UnitResourceKey { get; set; }
    }
}
