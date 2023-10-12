using System.Linq;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.PEContext;
using PE.BaseInterfaces.Managers;
using PE.L1A.Base.Models;
using PE.L1A.Base.Providers.Abstract;
using SMF.Core.Notification;

namespace PE.L1A.Base.Managers
{
  public class PlantConfigurationManagerBase : IPlantConfigurationManagerBase
  {
    protected readonly IConfigurationStorageProviderBase ConfigurationStorageProvider;

    public PlantConfigurationManagerBase(IConfigurationStorageProviderBase configurationStorageProvider)
    {
      ConfigurationStorageProvider = configurationStorageProvider;
    }

    public void ReadPlantConfiguration()
    {
      using var ctx = new PEContext();

      var trackingFeatures = ctx.MVHFeatures
        .Include(x => x.InverseFKParentFeature)
        .Where(x => x.IsTrackingPoint && x.EnumCommChannelType != CommChannelType.Undefined)
        .ToList();

      /* Example
       ConfigurationStorageProvider.L1ConfigurationPoints.Add(62002,
        new OpcConfigurationPoint(ConfigurationPointType.TrackingPoint,
        120,
        62002,
        "opc.tcp://148.56.68.99:4840",
        $"ns=3;s=\"ROD_HMI\".\"AREA\".\"ATD\""));

      ConfigurationStorageProvider.FeatureConfigurationPointsByParentFeatureCode
        .Add(62022, new List<FeatureConfigurationPointBase>()
        { 
          new FeatureConfigurationPointBase(ConfigurationPointType.MeasurementPoint,
          62065,
          true, 
          0, 
          100, 
          true, 
          33, 
          AggregationStrategy.FloatingPoint)
        });
       */

      foreach (var trackingFeature in trackingFeatures)
      {
        ConfigurationStorageProvider.L1ConfigurationPoints.Add(trackingFeature.FeatureCode,
          new OpcConfigurationPoint(ConfigurationPointType.TrackingPoint,
            trackingFeature.FeatureCode,
            trackingFeature.RetentionFactor ?? 120,
            (long?)(trackingFeature.SampleOffsetTime * 1000) ?? 50, // in database we store in [s] in code should be in [ms]
            trackingFeature.CommAttr1,
            trackingFeature.CommAttr2));

        if (trackingFeature.InverseFKParentFeature.Any())
        {
          ConfigurationStorageProvider.FeatureConfigurationPointsByParentFeatureCode
          .Add(trackingFeature.FeatureCode, trackingFeature.InverseFKParentFeature
          .Where(x => x.IsMeasurementPoint)
          .Select(x =>
            new FeatureConfigurationPointBase(ConfigurationPointType.MeasurementPoint,
            x.FeatureCode,
            x.IsSampledFeature,
            (long?)(x.SampleOffsetTime * 1000) ?? 200,
            x.MinValue,
            x.MaxValue,
            x.EnumFeatureType == FeatureType.MeasuringSpeed,
            x.FKExtUnitOfMeasureId,
            x.EnumAggregationStrategy,
            x.IsLengthRelated))
          .ToList());
        }
      }

      var measuringFeatures = ctx.MVHFeatures
        .Where(x => x.IsMeasurementPoint && x.EnumCommChannelType != CommChannelType.Undefined)
        .ToList();

      /* Example
      ConfigurationStorageProvider.L1ConfigurationPoints.Add(62065,
       new OpcConfigurationPoint(ConfigurationPointType.MeasurementPoint,
       120,
       62065,
       "opc.tcp://148.56.68.99:4840",
       $"ns=3;s=\"ROD_HMI\".\"PR_REN\".\"HMI_STAT\".\"MSA\""));
       */
      bool logTimeOffset = true;
      foreach (var measuringFeature in measuringFeatures)
      {
        if (logTimeOffset)
        {
          NotificationController.Warn($"Logged timeOffset: {(long?)(measuringFeature.SampleOffsetTime * 1000) ?? 50}");
          logTimeOffset = false;
        }

        ConfigurationStorageProvider.FeatureConfigurationPointDictionary
          .Add(measuringFeature.FeatureCode, 
            new FeatureConfigurationPointBase(ConfigurationPointType.MeasurementPoint,
            measuringFeature.FeatureCode,
            measuringFeature.IsSampledFeature,
            (long?)(measuringFeature.SampleOffsetTime * 1000) ?? 200,
            measuringFeature.MinValue,
            measuringFeature.MaxValue,
            measuringFeature.EnumFeatureType == FeatureType.MeasuringSpeed,
            measuringFeature.FKExtUnitOfMeasureId,
            measuringFeature.EnumAggregationStrategy,
            measuringFeature.IsLengthRelated));


        ConfigurationStorageProvider.L1ConfigurationPoints.Add(measuringFeature.FeatureCode,
          new OpcConfigurationPoint(ConfigurationPointType.MeasurementPoint,
            measuringFeature.FeatureCode,
            measuringFeature.RetentionFactor ?? 120,
            (long?)(measuringFeature.SampleOffsetTime * 1000) ?? 50, // in database we store in [s] in code should be in [ms]
            measuringFeature.CommAttr1,
            measuringFeature.CommAttr2));
      }

      ConfigurationStorageProvider.L1MillControlDatas = ctx.TRKMillControlDatas
        .Where(x => x.EnumCommChannelType != CommChannelType.Undefined)
        .Select(x => new L1MillControlDataBase()
        {
          Code = x.MillControlCode,
          EnumCommChannelType = x.EnumCommChannelType,
          CommAttr1 = x.CommAttr1,
          CommAttr2 = x.CommAttr2,
          CommAttr3 = x.CommAttr3,
        })
        .ToDictionary(x => x.Code, y => y);

      ConfigurationStorageProvider.CalculatedFeatures = ctx.MVHFeatureCalculateds
        .Include(x => x.FKFeature)
        .Include(x => x.FKFeatureId_1Navigation)
        .Include(x => x.FKFeatureId_2Navigation)
        .Select(x => new FeatureCalculatedPointBase()
        {
          CalculatedFeatureCode = x.FKFeature.FeatureCode,
          CalculatedValue = x.CalculatedValue,
          IsVirtual = x.IsVirtual,
          Seq = x.Seq,
          IsResendEnabled = false, //x.FKFeature.IsResendEnabled,
          FeatureCode_1 = x.FKFeatureId_1Navigation.FeatureCode,
          Value_1 = x.Value_1,
          EnumCompareOperator_1 = x.EnumCompareOperator_1,
          EnumLogicalOperator = x.EnumLogicalOperator,
          FeatureCode_2 = x.FKFeatureId_2 != null ? x.FKFeatureId_2Navigation.FeatureCode : null,
          Value_2 = x.Value_2,
          EnumCompareOperator_2 = x.EnumCompareOperator_2,
          TimeFilter = x.TimeFilter.HasValue ? (int)(x.TimeFilter.Value * 1000) : null, // convert [s] to [ms]
          EnumLogicalOperator_ForNextSequence = x.EnumLogicalOperator_ForNextSequence
        })
        .ToList();
    }
  }
}
