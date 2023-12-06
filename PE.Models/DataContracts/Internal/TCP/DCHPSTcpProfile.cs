using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.Models.DataContracts.Internal.TCP
{
  [DataContract]
  [Serializable]
  public class DCHPSTcpProfile : DataContractBase
  {
    [DataMember] public char Length { get; set; }
    [DataMember] public char Id { get; set; }
    [DataMember] public char BarNo { get; set; }
    [DataMember] public char ProfNo { get; set; }
    [DataMember] public char Valid { get; set; }
    [DataMember] public char Statuss { get; set; }
    [DataMember] public char Q_Value1 { get; set; }
    [DataMember] public char Q_Value36 { get; set; }
    [DataMember] public char TempBar { get; set; }
    [DataMember] public char TempFrame { get; set; }
    [DataMember] public char Spare_97 { get; set; }
    [DataMember] public char Tilt { get; set; }
    [DataMember] public char LSBUnit { get; set; }
    [DataMember] public char X_000 { get; set; }
    [DataMember] public char X_719 { get; set; }
    [DataMember] public char Y_000 { get; set; }
    [DataMember] public char Y_719 { get; set; }
    [DataMember] public char Spare_4 { get; set; }
    [DataMember] public char MeasValid { get; set; }
    [DataMember] public float MidHeight { get; set; }
    [DataMember] public float MaxHeight { get; set; }
    [DataMember] public float MinHeight { get; set; }
    [DataMember] public float MidWidth { get; set; }
    [DataMember] public float MinWidth { get; set; }
    [DataMember] public float MaxWidth { get; set; }
    [DataMember] public float Diagonal1 { get; set; }
    [DataMember] public float Diagonal2 { get; set; }
    [DataMember] public float AngleRight { get; set; }
    [DataMember] public float AngleLeft { get; set; }
    [DataMember] public float uMax { get; set; }
    [DataMember] public float BarTemp { get; set; }
    [DataMember] public float PosMaxHeight { get; set; }
    [DataMember] public float PosMinHeight { get; set; }
    [DataMember] public float PosMaxWidth { get; set; }
    [DataMember] public float PosMinWidth { get; set; }
    [DataMember] public float CentPosX { get; set; }
    [DataMember] public float CentPosY { get; set; }
    [DataMember] public float ProfilePosition { get; set; }
    [DataMember] public float Velocity { get; set; }
    [DataMember] public char ProfilePosStatus { get; set; }
    [DataMember] public float LeftHeight { get; set; }
    [DataMember] public float RightHeight { get; set; }
    [DataMember] public float AvgDiameter { get; set; }
    [DataMember] public float WallThickAvg { get; set; }
    [DataMember] public float Diameter_at_0_degree { get; set; }
    [DataMember] public float Diameter_at_45_degree { get; set; }
    [DataMember] public float Diameter_at_90_degree { get; set; }
    [DataMember] public float Diameter_at_135_degree { get; set; }
    [DataMember] public float WallThickMin { get; set; }
    [DataMember] public float WallThickMax { get; set; }
    [DataMember] public float RadiusOvality { get; set; }
    [DataMember] public char MomTolerance { get; set; }
    [DataMember] public char Spare_3 { get; set; }
    [DataMember] public float RearCentPosXStraightnessX { get; set; }
    [DataMember] public float RearCentPosYStraightnessY { get; set; }
    [DataMember] public float Area { get; set; }
    [DataMember] public float LocalStraight { get; set; }
    [DataMember] public char AvgQValue { get; set; }
    [DataMember] public char Spare { get; set; }

  }
}
