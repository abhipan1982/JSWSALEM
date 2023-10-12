using System;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  [DataContract]
  public class DCTrackingEvent : DataContractBase
  {
    /// <summary>
    ///   Billet unique identification
    /// </summary>
    [DataMember]
    public long BasId { get; set; }

    /// <summary>
    ///   Max value
    /// </summary>
    [DataMember]
    public DateTime TriggerDate { get; set; }

    /// <summary>
    ///   Asset code
    /// </summary>
    [DataMember]
    public int AssetCode { get; set; }

    /// <summary>
    ///   Is tracking area
    /// </summary>
    [DataMember]
    public bool IsArea { get; set; }

    /// <summary>
    ///   Trigger type
    /// </summary>
    [DataMember]
    public TrackingEventType EventType { get; set; }

    /// <summary>
    ///   InitialLength
    /// </summary>
    [DataMember]
    public double? InitialLength { get; set; }

    /// <summary>
    ///   InitialWeight
    /// </summary>
    [DataMember]
    public double? InitialWeight { get; set; }

    /// <summary>
    ///   InitialTemperature
    /// </summary>
    [DataMember]
    public double? InitialTemperature { get; set; }

    /// <summary>
    ///   IsRejected
    /// </summary>
    [DataMember]
    public bool IsRejected { get; set; }
  }
}
