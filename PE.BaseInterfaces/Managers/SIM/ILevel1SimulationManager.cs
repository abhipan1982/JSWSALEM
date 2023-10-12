using System.ServiceModel;
using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.SIM
{
  public interface ILevel1SimulationManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task StartSimulation();
  }
  //public interface ILevel1SimulationManagerOld : IManagerBase
  //{
  //  [FaultContract(typeof(ModuleMessage))]
  //  Task CallLine();
  //  //Task<DataContractBase> AddWorkOrderToSchedule(DCWorkOrderToSchedule dc);
  //}
}
