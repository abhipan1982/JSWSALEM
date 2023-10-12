using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Event
{
  public class VM_EventCatalogueCategory : VM_Base
  {
    public VM_EventCatalogueCategory()
    {
    }

    public VM_EventCatalogueCategory(V_EventCategorySearchGrid d)
    {
      Id = d.EventCatalogueCategoryId;
      EventCatalogueCategoryName = d.EventCatalogueCategoryName;
      EventCatalogueCategoryCode = d.EventCatalogueCategoryCode;
      IsDefault = d.IsDefaultCategory;
      EventCategoryGroupId = d.EventCategoryGroupId;
      EventCategoryGroupName = d.EventCategoryGroupName;
      EventCategoryGroupCode = d.EventCategoryGroupCode;

      EventTypeId = d.EventTypeId;
      EventTypeName = d.EventTypeName;
      EventTypeCode = d.EventTypeCode;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_EventCatalogueCategory(EVTEventCatalogueCategory d)
    {
      Id = d.EventCatalogueCategoryId;
      EventCatalogueCategoryName = d.EventCatalogueCategoryName;
      EventCatalogueCategoryCode = d.EventCatalogueCategoryCode;
      EventCatalogueCategoryDescription = d.EventCatalogueCategoryDescription;
      IsDefault = d.IsDefault;
      EventCategoryGroupId = d.FKEventCategoryGroup?.EventCategoryGroupId;
      EventCategoryGroupName = d.FKEventCategoryGroup?.EventCategoryGroupName;
      EventCategoryGroupCode = d.FKEventCategoryGroup?.EventCategoryGroupCode;
      EnumAssignmentTypeId = d.EnumAssignmentType;
      EnumAssignmentTypeName = ResxHelper.GetResxByKey("ENUM_AssignmentType_" + d.EnumAssignmentType.Name);

      EventTypeId = d.FKEventTypeId;
      EventTypeName = d.FKEventType?.EventTypeName;
      EventTypeCode = d.FKEventType?.EventTypeCode;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long Id { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogueCategory), "EventCatalogueCategoryName", "NAME_EventCatalogueCategory")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    [SmfStringLength(50)]
    public string EventCatalogueCategoryName { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogueCategory), "EventCatalogueCategoryCode", "NAME_EventCatalogueCategoryCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    [SmfStringLength(10)]
    public string EventCatalogueCategoryCode { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogueCategory), "EventCatalogueCategoryDescription", "NAME_EventCatalogueCategoryDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfStringLength(100)]
    public string EventCatalogueCategoryDescription { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogueCategory), "EventCategoryGroupName", "NAME_EventCategoryGroup")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? EventCategoryGroupId { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogueCategory), "EventCategoryGroupName", "NAME_EventCategoryGroup")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string EventCategoryGroupName { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogueCategory), "EventCategoryGroupCode", "NAME_EventCategoryGroupCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string EventCategoryGroupCode { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogueCategory), "IsDefault", "NAME_IsDefault")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsDefault { get; set; }

    [SmfDisplay(typeof(VM_EventGroupsCatalogue), "EnumAssignmentType", "NAME_EnumAssignmentType")]
    public short EnumAssignmentTypeId { get; set; }

    [SmfDisplay(typeof(VM_EventGroupsCatalogue), "EnumAssignmentType", "NAME_EnumAssignmentType")]
    public string EnumAssignmentTypeName { get; set; }

    [SmfDisplay(typeof(VM_EventGroupsCatalogue), "EventTypeName", "NAME_EventType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [Required]
    public long? EventTypeId { get; set; }

    [SmfDisplay(typeof(VM_EventGroupsCatalogue), "EventTypeName", "NAME_EventType")]
    public string EventTypeName { get; set; }

    public short? EventTypeCode { get; set; }
  }
}
