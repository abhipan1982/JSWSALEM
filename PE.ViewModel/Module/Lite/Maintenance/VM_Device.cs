using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_Device : VM_Base
  {
    public VM_Device()
    {
      UnitConverterHelper.ConvertToLocal(this);
    }

    //public VM_Device(MNTDevice p)
    //{
    //  DeviceId = p.DeviceId;
    //  DeviceCode = p.DeviceCode;
    //  DeviceName = p.DeviceName;
    //  AcquireDate = p.AcquiredDate;
    //  DisposeDate = p.DisposedDate;
    //  Description = p.DeviceDescription;
    //  Model = p.DeviceModel;
    //  SerialNumber = p.DeviceSerialNumber;
    //  FkSupplierId = p.FKSupplierId;
    //  if (p.FKDeviceGroup != null)
    //  {
    //    DeviceGroup = p.FKDeviceGroup.DeviceGroupCode;
    //  }
    //  else
    //  {
    //    DeviceGroup = "-";
    //  }

    //  if (p.MNTDeviceComponents != null)
    //  {
    //    Components = p.MNTDeviceComponents.Count();
    //  }
    //  else
    //  {
    //    Components = 0;
    //  }

    //  Status = p.EnumDeviceAvailability;

    //  if (p.FKSupplier != null)
    //  {
    //    SupplierName = p.FKSupplier.SupplierName;
    //  }
    //  else
    //  {
    //    SupplierName = "-";
    //  }

    //  UnitConverterHelper.ConvertToLocal(this);
    //}

    public long DeviceId { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_Device), "DeviceName", "NAME_Name")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string DeviceName { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_Device), "DeviceCode", "NAME_Code")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string DeviceCode { get; set; }

    [SmfDisplay(typeof(VM_Device), "SupplierName", "NAME_Supplier")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string SupplierName { get; set; }

    [SmfDisplay(typeof(VM_Device), "Description", "NAME_Description")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string Description { get; set; }

    [SmfDisplay(typeof(VM_Device), "Model", "NAME_Model")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string Model { get; set; }

    [SmfDisplay(typeof(VM_Device), "Model", "NAME_SerialNumber")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string SerialNumber { get; set; }

    [SmfDisplay(typeof(VM_Device), "Status", "NAME_Status")]
    public short Status { get; set; }

    [SmfDisplay(typeof(VM_Device), "AcquireDate", "NAME_AcquireDate")]
    public DateTime? AcquireDate { get; set; }

    [SmfDisplay(typeof(VM_Device), "DisposeDate", "NAME_DisposeDate")]
    public DateTime? DisposeDate { get; set; }

    [SmfDisplay(typeof(VM_Device), "DeviceGroup", "NAME_DeviceGroup")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string DeviceGroup { get; set; }

    [SmfDisplay(typeof(VM_Device), "Components", "NAME_Components")]
    public int Components { get; set; }

    [SmfDisplay(typeof(VM_Device), "InstalationCycle", "NAME_InstalationCycle")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string InstalationCycle { get; set; }

    [SmfDisplay(typeof(VM_Device), "FkSupplierId", "NAME_Supplier")]
    public long? FkSupplierId { get; set; }

    [SmfDisplay(typeof(VM_Device), "FkDeviceGroupId", "NAME_DeviceGroup")]
    public long? FkDeviceGroupId { get; set; }
  }
}
