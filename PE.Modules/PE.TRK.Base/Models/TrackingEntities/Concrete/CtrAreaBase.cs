using System;
using PE.BaseDbEntity.EnumClasses;
using PE.TRK.Base.Managers;
using PE.TRK.Base.Models._Base;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using SMF.Core.Notification;

namespace PE.TRK.Base.Models.TrackingEntities.Concrete
{
  public class CtrAreaBase : TrackingProcessingAreaBase
  {
    public (int FeatureCode, bool Value) AreaModeProduction { get; protected set; }
    public (int FeatureCode, bool Value) AreaModeAdjustion { get; protected set; }
    public (int FeatureCode, bool Value) AreaSimulation { get; protected set; }
    public (int FeatureCode, bool Value) AreaAutomaticRelease { get; protected set; }
    public (int FeatureCode, bool Value) AreaEmpty { get; protected set; }
    public (int FeatureCode, bool Value) AreaCobbleDetected { get; protected set; }
    public (int FeatureCode, bool Value) AreaModeLocal { get; protected set; }
    public (int FeatureCode, bool Value) AreaCobbleDetectionSelected { get; protected set; }

    public CtrAreaBase(int assetCode,
      int areaModeProductionFeatureCode,
      int areaModeAdjustionFeatureCode,
      int areaSimulationSimulationFeatureCode,
      int areaAutomaticReleaseFeatureCode,
      int areaEmptyFeatureCode,
      int areaCobbleDetectedFeatureCode,
      int areaModeLocalFeatureCode,
      int areaCobbleDetectionSelectedFeatureCode)
      : base(assetCode)
    {
      AreaModeProduction = (areaModeProductionFeatureCode, false);
      AreaModeAdjustion = (areaModeAdjustionFeatureCode, false);
      AreaSimulation = (areaSimulationSimulationFeatureCode, false);
      AreaAutomaticRelease = (areaAutomaticReleaseFeatureCode, false);
      AreaEmpty = (areaEmptyFeatureCode, false);
      AreaCobbleDetected = (areaCobbleDetectedFeatureCode, false);
      AreaModeLocal = (areaModeLocalFeatureCode, false);
      AreaCobbleDetectionSelected = (areaCobbleDetectionSelectedFeatureCode, false);
    }


    public override ITrackingInstructionDataContractBase ProcessInstruction(ITrackingInstructionRequest request)
    {
      bool processedValue = Convert.ToBoolean(request.Value);
      //TODO Display new area attributes on HMI
      switch (request.InstructionType)
      {
        case var instructionType when instructionType == TrackingInstructionType.ModeProduction:
          AreaModeProduction = (AreaModeProduction.FeatureCode, processedValue);
          break;
        case var instructionType when instructionType == TrackingInstructionType.ModeAdjustion:
          AreaModeAdjustion = (AreaModeAdjustion.FeatureCode, processedValue);
          break;
        case var instructionType when instructionType == TrackingInstructionType.Simulation:
          AreaSimulation = (AreaSimulation.FeatureCode, processedValue);
          //TODOMN if change to false - remove materials
          break;
        case var instructionType when instructionType == TrackingInstructionType.AutomaticRelease:
          AreaAutomaticRelease = (AreaAutomaticRelease.FeatureCode, processedValue);
          break;
        case var instructionType when instructionType == TrackingInstructionType.Empty:
          AreaEmpty = (AreaEmpty.FeatureCode, processedValue);
          break;
        case var instructionType when instructionType == TrackingInstructionType.CobbleDetected:
          if (!AreaCobbleDetected.Value && processedValue)            
            NotificationController.RegisterAlarm(AlarmDefsBase.CobbleDetected,
              $"Cobble detected in area with code {TrackingArea.GetValue(AreaAssetCode)} [{AreaAssetCode}].",
              $"{TrackingArea.GetValue(AreaAssetCode).Name} [{AreaAssetCode}]");
          AreaCobbleDetected = (AreaCobbleDetected.FeatureCode, processedValue);
          break;
        case var instructionType when instructionType == TrackingInstructionType.ModeLocal:
          AreaModeLocal = (AreaModeLocal.FeatureCode, processedValue);
          break;
        case var instructionType when instructionType == TrackingInstructionType.CobbleDetectionSelected:
          AreaCobbleDetectionSelected = (AreaCobbleDetectionSelected.FeatureCode, processedValue);
          break;
      }

      return new TrackingInstructionDataContractBase();
    }
  }
}
