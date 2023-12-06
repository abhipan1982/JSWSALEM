using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_Setup
    {
        public long SetupTypeId { get; set; }
        [Required]
        [StringLength(10)]
        public string SetupTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string SetupTypeName { get; set; }
        public long SetupId { get; set; }
        [StringLength(10)]
        public string SetupCode { get; set; }
        [Required]
        [StringLength(50)]
        public string SetupName { get; set; }
        public long? ParameterId { get; set; }
        [StringLength(10)]
        public string ParameterCode { get; set; }
        [StringLength(50)]
        public string ParameterName { get; set; }
        public long? ParameterValueId { get; set; }
        public bool? IsRequiredParameter { get; set; }
    }
}
