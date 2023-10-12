using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.TransferModels
{
    [Table("L3L2BatchDataDefinition", Schema = "xfrprj")]
    public partial class L3L2BatchDataDefinition
    {
        [Key]
        public long CounterId { get; set; }
        /// <summary>
        /// Bloom Id (unique)
        /// </summary>
        [Required]
        [StringLength(50)]
        public string BATCH_NO { get; set; }
        /// <summary>
        /// Unique work order number
        /// </summary>
        [Required]
        [StringLength(50)]
        public string PO_NO { get; set; }
        /// <summary>
        /// Heat No
        /// </summary>
        [StringLength(50)]
        public string HEAT_NO { get; set; }
        /// <summary>
        /// Customer Name
        /// </summary>
        [StringLength(50)]
        public string CUST_NAME { get; set; }
        /// <summary>
        /// Weight of order to be delivered to customer
        /// </summary>
        public double? SO_QTY { get; set; }
        /// <summary>
        /// Product Standard No for the order
        /// </summary>
        [StringLength(50)]
        public string PSN { get; set; }
        /// <summary>
        /// Input thickness/diameter
        /// </summary>
        public double? BLM_BRM_C_S_THICK { get; set; }
        /// <summary>
        /// Input Width
        /// </summary>
        public double? BLM_BRM_C_S_WIDTH { get; set; }
        /// <summary>
        /// Bloom Shape Indicator (NRND,NBLM)
        /// </summary>
        [StringLength(4)]
        public string INPUT_MATERIAL { get; set; }
        /// <summary>
        /// Bloom Weight
        /// </summary>
        public double? BLM_BRM_WEIGHT { get; set; }
        /// <summary>
        /// Bloom Length
        /// </summary>
        public double? BLM_BRM_LENGTH { get; set; }
        /// <summary>
        /// Output Thickness
        /// </summary>
        public double? ROLLED_THICK { get; set; }
        /// <summary>
        /// Output Width
        /// </summary>
        public double? ROLLED_WIDTH { get; set; }
        /// <summary>
        /// Product Shape Indicator (RND, RCS)
        /// </summary>
        [StringLength(3)]
        public string OUTPUT_MATERIAL { get; set; }
        /// <summary>
        /// RCS MIN
        /// </summary>
        public double? S_SIDE_TOL_MM_NEG { get; set; }
        /// <summary>
        /// RCS MAX
        /// </summary>
        public double? S_SIDE_TOL_MM_POS { get; set; }
        /// <summary>
        /// Shape Tolerance for RCS MIN
        /// </summary>
        public double? S_OUT_OF_SQUARNESS_MM_MIN { get; set; }
        /// <summary>
        /// Shape Tolerance for RCS MAX
        /// </summary>
        public double? S_OUT_OF_SQUARNESS_MM_MAX { get; set; }
        /// <summary>
        /// Diameter Tolerance RND MIN
        /// </summary>
        public double? S_DIA_TOL_MM_LOWER { get; set; }
        /// <summary>
        /// Diameter Tolerance RND MAX
        /// </summary>
        public double? S_DIA_TOL_MM_UPPER { get; set; }
        /// <summary>
        /// Ovality Tolerance MIN
        /// </summary>
        public double? S_OVALITY_MM_MIN { get; set; }
        /// <summary>
        /// Ovality Tolerance MAX
        /// </summary>
        public double? S_OVALITY_MM_MAX { get; set; }
        /// <summary>
        /// Length Tolerance MIN
        /// </summary>
        public double? S_LENGTH_MM_MIN { get; set; }
        /// <summary>
        /// Length Tolerance MAX
        /// </summary>
        public double? S_LENGTH_MM_MAX { get; set; }
        /// <summary>
        /// Multiple Length
        /// </summary>
        public double? S_MULTIPLE_LENGTH_MM { get; set; }
        /// <summary>
        /// Target Length
        /// </summary>
        public double? S_LENGTH { get; set; }
        /// <summary>
        /// 1/2 Charging Type (1=Cold, 2=Hot)
        /// </summary>
        public short? CHARGE_TYPE { get; set; }
        /// <summary>
        /// Cooling Type (1=Slow, 2=Fast,3=Normal)
        /// </summary>
        public short? COOL_TYPE { get; set; }
        /// <summary>
        ///  1/2 Lifting Type (1=Chain, 2=Magnet)
        /// </summary>
        public short? LIFT_TYPE { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedTs { get; set; }
        public bool IsUpdated { get; set; }
        /// <summary>
        /// message processing:
        ///       --0: New entry, no validation errors
        ///         --1: Entry currently processed by system
        ///         --2: Entry processed by system, no errors
        ///          -- -1: Validation errors (DB Validation)
        ///           -- -2: Entry processed by system, but
        ///           --processing errors occurred.
        /// </summary>
        public short CommStatus { get; set; }
        /// <summary>
        /// Additional messages from subsequent processing modules
        /// </summary>
        [StringLength(400)]
        public string CommMessage { get; set; }
        /// <summary>
        /// Text describing current status of the message
        /// </summary>
        [StringLength(400)]
        public string ValidationCheck { get; set; }
    }
}
