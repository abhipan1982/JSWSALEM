using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement
{
  public class VM_RollSetOverviewFull : VM_Base
  {
    #region ctor

    public VM_RollSetOverviewFull() { }

    public VM_RollSetOverviewFull(short position)
    {
      RollSetId = -1;
      RollSetStatus = RollSetStatus.Undefined;
      RollSetStatusNew = 0;
      RollSetTypeEnum = BaseDbEntity.EnumClasses.RollSetType.Undefined;
      RollSetName = "";
      UpperRollId = null;
      UpperActualDiameter = null;
      UpperRollName = "";
      UpperRollTypeName = "";
      BottomRollId = null;
      BottomActualDiameter = null;
      BottomRollName = "";
      BottomRollTypeName = "";
      RollSetHistoryId = null;
      RollSetHistoryStatus = 0;
      CassetteId = 0;
      CassetteName = "";
      PositionInCassette = position;
      StandNo = null;
      MountedTs = null;
      DismountedTs = null;
      RollSetStatusActual = "";
      EnumCassetteStatus = 0;
      UpperRollTypeId = null;
      BottomRollTypeId = null;
      ThirdRollId = null;
      ThirdActualDiameter = null;
      ThirdRollName = "";
      ThirdRollTypeName = "";
      ThirdRollTypeId = null;
      IsThirdRoll = null;   // 0 = RM , 1 = IM, 2 = K500, 3 = K370
      InterCassetteId = null;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_RollSetOverviewFull(V_RollSetOverview model)
    {
      RollSetId = model.RollSetId;
      RollSetStatus = RollSetStatus.GetValue(model.EnumRollSetStatus);
      EnumRollSetStatus = model.EnumRollSetStatus;
      RollSetStatusNew = model.EnumRollSetStatus;
      RollSetTypeEnum = BaseDbEntity.EnumClasses.RollSetType.GetValue(model.RollSetType);
      RollSetType = model.RollSetType;
      RollSetName = model.RollSetName;
      UpperRollId = model.UpperRollId;
      UpperActualDiameter = model.UpperActualDiameter;
      UpperRollName = model.UpperRollName;
      UpperRollTypeName = model.UpperRollTypeName;
      BottomRollId = model.BottomRollId;
      BottomActualDiameter = model.BottomActualDiameter;
      BottomRollName = model.BottomRollName;
      BottomRollTypeName = model.BottomRollTypeName;
      RollSetHistoryId = model.RollSetHistoryId;
      RollSetHistoryStatus = model.EnumRollSetHistoryStatus;
      CassetteName = model.CassetteName;
      PositionInCassette = model.PositionInCassette;
      StandNo = model.StandNo;
      StandName = model.StandName;
      MountedTs = model.MountedTs;
      DismountedTs = model.DismountedTs;
      RollSetStatusActual = "";
      UpperRollTypeId = model.UpperRollTypeId;
      BottomRollTypeId = model.BottomRollTypeId;
      ThirdRollId = model.ThirdRollId;
      ThirdActualDiameter = model.ThirdActualDiameter;
      ThirdRollName = model.ThirdRollName;
      ThirdRollTypeName = model.ThirdRollTypeName;
      ThirdRollTypeId = model.ThirdRollTypeId;
      IsThirdRoll = Convert.ToInt16(model.IsThirdRoll);
      RollSetDescription = model.RollSetDescription;
      //condition used for proper ViewBag.CassReadyNew with CassetteId selection when model.CassetteId is null
      if ((model.EnumRollSetStatus == RollSetStatus.Ready.Value) || (model.EnumRollSetStatus == RollSetStatus.NotAvailable.Value))
      {
        CassetteId = 0;
      }
      else
      {
        CassetteId = model.CassetteId;
      }
      CassetteName = model.CassetteName;
      EnumCassetteStatus = model.EnumCassetteStatus;
      GrooveTemplateName = model.GrooveTemplateName;
      EnumGrooveSetting = model.EnumGrooveSetting;
      Editable = model.EnumRollSetStatus == RollSetStatus.Undefined
        || (model.EnumRollSetStatus == RollSetStatus.Empty)
        || (model.EnumRollSetStatus == RollSetStatus.Ready)
        || (model.EnumRollSetStatus == RollSetStatus.Dismounted)
        || (model.EnumRollSetStatus == RollSetStatus.NotAvailable);
      Assemblable = model.EnumRollSetStatus == RollSetStatus.Empty;
      Disassemblable = model.EnumRollSetStatus == RollSetStatus.Ready
        || model.EnumRollSetStatus == RollSetStatus.NotAvailable;
      PassGrooveChangeable = model.EnumRollSetStatus == RollSetStatus.Mounted;
      Removable = model.EnumRollSetStatus == RollSetStatus.Undefined
        || model.EnumRollSetStatus == RollSetStatus.Empty;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region properties
    [SmfDisplay(typeof(VM_RollSetOverviewFull), "RollSetId", "NAME_RollSetName")]
    public virtual long? RollSetId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "RollSetStatus", "NAME_RollsetStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual RollSetStatus RollSetStatus { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "EnumRollSetStatus", "NAME_RollsetStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short EnumRollSetStatus { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "RollSetStatusNew", "NAME_RollsetStatusNew")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? RollSetStatusNew { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "RollSetTypeEnum", "NAME_RollsetType")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual RollSetType RollSetTypeEnum { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_RollSetOverviewFull), "RollSetType", "NAME_RollsetType")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short RollSetType { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_RollSetOverviewFull), "RollSetName", "NAME_RollSetName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollSetName { get; set; }

    //[SmfRequired]
    [SmfDisplay(typeof(VM_RollSetOverviewFull), "UpperRollId", "NAME_RollUpperName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? UpperRollId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "UpperActualDiameter", "NAME_DiameterUpper")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? UpperActualDiameter { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "UpperRollName", "NAME_RollUpperName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string UpperRollName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "UpperRollTypeName", "NAME_RollTypeUpper")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string UpperRollTypeName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "UpperRollTypeId", "NAME_RollTypeUpper")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? UpperRollTypeId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "BottomRollId", "NAME_RollBottomName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? BottomRollId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "BottomActualDiameter", "NAME_DiameterBottom")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? BottomActualDiameter { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "BottomRollName", "NAME_RollBottomName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string BottomRollName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "BottomRollTypeName", "NAME_RollTypeBottom")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string BottomRollTypeName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "BottomRollTypeId", "NAME_RollTypeBottom")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? BottomRollTypeId { get; set; }

    public virtual long? RollSetHistoryId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "RollSetHistoryStatus", "NAME_RollsetHistoryStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? RollSetHistoryStatus { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "CassetteName", "NAME_CassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string CassetteName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "PositionInCassette", "NAME_PositionInCassette")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? PositionInCassette { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "StandNo", "NAME_StandNo")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? StandNo { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "MountedTs", "NAME_Mounted")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual DateTime? MountedTs { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "DismountedTs", "NAME_Dismounted")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual DateTime? DismountedTs { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "RollStatusName", "NAME_RollsetStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollStatusName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "CassetteId", "NAME_CassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? CassetteId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "RollSetStatusActual", "NAME_RollSetStatusActual")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollSetStatusActual { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "EnumCassetteStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? EnumCassetteStatus { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "ThirdRollId", "NAME_ThirdRollId")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? ThirdRollId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "ThirdActualDiameter", "NAME_DiameterThird")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? ThirdActualDiameter { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "ThirdRollName", "NAME_RollThirdName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string ThirdRollName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "ThirdRollTypeName", "NAME_RollTypeThird")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string ThirdRollTypeName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "ThirdRollTypeId", "NAME_RollTypeIdThird")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? ThirdRollTypeId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "IsThirdRoll", "NAME_IsThirdRoll")]
    public virtual short? IsThirdRoll { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "InterCassetteId", "NAME_InterCassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? InterCassetteId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "InterCassetteName", "NAME_InterCassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string InterCassetteName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "InternalCassetteStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? InternalCassetteStatus { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "GrooveTemplateId", "NAME_GrooveTemplate")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? GrooveTemplateId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "RollSetDescription", "NAME_Description")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollSetDescription { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "StandName", "NAME_StandName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string StandName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "GrooveTemplateName", "NAME_ActiveGroove")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string GrooveTemplateName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewFull), "EnumGrooveSetting", "NAME_GrooveSettingForColoring")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? EnumGrooveSetting { get; set; }

    public bool Editable { get; set; }

    public bool Assemblable { get; set; }

    public bool Disassemblable { get; set; }

    public bool PassGrooveChangeable { get; set; }

    public bool Removable { get; set; }

    #endregion
  }
}
