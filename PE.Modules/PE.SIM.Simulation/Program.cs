using PE.SIM.Managers;
using PE.SIM.Simulation.Communication;

namespace PE.SIM.Simulation
{
  internal class Program : IModule
  {
    private static Task Main(string[] args)
    {
      return ModuleController.StartModuleAsync<Program>(1000);
    }

    public void RegisterServices(ServiceCollection services)
    {
      services.AddSingleton<ExternalAdapterHandler>();

      services.AddSingleton<ILevel1SimulationManager, L1SimulationManager>();
      services.AddSingleton<ILevel3SimulationManager, L3SimulationManager>();
      services.AddSingleton<IL1ConsumptionSimulationManager, L1ConsumptionSimulationManager>();
    }
  }
}
