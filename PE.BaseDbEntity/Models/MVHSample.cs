using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKMeasurementId), Name = "NCI_MeasurementId")]
    public partial class MVHSample
    {
        [Key]
        public long SampleId { get; set; }
        public long FKMeasurementId { get; set; }
        [Required]
        public bool? IsValid { get; set; }
        public double SampleValue { get; set; }
        public double OffsetFromHead { get; set; }

        [ForeignKey(nameof(FKMeasurementId))]
        [InverseProperty(nameof(MVHMeasurement.MVHSamples))]
        public virtual MVHMeasurement FKMeasurement { get; set; }
    }
}
