using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.UnitConverter;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Visualization
{
  public class VM_AreaRawMaterialMeasurement
  {
    public VM_AreaRawMaterialMeasurement(V_AreaRawMaterialMeasurement model)
    {
      FeatureId = model.FeatureId;
      FeatureCode = model.FeatureCode;
      FeatureName = model.FeatureName;
      MeasurementValue = model.MeasurementValueAvg;
      UnitSymbol = model.UnitSymbol;
      UnitOfMeasureId = model.UnitOfMeasureId;

      FeatureUnitConverterHelper.ConvertToLocal(this);
    }

    public long FeatureId { get; set; }

    public int? FeatureCode { get; set; }

    [SmfDisplay(typeof(V_RawMaterialMeasurement), "FeatureName", "NAME_FeatureName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string FeatureName { get; set; }

    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [FeatureUnitValue(nameof(UnitOfMeasureId), nameof(FeatureId))]
    public double? MeasurementValue { get; set; }

    [FeatureUnitSymbol(nameof(UnitOfMeasureId), nameof(FeatureId))]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string UnitSymbol { get; set; }

    public long? UnitOfMeasureId { get; }
  }
}
