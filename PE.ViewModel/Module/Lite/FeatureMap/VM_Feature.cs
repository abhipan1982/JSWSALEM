using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Feature
{
  public class VM_Feature : VM_Base
  {
    public VM_Feature(MVHFeature table)
    {
      FkAssetId = table.FKAssetId;
      FeatureCode = table.FeatureCode;
      FeatureName = table.FeatureName;
      FeatureId = table.FeatureId;
      IsMaterialRelated = table.IsMaterialRelated;
      IsLengthRelated = table.IsLengthRelated;
      IsActive = table.IsActive;
    }

    public VM_Feature()
    {
    }

    [SmfDisplay(typeof(VM_Feature), "FeatureId", "NAME_FeatureId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long FeatureId { get; set; }


    [SmfDisplay(typeof(VM_Feature), "FeatureCode", "NAME_FeatureCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int FeatureCode { get; set; }

    [SmfDisplay(typeof(VM_Feature), "FeatureName", "NAME_FeatureName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string FeatureName { get; set; }


    [SmfDisplay(typeof(VM_Feature), "IsNewProcessingStep", "NAME_IsNewProcessingStep")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsNewProcessingStep { get; set; }


    [SmfDisplay(typeof(VM_Feature), "FkAssetId", "NAME_FkAssetId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long FkAssetId { get; set; }

    [SmfDisplay(typeof(VM_Feature), "IsMaterialRelated", "NAME_IsMaterialRelated")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsMaterialRelated { get; set; }


    [SmfDisplay(typeof(VM_Feature), "IsLengthRelated", "NAME_IsLengthRelated")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsLengthRelated { get; set; }


    [SmfDisplay(typeof(VM_Feature), "IsActive", "NAME_IsActive")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsActive { get; set; }


    [SmfDisplay(typeof(VM_Feature), "IsTrigger", "NAME_IsTrigger")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsTrackingTrigger { get; set; }
  }
}
