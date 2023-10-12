using System.Threading.Tasks;
using PE.HMIWWW.ViewModel.Module.Lite.KPI;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IKPIService
  {
    VM_KPIChart GetTimeBasedKPIs();

    VM_KPIChart GetWorkOrderBasedKPIs(long workOrderId);

    Task<VM_KPIChart> GetKPIValuesAsync(long kpiValueId);
  }
}
