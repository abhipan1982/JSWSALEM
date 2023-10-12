using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Asset;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.ConsumptionMonitoring
{
  public class VM_Feature : VM_Base
  {
    public VM_Feature() { }

    public VM_Feature(V_Feature data)
    {
      FeatureId = data.FeatureId;
      FeatureCode = data.FeatureCode;
      FeatureName = data.FeatureName;
      FeatureUnitSymbol = data.UnitSymbol;
      AssetCode = data.AssetCode;
      AssetName = data.AssetName;

      ConvertToLocal(this);
    }
    [SmfDisplay(typeof(VM_Asset), "AssetCode", "NAME_AssetCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int AssetCode { get; set; }

    [SmfDisplay(typeof(VM_Asset), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }

    [SmfDisplay(typeof(VM_Feature), "FeatureId", "NAME_FeatureId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FeatureId { get; set; }

    [SmfDisplay(typeof(VM_Feature), "FeatureCode", "NAME_FeatureCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? FeatureCode { get; set; }

    [SmfDisplay(typeof(VM_Feature), "FeatureName", "NAME_FeatureName")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    public string FeatureName { get; set; }

    [SmfDisplay(typeof(VM_Feature), "FeatureUnit", "NAME_UnitSymbol")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    public string FeatureUnitSymbol { get; set; }
  }
}
