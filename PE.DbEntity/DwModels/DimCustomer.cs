using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimCustomer
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimCustomerRow { get; set; }
        public bool DimCustomerIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimCustomerHash { get; set; }
        public long DimCustomerKey { get; set; }
        [Required]
        [StringLength(10)]
        public string CustomerCode { get; set; }
        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }
        [StringLength(200)]
        public string CustomerAddress { get; set; }
        [StringLength(100)]
        public string CustomerEmail { get; set; }
        [StringLength(20)]
        public string CustomerPhone { get; set; }
    }
}
