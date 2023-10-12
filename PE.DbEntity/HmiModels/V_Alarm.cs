using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_Alarm
    {
        public long AlarmId { get; set; }
        [StringLength(50)]
        public string AlarmOwner { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AlarmDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ConfirmationDate { get; set; }
        [StringLength(450)]
        public string UserIdConfirmed { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        public long? LanguageId { get; set; }
        public bool IsToConfirm { get; set; }
        public short EnumAlarmType { get; set; }
        [Required]
        [StringLength(10)]
        public string CategoryCode { get; set; }
        [StringLength(4000)]
        public string MessageTextFilled { get; set; }
    }
}
