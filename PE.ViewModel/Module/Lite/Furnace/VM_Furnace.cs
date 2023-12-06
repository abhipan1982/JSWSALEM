using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Furnace
{
  public class VM_Furnace : VM_Base
  {
    public VM_HeatOverview HeatOverview;

    public VM_WorkOrderOverview WorkOrderOverview;

    public VM_Furnace(DbEntity.SPModels.SPL3L1MaterialsInFurnance entity)
    {
      Position=entity.OrderSeq;
      RawMaterialName = entity.RawMaterialName;
      CurrentTemp = entity.Temperature;
      WorkOrderId = entity.WorkOrderId;
      WorkOrderName = entity.WorkOrderName;
      HeatName = entity.HeatName;
      RawMaterialId = entity.RawMaterialId;
    }
    public VM_Furnace(V_MaterialsInFurnace entity)
    {
      Position = entity.Sorting;
      RawMaterialId = entity.RawMaterialId;
      RawMaterialName = entity.RawMaterialName;
      //this.AssetId = entity.AssetId;
      //this.AssetName = entity.AssetName;
      Weight = entity.LastWeight;
      Length = entity.LastLength;
      MaterialId = entity.MaterialId;
      MaterialName = entity.MaterialName;
      WorkOrderName = entity.WorkOrderName;
      HeatName = entity.HeatName;
      SteelgradeCode = entity.SteelgradeCode;
      WorkOrderId = entity.WorkOrderId;
      SteelGradeId = entity.SteelgradeId;
      HeatId = entity.HeatId;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_Furnace(V_RawMaterialInFurnace entity)
    {
      Position = entity.OrderSeq;
      RawMaterialId = entity.RawMaterialId;
      RawMaterialName = entity.RawMaterialName;
      Weight = entity.LastWeight;
      Length = entity.LastLength;
      MaterialName = entity.MaterialName;
      WorkOrderName = entity.WorkOrderName;
      HeatName = entity.HeatName;
      SteelgradeName = entity.SteelgradeName;
      WorkOrderId = entity.WorkOrderId;
      SteelGradeId = entity.SteelgradeId;
      HeatId = entity.HeatId;
      TimeInFurnace = entity.TimeInFurnace;
      CurrentTemp = Convert.ToInt64(entity.Temperature);

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_Furnace()
    {
    }

    public long? Position { get; set; }
    public long? RawMaterialId { get; set; }

    [SmfDisplay(typeof(VM_Furnace), "RawMaterialName", "NAME_RawMaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string RawMaterialName { get; set; }

    public long AssetId { get; set; }

    [SmfDisplay(typeof(VM_Furnace), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }

    [SmfDisplay(typeof(VM_Furnace), "Weigth", "NAME_Weight")]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    public double? Weight { get; set; }

    [SmfDisplay(typeof(VM_Furnace), "Length", "NAME_Length")]
    [SmfUnit("UNIT_Length")]
    [SmfFormat("FORMAT_Length", NullDisplayText = "-")]
    public double? Length { get; set; }

    public long? MaterialId { get; set; }

    [SmfDisplay(typeof(VM_Furnace), "MaterialName", "NAME_MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }

    [SmfDisplay(typeof(VM_Furnace), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_Furnace), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_Furnace), "SteelgradeCode", "NAME_SteelgradeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }

    public string SteelgradeName { get; set; }

    public long? WorkOrderId { get; set; }
    public long? SteelGradeId { get; set; }
    public long? HeatId { get; set; }

    [SmfDisplay(typeof(VM_Furnace), "CurrentTemp", "NAME_TemperatureInFurnance")]
    [SmfUnit("UNIT_Temperature")]
    [SmfFormat("FORMAT_Temperature", NullDisplayText = "-")]
    public double? CurrentTemp { get; set; }

    [SmfDisplay(typeof(VM_Furnace), "CurrentTemp", "NAME_TimeInFurnace")]
    [SmfUnit("UNIT_Temperature")]
    public int TimeInFurnace { get; set; }
  }
}
