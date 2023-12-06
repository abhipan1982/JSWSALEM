using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_NewestAlarmsList
    {
        public long RowNumber { get; set; }
        public long AlarmId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdateTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AlarmDate { get; set; }
        [StringLength(30)]
        public string AlarmOwner { get; set; }
        [StringLength(150)]
        public string ShortDescription { get; set; }
        public long AlarmDefinitionId { get; set; }
        public bool? Confirmation { get; set; }
        [StringLength(128)]
        public string ConfirmedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ConfirmationDate { get; set; }
        public short DisplayFlag { get; set; }
        [StringLength(50)]
        public string AlarmCode { get; set; }
        [StringLength(300)]
        public string Message { get; set; }
        [Required]
        [StringLength(50)]
        public string AlarmCategoryName { get; set; }
        public int AlarmType { get; set; }
        [StringLength(10)]
        public string LanguageCode { get; set; }
        public bool ToConfirm { get; set; }
        public bool ShowPopup { get; set; }
    }
}
