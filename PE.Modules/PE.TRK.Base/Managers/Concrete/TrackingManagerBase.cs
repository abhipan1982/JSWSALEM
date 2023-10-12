using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseInterfaces.SendOffices.TRK;
using PE.BaseModels.DataContracts.External.TCP.Telegrams;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.BaseModels.DataContracts.Internal.HMI;
using PE.BaseModels.DataContracts.Internal.MNT;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.RLS;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.Common;
using PE.Core;
using PE.Helpers;
using PE.TRK.Base.Extensions;
using PE.TRK.Base.Handlers;
using PE.TRK.Base.Managers.Abstract;
using PE.TRK.Base.Models._Base;
using PE.TRK.Base.Models.Configuration.Concrete;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Abstract;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Concrete;
using PE.TRK.Base.Models.TrackingComponents.Collections.Concrete;
using PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Concrete;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Extensions;
using SMF.Core.Helpers;
using SMF.Core.Infrastructure;
using SMF.Core.Interfaces;
using SMF.Core.Notification;
using static Microsoft.EntityFrameworkCore.Design.OperationExecutor;

namespace PE.TRK.Base.Managers.Concrete
{
  public class TrackingManagerBase : BaseManager, ITrackingManagerBase
  {
    protected readonly ITrackingEventStorageProviderBase EventStorageProvider;
    protected readonly ITrackingHmiSendOfficeBase TrackingHmiSendOffice;
    protected readonly ITrackingProcessMaterialEventSendOfficeBase TrackingProcessMaterialEventSendOffice;
    protected readonly IParameterService ParameterService;
    protected readonly ITrackingStorageProviderBase StorageProvider;
    protected readonly ITrackingMaterialProcessingProviderBase TrackingMaterialProcessingProviderBase;

    protected long ElapsedSinceLastSend = 0;
    protected long CurrentSendPeriod = 0;
    protected readonly int MaxMaterialPositionSendPeriod = 30;
    protected List<int> HashCodes = new List<int>();
    protected readonly TrackingHandlerBase TrackingHandler;
    protected readonly TrackingRawMaterialHandlerBase TrackingRawMaterialHandler;
    protected readonly double WeightLossFactor;

    public TrackingManagerBase(ITrackingEventStorageProviderBase eventStorageProvider,
      ITrackingHmiSendOfficeBase trackingHmiSendOffice,
      ITrackingProcessMaterialEventSendOfficeBase trackingProcessMaterialEventSendOffice,
      IParameterService parameterService,
      ITrackingStorageProviderBase storageProvider,
      IModuleInfo moduleInfo,
      TrackingHandlerBase trackingHandler,
      TrackingRawMaterialHandlerBase trackingRawMaterialHandler,
      ITrackingMaterialProcessingProviderBase trackingMaterialProcessingProviderBase)
      : base(moduleInfo)
    {
      TrackingProcessMaterialEventSendOffice = trackingProcessMaterialEventSendOffice;
      EventStorageProvider = eventStorageProvider;
      TrackingHmiSendOffice = trackingHmiSendOffice;
      ParameterService = parameterService;
      StorageProvider = storageProvider;
      WeightLossFactor = parameterService.GetParameter("WeightLossFactor")?.ValueFloat ?? (double)0.97;
      TrackingHandler = trackingHandler;
      TrackingRawMaterialHandler = trackingRawMaterialHandler;
      TrackingMaterialProcessingProviderBase = trackingMaterialProcessingProviderBase;
    }

    /// <summary>
    /// Method for printing tracking line
    /// </summary>
    /// <returns></returns>
    public virtual Task PrintStateAsync()
    {
      //try
      //{
      //  string returnValue = "\n";
      //  List<string> trackingPointIds = StorageProvider.Materials
      //    .Values
      //    .First()
      //    .TrackingPoints
      //    .OrderBy(x => x.AssetCode)
      //    .Select(y => y.AssetCode
      //      .ToString())
      //    .ToList();
      //  returnValue += "".PadLeft(10);

      //  var maxIndex = trackingPointIds.Max(x => x.Length);

      //  for (int i = 0; i < maxIndex; i++)
      //  {
      //    for (int j = 0; j < trackingPointIds.Count; j++)
      //    {
      //      var trackingPoint = trackingPointIds[j];
      //      returnValue += trackingPoint.Length <= i ? "".PadLeft(1) : trackingPoint[i].ToString().PadLeft(1);
      //    }
      //    returnValue += "\n";
      //    returnValue += "".PadLeft(10);
      //  }

      //  returnValue += "\n";
      //  //returnValue += "".PadLeft(10);

      //  List<CtrMaterialBase> workflowDatas =
      //    StorageProvider.Materials.Values.Where(x => x.MaterialInfo.MaterialId > 0).ToList();
      //  for (int i = 0; i < workflowDatas.Count; i++)
      //  {
      //    returnValue += workflowDatas[i].MaterialInfo?.MaterialId == null
      //      ? "".PadLeft(10)
      //      : workflowDatas[i].MaterialInfo.MaterialId.ToString().PadLeft(10);
      //    List<TrackingPoint> trackingPoints = workflowDatas[i].TrackingPoints.OrderBy(x => x.AssetCode).ToList();

      //    foreach (TrackingPoint point in trackingPoints)
      //    {
      //      if(point.HeadReceived && point.TailReceived)
      //        returnValue += "=".PadLeft(1);
      //      else if(point.HeadReceived)
      //        returnValue += "-".PadLeft(1);
      //      else if (point.TailReceived)
      //        returnValue += "_".PadLeft(1);
      //      else
      //        returnValue += " ".PadLeft(1);
      //    }

      //    returnValue += "\n";
      //    returnValue += "\n";
      //    //TrackingHelper.LogWorkflowDataInformation($"Workflow print log of workflow number {i}", _workflowDatas[i]);
      //  }

      //  returnValue += "\n";
      //  NotificationController.Info(returnValue);
      //}
      //catch (Exception ex)
      //{
      //  NotificationController.LogException(ex);
      //}

      return Task.CompletedTask;
    }

    public virtual async Task SendMaterialPosition(long elapsedMillis)
    {
      Elapse(elapsedMillis);

      var materials = StorageProvider.Materials.Values.ToList();
      var areas = StorageProvider.TrackingAreas.Values.ToList();

      DCMaterialPosition dc = new DCMaterialPosition
      {
        IsLaneStopped = ParameterService.GetParameter("TRK_LineStatus")?.ValueInt.GetValueOrDefault() == 1,
        IsSlowProduction = ParameterService.GetParameter("DLS_Mode")?.ValueInt.GetValueOrDefault() == 1,
        Areas = new List<AreaMaterialPosition>()
      };

      for (int i = 0; i < areas.Count; i++)
      {
        var area = areas[i];
        if (area is Layer layer)
        {
          await PrepareLayerArea(area, layer, dc);
        }
        else if (area is TrackingCollectionAreaBase collectionArea)
          await PrepareCollectionArea(area, collectionArea, dc);
        else
          await PrepareCtrArea(area, materials, dc);
      }
      var listOfHashCodes = dc.Areas.Select(x => x.HashCode).ToList();
      bool positionChanged = listOfHashCodes.Except(HashCodes).Any();

      if (IsElapsedExceededSendPeriod() || positionChanged)
      {
        SendOfficeResult<DataContractBase> materialIdResponse = 
          await TrackingHmiSendOffice.SendL1MaterialPositionAsync(dc);

        if (positionChanged)
        {
          HashCodes = listOfHashCodes;
          ResetSendPeriod();
        }
        else
        {
          IncreaseSendPeriod();
        }
      }
    }

    protected virtual async Task PrepareLayerArea(TrackingProcessingAreaBase area, Layer layer, DCMaterialPosition dc)
    {
      await Task.CompletedTask;

      dc.Areas.Add(new AreaMaterialPosition
      {
        AreaId = area.AreaAssetCode,
        Signals = null,
        Layers = layer.GetLayers()
      });

      layer.SetHasChanged(false);
    }

    protected virtual async Task PrepareCollectionArea(TrackingProcessingAreaBase area, TrackingCollectionAreaBase collectionArea, DCMaterialPosition dc)
    {
      await Task.CompletedTask;

      dc.Areas.Add(new AreaMaterialPosition
      {
        AreaId = area.AreaAssetCode,
        Signals = null,
        Materials = collectionArea.GetMaterials()
      });
    }

    protected virtual async Task PrepareCtrArea(TrackingProcessingAreaBase area, List<CtrMaterialBase> materials, DCMaterialPosition dc)
    {
      await Task.CompletedTask;

      var currentAreaMaterials = materials
        .Where(x => x.StartDate.HasValue &&
                    x.MaterialInfo.MaterialId > 0 &&
                    x.TrackingPoints
                      .Any(p => p.HeadReceived && !p.TailReceived && p.StepAssetCode == area.AreaAssetCode))
        .Select(x => new { MaterialId = x.MaterialInfo.MaterialId, StartDate = x.StartDate.Value })
        .OrderBy(x => x.StartDate);

      int order = 0;

      dc.Areas.Add(new AreaMaterialPosition
      {
        AreaId = area.AreaAssetCode,
        Signals = area is CtrAreaBase ctrArea
        ? new AreaSignals
        {
          ModeProduction = ctrArea.AreaModeProduction.Value,
          ModeAdjustion = ctrArea.AreaModeAdjustion.Value,
          Simulation = ctrArea.AreaSimulation.Value,
          AutomaticRelease = ctrArea.AreaAutomaticRelease.Value,
          Empty = ctrArea.AreaEmpty.Value,
          CobbleDetected = ctrArea.AreaCobbleDetected.Value,
          ModeLocal = ctrArea.AreaModeLocal.Value,
          CobbleDetectionSelected = ctrArea.AreaCobbleDetectionSelected.Value
        }
        : null,
        Materials = currentAreaMaterials.Select(x => new MaterialPosition
        {
          PositionOrder = ++order,
          Order = 1,
          RawMaterialId = x.MaterialId
        }).ToList()
      });
    }

    public async Task<DataContractBase> ProcessScrapMessageAsync(DCL1ScrapData message)
    {
      NotificationController.Debug($"Received operation ProcessScrapMessageAsync with parameters: MaterialId: {message.Id}, TypeOfScrap: {message.TypeOfScrap.Name}");
      DataContractBase result = new DataContractBase();

      result = await TrackingMaterialProcessingProviderBase.ProcessScrapMessage(message);

      if ((short)message.TypeOfScrap == TypeOfScrap.Scrap)
      {
        var now = DateTime.Now.ExcludeMiliseconds();

        RemoveMaterialFromAllAreas(message.Id, now);

        HmiRefresh(HMIRefreshKeys.RawMaterialDetails);
        TaskHelper.FireAndForget(() =>
          TrackingHmiSendOffice.LastMaterialPositionRequestMessageAsync(new DataContractBase()).GetAwaiter()
            .GetResult());
      }

      return result;
    }

    public virtual Task<DataContractBase> RemoveMaterialFromArea(DCRemoveMaterial dc)
    {
      NotificationController.Debug($"Received operation RemoveMaterialFromArea with parameters: MaterialId: {dc.Id} AssetCode: {dc.AreaCode}");
      var result = Task.FromResult(new DataContractBase());
      var now = DateTime.Now.ExcludeMiliseconds();

      try
      {
        var trackingAreasWithExistingMaterial = StorageProvider.TrackingAreas.Values
          .Where(x => x is TrackingCollectionAreaBase && ((TrackingCollectionAreaBase)x).GetMaterialIds()
            .Contains(dc.Id))
          .ToList();

        NotificationController.Warn(
          $"Process removing materialId: {dc.Id} from Area: {dc.AreaCode} which exist " +
          $"in areas: {string.Join(",", trackingAreasWithExistingMaterial.Select(x => x.AreaAssetCode))}");

        if (!trackingAreasWithExistingMaterial.Any(x => x.AreaAssetCode == dc.AreaCode))
        {
          NotificationController.Warn($"MaterialId: {dc.Id} does not exist in Area: {dc.AreaCode}");
          return result;
        }

        if (dc.AreaCode == TrackingArea.GREY_AREA)
        {
          RemoveCtrMaterial(dc.Id);
          StorageProvider.CtrGreyArea.RemoveMaterialFromCollection(dc.Id, now);
        }
        else
        {
          (StorageProvider.TrackingAreas[dc.AreaCode] as TrackingCollectionAreaBase)?.RemoveMaterialFromCollection(dc.Id, now);
          NotificationController.Warn($"Material: {dc.Id} was successfully removed from area: {dc.AreaCode}");
        }

        return result;
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.MaterialNotRemovedFromArea,
          $"Unexpected error while removing raw material [{dc}] from area {dc.AreaCode}.", dc.AreaCode);
      }

      return result;
    }

    public virtual async Task<DataContractBase> ChargingGridUnCharge(DataContractBase dc)
    {
      NotificationController.Debug($"Received operation ChargingGridUnCharge with parameters: Initiator: {dc.HmiInitiator}");
      var result = new DataContractBase();
      var now = DateTime.Now.ExcludeMiliseconds();

      try
      {
        var unChargedElement = StorageProvider.ChargingGrid.UnChargeElement(now);

        await using var ctx = new PEContext();

        if (unChargedElement is not TrackingCollectionElementAbstractBase element)
          throw new InternalModuleException($"Undischarged material is not in TrackingCollectionElementAbstractBase type.",
            AlarmDefsBase.MaterialInChargingGridNotInCollectionType);

        foreach (var materialInfo in element.MaterialInfoCollection.MaterialInfos)
        {
          var rawMaterial = ctx.TRKRawMaterials
            .Include(x => x.FKMaterial)
            .Include(x => x.FKProduct)
            .First(x => x.RawMaterialId == materialInfo.MaterialId);

          var relations = ctx.TRKRawMaterialRelations
            .Include(x => x.ChildRawMaterial.FKMaterial)
            .Include(x => x.ParentRawMaterial.FKMaterial)
            .Where(x => x.ParentRawMaterialId == materialInfo.MaterialId || x.ChildRawMaterialId == materialInfo.MaterialId)
            .ToList();

          TrackingRawMaterialHandler.UnAssignL3Material(rawMaterial, relations);
        }

        await ctx.SaveChangesAsync();
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.MaterialNotUndischargeFromChargingGrid,
          $"Error while undischarging material form charging grid.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> ChargingGridCharge(DataContractBase dc)
    {
      NotificationController.Debug($"Received operation ChargingGridCharge with parameters: Initiator: {dc.HmiInitiator}");
      var result = new DataContractBase();
      var element = new TrackingCollectionElementBase();
      var now = DateTime.Now.ExcludeMiliseconds();

      try
      {
        await using PEContext ctx = new PEContext();
        var rawMaterial = await TrackingHandler.CreateRawMaterial(ctx,
          StorageProvider.AssetsDictionary[TrackingArea.CHG_AREA].AssetId,
          now, RawMaterialType.Material);

        element.MaterialInfoCollection.MaterialInfos.Add(new MaterialInfo(rawMaterial.RawMaterialId));

        StorageProvider.ChargingGrid.ChargeElement(element, now);
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.MaterialNotChargedToChargingGrid,
          $"Error while charging material to charging grid.");
      }

      return result;
    }

    public virtual Task<DataContractBase> FurnaceUnCharge(DataContractBase dc)
    {
      NotificationController.Debug($"Received operation FurnaceUnCharge with parameters: Initiator: {dc.HmiInitiator}");
      var result = Task.FromResult(new DataContractBase());
      var now = DateTime.Now.ExcludeMiliseconds();

      try
      {
        var unChargedElement = StorageProvider.Furnace.UnChargeElement(now);

        if (unChargedElement is TrackingCollectionElementAbstractBase element)
          StorageProvider.ChargingGrid.UnDischargeElement(unChargedElement, now);
        else
          throw new InternalModuleException($"Uncharged material is not in TrackingCollectionElementAbstractBase type.",
            AlarmDefsBase.MaterialInChargingGridNotInCollectionType);
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.MaterialNotUnchargedFromFurnace,
          $"Error while uncharging material from furnace.");
      }

      return result;
    }

    public virtual Task<DataContractBase> FurnaceCharge(DataContractBase dc)
    {
      NotificationController.Debug($"Received operation FurnaceCharge with parameters: Initiator: {dc.HmiInitiator}");
      var result = Task.FromResult(new DataContractBase());
      var now = DateTime.Now.ExcludeMiliseconds();

      try
      {
        var dischargedElement = StorageProvider.ChargingGrid.DischargeElement(now);

        StorageProvider.Furnace.ChargeElement(dischargedElement, now);

        StorageProvider.ChargingGrid.RemoveLastVirtualPosition();
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.MaterialNotChargedToFurnace,
          $"Error while charging element to furnace.");
      }

      return result;
    }

    public virtual Task<DataContractBase> FurnaceDischarge(DataContractBase dc)
    {
      NotificationController.Debug($"Received operation FurnaceDischarge with parameters: Initiator: {dc.HmiInitiator}");
      var result = Task.FromResult(new DataContractBase());
      var now = DateTime.Now.ExcludeMiliseconds();

      try
      {
        StorageProvider.Furnace.DischargeElement(now);
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.MaterialNotDischargedFromFurnace,
          $"Error while discharging material from furnace.");
      }

      return result;
    }

    public virtual Task<DataContractBase> FurnaceUnDischarge(DataContractBase dc)
    {
      NotificationController.Debug($"Received operation FurnaceUnDischarge with parameters: Initiator: {dc.HmiInitiator}");
      var result = Task.FromResult(new DataContractBase());
      var now = DateTime.Now.ExcludeMiliseconds();

      try
      {
        var element = new TrackingCollectionElementBase();

        if (!StorageProvider.Furnace.TryUnDischargeVirtualElement(now))
        {
          var material = StorageProvider.Materials.Values
          .Where(x => x.StartDate.HasValue && x.MaterialInfo.MaterialId > 0)
          .OrderByDescending(x => x.StartDate)
          .FirstOrDefault();

          if (material == null)
            throw new InternalModuleException($"No material in rolling found to undischarge.",
              AlarmDefsBase.CannotFindMaterialToUndischarge);

          element.MaterialInfoCollection.MaterialInfos.Add(material.MaterialInfo);

          StorageProvider.Furnace.UnDischargeElement(element, now);
        }
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.MaterialNotUndischargedFromFurnace,
          $"Error while undischarging material from furnace.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> FurnaceChargeOnExit(DCChargeMaterialOnFurnaceExit dc)
    {
      NotificationController.Debug($"Received operation FurnaceChargeOnExit with parameters: Initiator: {dc.HmiInitiator} WorkOrderId: {dc.Id}");
      var result = new DataContractBase();
      var element = new TrackingCollectionElementBase();
      var now = DateTime.Now.ExcludeMiliseconds();

      try
      {
        await using var ctx = new PEContext();

        var rawMaterial = await TrackingHandler.CreateRawMaterial(ctx, StorageProvider.AssetsDictionary[TrackingArea.FCE_AREA].AssetId, now, RawMaterialType.Material);

        var l3Material = await TrackingHandler.GetFirstUnAssignedL3MaterialByWorkOrderId(ctx, dc.Id);

        TrackingRawMaterialHandler.AssignL3Material(rawMaterial, l3Material);

        element.MaterialInfoCollection.MaterialInfos.Add(new MaterialInfo(rawMaterial.RawMaterialId));

        StorageProvider.Furnace.ChargeElementOnExit(element, now);

        await ctx.SaveChangesAsync();
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.MaterialNotChargedToFurnaceExit,
          $"Error while charging material [{dc.Id}] on furnace exit.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> HardRemoveMaterialFromTracking(DCHardRemoveMaterial dc)
    {
      NotificationController.Debug($"Received operation HardRemoveMaterialFromTracking with parameters: MaterialId: {dc.Id}");
      DataContractBase result = new DataContractBase();
      var now = DateTime.Now.ExcludeMiliseconds();
      try
      {
        RemoveMaterialFromAllAreas(dc.Id, now);

        using var ctx = new PEContext();

        var rawMaterial = ctx.TRKRawMaterials
          .Include(x => x.FKMaterial)
          .Include(x => x.FKProduct)
          .First(x => x.RawMaterialId == dc.Id);

        var relations = ctx.TRKRawMaterialRelations
          .Include(x => x.ChildRawMaterial.FKMaterial)
          .Include(x => x.ParentRawMaterial.FKMaterial)
          .Where(x => x.ParentRawMaterialId == dc.Id || x.ChildRawMaterialId == dc.Id)
          .ToList();

        TrackingRawMaterialHandler.UnAssignL3Material(rawMaterial, relations, true);
        rawMaterial.UnAssignProduct();

        var relationToRemove = relations.FirstOrDefault(x => x.ChildRawMaterialId == dc.Id);
        if (relationToRemove is not null)
          ctx.TRKRawMaterialRelations.Remove(relationToRemove);

        await ctx.SaveChangesAsync();
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.MaterialNotChargedToFurnaceExit,
          $"Error while hard removing material [{dc.Id}].");
      }

      return result;
    }

    /// <summary>
    ///   Manual operation to mark material as ready in case of rolling issue
    /// </summary>
    /// <param name="message"></param>
    public virtual async Task<DataContractBase> MarkAsReady(DCMaterialReady message)
    {
      NotificationController.Debug($"Received operation MarkRawMaterialAsReady with parameters: MaterialId: {message.Id}");
      DataContractBase result = new DataContractBase();
      string materialName = message.Id.ToString();
      try
      {
        var now = DateTime.Now.ExcludeMiliseconds();
        MarkCtrMaterialAsReady(message.Id, now);
        RemoveMaterialFromAllAreas(message.Id, now);

        await using var ctx = new PEContext();

        TRKRawMaterial rawMaterial = await TrackingHandler.FindRawMaterialByIdAsync(ctx, message.Id);

        if (rawMaterial is null)
          throw new InternalModuleException($"Raw material [{message.Id}] not found.", AlarmDefsBase.RecordNotFound, message.Id);

        PRMProductCatalogueType productType = await TrackingRawMaterialHandler.FindProductCatalogueTypeByIdAsync(ctx, message.ProductType);
        List<TRKRawMaterial> materialsToProcess = new List<TRKRawMaterial>();

        materialName = rawMaterial.RawMaterialName;
        rawMaterial.RollingEndTs = now;

        if (message.ChildsNo > rawMaterial.ChildsNo)
        {
          int childrenNumber = message.ChildsNo - rawMaterial.ChildsNo;
          materialsToProcess.AddRange(await TrackingHandler.CreateChildrenMaterials(ctx, childrenNumber, now, rawMaterial, true));
        }
        else
          materialsToProcess.Add(rawMaterial);

        foreach (var item in materialsToProcess)
        {
          switch (productType.ProductCatalogueTypeCode.ToUpper())
          {
            case var value when value.Equals(Constants.WireRod.ToUpper()):
              if (item.FKProductId.HasValue)
                throw new InternalModuleException($"Coil [{item.FKProductId}] exists for material {item.RawMaterialName} [{item.RawMaterialId}].",
                  AlarmDefsBase.ProductExistsForMaterial, item.RawMaterialName);
              else if (item.IsDummy)
                throw new InternalModuleException($"Cannot create coil because material {item.RawMaterialName} [{item.RawMaterialId}] is dummy.",
                  AlarmDefsBase.CannotCreateProductForDummyMaterial, item.RawMaterialName);
              else if (item.EnumTypeOfScrap == TypeOfScrap.Scrap)
                throw new InternalModuleException($"Cannot create coil because material {item.RawMaterialName} [{item.RawMaterialId}] is scrapped.",
                  AlarmDefsBase.CannotCreateProductForScrappedMaterial, item.RawMaterialName);
              else
                await ProcessWireRodReadyOperationAsync(ctx, item, rawMaterial, now, message.KeepInTracking, message.TargetAreaAssetCode); break;
            case var value when value.Equals(Constants.Bar.ToUpper()):
              await ProcessBarReadyOperationAsync(ctx, item, rawMaterial, now, message.KeepInTracking, message.TargetAreaAssetCode, message.ChildsNo); break;
            case var value when value.Equals(Constants.Garret.ToUpper()):
              if (item.FKProductId.HasValue)
                throw new InternalModuleException($"Bar in coil [{item.FKProductId}] exists for material {item.RawMaterialName} [{item.RawMaterialId}].",
                  AlarmDefsBase.ProductExistsForMaterial, item.RawMaterialName);
              else if (item.IsDummy)
                throw new InternalModuleException($"Cannot create bar in coil because material {item.RawMaterialName} [{item.RawMaterialId}] is dummy.",
                  AlarmDefsBase.CannotCreateProductForDummyMaterial, item.RawMaterialName);
              else if (item.EnumTypeOfScrap == TypeOfScrap.Scrap)
                throw new InternalModuleException($"Cannot create bar in coil because material {item.RawMaterialName} [{item.RawMaterialId}] is scrapped.",
                  AlarmDefsBase.CannotCreateProductForScrappedMaterial, item.RawMaterialName);
              else
                await ProcessGarretReadyOperationAsync(ctx, item, rawMaterial, now, message.KeepInTracking, message.TargetAreaAssetCode); break;
            default:
              break;
          }
        }

        double weightWithCoeff;
        double? coeff = null;
        double materialWeight = 0;
        TRKRawMaterial rolledMaterial = null;
        var rawMaterialWithParent = await (from rm in ctx.TRKRawMaterials
                                           join pmr in ctx.TRKRawMaterialRelations
                                             .Include(x => x.ParentRawMaterial)
                                            on rm.RawMaterialId equals pmr.ChildRawMaterialId
                                            into pmrs
                                           from pmr in pmrs.DefaultIfEmpty()
                                           where rm.RawMaterialId == message.Id
                                           select new { RawMaterial = rm, ParentRawMaterial = pmr }).SingleAsync();

        var originalRawMaterial = rawMaterialWithParent.RawMaterial;

        var parentRawMaterial = rawMaterialWithParent?.ParentRawMaterial?.ParentRawMaterial;

        if (parentRawMaterial is not null)
        {
          materialWeight = (parentRawMaterial.LastWeight == 0 || !parentRawMaterial.LastWeight.HasValue ?
          parentRawMaterial.FKMaterial?.MaterialWeight ?? 0
          : parentRawMaterial.LastWeight.Value) * StorageProvider.WeightLossFactor;
          coeff = await TrackingRawMaterialHandler.FindRawMaterialWearCoeffByIdAsync(ctx, parentRawMaterial.RawMaterialId);

          if (!parentRawMaterial.IsAfterDelayPoint)
            rolledMaterial = parentRawMaterial;

          parentRawMaterial.IsAfterDelayPoint = true;
          await ctx.TRKRawMaterialRelations
            .Include(x => x.ChildRawMaterial)
            .Where(x => x.ParentRawMaterialId == parentRawMaterial.RawMaterialId && x.ChildRawMaterialId != originalRawMaterial.RawMaterialId)
            .Select(x => x.ChildRawMaterial)
            .ForEachAsync(x => x.IsAfterDelayPoint = true);
        }
        else
        {
          materialWeight = (originalRawMaterial.LastWeight == 0 || !originalRawMaterial.LastWeight.HasValue ?
          originalRawMaterial.FKMaterial?.MaterialWeight ?? 0
          : originalRawMaterial.LastWeight.Value) * StorageProvider.WeightLossFactor;
          coeff = await TrackingRawMaterialHandler.FindRawMaterialWearCoeffByIdAsync(ctx, originalRawMaterial.RawMaterialId);

          if (!originalRawMaterial.IsAfterDelayPoint)
            rolledMaterial = originalRawMaterial;

          await ctx.TRKRawMaterialRelations
            .Include(x => x.ChildRawMaterial)
            .Where(x => x.ParentRawMaterialId == originalRawMaterial.RawMaterialId)
            .Select(x => x.ChildRawMaterial)
            .ForEachAsync(x => x.IsAfterDelayPoint = true);
        }

        originalRawMaterial.IsAfterDelayPoint = true;

        if (rolledMaterial is not null)
        {
          await EquipmentUsageAfterReadyOperationAsync(rolledMaterial, materialWeight);

          if (coeff is not null)
            weightWithCoeff = materialWeight * (double)coeff;
          else
            weightWithCoeff = materialWeight;
          await RollsUsageAfterReadyOperationAsync(rolledMaterial, materialWeight, weightWithCoeff);
        }

        await ctx.SaveChangesAsync();

        TaskHelper.FireAndForget(() =>
          TrackingProcessMaterialEventSendOffice
            .CheckShiftsWorkOrderStatusses(new DCShiftCalendarId() { ShiftCalendarId = rawMaterial.FKShiftCalendarId })
              .GetAwaiter().GetResult());

        TaskHelper.FireAndForget(() =>
        {
          var result = TrackingProcessMaterialEventSendOffice.AddMillEvent(new DCMillEvent()
          {
            EventType = ChildMillEventType.MaterialReady,
            AssetId = StorageProvider.AssetsDictionary[message.TargetAreaAssetCode].AssetId,
            DateStart = now,
            DateEnd = now,
            RawMaterialId = rawMaterial.RawMaterialId
          }).GetAwaiter().GetResult();

          if (!result.OperationSuccess)
            NotificationController.Warn($"Something went wrong while AddMillEvent for {message.TargetAreaAssetCode}, MaterialId: {rawMaterial.RawMaterialId}, EventType: {ChildMillEventType.MaterialReady}");
        });
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), message, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation for raw material [{message.Id}].");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), message, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation for raw material [{message.Id}].");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), message, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), message, AlarmDefsBase.UnexpectedError,
          $"Unexpected error for crew for raw material [{message.Id}].");
      }

      return result;
    }

    /// <summary>
    /// Send product weight to Maintenence to recalculate accumulated weight on equipment
    /// </summary>
    /// <param name="rawMaterial"></param>
    /// <param name="materialWeight"></param>
    /// <returns>True or false</returns>
    protected virtual async Task EquipmentUsageAfterReadyOperationAsync(TRKRawMaterial rawMaterial, double materialWeight)
    {
      try
      {
        if (rawMaterial.IsDummy)
          NotificationController.Warn($"Cannot accumulate equipment usage for dummy material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}]");
        else if (rawMaterial.EnumTypeOfScrap == TypeOfScrap.Scrap)
          NotificationController.Warn($"Cannot accumulate equipment usage for scrapped material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}]");
        else if (rawMaterial.CuttingSeqNo > 1)
          NotificationController.Warn($"Cannot accumulate equipment usage for material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}] because is has been already done for parent");
        else
        {
          var result = await TrackingProcessMaterialEventSendOffice.AccumulateEquipmentUsageAsync(new DCEquipmentAccu()
          {
            MaterialWeight = materialWeight
          });

          if (result.OperationSuccess)
            NotificationController.Debug($"Forwarding material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}] weight {materialWeight} to Maintenance - success");
          else
            throw new InternalModuleException($"Something went wrong while adding material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}] weight to active equipment.",
              AlarmDefsBase.FailedToSendAccumulatedEquipmentUsageForMaterial, rawMaterial.RawMaterialName, materialWeight);
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }
    }

    /// <summary>
    /// Send product weight to Rollshop to recalculate accumulated weight on rolls
    /// </summary>
    /// <param name="rawMaterial"></param>
    /// <param name="materialWeight"></param>
    /// <param name="materialWeightWithCoeff"></param>
    /// <returns>True or false</returns>
    protected virtual async Task RollsUsageAfterReadyOperationAsync(TRKRawMaterial rawMaterial, double materialWeight, double materialWeightWithCoeff)
    {
      try
      {
        if (rawMaterial.IsDummy)
          NotificationController.Warn($"Cannot accumulate rolls usage for dummy material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}]");
        else if (rawMaterial.EnumTypeOfScrap == TypeOfScrap.Scrap)
          NotificationController.Warn($"Cannot accumulate rolls usage for scrapped material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}]");
        else if (rawMaterial.CuttingSeqNo > 1)
          NotificationController.Warn($"Cannot accumulate rolls usage for material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}] because is has been already done for parent");
        else
        {
          var result = await TrackingProcessMaterialEventSendOffice.AccumulateRollsUsageAsync(new DCRollsAccu()
          {
            MaterialWeight = materialWeight,
            MaterialWeightWithCoeff = materialWeightWithCoeff
          });

          if (result.OperationSuccess)
            NotificationController.Debug($"Forwarding material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}] weight {materialWeight} and weight with coefficient {materialWeightWithCoeff} to RollShop - success");
          else
            throw new InternalModuleException($"Something went wrong while adding material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}] weight and weight with coefficient to rolls.",
              AlarmDefsBase.FailedToSendAccumulatedRollsUsageForMaterial, rawMaterial.RawMaterialName, materialWeight, materialWeightWithCoeff);
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }
    }

    public virtual async Task<DataContractBase> ProcessOCRMessageAsync(DataContractBase dc)
    {
      DataContractBase result = new DataContractBase();
      try
      {
        await Task.CompletedTask;
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.UnexpectedExceptionDuringOCRMessageProcessing,
          $"Unexpected error while processing OCR message.");
      }

      return result;
    }

    /// <summary>
    ///   Transfer material to correct area after ready operation
    /// </summary>
    /// <param name="targetArea"></param>
    /// <param name="rawMaterial"></param>
    /// <param name="parentRawMaterial"></param>
    /// <param name="operationDate"></param>
    protected virtual async Task TransferMaterialAfterReadyOperationAsync(TrackingCollectionAreaBase targetArea, TRKRawMaterial rawMaterial, TRKRawMaterial parentRawMaterial, DateTime operationDate)
    {
      var element = new TrackingCollectionElementBase();
      var materialInfo = new MaterialInfo(rawMaterial.RawMaterialId);
      materialInfo.ChangeParentMaterialId(parentRawMaterial.RawMaterialId);
      element.MaterialInfoCollection.MaterialInfos.Add(materialInfo);
      targetArea.ChargeElement(element, operationDate);

      await Task.CompletedTask;
    }

    /// <summary>
    ///   Process material ready for coils
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="rawMaterial"></param>
    /// <param name="parentRawMaterial"></param>
    /// <param name="operationDate"></param>
    /// <param name="keepInTracking"></param>
    /// <param name="targetAreaAssetCode"></param>
    protected virtual async Task ProcessWireRodReadyOperationAsync(PEContext ctx, TRKRawMaterial rawMaterial, TRKRawMaterial parentRawMaterial, DateTime operationDate, bool keepInTracking, int targetAreaAssetCode)
    {
      var shouldBeFinished = false;
      var materialWeight = (rawMaterial.LastWeight == 0 || !rawMaterial.LastWeight.HasValue
        ? rawMaterial.FKMaterial?.MaterialWeight ?? 0
        : rawMaterial.LastWeight.Value) * WeightLossFactor;

      NotificationController.Debug($"Requesting product creation for raw material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}].");
      var productCreationResult = await TrackingProcessMaterialEventSendOffice.SendRequestToCreateCoilAsync(new DCCoilData()
      {
        RawMaterialId = rawMaterial.RawMaterialId,
        FKMaterialId = rawMaterial.FKMaterialId,
        OverallWeight = materialWeight,
        Date = operationDate
      });

      if (!productCreationResult.OperationSuccess)
        throw new InternalModuleException($"Something went wrong while creating coil for material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}].",
          AlarmDefsBase.ProductCreatonForMaterialFailed, rawMaterial.RawMaterialName);

      try
      {
        if (keepInTracking)
          await TransferMaterialAfterReadyOperationAsync(StorageProvider.TrackingAreas[targetAreaAssetCode] as TrackingCollectionAreaBase, rawMaterial, parentRawMaterial, operationDate);
        else
          rawMaterial.EnumRawMaterialStatus = shouldBeFinished ? RawMaterialStatus.Finished : RawMaterialStatus.Rolled;

        //assign product to raw material
        rawMaterial.FKProductId = productCreationResult.DataConctract.ProductId;
        rawMaterial.ProductCreatedTs = operationDate;
        await ctx.SaveChangesAsync();

        TaskHelper.FireAndForget(() =>
          TrackingProcessMaterialEventSendOffice.SendProductReportAsync(
            new DCRawMaterial(rawMaterial.RawMaterialId)),
            $"Something went wrong while sending coil report for material {rawMaterial.RawMaterialId}");

        TaskHelper.FireAndForget(() =>
        {
          var result = TrackingProcessMaterialEventSendOffice.AddMillEvent(new DCMillEvent()
          {
            EventType = ChildMillEventType.ProductCreate,
            AssetId = StorageProvider.AssetsDictionary[targetAreaAssetCode].AssetId,
            DateStart = operationDate,
            DateEnd = operationDate,
            RawMaterialId = rawMaterial.RawMaterialId
          }).GetAwaiter().GetResult();

          if (!result.OperationSuccess)
            NotificationController.Warn($"Something went wrong while AddMillEvent for {targetAreaAssetCode}, MaterialId: {rawMaterial.RawMaterialId}, EventType: {ChildMillEventType.ProductCreate}");
        });
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, $"Something went wrong while creating coil for material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}].");
        throw new InternalModuleException($"Something went wrong while creating coil for material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}].", AlarmDefsBase.ProductCreatonForMaterialFailed, rawMaterial.RawMaterialName);
      }
    }

    /// <summary>
    ///   Process material ready for bars
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="rawMaterial"></param>
    /// <param name="parentRawMaterial"></param>
    /// <param name="operationDate"></param>
    /// <param name="keepInTracking"></param>
    /// <param name="targetAreaAssetCode"></param>
    protected virtual async Task ProcessBarReadyOperationAsync(PEContext ctx, TRKRawMaterial rawMaterial, TRKRawMaterial parentRawMaterial, DateTime operationDate, bool keepInTracking, int targetAreaAssetCode, short childsNo)
    {
      if (keepInTracking)
        await TransferMaterialAfterReadyOperationAsync(StorageProvider.TrackingAreas[targetAreaAssetCode] as TrackingCollectionAreaBase, rawMaterial, parentRawMaterial, operationDate);
      else if (childsNo == 0)
        rawMaterial.EnumRawMaterialStatus = RawMaterialStatus.Finished;

      await ctx.SaveChangesAsync();
    }

    /// <summary>
    ///   Process material ready for bar in coils
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="rawMaterial"></param>
    /// <param name="parentRawMaterial"></param>
    /// <param name="operationDate"></param>
    /// <param name="keepInTracking"></param>
    /// <param name="targetAreaAssetCode"></param>
    protected virtual async Task ProcessGarretReadyOperationAsync(PEContext ctx, TRKRawMaterial rawMaterial, TRKRawMaterial parentRawMaterial, DateTime operationDate, bool keepInTracking, int targetAreaAssetCode)
    {
      if (keepInTracking)
        await TransferMaterialAfterReadyOperationAsync(StorageProvider.TrackingAreas[targetAreaAssetCode] as TrackingCollectionAreaBase, rawMaterial, parentRawMaterial, operationDate);
      else
      {
        NotificationController.Debug($"Requesting product creation for raw material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}].");
        SendOfficeResult<DCProductData> productCreationResult =
          await TrackingProcessMaterialEventSendOffice.SendRequestToCreateCoilAsync(new DCCoilData()
          {
            RawMaterialId = rawMaterial.RawMaterialId,
            FKMaterialId = rawMaterial.FKMaterialId,
            OverallWeight = (rawMaterial.LastWeight == 0 || !rawMaterial.LastWeight.HasValue
              ? rawMaterial.FKMaterial?.MaterialWeight ??
                0
              : rawMaterial.LastWeight.Value) * WeightLossFactor,
            Date = operationDate
          });

        if (!productCreationResult.OperationSuccess)
          throw new InternalModuleException($"Something went wrong while creating bar in coil for material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}].", AlarmDefsBase.ProductCreatonForMaterialFailed, rawMaterial.RawMaterialName);

        try
        {
          rawMaterial.EnumRawMaterialStatus = RawMaterialStatus.Finished;

          //assign product to raw material
          rawMaterial.FKProductId = productCreationResult.DataConctract.ProductId;
          rawMaterial.ProductCreatedTs = operationDate;
          await ctx.SaveChangesAsync();

          TaskHelper.FireAndForget(() =>
            TrackingProcessMaterialEventSendOffice.SendProductReportAsync(
              new DCRawMaterial(rawMaterial.RawMaterialId)),
              $"Something went wrong while sending bar in coil report for material {rawMaterial.RawMaterialId}");

          TaskHelper.FireAndForget(() =>
          {
            var result = TrackingProcessMaterialEventSendOffice.AddMillEvent(new DCMillEvent()
            {
              EventType = ChildMillEventType.ProductCreate,
              AssetId = StorageProvider.AssetsDictionary[targetAreaAssetCode].AssetId,
              DateStart = operationDate,
              DateEnd = operationDate,
              RawMaterialId = rawMaterial.RawMaterialId
            }).GetAwaiter().GetResult();

            if (!result.OperationSuccess)
              NotificationController.Warn($"Something went wrong while AddMillEvent for {targetAreaAssetCode}, MaterialId: {rawMaterial.RawMaterialId}, EventType: {ChildMillEventType.ProductCreate}");
          });
        }
        catch (Exception ex)
        {
          NotificationController.LogException(ex, $"Something went wrong while creating bar in coil for material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}].");
          throw new InternalModuleException($"Something went wrong while creating bar in coil for material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}].", AlarmDefsBase.ProductCreatonForMaterialFailed, rawMaterial.RawMaterialName);
        }
      }

    }

    public virtual async Task<DataContractBase> ProductUndo(DCProductUndo dc)
    {
      NotificationController.Debug($"Received operation ProductUndo with parameters: MaterialId: {dc.Id}");
      DataContractBase result = new DataContractBase();
      string materialName = dc.Id.ToString();

      try
      {
        using var ctx = new PEContext();

        var rawMaterial = await ctx.TRKRawMaterials.Include(m => m.FKProduct).FirstAsync(m => m.RawMaterialId == dc.Id);
        long? workOrderId = rawMaterial.FKProduct.FKWorkOrderId;

        materialName = rawMaterial.RawMaterialName;

        if (rawMaterial.FKProductId.HasValue)
        {
          rawMaterial.EnumRawMaterialStatus = RawMaterialStatus.InFinalProduction;

          rawMaterial.UnAssignProduct();

          await ctx.SaveChangesAsync();

          TaskHelper.FireAndForget(() =>
            TrackingProcessMaterialEventSendOffice.CheckShiftsWorkOrderStatusses(
              new DCShiftCalendarId() { ShiftCalendarId = rawMaterial.FKShiftCalendarId }));

          if (workOrderId.HasValue)
          {
            TaskHelper.FireAndForget(async () => await TrackingProcessMaterialEventSendOffice
              .CalculateWorkOrderKPIsAsync(new() { WorkOrderId = workOrderId.Value }), "CalculateWorkOrderKPIsAsync has failed.");
          }

          //NotificationController.RegisterAlarm(ModuleAlarmDefs.AlarmCode_MaterialManualOperationByUser, "",
          //  materialName,
          //  message.HmiInitiator == null
          //    ? "UNKNOWN USER"
          //    : $"{message.HmiInitiator.UserId}({message.HmiInitiator.IpAddress})",
          //  "MarkAsReady",
          //  "Success");
        }
        else
          throw new InternalModuleException($"Cannot undo product for material [{dc.Id}].",
            AlarmDefsBase.ProductCannotBeUndone);

        TaskHelper.FireAndForget(() =>
          TrackingProcessMaterialEventSendOffice
            .CheckShiftsWorkOrderStatusses(new DCShiftCalendarId() { ShiftCalendarId = rawMaterial.FKShiftCalendarId })
            .GetAwaiter().GetResult());
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ProductUndoFailed,
          $"Something went wrong during product undo operation for material [{dc.Id}].");
      }

      return result;
    }

    public virtual async Task<DataContractBase> UnassignMaterial(DCMaterialUnassign dc)
    {
      NotificationController.Debug($"Received operation UnassignMaterial with parameters:: Initiator: {dc.HmiInitiator} MaterialId: {dc.Id}");
      var result = new DataContractBase();
      try
      {
        using var ctx = new PEContext();

        var rawMaterial = ctx.TRKRawMaterials
          .Include(x => x.FKMaterial)
          .Include(x => x.FKProduct)
          .First(x => x.RawMaterialId == dc.Id);

        var relations = ctx.TRKRawMaterialRelations
          .Include(x => x.ChildRawMaterial.FKMaterial)
          .Include(x => x.ParentRawMaterial.FKMaterial)
          .Where(x => x.ParentRawMaterialId == dc.Id || x.ChildRawMaterialId == dc.Id)
          .ToList();

        TrackingRawMaterialHandler.UnAssignL3Material(rawMaterial, relations);

        await ctx.SaveChangesAsync();
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.MaterialNotUnassigned,
          $"Something went wrong material [{dc.Id}] unassign operation.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> AssignMaterial(DCMaterialAssign dc)
    {
      NotificationController.Debug($"Received operation AssignMaterial with parameters: Initiator: {dc.HmiInitiator} MaterialId: {dc.RawMaterialId} L3MaterialId: {dc.L3MaterialId}");
      var result = new DataContractBase();
      var now = DateTime.Now.ExcludeMiliseconds();
      try
      {
        await using var ctx = new PEContext();

        var rawMaterial = ctx.TRKRawMaterials.Include(m => m.FKProduct).First(m => m.RawMaterialId == dc.RawMaterialId);
        var l3Material = await TrackingHandler.FindL3MaterialByIdAsync(ctx, dc.L3MaterialId);

        TrackingRawMaterialHandler.AssignL3Material(rawMaterial, l3Material);

        await ctx.SaveChangesAsync();
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.MaterialNotAssigned,
          $"Something went wrong material [{dc.RawMaterialId}] assign operation.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> CollectionMoveForward(DCUpdateArea dc)
    {
      var now = DateTime.Now.ExcludeMiliseconds();
      NotificationController.Debug($"Received operation CollectionMoveForward with parameters: AreaCode: {dc.Id}");
      DataContractBase result = new DataContractBase();
      try
      {
        if (StorageProvider.TrackingAreas[dc.Id] is not ICollectionMoveable area)
          throw new InternalModuleException($"Area with code {dc.Id} is not in type ICollectionMoveable",
                        AlarmDefsBase.AreaNotMoveable, dc.Id);

        area.MoveForward(now);

        if (dc.Id == TrackingArea.RAKE_AREA)
        {
          var virtualMaterialsInRake = StorageProvider.Rake.GetMaterials().Where(x => x.IsVirtual).ToList();
          var lastVirtualPosition = virtualMaterialsInRake.OrderByDescending(x => x.PositionOrder).First().PositionOrder;

          var materials = virtualMaterialsInRake.Where(x => x.PositionOrder == lastVirtualPosition).ToList();

          await using var ctx = new PEContext();

          if (materials.Any())
          {
            if (StorageProvider.TrackingAreas[TrackingArea.LAYER_AREA] is not Layer layer)
              throw new InvalidOperationException($"AssetCode {TrackingArea.LAYER_AREA} is not Layer!");

            var checkLayers = layer.GetLayers();
            var availableLayer = layer.GetLayers().FirstOrDefault(x => x.IsForming);
            if (availableLayer is null)
              availableLayer = layer.GetLayers().OrderBy(x => x.PositionOrder).First(x => !x.IsFormed);

            var materialList = await TrackingHandler.GetRawMaterialsWithLayerByIds(ctx, materials.Select(x => x.RawMaterialId).ToList(), availableLayer.Id);
            var layerMaterial = materialList.First(x => x.EnumRawMaterialType == RawMaterialType.Layer);

            foreach (var item in materialList)
            {
              if (item.EnumRawMaterialType == RawMaterialType.Layer)
                continue;
              var rawMaterial = await TrackingHandler.GetRawMaterialById(ctx, item.RawMaterialId);
              rawMaterial.EnumRawMaterialStatus = RawMaterialStatus.Rolled;

              var layerRelation = new TRKLayerRawMaterialRelation()
              {
                ParentLayerRawMaterialId = layerMaterial.RawMaterialId,
                ChildLayerRawMaterialId = item.RawMaterialId,
                IsActualRelation = true
              };

              ctx.TRKLayerRawMaterialRelations.Add(layerRelation);
            }

            if (layerMaterial.EnumLayerStatus != LayerStatus.IsForming)
              layerMaterial.EnumLayerStatus = LayerStatus.IsForming;

            TaskHelper.FireAndForget(() =>
                TrackingProcessMaterialEventSendOffice.CheckShiftsWorkOrderStatusses(
                  new DCShiftCalendarId() { ShiftCalendarId = materialList.First(x => x.EnumRawMaterialType != RawMaterialType.Layer).FKShiftCalendarId }));

            await ctx.SaveChangesAsync();
          }
        }

      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.AreaMoveForwardFailed,
          $"Something went wrong while moving material forward in area with code {dc.Id}.", dc.Id);
      }

      return result;
    }
    public virtual Task<DataContractBase> CollectionMoveBackward(DCUpdateArea dc)
    {
      NotificationController.Debug($"Received operation CollectionMoveBackward with parameters: AreaCode: {dc.Id}");
      DataContractBase result = new DataContractBase();
      try
      {
        if (StorageProvider.TrackingAreas[dc.Id] is ICollectionMoveable area)
        {
          area.MoveBackward();
        }
        else
          throw new InvalidOperationException($"Operation CollectionMoveBackward is not allowed because collection with code:" +
            $" AreaCode: {dc.Id} is not of type {nameof(ICollectionMoveable)}");

      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.AreaMoveBackwardFailed,
          $"Something went wrong while moving material backward in area with code {dc.Id}.", dc.Id);
      }

      return Task.FromResult(result);
    }

    public virtual async Task<DataContractBase> ReplaceMaterialPosition(DCMoveMaterial dc)
    {
      NotificationController.Debug($"Received operation ReplaceMaterialPosition with parameters: " +
        $"DropAssetCode: {dc.DropAssetCode}, DropOrderSeq: {dc.DropOrderSeq}, RawMaterialId: {dc.RawMaterialId}," +
        $"DragAssetCode: {dc.DragAssetCode}, DragOrderSeq: {dc.DragOrderSeq}, DragFromVirtualPosition: {dc.DragFromVirtualPosition}");
      DataContractBase result = new DataContractBase();
      try
      {
        if (StorageProvider.TrackingAreas[dc.DropAssetCode] is TrackingCollectionAreaBase dropArea &&
          StorageProvider.TrackingAreas[dc.DragAssetCode] is TrackingCollectionAreaBase dragArea)
        {
          if (StorageProvider.TrackingAreas[dc.DropAssetCode] is Layer layer)
          {
            var material = layer.GetMaterialByPosition(dc.DropOrderSeq);

            if (material is null)
              throw new InternalModuleException($"There is no layer at position: {dc.DropOrderSeq} for {dc.DropAssetCode}",
                AlarmDefsBase.CannotFindLayerInPosition, dc.DropOrderSeq, dc.DropAssetCode);

            await using var ctx = new PEContext();

            var rawMaterial = await TrackingHandler.GetRawMaterialById(ctx, dc.RawMaterialId);
            var layerMaterial = await TrackingHandler.GetRawMaterialById(ctx, material.MaterialId);

            if (layerMaterial.EnumLayerStatus > LayerStatus.IsForming)
              throw new InternalModuleException($"Cannot paste material {dc.RawMaterialId} to layer: {material.MaterialId} - its status is {layerMaterial.EnumLayerStatus.Name}",
                AlarmDefsBase.LayerStatusPreventsPaste, layerMaterial.EnumLayerStatus.Name);

            if (layerMaterial.EnumLayerStatus != LayerStatus.IsForming)
              layerMaterial.EnumLayerStatus = LayerStatus.IsForming;
            rawMaterial.EnumRawMaterialStatus = RawMaterialStatus.Rolled;

            var layerRelation = new TRKLayerRawMaterialRelation()
            {
              ParentLayerRawMaterialId = layerMaterial.RawMaterialId,
              ChildLayerRawMaterialId = rawMaterial.RawMaterialId,
              IsActualRelation = true
            };

            ctx.TRKLayerRawMaterialRelations.Add(layerRelation);

            await ctx.SaveChangesAsync();

            layer.AssignMaterialsSumByRawMaterialId(layerMaterial.RawMaterialId,
             (short)ctx.TRKLayerRawMaterialRelations.Count(x => x.ParentLayerRawMaterialId == layerMaterial.RawMaterialId));
          }
          else
            dropArea.Drop(dc.RawMaterialId, dc.DropOrderSeq, dc.DropAssetCode == dc.DragAssetCode ? dc.DragOrderSeq : (int?)null);

          dragArea.Drag(dc.RawMaterialId, dc.DragOrderSeq, dc.DropAssetCode == dc.DragAssetCode && dc.DragOrderSeq > dc.DropOrderSeq, dc.DragFromVirtualPosition);
        }
        else
          throw new InternalModuleException($"Operation ReplaceMaterialPosition is not allowed because condition for collection with code:" +
            $" AreaCode: {dc.DropOrderSeq} is {StorageProvider.TrackingAreas[dc.DropAssetCode] is TrackingCollectionAreaBase}" +
            $"or condition for collection with code: AreaCode: {dc.DragAssetCode} is {StorageProvider.TrackingAreas[dc.DragAssetCode] is TrackingCollectionAreaBase}",
            AlarmDefsBase.CannotReplaceMaterialPositionForArea, dc.DragAssetCode);
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ReplaceMaterialPositionFailed,
          $"Cannot move material [{dc.RawMaterialId}] from position {dc.DragOrderSeq} in area {dc.DropAssetCode} to position {dc.DropOrderSeq} in area {dc.DropAssetCode}.",
          AlarmDefsBase.ReplaceMaterialPositionFailed, dc.DragOrderSeq, dc.DragAssetCode, dc.DropOrderSeq, dc.DropAssetCode);
      }

      return result;
    }

    public virtual async Task<DataContractBase> CreateBundleAsync(DCBundleData dc)
    {
      DataContractBase result = new();

      try
      {
        await using var ctx = new PEContext();

        var workOrder = await ctx.PRMWorkOrders.FirstAsync(x => x.WorkOrderId == dc.WorkOrderId);

        if (workOrder.EnumWorkOrderStatus >= WorkOrderStatus.Finished)
          throw new InternalModuleException($"Cannot create bundle for work order {workOrder.WorkOrderName} [{dc.WorkOrderId}] because it is in status {workOrder.EnumWorkOrderStatus}.",
            AlarmDefsBase.WorkOrderStatusPreventsBundleCreation, workOrder.WorkOrderName);

        var now = DateTime.Now.ExcludeMiliseconds();
        dc.Date = now;

        var productCreationResult = await TrackingProcessMaterialEventSendOffice.SendRequestToCreateBundleAsync(dc);

        if (!productCreationResult.OperationSuccess)
          throw new InternalModuleException($"Something went wrong while creating bundle for work order {workOrder.WorkOrderName} [{dc.WorkOrderId}].",
            AlarmDefsBase.SomethingWentWrongWhileCreatingBundleForWorkOrder, workOrder.WorkOrderName);

        var product = await ctx.PRMProducts.FirstAsync(x => x.ProductId == productCreationResult.DataConctract.ProductId);

        var rawMaterial = await TrackingHandler.CreateRawMaterial(ctx,
          StorageProvider.AssetsDictionary[TrackingArea.BAR_WEIGHING_AREA].AssetId,
          now, RawMaterialType.Bundle, false, false);

        rawMaterial.OldRawMaterialName = rawMaterial.RawMaterialName;
        rawMaterial.RawMaterialName = product.ProductName;
        rawMaterial.LastWeight = product.ProductWeight;
        rawMaterial.WeighingStationWeight = product.ProductWeight;
        rawMaterial.FKProductId = product.ProductId;
        rawMaterial.ProductCreatedTs = product.ProductCreatedTs;
        rawMaterial.IsAfterDelayPoint = true;

        await ctx.SaveChangesAsync();

        if (dc.KeepInTracking)
        {
          if (StorageProvider.TrackingAreas[TrackingArea.BAR_WEIGHING_AREA] is not TrackingNonPositionRelatedListBase weighing)
            throw new InternalModuleException($"Tracking area {TrackingArea.BAR_WEIGHING_AREA} type must be {nameof(TrackingNonPositionRelatedListBase)}.",
              AlarmDefsBase.UnexpectedError);

          weighing.ChargeElementWithPreventOverflow(new TrackingCollectionElementBase(new MaterialInfo(rawMaterial.RawMaterialId)), now);
        }
        else
        {
          TaskHelper.FireAndForget(() =>
            TrackingProcessMaterialEventSendOffice.SendProductReportAsync(
              new DCRawMaterial(rawMaterial.RawMaterialId)),
              $"Something went wrong while sending bundle report for material {rawMaterial.RawMaterialId}.");
        }
        HmiRefresh(HMIRefreshKeys.BundleWeighingMonitor);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while creating bundle for work order {dc.WorkOrderId}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while creating bundle for work order {dc.WorkOrderId}.");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while creating bundle for work order {dc.WorkOrderId}.");
      }

      return result;
    }

    protected virtual void Elapse(long elapsedMillis)
    {
      ElapsedSinceLastSend += elapsedMillis;
    }

    protected virtual bool IsElapsedExceededSendPeriod()
    {
      if (ElapsedSinceLastSend > CurrentSendPeriod * 1000)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    protected virtual void ResetSendPeriod()
    {
      ElapsedSinceLastSend = 0;
      CurrentSendPeriod = 1;
    }

    protected virtual void IncreaseSendPeriod()
    {
      ElapsedSinceLastSend = 0;
      CurrentSendPeriod = Math.Min(CurrentSendPeriod + 2, MaxMaterialPositionSendPeriod);
      NotificationController.Debug($"SendL1MaterialPositionToHMIAsync increase send period to {CurrentSendPeriod}");
    }

    protected virtual void RemoveCtrMaterial(long materialId)
    {
      CtrMaterialBase material = GetMaterial(materialId);

      if (material != null)
      {
        material.RemoveMaterial();
        NotificationController.Warn($"Ctr material with Id: {materialId} was successfully removed");
      }
      else
        NotificationController.Warn($"RemoveCtrMaterial - material with Id: {materialId} not found");
    }

    protected virtual void RemoveMaterialFromAllAreas(long materialId, DateTime operationDate, params TrackingArea[] trackingAreasToExclude)
    {
      RemoveCtrMaterial(materialId);

      var trackingAreasWithExistingMaterial = StorageProvider.TrackingAreas.Values
        .Where(x => x is TrackingCollectionAreaBase && ((TrackingCollectionAreaBase)x).GetMaterialIds()
          .Contains(materialId))
        .ToList();

      foreach (var trackingArea in trackingAreasWithExistingMaterial)
      {
        try
        {
          if (trackingAreasToExclude.Contains((TrackingArea)trackingArea.AreaAssetCode))
          {
            NotificationController.Warn($"Material: {materialId} wasn't successfully removed from area: {trackingArea.AreaAssetCode} due to its exclude");
            continue;
          }

          (trackingArea as TrackingCollectionAreaBase)?.RemoveMaterialFromCollection(materialId, operationDate);
          NotificationController.Warn($"Material: {materialId} was successfully removed from area: {trackingArea.AreaAssetCode}");
        }
        catch (InternalModuleException ex)
        {
          ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), ex.AlarmCode,
            ex.Message, ex.AlarmParams);
        }
        catch (Exception ex)
        {
          ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.RemoveMaterialFromAllAreasFailed,
            $"Something went wrong while removing material [{materialId}] form all areas.");
        }
      }
    }

    protected virtual void MarkCtrMaterialAsReady(long materialId, DateTime operationDate)
    {
      CtrMaterialBase material = GetMaterial(materialId);
      if (material != null)
      {
        material.MarkAllPointsAsHeadReceived(operationDate);
        material.MarkAllPointsAsTailReceived(operationDate);
      }
    }

    protected virtual CtrMaterialBase GetMaterial(long materialId)
    {
      CtrMaterialBase material =
        StorageProvider.Materials.Values.FirstOrDefault(m => m.MaterialInfo.MaterialId == materialId);

      return material;
    }
  }
}
