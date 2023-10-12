using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_SetupParameter
    {
        public long OrderSeq { get; set; }
        public long SetupId { get; set; }
        [Required]
        [StringLength(50)]
        public string SetupName { get; set; }
        public long? SetupTypeId { get; set; }
        [StringLength(10)]
        public string SetupTypeCode { get; set; }
        [StringLength(50)]
        public string SetupTypeName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsSteelgradeRelated { get; set; }
        [StringLength(50)]
        public string Steelgrade { get; set; }
        [Column("Product Size")]
        [StringLength(50)]
        public string Product_Size { get; set; }
        [Column("Work Order")]
        [StringLength(50)]
        public string Work_Order { get; set; }
        [Column("Heat Number")]
        [StringLength(50)]
        public string Heat_Number { get; set; }
        [StringLength(50)]
        public string Layout { get; set; }
        [StringLength(50)]
        public string Issue { get; set; }
        [Column("Previous Steelgrade")]
        [StringLength(50)]
        public string Previous_Steelgrade { get; set; }
        [Column("Previous Product Size")]
        [StringLength(50)]
        public string Previous_Product_Size { get; set; }
        [Column("Previous Layout")]
        [StringLength(50)]
        public string Previous_Layout { get; set; }
        [Column("Parameter Name")]
        [StringLength(50)]
        public string Parameter_Name { get; set; }
    }
}
