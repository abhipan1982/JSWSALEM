using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Event
{
  public class VM_EventCatalogue : VM_Base
  {
    public VM_EventCatalogue()
    {
    }

    public VM_EventCatalogue(EVTEventCatalogue d)
    {
      //TODOMN - refactor this
      Id = d.EventCatalogueId;
      EventCatalogueCode = d.EventCatalogueCode;
      EventCatalogueName = d.EventCatalogueName;
      EventDescription = d.EventCatalogueDescription;

      StdEventTime = d.StdEventTime;
      ParentEventCatalogueId = d.FKParentEventCatalogueId;
      ParentEventCode = d.FKParentEventCatalogue?.EventCatalogueCode;
      EventCatalogueCategoryId = d.FKEventCatalogueCategoryId;
      EventCategory = d.FKEventCatalogueCategory?.EventCatalogueCategoryName;

      IsActive = d.IsActive ?? true;
      IsDefault = d.IsDefault;
      IsPlanned = d.IsPlanned;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long Id { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogue), "KeyName", "NAME_Name")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfRequired]
    public string EventCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogue), "EventCatalogueCategoryId", "NAME_Category")]
    [SmfRequired]
    public long? EventCatalogueCategoryId { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogue), "EventCategory", "NAME_Category")]
    public string EventCategory { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogue), "StdEventTime", "NAME_StdEventTime")]
    [SmfUnit("UNIT_Second")]
    [SmfFormat("FORMAT_Plain3", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public double? StdEventTime { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogue), "AssetName", "NAME_AssetName")]
    public string AssetName { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogue), "IsActive", "NAME_IsActive")]
    public bool IsActive { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogue), "IsDefault", "NAME_IsDefault")]
    public bool IsDefault { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogue), "IsPlanned", "NAME_IsPlanned")]
    public bool IsPlanned { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogue), "EventCatalogueCode", "NAME_Code")]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfRequired]
    public string EventCatalogueCode { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogue), "EventDescription", "NAME_Description")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string EventDescription { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogue), "ParentEventCatalogueId", "NAME_ParentEventCode")]
    public long? ParentEventCatalogueId { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogue), "ParentEventCode", "NAME_ParentEventCode")]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string ParentEventCode { get; set; }
  }
}
