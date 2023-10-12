using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.TRK.Base.Extensions;

namespace PE.TRK.Base.Handlers
{
  public class TrackingRawMaterialHandlerBase
  {
    public PRMMaterial GetLastMaterialByWorkOrderId(PEContext ctx, long workOrderId)
    {
      return ctx.PRMMaterials
          .Where(x => x.FKWorkOrderId == workOrderId)
          .OrderByDescending(x => x.SeqNo)
          .FirstOrDefault();
    }

    public PRMMaterial GetMaterialByRawMaterialId(PEContext ctx, long rawMaterialId)
    {
      return ctx.TRKRawMaterials
          .Include(x => x.FKMaterial)
          .Where(x => x.FKMaterialId.HasValue && x.RawMaterialId == rawMaterialId)
          .Select(x => x.FKMaterial)
          .FirstOrDefault();
    }

    public void AssignShiftForRawMaterial(PEContext ctx, TRKRawMaterial rawMaterial)
    {
      //Assign shift to RawMaterial
      rawMaterial.FKShiftCalendarId = ctx.EVTShiftCalendars.First(x => x.IsActualShift).ShiftCalendarId;
    }

    public async Task<PRMWorkOrder> GetWorkOrderByRawMaterialId(long basId)
    {
      using (PEContext ctx = new PEContext())
      {
        TRKRawMaterial mvhRawMaterial = await ctx.TRKRawMaterials
          .Include(x => x.FKMaterial.FKWorkOrder)
          .Where(x => x.RawMaterialId == basId && x.FKMaterialId != null)
          .FirstOrDefaultAsync();

        if (mvhRawMaterial == null)
        {
          return null;
        }

        return mvhRawMaterial.FKMaterial.FKWorkOrder;
      }
    }

    public void AssignL3Material(TRKRawMaterial rawMaterial, PRMMaterial prmMaterial)
    {
      string rawMaterialName = rawMaterial.RawMaterialName;

      rawMaterial.SetFKMaterial(prmMaterial);
      rawMaterial.EnumRawMaterialStatus = RawMaterialStatus.Assigned;
      rawMaterial.LastWeight = prmMaterial.MaterialWeight;
      rawMaterial.OldRawMaterialName = rawMaterialName;
      rawMaterial.RawMaterialName = prmMaterial.MaterialName;

      prmMaterial.IsAssigned = true;
    }

    public void UnAssignL3Material(TRKRawMaterial rawMaterial, List<TRKRawMaterialRelation> materialsRelated, bool markAsDummy = false)
    {
      if (rawMaterial.FKMaterial != null) rawMaterial.FKMaterial.IsAssigned = false;

      rawMaterial.SetFKMaterial(null);

      rawMaterial.RawMaterialName = string.IsNullOrEmpty(rawMaterial.OldRawMaterialName) ? rawMaterial.RawMaterialName : rawMaterial.OldRawMaterialName;

      if (rawMaterial.EnumRawMaterialStatus == RawMaterialStatus.Assigned)
        rawMaterial.EnumRawMaterialStatus = RawMaterialStatus.Unassigned;

      if (materialsRelated.Any())
      {
        foreach (var rm in materialsRelated)
        {
          if (rm.ChildRawMaterial is not null && rm.ChildRawMaterial.FKMaterial is not null)
            rm.ChildRawMaterial.SetFKMaterial(null);

          if (rm.ParentRawMaterial is not null && rm.ParentRawMaterial.FKMaterial is not null)
            rm.ParentRawMaterial.SetFKMaterial(null);
        }
      }

      if (markAsDummy) rawMaterial.IsDummy = true;
    }

    public async Task<TRKRawMaterial> FindRawMaterialByIdAsync(PEContext ctx, long id)
    {
      if (ctx == null)
      {
        using (ctx = new PEContext())
        {
          return await ctx.TRKRawMaterials
            .Where(w => w.RawMaterialId == id)
            .OrderByDescending(x => x.RawMaterialId)
            .FirstOrDefaultAsync();
        }
      }

      return await ctx.TRKRawMaterials
            .Where(w => w.RawMaterialId == id)
            .OrderByDescending(x => x.RawMaterialId)
            .FirstOrDefaultAsync();
    }

    public async Task<PRMProductCatalogueType> FindProductCatalogueTypeByIdAsync(PEContext ctx, long id)
    {
      if (ctx == null)
      {
        using (ctx = new PEContext())
        {
          return await ctx.PRMProductCatalogueTypes
            .Where(w => w.ProductCatalogueTypeId == id)
            .FirstOrDefaultAsync();
        }
      }

      return await ctx.PRMProductCatalogueTypes
            .Where(w => w.ProductCatalogueTypeId == id)
            .FirstOrDefaultAsync();
    }

    public async Task<double?> FindRawMaterialWearCoeffByIdAsync(PEContext ctx, long id)
    {
      if (ctx == null)
      {
        await using var context = new PEContext();
        var rawMaterial = await context.TRKRawMaterials
          .OrderByDescending(x => x.RawMaterialId)
          .Include(i => i.FKMaterial.FKHeat.FKSteelgrade.FKSteelGroup)
          .FirstOrDefaultAsync(x => x.RawMaterialId == id);

        return rawMaterial?.FKMaterial?.FKHeat?.FKSteelgrade?.FKSteelGroup?.WearCoefficient ?? null;
      }
      else
      {
        var rawMaterial = await ctx.TRKRawMaterials
          .OrderByDescending(x => x.RawMaterialEndTs)
          .Include(i => i.FKMaterial.FKHeat.FKSteelgrade.FKSteelGroup)
          .FirstOrDefaultAsync(x => x.RawMaterialId == id);

        return rawMaterial?.FKMaterial?.FKHeat?.FKSteelgrade?.FKSteelGroup?.WearCoefficient ?? null;
      }
    }

    public async Task<TRKRawMaterial> FindRawMaterialWithProductByIdAsync(PEContext ctx, long id)
    {
      if (ctx == null)
      {
        using (ctx = new PEContext())
        {
          return await ctx.TRKRawMaterials
            .Include(x => x.FKProduct)
            .OrderByDescending(x => x.RawMaterialId)
            .FirstOrDefaultAsync(x => x.RawMaterialId == id);
        }
      }

      return await ctx.TRKRawMaterials
        .Include(x => x.FKProduct)
        .OrderByDescending(x => x.RawMaterialId)
        .FirstOrDefaultAsync(x => x.RawMaterialId == id);
    }
  }
}
