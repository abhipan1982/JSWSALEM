using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.TransferModels
{
    [Keyless]
    [Table("HPS_L2_TcpProfile", Schema = "xfr")]
    public partial class HPS_L2_TcpProfile
    {
        [StringLength(4)]
        public string Length { get; set; }
        [StringLength(4)]
        public string Id { get; set; }
        [StringLength(10)]
        public string BarNo { get; set; }
        [StringLength(5)]
        public string ProfNo { get; set; }
        [StringLength(1)]
        public string Valid { get; set; }
        [StringLength(2)]
        public string Statuss { get; set; }
        [StringLength(3)]
        public string Q_Value1 { get; set; }
        [StringLength(3)]
        public string Q_Value36 { get; set; }
        [StringLength(4)]
        public string TempBar { get; set; }
        [StringLength(4)]
        public string TempFrame { get; set; }
        [StringLength(10)]
        public string Spare_97 { get; set; }
        [StringLength(2)]
        public string Tilt { get; set; }
        [StringLength(1)]
        public string LSBUnit { get; set; }
        [StringLength(4)]
        public string X_000 { get; set; }
        [StringLength(4)]
        public string X_719 { get; set; }
        [StringLength(4)]
        public string Y_000 { get; set; }
        [StringLength(4)]
        public string Y_719 { get; set; }
        [StringLength(2)]
        public string Spare_4 { get; set; }
        [StringLength(1)]
        public string MeasValid { get; set; }
        public double? MidHeight { get; set; }
        public double? MaxHeight { get; set; }
        public double? MinHeight { get; set; }
        public double? MidWidth { get; set; }
        public double? MinWidth { get; set; }
        public double? MaxWidth { get; set; }
        public double? Diagonal1 { get; set; }
        public double? Diagonal2 { get; set; }
        public double? AngleRight { get; set; }
        public double? AngleLeft { get; set; }
        public double? uMax { get; set; }
        public double? BarTemp { get; set; }
        public double? PosMaxHeight { get; set; }
        public double? PosMinHeight { get; set; }
        public double? PosMaxWidth { get; set; }
        public double? PosMinWidth { get; set; }
        public double? CentPosX { get; set; }
        public double? CentPosY { get; set; }
        public double? ProfilePosition { get; set; }
        public double? Velocity { get; set; }
        [StringLength(2)]
        public string ProfilePosStatus { get; set; }
        public double? LeftHeight { get; set; }
        public double? RightHeight { get; set; }
        public double? AvgDiameter { get; set; }
        public double? WallThickAvg { get; set; }
        public double? Diameter_at_0_degree { get; set; }
        public double? Diameter_at_45_degree { get; set; }
        public double? Diameter_at_90_degree { get; set; }
        public double? Diameter_at_135_degree { get; set; }
        public double? WallThickMin { get; set; }
        public double? WallThickMax { get; set; }
        public double? RadiusOvality { get; set; }
        [StringLength(20)]
        public string MomTolerance { get; set; }
        [StringLength(5)]
        public string Spare_3 { get; set; }
        public double? RearCentPosXStraightnessX { get; set; }
        public double? RearCentPosYStraightnessY { get; set; }
        public double? Area { get; set; }
        public double? LocalStraight { get; set; }
        [StringLength(3)]
        public string AvgQValue { get; set; }
        [StringLength(3)]
        public string Spare { get; set; }
    }
}
