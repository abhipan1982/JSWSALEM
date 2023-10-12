using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.TRK.Base.Extensions;
using PE.TRK.Base.Managers;
using PE.TRK.Base.Models.Configuration.Concrete;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using SMF.Core.ExceptionHelpers;

namespace PE.TRK.Base.Handlers
{
  public class TrackingHandlerBase
  {
    public virtual List<QueuePosition> GetQueuePositionsInit()
    {
      using (PEContext ctx = new PEContext())
      {
        return ctx.TRKRawMaterialLocations
          .Select(ml => new QueuePosition
          {
            PositionSeq = ml.PositionSeq,
            OrderSeq = ml.OrderSeq,
            AssetCode = ml.AssetCode,
            RawMaterialId = ml.FKRawMaterialId,
            IsVirtualPosition = ml.IsVirtual,
            IsEmpty = !ml.IsOccupied,
            CorrelationId = ml.CorrelationId,
          })
          .ToList();
      }
    }

    public virtual List<TRKLayerRawMaterialRelation> LayerRelationInit(IEnumerable<long> rawMaterialIds)
    {
      using PEContext ctx = new PEContext();
      return ctx.TRKLayerRawMaterialRelations
        .Where(x => x.IsActualRelation && rawMaterialIds.Contains(x.ParentLayerRawMaterialId))
        .ToList();
    }

    public async Task<TRKRawMaterial> CreateRawMaterial(PEContext ctx,
      long assetId,
      DateTime dateOfEvent,
      RawMaterialType rawMaterialType,
      bool isSimulation = false,
      bool executeSaveChanges = true,
      bool isBeforeCommit = false)
    {
      string rawMaterialPrefix = rawMaterialType switch
      {
        _ when rawMaterialType == RawMaterialType.Layer => "Layer_",
        _ when rawMaterialType == RawMaterialType.Bundle => "Bundle_",
        _ when rawMaterialType == RawMaterialType.Material => "RawMaterial_",
        _ => throw new ArgumentOutOfRangeException("Invalid RawMaterialType"),
      };

      string finalMaterialName = isSimulation switch
      {
        true => "SIM_" + rawMaterialPrefix + dateOfEvent.ToString("yyyyMMddHHmmssfff"),
        false => rawMaterialPrefix + dateOfEvent.ToString("yyyyMMddHHmmssfff"),
      };

      TRKRawMaterial rawMaterial = new TRKRawMaterial();
      rawMaterial.Initialize();

      // Before commit - such RawMaterial will 
      rawMaterial.IsBeforeCommit = isBeforeCommit;
      
      rawMaterial.IsDummy = isSimulation;
      rawMaterial.IsVirtual = false;
      rawMaterial.EnumRawMaterialStatus = RawMaterialStatus.Unassigned;
      rawMaterial.EnumLayerStatus = rawMaterialType != RawMaterialType.Layer ? LayerStatus.Undefined : LayerStatus.New;
      rawMaterial.EnumRawMaterialType = rawMaterialType;
      rawMaterial.RawMaterialCreatedTs = dateOfEvent;
      rawMaterial.RawMaterialName = finalMaterialName;

      //add overall step
      rawMaterial.TRKRawMaterialsSteps
        .Add(new TRKRawMaterialsStep()
        {
          ProcessingStepNo = RawMaterialStepNo.OverallStep.Value,
          ProcessingStepTs = dateOfEvent,
          FKAssetId = assetId
        });

      rawMaterial.TRKRawMaterialsSteps
        .Add(new TRKRawMaterialsStep()
        {
          ProcessingStepNo = RawMaterialStepNo.FirstStep.Value,
          ProcessingStepTs = dateOfEvent,
          FKAssetId = assetId
        });

      var shiftCalendar = await ctx.EVTShiftCalendars.FirstOrDefaultAsync(x => x.IsActualShift);
      if (shiftCalendar is null)
        throw new InternalModuleException($"Cannot create new raw material because actual shift is not found.", AlarmDefsBase.UnexpectedError);
      rawMaterial.FKShiftCalendarId = shiftCalendar.ShiftCalendarId;

      ctx.TRKRawMaterials.Add(rawMaterial);

      if (executeSaveChanges)
        await ctx.SaveChangesAsync();

      return rawMaterial;
    }

    public async Task<TRKRawMaterial> GetRawMaterialById(PEContext ctx, long rawMatertialId)
    {
      return await ctx.TRKRawMaterials
        .FirstOrDefaultAsync(x => x.RawMaterialId == rawMatertialId);
    }

    public async Task<List<TRKRawMaterial>> GetRawMaterialsWithLayerByIds(PEContext ctx, List<long> materialIds, long layerId)
    {
      return await ctx.TRKRawMaterials
        .Where(x => x.RawMaterialId == layerId || materialIds.Contains(x.RawMaterialId))
        .ToListAsync();
    }

    public async Task<string> FindRawMaterialNameByIdAsync(long rawMaterialId)
    {
      await using PEContext ctx = new PEContext();
      TRKRawMaterial material = await ctx.TRKRawMaterials
        .FirstOrDefaultAsync(x => x.RawMaterialId == rawMaterialId);

      return material.RawMaterialName;
    }

    public async Task<TRKRawMaterial> FindRawMaterialByIdAsync(PEContext ctx, long id)
    {
      if (ctx == null)
      {
        using (PEContext context = new PEContext())
        {
          return await context.TRKRawMaterials.Where(w => w.RawMaterialId == id).OrderByDescending(x => x.RawMaterialId).FirstOrDefaultAsync();
        }
      }
      else
      {
        return await ctx.TRKRawMaterials.Where(w => w.RawMaterialId == id).OrderByDescending(x => x.RawMaterialId).FirstOrDefaultAsync();
      }
    }

    public async Task<TRKRawMaterial> RejectRawMaterialAsync(PEContext ctx, long id, RejectLocation rejectLocation, short outputPieces)
    {
      TRKRawMaterial rawMaterial;

      if (ctx is null)
      {
        using var context = new PEContext();
        rawMaterial = await context.TRKRawMaterials.Include(m => m.FKProduct).SingleAsync(w => w.RawMaterialId == id);
        rawMaterial.EnumRejectLocation = rejectLocation;
        rawMaterial.OutputPieces = outputPieces;

        if (rejectLocation != RejectLocation.None)
        {
          rawMaterial.UnAssignProduct();

          if (rawMaterial.ScrapPercent > 0.0)
          {
            rawMaterial.ScrapPercent = 0.0;
            rawMaterial.EnumTypeOfScrap = TypeOfScrap.None;
            rawMaterial.ScrapRemarks = "";
            rawMaterial.FKScrapAssetId = null;
          }

          rawMaterial.EnumRawMaterialStatus = RawMaterialStatus.Rejected;
        }
        else
        {
          rawMaterial.EnumRawMaterialStatus = RawMaterialStatus.Discharged;
        }

        await context.SaveChangesAsync();
      }
      else
      {
        rawMaterial = await ctx.TRKRawMaterials.Include(m => m.FKProduct).SingleAsync(w => w.RawMaterialId == id);
        rawMaterial.EnumRejectLocation = rejectLocation;
        rawMaterial.OutputPieces = outputPieces;

        if (rejectLocation != RejectLocation.None)
        {
          rawMaterial.UnAssignProduct();

          if (rawMaterial.ScrapPercent > 0.0)
          {
            rawMaterial.ScrapPercent = 0.0;
            rawMaterial.EnumTypeOfScrap = TypeOfScrap.None;
            rawMaterial.ScrapRemarks = "";
            rawMaterial.FKScrapAssetId = null;
          }

          rawMaterial.EnumRawMaterialStatus = RawMaterialStatus.Rejected;
        }
        else
        {
          rawMaterial.EnumRawMaterialStatus = RawMaterialStatus.Discharged;
        }

        await ctx.SaveChangesAsync();
      }

      return rawMaterial;
    }

    public async Task<PRMMaterial> GetFirstUnAssignedL3MaterialByWorkOrderId(PEContext ctx, long workOrderId)
    {
      return await ctx.PRMMaterials
        .OrderBy(x => x.SeqNo)
        .Include(x => x.TRKRawMaterials)
        .FirstOrDefaultAsync(x => x.FKWorkOrderId == workOrderId && !x.TRKRawMaterials.Any());
    }

    public async Task<PRMMaterial> FindL3MaterialByIdAsync(PEContext ctx, long materialId)
    {
      return await ctx.PRMMaterials
        .OrderBy(x => x.SeqNo)
        .Include(x => x.TRKRawMaterials)
        .FirstOrDefaultAsync(x => x.MaterialId == materialId);
    }

    public async Task<TRKRawMaterial> ChangeLayerStatus(PEContext ctx, long layerId, LayerStatus status)
    {
      TRKRawMaterial layer;

      if (ctx is not null)
      {
        layer = await ctx.TRKRawMaterials.FirstAsync(x => x.RawMaterialId == layerId);
      }
      else
      {
        await using var context = new PEContext();
        layer = await context.TRKRawMaterials.FirstAsync(x => x.RawMaterialId == layerId);
      }

      layer.EnumLayerStatus = status;

      await ctx.SaveChangesAsync();

      return layer;
    }

    public async Task<TRKRawMaterial> ChangeLayerStatusByCriteria(PEContext ctx, LayerStatus statusToChange, List<long> layerIdsToVerify, LayerStatus existingStatus)
    {
      var layer = await ctx.TRKRawMaterials
        .OrderBy(x => x.RawMaterialId)
        .FirstOrDefaultAsync(x => x.EnumRawMaterialType == RawMaterialType.Layer && layerIdsToVerify
          .Contains(x.RawMaterialId) && x.EnumLayerStatus == existingStatus);

      if (layer == null)
        throw new ArgumentException($"There are no layers in pool: {string.Join(',', layerIdsToVerify)} with status: {existingStatus.Name}");

      layer.EnumLayerStatus = statusToChange;

      await ctx.SaveChangesAsync();

      return layer;
    }


    public async Task<TRKRawMaterial> AssignRawMaterialToLayer(PEContext ctx, long rawMaterialId, long layerId)
    {
      var layer = await ctx.TRKRawMaterials.FirstAsync(x => x.RawMaterialId == layerId);
      var rawMaterial = await ctx.TRKRawMaterials.FirstAsync(x => x.RawMaterialId == rawMaterialId);
      TRKLayerRawMaterialRelation layerRelation = new TRKLayerRawMaterialRelation()
      {
        ParentLayerRawMaterialId = layerId,
        ChildLayerRawMaterialId = rawMaterialId
      };

      ctx.TRKLayerRawMaterialRelations.Add(layerRelation);

      if (layer.EnumLayerStatus == LayerStatus.New)
        layer.EnumLayerStatus = LayerStatus.IsForming;

      return layer;
    }

    public async Task<List<TRKRawMaterial>> CreateChildrenMaterials(PEContext ctx,
      int childrenToCreate,
      DateTime dateOfEvent,
      TRKRawMaterial parentRawMaterial,
      bool saveChanges = false,
      bool updateCuttingSeqNo = true)
    {
      List<TRKRawMaterial> result = new List<TRKRawMaterial>();

      for (int i = 0; i < childrenToCreate; i++)
      {
        parentRawMaterial.ChildsNo++;

        string finalMaterialName = parentRawMaterial.RawMaterialName + "_" + parentRawMaterial.ChildsNo;

        TRKRawMaterial rawMaterial = new TRKRawMaterial();

        rawMaterial.Initialize();
        rawMaterial.RawMaterialCreatedTs = dateOfEvent;
        rawMaterial.RawMaterialName = finalMaterialName;
        rawMaterial.FKMaterialId = parentRawMaterial.FKMaterialId;
        rawMaterial.RollingStartTs = parentRawMaterial.RollingStartTs;
        rawMaterial.RollingEndTs = parentRawMaterial.RollingEndTs;
        rawMaterial.EnumRawMaterialType = parentRawMaterial.EnumRawMaterialType;
        rawMaterial.IsDummy = parentRawMaterial.IsDummy;
        rawMaterial.EnumRawMaterialStatus = RawMaterialStatus.Discharged;

        if (updateCuttingSeqNo)
          rawMaterial.CuttingSeqNo = parentRawMaterial.ChildsNo;

        rawMaterial.FKShiftCalendarId = (await ctx.EVTShiftCalendars.FirstOrDefaultAsync(x => x.IsActualShift)).ShiftCalendarId;

        ctx.TRKRawMaterials.Add(rawMaterial);

        TRKRawMaterialRelation materialRelation = new TRKRawMaterialRelation()
        {
          ParentRawMaterial = parentRawMaterial,
          ChildRawMaterial = rawMaterial
        };

        ctx.TRKRawMaterialRelations.Add(materialRelation);
        result.Add(rawMaterial);
      }

      parentRawMaterial.EnumRawMaterialStatus = RawMaterialStatus.Divided;

      if (saveChanges)
        await ctx.SaveChangesAsync();

      return result;
    }

    public async Task AddMaterialCutAsync(PEContext ctx, DateTime operationDate, long assetId, long materialId, TypeOfCut typeOfCut, double cutLength)
    {
      ctx.TRKRawMaterialsCuts.Add(new TRKRawMaterialsCut()
      {
        CuttingLength = cutLength,
        CuttingTs = operationDate,
        EnumTypeOfCut = typeOfCut,
        FKAssetId = assetId,
        FKRawMaterialId = materialId
      });

      await ctx.SaveChangesAsync();
    }

    public async Task<MVHFeature> GetFeatureByAssetIdAndFeatureType(PEContext ctx, long assetId, FeatureType featureType)
    {
      return await ctx.MVHFeatures.FirstAsync(x => x.FKAssetId == assetId && x.EnumFeatureType == featureType);
    }
  }
}
