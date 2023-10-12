using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.Models
{
    [Table("MVHAssetsEXT", Schema = "prj")]
    public partial class MVHAssetsEXT
    {
        [Key]
        public long FKAssetId { get; set; }
        public short MaxPassNo { get; set; }
        public short? TimeIn { get; set; }
        public bool IsInitial { get; set; }
        public bool IsLast { get; set; }
        public bool IsQueue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }
    }
}
