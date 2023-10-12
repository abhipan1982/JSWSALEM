using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.BaseDbEntity.TransferModels
{
    [Keyless]
    [Table("DelayCatalogueTT", Schema = "xfr")]
    public partial class DelayCatalogueTT
    {
        [StringLength(255)]
        public string DelayCode { get; set; }
        [StringLength(255)]
        public string DelayName { get; set; }
        [StringLength(255)]
        public string DelayDescription { get; set; }
        public short? IsActive { get; set; }
        public short? IsDefault { get; set; }
        public double? StdDelayTime { get; set; }
        [StringLength(255)]
        public string ParentDelayCatalogueCode { get; set; }
        [StringLength(255)]
        public string DelayCatalogueCategoryCode { get; set; }
        [StringLength(255)]
        public string DelayCatalogueCategoryName { get; set; }
    }
}
