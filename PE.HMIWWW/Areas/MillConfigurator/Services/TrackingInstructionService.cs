using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.DbEntity.HmiModels;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Areas.MillConfigurator.ViewModels.TrackingInstruction;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.EnumClasses;
using static PE.HMIWWW.Core.Helpers.SelectListHelpers;

namespace PE.HMIWWW.Areas.MillConfigurator.Services
{
  public class TrackingInstructionService
  {
    public TrackingInstructionService() { }

    public async Task<VM_TrackingInstruction> GetTrackingInstructionByIdAsync(PEContext peCtx, long trackingInstructionId)
    {
      var data = await peCtx.TRKTrackingInstructions
        .IgnoreQueryFilters()
        .Include(x => x.FKParentTrackingInstruction)
        .Include(x => x.FKParentTrackingInstruction.FKFeature)
        .Include(x => x.FKParentTrackingInstruction.FKAreaAsset)
        .Include(x => x.FKAreaAsset)
        .Include(x => x.FKPointAsset)
        .Include(x => x.FKFeature)
        .FirstAsync(x => x.TrackingInstructionId == trackingInstructionId);

      if (data.FKParentTrackingInstruction is not null)
        return new VM_TrackingInstruction(data, data.FKParentTrackingInstruction);

      return new VM_TrackingInstruction(data);
    }

    public DataSourceResult GetTrackingInstructionSearchList(HmiContext ctx, DataSourceRequest request)
    {
      return ctx.V_TrackingInstructions
        .Where(x => x.FeatureCode > 0)
        .ToListAsync().GetAwaiter().GetResult()
        .ToDataSourceLocalResult(request, data => new VM_TrackingInstruction(data));
    }

    public DataSourceResult GetRelatedTrackingInstructionsSearchList(PEContext peCtx, DataSourceRequest request, long featureId)
    {
      return peCtx.TRKTrackingInstructions
        .IgnoreQueryFilters()
        .Include(x => x.FKAreaAsset)
        .Include(x => x.FKPointAsset)
        .Include(x => x.FKFeature)
        .Where(x => x.FKFeatureId == featureId)
        .OrderBy(x => x.TrackingInstructionValue).ThenBy(x => x.SeqNo)
        .ToListAsync().GetAwaiter().GetResult()
        .ToDataSourceLocalResult(request, data => new VM_TrackingInstruction(data));
    }

    public async Task<VM_Base> CreateTrackingInstructionAsync(ModelStateDictionary modelState, PEContext ctx, VM_TrackingInstruction data)
    {
      VM_Base result = new VM_Base();

      await CheckTrackingInstructionUniqueAsync(ctx, modelState, data);

      if (!modelState.IsValid)
      {
        return result;
      }

      await ctx.TRKTrackingInstructions.AddAsync(new TRKTrackingInstruction
      {
        FKFeatureId = data.FKFeatureId,
        FKAreaAssetId = data.FKAreaAssetId,
        FKPointAssetId = data.FKPointAssetId,
        FKParentTrackingInstructionId = data.FKParentTrackingInstructionId,
        SeqNo = data.SeqNo,
        TrackingInstructionValue = data.TrackingInstructionValue,
        EnumTrackingInstructionType = TrackingInstructionType.GetValue(data.EnumTrackingInstructionType),
        ChannelId = data.ChannelId,
        TimeFilter = data.TimeFilter,
        IsAsync = data.IsAsync,
        IsIgnoredIfSimulation = data.IsIgnoredIfSimulation
      });

      await ctx.SaveChangesAsync();

      //return view model
      return result;
    }

    public async Task<VM_Base> EditTrackingInstructionAsync(ModelStateDictionary modelState, PEContext ctx, VM_TrackingInstruction data)
    {
      VM_Base result = new VM_Base();

      await CheckTrackingInstructionUniqueAsync(ctx, modelState, data, data.TrackingInstructionId);

      if (!modelState.IsValid)
      {
        return result;
      }

      var trackingInstruction = await ctx.TRKTrackingInstructions.FirstAsync(x => x.TrackingInstructionId == data.TrackingInstructionId);

      trackingInstruction.FKFeatureId = data.FKFeatureId;
      trackingInstruction.FKAreaAssetId = data.FKAreaAssetId;
      trackingInstruction.FKPointAssetId = data.FKPointAssetId;
      trackingInstruction.FKParentTrackingInstructionId = data.FKParentTrackingInstructionId;
      trackingInstruction.SeqNo = data.SeqNo;
      trackingInstruction.TrackingInstructionValue = data.TrackingInstructionValue;
      trackingInstruction.EnumTrackingInstructionType = TrackingInstructionType.GetValue(data.EnumTrackingInstructionType);
      trackingInstruction.ChannelId = data.ChannelId;
      trackingInstruction.TimeFilter = data.TimeFilter;
      trackingInstruction.IsAsync = data.IsAsync;
      trackingInstruction.IsIgnoredIfSimulation = data.IsIgnoredIfSimulation;

      await ctx.SaveChangesAsync();

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteTrackingInstructionAsync(ModelStateDictionary modelState, PEContext ctx, long trackingInstructionId)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      ctx.TRKTrackingInstructions.Remove(await ctx.TRKTrackingInstructions.FirstAsync(x => x.TrackingInstructionId == trackingInstructionId));

      await ctx.SaveChangesAsync();

      //return view model
      return result;
    }

    public async Task<VM_Base> CreateBasicTrackingInstructionsAsync(ModelStateDictionary modelState, PEContext ctx)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      var assets = await ctx.MVHAssets
        .IgnoreQueryFilters()
        .Include(x => x.MVHFeatures)
        .Include(x => x.FKAssetType)
        .Where(x => x.OrderSeq > 0)
        .ToListAsync();

      var instructions = await ctx.TRKTrackingInstructions
        .ToListAsync();

      foreach (var item in assets)
      {
        if (item.IsArea)
        {
          await GenerateAreaTrackingInstructionsAsync(ctx, item, instructions);
          continue;
        }

        var areaAssetId = await FindParentAreaAsync(assets, item.FKParentAssetId);
        await GenerateOccupiedTrackingInstructionsAsync(ctx, item, instructions, areaAssetId);
      }

      await ctx.SaveChangesAsync();

      //return view model
      return result;
    }

    public IList<MVHFeature> GetFeatureList(PEContext ctx)
    {
      return ctx.MVHFeatures
        .IgnoreQueryFilters()
        .Where(x => x.FeatureCode > 0)
        .ToList();
    }

    public IList<MVHAsset> GetAssetList(PEContext ctx)
    {
      return ctx.MVHAssets
        .IgnoreQueryFilters()
        .Where(x => x.AssetCode > 0 && !x.IsArea && !x.IsZone)
        .ToList();
    }

    public IList<MVHAsset> GetAreaList(PEContext ctx)
    {
      return ctx.MVHAssets
        .IgnoreQueryFilters()
        .Where(x => x.AssetCode > 0 && x.IsArea)
        .ToList();
    }

    public IList<TRKTrackingInstruction> GetTrackingInstructionList(PEContext ctx)
    {
      return ctx.TRKTrackingInstructions
        .ToList();
    }

    public SelectList GetTrackingInstructionTypeList<V, T>(SelectListMode mode = SelectListMode.Exclude, params T[] items) where V : IEnumName
    {
      Dictionary<T, string> resultDictionary = new Dictionary<T, string>();

      var fieldInfos = new List<FieldInfo>();

      if (typeof(TrackingInstructionType).BaseType.Name != typeof(GenericEnumType<int>).Name)
        fieldInfos.AddRange(typeof(V).BaseType.GetFields(BindingFlags.Public | BindingFlags.Static).ToList());

      fieldInfos.AddRange(typeof(V).GetFields(BindingFlags.Public | BindingFlags.Static).ToList());

      foreach (var propertyItem in fieldInfos)
      {
        dynamic item = propertyItem.GetValue(null);

        var value = (T)item.Value;

        if (mode == SelectListMode.Exclude && !items.Any(x => value.Equals(x)))
        {
          resultDictionary.Add((T)item.Value, item.Name);
        }

        if (mode == SelectListMode.Include && items.Any(x => value.Equals(x)))
        {
          resultDictionary.Add((T)item.Value, item);
        }
      }

      return new SelectList(resultDictionary, "Key", "Value");
    }

    public IList<V_TrackingInstruction> GetTrackingInstructionListForInput(HmiContext ctx)
    {
      return ctx.V_TrackingInstructions
        .ToList();
    }

    private async Task GenerateAreaTrackingInstructionsAsync(PEContext ctx, MVHAsset asset, List<TRKTrackingInstruction> trackingInstructions)
    {
      foreach (var feature in asset.MVHFeatures)
      {
        var type = TrackingInstructionType.Undefined;
        var add = true;
        switch (feature.EnumFeatureType)
        {
          case var value when value == FeatureType.TrackingAreaModeProduction:
            type = TrackingInstructionType.ModeProduction; break;
          case var value when value == FeatureType.TrackingAreaModeAdjustion:
            type = TrackingInstructionType.ModeAdjustion; break;
          case var value when value == FeatureType.TrackingAreaSimulation:
            type = TrackingInstructionType.Simulation; break;
          case var value when value == FeatureType.TrackingAreaAutomaticRelease:
            type = TrackingInstructionType.AutomaticRelease; break;
          case var value when value == FeatureType.TrackingAreaEmpty:
            type = TrackingInstructionType.Empty; break;
          case var value when value == FeatureType.TrackingAreaCobbleDetected:
            type = TrackingInstructionType.CobbleDetected; break;
          case var value when value == FeatureType.TrackingAreaModeLocal:
            type = TrackingInstructionType.ModeLocal; break;
          case var value when value == FeatureType.TrackingAreaCobbleDetectionSelected:
            type = TrackingInstructionType.CobbleDetectionSelected; break;
          default:
            add = false; break;
        }

        if (add)
        {
          var instruction = new TRKTrackingInstruction
          {
            FKFeatureId = feature.FeatureId,
            FKAreaAssetId = asset.AssetId,
            FKPointAssetId = null,
            FKParentTrackingInstruction = null,
            SeqNo = 1,
            TrackingInstructionValue = null,
            EnumTrackingInstructionType = type,
            ChannelId = 1,
            TimeFilter = null,
            IsAsync = false,
            IsIgnoredIfSimulation = false
          };
          if (await CheckTrackingInstructionUniqueAsync(trackingInstructions, instruction))
            await ctx.TRKTrackingInstructions.AddAsync(instruction);
        }
      }
    }

    private async Task GenerateOccupiedTrackingInstructionsAsync(PEContext ctx, MVHAsset asset, List<TRKTrackingInstruction> trackingInstructions, long? areaAssetId)
    {
      if (areaAssetId is null)
        return;

      foreach (var feature in asset.MVHFeatures)
      {
        var assetTypeName = asset.FKAssetType?.AssetTypeName;
        var isShear = !string.IsNullOrEmpty(assetTypeName) && (assetTypeName.ToUpper().Equals("SHEAR") || assetTypeName.ToUpper().Equals("DIVIDING SHEAR"));
        var isDividingShear = !string.IsNullOrEmpty(assetTypeName) && assetTypeName.ToUpper().Equals("DIVIDING SHEAR");
        var isChopping = isShear && feature.FeatureName.ToUpper().Contains(".CHOP_FB");
        var isHeadCut = isShear && feature.FeatureName.ToUpper().Contains(".HCUT_FB");
        var isTailCut = isShear && feature.FeatureName.ToUpper().Contains(".TCUT_FB");

        if (feature.EnumFeatureType == FeatureType.TrackingOccupied)
        {
          var occInstruction = new TRKTrackingInstruction
          {
            FKFeatureId = feature.FeatureId,
            FKAreaAssetId = areaAssetId.Value,
            FKPointAssetId = asset.AssetId,
            FKParentTrackingInstruction = null,
            SeqNo = 1,
            TrackingInstructionValue = 1,
            EnumTrackingInstructionType = TrackingInstructionType.Occupied,
            ChannelId = 1,
            TimeFilter = null,
            IsAsync = false,
            IsIgnoredIfSimulation = false
          };
          var notOccInstruction = new TRKTrackingInstruction
          {
            FKFeatureId = feature.FeatureId,
            FKAreaAssetId = areaAssetId.Value,
            FKPointAssetId = asset.AssetId,
            FKParentTrackingInstruction = null,
            SeqNo = 1,
            TrackingInstructionValue = 0,
            EnumTrackingInstructionType = TrackingInstructionType.Occupied,
            ChannelId = 1,
            TimeFilter = null,
            IsAsync = false,
            IsIgnoredIfSimulation = false
          };
          if (await CheckTrackingInstructionUniqueAsync(trackingInstructions, occInstruction))
            await ctx.TRKTrackingInstructions.AddAsync(occInstruction);
          if (await CheckTrackingInstructionUniqueAsync(trackingInstructions, notOccInstruction))
            await ctx.TRKTrackingInstructions.AddAsync(notOccInstruction);

          if (isDividingShear)
          {
            var lastDivideInstruction = new TRKTrackingInstruction
            {
              FKFeatureId = feature.FeatureId,
              FKAreaAssetId = areaAssetId.Value,
              FKPointAssetId = asset.AssetId,
              FKParentTrackingInstruction = null,
              SeqNo = 2,
              TrackingInstructionValue = 0,
              EnumTrackingInstructionType = TrackingInstructionType.LastDivideVerification,
              ChannelId = 1,
              TimeFilter = null,
              IsAsync = false,
              IsIgnoredIfSimulation = false
            };
            if (await CheckTrackingInstructionUniqueAsync(trackingInstructions, lastDivideInstruction))
              await ctx.TRKTrackingInstructions.AddAsync(lastDivideInstruction);
          }

          if (isShear)
          {
            var partialScrapInstruction = new TRKTrackingInstruction
            {
              FKFeatureId = feature.FeatureId,
              FKAreaAssetId = areaAssetId.Value,
              FKPointAssetId = asset.AssetId,
              FKParentTrackingInstruction = null,
              SeqNo = (short)(isDividingShear ? 3 : 2),
              TrackingInstructionValue = 0,
              EnumTrackingInstructionType = TrackingInstructionType.CalculatePartialScrapForShear,
              ChannelId = 1,
              TimeFilter = null,
              IsAsync = false,
              IsIgnoredIfSimulation = false
            };
            if (await CheckTrackingInstructionUniqueAsync(trackingInstructions, partialScrapInstruction))
              await ctx.TRKTrackingInstructions.AddAsync(partialScrapInstruction);
          }
        }
        if (isChopping)
        {
          var chopOccInstruction = new TRKTrackingInstruction
          {
            FKFeatureId = feature.FeatureId,
            FKAreaAssetId = areaAssetId.Value,
            FKPointAssetId = asset.AssetId,
            FKParentTrackingInstruction = null,
            SeqNo = 1,
            TrackingInstructionValue = 1,
            EnumTrackingInstructionType = TrackingInstructionType.ProcessAutoChopping,
            ChannelId = 1,
            TimeFilter = null,
            IsAsync = false,
            IsIgnoredIfSimulation = false
          };
          if (await CheckTrackingInstructionUniqueAsync(trackingInstructions, chopOccInstruction))
            await ctx.TRKTrackingInstructions.AddAsync(chopOccInstruction);
          var chopNotOccInstruction = new TRKTrackingInstruction
          {
            FKFeatureId = feature.FeatureId,
            FKAreaAssetId = areaAssetId.Value,
            FKPointAssetId = asset.AssetId,
            FKParentTrackingInstruction = null,
            SeqNo = 1,
            TrackingInstructionValue = 0,
            EnumTrackingInstructionType = TrackingInstructionType.ProcessAutoChopping,
            ChannelId = 1,
            TimeFilter = null,
            IsAsync = false,
            IsIgnoredIfSimulation = false
          };
          if (await CheckTrackingInstructionUniqueAsync(trackingInstructions, chopNotOccInstruction))
            await ctx.TRKTrackingInstructions.AddAsync(chopNotOccInstruction);
        }
        if (isHeadCut || isTailCut)
        {
          var headCutInstruction = new TRKTrackingInstruction
          {
            FKFeatureId = feature.FeatureId,
            FKAreaAssetId = areaAssetId.Value,
            FKPointAssetId = asset.AssetId,
            FKParentTrackingInstruction = null,
            SeqNo = 1,
            TrackingInstructionValue = 0,
            EnumTrackingInstructionType = isHeadCut ? TrackingInstructionType.ProcessHeadCut : TrackingInstructionType.ProcessTailCut,
            ChannelId = 1,
            TimeFilter = null,
            IsAsync = false,
            IsIgnoredIfSimulation = false
          };
          if (await CheckTrackingInstructionUniqueAsync(trackingInstructions, headCutInstruction))
            await ctx.TRKTrackingInstructions.AddAsync(headCutInstruction);
        }
      }
    }

    private async Task CheckTrackingInstructionUniqueAsync(PEContext ctx, ModelStateDictionary modelState, VM_TrackingInstruction data, long? trackingInstructionId = null)
    {
      TRKTrackingInstruction trackingInstruction = null;
      if (trackingInstructionId is null)
        trackingInstruction = await ctx.TRKTrackingInstructions.FirstOrDefaultAsync(x => x.FKFeatureId == data.FKFeatureId
          && x.FKAreaAssetId == data.FKAreaAssetId
          && x.FKPointAssetId == data.FKPointAssetId
          && x.TrackingInstructionValue == data.TrackingInstructionValue
          && x.SeqNo == data.SeqNo
          && x.EnumTrackingInstructionType == TrackingInstructionType.GetValue(data.EnumTrackingInstructionType));
      else
        trackingInstruction = await ctx.TRKTrackingInstructions.FirstOrDefaultAsync(x => x.TrackingInstructionId != trackingInstructionId
          && x.FKFeatureId == data.FKFeatureId
          && x.FKAreaAssetId == data.FKAreaAssetId
          && x.FKPointAssetId == data.FKPointAssetId
          && x.TrackingInstructionValue == data.TrackingInstructionValue
          && x.SeqNo == data.SeqNo
          && x.EnumTrackingInstructionType == TrackingInstructionType.GetValue(data.EnumTrackingInstructionType));

      if (trackingInstruction is not null)
        modelState.AddModelError("error", ResourceController.GetErrorText("TrackingInstructionNameNotUnique"));
    }

    private async Task<bool> CheckTrackingInstructionUniqueAsync(List<TRKTrackingInstruction> instructions, TRKTrackingInstruction data)
    {
      await Task.CompletedTask;

      return instructions.FirstOrDefault(x => x.FKFeatureId == data.FKFeatureId
        && x.FKAreaAssetId == data.FKAreaAssetId
        && x.FKPointAssetId == data.FKPointAssetId
        && x.TrackingInstructionValue == data.TrackingInstructionValue
        && x.SeqNo == data.SeqNo
        && x.EnumTrackingInstructionType == TrackingInstructionType.GetValue(data.EnumTrackingInstructionType)) == null;
    }

    private async Task<long?> FindParentAreaAsync(List<MVHAsset> assets, long? parentAssetId)
    {
      if (parentAssetId is null)
        return null;

      var asset = assets
        .First(x => x.AssetId == parentAssetId);

      if (asset.IsArea)
        return asset.AssetId;

      return await FindParentAreaAsync(assets, asset.FKParentAssetId);
    }
  }
}
