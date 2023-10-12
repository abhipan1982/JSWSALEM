using System;
using System.Collections.Generic;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.GrindingTurning
{
  public class VM_RollSetTurningHistory : VM_Base
  {
    #region ctor

    public VM_RollSetTurningHistory() { }

    public VM_RollSetTurningHistory(V_RollSetOverview model, IList<V_RollHistory> upperGroove, IList<V_RollHistory> bottomGroove, IList<V_RollHistory> thirdGroove)
    {
      RollSetId = model.RollSetId;
      RollSetName = model.RollSetName;
      EnumRollSetStatus = model.EnumRollSetHistoryStatus;
      RollSetType = model.RollSetType;
      RollSetHistoryId = model.RollSetHistoryId;
      EnumRollSetHistoryStatus = model.EnumRollSetHistoryStatus;
      MountedTs = model.MountedTs;
      DismountedTs = model.DismountedTs;
      UpperRollId = model.UpperRollId ?? 0;
      BottomRollId = model.BottomRollId ?? 0;
      ThirdRollId = model.ThirdRollId ?? 0;
      UpperRollName = model.UpperRollName;
      BottomRollName = model.BottomRollName;
      ThirdRollName = model.ThirdRollName;
      GrooveActualRollUpper = new List<VM_GroovesRoll>();
      foreach (V_RollHistory upperHistoryListElement in upperGroove)
      {
        GrooveActualRollUpper.Add(new VM_GroovesRoll(upperHistoryListElement));
        ActualUpperDiameter = upperHistoryListElement.ActualDiameter;
      }
      GrooveActualRollBottom = new List<VM_GroovesRoll>();
      foreach (V_RollHistory bottomHistoryListElement in bottomGroove)
      {
        GrooveActualRollBottom.Add(new VM_GroovesRoll(bottomHistoryListElement));
        ActualBottomDiameter = bottomHistoryListElement.ActualDiameter;
      }
      GrooveActualRollThird = new List<VM_GroovesRoll>();
      foreach (V_RollHistory thirdHistoryListElement in thirdGroove)
      {
        GrooveActualRollThird.Add(new VM_GroovesRoll(thirdHistoryListElement));
        ActualThirdDiameter = thirdHistoryListElement.ActualDiameter;
      }
      GrooveTemplateName = "";
      RollSetHistoryName = "";

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_RollSetTurningHistory(V_RollSetOverview model, IList<V_RollHistory> upperGroove, IList<V_RollHistory> bottomGroove)
    {
      RollSetId = model.RollSetId;
      RollSetName = model.RollSetName;
      EnumRollSetStatus = model.EnumRollSetStatus;
      RollSetType = model.RollSetType;
      RollSetHistoryId = (short)model.RollSetHistoryId;
      EnumRollSetHistoryStatus = model.EnumRollSetHistoryStatus;
      MountedTs = model.MountedTs;
      DismountedTs = model.DismountedTs;
      UpperRollId = model.UpperRollId ?? 0;
      BottomRollId = model.BottomRollId ?? 0;
      ThirdRollId = model.ThirdRollId ?? 0;
      UpperRollName = model.UpperRollName;
      BottomRollName = model.BottomRollName;
      ThirdRollName = model.ThirdRollName;
      GrooveActualRollUpper = new List<VM_GroovesRoll>();
      foreach (V_RollHistory upperHistoryListElement in upperGroove)
      {
        GrooveActualRollUpper.Add(new VM_GroovesRoll(upperHistoryListElement));
        ActualUpperDiameter = upperHistoryListElement.ActualDiameter;
      }
      GrooveActualRollBottom = new List<VM_GroovesRoll>();
      foreach (V_RollHistory bottomHistoryListElement in bottomGroove)
      {
        GrooveActualRollBottom.Add(new VM_GroovesRoll(bottomHistoryListElement));
        ActualBottomDiameter = bottomHistoryListElement.ActualDiameter;
      }
      GrooveActualRollThird = new List<VM_GroovesRoll>();

      GrooveTemplateName = "";
      RollSetHistoryName = "";

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region properties

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "RollSetId", "NAME_RollSetName")]
    public virtual long RollSetId { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "RollSetName", "NAME_RollSetName")]
    public virtual string RollSetName { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "EnumRollSetStatus", "NAME_RollsetStatus")]
    public virtual short EnumRollSetStatus { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "RollSetType", "NAME_RollsetType")]
    public virtual short RollSetType { get; set; }

    public virtual long RollSetHistoryId { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "MountedTs", "NAME_Mounted")]
    public virtual DateTime? MountedTs { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "DismountedTs", "NAME_Dismounted")]
    public virtual DateTime? DismountedTs { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "EnumRollSetHistoryStatus", "NAME_RollsetHistoryStatus")]
    public virtual short EnumRollSetHistoryStatus { get; set; }

    public virtual long UpperRollId { get; set; }

    public virtual long BottomRollId { get; set; }

    public virtual long ThirdRollId { get; set; }

    public virtual List<VM_GroovesRoll> GrooveActualRollUpper { get; set; }

    public virtual List<VM_GroovesRoll> GrooveActualRollBottom { get; set; }

    public virtual List<VM_GroovesRoll> GrooveActualRollThird { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "UpperRollName", "NAME_RollUpperName")]
    public virtual string UpperRollName { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "BottomRollName", "NAME_RollBottomName")]
    public virtual string BottomRollName { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "ThirdRollName", "NAME_RollThirdName")]
    public virtual string ThirdRollName { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "ActualUpperDiameter", "NAME_DiameterUpper")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    public virtual double ActualUpperDiameter { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "ActualBottomDiameter", "NAME_DiameterBottom")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    public virtual double ActualBottomDiameter { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "ActualThirdDiameter", "NAME_DiameterThird")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    public virtual double ActualThirdDiameter { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "AccWeightLimitLabel", "NAME_AccWeightLimit_ToTable")]
    [SmfUnit("UNIT_WeightTons")]
    [SmfFormat("FORMAT_HeatWeight")]
    public virtual double? AccWeightLimitLabel { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "AccBilletCntLimitLabel", "NAME_AccBilletCntLimit_ToTable")]
    public virtual long? AccBilletCntLimitLabel { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "AccWeight", "NAME_AccWeight")]
    [SmfUnit("UNIT_WeightTons")]
    [SmfFormat("FORMAT_HeatWeight")]
    public double AccWeight { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "AccWeightWithCoeff", "NAME_GrooveWeightCalculated")]
    [SmfUnit("UNIT_WeightTons")]
    [SmfFormat("FORMAT_HeatWeight")]
    public virtual double? AccWeightWithCoeff { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "AccBilletCnt", "NAME_AccBilletCnt")]
    public long AccBilletCnt { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "GrooveTemplateName", "NAME_GrooveTemplateName")]
    public string GrooveTemplateName { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "RollSetHistoryName", "NAME_RollSetHistoryName")]
    public string RollSetHistoryName { get; set; }

    public long? CassetteId { get; set; }

    public short? PositionInCassette { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "GrooveRemark", "NAME_Remark")]
    public string GrooveRemark { get; set; }

    [SmfDisplay(typeof(VM_RollSetTurningHistory), "GrooveCondition", "NAME_GrooveCondition")]
    public string GrooveCondition { get; set; }

    #endregion
  }
}
