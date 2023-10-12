using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module
{
  public class VM_GroovesRoll : VM_Base
  {
    #region ctor

    public VM_GroovesRoll() { }

    public VM_GroovesRoll(V_RollHistory model)
    {
      AccWeight = model.AccWeight;
      AccWeightLimit = model.AccWeightLimit ?? 0;
      AccBilletCntLimit = model.AccBilletCntLimit ?? 0;
      AccWeightWithCoeff = model.AccWeightWithCoeff;
      EnumGrooveCondition = model.EnumGrooveCondition;
      AccBilletCnt = model.AccBilletCnt;
      GrooveTemplateId = model.GrooveTemplateId;
      GrooveTemplateName = model.GrooveTemplateName;
      EnumRollGrooveStatus = model.EnumRollGrooveStatus;
      GrooveShortName = model.GrooveTemplateName;
      GrooveNumber = model.GrooveNumber;
      EnumGrooveSetting = model.EnumGrooveSetting;
      GrooveHistoryId = model.RollGrooveHistoryId;
      GrooveRemark = model.GrooveRemarks;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_GroovesRoll(RLSRollGroovesHistory model)
    {
      GrooveHistoryId = model.RollGrooveHistoryId;
      AccWeight = model.AccWeight;
      AccWeightWithCoeff = model.AccWeightWithCoeff;
      GrooveRemark = model.Remarks;
      if (model.EnumGrooveCondition != null)
      {
        EnumGrooveCondition = model.EnumGrooveCondition;
      }

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region properties

    [SmfDisplay(typeof(VM_GroovesRoll), "GrooveHistoryId", "NAME_GrooveHistoryId")]
    public virtual long GrooveHistoryId { get; set; }

    [SmfUnit("UNIT_WeightTons")]
    [SmfFormat("FORMAT_Weight")]
    [SmfDisplay(typeof(VM_GroovesRoll), "AccWeight", "NAME_AccWeight")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public double? AccWeight { get; set; }

    [SmfUnit("UNIT_WeightTons")]
    [SmfFormat("FORMAT_Weight")]
    [SmfDisplay(typeof(VM_GroovesRoll), "AccWeightLimit", "NAME_AccWeightLimit_ToTable")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? AccWeightLimit { get; set; }

    [SmfDisplay(typeof(VM_GroovesRoll), "AccBilletCnt", "NAME_AccBilletCnt")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public long? AccBilletCnt { get; set; }

    [SmfDisplay(typeof(VM_GroovesRoll), "AccBilletCntLimit", "NAME_AccBilletCntLimit_ToTable")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? AccBilletCntLimit { get; set; }

    [SmfDisplay(typeof(VM_GroovesRoll), "GrooveTemplateId", "NAME_GrooveTemplateId")]
    public long? GrooveTemplateId { get; set; }

    [SmfDisplay(typeof(VM_GroovesRoll), "GrooveTemplateName", "NAME_GrooveTemplateName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string GrooveTemplateName { get; set; }

    [SmfDisplay(typeof(VM_GroovesRoll), "GrooveConfirmed", "NAME_GrooveConfirmed")]
    public virtual Boolean GrooveConfirmed { get; set; }

    [SmfDisplay(typeof(VM_GroovesRoll), "EnumRollGrooveStatus", "NAME_GrooveStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? EnumRollGrooveStatus { get; set; }

    [SmfDisplay(typeof(VM_GroovesRoll), "GrooveShortName", "NAME_GrooveNameShort")]
    public virtual string GrooveShortName { get; set; }

    [SmfDisplay(typeof(VM_GroovesRoll), "GrooveNumber", "NAME_GrooveNumber")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? GrooveNumber { get; set; }

    [SmfDisplay(typeof(VM_GroovesRoll), "AccWeightWithCoeff", "NAME_AccWeight")]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? AccWeightWithCoeff { get; set; }

    [SmfDisplay(typeof(VM_GroovesRoll), "EnumGrooveCondition", "NAME_GroveCondition")]
    public virtual short? EnumGrooveCondition { get; set; }

    public virtual short? EnumGrooveSetting { get; set; }

    [SmfDisplay(typeof(VM_GroovesRoll), "GrooveRemark", "NAME_Remark")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string GrooveRemark { get; set; }

    #endregion
  }
}
