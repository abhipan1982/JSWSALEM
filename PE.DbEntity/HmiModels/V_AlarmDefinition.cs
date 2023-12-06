using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_AlarmDefinition
    {
        public int? AlarmDefinitionId { get; set; }
        [Required]
        [StringLength(10)]
        public string AlarmCode { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public int AlarmType { get; set; }
        public bool ToConfirm { get; set; }
        public int? AlarmCategoryId { get; set; }
        public bool ShowPopup { get; set; }
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }
        [StringLength(150)]
        public string CategoryDescription { get; set; }
        public short DisplayFlag { get; set; }
        [StringLength(10)]
        public string LanguageCode { get; set; }
        [StringLength(250)]
        public string Message { get; set; }
        [StringLength(4000)]
        public string ValueDescriptions { get; set; }
    }
}
