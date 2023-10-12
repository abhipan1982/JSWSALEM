using System;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using PE.Helpers;

namespace PE.HMIWWW.ViewModel.Module.Lite.Delay
{
  public class VM_Delay : VM_Base
  {
    public VM_Delay()
    {
      EventStartTs = DateTime.Now;
    }

    public VM_Delay(EVTEvent d, DateTime? defaultEventEnd = null)
    {
      Id = d.EventId;
      EventStartTs = d.EventStartTs;
      EventEndTs = d.EventEndTs ?? defaultEventEnd;
      DelayCatalogueIsPlanned = d.FKEventCatalogue?.IsPlanned ?? false;
      IsPlanned = d.IsPlanned;
      UserComment = d.UserComment;

      DelayCatalogue = d.FKEventCatalogue?.EventCatalogueCode;

      DelayCatalogueName = d.FKEventCatalogue?.EventCatalogueName;
      DelayAssetName = d.FKAsset?.AssetDescription;
      WorkOrderName = d.FKWorkOrder?.WorkOrderName;
      RawMaterialName = d.FKRawMaterial?.RawMaterialName;

      CatalogueCategoryName = d.FKEventCatalogue?.FKEventCatalogueCategory?.EventCatalogueCategoryName;
      CatalogueCategoryId = d.FKEventCatalogue?.FKEventCatalogueCategory?.EventCatalogueCategoryId;

      FkEventCatalogueId = d.FKEventCatalogueId;
      FkAssetId = d.FKAssetId;
      FkWorkOrderId = d.FKWorkOrderId;
      FkRawMaterialId = d.FKRawMaterialId;
      NewDelayLength = 0;

      if (EventEndTs != null)
      {
        Duration = EventEndTs - EventStartTs;
        DurationInSeconds = (int)Math.Round(Duration.Value.TotalSeconds);
        TimeSpan time = TimeSpan.FromSeconds(DurationInSeconds);

        NewDelayLength = DurationInSeconds;

        DelayDuration = time.ToString(@"hh\:mm\:ss");
      }

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_Delay(V_DelayOverview d, DateTime? defaultEventEnd = null)
    {
      Id = d.EventId;
      EventStartTs = d.EventStartTs;
      EventEndTs = d.EventEndTs ?? defaultEventEnd;
      DelayCatalogueIsPlanned = d.IsPlanned;
      UserComment = d.UserComment;
      ShiftCode = d.ShiftCode;

      DelayCatalogueName = d.EventCatalogueName;

      CatalogueCategoryName = d.EventCatalogueCategoryName;
      //FkUserId = d.FKUserId;
      NewDelayLength = 0;

      if (EventEndTs != null)
      {
        Duration = EventEndTs - EventStartTs;
        DurationInSeconds = (int)Math.Round(Duration.Value.TotalSeconds);
        TimeSpan time = TimeSpan.FromSeconds(DurationInSeconds);

        NewDelayLength = DurationInSeconds;

        DelayDuration = time.ToString(@"hh\:mm\:ss");
      }

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long Id { get; set; }

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

    [SmfDisplay(typeof(VM_Delay), "DelayCatalogue", "NAME_Code")]
    public string DelayCatalogue { get; set; }

    [SmfDisplay(typeof(VM_Delay), "NewDelayLength", "NAME_NewDelayLength")]
    //[SmfUnit("UNIT_Second")]
    public int? NewDelayLength { get; set; }

    [SmfDisplay(typeof(VM_Delay), "Duration", "NAME_Duration")]
    public TimeSpan? Duration { get; set; }

    [SmfDisplay(typeof(VM_Delay), "DurationInSeconds", "NAME_Duration")]
    //[SmfUnit("UNIT_Second")]
    public int DurationInSeconds { get; set; }

    [SmfDisplay(typeof(VM_Delay), "DelayDuration", "NAME_Duration")]
    public string DelayDuration { get; set; }

    [SmfDisplay(typeof(VM_Delay), "DelayCatalogueName", "NAME_DelayCatalogueName")]
    public string DelayCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_Delay), "DelayAssetName", "NAME_DelayAssetName")]
    public string DelayAssetName { get; set; }

    [SmfDisplay(typeof(VM_Delay), "WorkOrderName", "NAME_WorkOrderName")]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_Delay), "RawMaterialName", "NAME_RawMaterialName")]
    public string RawMaterialName { get; set; }

    [SmfDisplay(typeof(VM_Delay), "CatalogueCategoryName", "NAME_DelayCategory")]
    public string CatalogueCategoryName { get; set; }

    public long? CatalogueCategoryId { get; set; }

    [SmfDisplay(typeof(VM_Delay), "FkEventCatalogueId", "NAME_DelayCatalogueName")]
    [SmfRequired]
    public long? FkEventCatalogueId { get; set; }

    public double? FkRawMaterialId { get; set; }

    public long? FkAssetId { get; set; }

    public long? FkWorkOrderId { get; set; }

    public long? FkUserId { get; set; }

    public string ShiftCode { get; set; }
  }
}
