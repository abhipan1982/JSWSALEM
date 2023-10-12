using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.PEContext;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.KPI;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  internal class KPIService : BaseService, IKPIService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public KPIService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext)
      : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public VM_KPIChart GetTimeBasedKPIs()
    {
      VM_KPIChart result = new();

      var kpiDefinitions = _peContext.PRFKPIDefinitions.Where(d => d.IsActive.Value).ToList();

      var kpiValues = _hmiContext.V_KPIValues
         .Where(v => v.WorkOrderId == null)
         .GroupBy(v => v.KPIDefinitionId)
         .Select(g => g.OrderByDescending(v => v.KPITime).First())
         .ToList()
         .Where(v => kpiDefinitions.Any(d => d.KPIDefinitionId == v.KPIDefinitionId));

      result.KPIOverviews.AddRange(kpiValues.Select(v => new VM_KPIOverview(v)));

      return result;
    }

    public VM_KPIChart GetWorkOrderBasedKPIs(long workOrderId)
    {
      VM_KPIChart result = new();

      var kpiDefinitions = _peContext.PRFKPIDefinitions.Where(d => d.IsActive.Value).ToList();

      var kpis = _hmiContext.V_KPIValues
        .Where(v => v.WorkOrderId == workOrderId)
        .GroupBy(v => v.KPIDefinitionId)
        .Select(g => g.OrderByDescending(v => v.KPITime).First())
        .ToList()
        .Where(v => kpiDefinitions.Any(d => d.KPIDefinitionId == v.KPIDefinitionId));

      result.KPIOverviews.AddRange(kpis.Select(v => new VM_KPIOverview(v)));

      return result;
    }

    public async Task<VM_KPIChart> GetKPIValuesAsync(long kpiValueId)
    {
      VM_KPIChart result = new();

      var kpi = await _peContext.PRFKPIValues.Include(v => v.FKKPIDefinition).FirstAsync(v => v.KPIValueId == kpiValueId);

      var kpiValues = await _hmiContext.V_KPIValues
         .Where(v => v.KPICode == kpi.FKKPIDefinition.KPICode && v.WorkOrderId == kpi.FKWorkOrderId)
         .OrderByDescending(v => v.KPITime)
         .Take(50)
         .ToListAsync();

      result.KPIOverviews.AddRange(kpiValues.OrderBy(v => v.KPITime).Select(v => new VM_KPIOverview(v)));

      return result;
    }
  }
}
