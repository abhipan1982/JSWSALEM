using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

using PE.DbEntity.Models;
using PE.DbEntity.PEContext;
namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_MaterialOverview : VM_Base
  {
    public VM_MaterialOverview(PRMMaterial material)
    //public VM_MaterialOverview(PRMMaterialsEXT material)
    {
      MaterialId = material.MaterialId;
      RawMaterialId = material?.TRKRawMaterials?.FirstOrDefault()?.RawMaterialId;
      IsDummy = material.IsDummy;
      IsAssigned = material.IsAssigned;
      MaterialName = material.MaterialName;
      FKWorkOrderId = material.FKWorkOrderId;
      WorkOrderName = material.FKWorkOrder?.WorkOrderName;
      WorkOrderStatus = material.FKWorkOrder == null ? null : ResxHelper.GetResxByKey(material.FKWorkOrder?.EnumWorkOrderStatus);
      MaterialCatalogueName = material.FKMaterialCatalogue?.MaterialCatalogueName;
      HeatName = material.FKHeat?.HeatName;
      FKHeatId = material.FKHeatId;
      SteelgradeName = material.FKWorkOrder?.FKSteelgrade?.SteelgradeName;
      FKSteelgradeId = material.FKWorkOrder?.FKSteelgradeId;
      MaterialWeight = material.MaterialWeight;
      if (material.FKWorkOrder != null)
      {
        WorkOrderOverview = new VM_WorkOrderOverview(material.FKWorkOrder);
      }
      else
      {
        WorkOrderOverview = new VM_WorkOrderOverview();
      }

      MaterialCreatedTs = material.MaterialCreatedTs;
      MaterialStartTs = material.MaterialStartTs;
      MaterialEndTs = material.MaterialEndTs;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_MaterialOverview(PRMMaterial material, PRMWorkOrder woo)
    //public VM_MaterialOverview(PRMMaterial material, PRMWorkOrdersEXT woo)

    {
      MaterialId = material.MaterialId;
      IsDummy = material.IsDummy;
      MaterialName = material.MaterialName;
      MaterialWeight = material.MaterialWeight;
      MaterialThickness = material.MaterialThickness;
      FKWorkOrderId = material.FKWorkOrderId;
      IsAssigned = material.IsAssigned;
      WorkOrderName = material.FKWorkOrder?.WorkOrderName;
      HeatName = material.FKHeat?.HeatName;
      WorkOrderOverview = new VM_WorkOrderOverview(woo);

      MaterialCreatedTs = material.MaterialCreatedTs;
      MaterialStartTs = material.MaterialStartTs;
      MaterialEndTs = material.MaterialEndTs;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_MaterialOverview() { }

    public VM_MaterialOverview(V_MaterialSearchGrid data)
    {
      MaterialId = data.MaterialId;
      MaterialName = data.MaterialName;
      WorkOrderName = data.WorkOrderName;
      HeatName = data.HeatName;
      MaterialCatalogueName = data.MaterialCatalogueName;
      MaterialWeight = data.MaterialWeight;
      MaterialThickness = data.MaterialThickness;
      MaterialIsAssigned = data.MaterialIsAssigned;

      MaterialCreatedTs = data.MaterialCreatedTs;
      MaterialStartTs = data.MaterialStartTs;
      MaterialEndTs = data.MaterialEndTs;

      UnitConverterHelper.ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_MaterialOverview), "MaterialId", "NAME_MaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? MaterialId { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "IsDummy", "NAME_IsDummyMaterial")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsDummy { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "MaterialName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }
    [SmfDisplay(typeof(VM_MaterialOverview), "MaterialWeight", "NAME_MaterialWeight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double MaterialWeight { get; private set; }
    [SmfDisplay(typeof(VM_MaterialOverview), "MaterialThickness", "NAME_Thickness")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double MaterialThickness { get; private set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "FKWorkOrderId", "NAME_FKWorkOrderId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKWorkOrderId { get; set; }

    public long? FKSteelgradeId { get; set; }

    public long? FKHeatId { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "WorkOrderName", "NAME_WorkOrder")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "SteelgradeName", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeName { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "MaterialCatalogueName", "NAME_MaterialCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "FKMaterialCatalogueIdRef", "NAME_FKMaterialCatalogueIdRef")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FKMaterialCatalogueIdRef { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "IsAssigned", "NAME_IsAssigned")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsAssigned { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "MaterialIsAssigned", "NAME_IsAssigned")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool MaterialIsAssigned { get; set; }

    public VM_WorkOrderOverview WorkOrderOverview { get; set; }
    public long? RawMaterialId { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "WorkOrderStatus", "NAME_WorkOrderStatus")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderStatus { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "CreatedTs", "NAME_MaterialCreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false, DataFormatString = "{0:MM/dd/yyyy HH:mm:ss}")]
    public DateTime? MaterialCreatedTs { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "CreatedTs", "NAME_MaterialStartTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false, DataFormatString = "{0:MM/dd/yyyy HH:mm:ss}")]
    public DateTime? MaterialStartTs { get; set; }

    [SmfDisplay(typeof(VM_MaterialOverview), "CreatedTs", "NAME_MaterialEndTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false, DataFormatString = "{0:MM/dd/yyyy HH:mm:ss}")]
    public DateTime? MaterialEndTs { get; set; }
  }
}
