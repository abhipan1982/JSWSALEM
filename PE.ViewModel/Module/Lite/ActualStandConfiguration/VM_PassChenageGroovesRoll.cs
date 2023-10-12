using System;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.ActualStandConfiguration
{
  public class VM_PassChangeGroovesRoll : VM_Base
  {
    #region properties

    [SmfUnit("UNIT_WeightTons")]
    [SmfFormat("FORMAT_OrderWeight")]
    [SmfDisplay(typeof(VM_PassChangeGroovesRoll), "AccWeight", "NAME_AccWeight")]
    public double AccWeight { get; set; }

    [SmfDisplay(typeof(VM_PassChangeGroovesRoll), "AccBilletCnt", "NAME_AccBilletCnt")]
    public long AccBilletCnt { get; set; }

    [SmfDisplay(typeof(VM_PassChangeGroovesRoll), "AccBilletCntLimit", "NAME_AccBilletCntLimit_ToTable")]
    public virtual long? AccBilletCntLimit { get; set; }

    [SmfDisplay(typeof(VM_PassChangeGroovesRoll), "GrooveTemplateId", "NAME_GrooveTemplateId")]
    public long GrooveTemplateId { get; set; }

    [SmfDisplay(typeof(VM_PassChangeGroovesRoll), "GrooveTemplateName", "NAME_GrooveTemplateName")]
    public string GrooveTemplateName { get; set; }

    [SmfDisplay(typeof(VM_PassChangeGroovesRoll), "GrooveConfirmed", "NAME_GrooveConfirmed")]
    public virtual Boolean GrooveConfirmed { get; set; }

    [SmfDisplay(typeof(VM_PassChangeGroovesRoll), "GroovesStatus", "NAME_GrooveStatus")]
    public virtual short EnumRollGrooveStatus { get; set; }

    [SmfDisplay(typeof(VM_PassChangeGroovesRoll), "GrooveShortName", "NAME_GrooveNameShort")]
    public virtual string GrooveShortName { get; set; }

    [SmfDisplay(typeof(VM_PassChangeGroovesRoll), "GrooveNumber", "NAME_GrooveNumber")]
    public virtual short GrooveNumber { get; set; }

    [SmfDisplay(typeof(VM_PassChangeGroovesRoll), "AccWeightRatio", "NAME_AccWeightRatio_ToTable")]
    [SmfFormat("FORMAT_Percent")]
    [SmfUnit("UNIT_Percent")]
    public virtual double? AccWeightRatio { get; set; }

    [SmfDisplay(typeof(VM_PassChangeGroovesRoll), "EstimatedPassChangeDate", "NAME_EstimatedProdDate")]
    public DateTime? EstimatedPassChangeDate { get; set; }

    #endregion

    #region ctor

    public VM_PassChangeGroovesRoll()
    {
    }

    public VM_PassChangeGroovesRoll(V_RollHistory rb, DateTime? estimationDateTime = null)
    {
      AccWeight = rb.AccWeight;
      AccBilletCnt = rb.AccBilletCnt;
      GrooveTemplateId = rb.GrooveTemplateId;
      GrooveTemplateName = rb.GrooveTemplateName;
      EnumRollGrooveStatus = rb.EnumRollGrooveStatus;
      GrooveShortName = rb.GrooveTemplateName;
      AccBilletCntLimit = 0;
      GrooveNumber = rb.GrooveNumber;
      AccWeightRatio = (rb.AccWeightLimit ?? 0.0) == 0.0 ? 0.0 : AccWeight / rb.AccWeightLimit;
      EstimatedPassChangeDate = estimationDateTime;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion
  }
}
