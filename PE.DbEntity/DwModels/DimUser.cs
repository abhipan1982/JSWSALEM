using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimUser
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimUserRow { get; set; }
        public bool DimUserIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimUserHash { get; set; }
        [Required]
        [StringLength(450)]
        public string DimUserKey { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string UserFirstName { get; set; }
        [StringLength(50)]
        public string UserLastName { get; set; }
        [StringLength(100)]
        public string UserJobPosition { get; set; }
        [StringLength(100)]
        public string UserFullName { get; set; }
        [StringLength(256)]
        public string UserEmail { get; set; }
    }
}
