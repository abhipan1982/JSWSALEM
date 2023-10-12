using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class FactShift
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public bool FactShiftIsDeleted { get; set; }
        public long FactShiftRow { get; set; }
        [MaxLength(16)]
        public byte[] FactShiftHash { get; set; }
        public long FactShiftKey { get; set; }
        public int DimYearKey { get; set; }
        public int DimDateKey { get; set; }
        [StringLength(4000)]
        public string DimYear { get; set; }
        [StringLength(4000)]
        public string DimMonth { get; set; }
        [Required]
        [StringLength(4000)]
        public string DimWeek { get; set; }
        [StringLength(4000)]
        public string DimDate { get; set; }
        [Required]
        [StringLength(10)]
        public string DimShiftCode { get; set; }
        [Required]
        [StringLength(50)]
        public string DimCrewName { get; set; }
        [Required]
        [StringLength(51)]
        public string ShiftDateWithCode { get; set; }
        public TimeSpan DefaultStartTime { get; set; }
        public TimeSpan DefaultEndTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ShiftStartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ShiftEndTime { get; set; }
        public int? ShiftDuration { get; set; }
        public long DelayDuration { get; set; }
        public int ChargeNumber { get; set; }
        public int DischargeNumber { get; set; }
        public int UnchargeNumber { get; set; }
        public int UndischargeNumber { get; set; }
        public int FullScrapNumber { get; set; }
        public int PartialScrapNumber { get; set; }
        public int RejectNumber { get; set; }
        public int ProductCreateNumber { get; set; }
        public int WorkOrdersNumber { get; set; }
        public int WorkOrdersDuration { get; set; }
        public double MaterialsWeight { get; set; }
        public double ScrappedMaterialsWeight { get; set; }
        public double RejectedMaterialsWeight { get; set; }
        public double ProductsWeight { get; set; }
    }
}
