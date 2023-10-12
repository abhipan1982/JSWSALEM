using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.Areas.MillConfigurator.ViewModels.AssetTemplate
{
  public class VM_AssetTemplate : VM_Base
  {
    public VM_AssetTemplate() { }

    public VM_AssetTemplate(MVHAssetTemplate data)
    {
      AssetTemplateId = data.AssetTemplateId;
      AssetTemplateName = data.AssetTemplateName;
      AssetTemplateDescription = data.AssetTemplateDescription;
      IsArea = data.IsArea;
      IsZone = data.IsZone;
      EnumTrackingAreaType = data.EnumTrackingAreaType;

      EnumTrackingAreaTypeText = ResxHelper.GetResxByKey(data.EnumTrackingAreaType);
    }

    public long AssetTemplateId { get; set; }

    [SmfDisplay(typeof(VM_AssetTemplate), "AssetTemplateName", "NAME_AssetTemplateName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    [SmfStringLength(50)]
    public string AssetTemplateName { get; set; }

    [SmfDisplay(typeof(VM_AssetTemplate), "AssetTemplateDescription", "NAME_AssetTemplateDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(100)]
    public string AssetTemplateDescription { get; set; }

    [SmfDisplay(typeof(VM_AssetTemplate), "IsArea", "NAME_IsArea")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsArea { get; set; }

    [SmfDisplay(typeof(VM_AssetTemplate), "IsZone", "NAME_IsZone")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsZone { get; set; }

    [SmfDisplay(typeof(VM_AssetTemplate), "EnumTrackingAreaTypeText", "NAME_TrackingAreaType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string EnumTrackingAreaTypeText { get; set; }

    [SmfDisplay(typeof(VM_AssetTemplate), "EnumTrackingAreaType", "NAME_TrackingAreaType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumTrackingAreaType { get; set; }

    [SmfDisplay(typeof(VM_AssetTemplate), "CloneFeatureTemplates", "NAME_CloneFeatureTemplates")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool CloneFeatureTemplates { get; set; }
  }
}
