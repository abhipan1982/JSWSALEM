namespace PE.SIM.Simulation.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, PE.Interfaces.Lite.ISimulation
  {
    #region ctor

    public ExternalAdapter() : base(typeof(PE.Interfaces.Lite.ISimulation)) { }

    #endregion
  }
}
