using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.ActualStandConfiguration
{
  public class VM_PassChangeDataActual : VM_Base
  {
    #region ctor

    public VM_PassChangeDataActual() { }

    public VM_PassChangeDataActual(V_PassChangeDataActual model)
    {
      StandId = model.StandId;
      StandNo = model.StandNo;
      StandName = model.StandName;
      EnumStandStatus = model.EnumStandStatus;
      RollSetId = model.RollSetId;
      RollSetName = model.RollSetName ?? "";
      RollSetHistoryId = model.RollSetHistoryId;
      MountedTs = model.MountedTs;
      CassetteName = model.CassetteName ?? "";
      CassetteId = String.IsNullOrEmpty(model.CassetteName) ? 0 : model.CassetteId;
      PositionInCassette = model.PositionInCassette;
      Arrangement = model.Arrangement;
      AccBilletCnt = model.AccBilletCnt;
      AccBilletCntLimit = model.AccBilletCntLimit ?? 0;
      AccWeight = model.AccWeight;
      AccWeightWithCoeff = model.AccWeightWithCoeff;
      AccWeightLimit = model.AccWeightLimit ?? 0;
      RollTypeName = model.RollTypeName;
      EnumRollSetHistoryStatus = model.EnumRollSetHistoryStatus;
      ActualDiameter = model.ActualDiameter;
      GrooveNumber = model.GrooveNumber;
      GrooveTemplateId = model.GrooveTemplateId;
      GrooveTemplateName = model.GrooveTemplateName;
      EnumRollGrooveStatus = model.EnumRollGrooveStatus;
      AccBilletCntRatio = model.AccBilletCntRatio;
      AccWeightRatio = model.AccWeightRatio;
      AccWeightCoeffRatio = model.AccWeightCoeffRatio;
      Position = model.Position;
      RollSetType = model.RollSetType;
      EnumGrooveSetting = model.EnumGrooveSetting;
      IsOverdue = model.AccBilletCnt > (model.AccBilletCntLimit ?? 0) ||
        model.AccWeight > (model.AccWeightLimit ?? 0) ||
        model.AccWeightWithCoeff > (model.AccWeightLimit ?? 0);

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region properties

    public virtual long StandId { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "StandNo", "NAME_StandNo")]
    public virtual short StandNo { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "StandName", "NAME_StandName")]
    public virtual string StandName { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "EnumStandStatus", "NAME_Status")]
    public virtual short? EnumStandStatus { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "RollSetId", "NAME_RollSetName")]
    public virtual long? RollSetId { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "RollSetName", "NAME_RollSetName")]
    public virtual string RollSetName { get; set; }

    public virtual long? RollSetHistoryId { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "MountedTs", "NAME_Mounted")]
    public virtual DateTime? MountedTs { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "CassetteName", "NAME_CassetteName")]
    public virtual string CassetteName { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "CassetteId", "NAME_CassetteName")]
    public virtual long? CassetteId { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "PositionInCassette", "NAME_PositionInCassette")]
    public virtual short? PositionInCassette { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "Arrangement", "NAME_Arrangement")]
    public virtual short? Arrangement { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "AccBilletCntLimit", "NAME_AccBilletCntLimit_ToTable")]
    public virtual long? AccBilletCntLimit { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "AccBilletCnt", "NAME_AccBilletCnt_ToTable")]
    public virtual long AccBilletCnt { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "AccWeight", "NAME_AccWeight_ToTable")]
    [SmfUnit("UNIT_WeightTons")]
    [SmfFormat("FORMAT_HeatWeight")]
    public virtual double? AccWeight { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "AccWeightWithCoeff", "NAME_AccWeightWithCoeff_ToTable")]
    [SmfUnit("UNIT_WeightTons")]
    [SmfFormat("FORMAT_HeatWeight")]
    public virtual double? AccWeightWithCoeff { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "AccWeightLimit", "NAME_AccWeightLimit_ToTable")]
    [SmfUnit("UNIT_WeightTons")]
    [SmfFormat("FORMAT_HeatWeight")]
    public virtual double? AccWeightLimit { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "RollTypeName", "NAME_RollTypeName")]
    public virtual string RollTypeName { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "RollSetType", "NAME_RollsetType")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? RollSetType { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "EnumRollSetHistoryStatus", "NAME_RollsetHistoryStatus")]
    public virtual short EnumRollSetHistoryStatus { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "ActualDiameter", "NAME_DiameterActual")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    public virtual double? ActualDiameter { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "GrooveNumber", "NAME_Groove")]
    public virtual int GrooveNumber { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "GrooveTemplateId", "NAME_GrooveTemplateId")]
    public virtual long GrooveTemplateId { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "GrooveTemplateName", "NAME_GrooveTemplateName")]
    public virtual string GrooveTemplateName { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "EnumRollGrooveStatus", "NAME_GrooveStatus")]
    public virtual short EnumRollGrooveStatus { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "AccBilletCntRatio", "NAME_AccBilletCntRatio_ToTable")]
    [SmfFormat("FORMAT_Percent")]
    [SmfUnit("UNIT_Percent")]
    public virtual double? AccBilletCntRatio { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "AccWeightRatio", "NAME_AccWeightRatio_ToTable")]
    [SmfFormat("FORMAT_Percent")]
    [SmfUnit("UNIT_Percent")]
    public virtual double? AccWeightRatio { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "AccWeightCoeffRatio", "NAME_AccWeightCoeffRatio_ToTable")]
    [SmfFormat("FORMAT_Percent")]
    [SmfUnit("UNIT_Percent")]
    public virtual double? AccWeightCoeffRatio { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "Position", "NAME_Position")]
    public virtual short? Position { get; set; }

    [SmfDisplay(typeof(VM_PassChangeDataActual), "EnumGrooveSetting", "NAME_GrooveSettingForColoring")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short EnumGrooveSetting { get; set; }

    public bool IsOverdue { get; set; }

    #endregion
  }
}
