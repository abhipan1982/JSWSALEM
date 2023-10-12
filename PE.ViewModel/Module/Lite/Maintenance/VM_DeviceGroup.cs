using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_DeviceGroup : VM_Base
  {
    public VM_DeviceGroup()
    {
      UnitConverterHelper.ConvertToLocal(this);
    }

    //public VM_DeviceGroup(MNTDeviceGroup p)
    //{
    //  DeviceGroupId = p.DeviceGroupId;
    //  DeviceGroupName = p.DeviceGroupName;
    //  DeviceGroupCode = p.DeviceGroupCode;
    //  DeviceGroupDescription = p.DeviceGroupDescription;
    //  //TODOMN - exclude this
    //  //this.CreatedTs = p.CreatedTs;
    //  //this.LastUpdateTs = p.LastUpdateTs;
    //  FkParentDeviceGroupId = p.FKParentDeviceGroupId;
    //  if (p.FKParentDeviceGroup != null)
    //  {
    //    ParentDeviceGroupCode = p.FKParentDeviceGroup.DeviceGroupCode;
    //  }
    //  else
    //  {
    //    ParentDeviceGroupCode = "-";
    //  }

    //  UnitConverterHelper.ConvertToLocal(this);
    //}

    public long DeviceGroupId { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_DeviceGroup), "DeviceGroupCode", "NAME_DeviceGroupCode")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string DeviceGroupCode { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_DeviceGroup), "DeviceGroupName", "NAME_DeviceGroupName")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string DeviceGroupName { get; set; }

    [SmfDisplay(typeof(VM_DeviceGroup), "DeviceGroupDescription", "NAME_DeviceGroupDescription")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string DeviceGroupDescription { get; set; }

    [SmfDisplay(typeof(VM_DeviceGroup), "CreatedTs", "NAME_CreatedTs")]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_DeviceGroup), "LastUpdateTs", "NAME_LastUpdateTs")]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplay(typeof(VM_DeviceGroup), "ParentDeviceGroupCode", "NAME_ParentDeviceGroupCode")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string ParentDeviceGroupCode { get; set; }

    [SmfDisplay(typeof(VM_DeviceGroup), "FkParentDeviceGroupId", "NAME_ParentDeviceGroupCode")]
    public long? FkParentDeviceGroupId { get; set; }
  }
}
