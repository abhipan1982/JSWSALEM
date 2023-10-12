using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_L3MaterialData : VM_Base
  {
    public VM_L3MaterialData() { }

    public VM_L3MaterialData(PRMMaterial data, long? rawMaterialId)
    {
      MaterialId = data.MaterialId;
      IsDummy = data.IsDummy;
      MaterialName = data.MaterialName;
      WorkOrder = new VM_WorkOrderOverview(data.FKWorkOrder);
      IsAssigned = data.IsAssigned;
      RawMaterialId = rawMaterialId;
    }

    public VM_L3MaterialData(long rawMaterialId)
    {
      RawMaterialId = rawMaterialId;
    }

    [SmfDisplay(typeof(VM_L3MaterialData), "Name", "NAME_MaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? MaterialId { get; set; }

    public long? RawMaterialId { get; set; }

    [SmfDisplay(typeof(VM_L3MaterialData), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_L3MaterialData), "LastUpdateTs", "NAME_LastUpdateTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime LastUpdateTs { get; set; }

    [SmfDisplay(typeof(VM_L3MaterialData), "IsDummy", "NAME_IsDummyMaterial")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsDummy { get; set; }

    [SmfDisplay(typeof(VM_L3MaterialData), "MaterialName", "NAME_MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }

    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public VM_WorkOrderOverview WorkOrder { get; set; }

    [SmfDisplay(typeof(VM_L3MaterialData), "IsAssigned", "NAME_IsAssigned")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsAssigned { get; set; }
  }
}
