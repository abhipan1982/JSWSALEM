using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.Models
{
    [Table("MVHFeaturesEXT", Schema = "prj")]
    public partial class MVHFeaturesEXT
    {
        [Key]
        public long FKFeatureId { get; set; }
        public short? ResNum { get; set; }
        public short? ResDen { get; set; }
        public double? MinValue { get; set; }
        public double? MaxValue { get; set; }
        [StringLength(400)]
        public string ListValues { get; set; }
        public bool IsLengthChange { get; set; }
        public bool IsWeightChange { get; set; }
        public bool IsPossibleInversion { get; set; }
        public bool? OnAssetEntry { get; set; }
        public TypeOfCut EnumTypeOfCut { get; set; }
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
