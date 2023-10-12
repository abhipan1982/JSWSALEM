using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_EquipmentGroup : VM_Base
  {
    #region ctor

    public VM_EquipmentGroup() { }

    public VM_EquipmentGroup(MNTEquipmentGroup data)
    {
      EquipmentGroupId = data.EquipmentGroupId;
      EquipmentGroupCode = data.EquipmentGroupCode;
      EquipmentGroupName = data.EquipmentGroupName;
      EquipmentGroupDescription = data.EquipmentGroupDescription;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region props

    public long EquipmentGroupId { get; set; }

    [SmfDisplay(typeof(VM_EquipmentGroup), "EquipmentGroupCode", "NAME_Code")]
    [StringLength(5, MinimumLength = 3)]
    public string EquipmentGroupCode { get; set; }

    [SmfDisplay(typeof(VM_EquipmentGroup), "EquipmentGroupName", "NAME_EquipmentGroupName")]
    [StringLength(50, MinimumLength = 5)]
    public string EquipmentGroupName { get; set; }

    [SmfDisplay(typeof(VM_EquipmentGroup), "EquipmentGroupDescription", "NAME_Description")]
    public string EquipmentGroupDescription { get; set; }

    #endregion
  }
}
