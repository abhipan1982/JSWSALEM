using Microsoft.AspNetCore.Http;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class BarOutletManagementService : MaterialInAreaBaseService, IBarOutletManagementService
  {
    private readonly PEContext _peContext;
    private readonly HmiContext _hmiContext;

    public BarOutletManagementService(IHttpContextAccessor httpContextAccessor, PEContext peContext, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
      _hmiContext = hmiContext;
    }
  }
}
