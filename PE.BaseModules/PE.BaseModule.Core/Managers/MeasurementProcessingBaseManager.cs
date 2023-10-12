using System;
using System.Threading.Tasks;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseInterfaces.Managers;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.Core.Cache;
using PE.Module.Core.Handlers;
using SMF.Core.DC;
using SMF.Core.Infrastructure;
using SMF.Core.Interfaces;
using SMF.Core.Notification;

namespace PE.Module.Core.Managers
{
  public class MeasurementProcessingBaseManager : BaseManager, IMeasurementProcessingBaseManager
  {
    #region members

    protected readonly MeasurementsHandler MeasurementsHandler;
    protected readonly CircularBuffer<DcMeasData> ConsumptionMeasurementsCache;

    #endregion

    public MeasurementProcessingBaseManager(IModuleInfo moduleInfo) : base (moduleInfo)
    {
      MeasurementsHandler = new MeasurementsHandler();
      ConsumptionMeasurementsCache = new CircularBuffer<DcMeasData>(100);
    }
    
    #region measurement processing

    public virtual async Task<DataContractBase> ProcessSingleMeasurementAsync(DcMeasData message)
    {
      try
      {
        using (PEContext ctx = new PEContext())
        {
          MeasurementsHandler.SaveMeasurement(ctx, message);
          CacheMeasurement(message);

          await ctx.SaveChangesAsync();
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, "Exception while processing measurements ");
      }

      return new DataContractBase();
    }

    public virtual async Task<DataContractBase> ProcessAggregatedMeasurementsAsync(DCMeasDataAggregated message)
    {
      try
      {
        using (PEContext ctx = new PEContext())
        {
          foreach (DcMeasData measurement in message.Measurements)
          {
            measurement.RawMaterialId = message.RawMaterialId;

            MeasurementsHandler.SaveMeasurement(ctx, measurement);
            CacheMeasurement(measurement);
          }

          await ctx.SaveChangesAsync();
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, "Exception while processing measurements ");
      }

      return new DataContractBase();
    }

    public virtual async Task<DataContractBase> ProcessSingleMeasurementAsync(DcMeasDataSample message)
    {
      try
      {
        using (PEContext ctx = new PEContext())
        {
          MVHMeasurement dbMeasurement = MeasurementsHandler.SaveMeasurement(ctx, message);
          await ctx.SaveChangesAsync();
          MeasurementsHandler.SaveMeasurementSamples(ctx, dbMeasurement, message);

          await ctx.SaveChangesAsync();
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, "Exception while single measurement with samples");
      }

      return new DataContractBase();
    }

    public virtual async Task<DataContractBase> ProcessAggregatedMeasurementsAsync(DCMeasDataAggregatedSample message)
    {
      try
      {
        using (PEContext ctx = new PEContext())
        {
          foreach (DcMeasDataSample measurement in message.Measurements)
          {
            try
            {
              measurement.RawMaterialId = message.RawMaterialId;

              MVHMeasurement dbMeasurement = MeasurementsHandler.SaveMeasurement(ctx, measurement);
              await ctx.SaveChangesAsync();
              MeasurementsHandler.SaveMeasurementSamples(ctx, dbMeasurement, measurement);
            }
            catch (Exception ex)
            {
              NotificationController.LogException(ex, $"Something went wrong while save measurement with samples for {measurement.FeatureCode}");
            }
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, "Exception while single measurement with samples");
      }

      return new DataContractBase();
    }

    #endregion

    protected virtual void CacheMeasurement(DcMeasData measurement)
    {
      if (measurement.ShouldCached)
      {
        ConsumptionMeasurementsCache.PushBack(measurement);
      }
    }
  }
}
