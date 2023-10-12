using Microsoft.AspNetCore.Http;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class BarHandlingService : MaterialInAreaBaseService, IBarHandlingService
  {
    private readonly PEContext _peContext;
    private readonly HmiContext _hmiContext;

    public BarHandlingService(IHttpContextAccessor httpContextAccessor, PEContext peContext, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
      _hmiContext = hmiContext;
    }
  }
}
