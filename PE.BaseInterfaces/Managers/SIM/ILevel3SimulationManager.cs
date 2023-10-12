using System.ServiceModel;
using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.SIM
{
  public interface ILevel3SimulationManager : IManagerBase
  {
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> AddWorkOrderToSchedule(DCWorkOrderToSchedule dc);
    [FaultContract(typeof(ModuleMessage))]
    Task CreateWorkOrders(int limitWeightMin, int limitWeightMax, int materialMin, int materialMax,
      int delayBetweenOrders);
  }
}
