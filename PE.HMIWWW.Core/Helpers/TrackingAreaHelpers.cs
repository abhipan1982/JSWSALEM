using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.Model;

namespace PE.HMIWWW.Core.Helpers
{
  public static class TrackingAreaHelpers
  {
    public static List<TrackingAreaModel> TrackingAreas;

    public static void InitTrackingAreas()
    {
      if (TrackingAreas == null || TrackingAreas.Count() == 0)
      {
        using PEContext ctx = new PEContext();
        List<MVHAsset> areas = ctx.MVHAssets.Where(x => x.IsArea).ToList();
        List<TrackingAreaModel> trackingAreas = new List<TrackingAreaModel>();
        foreach (FieldInfo propertyItem in typeof(TrackingArea).GetFields(BindingFlags.Public |
          BindingFlags.Static))
        {
          dynamic item = propertyItem.GetValue(null);
          var area = areas.Where(x => x.AssetCode == (int)item.Value).FirstOrDefault();

          if (item > 0 && area != null)
          {
            TrackingAreaModel trackingArea = new TrackingAreaModel
            {
              TrackingAreaName = item.Name,
              TrackingAreaCode = (int)item.Value,
              TrackingAreaTitle = ResxHelper.GetResxByKey($"NAME_AREA_{(int)item.Value}"),
              TrackingAreaPositions = area.PositionsNumber ?? 0,
              TrackingAreaVirtualPositions = area.VirtualPositionsNumber ?? 0
            };

            trackingAreas.Add(trackingArea);
          }
        }

        TrackingAreas = trackingAreas
          .GroupBy(x => new { x.TrackingAreaCode })
          .Select(x => x.First())
          .ToList();
      }
    }
  }
}
