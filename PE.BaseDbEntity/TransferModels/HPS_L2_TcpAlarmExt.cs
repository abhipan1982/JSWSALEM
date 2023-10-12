using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.TransferModels
{
    [Keyless]
    [Table("HPS_L2_TcpAlarmExt", Schema = "xfr")]
    public partial class HPS_L2_TcpAlarmExt
    {
        [StringLength(4)]
        public string Length { get; set; }
        [StringLength(4)]
        public string Id { get; set; }
        [StringLength(1)]
        public string Updated { get; set; }
        [StringLength(1)]
        public string AirFlowHardwareFault { get; set; }
        [StringLength(1)]
        public string FrameFlowHardwareFault { get; set; }
        [StringLength(1)]
        public string PyrometerHardwareFault { get; set; }
        [StringLength(1)]
        public string HeightPosSensorHardwareFault { get; set; }
        [StringLength(1)]
        public string LengthEncoderHardwareFault { get; set; }
        [StringLength(1)]
        public string ProfiCura_ExposureTime_Alarm { get; set; }
        [StringLength(10)]
        public string Spare_199 { get; set; }
    }
}
