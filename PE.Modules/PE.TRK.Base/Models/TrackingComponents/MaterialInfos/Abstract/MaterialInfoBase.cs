using System;
using System.Collections.Generic;
using System.Linq;
using PE.TRK.Base.Models._Base;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;

namespace PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Abstract
{
  public abstract class MaterialInfoBase : ITrackingInstructionDataContractBase
  {
    #region properties

    /// <summary>
    ///   Material Id (received on request)
    /// </summary>
    public long MaterialId { get; protected set; }

    public long? ParentMaterialId { get; protected set; }

    public int? DivideAssetCode { get; protected set; }

    public TrackingCorrelation CorrelationId { get; protected set; }

    public bool IsDummy { get; protected set; }

    public virtual List<TrackingHistoryItem> AreaHistoryItems { get; set; } = new List<TrackingHistoryItem>();

    #endregion properties

    #region ctor

    public MaterialInfoBase(long materialId)
    {
      MaterialId = materialId;
    }

    #endregion ctor

    #region public methods

    /// <summary>
    ///   Change IsDummy
    /// </summary>
    /// <param name="isDummy"></param>
    public virtual void ChangeIsDummy(bool isDummy)
    {
      IsDummy = isDummy;
    }

    /// <summary>
    ///   Change material Id
    /// </summary>
    /// <param name="materialId"></param>
    public virtual void ChangeMaterialId(long materialId)
    {
      MaterialId = materialId;
    }

    /// <summary>
    ///   Change parentMaterialId
    /// </summary>
    /// <param name="parentMaterialId"></param>
    public virtual void ChangeParentMaterialId(long parentMaterialId)
    {
      ParentMaterialId = parentMaterialId;
    }

    public virtual void ChangeDivideAssetCode(int divideAssetCode)
    {
      DivideAssetCode = divideAssetCode;
    }

    public virtual void ChangeCorrelationId(TrackingCorrelation correlationId)
    {
      CorrelationId = correlationId;
    }

    public virtual void AddHistoryItem(int areaCode, DateTime date, TrackingHistoryTypeEnum historyType)
    {
      AreaHistoryItems.Add(new TrackingHistoryItem()
      {
        AreaCode = areaCode,
        TrackingHistoryType = historyType,
        Date = date
      });
    }

    public virtual DateTime? GetHistoryItemDateByAreaCodeAndHistoryType(int areaCode, TrackingHistoryTypeEnum historyType)
    {
      return AreaHistoryItems
        .OrderByDescending(x => x.Date)
        .FirstOrDefault(x => x.AreaCode == areaCode && x.TrackingHistoryType == historyType)
        ?.Date;
    }

    #endregion public methods
  }
}
