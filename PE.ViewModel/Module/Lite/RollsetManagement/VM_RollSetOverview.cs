using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement
{
  public class VM_RollSetOverview : VM_Base
  {
    #region ctor

    public VM_RollSetOverview() { }

    public VM_RollSetOverview(V_RollSetOverview model)
    {
      RollSetId = model.RollSetId;
      EnumRollSetStatus = model.EnumRollSetStatus;
      EnumRollSetStatusNew = model.EnumRollSetStatus;
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
      EnumRollSetHistoryStatus = model.EnumRollSetHistoryStatus;
      CassetteName = model.CassetteName;
      PositionInCassette = model.PositionInCassette;
      StandNo = model.StandNo;
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
      CassetteId = model.CassetteId;
      CassetteName = model.CassetteName;
      EnumCassetteStatus = model.EnumCassetteStatus;
      RollSetStatusTxt = ResxHelper.GetResxByKey(BaseDbEntity.EnumClasses.RollSetStatus.GetValue(model.EnumRollSetStatus));
      RollSetTypeTxt = ResxHelper.GetResxByKey(BaseDbEntity.EnumClasses.RollSetType.GetValue(model.RollSetType));

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region properties

    [SmfDisplay(typeof(VM_RollSetOverview), "RollSetId", "NAME_RollSetName")]
    public virtual long? RollSetId { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetOverview), "EnumRollSetStatus", "NAME_RollsetStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? EnumRollSetStatus { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetOverview), "RollSetStatusTxt", "NAME_RollsetStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollSetStatusTxt { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "EnumRollSetStatusNew", "NAME_RollsetStatusNew")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? EnumRollSetStatusNew { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "RollSetType", "NAME_RollsetType")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? RollSetType { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "RollSetTypeTxt", "NAME_RollsetType")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollSetTypeTxt { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetOverview), "RollSetName", "NAME_RollSetName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollSetName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "UpperRollId", "NAME_RollUpperName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? UpperRollId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "UpperActualDiameter", "NAME_DiameterUpper")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? UpperActualDiameter { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetOverview), "UpperRollName", "NAME_RollUpperName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string UpperRollName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "UpperRollTypeName", "NAME_RollTypeUpper")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string UpperRollTypeName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "UpperRollTypeId", "NAME_RollTypeUpper")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? UpperRollTypeId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "BottomRollId", "NAME_RollBottomName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? BottomRollId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "BottomActualDiameter", "NAME_DiameterBottom")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? BottomActualDiameter { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetOverview), "BottomRollName", "NAME_RollBottomName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string BottomRollName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "BottomRollTypeName", "NAME_RollTypeBottom")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string BottomRollTypeName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "BottomRollTypeId", "NAME_RollTypeBottom")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? BottomRollTypeId { get; set; }

    public virtual long? RollSetHistoryId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "EnumRollSetHistoryStatus", "NAME_RollsetHistoryStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? EnumRollSetHistoryStatus { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "CassetteName", "NAME_CassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string CassetteName { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetOverview), "PositionInCassette", "NAME_PositionInCassette")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? PositionInCassette { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "StandNo", "NAME_StandNo")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? StandNo { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "MountedTs", "NAME_Mounted")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual DateTime? MountedTs { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "DismountedTs", "NAME_Dismounted")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual DateTime? DismountedTs { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "RollStatusName", "NAME_RollsetStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollStatusName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "CassetteId", "NAME_CassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? CassetteId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "RollSetStatusActual", "NAME_RollSetStatusActual")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string RollSetStatusActual { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "EnumCassetteStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? EnumCassetteStatus { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "ThirdRollId", "NAME_ThirdRollId")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? ThirdRollId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "ThirdActualDiameter", "NAME_DiameterThird")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? ThirdActualDiameter { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetOverview), "ThirdRollName", "NAME_RollThirdName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string ThirdRollName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "ThirdRollTypeName", "NAME_RollTypeThird")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string ThirdRollTypeName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "ThirdRollTypeId", "NAME_RollTypeIdThird")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? ThirdRollTypeId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "InterCassetteId", "NAME_InterCassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? InterCassetteId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "InterCassetteName", "NAME_InterCassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string InterCassetteName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverview), "InternalCassetteStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? InternalCassetteStatus { get; set; }

    #endregion
  }
}
