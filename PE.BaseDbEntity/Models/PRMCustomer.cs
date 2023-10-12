using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(CustomerName), Name = "UQ_CustomerName", IsUnique = true)]
    public partial class PRMCustomer
    {
        public PRMCustomer()
        {
            PRMWorkOrders = new HashSet<PRMWorkOrder>();
        }

        [Key]
        public long CustomerId { get; set; }
        [Required]
        [StringLength(10)]
        public string CustomerCode { get; set; }
        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }
        [StringLength(200)]
        public string CustomerAddress { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string DocPatternName { get; set; }
        [StringLength(50)]
        public string Country { get; set; }
        [StringLength(20)]
        public string LogoName { get; set; }
        [StringLength(20)]
        public string SAPKey { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public bool IsDefault { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(PRMWorkOrder.FKCustomer))]
        public virtual ICollection<PRMWorkOrder> PRMWorkOrders { get; set; }
    }
}
