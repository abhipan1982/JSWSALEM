using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_RawMaterialsTree : VM_Base
  {
    public VM_RawMaterialsTree(V_RawMaterialSearchGrid data)
    {
      RawMaterialId = data.RawMaterialId;
      MaterialName = data.RawMaterialName;
      RawMaterialName = data.RawMaterialName;
      EnumRawMaterialStatus = data.EnumRawMaterialStatus;
      RootRawMaterialId = data.RootRawMaterialId;
      DisplayedMaterialName = data.DisplayedMaterialName;
      MaterialIsAssigned = data.MaterialIsAssigned;

      RawMaterialCreatedTs = data.RawMaterialCreatedTs;
      RawMaterialStartTs = data.RawMaterialStartTs;
      RawMaterialEndTs = data.RawMaterialEndTs;
      RollingStartTs = data.RollingStartTs;
      RollingEndTs = data.RollingEndTs;
      ProductCreatedTs = data.ProductCreatedTs;
    }

    public VM_RawMaterialsTree(V_MeasurementSearchGrid data)
    {
      RawMaterialId = data.RawMaterialId;
      MaterialName = data.DisplayedMaterialName;
      RawMaterialName = data.RawMaterialName;
      EnumRawMaterialStatus = data.EnumRawMaterialStatus;
      DisplayedMaterialName = data.DisplayedMaterialName;
      IsParent = data.IsParent;
      IsChild = data.IsChild;

      RawMaterialCreatedTs = data.RawMaterialCreatedTs;
      RawMaterialStartTs = data.RawMaterialStartTs;
      RawMaterialEndTs = data.RawMaterialEndTs;
      RollingStartTs = data.RollingStartTs;
      RollingEndTs = data.RollingEndTs;
      ProductCreatedTs = data.ProductCreatedTs;
    }

    public VM_RawMaterialsTree(V_LayerSearchGrid data)
    {
      RawMaterialId = data.RawMaterialId;
      RawMaterialName = data.RawMaterialName;
      EnumLayerStatus = data.EnumLayerStatus;
      RawMaterialName = data.RawMaterialName;
      RawMaterialCreatedTs = data.RawMaterialCreatedTs;

      RawMaterialCreatedTs = data.RawMaterialCreatedTs;
      RawMaterialStartTs = data.RawMaterialStartTs;
      RawMaterialEndTs = data.RawMaterialEndTs;
      RollingStartTs = data.RollingStartTs;
      RollingEndTs = data.RollingEndTs;
      ProductCreatedTs = data.ProductCreatedTs;
    }

    public VM_RawMaterialsTree(V_BundleSearchGrid data)
    {
      RawMaterialId = data.RawMaterialId;
      RawMaterialName = data.RawMaterialName;
      EnumRawMaterialStatus = data.EnumRawMaterialStatus;
      RootRawMaterialId = data.RootRawMaterialId;
      RawMaterialCreatedTs = data.RawMaterialCreatedTs;
      RawMaterialStartTs = data.RawMaterialStartTs;
      RawMaterialEndTs = data.RawMaterialEndTs;
      RollingStartTs = data.RollingStartTs;
      RollingEndTs = data.RollingEndTs;
      ProductCreatedTs = data.ProductCreatedTs;
    }

    [SmfDisplay(typeof(VM_RawMaterialsTree), "RawMaterialId", "NAME_MaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? RawMaterialId { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialsTree), "MaterialName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialsTree), "DisplayedMaterialName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DisplayedMaterialName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialsTree), "RawMaterialName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string RawMaterialName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialsTree), "EnumRawMaterialStatus", "NAME_MaterialStatus")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumRawMaterialStatus { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialsTree), "EnumLayerStatus", "NAME_LayerStatus")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumLayerStatus { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialsTree), "ParentRawMaterial", "NAME_ParentName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ParentRawMaterial { get; set; }

    public long? RootRawMaterialId { get; set; }

    public bool MaterialIsAssigned { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialsTree), "IsParent", "NAME_IsParent")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsParent { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialsTree), "IsChild", "NAME_IsChild")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsChild { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialsTree), "RawMaterialCreatedTs", "NAME_RawMaterialCreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false, DataFormatString = "{0:MM/dd/yyyy HH:mm:ss}")]
    public DateTime RawMaterialCreatedTs { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialsTree), "RawMaterialStartTs", "NAME_RawMaterialStartTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false, DataFormatString = "{0:MM/dd/yyyy HH:mm:ss}")]
    public DateTime? RawMaterialStartTs { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialsTree), "RawMaterialStartTs", "NAME_RawMaterialEndTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false, DataFormatString = "{0:MM/dd/yyyy HH:mm:ss}")]
    public DateTime? RawMaterialEndTs { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialsTree), "RawMaterialStartTs", "NAME_RollingStartTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false, DataFormatString = "{0:MM/dd/yyyy HH:mm:ss}")]
    public DateTime? RollingStartTs { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialsTree), "RawMaterialStartTs", "NAME_RollingEndTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false, DataFormatString = "{0:MM/dd/yyyy HH:mm:ss}")]
    public DateTime? RollingEndTs { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialsTree), "RawMaterialStartTs", "NAME_ProductCreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false, DataFormatString = "{0:MM/dd/yyyy HH:mm:ss}")]
    public DateTime? ProductCreatedTs { get; set; }
  }
}
