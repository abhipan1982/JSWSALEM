using System;
using PE.BaseDbEntity.Models;
using PE.Helpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Delay
{
  public class VM_DelayDivision : VM_Base
  {
    public VM_DelayDivision()
    {
      EventStartTs = DateTime.Now;
    }

    public VM_DelayDivision(EVTEvent d)
    {
      Id = d.EventId;
      EventStartTs = d.EventStartTs;
      EventEndTs = d.EventEndTs ?? DateTime.Now.ExcludeMiliseconds();
      DelayCatalogueIsPlanned = d.FKEventCatalogue.IsPlanned;
      IsPlanned = d.IsPlanned;
      UserComment = d.UserComment;

      EventCatalogueCode = d.FKEventCatalogue?.EventCatalogueCode;

      EventCatalogueName = d.FKEventCatalogue?.EventCatalogueName;
      EventAssetName = d.FKAsset?.AssetName;
      WorkOrderName = d.FKWorkOrder?.WorkOrderName;
      RawMaterialName = d.FKRawMaterial?.RawMaterialName;

      CatalogueCategoryName = d.FKEventCatalogue?.FKEventCatalogueCategory?.EventCatalogueCategoryName;

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

    public long Id { get; set; }

    [SmfDisplay(typeof(VM_Delay), "DelayStart", "NAME_DelayStart")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime EventStartTs { get; set; }

    [SmfDisplay(typeof(VM_Delay), "DelayEnd", "NAME_DelayEnd")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime? EventEndTs { get; set; }

    [SmfDisplay(typeof(VM_Delay), "Comment", "NAME_Comment")]
    public string UserComment { get; set; }

    [SmfDisplay(typeof(VM_Delay), "IsPlanned", "NAME_IsPlanned")]
    public bool IsPlanned { get; set; }

    [SmfDisplay(typeof(VM_Delay), "IsPlanned", "NAME_IsPlanned")]
    public bool DelayCatalogueIsPlanned { get; set; }

    [SmfDisplay(typeof(VM_Delay), "DelayCode", "NAME_Code")]
    public string EventCatalogueCode { get; set; }

    [SmfDisplay(typeof(VM_Delay), "NewDelayLength", "NAME_NewDelayLength")]
    //[SmfUnit("UNIT_Second")]
    public int? NewDelayLength { get; set; }

    [SmfDisplay(typeof(VM_Delay), "Duration", "NAME_Duration")]
    public TimeSpan? Duration { get; set; }

    [SmfDisplay(typeof(VM_Delay), "Duration", "NAME_Duration")]
    //[SmfUnit("UNIT_Second")]
    public int DurationInSeconds { get; set; }

    [SmfDisplay(typeof(VM_Delay), "DelayDuration", "NAME_Duration")]
    public string DelayDuration { get; set; }

    [SmfDisplay(typeof(VM_Delay), "DelayCatalogueName", "NAME_DelayCatalogueName")]
    public string EventCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_Delay), "DelayAssetName", "NAME_DelayAssetName")]
    public string EventAssetName { get; set; }

    [SmfDisplay(typeof(VM_Delay), "WorkOrderName", "NAME_WorkOrderName")]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_Delay), "RawMaterialName", "NAME_RawMaterialName")]
    public string RawMaterialName { get; set; }

    [SmfDisplay(typeof(VM_Delay), "CatalogueCategoryName", "NAME_DelayCategory")]
    public string CatalogueCategoryName { get; set; }

    public long? FkEventCatalogueId { get; set; }
    public double? FkRawMaterialId { get; set; }
    public long? FkAssetId { get; set; }
    public long? FkWorkOrderId { get; set; }
    public long? FkUserId { get; set; }
  }
}
