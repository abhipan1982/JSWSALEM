using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.Areas.MillConfigurator.ViewModels.AssetTemplate
{
  public class VM_AreaTemplate : VM_Base
  {
    public VM_AreaTemplate() { }

    public VM_AreaTemplate(MVHAssetTemplate data)
    {
      AssetTemplateId = data.AssetTemplateId;
      AssetTemplateName = data.AssetTemplateName;
      AssetTemplateDescription = data.AssetTemplateDescription;
      EnumTrackingAreaType = data.EnumTrackingAreaType;
      IsZone = data.IsZone;
    }

    public long AssetTemplateId { get; set; }

    [SmfDisplay(typeof(VM_AreaTemplate), "AssetTemplateName", "NAME_AreaTemplateName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    [SmfStringLength(50)]
    public string AssetTemplateName { get; set; }

    [SmfDisplay(typeof(VM_AreaTemplate), "AssetTemplateDescription", "NAME_Description")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(100)]
    public string AssetTemplateDescription { get; set; }

    [SmfDisplay(typeof(VM_AreaTemplate), "EnumTrackingAreaType", "NAME_TrackingAreaType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public short EnumTrackingAreaType { get; set; }

    [SmfDisplay(typeof(VM_AssetTemplate), "IsZone", "NAME_IsZone")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsZone { get; set; }

    [SmfDisplay(typeof(VM_AssetTemplate), "CloneFeatureTemplates", "NAME_CloneFeatureTemplates")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool CloneFeatureTemplates { get; set; }
  }
}
