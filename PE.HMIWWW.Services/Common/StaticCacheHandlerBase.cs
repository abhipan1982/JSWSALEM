using System.Collections.Generic;
using System.Linq;
using PE.BaseDbEntity.PEContext;

namespace PE.HMIWWW.Services.Common
{
  public static class StaticCacheHandlerBase
  {
    private static List<(long EventTypeId, short EventTypeCode, string EventTypeName)> _eventTypes;

    private static List<(long EventCatalogueId, string EventCatalogueCode, string EventCatalogueName)>
      _defaultCatalogueTypes;

    public static List<(long EventTypeId, short EventTypeCode, string EventTypeName)> EventTypes
    {
      get
      {
        if (_eventTypes == null)
        {
          _eventTypes = GetEventTypes();
          return _eventTypes;
        }

        return _eventTypes;
      }
    }


    public static List<(long EventCatalogueId, string EventCatalogueCode, string EventCatalogueName)>
      DefaultCatalogueTypes
    {
      get
      {
        if (_defaultCatalogueTypes == null)
        {
          _defaultCatalogueTypes = GetDefaultCatalogueTypes();
          return _defaultCatalogueTypes;
        }

        return _defaultCatalogueTypes;
      }
    }

    private static List<(long EventTypeId, short EventTypeCode, string EventTypeName)> GetEventTypes()
    {
      using (PEContext ctx = new PEContext())
      {
        return ctx.EVTEventTypes
          .Select(x => new {x.EventTypeId, x.EventTypeCode, x.EventTypeName})
          .ToList()
          .Select(x => (x.EventTypeId, x.EventTypeCode, x.EventTypeName))
          .ToList();
      }
    }

    private static List<(long EventCatalogueId, string EventCatalogueCode, string EventCatalogueName)>
      GetDefaultCatalogueTypes()
    {
      using (PEContext ctx = new PEContext())
      {
        return ctx.EVTEventCatalogues
          .Where(x => x.IsDefault)
          .Select(x => new {x.EventCatalogueId, x.EventCatalogueCode, x.EventCatalogueName})
          .ToList()
          .Select(x => (x.EventCatalogueId, x.EventCatalogueCode, x.EventCatalogueName))
          .ToList();
      }
    }

    public static (long EventTypeId, short EventTypeCode, string EventTypeName) GetEventTypeByCode(short code)
    {
      return EventTypes.First(x => x.EventTypeCode == code);
    }

    public static (long EventCatalogueId, string EventCatalogueCode, string EventCatalogueName)
      GetDefaultCatalogueTypeByCode(string code)
    {
      return DefaultCatalogueTypes.First(x => x.EventCatalogueCode.ToLower() == code.ToLower());
    }
  }
}
