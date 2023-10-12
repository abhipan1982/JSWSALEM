using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.PEContext;
using PE.TRK.Base.Managers.Abstract;
using PE.TRK.Base.Models._Base;
using PE.TRK.Base.Models.Configuration.Concrete;
using PE.TRK.Base.Providers.Abstract;
using SMF.Core.Interfaces;

namespace PE.TRK.Base.Managers.Concrete
{
  public class TrackingConfigurationManagerBase : ITrackingConfigurationManagerBase
  {
    protected readonly ITrackingStorageProviderBase TrackingStorageProvider;
    protected readonly ITrackingEventStorageProviderBase TrackingEventStorageProvider;
    public TrackingConfigurationManagerBase(ITrackingStorageProviderBase trackingStorageProvider,
      ITrackingEventStorageProviderBase trackingEventStorageProvider,
      IParameterService parameterService)
    {
      TrackingStorageProvider = trackingStorageProvider;
      TrackingEventStorageProvider = trackingEventStorageProvider;

      trackingStorageProvider.MaxAssetCodeForNonInitializedMaterialBeingUsed = parameterService.GetParameter("TRK_MaxAssetCodeForNonInitializedMaterialBeingUsed")?.ValueInt ?? 0;
      trackingStorageProvider.WeightLossFactor = parameterService.GetParameter("WeightLossFactor")?.ValueFloat ?? (double)0.97;
      trackingStorageProvider.ProcessOCR = parameterService.GetParameter("TRK_ProcessOCR")?.ValueInt == 1;
      trackingStorageProvider.DischargeTemperatureAreaAssetCode = parameterService.GetParameter("TRK_DischargeTemperatureAreaAssetCode")?.ValueInt ?? 0;
      trackingStorageProvider.DischargeTemperatureFeatureCode = parameterService.GetParameter("TRK_DischargeTemperatureFeatureCode")?.ValueInt ?? 0;
      trackingEventStorageProvider.LayingHeadAssetCode = parameterService.GetParameter("TRK_LayingHeadAssetCode")?.ValueInt ?? 0;
    }

    public void Init()
    {
      using PEContext ctx = new PEContext();

      var features = ctx.MVHFeatures.ToList();

      TrackingStorageProvider.AssetsDictionary = ctx.MVHAssets
        .ToDictionary(x => x.AssetCode, y => new AssetBase()
        {
          AssetId = y.AssetId,
          AssetCode = y.AssetCode,
          AssetName = y.AssetName,
          FeaturesDictionary = features
          .ToDictionary(fk => fk.FeatureCode, fv => new FeatureBase()
          {
            FeatureCode = fv.FeatureCode,
            FeatureId = fv.FeatureId,
            FeatureType = fv.EnumFeatureType
          })
        });

      var trackingInstructions = ctx.TRKTrackingInstructions
        .Include(x => x.FKAreaAsset)
        .Include(x => x.FKFeature)
        .Include(x => x.FKPointAsset)
        .Select(x => new TrackingInstruction
        {
          InstructionId = x.TrackingInstructionId,
          TrackingInstructionType = (TrackingInstructionType)x.EnumTrackingInstructionType,
          AreaAssetCode = x.FKAreaAsset.AssetCode,
          FeatureCode = x.FKFeature.FeatureCode,
          TrackingInstructionValue = x.TrackingInstructionValue,
          PointAssetCode = x.FKPointAssetId == null ? (int?)null : x.FKPointAsset.AssetCode,
          SeqNo = x.SeqNo,
          IgnoreIfSimulation = x.IsIgnoredIfSimulation,
          ParentInstructionId = x.FKParentTrackingInstructionId,
          TimeFilter = x.TimeFilter ?? 0,
          IsProcessedDuringAdjustment = x.IsProcessedDuringAdjustment.Value
        })
        .ToList();

      foreach (var instruction in trackingInstructions)
      {
        if (instruction.ParentInstructionId.HasValue)
        {
          var parentMaterial = trackingInstructions.First(x => x.InstructionId == instruction.ParentInstructionId.Value);

          parentMaterial.ChildInstructions.Add(instruction);
          instruction.ParentInstruction = parentMaterial;
        }
      }


      TrackingStorageProvider.TrackingInstructionsDictionary = trackingInstructions.GroupBy(x => x.FeatureCode).ToDictionary(x => x.Key, y => y.ToList());

      var trackingConfiguration = new TrackingConfigurationBase();
      trackingConfiguration.TrackingCollectionAreas = ctx.MVHAssets.Where(x => x.IsArea &&
      x.IsTrackingPoint == true &&
      x.EnumTrackingAreaType == TrackingAreaType.Collection)
        .Select(x =>
          new ConfigurationCollectionAreaBase(x.AssetCode, x.IsPositionBased, x.PositionsNumber ?? 0, x.VirtualPositionsNumber ?? 0))
        .ToList();

      var layouts = ctx.MVHAssetLayouts.Select(x => new
      {
        PrevAssetCode = x.FKPrevAsset.AssetCode,
        NextAssetCode = x.FKNextAsset.AssetCode
      })
      .ToList();

      // REFACTOR this
      // ADD previous area

      trackingConfiguration.TrackingCtrAreas = ctx.MVHAssets
        .Include(x => x.MVHFeatures)
        .Include(x => x.FKAssetType)
        .Where(x => x.IsArea &&
          x.IsTrackingPoint == true &&
          x.EnumTrackingAreaType == TrackingAreaType.CTR)
        .OrderBy(x => x.OrderSeq)
        .Select(x => new ConfigurationCtrAreaBase(x.AssetCode,
          x.MVHFeatures.Where(y => y.EnumFeatureType == FeatureType.TrackingAreaModeProduction).Select(y => y.FeatureCode).First(),
          x.MVHFeatures.Where(y => y.EnumFeatureType == FeatureType.TrackingAreaModeAdjustion).Select(y => y.FeatureCode).First(),
          x.MVHFeatures.Where(y => y.EnumFeatureType == FeatureType.TrackingAreaSimulation).Select(y => y.FeatureCode).First(),
          x.MVHFeatures.Where(y => y.EnumFeatureType == FeatureType.TrackingAreaAutomaticRelease).Select(y => y.FeatureCode).First(),
          x.MVHFeatures.Where(y => y.EnumFeatureType == FeatureType.TrackingAreaEmpty).Select(y => y.FeatureCode).First(),
          x.MVHFeatures.Where(y => y.EnumFeatureType == FeatureType.TrackingAreaCobbleDetected).Select(y => y.FeatureCode).First(),
          x.MVHFeatures.Where(y => y.EnumFeatureType == FeatureType.TrackingAreaModeLocal).Select(y => y.FeatureCode).First(),
          x.MVHFeatures.Where(y => y.EnumFeatureType == FeatureType.TrackingAreaCobbleDetectionSelected).Select(y => y.FeatureCode).First(),
          new List<BaseCtrAreaBase>(),
          x.InverseFKParentAsset
          .OrderBy(x => x.OrderSeq)
          .Where(x => x.MVHFeatures.Any(y => y.EnumFeatureType == FeatureType.TrackingOccupied))
          .Select(y =>
            new ConfigurationTrackingPointBase(y.MVHFeatures
              .First(x => x.EnumFeatureType == FeatureType.TrackingOccupied).FeatureCode,
              y.AssetCode,
              y.OrderSeq,
              y.FKAssetType.AssetTypeCode == "SH" || y.FKAssetType.AssetTypeCode == "DIVSH"))
          .ToList()
        ))
        .ToList();

      foreach (var ctrArea in trackingConfiguration.TrackingCtrAreas)
      {
        var list = new List<BaseCtrAreaBase>();

        var prevAreaAssetCodes = layouts
          .Where(x => x.NextAssetCode == ctrArea.AreaAssetCode)
          .Select(x => x.PrevAssetCode)
          .Distinct()
          .ToList();

        list.AddRange(trackingConfiguration.TrackingCtrAreas.Where(x => prevAreaAssetCodes.Contains(x.AreaAssetCode)));
        ctrArea.SetPreviousAreas(list);
      }

      TrackingStorageProvider.TrackingConfiguration = trackingConfiguration;
    }
  }
}
