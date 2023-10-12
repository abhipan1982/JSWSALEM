using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.UnitConverter;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.MeasurementsSummary
{
  public class VM_FeatureMap : VM_Base
  {
    public VM_FeatureMap(V_Feature table)
    {
      FeatureId = table.FeatureId;
      AssetName = table.AssetName;
      FeatureName = table.FeatureName;
      UnitSymbol = table.UnitSymbol;
      UnitOfMeasureId = table.UnitId;
      IsLengthRelated = table.IsLengthRelated;

      FeatureUnitConverterHelper.ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_FeatureMap), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "FeatureName", "NAME_FeatureName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string FeatureName { get; set; }

    [SmfDisplay(typeof(VM_FeatureMap), "UnitSymbol", "NAME_UnitSymbol")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [FeatureUnitSymbol(nameof(UnitOfMeasureId), nameof(FeatureId), false)]
    public string UnitSymbol { get; set; }

    public long? UnitOfMeasureId { get; }

    public long FeatureId { get; set; }
    public bool IsLengthRelated { get; set; }
  }
}
