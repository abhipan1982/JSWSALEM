using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.TransferModels
{
    [Keyless]
    [Table("HPS_L2_TcpAlarm", Schema = "xfr")]
    public partial class HPS_L2_TcpAlarm
    {
        [StringLength(4)]
        public string Length { get; set; }
        [StringLength(4)]
        public string Id { get; set; }
        [StringLength(1)]
        public string LowAirFlow { get; set; }
        [StringLength(1)]
        public string NoAirFlow { get; set; }
        [StringLength(1)]
        public string HiTemp { get; set; }
        [StringLength(1)]
        public string HardwareFault { get; set; }
        [StringLength(1)]
        public string TempWarning { get; set; }
        [StringLength(1)]
        public string SensorHardwareFault { get; set; }
        [StringLength(1)]
        public string SensorFault { get; set; }
        [StringLength(1)]
        public string Spare { get; set; }
        [StringLength(1)]
        public string AlarmChange { get; set; }
        [StringLength(3)]
        public string TempFrame { get; set; }
        [StringLength(4)]
        public string TempBar { get; set; }
        [StringLength(2)]
        public string AirFlow { get; set; }
        [StringLength(1)]
        public string OutOfTolerance { get; set; }
        [StringLength(1)]
        public string TopBottOutOfTol { get; set; }
        [StringLength(10)]
        public string LiftTablePos { get; set; }
        [StringLength(1)]
        public string FanOn { get; set; }
        [StringLength(1)]
        public string SystemReady { get; set; }
        [StringLength(1)]
        public string ObjectPresent { get; set; }
        [StringLength(1)]
        public string PyrometerError { get; set; }
        [StringLength(1)]
        public string LiftServoError { get; set; }
        [StringLength(1)]
        public string MeasuringActive { get; set; }
        [StringLength(1)]
        public string ExternalControlError { get; set; }
        [StringLength(1)]
        public string SyncError { get; set; }
        [StringLength(1)]
        public string OutOfMeasuringRange { get; set; }
        [StringLength(1)]
        public string DopplerFault { get; set; }
        [StringLength(1)]
        public string GaugeTempOutOfRange { get; set; }
        [StringLength(1)]
        public string GaugeIsNotInAutoMode { get; set; }
        [StringLength(1)]
        public string TolWarning { get; set; }
        [StringLength(1)]
        public string DataFromLevel2Missing { get; set; }
    }
}
