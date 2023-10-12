using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay
{
  public class VM_RollsetDisplay : VM_Base
  {
    #region ctor

    public VM_RollsetDisplay() { }

    // For RM & IM
    public VM_RollsetDisplay(V_RollSetOverview model, IList<V_RollHistory> upperGroove)
    {
      RollSetId = model.RollSetId;
      RollSetName = model.RollSetName;
      RollSetStatus = RollSetStatus.GetValue(model.EnumRollSetStatus);
      EnumRollSetStatus = model.EnumRollSetStatus;
      RollSetType = model.RollSetType;
      RollSetHistoryId = (short)model.RollSetHistoryId;
      UpperRollId = model.UpperRollId;
      BottomRollId = model.BottomRollId;
      UpperRollName = model.UpperRollName;
      BottomRollName = model.BottomRollName;
      UpperActualDiameter = model.UpperActualDiameter;
      BottomActualDiameter = model.BottomActualDiameter;
      StandNo = model.StandNo;
      GrooveTemplateName = "";
      GrooveActualTemplate = "";
      GrooveActualRollUpper = new List<VM_GroovesRoll>();
      GrooveActualRollBottom = new List<VM_GroovesRoll>();
      NewUpperDiameter = model.UpperActualDiameter;
      NewBottomDiameter = model.BottomActualDiameter;

      foreach (V_RollHistory element in upperGroove)
      {
        GrooveActualRollUpper.Add(new VM_GroovesRoll(element));
      }

      UnitConverterHelper.ConvertToLocal(this);
    }

    // For Kocks
    public VM_RollsetDisplay(V_RollSetOverview model, IList<V_RollHistory> upperGroove, IList<V_RollHistory> bottomGroove, IList<V_RollHistory> thirdGroove)
    {
      RollSetId = Convert.ToInt64(model.RollSetId);
      RollSetName = model.RollSetName;
      RollSetStatus = (RollSetStatus)model.EnumRollSetStatus;
      RollSetType = model.RollSetType;
      RollSetHistoryId = (short)model.RollSetHistoryId;
      UpperRollId = model.UpperRollId;
      BottomRollId = model.BottomRollId;
      ThirdRollId = model.ThirdRollId;
      UpperRollName = model.UpperRollName;
      BottomRollName = model.BottomRollName;
      ThirdRollName = model.ThirdRollName;
      UpperActualDiameter = model.UpperActualDiameter;
      BottomActualDiameter = model.BottomActualDiameter;
      ThirdActualDiameter = model.ThirdActualDiameter;
      StandNo = model.StandNo;
      GrooveTemplateName = "";
      GrooveActualTemplate = "";
      NewUpperDiameter = model.UpperActualDiameter;
      BottomActualDiameter = model.BottomActualDiameter;
      NewThirdDiameter = model.ThirdActualDiameter;
      GrooveActualRollUpper = new List<VM_GroovesRoll>();
      GrooveActualRollBottom = new List<VM_GroovesRoll>();
      GrooveActualRollThird = new List<VM_GroovesRoll>();
      foreach (V_RollHistory element in upperGroove)
      {
        GrooveActualRollUpper.Add(new VM_GroovesRoll(element));
      }
      foreach (V_RollHistory element in bottomGroove)
      {
        GrooveActualRollBottom.Add(new VM_GroovesRoll(element));
      }
      foreach (V_RollHistory element in thirdGroove)
      {
        GrooveActualRollThird.Add(new VM_GroovesRoll(element));
      }

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region properties

    [SmfDisplay(typeof(VM_RollsetDisplay), "RollSetId", "NAME_RollSetName")]
    public virtual long RollSetId { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "RollSetName", "NAME_RollSetName")]
    public virtual string RollSetName { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "RollSetStatus", "NAME_RollsetStatus")]
    public virtual RollSetStatus RollSetStatus { get; set; }

    public virtual short EnumRollSetStatus { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "RollSetType", "NAME_RollsetType")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? RollSetType { get; set; }

    public virtual long RollSetHistoryId { get; set; }

    public virtual long? UpperRollId { get; set; }

    public virtual long? BottomRollId { get; set; }

    public virtual long? ThirdRollId { get; set; }

    public virtual List<VM_GroovesRoll> GrooveActualRollUpper { get; set; }

    public virtual List<VM_GroovesRoll> GrooveActualRollBottom { get; set; }

    public virtual List<VM_GroovesRoll> GrooveActualRollThird { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "UpperRollName", "NAME_RollUpperName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string UpperRollName { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "BottomRollName", "NAME_RollBottomName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string BottomRollName { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "ThirdRollName", "NAME_RollThirdName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string ThirdRollName { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "UpperActualDiameter", "NAME_DiameterUpper")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? UpperActualDiameter { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "BottomActualDiameter", "NAME_DiameterBottom")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? BottomActualDiameter { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "ThirdActualDiameter", "NAME_DiameterThird")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? ThirdActualDiameter { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "NewUpperDiameter", "NAME_NewDiameter")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? NewUpperDiameter { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "NewBottomDiameter", "NAME_NewDiameter")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? NewBottomDiameter { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "NewThirdDiameter", "NAME_NewDiameter")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? NewThirdDiameter { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "AccWeightLabel", "NAME_AccWeight")]
    [SmfUnit("UNIT_WeightTons")]
    [SmfFormat("FORMAT_HeatWeight")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? AccWeightLabel { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "AccWeightWithCoeffLabel", "NAME_GrooveWeightCalculated")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfUnit("UNIT_WeightTons")]
    [SmfFormat("FORMAT_HeatWeight")]
    public double? AccWeightWithCoeffLabel { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "AccWeightLimitLabel", "NAME_AccWeightLimit_ToTable")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfUnit("UNIT_WeightTons")]
    [SmfFormat("FORMAT_HeatWeight")]
    public virtual double? AccWeightLimitLabel { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "AccBilletCntLabel", "NAME_AccBilletCnt")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? AccBilletCntLabel { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "AccBilletCntLimitLabel", "NAME_AccBilletCntLimit_ToTable")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? AccBilletCntLimitLabel { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "GrooveTemplateName", "NAME_GrooveTemplateName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string GrooveTemplateName { get; set; }

    [SmfDisplay(typeof(VM_GroovesRoll), "GrooveConditionDescription", "NAME_GrooveCondition")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string GrooveConditionDescription { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "GrooveActualTemplate", "NAME_GrooveActualTemplate")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string GrooveActualTemplate { get; set; }

    [SmfDisplay(typeof(VM_RollsetDisplay), "StandNo", "NAME_StandNo")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? StandNo { get; set; }

    #endregion
  }
}
