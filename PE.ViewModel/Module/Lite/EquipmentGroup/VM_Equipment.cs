using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_Equipment : VM_Base
  {
    #region ctor

    public VM_Equipment()
    {
      EnumServiceType = (short)ServiceType.Undefined;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_Equipment(MNTEquipment data)
    {
      AccBilletCnt = data.CntMatsProcessed ?? 0;
      ActualValue = data.ActualValue;
      AlarmValue = data.AlarmValue;
      WarningValue = data.WarningValue;
      EquipmentCode = data.EquipmentCode;
      EquipmentDescription = data.EquipmentDescription;
      EquipmentId = data.EquipmentId;
      EquipmentName = data.EquipmentName;
      EquipmentStatus = data.EquipmentStatus;
      EnumServiceType = (short)data.EnumServiceType;
      ServiceExpires = data.ServiceExpires;
      EquipmentGroupCode = data.FKEquipmentGroup.EquipmentGroupCode;
      EquipmentGroupId = data.FKEquipmentGroupId;

      IsWarned = !IsOverdue && data.ActualValue > data.WarningValue && data.ActualValue < data.AlarmValue;
      IsInactive = data.EquipmentStatus == BaseDbEntity.EnumClasses.EquipmentStatus.Inactive;
      IsOverdue = (data.ActualValue > data.AlarmValue &&
        (data.EnumServiceType == ServiceType.Both || data.EnumServiceType == ServiceType.WeightRelated)) ||
        (data.ServiceExpires.HasValue && data.ServiceExpires < DateTime.Now &&
        (data.EnumServiceType == ServiceType.Both || data.EnumServiceType == ServiceType.DateRelated));

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region props

    public long EquipmentId { get; set; }

    public long EquipmentGroupId { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_Equipment), "EquipmentCode", "NAME_EquipmentCode")]
    [StringLength(10, MinimumLength = 3)]
    public string EquipmentCode { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_Equipment), "EquipmentName", "NAME_Name")]
    [SmfStringLength(50, MinimumLength = 5)]
    public string EquipmentName { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "EquipmentDescription", "NAME_Description")]
    [StringLength(100)]
    public string EquipmentDescription { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "EquipmentGroupCode", "NAME_GroupCode")]
    [StringLength(50, MinimumLength = 5)]
    public string EquipmentGroupCode { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "EquipmentStatus", "NAME_Status")]
    public short? EquipmentStatus { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "EnumServiceType", "NAME_ServiceType")]
    public short? EnumServiceType { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "ActualValue", "NAME_ActualAccumulatedWeight")]
    [SmfUnit("UNIT_MaintenanceWeight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    public double? ActualValue { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "AlarmValue", "NAME_AccWeightLimit")]
    [SmfUnit("UNIT_MaintenanceWeight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    public double? AlarmValue { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "WarningValue", "NAME_TotalWeightWarningLevelShort")]
    [SmfUnit("UNIT_MaintenanceWeight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    public double? WarningValue { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "ServiceExpires", "NAME_NextServiceDate")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime? ServiceExpires { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "AccBilletCnt", "NAME_AccBilletCntShort")]
    public long AccBilletCnt { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "Remark", "NAME_Remark")]
    [StringLength(200)]
    public string Remark { get; set; }

    public bool IsOverdue { get; set; }

    public bool IsWarned { get; set; }

    public bool IsInactive { get; set; }

    #endregion
  }
}
