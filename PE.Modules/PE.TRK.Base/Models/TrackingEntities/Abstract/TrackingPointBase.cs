using System;
using PE.BaseDbEntity.EnumClasses;

namespace PE.TRK.Base.Models.TrackingEntities.Abstract
{
  [Serializable]
  public abstract class TrackingPointBase
  {
    public TrackingPointBase(int occupiedFeatureCode, int assetCode, int stepAssetCode, int sequence)
    {
      OccupiedFeatureCode = occupiedFeatureCode;
      AssetCode = assetCode;
      StepAssetCode = stepAssetCode;
      Sequence = sequence;
    }

    public int OccupiedFeatureCode { get; }

    public int AssetCode { get; }

    public int Sequence { get; }

    /// <summary>
    ///   Flag for HeadReceived signal
    /// </summary>
    public bool HeadReceived { get; protected set; }
    
    public DateTime? HeadReceivedDate { get; protected set; }

    /// <summary>
    ///   Flag for TailReceived signal
    /// </summary>
    public bool TailReceived { get; protected set; }

    public DateTime? TailReceivedDate { get; protected set; }

    public int StepAssetCode { get; protected set; }
  }
}
