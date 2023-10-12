namespace PE.SIM.Simulation.Communication
{
  public class ExternalAdapterHandler
  {
    #region ctor

    public ExternalAdapterHandler(ILevel1SimulationManager level1SimulationManager,
      ILevel3SimulationManager level3SimulationManager)
    {
      _level1SimulationManager = level1SimulationManager;
      _level3SimulationManager = level3SimulationManager;
    }

    #endregion

    #region members

    private readonly ILevel1SimulationManager _level1SimulationManager;
    private readonly ILevel3SimulationManager _level3SimulationManager;

    #endregion
  }
}
