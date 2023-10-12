using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PE.BaseDbEntity.EnumClasses;
using PE.TRK.Base.Models.TrackingEntities.Abstract;

namespace PE.TRK.Base.Models.TrackingEntities.Concrete
{
  public class GetMaterialInfoEventArgs : EventArgs
  {
    #region ctor

    /// <summary>
    ///   ctor
    /// </summary>
    /// <param name="stepId"></param>
    public GetMaterialInfoEventArgs(int stepId)
    {
      StepId = stepId;
    }

    #endregion

    #region properties

    /// <summary>
    ///   Id tracking area
    /// </summary>
    public int StepId { get; }

    #endregion
  }

  public class CutEventArgs : EventArgs
  {
    #region ctor

    /// <summary>
    ///   ctor
    /// </summary>
    /// <param name="cutLength"></param>
    /// <param name="typeOfCut"></param>
    public CutEventArgs(int assetCode, double cutLength, TypeOfCut typeOfCut)
    {
      AssetCode = assetCode;
      CutLength = cutLength;
      TypeOfCut = typeOfCut;
    }

    #endregion

    #region properties

    /// <summary>
    ///   Asset code
    /// </summary>
    public int AssetCode { get; }

    /// <summary>
    ///   Cut length
    /// </summary>
    public double CutLength { get; }

    /// <summary>
    ///   TypeOfCut
    /// </summary>
    public TypeOfCut TypeOfCut { get; }

    #endregion
  }

  public class CbShearEventArgs : EventArgs
  {
    #region ctor

    /// <summary>
    ///   ctor
    /// </summary>
    /// <param name="assetCode"></param>
    /// <param name="cutLength"></param>
    /// <param name="slittingFactor"></param>
    /// <param name="isBilletEnd"></param>
    public CbShearEventArgs(int assetCode, double cutLength, short slittingFactor)
    {
      AssetCode = assetCode;
      CutLength = cutLength;
      SlittingFactor = slittingFactor;
    }

    #endregion

    #region properties

    /// <summary>
    ///   Asset code
    /// </summary>
    public int AssetCode { get; }

    /// <summary>
    ///   SlittingFactor
    /// </summary>
    public short SlittingFactor { get; }

    /// <summary>
    ///   Cut length
    /// </summary>
    public double CutLength { get; }

    #endregion
  }
  /// <summary>
  ///   TrackingEventEventArgs
  /// </summary>
  public class TrackingEventArgs : EventArgs
  {
    #region ctor

    public TrackingEventArgs(long materialId, int assetCode,
      bool isArea, TrackingEventType eventType, DateTime triggerDate)
    {
      MaterialId = materialId;
      AssetCode = assetCode;
      IsArea = isArea;
      TriggerDate = triggerDate;
      EventType = eventType;
    }

    #endregion

    #region properties

    public DateTime TriggerDate { get; }
    public int AssetCode { get; set; }
    public bool IsArea { get; set; }
    public long MaterialId { get; set; }
    public TrackingEventType EventType { get; set; }
    public double? Length { get; set; }
    public double? Weight { get; set; }
    public double? Temperature { get; set; }

    #endregion
  }
}
