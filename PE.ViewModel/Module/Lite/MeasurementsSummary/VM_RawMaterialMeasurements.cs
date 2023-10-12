using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.UnitConverter;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.MeasurementsSummary
{
  public class VM_RawMaterialMeasurements : VM_Base
  {
    #region ctor

    public VM_RawMaterialMeasurements() { }

    public VM_RawMaterialMeasurements(V_RawMaterialMeasurement data)
    {
      RawMaterialId = data.RawMaterialId;
      RawMaterialName = data.RawMaterialName;
      FeatureName = data.FeatureName;
      MeasurementValueMin = data.MeasurementValueMin;
      MeasurementValueMax = data.MeasurementValueMax;
      UnitOfMeasureId = data.UnitOfMeasureId;
      MeasurementValueAvg = data.MeasurementValueAvg;
      AreaCode = data.AreaName;
      AssetName = data.AssetName;
      MeasurementTime = data.MeasurementTime;
      UnitSymbol = data.UnitSymbol;
      FeatureId = data.FeatureId;

      FeatureUnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region props

    public long? RawMaterialId { get; set; }

    public long FeatureId { get; set; }


    [SmfDisplay(typeof(V_RawMaterialMeasurement), "MeasurementValueMin", "NAME_Min")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [FeatureUnitValue(nameof(UnitOfMeasureId), nameof(FeatureId))]
    public double? MeasurementValueMin { get; set; }

    [SmfDisplay(typeof(V_RawMaterialMeasurement), "MeasurementValueMax", "NAME_Max")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [FeatureUnitValue(nameof(UnitOfMeasureId), nameof(FeatureId))]
    public double? MeasurementValueMax { get; set; }

    [SmfDisplay(typeof(V_RawMaterialMeasurement), "Average", "NAME_Average")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [FeatureUnitValue(nameof(UnitOfMeasureId), nameof(FeatureId))]
    public double? MeasurementValueAvg { get; set; }

    public string RawMaterialName { get; set; }

    public string AreaCode { get; set; }

    public string FeatureName { get; set; }
    public string AssetName { get; set; }

    [FeatureUnitSymbol(nameof(UnitOfMeasureId), nameof(FeatureId), false)]
    public string UnitSymbol { get; set; }

    public long? UnitOfMeasureId { get; }

    public DateTime MeasurementTime { get; set; }

    #endregion
  }
}
