using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_EquipmentHistory : VM_Base
  {
    #region ctor

    public VM_EquipmentHistory() { }

    public VM_EquipmentHistory(MNTEquipmentHistory data)
    {
      EquipmentHistoryId = data.EquipmentHistoryId;
      MaterialsProcessed = data.MaterialsProcessed ?? 0;
      WeightProcessed = data.WeightProcessed;
      EquipmentStatus = data.EquipmentStatus;
      EnumServiceType = (short)data.EnumServiceType;
      Remark = data.Remark;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region props

    public long EquipmentHistoryId { get; set; }

    public long EquipmentId { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "EquipmentStatus", "NAME_Status")]
    public short EquipmentStatus { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "EnumServiceType", "NAME_ServiceType")]
    public short EnumServiceType { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "WeightProcessed", "NAME_ActualAccumulatedWeight")]
    [SmfUnit("UNIT_MaintenanceWeight")]
    public double? WeightProcessed { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "MaterialsProcessed", "NAME_AccBilletCntShort")]
    public long MaterialsProcessed { get; set; }

    [SmfDisplay(typeof(VM_Equipment), "Remark", "NAME_Remark")]
    public string Remark { get; set; }

    #endregion
  }
}
