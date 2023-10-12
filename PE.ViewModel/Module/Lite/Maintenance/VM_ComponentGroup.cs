using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_ComponentGroup : VM_Base
  {
    public VM_ComponentGroup()
    {
      Items = new List<VM_ComponentGroup>();
      Components = new List<VM_Component>();
      UnitConverterHelper.ConvertToLocal(this);
    }

    //public VM_ComponentGroup(MNTComponentGroup p)
    //{
    //  ComponentGroupId = p.ComponentGroupId;
    //  ComponentGroupName = p.ComponentGroupName;
    //  ComponentGroupDescription = p.ComponentGroupDescription;
    //  ComponentGroupCode = p.ComponentGroupCode;
    //  FkParentComponentGroupId = p.FKParentComponentGroupId;
    //  if (p.FKParentComponentGroup != null)
    //  {
    //    ParentComponentGroupCode = p.FKParentComponentGroup.ComponentGroupCode;
    //  }
    //  else
    //  {
    //    ParentComponentGroupCode = "-";
    //  }

    //  Items = new List<VM_ComponentGroup>();
    //  Components = new List<VM_Component>();
    //  UnitConverterHelper.ConvertToLocal(this);
    //}

    public long? ComponentGroupId { get; set; }

    [SmfDisplay(typeof(VM_ComponentGroup), "CreatedTs", "NAME_CreatedTs")]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_ComponentGroup), "LastUpdateTs", "NAME_LastUpdateTs")]
    public DateTime? LastUpdateTs { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_ComponentGroup), "ComponentGroupName", "NAME_ComponentGroupName")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string ComponentGroupName { get; set; }

    [SmfDisplay(typeof(VM_ComponentGroup), "ComponentGroupCode", "NAME_ComponentGroupCode")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string ComponentGroupCode { get; set; }

    [SmfDisplay(typeof(VM_ComponentGroup), "ComponentGroupDescription", "NAME_ComponentGroupDescription")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string ComponentGroupDescription { get; set; }

    [SmfDisplay(typeof(VM_ComponentGroup), "FkParentComponentGroupId", "NAME_ParentComponentGroupCode")]
    public long? FkParentComponentGroupId { get; set; }

    public List<VM_ComponentGroup> Items { get; set; }
    public List<VM_Component> Components { get; set; }

    [StringLength(50, MinimumLength = 5, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfDisplay(typeof(VM_ComponentGroup), "ParentComponentGroupCode", "NAME_ParentComponentGroupCode")]
    public string ParentComponentGroupCode { get; set; }
  }
}
