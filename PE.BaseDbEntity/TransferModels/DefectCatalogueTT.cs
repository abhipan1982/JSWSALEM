using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.BaseDbEntity.TransferModels
{
    [Keyless]
    [Table("DefectCatalogueTT", Schema = "xfr")]
    public partial class DefectCatalogueTT
    {
        [StringLength(255)]
        public string DefectCatalogueName { get; set; }
        [StringLength(255)]
        public string DefectCatalogueCategory { get; set; }
        [StringLength(255)]
        public string DefectCatalogueDescription { get; set; }
        [StringLength(255)]
        public string ParentDefectCatalogueCode { get; set; }
        public short? IsDefault { get; set; }
    }
}
