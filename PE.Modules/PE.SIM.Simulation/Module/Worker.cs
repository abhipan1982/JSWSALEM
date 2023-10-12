namespace PE.SIM.Simulation.Module
{
  public class Worker : BaseWorker
  {
    #region members

    private static int _timerCycle;

    #endregion

    #region ctor

    public Worker(ILevel1SimulationManager level1SimulationManager, ILevel3SimulationManager level3SimulationManager,
      IL1ConsumptionSimulationManager l1ConsumptionSimulationManager)
    {
      _level1ConsumptionSimulationManager = l1ConsumptionSimulationManager;
      _level1SimulationManager = level1SimulationManager;
      _level3SimulationManager = level3SimulationManager;
    }

    #endregion

    #region managers

    private readonly IL1ConsumptionSimulationManager _level1ConsumptionSimulationManager;
    private readonly ILevel1SimulationManager _level1SimulationManager;
    private readonly ILevel3SimulationManager _level3SimulationManager;

    #endregion

    #region properties

    public static int ParameterOrderGeneartionDelay { get; private set; }
    public static int LimitWeightMin { get; set; }
    public static int LimitWeightMax { get; set; }
    public static int LimitMaterialsMin { get; set; }
    public static int LimitMaterialsMax { get; set; }

    #endregion

    #region module calls

    public override async void TimerMethod(object sender, ElapsedEventArgs e)
    {
      //if (timerCycle % 1 == 0)
      //{
      //  try
      //  {
      //    NotificationController.Info("------------------------------------------------------");

      //    NotificationController.Info("Start consumption simulation");

      //    await _level1ConsumptionSimulationManager.ReadMeasurements();

      //    NotificationController.Info("End consumption simulation");

      //    NotificationController.Info("------------------------------------------------------");
      //  }
      //  catch (Exception ex)
      //  {
      //    NotificationController.Error($"Exception has been throwed during consumption simulation. Exception: {ex}");
      //  }
      //}

      try
      {
        if (_timerCycle % 20 == 0)
        {
          await _level3SimulationManager.CreateWorkOrders(LimitWeightMin, LimitWeightMax, LimitMaterialsMin,
            LimitMaterialsMax, ParameterOrderGeneartionDelay);
        }

        _timerCycle++;
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }
    }

    public override async void ModuleInitialized(object sender, ModuleInitEventArgs e)
    {
      ParameterOrderGeneartionDelay = ParameterController.GetParameterStatic("SIM_WorkOrderGenerationDelay").ValueInt
        .GetValueOrDefault();
      LimitMaterialsMin = LimitController.GetLimitStatic("SIM_WorkOrderNumMaterials").LowerValueInt.GetValueOrDefault();
      LimitMaterialsMax = LimitController.GetLimitStatic("SIM_WorkOrderNumMaterials").UpperValueInt.GetValueOrDefault();

      try
      {
        await _level3SimulationManager.CreateWorkOrders(LimitWeightMin, LimitWeightMax, LimitMaterialsMin,
          LimitMaterialsMax, ParameterOrderGeneartionDelay);
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }
    }

    public override async void ModuleStarted(object sender, ModuleStartEventArgs e)
    {
      await Task.CompletedTask;
      //await _level1SimulationManager.StartSimulation();
    }

    #endregion
  }
}
