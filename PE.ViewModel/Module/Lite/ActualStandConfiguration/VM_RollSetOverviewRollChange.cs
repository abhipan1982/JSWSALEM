using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.ActualStandConfiguration
{
  public class VM_RollSetOverviewRollChange : VM_Base
  {
    #region ctor

    public VM_RollSetOverviewRollChange() { }

    public VM_RollSetOverviewRollChange(short position)
    {
      RollSetId = -1;
      EnumRollSetStatus = 0;
      RollSetStatusNew = 0;
      RollSetType = 0;
      RollSetName = "";
      UpperRollId = null;
      UpperActualDiameter = null;
      UpperRollName = "";
      UpperRollTypeName = "";
      BottomRollId = null;
      BottomActualDiameter = null;
      RollNameBottom = "";
      BottomRollTypeName = "";
      RollSetHistoryId = null;
      EnumRollSetHistoryStatus = 0;
      CassetteName = "";
      PositionInCassette = position;
      StandNo = null;
      MountedTs = null;
      DismountedTs = null;
      RollSetStatusActual = "";
      EnumCassetteStatus = 0;
      RollTypeIdUpper = null;
      BottomRollTypeId = null;
      EstRollChangeTime = null;
      InstalledInStandStatus = null;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_RollSetOverviewRollChange(V_RollSetOverview data)
    {
      RollSetId = data.RollSetId;
      EnumRollSetStatus = data.EnumRollSetStatus;
      RollSetStatusNew = data.EnumRollSetStatus;
      RollSetType = data.RollSetType;
      RollSetName = data.RollSetName;
      UpperRollId = data.UpperRollId;
      UpperActualDiameter = data.UpperActualDiameter;
      UpperRollName = data.UpperRollName;
      UpperRollTypeName = data.UpperRollTypeName;
      BottomRollId = data.BottomRollId;
      BottomActualDiameter = data.BottomActualDiameter;
      RollNameBottom = data.BottomRollName;
      BottomRollTypeName = data.BottomRollTypeName;
      RollSetHistoryId = data.RollSetHistoryId;
      EnumRollSetHistoryStatus = data.EnumRollSetHistoryStatus;
      CassetteName = data.CassetteName;
      PositionInCassette = data.PositionInCassette;
      StandNo = data.StandNo;
      MountedTs = data.MountedTs;
      DismountedTs = data.DismountedTs;
      RollSetStatusActual = "";
      RollTypeIdUpper = data.UpperRollTypeId;
      BottomRollTypeId = data.BottomRollTypeId;
      InstalledInStandStatus = GetStandStatus(data.StandId ?? 0);
      EstRollChangeTime = null;
      CassetteName = data.CassetteName;
      EnumCassetteStatus = data.EnumCassetteStatus;
      if ((data.EnumRollSetStatus == BaseDbEntity.EnumClasses.RollSetStatus.Ready.Value) || (data.EnumRollSetStatus == BaseDbEntity.EnumClasses.RollSetStatus.NotAvailable.Value))
      {
        CassetteId = 0;
      }
      else
      {
        CassetteId = data.CassetteId;
      }

      UnitConverterHelper.ConvertToLocal(this);
    }
    #endregion

    #region properties

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "RollSetId", "NAME_RollSetName")]
    public virtual long RollSetId { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "EnumRollSetStatus", "NAME_RollsetStatus")]
    public virtual short EnumRollSetStatus { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "RollSetStatusNew", "NAME_RollsetStatusNew")]
    public virtual short RollSetStatusNew { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "RollSetType", "NAME_RollsetType")]
    public virtual short RollSetType { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "RollSetName", "NAME_RollSetName")]
    public virtual string RollSetName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "UpperRollId", "NAME_RollUpperName")]
    public virtual long? UpperRollId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "UpperActualDiameter", "NAME_DiameterUpper")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    public virtual double? UpperActualDiameter { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "UpperRollName", "NAME_RollUpperName")]
    public virtual string UpperRollName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "UpperRollTypeName", "NAME_RollTypeUpper")]
    public virtual string UpperRollTypeName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "RollTypeIdUpper", "NAME_RollTypeUpper")]
    public virtual long? RollTypeIdUpper { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "BottomRollId", "NAME_RollBottomName")]
    public virtual long? BottomRollId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "BottomActualDiameter", "NAME_DiameterBottom")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    public virtual double? BottomActualDiameter { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "RollNameBottom", "NAME_RollBottomName")]
    public virtual string RollNameBottom { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "BottomRollTypeName", "NAME_RollTypeBottom")]
    public virtual string BottomRollTypeName { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "BottomRollTypeId", "NAME_RollTypeBottom")]
    public virtual long? BottomRollTypeId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "ThirdRollId", "NAME_RollThirdName")]
    public virtual long? ThirdRollId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "ThirdDiameter", "NAME_DiameterThird")]
    [SmfFormat("FORMAT_Diameter")]
    [SmfUnit("UNIT_Diameter")]
    public virtual double? ThirdDiameter { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "RollNameThird", "NAME_RollThirdName")]
    public virtual string RollNameThird { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "RollTypeThird", "NAME_RollTypeThird")]
    public virtual string RollTypeThird { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "RollTypeIdThird", "NAME_RollTypeThird")]
    public virtual long? RollTypeIdThird { get; set; }

    public virtual long? RollSetHistoryId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "EnumRollSetHistoryStatus", "NAME_RollsetHistoryStatus")]
    public virtual short EnumRollSetHistoryStatus { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "CassetteName", "NAME_CassetteName")]
    public virtual string CassetteName { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "PositionInCassette", "NAME_PositionInCassette")]
    public virtual short? PositionInCassette { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "StandNo", "NAME_StandNo")]
    public virtual short? StandNo { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "MountedTs", "NAME_Mounted")]
    public virtual DateTime? MountedTs { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "DismountedTs", "NAME_Dismounted")]
    public virtual DateTime? DismountedTs { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "CassetteId", "NAME_CassetteName")]
    public virtual long? CassetteId { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "RollSetStatusActual", "NAME_RollSetStatusActual")]
    public virtual string RollSetStatusActual { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "EnumCassetteStatus", "NAME_Status")]
    public virtual short EnumCassetteStatus { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "EstRollChangeTime", "NAME_EstRollChangeTime")]
    public virtual DateTime? EstRollChangeTime { get; set; }

    [SmfDisplay(typeof(VM_RollSetOverviewRollChange), "InstalledInStandStatus", "NAME_StandStatus")]
    public virtual short? InstalledInStandStatus { get; set; }

    #endregion

    private short GetStandStatus(long standId)
    {
      using PEContext ctx = new PEContext();
      return ctx.RLSStands.FirstOrDefault(x => x.StandId == standId)?.EnumStandStatus?.Value ?? 0;
    }
  }
}
