using System;
using System.Collections.Generic;
using System.Text;
using PE.DbEntity.HmiModels;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Event
{
  public class VM_EventCatalogueSearchGrid
  {
    public long EventTypeId { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogueSearchGrid), "EventTypeCode", "NAME_EventType")]
    public short EventTypeCode { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogueSearchGrid), "EventTypeName", "NAME_EventTypeName")]
    public string EventTypeName { get; set; }
    public long? ParentEventTypeId { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogueSearchGrid), "ParentEventTypeCode", "NAME_ParentEventType")]
    public short? ParentEventTypeCode { get; set; }
    public string ParentEventTypeName { get; set; }
    public long? EventCategoryGroupId { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogueSearchGrid), "EventCategoryGroupCode", "NAME_EventCategoryGroupCode")]
    public string EventCategoryGroupCode { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogueSearchGrid), "EventCategoryGroupName", "NAME_EventCategoryGroup")]
    public string EventCategoryGroupName { get; set; }

    public long? EventCatalogueCategoryId { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogueSearchGrid), "EventCatalogueCategoryCode", "NAME_EventCatalogueCategoryCode")] 
    public string EventCatalogueCategoryCode { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogueSearchGrid), "EventCatalogueCategoryName", "NAME_EventCatalogueCategory")]
    public string EventCatalogueCategoryName { get; set; }
    public long? EventCatalogueId { get; set; }
    public string EventCatalogueCode { get; set; }
    public string EventCatalogueName { get; set; }
    public long? ParentEventCatalogueId { get; set; }
    public string ParentEventCatalogueCode { get; set; }
    public string ParentEventCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_EventCatalogueSearchGrid), "IsActiveCatalogue", "NAME_IsActive")]
    public bool IsActiveCatalogue { get; set; }
    public bool IsDefaultCatalogue { get; set; }
    public bool IsDefaultCategory { get; set; }

    public VM_EventCatalogueSearchGrid(short eventTypeCode, string eventCategoryGroupCode, string eventCatalogueCategoryCode)
    {
      EventTypeCode = eventTypeCode;
      EventCategoryGroupCode = eventCategoryGroupCode;
      EventCatalogueCategoryCode = eventCatalogueCategoryCode;
    }

    public VM_EventCatalogueSearchGrid(V_EventCatalogueSearchGrid entity)
    {
      EventTypeId = entity.EventTypeId;
      EventTypeCode = entity.EventTypeCode;
      EventTypeName = entity.EventTypeName;

      ParentEventTypeId = entity.ParentEventTypeId;
      ParentEventTypeCode = entity.ParentEventTypeCode;
      ParentEventTypeName = entity.ParentEventTypeName;

      EventCategoryGroupId = entity.EventCategoryGroupId;
      EventCategoryGroupCode = entity.EventCategoryGroupCode;
      EventCategoryGroupName = entity.EventCategoryGroupName;

      EventCatalogueCategoryId = entity.EventCatalogueCategoryId;
      EventCatalogueCategoryCode = entity.EventCatalogueCategoryCode;
      EventCatalogueCategoryName = entity.EventCatalogueCategoryName;

      EventCatalogueId = entity.EventCatalogueId;
      EventCatalogueCode = entity.EventCatalogueCode;
      EventCatalogueName = entity.EventCatalogueName;

      ParentEventCatalogueId = entity.ParentEventCatalogueId;
      ParentEventCatalogueCode = entity.ParentEventCatalogueCode;
      ParentEventCatalogueName = entity.ParentEventCatalogueName;

      IsActiveCatalogue = entity.IsActiveCatalogue;
      IsDefaultCatalogue = entity.IsDefaultCatalogue;
      IsDefaultCategory = entity.IsDefaultCategory;
    }

    public VM_EventCatalogueSearchGrid(V_EventsStructureSearchGrid eventStructureSearchGrid)
    {
      EventTypeId = eventStructureSearchGrid.EventTypeId;
      EventTypeCode = eventStructureSearchGrid.EventTypeCode;
      EventTypeName = eventStructureSearchGrid.EventTypeName;

      ParentEventTypeId = eventStructureSearchGrid.ParentEventTypeId;
      ParentEventTypeCode = eventStructureSearchGrid.ParentEventTypeCode;
      ParentEventTypeName = eventStructureSearchGrid.ParentEventTypeName;

      EventCategoryGroupId = eventStructureSearchGrid.EventCategoryGroupId;
      EventCategoryGroupCode = eventStructureSearchGrid.EventCategoryGroupCode;
      EventCategoryGroupName = eventStructureSearchGrid.EventCategoryGroupName;

      EventCatalogueCategoryId = eventStructureSearchGrid.EventCatalogueCategoryId;
      EventCatalogueCategoryCode = eventStructureSearchGrid.EventCatalogueCategoryCode;
      EventCatalogueCategoryName = eventStructureSearchGrid.EventCatalogueCategoryName;

      EventCatalogueId = eventStructureSearchGrid.EventCatalogueId;
      EventCatalogueCode = eventStructureSearchGrid.EventCatalogueCode;
      EventCatalogueName = eventStructureSearchGrid.EventCatalogueName;

      ParentEventCatalogueId = eventStructureSearchGrid.ParentEventCatalogueId;
      ParentEventCatalogueCode = eventStructureSearchGrid.ParentEventCatalogueCode;
      ParentEventCatalogueName = eventStructureSearchGrid.ParentEventCatalogueName;
    }
  }
}
