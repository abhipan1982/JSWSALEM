using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.Models.DataContracts.Internal.TCP
{
  [DataContract]
  [Serializable]
  public class DCHPSTcpAlarmExt : DataContractBase
  {
    [DataMember] public char Length { get; set; }
    [DataMember] public char Id { get; set; }
    [DataMember] public char Updated { get; set; }
    [DataMember] public char AirFlowHardwareFault { get; set; }
    [DataMember] public char FrameFlowHardwareFault { get; set; }
    [DataMember] public char PyrometerHardwareFault { get; set; }
    [DataMember] public char HeightPosSensorHardwareFault { get; set; }
    [DataMember] public char LengthEncoderHardwareFault { get; set; }
    [DataMember] public char ProfiCura_ExposureTime_Alarm { get; set; }
    [DataMember] public char Spare_199 { get; set; }
   


  }
}
