using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_MaterialSearchGrid
    {
        public long MaterialId { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialName { get; set; }
        public short MaterialSeqNo { get; set; }
        public bool MaterialIsAssigned { get; set; }
        public double MaterialWeight { get; set; }
        public double? MaterialWidth { get; set; }
        public double MaterialThickness { get; set; }
        public double? MaterialLength { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime MaterialCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MaterialStartTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MaterialEndTs { get; set; }
        public long HeatId { get; set; }
        [Required]
        [StringLength(50)]
        public string HeatName { get; set; }
        public long? WorkOrderId { get; set; }
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public long? MaterialCatalogueId { get; set; }
        [StringLength(50)]
        public string MaterialCatalogueName { get; set; }
    }
}
