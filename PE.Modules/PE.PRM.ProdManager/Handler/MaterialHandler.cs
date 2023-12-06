using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.Models.DataContracts.Internal.PRM;

namespace PE.PRM.ProdManager.Handler
{
  public class MaterialHandler
  {
    /// <summary>
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="workOrderId"></param>
    /// <param name="heatId"></param>
    /// <param name="rawMaterialNumber"></param>
    public List<PRMMaterial> CreateMaterials(MVHAsset location,
      string workOrderName,
      short rawMaterialNumber,
      PRMMaterialCatalogue materialCatalogue,
      PRMHeat heat,
      double weight,
      double materialThickness,
      double? materialLength = null,
      double? materialWidth = null)
    {
      List<PRMMaterial> materialsList = new List<PRMMaterial>();
      for (short i = 1; i <= rawMaterialNumber; i++)
      {
        materialsList.Add(new PRMMaterial
        {
          SeqNo = i,
          MaterialName = $"{workOrderName}_{i}",
          FKHeat = heat,
          MaterialWeight = weight,
          FKMaterialCatalogue = materialCatalogue,
          PRMMaterialSteps = new List<PRMMaterialStep> { CreateOverallStep(location) },
          MaterialLength = materialLength,
          MaterialThickness = materialThickness,
          MaterialWidth = materialWidth
        });
      }

      return materialsList;
    }

    /// <summary>
    ///   Create test material to add it to test work order - called from HMI.
    /// </summary>
    public List<PRMMaterial> CreateTestMaterials(MVHAsset location, string workOrderName, short rawMaterialNumber,
      PRMMaterialCatalogue materialCatalogue, PRMHeat heat, double weight)
    {
      List<PRMMaterial> materialsList = new List<PRMMaterial>();
      for (short i = 1; i <= rawMaterialNumber; i++)
      {
        materialsList.Add(new PRMMaterial
        {
          SeqNo = i,
          MaterialName = $"{workOrderName}_{i}",
          FKHeat = heat,
          MaterialWeight = weight,
          FKMaterialCatalogue = materialCatalogue,
          //IsDummy = true
          //PRMMaterialSteps = new List<PRMMaterialStep> { CreateOverallStep(location) }
        });
      }

      return materialsList;
    }

    public PRMMaterialStep CreateOverallStep(MVHAsset targetLocation)
    {
      PRMMaterialStep overallStep = new PRMMaterialStep { StepNo = 0, FKAssetId = targetLocation.AssetId };

      return overallStep;
    }


    public void DeleteOldMaterialsAfterWoUpdate(PEContext ctx, long workOrderId)
    {
      IQueryable<PRMMaterial> workOrderToDelete = ctx.PRMMaterials.Where(x => x.FKWorkOrderId == workOrderId);

      ctx.PRMMaterials.RemoveRange(workOrderToDelete);
    }

    public PRMMaterial GetLastMaterialByWorkOrderIdAsync(PEContext ctx, long workOrderId)
    {
      PRMMaterial material = ctx.PRMMaterials
        .OrderBy(o => o.MaterialId)
        .FirstOrDefault(w => w.FKWorkOrderId == workOrderId);
      return material;
    }

    public async Task<List<TRKRawMaterial>> GetRawMaterialsByWorkOrderIdAsync(PEContext ctx, long workOrderId)
    {
      var materials = await ctx.PRMMaterials
        .Include(x => x.TRKRawMaterials)
        .Where(x => x.FKWorkOrderId == workOrderId)
        .ToListAsync();

      var result = new List<TRKRawMaterial>();

      materials.ForEach(x =>
      {
        foreach (var item in x.TRKRawMaterials)
        {
          result.Add(item);
        }
      });

      return result;
    }

    /// <summary>
    ///   Get Material by internal Id
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="materialId"></param>
    /// <returns></returns>
    public Task<PRMMaterial> GetMaterialByIdAsync(PEContext ctx, long materialId)
    {
      return ctx.PRMMaterials.Where(w => w.MaterialId == materialId).Include(i => i.FKWorkOrder).SingleOrDefaultAsync();
    }

    /// <summary>
    ///   Get RawMaterial by internal Id
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="rawMaterialId"></param>
    /// <returns></returns>
    public Task<TRKRawMaterial> GetRawMaterialByIdAsync(PEContext ctx, long rawMaterialId)
    {
      return ctx.TRKRawMaterials.Where(w => w.RawMaterialId == rawMaterialId).SingleOrDefaultAsync();
    }

    public void CreateMaterialByUserEXT(PEContext ctx, DCMaterialEXT dc, MVHAsset location, long workOrderId)
    {
      //double matWeight = Convert.ToDouble(dc.Weight / dc.MaterialsNumber);
      for (short i = 1; i <= dc.MaterialsNumber; i++)
      {
        PRMMaterial material = new PRMMaterial
        {
          SeqNo = i,
          MaterialName = $"{dc.FKWorkOrderIdRef}_{i}",
          FKHeatId = dc.FKHeatId,
          MaterialWeight = dc.Weight,
          FKMaterialCatalogueId = dc.FKMaterialCatalogueId,
          FKWorkOrderId = workOrderId,
          PRMMaterialSteps = new List<PRMMaterialStep> { CreateOverallStep(location) }
        };

        ctx.PRMMaterials.Add(material);
      }
    }

    public void RemoveMaterialFromWorkOrder(PEContext ctx, long workOrderId)
    {
      PRMMaterial material = GetLastMaterialByWorkOrderIdAsync(ctx, workOrderId);
      ctx.PRMMaterials.Remove(material);
    }

    public void AddMaterialToWorkOrderAsync(PEContext ctx, DCMaterialEXT dc, long workOrderId, int i, MVHAsset location)
    {
      DateTime currentTime = DateTime.Now;
      TimeSpan testName = DateTime.Now.TimeOfDay;
      double matWeight = Convert.ToDouble(dc.Weight / (dc.MaterialsNumber == 0 ? 1 : dc.MaterialsNumber));
      PRMMaterial material = new PRMMaterial
      {
        IsDummy = false,
        MaterialName = "TestMat_" + testName + "_" + i,
        FKWorkOrderId = workOrderId,
        MaterialWeight = matWeight,
        IsAssigned = false,
        FKHeatId = dc.FKHeatId,
        PRMMaterialSteps = new List<PRMMaterialStep> { CreateOverallStep(location) }
      };

      ctx.PRMMaterials.Add(material);
    }
  }
}
