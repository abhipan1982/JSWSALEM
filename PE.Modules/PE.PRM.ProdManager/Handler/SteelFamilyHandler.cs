using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.Models.DataContracts.Internal.PRM;

namespace PE.PRM.Base.Handlers
{
  public class SteelFamilyHandler
  {
    /// <summary>
    ///   Return steelGroup object found by his name
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="steelGroupCode"></param>
    /// <returns></returns>
    public async Task<PRMSteelGroup> GetSteelFamilyByCodeAsyncEXT(PEContext ctx, string steelGroupCode)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      PRMSteelGroup steelGroup = await ctx.PRMSteelGroups
        .Where(x => x.SteelGroupCode.ToLower().Equals(steelGroupCode.ToLower())).SingleOrDefaultAsync();
      return steelGroup;
    }

    /// <summary>
    ///   Return scrapgroup object found by his ID or null
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="scrapGroupId"></param>
    /// <returns></returns>
    public ValueTask<PRMSteelGroup> GetSteelFamilyByIdAsyncEXT(PEContext ctx, long steelGroupId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      return ctx.PRMSteelGroups.FindAsync(steelGroupId);
    }

    public PRMSteelGroup CreateSteelFamily(DCSteelFamilyEXT dc)
    {
      PRMSteelGroup steelGroup = new PRMSteelGroup
      {
        SteelGroupName = dc.SteelFamilyName,
        SteelGroupCode = dc.SteelFamilyCode,
        SteelGroupDescription = dc.SteelFamilyDescription,
        WearCoefficient = dc.WearCoefficient,
        IsDefault = dc.IsDefault
      };

      return steelGroup;
    }

    public void UpdateSteelFamily(PRMSteelGroup steelGroup, DCSteelFamilyEXT dc)
    {
      if (steelGroup != null)
      {
        steelGroup.SteelGroupName = dc.SteelFamilyName;
        steelGroup.SteelGroupCode = dc.SteelFamilyCode;
        steelGroup.SteelGroupDescription = dc.SteelFamilyDescription;
        steelGroup.WearCoefficient = dc.WearCoefficient;
        steelGroup.IsDefault = dc.IsDefault;
      }
    }

    /// <summary>
    ///   Removes 'IsDefault' flag from steel family default item
    /// </summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    public async Task RemoveIsDefaultFromDefaultItem(PEContext ctx)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      PRMSteelGroup steelGroup = await ctx.PRMSteelGroups.Where(x => x.IsDefault).SingleOrDefaultAsync();
      if (steelGroup != null)
      {
        steelGroup.IsDefault = false;
      }
    }
  }
}
