using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(DataTypeName), Name = "UQ_DataTypeName", IsUnique = true)]
    public partial class DBDataType
    {
        public DBDataType()
        {
            DBProperties = new HashSet<DBProperty>();
            MVHFeatureTemplates = new HashSet<MVHFeatureTemplate>();
            MVHFeatures = new HashSet<MVHFeature>();
            STPInstructions = new HashSet<STPInstruction>();
        }

        [Key]
        public long DataTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string DataTypeName { get; set; }
        [StringLength(50)]
        public string DataTypeNameDotNet { get; set; }
        [StringLength(50)]
        public string DataType { get; set; }
        public short? MaxLength { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(DBProperty.FKDataType))]
        public virtual ICollection<DBProperty> DBProperties { get; set; }
        [InverseProperty(nameof(MVHFeatureTemplate.FKDataType))]
        public virtual ICollection<MVHFeatureTemplate> MVHFeatureTemplates { get; set; }
        [InverseProperty(nameof(MVHFeature.FKDataType))]
        public virtual ICollection<MVHFeature> MVHFeatures { get; set; }
        [InverseProperty(nameof(STPInstruction.FKDataType))]
        public virtual ICollection<STPInstruction> STPInstructions { get; set; }
    }
}
