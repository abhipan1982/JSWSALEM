using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_Component : VM_Base
  {
    public VM_Component()
    {
      Items = new List<VM_Component>();
      IsActiveNum = 1;
      UnitConverterHelper.ConvertToLocal(this);
    }

    //public VM_Component(MNTComponent p)
    //{
    //  //TODOMN - exclude this
    //  //this.CreatedTs = p.CreatedTs;
    //  //this.LastUpdateTs = p.LastUpdateTs;
    //  ComponentId = p.ComponentId;
    //  ComponentName = p.ComponentName;
    //  ComponentCode = p.ComponentCode;
    //  ComponentQuantity = p.ComponentQuantity;
    //  FkSupplierId = p.FKDefaultSupplierId;
    //  FkComponentGroupId = p.FKComponentGroupId;
    //  Items = new List<VM_Component>();
    //  IsActiveNum = 1;
    //  if (p.MNTDeviceComponents != null)
    //  {
    //    MNTDeviceComponent mNTDeviceComponent =
    //      p.MNTDeviceComponents.Where(z => z.FKComponentId == p.ComponentId).FirstOrDefault();
    //    if (mNTDeviceComponent != null)
    //    {
    //      FkDeviceId = mNTDeviceComponent.FKDeviceId;
    //    }
    //  }

    //  if (p.FKComponentGroup != null)
    //  {
    //    GroupCode = p.FKComponentGroup.ComponentGroupCode;
    //  }

    //  if (p.FKDefaultSupplier != null)
    //  {
    //    Supplier = p.FKDefaultSupplier.SupplierName;
    //  }

    //  UnitConverterHelper.ConvertToLocal(this);
    //}

    public long? ComponentId { get; set; }
    public long? FkDeviceId { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_Component), "ComponentName", "NAME_Name")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string ComponentName { get; set; }

    [SmfDisplay(typeof(VM_Component), "ComponentType", "NAME_Name")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string ComponentType { get; set; }

    [SmfDisplay(typeof(VM_Component), "ComponentCode", "NAME_Code")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string ComponentCode { get; set; }

    [SmfDisplay(typeof(VM_Component), "Installed", "NAME_Installed")]
    public DateTime? Installed { get; set; }

    [SmfDisplay(typeof(VM_Component), "IsActive", "NAME_IsActive")]
    public bool? IsActive { get; set; }

    [SmfDisplay(typeof(VM_Component), "IsActiveNum", "NAME_IsActive")]
    public short? IsActiveNum { get; set; }

    [SmfDisplay(typeof(VM_Component), "IsActiveLabel", "NAME_IsActive")]
    public string IsActiveLabel { get; set; }

    [SmfDisplay(typeof(VM_Component), "GroupCode", "NAME_GroupCode")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string GroupCode { get; set; }

    [SmfDisplay(typeof(VM_Component), "FkComponentGroupId", "NAME_GroupCode")]
    public long? FkComponentGroupId { get; set; }

    [SmfDisplay(typeof(VM_Component), "Supplier", "NAME_Supplier")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string Supplier { get; set; }

    [SmfDisplay(typeof(VM_Component), "FkSupplierId", "NAME_Supplier")]
    public long? FkSupplierId { get; set; }

    public string ColorScheme { get; set; }
    public List<VM_Component> Items { get; set; }

    [SmfDisplay(typeof(VM_Component), "LastTimeUninstalled", "NAME_LastTimeUninstalled")]
    public DateTime? LastTimeUninstalled { get; set; }

    [SmfDisplay(typeof(VM_Component), "LastTimeInstalled", "NAME_LastTimeInstalled")]
    public DateTime? LastTimeInstalled { get; set; }

    [SmfDisplay(typeof(VM_Component), "Description", "NAME_Description")]
    public string Description { get; set; }

    [SmfDisplay(typeof(VM_Component), "CreatedTs", "NAME_CreatedTs")]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_Component), "LastUpdateTs", "NAME_LastUpdateTs")]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplay(typeof(VM_Component), "ComponentQuantity", "NAME_ComponentQuantity")]
    public int? ComponentQuantity { get; set; }
  }
}
