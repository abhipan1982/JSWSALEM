using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.Models.DataContracts.Internal.PRM;

namespace PE.PRM.ProdManager.Handlers
{
  public class HeatHandler
  {
    /// <summary>
    ///   Return Heat found by name or default (null) if any don't match
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="heatName"></param>
    /// <returns></returns>
    public Task<PRMHeat> GetHeatByNameAsyncEXT(PEContext ctx, string heatName)
    {
      return ctx.PRMHeats.Where(x => x.HeatName.ToLower().Equals(heatName.ToLower())).SingleOrDefaultAsync();
    }

    /// <summary>
    ///   Return new heat - need to be addet to ctx and saved
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="heatName"></param>
    /// <param name="steelGradeId"></param>
    /// <returns></returns>
    public async Task<PRMHeat> CreateNewHeatAsync(PEContext ctx, long steelGradeId, string heatName)
    {
      PRMHeat heat = new PRMHeat
      {
        HeatName = heatName,
        FKSteelgradeId = steelGradeId,
        FKHeatSupplier = await ctx.PRMHeatSuppliers.Where(x => x.IsDefault).SingleOrDefaultAsync(),
        HeatCreatedTs = DateTime.Now
      };

      return heat;
    }

    /// <summary>
    ///   Function create test heat - called from HMI,
    ///   many fileds are being copied form existing schedule - so param schedule have to have included heat.
    /// </summary>
    /// <param name="schedule"></param>
    /// <returns></returns>
    public PRMHeat CreateTestHeat(PRMHeat heat)
    {
      if (heat is null)
      {
        throw new Exception { Source = "CreateTestHeat" };
      }

      return new PRMHeat { HeatName = "TestHeat" + DateTime.Now, FKHeatSupplierId = heat.FKHeatSupplierId, HeatCreatedTs = DateTime.Now };
    }

    public PRMHeat CreateHeatByUserEXT(PEContext ctx, DCHeatEXT dc)
    {
      PRMHeat heat = new PRMHeat
      {
        HeatName = dc.HeatName,
        FKHeatSupplierId = dc.FKHeatSupplierId,
        FKSteelgradeId = dc.FKSteelgradeId,
        HeatWeight = dc.HeatWeight,
        IsDummy = dc.IsDummy ?? false,
        HeatCreatedTs = DateTime.Now
      };

      ctx.PRMHeats.Add(heat);

      return heat;
    }

    public void UpdateHeat(PRMHeat heat, DCHeatEXT dc)
    {
      if (heat is null)
      {
        throw new Exception { Source = "UpdateHeat" };
      }

      heat.HeatName = dc.HeatName;
      heat.FKHeatSupplierId = dc.FKHeatSupplierId;
      heat.FKSteelgradeId = dc.FKSteelgradeId;
      heat.HeatWeight = dc.HeatWeight;
      heat.IsDummy = dc.IsDummy ?? false;
    }
  }
}
