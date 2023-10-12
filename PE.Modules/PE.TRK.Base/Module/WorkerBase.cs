using System;
using System.Timers;
using PE.Helpers;
using PE.TRK.Base.Managers.Abstract;
using SMF.Core.Interfaces;
using SMF.Module.Core;
using SMF.Module.Parameter;

namespace PE.TRK.Base.Module
{
  public class WorkerBase : BaseWorker
  {
    protected readonly ITrackingConfigurationManagerBase ConfigurationManager;
    protected readonly ITrackingDispatcherManagerBase DispatcherManager;
    protected readonly ITrackingManagerBase TrackingManager;
    protected readonly ITrackingEventHandlingManagerBase TrackingEventHandlingManager;
    protected readonly IParameterService ParameterService;
    protected int CycleTime = 0;//[s]
    protected DateTime LastTick = DateTime.Now;
    protected Timer ATimer;
    protected static string DefaultLabelTemplateCode;


    public WorkerBase(ITrackingConfigurationManagerBase configurationManager,
      ITrackingDispatcherManagerBase dispatcherManager,
      ITrackingManagerBase trackingManager,
      ITrackingEventHandlingManagerBase trackingEventHandlingManager,
      IParameterService parameterService)
    {
      ConfigurationManager = configurationManager;
      DispatcherManager = dispatcherManager;
      TrackingManager = trackingManager;
      TrackingEventHandlingManager = trackingEventHandlingManager;
      ParameterService = parameterService;
      DefaultLabelTemplateCode = parameterService.GetParameter("DefaultLabelTemplateCode")?.ValueText;

      ModuleController.ParametersChanged += ModuleController_ParametersChanged;
      UpdateParamaters();
    }

    public override void ModuleInitialized(object sender, ModuleInitEventArgs e)
    {
      base.ModuleInitialized(sender, e);

      Init();
    }

    public override void ModuleStarted(object sender, ModuleStartEventArgs e)
    {
      base.ModuleStarted(sender, e);

      ReadConfiguration();
      ReceiveTrackingPointSignals();
    }

    

    public override void ModuleClosed(object sender, ModuleCloseEventArgs e)
    {
      base.ModuleClosed(sender, e);

      Stop();
    }

    protected virtual void ReadConfiguration()
    {
      ConfigurationManager.Init();
    }

    protected virtual void Init()
    {
      DispatcherManager.Init();
      DispatcherManager.Start();
      TrackingEventHandlingManager.Init();

      Timer();
    }

    protected virtual void ReceiveTrackingPointSignals()
    {
      DispatcherManager.ReceiveTrackingPointSignals();
    }

    protected virtual void Stop()
    {

    }

    protected void Timer()
    {
      if (ATimer != null)
      {
        ATimer.Enabled = true;  // start timer
        return;
      }

      ATimer = new System.Timers.Timer();
      ATimer.Elapsed += _aTimer_Elapsed;
      ATimer.Interval = 1000; // every 1 second
      ATimer.Enabled = true;  // start timer
    }

    protected void _aTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      ATimer.Enabled = false; // stop timer

      long elapsedMillis = (long)(DateTime.Now - LastTick).TotalMilliseconds;

      //if (CycleTime % 1 == 0)
      //{
      //  if (CustomSignalToL1State.ShouldBeSent())
      //  {
      //    NotificationController.Warn("Sending Custom signals to L1");

      //    PeCustomSignals customSignals = CustomSignalToL1State.GetPeCustomSignals();
      //    TaskHelper.FireAndForget(() => _level1SignalManager.CallPeCustomSignalsMethod(customSignals),
      //      $"There is a problem while CallPeCustomSignalsMethod");
      //  }
      //}

      if (CycleTime % 1 == 0)
      {
        TaskHelper.FireAndForget(() =>
          TrackingManager.SendMaterialPosition(elapsedMillis).GetAwaiter().GetResult()
          , $"There is a problem while SendMaterialPosition");
      }

      LastTick = DateTime.Now;

      if (CycleTime % 1 == 0)
      {
        //_consumptionSendingCycleTime = 0;

        TaskHelper.FireAndForget(() =>
          TrackingManager.PrintStateAsync().GetAwaiter().GetResult()
          , $"There is a problem while PrintStateAsync");
      }

      CycleTime++;

      ATimer.Enabled = true; // start timer
    }

    protected virtual void ModuleController_ParametersChanged(object sender, ParametersChangedEventArgs e)
    {
      UpdateParamaters();
    }

    protected virtual void UpdateParamaters()
    {
      DefaultLabelTemplateCode = ParameterController.GetParameterStatic("DefaultLabelTemplateCode").ValueText;

      DispatcherManager.SetLabelTemplateCode(DefaultLabelTemplateCode);
    }
  }
}
