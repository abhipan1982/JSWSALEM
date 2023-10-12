using System;
using System.Linq;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseDbEntity.EnumClasses;

namespace PE.HMIWWW.ViewModel.Module.Lite.ActualStandConfiguration
{
  public class VM_RollChangeOperation : VM_Base
  {
    #region ctor

    public VM_RollChangeOperation() { }

    public VM_RollChangeOperation(RLSCassette model, V_RollSetOverview rollSetToBeDismounted, V_RollSetOverview rollSetBeMounted, RollChangeAction rollChangeAction)
    {
      StandId = model.FKStand?.StandId;
      StandNo = model.FKStand?.StandNo;
      CassetteId = model.CassetteId;
      CassetteName = model.CassetteName;
      EnumCassetteType = model.FKCassetteType.EnumCassetteType;
      EnumStandStatus = model.FKStand?.EnumStandStatus ?? StandStatus.Undefined.Value;
      Arrangement = model.Arrangement;
      if (rollSetBeMounted != null)
      {
        RollsetToBeMountedId = rollSetBeMounted.RollSetId;
        PositionInCassette = GetPositionInCassette(rollSetBeMounted.RollSetId, model.CassetteId);
        RollsetToBeMounted = rollSetToBeDismounted.RollSetName;
      }
      else
        RollsetToBeMountedId = -1;

      if (rollSetToBeDismounted != null)
      {
        RollsetToBeDismountedId = rollSetToBeDismounted.RollSetId;
        Mounted = rollSetToBeDismounted.MountedTs;
        RollsetToBeDismounted = rollSetToBeDismounted.RollSetName;
        PositionInCassette = GetPositionInCassette(rollSetToBeDismounted.RollSetId, model.CassetteId);
      }
      else
        RollsetToBeDismountedId = -1;

      rcAction = rollChangeAction;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region properties

    public virtual long? StandId { get; set; }

    [SmfDisplay(typeof(VM_RollChangeOperation), "StandNo", "NAME_StandNo")]
    public virtual short? StandNo { get; set; }

    [SmfDisplay(typeof(VM_RollChangeOperation), "CassetteId", "NAME_CassetteName")]
    public virtual long? CassetteId { get; set; }

    [SmfDisplay(typeof(VM_RollChangeOperation), "CassetteName", "NAME_CassetteName")]
    public virtual string CassetteName { get; set; }

    [SmfDisplay(typeof(VM_RollChangeOperation), "EnumCassetteType", "NAME_Name")]
    public virtual short? EnumCassetteType { get; set; }

    [SmfDisplay(typeof(VM_RollChangeOperation), "EnumStandStatus", "NAME_Status")]
    public virtual short? EnumStandStatus { get; set; }

    [SmfDisplay(typeof(VM_RollChangeOperation), "Arrangement", "NAME_Arrangement")]
    public virtual short? Arrangement { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "PositionInCassette", "NAME_PositionInCassette")]
    public virtual short? PositionInCassette { get; set; }

    [SmfDisplay(typeof(VM_RollChangeOperation), "RollsetToBeDismountedId", "NAME_Rollset4Dismounting")]
    public virtual long? RollsetToBeDismountedId { get; set; }

    [SmfDisplay(typeof(VM_RollChangeOperation), "RollsetToBeDismounted", "NAME_Rollset4Dismounting")]
    public virtual string RollsetToBeDismounted { get; set; }

    [SmfDisplay(typeof(VM_RollChangeOperation), "Mounted", "NAME_Mounted")]
    public virtual DateTime? Mounted { get; set; }

    [SmfDisplay(typeof(VM_RollChangeOperation), "RollsetToBeMountedId", "NAME_Rollset4Mounting")]
    public virtual long? RollsetToBeMountedId { get; set; }

    [SmfDisplay(typeof(VM_RollChangeOperation), "RollsetToBeMounted", "NAME_Rollset4Mounting")]
    public virtual string RollsetToBeMounted { get; set; }

    public virtual short? rcAction { get; set; }

    #endregion

    public short? GetPositionInCassette(long rollSetId, long cassetteId)
    {
      using PEContext ctx = new PEContext();
      return ctx.RLSRollSetHistories
        .FirstOrDefault(x => x.FKRollSetId == rollSetId && CassetteId == cassetteId && x.EnumRollSetHistoryStatus == RollSetHistoryStatus.Actual)
        .PositionInCassette;
    }
  }
}
