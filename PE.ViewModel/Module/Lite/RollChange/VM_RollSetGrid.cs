using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollChange
{
  public class VM_RollsetGrid : VM_Base
  {
    #region ctor

    public VM_RollsetGrid() { }

    public VM_RollsetGrid(V_RollSetOverview entity)
    {
      CassetteId = entity.CassetteId;
      StandId = entity.StandId;
      CassetteName = entity.CassetteName;
      StandNo = entity.StandNo;
      StandStatus = entity.EnumRollSetHistoryStatus;
      CassetteName = entity.CassetteName;
      CassetteStatus = entity.EnumCassetteStatus;
      RollsetId = entity.RollSetId;
      RollSetName = entity.RollSetName;
      RollSetHistoryId = entity.RollSetHistoryId;
      RollSetHistoryStatus = entity.EnumRollSetHistoryStatus;
      RollSetStatus = entity.EnumRollSetStatus;
      RollSetType = entity.RollSetType;
      UpperRollTypeName = entity.UpperRollTypeName;
      BottomRollTypeName = entity.BottomRollTypeName;
      ThirdRollTypeName = entity.ThirdRollTypeName;
      UpperRollTypeId = entity.UpperRollTypeId;
      BottomRollTypeId = entity.BottomRollTypeId;
      ThirdRollTypeId = entity.ThirdRollTypeId;
      UpperRollName = entity.UpperRollName;
      BottomRollName = entity.BottomRollName;
      ThirdRollName = entity.ThirdRollName;
      UpperRollId = entity.UpperRollId;
      BottomRollId = entity.BottomRollId;
      ThirdRollId = entity.ThirdRollId;
      PositionInCassette = entity.PositionInCassette;
      MountedTs = entity.MountedTs;
      DismountedTs = entity.DismountedTs;
      UpperActualDiameter = entity.UpperActualDiameter;
      BottomActualDiameter = entity.BottomActualDiameter;
      ThirdActualDiameter = entity.ThirdActualDiameter;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region properties

    public long? RollsetId { get; set; }
    public long? UpperRollId { get; set; }
    public long? BottomRollId { get; set; }
    public long? ThirdRollId { get; set; }
    public long? UpperRollTypeId { get; set; }
    public long? BottomRollTypeId { get; set; }
    public long? ThirdRollTypeId { get; set; }
    public long? CassetteId { get; set; }
    public long? InterCassetteId { get; set; }
    public long? RollSetHistoryId { get; set; }
    public long? StandId { get; set; }
    public short? PositionInCassette { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "RollSetStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short RollSetStatus { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "RollSetType", "NAME_Type")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short RollSetType { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "RollSetName", "NAME_RollSetName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string RollSetName { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "UpperRollName", "NAME_RollNameUpper")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string UpperRollName { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "BottomRollName", "NAME_RollNameBottom")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string BottomRollName { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "ThirdRollName", "NAME_RollNameThird")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string ThirdRollName { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "UpperActualDiameter", "NAME_DiameterUpper")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    public double? UpperActualDiameter { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "BottomActualDiameter", "NAME_DiameterBottom")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    public double? BottomActualDiameter { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "ThirdActualDiameter", "NAME_DiameterThird")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    public double? ThirdActualDiameter { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "UpperRollTypeName", "NAME_RollTypeUpper")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string UpperRollTypeName { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "BottomRollTypeName", "NAME_RollTypeBottom")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string BottomRollTypeName { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "ThirdRollTypeName", "NAME_RollTypeThird")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string ThirdRollTypeName { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "StandStatus", "NAME_StandStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short? StandStatus { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "RollSetHistoryStatus", "NAME_RollSetHistoryStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short RollSetHistoryStatus { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "CassetteName", "NAME_CassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string CassetteName { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "InterCassetteName", "NAME_InterCassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string InterCassetteName { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "CassetteStatus", "NAME_CassetteStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short? CassetteStatus { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "StandNo", "NAME_StandNo")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short? StandNo { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "MountedTs", "NAME_Mounted")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public DateTime? MountedTs { get; set; }

    [SmfDisplay(typeof(VM_RollsetGrid), "DismountedTs", "NAME_Dismounted")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public DateTime? DismountedTs { get; set; }

    #endregion
  }
}
