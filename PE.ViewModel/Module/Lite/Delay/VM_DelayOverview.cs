using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Delay
{
  public class VM_DelayOverview : VM_Base
  {
    public VM_DelayOverview(EVTEvent data)
    {
      EventId = data.EventId;
      EventStartTs = data.EventStartTs;
      EventEndTs = data.EventEndTs;
      DelayCatalogueIsPlanned = data.FKEventCatalogue.IsPlanned;
      IsPlanned = data.IsPlanned;
      UserComment = data.UserComment;
      EventCatalogueCode = data.FKEventCatalogue?.EventCatalogueCode;
      EventCatalogueName = data.FKEventCatalogue?.EventCatalogueName;
      EventCatalogueName = data.FKEventCatalogue?.EventCatalogueName;
      AssetName = data.FKAsset?.AssetDescription;
      RawMaterialName = data.FKRawMaterial?.RawMaterialName;
      EventCatalogueCategoryName = data.FKEventCatalogue?.FKEventCatalogueCategory?.EventCatalogueCategoryName;
      FkEventCatalogueId = data.FKEventCatalogueId;
      FkAssetId = data.FKAssetId;
      FkRawMaterialId = data.FKRawMaterialId;
      NewDelayLength = 0;

      if (data.EventEndTs != null)
      {
        Duration = data.EventEndTs - data.EventStartTs;
        DurationInSeconds = (int)Math.Round(Duration.Value.TotalSeconds);
        TimeSpan time = TimeSpan.FromSeconds(DurationInSeconds);

        NewDelayLength = DurationInSeconds;

        DelayDurationText = time.ToString(@"hh\:mm\:ss");
      }

      ShouldBeUpdated = data.FKEventCatalogue.IsDefault ||
        (data.FKEventCatalogue.FKEventCatalogueCategory.EnumAssignmentType == AssignmentType.Assigned.Value && !data.FKWorkOrderId.HasValue);

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_DelayOverview(V_DelayOverview data)
    {
      EventId = data.EventId;
      EventStartTs = data.EventStartTs;
      EventEndTs = data.EventEndTs;
      IsPlanned = data.IsPlanned;
      UserComment = data.UserComment;
      EventCatalogueCode = data.EventCatalogueCode;
      EventCatalogueName = data.EventCatalogueName;
      RawMaterialName = data.RawMaterialName;
      EventCatalogueCategoryName = data.EventCatalogueCategoryName;
      AssetId = data.AssetId;
      FkRawMaterialId = data.RawMaterialId;
      NewDelayLength = 0;
      DelayDuration = data.DelayDuration;
      AssetName = data.AssetName;
      RawMaterialName = data.RawMaterialName;

      if (EventEndTs != null)
      {
        Duration = EventEndTs - EventStartTs;
        DurationInSeconds = (int)Math.Round(Duration.Value.TotalSeconds);
        TimeSpan time = TimeSpan.FromSeconds(DurationInSeconds);
        NewDelayLength = DurationInSeconds;
        DelayDurationText = time.ToString(@"hh\:mm\:ss");
      }
      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_DelayOverview(V_Event data)
    {
      EventId = data.EventId;
      EventStartTs = data.EventStartTs;
      EventEndTs = data.EventEndTs;
      UserComment = data.UserComment;
      AssetId = data.AssetId;
      EventCatalogueName = data.EventCatalogueName;
      AssetName = data.AssetName;
      RawMaterialName = data.RawMaterialName;
      FkRawMaterialId = data.RawMaterialId;

      if (EventEndTs != null)
      {
        Duration = EventEndTs - EventStartTs;
        DurationInSeconds = (int)Math.Round(Duration.Value.TotalSeconds);
        TimeSpan time = TimeSpan.FromSeconds(DurationInSeconds);
        NewDelayLength = DurationInSeconds;
        DelayDurationText = time.ToString(@"hh\:mm\:ss");
      }

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long EventId { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_Delay), "EventStartTs", "NAME_DelayStart")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime EventStartTs { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_Delay), "EventEndTs", "NAME_DelayEnd")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime? EventEndTs { get; set; }

    [SmfDisplay(typeof(VM_Delay), "UserComment", "NAME_Comment")]
    public string UserComment { get; set; }

    [SmfDisplay(typeof(VM_Delay), "IsPlanned", "NAME_IsPlanned")]
    public bool IsPlanned { get; set; }

    [SmfDisplay(typeof(VM_Delay), "DelayCatalogueIsPlanned", "NAME_IsPlanned")]
    public bool DelayCatalogueIsPlanned { get; set; }

    [SmfDisplay(typeof(VM_Delay), "EventCatalogueCode", "NAME_Code")]
    public string EventCatalogueCode { get; set; }

    [SmfDisplay(typeof(VM_Delay), "NewDelayLength", "NAME_NewDelayLength")]
    //[SmfUnit("UNIT_Second")]
    public int? NewDelayLength { get; set; }

    [SmfDisplay(typeof(VM_Delay), "Duration", "NAME_Duration")]
    public TimeSpan? Duration { get; set; }

    [SmfDisplay(typeof(VM_Delay), "DurationInSeconds", "NAME_Duration")]
    //[SmfUnit("UNIT_Second")]
    public int DurationInSeconds { get; set; }

    [SmfDisplay(typeof(VM_Delay), "DelayDurationText", "NAME_Duration")]
    public string DelayDurationText { get; set; }

    public double? DelayDuration { get; set; }

    [SmfDisplay(typeof(VM_Delay), "EventCatalogueName", "NAME_DelayCatalogueName")]
    public string EventCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_Delay), "AssetName", "NAME_DelayAssetName")]
    public string AssetName { get; set; }

    [SmfDisplay(typeof(VM_Delay), "RawMaterialName", "NAME_RawMaterialName")]
    public string RawMaterialName { get; set; }

    [SmfDisplay(typeof(VM_Delay), "EventCatalogueCategoryName", "NAME_DelayCategory")]
    public string EventCatalogueCategoryName { get; set; }

    public long? FkEventCatalogueId { get; set; }
    public double? FkRawMaterialId { get; set; }
    public long? AssetId { get; set; }
    public long? FkAssetId { get; set; }
    public bool ShouldBeUpdated { get; set; }
  }
}
