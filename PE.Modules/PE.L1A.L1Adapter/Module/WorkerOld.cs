using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseInterfaces.Managers.L1A;
using PE.BaseModels.AbstractionModels.L1A;
using PE.BaseModels.ConcreteModels.L1A;
using PE.Common;
using PE.L1A.Managers.Tracking.TrackingAreas;
using PE.L1A.Managers.Tracking.TrackingPoints;
using PE.Tracking;
using SMF.Core.Notification;
using SMF.Module.Core;
using Furnace = PE.L1A.Managers.Tracking.TrackingAreas.Furnace;
using Rake = PE.L1A.Managers.Tracking.TrackingAreas.Rake;

namespace PE.L1A.L1Adapter.Module
{
  public class Worker : BaseWorker
  {
    #region ctor

    public Worker(IL1SetupManager l1SetupManager, IL1SignalManager l1SignalManager,
      IL1ConsumptionManager l1ConsumptionManager, ITrackingManager trackingManager,
      IDispatcherManager dispatcherManager)
    {
      _level1SetupManager = l1SetupManager;
      _level1SignalManager = l1SignalManager;
      _level1ConsumptionManager = l1ConsumptionManager;
      _trackingManager = trackingManager;
      _dispatcherManager = dispatcherManager;

      try
      {
        TrackingHelper.TrackingData = _level1SignalManager.GetTrackingData().GetAwaiter().GetResult();

        _dispatcherManager.InitAsync().GetAwaiter().GetResult();
        _trackingManager.InitAsync().GetAwaiter().GetResult();

        Material.OnGetMaterialInfoEvent += Material_OnGetMaterialInfoEvent;
        Material.OnSendMeasurementsEvent += Material_OnSendMeasurementsEvent;
        TrackingPoint.OnSendTrackingEventEvent += TrackingPoint_OnSendTrackingEventEvent;
        Step.OnSendTrackingEventEvent += Step_OnSendTrackingEventEvent;
        Shear.OnCutEvent += Shear_OnCutEvent;

        _level1SetupManager.InitAsync().GetAwaiter().GetResult();
        _level1SignalManager.InitAsync().GetAwaiter().GetResult();
        _level1ConsumptionManager.InitAsync(_level1SignalManager.GetConsumptionData()).GetAwaiter().GetResult();

        // Events which are stored in queue
        ProcessL1TrackingEvent();
        ProcessL1TrackingQueuePositionChangeEvents();

        //_level1SignalManager.OnL1TrackingPointEvent += Level1SignalManager_OnL1TrackingEvent;
        _level1SignalManager.OnL1ConsumptionMeasurementEvent += _level1SignalManager_OnL1ConsumptionMeasurementEvent;
        _level1SignalManager.OnL1MeasurementEvent += Level1SignalManager_OnL1MeasurementEvent;
        _level1SignalManager.OnL1DisconnectEvent += Level1SignalManager_OnL1DisconnectEvent;

        Rake.OnApronChargeEvent += Rake_OnApronChargeEvent;
        Rake.OnRakeCycleEvent += Rake_OnRakeCycleEvent;
        Layer.OnBarsAmountChangedEvent += Layer_OnBarsAmountChangedEvent;
        Layer.OnLayerFormFinishedEvent += Layer_OnLayerFormFinishedEvent;
        Layer.OnTransferredEvent += Layer_OnTransferredEvent;
        //PE.L1A.Managers.Tracking.TrackingAreas.ChargingArea.OnChargingAreaEvent += ChargingArea_OnCharginAreaEvent;
        //PE.L1A.Managers.Tracking.TrackingAreas.ChargingArea.OnLengthMeasurementReadyEvent += ChargingArea_OnLengthMeasurementReadyEvent;
        //PE.L1A.Managers.Tracking.TrackingAreas.ChargingArea.OnWeightMeasurementReadyEvent += ChargingArea_OnWeightMeasurementReadyEvent;
        //PE.L1A.Managers.Tracking.TrackingAreas.Furnace.OnBT13MeasurementReadyEvent += Furnace_OnMeasurementReadyEvent;
        //PE.L1A.Managers.Tracking.TrackingAreas.ReformingArea.OnReformingAreaEvent += ReformingArea_OnReformingAreaEvent;
        //PE.L1A.Managers.Tracking.TrackingAreas.InsulatedCorridorArea.OnInsulatedCorridorAreaEvent += InsulatedCorridorArea_OnInsulatedCorridorAreaEvent;
        //PE.L1A.Managers.Tracking.TrackingAreas.Garret.OnGarretAreaEvent += Garret_OnGarretAreaEvent;
        //PE.L1A.Managers.Tracking.TrackingAreas.Auxiliaries.OnAuxiliariesAreaEvent += Auxiliaries_OnAuxiliariesAreaEvent;
        //PE.L1A.Managers.Tracking.TrackingAreas.Auxiliaries.OnAuxiliariesMeasurementReadyEvent += Auxiliaries_OnAuxiliariesMeasurementReadyEvent;
        //PE.L1A.Managers.Tracking.TrackingAreas.Conveyor.OnConveyorAreaEvent += Conveyor_OnConveyorAreaEvent;
        //PE.L1A.Managers.Tracking.TrackingAreas.Banding.OnBandingAreaEvent += Banding_OnBandingAreaEvent;
        //PE.L1A.Managers.Tracking.TrackingAreas.Weighing.OnWeighingAreaEvent += Weighing_OnWeighingAreaEvent;
        //PE.L1A.Managers.Tracking.TrackingAreas.Weighing.OnWeightMeasurementReadyEvent += Weighing_OnMeasurementReadyEvent;
        //PE.L1A.Managers.Tracking.TrackingAreas.Transport.OnTransportAreaEvent += Transport_OnTransportAreaEvent;
        CbShear.OnCbShearEvent += CbShear_OnCbShearEvent;
        TrackingQueueElement.OnGetMaterialInfoEvent += TrackingQueueElement_OnGetMaterialInfoEvent;
        //Furnace.OnL1TrackingQueuePositionChangeEvent += OnL1TrackingQueuePositionChangeEvent;
        //InsulatedCorridor.OnL1TrackingQueuePositionChangeEvent += OnL1TrackingQueuePositionChangeEvent;
        //ChargingENF.OnL1TrackingQueuePositionChangeEvent += OnL1TrackingQueuePositionChangeEvent;
        //ReformingEB.OnL1TrackingQueuePositionChangeEvent += OnL1TrackingQueuePositionChangeEvent;
        //Conveyor.OnL1TrackingQueuePositionChangeEvent += OnL1TrackingQueuePositionChangeEvent;
        //Banding.OnL1TrackingQueuePositionChangeEvent += OnL1TrackingQueuePositionChangeEvent;
        //Weighing.OnL1TrackingQueuePositionChangeEvent += OnL1TrackingQueuePositionChangeEvent;
        //Transport.OnL1TrackingQueuePositionChangeEvent += OnL1TrackingQueuePositionChangeEvent;
        //Storage.OnL1TrackingQueuePositionChangeEvent += OnL1TrackingQueuePositionChangeEvent;
        //EnterTableET.OnL1TrackingQueuePositionChangeEvent += OnL1TrackingQueuePositionChangeEvent;
        ////Garret.OnL1TrackingQueuePositionChangeEvent += OnL1TrackingQueuePositionChangeEvent;
        //Rake.OnL1TrackingQueuePositionChangeEvent += OnL1TrackingQueuePositionChangeEvent;
        Furnace.OnSendTrackingEvent += OnSendTrackingEvent;
        //InsulatedCorridor.OnSendTrackingEvent += OnSendTrackingEvent;
        //ChargingENF.OnSendTrackingEvent += OnSendTrackingEvent;
        //ReformingEB.OnSendTrackingEvent += OnSendTrackingEvent;
        //Conveyor.OnSendTrackingEvent += OnSendTrackingEvent;
        //Banding.OnSendTrackingEvent += OnSendTrackingEvent;
        //Weighing.OnSendTrackingEvent += OnSendTrackingEvent;
        //Transport.OnSendTrackingEvent += OnSendTrackingEvent;
        //Storage.OnSendTrackingEvent += OnSendTrackingEvent;
        EnterTableET.OnSendTrackingEvent += OnSendTrackingEvent;
        //Garret.OnSendTrackingEvent += OnSendTrackingEvent;
        //Auxiliaries.OnSendTrackingEvent += OnSendTrackingEvent;
        Rake.OnSendTrackingEvent += OnSendTrackingEvent;
        Layers.OnSendTrackingEvent += OnSendTrackingEvent;
        //Layers.OnL1TrackingQueuePositionChangeEvent += OnL1TrackingQueuePositionChangeEvent;

        _moduleInitialized = true;
        StartSimulation();

        Timer();
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, "Something went wrong while initializing L1Adapter module");
      }
    }

    #endregion ctor

    #region managers

    private readonly IL1SetupManager _level1SetupManager;
    private readonly IL1SignalManager _level1SignalManager;
    private readonly IL1ConsumptionManager _level1ConsumptionManager;
    private readonly ITrackingManager _trackingManager;
    private readonly IDispatcherManager _dispatcherManager;
    private readonly bool _moduleInitialized = false;

    #endregion managers

    #region members

    private static int _consumptionSendingCycleTime; //[s]
    private DateTime _lastTick = DateTime.Now;
    private System.Timers.Timer _aTimer;

    #endregion members

    #region event methods

    private void CbShear_OnCbShearEvent(CBShearEventArgs eventArgs)
    {
      NotificationController.Debug("{TrackingOperation}" +
                                   $"CbShear_OnCbShearEvent with Arguments: SlittingFactor:{eventArgs.SlittingFactor}" +
                                   DateTime.Now.ToString("yyyyMMddHHmmss.fff"));

      TaskHelper.FireAndForget(() =>
          _trackingManager.SendCutMessage(eventArgs.AssetCode, eventArgs.CutLength, TypeOfCut.DivideCutN,
              eventArgs.SlittingFactor)
            .GetAwaiter()
            .GetResult(),
        "Something went wrong while SendCutMessage on CbShear_OnCbShearEvent");
    }

    private async void TrackingQueueElement_OnGetMaterialInfoEvent(EventArgs eventArgs)
    {
      try
      {
        NotificationController.Debug("{TrackingOperation} TrackingQueueElement_OnGetMaterialInfoEvent");
        MaterialInfoBase materialInfo = await _trackingManager.GetMaterialInfoAsync(false).ConfigureAwait(false);

        TrackingHelper.Furnace.SetMaterialInfo(materialInfo);
        TaskHelper.FireAndForget(() => TrackingHelper.Furnace.TriggerL1TrackingQueuePositionChangeEvent());
        TaskHelper.FireAndForget(() =>
          TrackingHelper.Furnace.TriggerSendTrackingEvent(new TrackingEventEventArgs(materialInfo.MaterialId, false,
            false, 1, TrackingHelper.Furnace.AssetCode, true, PE.DbEntity.Enums.TrackingEventType.HeadEnter,
            DateTime.Now)));
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, "There is a problem while getting material info");
      }
    }

    private void OnL1TrackingQueuePositionChangeEvent(L1TrackingQueuePositionChangeEventArgs eventArgs)
    {
      NotificationController.Debug(
        "{TrackingOperation} OnL1TrackingQueuePositionChangeEvent start for AssetCode: {AssetCode} queuePositions: {QueuePositions}",
        "OnL1TrackingQueuePositionChangeEvent", eventArgs.AssetCode,
        JsonConvert.SerializeObject(eventArgs.QueuePositions));

      TaskHelper.FireAndForget(() =>
        {
          _trackingManager.ProcessL1TrackingQueuePositionChange(eventArgs.QueuePositions, eventArgs.AssetCode)
            .GetAwaiter().GetResult();

          ModuleController.HmiRefresh(HMIRefreshKeys.TrackingManagement)
            .GetAwaiter().GetResult();
        },
        $"Something went wrong while ProcessL1TrackingQueuePositionChange for AssetCode: {eventArgs.AssetCode}");
    }

    /// <summary>
    ///   Event for cutting
    /// </summary>
    /// <param name="eventArgs"></param>
    private void Shear_OnCutEvent(CutEventArgs eventArgs)
    {
      NotificationController.Debug("{TrackingOperation} Shear_OnCutEvent eventArgs: {EventArgs}", "Shear_OnCutEvent",
        JsonConvert.SerializeObject(eventArgs));
      TaskHelper.FireAndForget(() =>
          _trackingManager.SendCutMessage(eventArgs.AssetCode, eventArgs.CutLength, eventArgs.TypeOfCut)
            .GetAwaiter().GetResult(),
        "There is a problem while sending cut telegram"
      );
    }

    /// <summary>
    ///   Event for disconnect with opc server
    /// </summary>
    /// <param name="eventArgs"></param>
    private void Level1SignalManager_OnL1DisconnectEvent(EventArgs eventArgs)
    {
      try
      {
        _trackingManager.StopAsync().GetAwaiter().GetResult();
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, "There is a problem while disconnect event");
      }
    }

    private void TrackingPoint_OnSendTrackingEventEvent(TrackingEventEventArgs eventArgs)
    {
      NotificationController.Debug("{TrackingOperation} TrackingPoint_OnSendTrackingEventEvent eventArgs: {EventArgs}",
        "TrackingPoint_OnSendTrackingEventEvent", JsonConvert.SerializeObject(eventArgs));

      TaskHelper.FireAndForget(() =>
          _trackingManager.SendTrackingEvent(eventArgs.MaterialId, eventArgs.IsLastPass, eventArgs.IsReversed,
              eventArgs.PassNumber, eventArgs.AssetCode, eventArgs.IsArea, eventArgs.EventType, eventArgs.TriggerDate)
            .GetAwaiter().GetResult(),
        $"There is a problem while sending tracking event for material: {eventArgs.MaterialId} AssetCode: {eventArgs.AssetCode}");
    }

    private void Step_OnSendTrackingEventEvent(TrackingEventEventArgs eventArgs)
    {
      NotificationController.Debug("{TrackingOperation} Step_OnSendTrackingEventEvent eventArgs: {EventArgs}",
        "Step_OnSendTrackingEventEvent", JsonConvert.SerializeObject(eventArgs));

      TaskHelper.FireAndForget(() =>
          _trackingManager.SendTrackingEvent(eventArgs.MaterialId, eventArgs.IsLastPass, eventArgs.IsReversed,
              eventArgs.PassNumber, eventArgs.AssetCode, eventArgs.IsArea, eventArgs.EventType, eventArgs.TriggerDate)
            .GetAwaiter().GetResult(),
        $"There is a problem while sending tracking event for material: {eventArgs.MaterialId} AssetCode: {eventArgs.AssetCode}");
    }

    private void OnSendTrackingEvent(TrackingEventEventArgs eventArgs)
    {
      NotificationController.Debug("{TrackingOperation} OnSendTrackingEvent eventArgs: {EventArgs}",
        "OnSendTrackingEvent", JsonConvert.SerializeObject(eventArgs));
      TaskHelper.FireAndForget(() =>
          _trackingManager.SendTrackingEvent(eventArgs.MaterialId, eventArgs.IsLastPass, eventArgs.IsReversed,
              eventArgs.PassNumber, eventArgs.AssetCode, eventArgs.IsArea, eventArgs.EventType, eventArgs.TriggerDate)
            .GetAwaiter().GetResult(),
        $"There is a problem while sending tracking event for material: {eventArgs.MaterialId} AssetCode: {eventArgs.AssetCode}");
    }

    /// <summary>
    ///   Material_OnSendMeasurementsEvent
    /// </summary>
    /// <param name="eventArgs"></param>
    private void Material_OnSendMeasurementsEvent(MeasurementEventArgs eventArgs)
    {
      NotificationController.Debug("{TrackingOperation} Material_OnSendMeasurementsEvent eventArgs: {EventArgs}",
        "Material_OnSendMeasurementsEvent", JsonConvert.SerializeObject(eventArgs));
      TaskHelper.FireAndForget(() =>
          _trackingManager.SendMeasurements(eventArgs.MaterialId, eventArgs.IsLastPass, eventArgs.IsReversed,
              eventArgs.PassNumber, eventArgs.TrackingStep, eventArgs.TrackingPoints)
            .GetAwaiter().GetResult(),
        $"There is a problem while sending measurements event for material: {eventArgs.MaterialId}");
    }

    /// <summary>
    ///   Material_OnGetMaterialInfoEvent
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventArgs"></param>
    private void Material_OnGetMaterialInfoEvent(Object sender, GetMaterialInfoEventArgs eventArgs)
    {
      TaskHelper.FireAndForget(() =>
      {
        MaterialInfoBase materialInfo = _trackingManager.GetMaterialInfoAsync(true).GetAwaiter().GetResult();

        if (materialInfo == null)
        {
          NotificationController.Error("Material_OnGetMaterialInfoEvent materialInfo is null");
        }

        if (!(sender is Material))
        {
          NotificationController.Error("Material_OnGetMaterialInfoEvent could not cast sender to Material");
        }

        (sender as Material)?.SetMaterialInfo(materialInfo);
      });
    }

    /// <summary>
    ///   _level1SignalManager_OnL1ConsumptionMeasurementEvent
    /// </summary>
    /// <param name="eventArgs"></param>
    private void _level1SignalManager_OnL1ConsumptionMeasurementEvent(
      PE.DTO.Internal.L1Adapter.L1ConsumptionMeasurementEventArgs eventArgs)
    {
      TaskHelper.FireAndForget(() =>
        _level1ConsumptionManager.UpdateConsumptionMeasurement(eventArgs.MillConsumptionMeasurement));
    }

    /// <summary>
    ///   Level1SignalManager_OnL1TrackingEvent
    /// </summary>
    /// <param name="eventArgs"></param>
    private void ProcessL1TrackingEvent()
    {
      _ = Task.Run(() =>
      {
        while (true)
        {
          try
          {
            //NotificationController.Debug("Level1SignalManager_OnL1TrackingEvent");
            if (!TrackingHelper.L1MillEvents.IsEmpty)
            {
              //NotificationController.Debug("Level1SignalManager_OnL1TrackingEvent - not empty");
              bool dequeuResult =
                TrackingHelper.L1MillEvents.TryDequeue(
                  out PE.DTO.Internal.L1Adapter.L1TrackingPointEventArgs eventArgs);
              if (dequeuResult)
              {
                //NotificationController.Debug("{TrackingOperation} Level1SignalManager_OnL1TrackingEvent eventArgs: {EventArgs}", "Level1SignalManager_OnL1TrackingEvent", JsonConvert.SerializeObject(eventArgs));

                int trackingArea = TrackingHelper.TrackingData.TrackingAreas
                  .First(x => x.TrackingPoints
                    .Any(y => y.TrackPoint == eventArgs.TrackPoint)).TrackArea;
                _dispatcherManager
                  .MaterialHasOccupiedTheTrackingPoint(eventArgs.TrackPoint, trackingArea, eventArgs.PlaceOccupied)
                  .GetAwaiter().GetResult();
              }
              else
              {
                NotificationController.Warn("Level1SignalManager_OnL1TrackingEvent - Dequeue false");
              }
            }
          }
          catch (Exception ex)
          {
            NotificationController.LogException(ex);
          }
        }
      });
    }

    /// <summary>
    ///   Level1SignalManager_OnL1TrackingEvent
    /// </summary>
    /// <param name="eventArgs"></param>
    private void ProcessL1TrackingQueuePositionChangeEvents()
    {
      _ = Task.Run(() =>
      {
        while (true)
        {
          try
          {
            if (!TrackingHelper.TrackingQueuePositionChangeEvents.IsEmpty)
            {
              bool dequeuResult =
                TrackingHelper.TrackingQueuePositionChangeEvents.TryDequeue(
                  out PE.DTO.Internal.L1Adapter.L1TrackingQueuePositionChangeEventArgs eventArgs);
              if (dequeuResult)
              {
                _trackingManager.ProcessL1TrackingQueuePositionChange(eventArgs.QueuePositions, eventArgs.AssetCode)
                  .GetAwaiter().GetResult();

                ModuleController.HmiRefresh(HMIRefreshKeys.TrackingManagement);
              }
              else
              {
                NotificationController.Warn("OnL1TrackingQueuePositionChangeEvent - Dequeue false");
              }
            }
          }
          catch (Exception ex)
          {
            NotificationController.LogException(ex);
          }
        }
      });
    }

    /// <summary>
    ///   Level1SignalManager_OnL1MeasurementEvent
    /// </summary>
    /// <param name="eventArgs"></param>
    private void Level1SignalManager_OnL1MeasurementEvent(PE.DTO.Internal.L1Adapter.L1MeasurementEventArgs eventArgs)
    {
      TaskHelper.FireAndForget(() => _trackingManager.PrepareMeasurements(eventArgs.TrackingData),
        "Something went wrong while preparing measurements");
    }

    private void Rake_OnApronChargeEvent(EventArgs eventArgs)
    {
      NotificationController.Debug("{TrackingOperation} Rake_OnApronChargeEvent " +
                                   DateTime.Now.ToString("yyyyMMddHHmmss.fff"));
      TaskHelper.FireAndForget(() => _dispatcherManager.ProcessApronChargeEvent().GetAwaiter().GetResult(),
        "Something went wrong while ProcessApronChargeEvent on Rake_OnApronChargeEvent");
    }

    private void Rake_OnRakeCycleEvent(EventArgs eventArgs)
    {
      NotificationController.Debug("{TrackingOperation} Rake_OnRakeCycleEvent" +
                                   DateTime.Now.ToString("yyyyMMddHHmmss.fff"));
      TaskHelper.FireAndForget(() => _dispatcherManager.ProcessRakeCycleEvent().GetAwaiter().GetResult(),
        "Something went wrong while ProcessRakeCycleEvent on Rake_OnRakeCycleEvent");
    }

    private void Layer_OnTransferredEvent(EventArgs eventArgs)
    {
      NotificationController.Debug("{TrackingOperation} Layer_OnTransferredEvent ", "Layer_OnTransferredEvent");
      TaskHelper.FireAndForget(() =>
          _dispatcherManager.ProcessLayerTransferredEventAsync().GetAwaiter().GetResult(),
        "There is a problem while processing Layer_OnTransferredEvent");
    }

    private void Layer_OnLayerFormFinishedEvent(EventArgs eventArgs)
    {
      NotificationController.Debug("{TrackingOperation} Layer_OnLayerFormFinishedEvent ",
        "Layer_OnLayerFormFinishedEvent");
      TaskHelper.FireAndForget(() =>
          _dispatcherManager.ProcessLayerFormFinishedEventAsync().GetAwaiter().GetResult(),
        "There is a problem while processing Layer_OnLayerFormFinishedEvent");
    }

    private void Layer_OnBarsAmountChangedEvent(BarsAmountChangeEventArgs eventArgs)
    {
      NotificationController.Debug("{TrackingOperation} Layer_OnBarsAmountChangedEvent eventArgs: {EventArgs}",
        "Layer_OnBarsAmountChangedEvent", JsonConvert.SerializeObject(eventArgs));

      TaskHelper.FireAndForget(() =>
          _dispatcherManager.ProcessBarsAmountChangedEventAsync(eventArgs.BarsAmount).GetAwaiter().GetResult()
        , $"There is a problem while processing Layer_OnBarsAmountChangedEvent with barsAmount: {eventArgs.BarsAmount}");
    }

    #endregion event methods

    #region module calls

    public override void TimerMethod(object sender, ElapsedEventArgs e)
    {
    }

    public override void ModuleInitialized(object sender, ModuleInitEventArgs e)
    {
    }

    public override void ModuleStarted(object sender, ModuleStartEventArgs e)
    {
    }

    public override void ModuleClosed(object sender, ModuleCloseEventArgs e)
    {
      Material.OnGetMaterialInfoEvent -= Material_OnGetMaterialInfoEvent;
      Material.OnSendMeasurementsEvent -= Material_OnSendMeasurementsEvent;
      TrackingPoint.OnSendTrackingEventEvent -= TrackingPoint_OnSendTrackingEventEvent;
      Step.OnSendTrackingEventEvent -= Step_OnSendTrackingEventEvent;
      Shear.OnCutEvent -= Shear_OnCutEvent;

      //_level1SignalManager.OnL1TrackingPointEvent -= Level1SignalManager_OnL1TrackingEvent;
      _level1SignalManager.OnL1ConsumptionMeasurementEvent -= _level1SignalManager_OnL1ConsumptionMeasurementEvent;
      _level1SignalManager.OnL1MeasurementEvent -= Level1SignalManager_OnL1MeasurementEvent;
      _level1SignalManager.OnL1DisconnectEvent -= Level1SignalManager_OnL1DisconnectEvent;

      Rake.OnApronChargeEvent -= Rake_OnApronChargeEvent;
      Rake.OnRakeCycleEvent -= Rake_OnRakeCycleEvent;
      Layer.OnBarsAmountChangedEvent -= Layer_OnBarsAmountChangedEvent;
      Layer.OnLayerFormFinishedEvent -= Layer_OnLayerFormFinishedEvent;
      Layer.OnTransferredEvent -= Layer_OnTransferredEvent;
      //PE.L1A.Managers.Tracking.TrackingAreas.Furnace.OnFurnaceEvent -= Furnace_OnFurnaceEvent;
      //PE.L1A.Managers.Tracking.TrackingAreas.ChargingArea.OnChargingAreaEvent -= ChargingArea_OnCharginAreaEvent;
      //PE.L1A.Managers.Tracking.TrackingAreas.ChargingArea.OnLengthMeasurementReadyEvent -= ChargingArea_OnLengthMeasurementReadyEvent;
      //PE.L1A.Managers.Tracking.TrackingAreas.ChargingArea.OnWeightMeasurementReadyEvent -= ChargingArea_OnWeightMeasurementReadyEvent;
      //PE.L1A.Managers.Tracking.TrackingAreas.Furnace.OnBT13MeasurementReadyEvent -= Furnace_OnMeasurementReadyEvent;
      //PE.L1A.Managers.Tracking.TrackingAreas.ReformingArea.OnReformingAreaEvent -= ReformingArea_OnReformingAreaEvent;
      //PE.L1A.Managers.Tracking.TrackingAreas.Garret.OnGarretAreaEvent -= Garret_OnGarretAreaEvent;
      //PE.L1A.Managers.Tracking.TrackingAreas.Auxiliaries.OnAuxiliariesAreaEvent -= Auxiliaries_OnAuxiliariesAreaEvent;

      //PE.L1A.Managers.Tracking.TrackingAreas.Auxiliaries.OnAuxiliariesMeasurementReadyEvent -= Auxiliaries_OnAuxiliariesMeasurementReadyEvent;
      //PE.L1A.Managers.Tracking.TrackingAreas.Conveyor.OnConveyorAreaEvent -= Conveyor_OnConveyorAreaEvent;
      //PE.L1A.Managers.Tracking.TrackingAreas.Banding.OnBandingAreaEvent -= Banding_OnBandingAreaEvent;
      //PE.L1A.Managers.Tracking.TrackingAreas.Weighing.OnWeighingAreaEvent -= Weighing_OnWeighingAreaEvent;
      //PE.L1A.Managers.Tracking.TrackingAreas.Weighing.OnWeightMeasurementReadyEvent -= Weighing_OnMeasurementReadyEvent;
      //PE.L1A.Managers.Tracking.TrackingAreas.Transport.OnTransportAreaEvent -= Transport_OnTransportAreaEvent;
      //PE.L1A.Managers.Tracking.TrackingAreas.InsulatedCorridorArea.OnInsulatedCorridorAreaEvent -= InsulatedCorridorArea_OnInsulatedCorridorAreaEvent;
      CbShear.OnCbShearEvent -= CbShear_OnCbShearEvent;
      //InsulatedCorridor.OnL1TrackingQueuePositionChangeEvent += OnL1TrackingQueuePositionChangeEvent;
      TrackingQueueElement.OnGetMaterialInfoEvent -= TrackingQueueElement_OnGetMaterialInfoEvent;
      //Furnace.OnL1TrackingQueuePositionChangeEvent -= OnL1TrackingQueuePositionChangeEvent;
      //ChargingENF.OnL1TrackingQueuePositionChangeEvent -= OnL1TrackingQueuePositionChangeEvent;
      //Conveyor.OnL1TrackingQueuePositionChangeEvent -= OnL1TrackingQueuePositionChangeEvent;
      //Banding.OnL1TrackingQueuePositionChangeEvent -= OnL1TrackingQueuePositionChangeEvent;
      //Weighing.OnL1TrackingQueuePositionChangeEvent -= OnL1TrackingQueuePositionChangeEvent;
      //Transport.OnL1TrackingQueuePositionChangeEvent -= OnL1TrackingQueuePositionChangeEvent;
      //Storage.OnL1TrackingQueuePositionChangeEvent -= OnL1TrackingQueuePositionChangeEvent;
      EnterTableET.OnL1TrackingQueuePositionChangeEvent -= OnL1TrackingQueuePositionChangeEvent;
      Rake.OnL1TrackingQueuePositionChangeEvent -= OnL1TrackingQueuePositionChangeEvent;
      Furnace.OnSendTrackingEvent -= OnSendTrackingEvent;
      //ChargingENF.OnSendTrackingEvent -= OnSendTrackingEvent;
      //ReformingEB.OnSendTrackingEvent -= OnSendTrackingEvent;
      //Conveyor.OnSendTrackingEvent -= OnSendTrackingEvent;
      //Banding.OnSendTrackingEvent -= OnSendTrackingEvent;
      //Weighing.OnSendTrackingEvent -= OnSendTrackingEvent;
      //Transport.OnSendTrackingEvent -= OnSendTrackingEvent;
      //Storage.OnSendTrackingEvent -= OnSendTrackingEvent;
      EnterTableET.OnSendTrackingEvent -= OnSendTrackingEvent;
      Rake.OnSendTrackingEvent -= OnSendTrackingEvent;
      //Garret.OnSendTrackingEvent -= OnSendTrackingEvent;
      //InsulatedCorridor.OnSendTrackingEvent -= OnSendTrackingEvent;
      //Garret.OnL1TrackingQueuePositionChangeEvent -= OnL1TrackingQueuePositionChangeEvent;
      //Auxiliaries.OnSendTrackingEvent -= OnSendTrackingEvent;
      Layers.OnSendTrackingEvent -= OnSendTrackingEvent;
      Layers.OnL1TrackingQueuePositionChangeEvent -= OnL1TrackingQueuePositionChangeEvent;

      _level1SignalManager.Close();
      _level1SetupManager.Close();
    }

    #endregion module calls

    #region private methods

    private void Timer()
    {
      if (_aTimer != null)
      {
        _aTimer.Enabled = true; // start timer
        return;
      }

      _aTimer = new System.Timers.Timer();
      _aTimer.Elapsed += _aTimer_Elapsed;
      _aTimer.Interval = 1000; // every 1 second
      _aTimer.Enabled = true; // start timer
    }

    private void _aTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      _aTimer.Enabled = false; // stop timer

      long elapsedMillis = (long)(DateTime.Now - _lastTick).TotalMilliseconds;

      if (_consumptionSendingCycleTime % 1 == 0)
      {
        TaskHelper.FireAndForget(() => _trackingManager.SendMaterialPosition(elapsedMillis).GetAwaiter().GetResult(),
          "There is a problem while SendMaterialPosition");
      }

      _lastTick = DateTime.Now;

      if (_consumptionSendingCycleTime % 1 == 0)
      {
        //_consumptionSendingCycleTime = 0;
        TaskHelper.FireAndForget(() =>
            _trackingManager.PrintStateAsync().GetAwaiter().GetResult(),
          "There is a problem while PrintStateAsync");
      }

      //send consumption measurements
      if (_consumptionSendingCycleTime % 60 == 0)
      {
        TaskHelper.FireAndForget(() =>
            _level1ConsumptionManager.SendMeasurements().GetAwaiter().GetResult(),
          "There is a problem while SendMeasurements");

        _consumptionSendingCycleTime = 0;
      }

      _consumptionSendingCycleTime++;

      _aTimer.Enabled = true; // start timer
    }

    private void StartSimulation()
    {
      bool testSimulation = false;
      try
      {
        testSimulation = Convert.ToBoolean(ConfigurationManager.AppSettings["TestSimulation"]);
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex,
          "Something went wrong while getting TestSimulation value from ConfigurationManager");
      }

      if (testSimulation)
      {
        RunSimulation();
      }
    }

    private void RunSimulation()
    {
      _level1SignalManager.ProcessAdhData();
    }

    #endregion private methods
  }
}
