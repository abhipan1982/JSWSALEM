using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.QualityExpert
{
  public class VM_QualityExpertRawMaterial : VM_Base
  {
    public VM_QualityExpertRawMaterial(V_QualityExpertSearchGrid data)
    {
      RawMaterialId = data.RawMaterialId;
      MaterialName = data.MaterialName;
      RawMaterialName = data.RawMaterialName;
      LastGrading = data.LastGrading;
      EnumRawMaterialStatus = data.EnumRawMaterialStatus; 
      HeatName = data.HeatName;
      RollingStartTs = data.RollingStartTs;
      RollingEndTs = data.RollingEndTs;
      ProductCreatedTs = data.ProductCreatedTs;
      MaterialCreatedTs = data.MaterialCreatedTs;
      MaterialStartTs = data.MaterialStartTs;
      MaterialEndTs = data.MaterialEndTs;
      WorkOrderStartTs = data.WorkOrderStartTs;
      WorkOrderEndTs = data.WorkOrderEndTs;
    }

    public VM_QualityExpertRawMaterial() { }

    public long RawMaterialId { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "MaterialName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "DisplayedMaterialName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DisplayedMaterialName { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "RawMaterialName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string RawMaterialName { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "LastGrading", "NAME_CurrentGradingValue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? LastGrading { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "EnumRawMaterialStatus", "NAME_MaterialStatus")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumRawMaterialStatus { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "EnumLayerStatus", "NAME_LayerStatus")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumLayerStatus { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "ParentRawMaterial", "NAME_ParentName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ParentRawMaterial { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "RawMaterialCreatedTs", "NAME_MaterialCreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? MaterialCreatedTs { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "RawMaterialCreatedTs", "NAME_MaterialStartTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? MaterialStartTs { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "RawMaterialCreatedTs", "NAME_MaterialEndTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? MaterialEndTs { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "RawMaterialCreatedTs", "NAME_WorkOrderStartTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? WorkOrderStartTs { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "RawMaterialCreatedTs", "NAME_WorkOrderEndTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? WorkOrderEndTs { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "RawMaterialCreatedTs", "NAME_RollingStartTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? RollingStartTs { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "RawMaterialCreatedTs", "NAME_RollingEndTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? RollingEndTs { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "RawMaterialCreatedTs", "NAME_ProductCreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? ProductCreatedTs { get; set; }
  }
}
