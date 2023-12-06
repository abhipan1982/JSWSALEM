using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;

namespace PE.PRM.ProdManager.Handler
{
  public class BilletYardHandler
  {
    public Task<MVHAsset> GetReceptionEXT(PEContext ctx)
    {
      return ctx.MVHAssets.Where(x => x.FKAssetType != null && x.FKAssetType.EnumYardType == YardType.MaterialReception).FirstOrDefaultAsync();
    }
  }
}
