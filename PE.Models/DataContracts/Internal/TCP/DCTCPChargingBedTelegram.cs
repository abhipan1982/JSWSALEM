using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.Models.DataContracts.Internal.TCP
{
  [DataContract]
  [Serializable]
  public class DCTCPChargingBedTelegram : DataContractBase
  {
    [DataMember] public int New { get; set; }
    [DataMember] public int Forward_OCC { get; set; }
    [DataMember] public int Backward_OCC { get; set; }

    [DataMember] public int PYRO_OCC { get; set; }
    [DataMember] public int BIT01 { get; set; }
    [DataMember] public int BIT02 { get; set; }
    [DataMember] public int BIT03 { get; set; }
    [DataMember] public int BIT04 { get; set; }
    [DataMember] public int BIT05 { get; set; }
    [DataMember] public int BIT06 { get; set; }
    [DataMember] public int BIT07 { get; set; }

    [DataMember] public ushort MEAS_TEMP_ACT { get; set; }
    [DataMember] public ushort MEAS_VEL_ACT { get; set; }
  }
}
