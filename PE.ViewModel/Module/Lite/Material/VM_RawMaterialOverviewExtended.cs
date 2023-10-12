using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_RawMaterialOverviewExtended : VM_Base
  {
    public VM_RawMaterialOverviewExtended()
    {
    }

    public VM_RawMaterialOverviewExtended(V_L1L3MaterialAssignment data)
    {
      RawMaterialId = data.RawMaterialId;
      //TODOMN - refactor this
      //CreatedTs = data.CreatedTs;
      //LastUpdateTs = data.LastUpdateTs;
      RawMaterialName = data.RawMaterialName;
      MaterialName = data.MaterialName;
      Status = ResxHelper.GetResxByKey((RawMaterialStatus)data.EnumRawMaterialStatus);
    }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "RawMaterialId", "NAME_MaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? RawMaterialId { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "LastUpdateTs", "NAME_LastUpdateTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? LastUpdateTs { get; set; }


    [SmfDisplay(typeof(VM_RawMaterialOverview), "RawMaterialName", "NAME_MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string RawMaterialName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialOverview), "Status", "NAME_MaterialStatus")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public virtual string Status { get; set; }


    [SmfDisplay(typeof(VM_RawMaterialOverview), "L3MaterialName", "NAME_L3MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }
  }
}
