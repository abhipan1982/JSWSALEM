using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimCustomer
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long DimCustomerKey { get; set; }
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }
        [StringLength(200)]
        public string CustomerAddress { get; set; }
        [StringLength(150)]
        public string CustomerEmail { get; set; }
        [StringLength(20)]
        public string CustomerPhone { get; set; }
    }
}
