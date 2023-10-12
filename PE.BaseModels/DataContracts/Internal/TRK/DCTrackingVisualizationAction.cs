using System;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  [Serializable]
  public class DCTrackingVisualizationAction : DataContractBase
  {
    /// <summary>
    ///   Id of raw material
    /// </summary>
    [DataMember]
    public long RawMaterialId { get; set; }

    /// <summary>
    ///   Time stamp when record has been inserted to transfer table
    /// </summary>
    [DataMember]
    public TrackingVisualizationAction TrackingVisualizationAction { get; set; }

    [DataMember] public long? NextAreaId { get; set; }
  }
}
