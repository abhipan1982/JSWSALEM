using System;
using System.Threading.Tasks;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.PEContext;
using PE.BaseInterfaces.SendOffices.TRK;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.Helpers;
using PE.TRK.Base.Handlers;
using PE.TRK.Base.Providers.Abstract;
using SMF.Core.DC;
using SMF.Core.Helpers;
using SMF.Core.Notification;

namespace PE.TRK.Base.Providers.Concrete
{
  public class TrackingMaterialProcessingProviderBase : ITrackingMaterialProcessingProviderBase
  {
    protected readonly TrackingHandlerBase TrackingHandler;
    protected readonly ITrackingProcessMaterialEventSendOfficeBase TrackingProcessMaterialEventSendOffice;
    public TrackingMaterialProcessingProviderBase(TrackingHandlerBase trackingHandler,
      ITrackingProcessMaterialEventSendOfficeBase trackingProcessMaterialEventSendOffice)
    {
      TrackingHandler = trackingHandler;
      TrackingProcessMaterialEventSendOffice = trackingProcessMaterialEventSendOffice;
    }

    public async Task<DataContractBase> ProcessScrapMessage(DCL1ScrapData message)
    {
      var result = new DataContractBase();
      try
      {
        using var ctx = new PEContext();
        var rawMaterial = await TrackingHandler.FindRawMaterialByIdAsync(ctx, message.Id);
        var now = DateTime.Now.ExcludeMiliseconds();
        TypeOfScrap scrapType = message.TypeOfScrap;

        if (message.TypeOfScrap == TypeOfScrap.Scrap)
        {
          if (rawMaterial.EnumTypeOfScrap != (short)scrapType)
          {
            TaskHelper.FireAndForget(async () => await TrackingProcessMaterialEventSendOffice.AddMillEvent(new()
            {
              EventType = ChildMillEventType.FullScrap,
              RawMaterialId = rawMaterial.RawMaterialId,
              UserId = message.HmiInitiator?.UserId,
              DateStart = now,
              DateEnd = now,
              AssetId = message.AssetId
            }));
          }
        }
        else if (message.TypeOfScrap == TypeOfScrap.PartialScrap)
        {
          if (message.ScrapPercent.HasValue && message.ScrapPercent.Value > 0)
          {
            if (rawMaterial.EnumTypeOfScrap != (short)scrapType)
            {
              TaskHelper.FireAndForget(async () => await TrackingProcessMaterialEventSendOffice.AddMillEvent(new()
              {
                EventType = ChildMillEventType.PartialScrap,
                RawMaterialId = rawMaterial.RawMaterialId,
                UserId = message.HmiInitiator?.UserId,
                DateStart = now,
                DateEnd = now,
                AssetId = message.AssetId
              }));
            }
          }
          else
          {
            scrapType = TypeOfScrap.None;
          }
        }
        else
        {
          TaskHelper.FireAndForget(async () => await TrackingProcessMaterialEventSendOffice.AddMillEvent(new()
          {
            EventType = ChildMillEventType.UnscrapMaterial,
            RawMaterialId = rawMaterial.RawMaterialId,
            UserId = message.HmiInitiator?.UserId,
            DateStart = now,
            DateEnd = now,
            AssetId = message.AssetId
          }));
        }

        rawMaterial.EnumTypeOfScrap = scrapType;
        rawMaterial.ScrapPercent = message.ScrapPercent;
        rawMaterial.ScrapRemarks = message.ScrapRemark;
        rawMaterial.FKScrapAssetId = message.AssetId;

        if (scrapType == TypeOfScrap.Scrap)
          rawMaterial.EnumRawMaterialStatus = RawMaterialStatus.Scrap;

        await ctx.SaveChangesAsync();

        TaskHelper.FireAndForget(async () => await TrackingProcessMaterialEventSendOffice.CheckShiftsWorkOrderStatusses(new()
        {
          ShiftCalendarId = rawMaterial.FKShiftCalendarId
        }));

        NotificationController.Warn($"Material: {rawMaterial.RawMaterialId}:{rawMaterial.RawMaterialName} " +
                                    $"was successfully updated after Processing Scrap Message");

        TaskHelper.FireAndForget(async () =>
              await TrackingProcessMaterialEventSendOffice.AssignRawMaterialQualityAsync(new()
              {
                RawMaterialId = rawMaterial.RawMaterialId,
                CrashTest = CrashTest.Undefined,
                InspectionResult = scrapType == TypeOfScrap.Scrap ? InspectionResult.Bad : InspectionResult.Doubtful,
              }), $"Something went wrong while AssignQuality for Material: {rawMaterial.RawMaterialId}");
      }
      catch (Exception ex)
      {
        NotificationController.Error($"[CRITICAL] fun {MethodHelper.GetMethodName()} failed.");
        NotificationController.LogException(ex);
        throw;
      }

      return result;
    }
  }
}
