using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.DbEntity.Models;
using PE.DbEntity.PEContext;
//using PE.BaseModels.DataContracts.Internal.PRM;
using PE.Models.DataContracts.Internal.PRM;
using PE.PRM.Base.Handlers;

namespace PE.PRM.ProdManager.Handler
{
  public class SteelgradeHandler
  {
    /// <summary>
    ///   Return steelgrade object found by his name or default steelgrade if any dont match (optional )
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="steelgradeCode"></param>
    /// <returns></returns>
    public virtual async Task<PRMSteelgrade> GetSteelgradeByCodeAsyncEXT(PEContext ctx, string steelgradeCode,
      bool getDefault = false)
    {
      PRMSteelgrade steelgrade = string.IsNullOrWhiteSpace(steelgradeCode) ?
        null :
        await ctx.PRMSteelgrades
        .Where(x => x.SteelgradeCode.ToLower().Equals(steelgradeCode.ToLower()))
        .Include(i => i.PRMSteelgradeChemicalComposition)
        .Include(i => i.FKScrapGroup)
        .SingleOrDefaultAsync();

      if (steelgrade == null && getDefault)
      {
        steelgrade = await ctx.PRMSteelgrades.Where(x => x.IsDefault)
          .Include(i => i.PRMSteelgradeChemicalComposition)
          .Include(i => i.FKScrapGroup)
          .SingleAsync();
      }

      return steelgrade;
    }

    /// <summary>
    ///   Return steelgrade object found by his ID or null
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="steelgradeId"></param>
    /// <returns></returns>
    public ValueTask<PRMSteelgrade> GetSteelgradeByIdAsyncEXT(PEContext ctx, long steelgradeId)
    {
      return ctx.PRMSteelgrades.FindAsync(steelgradeId);
    }

    /// <summary>
    ///   Return steelgrade and related objects found by his ID or null
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="steelgradeId"></param>
    /// <returns></returns>
    public Task<PRMSteelgrade> GetSteelgradeRelatedObjectsByIdAsync(PEContext ctx, long steelgradeId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      return ctx.PRMSteelgrades.Where(x => x.SteelgradeId == steelgradeId)
        .Include(i => i.PRMSteelgradeChemicalComposition)
        .Include(i => i.FKScrapGroup)
        .Include(i => i.FKSteelGroup)
        .Include(i => i.FKParentSteelgrade)
        .SingleOrDefaultAsync();
    }

    public virtual PRMSteelgrade CreateSteelgrade(DCSteelgradeEXT dc)
    {
      PRMSteelgrade steelgrade = new PRMSteelgrade
      {
        SteelgradeCode = dc.SteelgradeCode,
        SteelgradeName = dc.SteelgradeName,
        SteelgradeDescription = dc.Description,
        Density = dc.Density,
        IsDefault = dc.IsDefault.GetValueOrDefault(),
        CustomCode = dc.CustomCode,
        CustomDescription = dc.CustomDescription,
        CustomName = dc.CustomName,
        FKSteelGroupId = dc.FkSteelGroup,
        FKScrapGroupId = dc.FkScrapGroup,
        FKParentSteelgradeId = dc.ParentSteelgradeId,
      };

      steelgrade.PRMSteelgradeChemicalComposition =
        CreateUpdateSteelgradeChemicalComposition(steelgrade.PRMSteelgradeChemicalComposition, dc);

      return steelgrade;
    }

    public void UpdateSteelgrade(PRMSteelgrade steelgrade, DCSteelgradeEXT dc)
    {
      steelgrade.SteelgradeCode = dc.SteelgradeCode;
      steelgrade.SteelgradeName = dc.SteelgradeName;
      steelgrade.SteelgradeDescription = dc.Description;
      steelgrade.Density = dc.Density;
      steelgrade.IsDefault = dc.IsDefault.GetValueOrDefault();
      steelgrade.CustomCode = dc.CustomCode;
      steelgrade.CustomDescription = dc.CustomDescription;
      steelgrade.CustomName = dc.CustomName;
      steelgrade.FKSteelGroupId = dc.FkSteelGroup;
      steelgrade.FKScrapGroupId = dc.FkScrapGroup;
      steelgrade.FKParentSteelgradeId = dc.ParentSteelgradeId;

      steelgrade.PRMSteelgradeChemicalComposition =
        CreateUpdateSteelgradeChemicalComposition(steelgrade.PRMSteelgradeChemicalComposition, dc);
    }

    private PRMSteelgradeChemicalComposition CreateUpdateSteelgradeChemicalComposition(
      PRMSteelgradeChemicalComposition steelgradeChemicalComposition, DCSteelgradeEXT dc)
    {
      if (steelgradeChemicalComposition == null)
      {
        steelgradeChemicalComposition = new PRMSteelgradeChemicalComposition { FKSteelgradeId = dc.Id };
      }

      steelgradeChemicalComposition.FeMax = dc.FeMax == null ? 0 : dc.FeMax.Value;
      steelgradeChemicalComposition.FeMin = dc.FeMin == null ? 0 : dc.FeMin.Value;
      steelgradeChemicalComposition.CMax = dc.CMax == null ? 0 : dc.CMax.Value;
      steelgradeChemicalComposition.CMin = dc.CMin == null ? 0 : dc.CMin.Value;
      steelgradeChemicalComposition.MnMax = dc.MnMax == null ? 0 : dc.MnMax.Value;
      steelgradeChemicalComposition.MnMin = dc.MnMin == null ? 0 : dc.MnMin.Value;
      steelgradeChemicalComposition.CrMax = dc.CrMax == null ? 0 : dc.CrMax.Value;
      steelgradeChemicalComposition.CrMin = dc.CrMin == null ? 0 : dc.CrMin.Value;
      steelgradeChemicalComposition.MoMax = dc.MoMax == null ? 0 : dc.MoMax.Value;
      steelgradeChemicalComposition.MoMin = dc.MoMin == null ? 0 : dc.MoMin.Value;
      steelgradeChemicalComposition.VMax = dc.VMax == null ? 0 : dc.VMax.Value;
      steelgradeChemicalComposition.VMin = dc.VMin == null ? 0 : dc.VMin.Value;
      steelgradeChemicalComposition.NiMax = dc.NiMax == null ? 0 : dc.NiMax.Value;
      steelgradeChemicalComposition.NiMin = dc.NiMin == null ? 0 : dc.NiMin.Value;
      steelgradeChemicalComposition.CoMax = dc.CoMax == null ? 0 : dc.CoMax.Value;
      steelgradeChemicalComposition.CoMin = dc.CoMin == null ? 0 : dc.CoMin.Value;
      steelgradeChemicalComposition.SiMax = dc.SiMax == null ? 0 : dc.SiMax.Value;
      steelgradeChemicalComposition.SiMin = dc.SiMin == null ? 0 : dc.SiMin.Value;
      steelgradeChemicalComposition.PMax = dc.PMax == null ? 0 : dc.PMax.Value;
      steelgradeChemicalComposition.PMin = dc.PMin == null ? 0 : dc.PMin.Value;
      steelgradeChemicalComposition.SMax = dc.SMax == null ? 0 : dc.SMax.Value;
      steelgradeChemicalComposition.SMin = dc.SMin == null ? 0 : dc.SMin.Value;
      steelgradeChemicalComposition.CuMax = dc.CuMax == null ? 0 : dc.CuMax.Value;
      steelgradeChemicalComposition.CuMin = dc.CuMin == null ? 0 : dc.CuMin.Value;
      steelgradeChemicalComposition.NbMax = dc.NbMax == null ? 0 : dc.NbMax.Value;
      steelgradeChemicalComposition.NbMin = dc.NbMin == null ? 0 : dc.NbMin.Value;
      steelgradeChemicalComposition.AlMax = dc.AlMax == null ? 0 : dc.AlMax.Value;
      steelgradeChemicalComposition.AlMin = dc.AlMin == null ? 0 : dc.AlMin.Value;
      steelgradeChemicalComposition.NMax = dc.NMax == null ? 0 : dc.NMax.Value;
      steelgradeChemicalComposition.NMin = dc.NMin == null ? 0 : dc.NMin.Value;
      steelgradeChemicalComposition.CaMax = dc.CaMax == null ? 0 : dc.CaMax.Value;
      steelgradeChemicalComposition.CaMin = dc.CaMin == null ? 0 : dc.CaMin.Value;
      steelgradeChemicalComposition.BMax = dc.BMax == null ? 0 : dc.BMax.Value;
      steelgradeChemicalComposition.BMin = dc.BMin == null ? 0 : dc.BMin.Value;
      steelgradeChemicalComposition.TiMax = dc.TiMax == null ? 0 : dc.TiMax.Value;
      steelgradeChemicalComposition.TiMin = dc.TiMin == null ? 0 : dc.TiMin.Value;
      steelgradeChemicalComposition.SnMax = dc.SnMax == null ? 0 : dc.SnMax.Value;
      steelgradeChemicalComposition.SnMin = dc.SnMin == null ? 0 : dc.SnMin.Value;
      steelgradeChemicalComposition.OMax = dc.OMax == null ? 0 : dc.OMax.Value;
      steelgradeChemicalComposition.OMin = dc.OMin == null ? 0 : dc.OMin.Value;
      steelgradeChemicalComposition.HMax = dc.HMax == null ? 0 : dc.HMax.Value;
      steelgradeChemicalComposition.HMin = dc.HMin == null ? 0 : dc.HMin.Value;
      steelgradeChemicalComposition.WMax = dc.WMax == null ? 0 : dc.WMax.Value;
      steelgradeChemicalComposition.WMin = dc.WMin == null ? 0 : dc.WMin.Value;
      steelgradeChemicalComposition.PbMax = dc.PbMax == null ? 0 : dc.PbMax.Value;
      steelgradeChemicalComposition.PbMin = dc.PbMin == null ? 0 : dc.PbMin.Value;
      steelgradeChemicalComposition.ZnMax = dc.ZnMax == null ? 0 : dc.ZnMax.Value;
      steelgradeChemicalComposition.ZnMin = dc.ZnMin == null ? 0 : dc.ZnMin.Value;
      steelgradeChemicalComposition.AsMax = dc.AsMax == null ? 0 : dc.AsMax.Value;
      steelgradeChemicalComposition.AsMin = dc.AsMin == null ? 0 : dc.AsMin.Value;
      steelgradeChemicalComposition.MgMax = dc.MgMax == null ? 0 : dc.MgMax.Value;
      steelgradeChemicalComposition.MgMin = dc.MgMin == null ? 0 : dc.MgMin.Value;
      steelgradeChemicalComposition.SbMax = dc.SbMax == null ? 0 : dc.SbMax.Value;
      steelgradeChemicalComposition.SbMin = dc.SbMin == null ? 0 : dc.SbMin.Value;
      steelgradeChemicalComposition.BiMax = dc.BiMax == null ? 0 : dc.BiMax.Value;
      steelgradeChemicalComposition.BiMin = dc.BiMin == null ? 0 : dc.BiMin.Value;
      steelgradeChemicalComposition.TaMax = dc.TaMax == null ? 0 : dc.TaMax.Value;
      steelgradeChemicalComposition.TaMin = dc.TaMin == null ? 0 : dc.TaMin.Value;
      steelgradeChemicalComposition.ZrMax = dc.ZrMax == null ? 0 : dc.ZrMax.Value;
      steelgradeChemicalComposition.ZrMin = dc.ZrMin == null ? 0 : dc.ZrMin.Value;
      steelgradeChemicalComposition.CeMax = dc.CeMax == null ? 0 : dc.CeMax.Value;
      steelgradeChemicalComposition.CeMin = dc.CeMin == null ? 0 : dc.CeMin.Value;
      steelgradeChemicalComposition.TeMax = dc.TeMax == null ? 0 : dc.TeMax.Value;
      steelgradeChemicalComposition.TeMin = dc.TeMin == null ? 0 : dc.TeMin.Value;


      return steelgradeChemicalComposition;
    }

    public async Task<PRMScrapGroup> GetScrapGroupByCodeAsync(PEContext ctx, string scrapGroupCode)
    {
      if (ctx == null) { ctx = new PEContext(); }

      PRMScrapGroup scrapGroup = await ctx.PRMScrapGroups
        .Where(x => x.ScrapGroupCode.ToLower().Equals(scrapGroupCode.ToLower()))
        .SingleOrDefaultAsync();

      return scrapGroup;
    }
  }
}
