using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.Models
{
    [Table("PRMProductCatalogueEXT", Schema = "prj")]
    public partial class PRMProductCatalogueEXT
    {
        [Key]
        public long FKProductCatalogueId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }
        public double? MinOvality { get; set; }
        public double? MinDiameter { get; set; }
        public double? MaxDiameter { get; set; }
        public double? Diameter { get; set; }
        public double? NegRcsSide { get; set; }
        public double? PosRcsSide { get; set; }
        public double? MinSquareness { get; set; }
        public double? MaxSquareness { get; set; }
    }
}
