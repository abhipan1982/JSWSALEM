using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.TransferModels
{
    [Table("L2L3BatchReport", Schema = "xfrprj")]
    public partial class L2L3BatchReport
    {
        /// <summary>
        /// Bloom Id (unique)
        /// </summary>
        [Key]
        [StringLength(50)]
        public string BATCH_NO { get; set; }
        /// <summary>
        /// Unique work order number
        /// </summary>
        [Required]
        [StringLength(50)]
        public string PO_NO { get; set; }
        /// <summary>
        /// YYYYMMDDHHMMSS Bar code scanning Time
        /// </summary>
        [StringLength(14)]
        public string BAR_CODE_SCAN_TIME { get; set; }
        /// <summary>
        /// YYYYMMDDHHMMSS Furnace Charging Time
        /// </summary>
        [StringLength(14)]
        public string FUR_CHARGE_TIME { get; set; }
        /// <summary>
        /// YYYYMMDDHHMMSS Furnace DisCharging Time
        /// </summary>
        [StringLength(14)]
        public string FUR_DISCHARGE_TIME { get; set; }
        /// <summary>
        /// YYYYMMDDHHMMSS Rolling Time
        /// </summary>
        [StringLength(14)]
        public string ROLLING_TIME { get; set; }
        /// <summary>
        /// Total Number of Rolled Billets. Partial scraps are included.
        /// </summary>
        public short? NO_ROLLED_BARS { get; set; }
        /// <summary>
        /// Furnace Charging – CP1 Operator Name
        /// </summary>
        [StringLength(50)]
        public string CP1_OP_NAME { get; set; }
        /// <summary>
        /// Furnace Charging – CP1 Supervisor Name
        /// </summary>
        [StringLength(50)]
        public string CP1_SUP_NAME { get; set; }
        /// <summary>
        /// Furnace Discharging CP2 Operator Name
        /// </summary>
        [StringLength(50)]
        public string CP2_OP_NAME { get; set; }
        /// <summary>
        /// Furnace Discharging –CP2 Helper Name
        /// </summary>
        [StringLength(50)]
        public string CP2_HELP_NAME { get; set; }
        /// <summary>
        /// Rolling Mill – CP3 Operator Name
        /// </summary>
        [StringLength(50)]
        public string CP3_OP_NAME { get; set; }
        /// <summary>
        /// Cooling Bed – CP4 operator Name
        /// </summary>
        [StringLength(50)]
        public string CP4_OP_NAME { get; set; }
        /// <summary>
        /// Rolling Mill – CP5 Operator Name
        /// </summary>
        [StringLength(50)]
        public string CP5_OP_NAME { get; set; }
        /// <summary>
        /// Cooling Bed – CP6 Operator Name
        /// </summary>
        [StringLength(50)]
        public string CP6_OP_NAME { get; set; }
        /// <summary>
        /// Shift-In-Charge Name
        /// </summary>
        [StringLength(50)]
        public string SHIFT_IN_CHARGE_NAME { get; set; }
        /// <summary>
        /// Supervisor Name 
        /// </summary>
        [StringLength(50)]
        public string SUPERVISOR_NAME { get; set; }
        /// <summary>
        ///  No. of blooms in bundle
        /// </summary>
        public short? BLOOMS_IN_BUNDLE { get; set; }
        /// <summary>
        /// Charging - Remarks
        /// </summary>
        [StringLength(200)]
        public string CHARGING_REMARKS { get; set; }
        /// <summary>
        /// DisCharging - Remarks
        /// </summary>
        [StringLength(200)]
        public string DISCHARGING_REMARKS { get; set; }
        /// <summary>
        /// Rolling - Remarks
        /// </summary>
        [StringLength(200)]
        public string ROLLING_REMARKS { get; set; }
        /// <summary>
        /// Cooling Bed - Remarks
        /// </summary>
        [StringLength(200)]
        public string COLLING_REMARKS { get; set; }
        /// <summary>
        /// Cobble Area
        /// </summary>
        [StringLength(50)]
        public string COBBLE_AREA { get; set; }
        /// <summary>
        /// Cobble Reason
        /// </summary>
        [StringLength(200)]
        public string COBBLE_REASON { get; set; }
        /// <summary>
        /// Tons Bundle Weight
        /// </summary>
        public double? BUNDLE_WGT { get; set; }
        /// <summary>
        /// % MISROLL %
        /// </summary>
        public double? MISROLL_PERC { get; set; }
        /// <summary>
        /// % COBBLE %
        /// </summary>
        public double? COBBLE_PERC { get; set; }
        /// <summary>
        /// % INDIRECT %
        /// </summary>
        public double? INDIRECT_PERC { get; set; }
        [StringLength(2)]
        public string CHARGING_SIDE { get; set; }
        /// <summary>
        /// --Communication status,
        ///      --result of message
        ///       --processing:
        ///         --0: New entry, no validation
        ///          --errors
        ///          --1: Entry currently
        ///          --processed by system
        ///          --2: Entry processed by
        ///           --system, no errors
        ///          ---1: Validation errors (DB
        ///          --validation)
        ///       ---2: Entry processed by
        ///         --system, but processing
        ///           --errors occured
        /// </summary>
        public short? CommStatus { get; set; }
        /// <summary>
        /// Additional messages from subsequent processing modules
        /// </summary>
        [StringLength(400)]
        public string CommMessage { get; set; }
        /// <summary>
        /// Text describing current state of the message
        /// </summary>
        [StringLength(400)]
        public string ValidationCheck { get; set; }
    }
}
