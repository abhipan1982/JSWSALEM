using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.Models.DataContracts.Internal.TCP
{
  [DataContract]
  [Serializable]
  public class DCHPSTcpAlarm : DataContractBase
  {
    [DataMember] public char Length { get; set; }
    [DataMember] public char Id { get; set; }
    [DataMember] public char LowAirFlow { get; set; }
    [DataMember] public char NoAirFlow { get; set; }
    [DataMember] public char HiTemp { get; set; }
    [DataMember] public char HardwareFault { get; set; }
    [DataMember] public char TempWarning { get; set; }
    [DataMember] public char SensorHardwareFault { get; set; }
    [DataMember] public char SensorFault { get; set; }
    [DataMember] public char Spare { get; set; }
    [DataMember] public char AlarmChange { get; set; }
    [DataMember] public char TempFrame { get; set; }
    [DataMember] public char TempBar { get; set; }
    [DataMember] public char AirFlow { get; set; }
    [DataMember] public char OutOfTolerance { get; set; }
    [DataMember] public char TopBottOutOfTol { get; set; }
    [DataMember] public char LiftTablePos { get; set; }
    [DataMember] public char FanOn { get; set; }
    [DataMember] public char SystemReady { get; set; }
    [DataMember] public char ObjectPresent { get; set; }
    [DataMember] public char PyrometerError { get; set; }
    [DataMember] public char LiftServoError { get; set; }
    [DataMember] public char MeasuringActive { get; set; }
    [DataMember] public char ExternalControlError { get; set; }
    [DataMember] public char SyncError { get; set; }
    [DataMember] public char OutOfMeasuringRange { get; set; }
    [DataMember] public char DopplerFault { get; set; }
    [DataMember] public char GaugeTempOutOfRange { get; set; }
    [DataMember] public char GaugeIsNotInAutoMode { get; set; }
    [DataMember] public char TolWarning { get; set; }
    [DataMember] public char DataFromLevel2Missing { get; set; }


  }
}
