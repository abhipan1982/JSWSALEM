using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.UnitConverter;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_RawMaterialMeasurements : VM_Base
  {
    #region ctor
    public VM_RawMaterialMeasurements() { }

    public VM_RawMaterialMeasurements(V_RawMaterialMeasurement data)
    {
      RawMaterialId = data.RawMaterialId;
      MeasurementId = data.MeasurementId;
      FeatureId = data.FeatureId;
      FeatureName = data.FeatureName;
      AssetName = data.AssetName;
      IsValid = data.IsValid;
      MeasurementTime = data.MeasurementTime;
      MeasurementValueMin = data.MeasurementValueMin;
      MeasurementValueMax = data.MeasurementValueMax;
      UnitSymbol = data.UnitSymbol;
      UnitOfMeasureId = data.UnitOfMeasureId;
      MeasurementValueAvg = data.MeasurementValueAvg;
      AreaCode = data.AreaName;

      FeatureUnitConverterHelper.ConvertToLocal(this);
    }
    #endregion

    #region props
    public long? RawMaterialId { get; set; }
    public long MeasurementId { get; set; }
    public long FeatureId { get; set; }
    [SmfDisplay(typeof(VM_RawMaterialMeasurements), "FeatureName", "NAME_FeatureName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string FeatureName { get; set; }
    [SmfDisplay(typeof(VM_RawMaterialMeasurements), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }
    [SmfDisplay(typeof(VM_RawMaterialMeasurements), "ParentAssetName", "NAME_ParentAssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ParentAssetName { get; set; }
    [SmfDisplay(typeof(VM_RawMaterialMeasurements), "IsValid", "NAME_Valid")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsValid { get; set; }
    [SmfDisplay(typeof(VM_RawMaterialMeasurements), "MeasurementTime", "NAME_MeasurementTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime MeasurementTime { get; set; }
    [SmfDisplay(typeof(VM_RawMaterialMeasurements), "MeasurementValueMin", "NAME_Min")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [FeatureUnitValue(nameof(UnitOfMeasureId), nameof(FeatureId))]
    public double? MeasurementValueMin { get; set; }
    [SmfDisplay(typeof(VM_RawMaterialMeasurements), "MeasurementValueMax", "NAME_Max")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [FeatureUnitValue(nameof(UnitOfMeasureId), nameof(FeatureId))]
    public double? MeasurementValueMax { get; set; }
    [SmfDisplay(typeof(VM_RawMaterialMeasurements), "UnitSymbol", "NAME_Unit")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [FeatureUnitSymbol(nameof(UnitOfMeasureId), nameof(FeatureId))]
    public string UnitSymbol { get; set; }
    [SmfDisplay(typeof(VM_RawMaterialMeasurements), "Average", "NAME_Average")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [FeatureUnitValue(nameof(UnitOfMeasureId), nameof(FeatureId))]
    public double? MeasurementValueAvg { get; set; }

    public string AreaCode { get; set; }
    public long? UnitOfMeasureId { get; }
    #endregion
  }
}
