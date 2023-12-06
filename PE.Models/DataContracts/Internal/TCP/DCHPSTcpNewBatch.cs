using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.Models.DataContracts.Internal.TCP
{
  [DataContract]
  [Serializable]
  public class DCHPSTcpNewBatch : DataContractBase
  {
    [DataMember] public char Length { get; set; }
    [DataMember] public char Id { get; set; }
    [DataMember] public char BatchId { get; set; }
    [DataMember] public short Shape { get; set; }
    [DataMember] public char SteelType { get; set; }
    [DataMember] public char NomHeight { get; set; }
    [DataMember] public char NomWidth { get; set; }
    [DataMember] public char TolMaxVert { get; set; }
    [DataMember] public char TolMinVert { get; set; }
    [DataMember] public char TolMaxHor { get; set; }
    [DataMember] public char TolMinHor { get; set; }
    [DataMember] public char CornerRadius { get; set; }
    [DataMember] public char RefTemp { get; set; }
    [DataMember] public int BarId { get; set; }
    [DataMember] public char TolDiagDiff { get; set; }
    [DataMember] public char TolUMax { get; set; }
    [DataMember] public char TolCentPos { get; set; }
    [DataMember] public char GroveRadius_Upper { get; set; }
    [DataMember] public char GroveRadius_Lower { get; set; }
   


  }
}
