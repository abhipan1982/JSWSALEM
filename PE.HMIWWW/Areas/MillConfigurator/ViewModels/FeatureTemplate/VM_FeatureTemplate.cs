using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.Areas.MillConfigurator.ViewModels.FeatureTemplate
{
  public class VM_FeatureTemplate : VM_Base
  {
    public VM_FeatureTemplate() { }

    public VM_FeatureTemplate(MVHFeatureTemplate data)
    {
      FeatureTemplateId = data.FeatureTemplateId;
      FKUnitOfMeasureId = data.FKUnitOfMeasureId;
      FKExtUnitOfMeasureId = data.FKExtUnitOfMeasureId;
      FKDataTypeId = data.FKDataTypeId;
      DataType = data.FKDataType?.DataType;
      FeatureTemplateName = data.FeatureTemplateName;
      FeatureTemplateDescription = data.FeatureTemplateDescription;
      EnumFeatureType = data.EnumFeatureType;
      EnumCommChannelType = data.EnumCommChannelType;
      EnumAggregationStrategy = data.EnumAggregationStrategy;
      TemplateCommAttr1 = data.TemplateCommAttr1;
      TemplateCommAttr2 = data.TemplateCommAttr2;
      TemplateCommAttr3 = data.TemplateCommAttr3;

      FeatureTypeText = ResxHelper.GetResxByKey(data.EnumFeatureType);
      AggregationStrategyText = ResxHelper.GetResxByKey(data.EnumAggregationStrategy);
      CommChannelTypeText = ResxHelper.GetResxByKey(data.EnumCommChannelType);
    }

    public long FeatureTemplateId { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "FKUnitOfMeasureId", "NAME_UnitOfMeasure")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public long FKUnitOfMeasureId { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "UnitOfMeasureSymbol", "NAME_UnitOfMeasure")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string UnitOfMeasureSymbol { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "FKExtUnitOfMeasureId", "NAME_ExternalUnitOfMeasure")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public long FKExtUnitOfMeasureId { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "ExtUnitOfMeasureSymbol", "NAME_ExternalUnitOfMeasure")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ExtUnitOfMeasureSymbol { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "FKDataTypeId", "NAME_DataType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public long FKDataTypeId { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "DataType", "NAME_DataType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DataType { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "FeatureTemplateName", "NAME_FeatureTemplateName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    [SmfStringLength(75)]
    public string FeatureTemplateName { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "FeatureTemplateDescription", "NAME_FeatureTemplateDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(100)]
    public string FeatureTemplateDescription { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "EnumFeatureType", "NAME_FeatureType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public short EnumFeatureType { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "FeatureTypeText", "NAME_FeatureType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string FeatureTypeText { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "EnumCommChannelType", "NAME_CommChannelType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public short EnumCommChannelType { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "CommChannelTypeText", "NAME_CommChannelType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string CommChannelTypeText { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "EnumAggregationStrategy", "NAME_AggregationStrategy")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public short EnumAggregationStrategy { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "AggregationStrategyText", "NAME_AggregationStrategy")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AggregationStrategyText { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "TemplateCommAttr1", "NAME_TemplateCommAttr1")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(350)]
    public string TemplateCommAttr1 { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "TemplateCommAttr2", "NAME_TemplateCommAttr2")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(350)]
    public string TemplateCommAttr2 { get; set; }

    [SmfDisplay(typeof(VM_FeatureTemplate), "TemplateCommAttr3", "NAME_TemplateCommAttr3")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(350)]
    public string TemplateCommAttr3 { get; set; }
  }
}
