using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
//using PE.BaseModels.DataContracts.Internal.PRM;
using PE.Models.DataContracts.Internal.PRM;

using PE.PRM.Base.Handlers.Models;
using PE.PRM.ProdManager.Handler.Models;

namespace PE.PRM.ProdManager.Handler
{
  public class MaterialCatalogueHandler
  {
    public async Task<PRMMaterialCatalogue> GetMaterialCatalogueByNameAsyncEXT(PEContext ctx,
      string materialCatalogueName)
    {
      PRMMaterialCatalogue materialCatalogue =
        string.IsNullOrWhiteSpace(materialCatalogueName)
          ? null
          : await ctx.PRMMaterialCatalogues.Include(x => x.FKShape)
            .Where(x => x.MaterialCatalogueName.ToLower().Equals(materialCatalogueName.ToLower()))
            .SingleOrDefaultAsync();

      return materialCatalogue;
    }

    public PRMMaterialCatalogue CreateMaterialCatalogueEXT(DCMaterialCatalogueEXT dc)
    {
      var catalogue = new PRMMaterialCatalogue
      {
        MaterialCatalogueId = dc.MaterialCatalogueId,
        MaterialCatalogueName = dc.MaterialCatalogueName,
        ExternalMaterialCatalogueName = dc.ExternalMaterialCatalogueName,
        MaterialCatalogueDescription = dc.Description,
        FKShapeId = dc.ShapeId,
        FKMaterialCatalogueTypeId = dc.MaterialCatalogueTypeId,
        IsActive = true,

        // Details
        LengthMin = dc.LengthMin,
        LengthMax = dc.LengthMax,
        WidthMin = dc.WidthMin,
        WidthMax = dc.WidthMax,
        ThicknessMin = dc.ThicknessMin,
        ThicknessMax = dc.ThicknessMax,
        WeightMin = dc.WeightMin,
        WeightMax = dc.WeightMax,
      };

      return catalogue;
    }

    public void UpdateMaterialCatalogueEXT(PRMMaterialCatalogue matCatalogue, DCMaterialCatalogueEXT dc)
    {
      if (matCatalogue != null)
      {
        matCatalogue.MaterialCatalogueId = dc.MaterialCatalogueId;
        matCatalogue.MaterialCatalogueName = dc.MaterialCatalogueName;
        matCatalogue.ExternalMaterialCatalogueName = dc.ExternalMaterialCatalogueName;
        matCatalogue.MaterialCatalogueDescription = dc.Description;
        matCatalogue.FKShapeId = dc.ShapeId;

        if (dc.TypeId.HasValue)
        {
          matCatalogue.FKMaterialCatalogueTypeId = dc.TypeId.Value;
        }

        // Details
        matCatalogue.LengthMin = dc.LengthMin;
        matCatalogue.LengthMax = dc.LengthMax;
        matCatalogue.WidthMin = dc.WidthMin;
        matCatalogue.WidthMax = dc.WidthMax;
        matCatalogue.ThicknessMin = dc.ThicknessMin;
        matCatalogue.ThicknessMax = dc.ThicknessMax;
        matCatalogue.WeightMin = dc.WeightMin;
        matCatalogue.WeightMax = dc.WeightMax;
      }
      else
      {
        throw new Exception { Source = "UpdateMaterialCatalogue - MC not found" };
      }
    }

    public async Task<PRMMaterialCatalogue> GetExistingMaterialCatalogueByParametersAsync(PEContext ctx,
      ValidatedBatchData message)
    {
      IQueryable<PRMMaterialCatalogue> materialCatalogueQuery;

      Expression<Func<PRMMaterialCatalogue, bool>> thickness =
        x => x.ThicknessMin <= message.InputThickness &&
             x.ThicknessMax >= message.InputThickness;

      Expression<Func<PRMMaterialCatalogue, bool>> shape =
        x => x.FKShape.ShapeCode.ToLower() == message.InputShapeSymbol.ToLower();

      if (message.InputWidth.HasValue)
      {

        Expression<Func<PRMMaterialCatalogue, bool>> width =
          x => x.WidthMin.HasValue &&
               x.WidthMax.HasValue &&
               x.WidthMin <= message.InputWidth.Value &&
               x.WidthMax >= message.InputWidth.Value;

        materialCatalogueQuery = ctx.PRMMaterialCatalogues.Where(thickness).Where(shape).Where(width);

        var result = await materialCatalogueQuery.SingleOrDefaultAsync();

        if (result != null)
          return result;
      }

      materialCatalogueQuery = ctx.PRMMaterialCatalogues.Where(thickness).Where(shape);

      return await materialCatalogueQuery.SingleOrDefaultAsync();
    }

    public async Task<PRMMaterialCatalogue> CreateMaterialCatalogueByParametersAsync(PEContext ctx,
      ValidatedBatchData message)
    {
      string materialCatalogueName = $"{message.InputShapeSymbol.ToLower()}_{message.InputThickness}";

      if (message.InputWidth.HasValue)
        materialCatalogueName += materialCatalogueName + $"_{message.InputWidth}";

      PRMMaterialCatalogue materialCatalogue = new PRMMaterialCatalogue
      {
        MaterialCatalogueName = materialCatalogueName,
        FKMaterialCatalogueType =
          await ctx.PRMMaterialCatalogueTypes
            .Where(x => x.MaterialCatalogueTypeCode.ToLower().Equals(message.InputThicknessString.ToLower()))
            .SingleOrDefaultAsync() ?? await ctx.PRMMaterialCatalogueTypes.Where(x => x.IsDefault).SingleAsync(),
        FKShape =
          await ctx.PRMShapes.Where(x => x.ShapeCode.ToLower().Equals(message.InputShapeSymbol.ToLower()))
            .SingleOrDefaultAsync() ?? await ctx.PRMShapes.Where(x => x.IsDefault).SingleAsync(),
        LengthMax = message.InputLength,
        LengthMin = message.InputLength,
        WidthMin = message.InputWidth,
        WidthMax = message.InputWidth,
        ThicknessMin = message.InputThickness,
        ThicknessMax = message.InputThickness,
        WeightMin = message.BilletWeight,
        WeightMax = message.BilletWeight,
        IsActive = true,
      };

      return materialCatalogue;
    }

    public async Task<PRMMaterialCatalogue> GetMaterialCatalogueByIdEXT(PEContext ctx, long materialCatalogueId)
    {
      PRMMaterialCatalogue matCat = await ctx.PRMMaterialCatalogues.FindAsync(materialCatalogueId);

      return matCat;
    }
  }
}
