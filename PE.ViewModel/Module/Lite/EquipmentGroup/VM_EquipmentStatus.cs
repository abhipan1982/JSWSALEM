using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_EquipmentStatus : VM_Base
  {
    #region

    public VM_EquipmentStatus()
    {
      EnumServiceType = (short)ServiceType.Undefined;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_EquipmentStatus(MNTEquipment p)
    {
      AccBilletCnt = p.CntMatsProcessed ?? 0;
      ActualValue = p.ActualValue;
      AlarmValue = p.AlarmValue;
      WarningValue = p.WarningValue;
      EquipmentId = p.EquipmentId;
      EquipmentStatus = p.EquipmentStatus;
      EnumServiceType = (short)p.EnumServiceType;
      ServiceExpires = p.ServiceExpires;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region props

    public long EquipmentId { get; set; }

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

    #endregion
  }
}
