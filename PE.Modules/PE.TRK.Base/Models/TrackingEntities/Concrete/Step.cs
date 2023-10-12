using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.BaseDbEntity.EnumClasses;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Base.Models.TrackingEntities
{
  public class Step : TrackingStepBase
  { 
    #region ctor

    public Step(int assetCode, 
      ITrackingEventStorageProviderBase eventStorageProvider) 
      : base(assetCode, eventStorageProvider)
    {
    }

    #endregion

    #region Properties
    public bool IsActiveForProcessing { get; private set; }
    public DateTime? HeadReceivedFirstDate { get; private set; }
    public DateTime? TailReceivedLastDate { get; private set; }
    public List<Step> PreviousSteps { get; private set; } = new List<Step>();
    public bool IsLastStep { get; private set; }

    #endregion

    #region public methods

    /// <summary>
    ///   SetHeadReceivedFirstDate
    /// </summary>
    /// <param name="date"></param>
    public void SetHeadReceivedFirstDate(DateTime? date, long? materialId = null)
    {
      HeadReceivedFirstDate = date;

      if(materialId.HasValue && date.HasValue)
        EventStorageProvider.TrackingPointEventsToBeProcessed
          .Enqueue(new TrackingEventArgs(materialId.Value, AssetCode, true, TrackingEventType.Enter, date.Value));
    }

    /// <summary>
    ///   SetTailReceivedLastDate
    /// </summary>
    /// <param name="date"></param>
    public void SetTailReceivedLastDate(DateTime? date, long? materialId = null)
    {
      TailReceivedLastDate = date;

      if (materialId.HasValue && date.HasValue)
        EventStorageProvider.TrackingPointEventsToBeProcessed
          .Enqueue(new TrackingEventArgs(materialId.Value, AssetCode, true, TrackingEventType.Exit, date.Value));
    }

    public void AddPreviousSteps(List<Step> previousSteps)
    {
      PreviousSteps.AddRange(previousSteps);
    }

    #endregion
  }
}
