using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.Models.DataContracts.Internal.TCP
{
  [DataContract]
  [Serializable]
  public class DCHPSTcpProduct : DataContractBase
  {
    [DataMember] public char Length { get; set; }
    [DataMember] public char Id { get; set; }
    [DataMember] public char BatchId { get; set; }
    [DataMember] public char ShapeTxt { get; set; }
    [DataMember] public short Shape { get; set; }
    [DataMember] public char QualityTxt { get; set; }
    [DataMember] public float K1 { get; set; }
    [DataMember] public float K2 { get; set; }
    [DataMember] public int RefTemp { get; set; }
    [DataMember] public char NomHeight { get; set; }
    [DataMember] public char NomWidth { get; set; }
    [DataMember] public char TolMaxVert { get; set; }
    [DataMember] public char TolMinVert { get; set; }
    [DataMember] public char TolMaxHor { get; set; }
    [DataMember] public char TolMinHor { get; set; }
    [DataMember] public char CornerRadius { get; set; }
    [DataMember] public int BarId { get; set; }
    [DataMember] public char TolDiagDiff { get; set; }
    [DataMember] public char TolUMax { get; set; }
    [DataMember] public char TolCentPos { get; set; }
    [DataMember] public char NewBatch { get; set; }
    [DataMember] public char Source { get; set; }
    [DataMember] public char GroveRadius_Upper { get; set; }
    [DataMember] public char GroveRadius_Lower { get; set; }
    [DataMember] public char LSBUnit { get; set; }
    [DataMember] public char Spare { get; set; }



  }
}
