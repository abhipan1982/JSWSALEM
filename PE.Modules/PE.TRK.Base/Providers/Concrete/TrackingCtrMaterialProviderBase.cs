using System;
using System.Collections.Generic;
using System.Linq;
using PE.Helpers;
using PE.TRK.Base.Exceptions;
using PE.TRK.Base.Helpers;
using PE.TRK.Base.Models.TrackingEntities;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;
using SMF.Core.Notification;

namespace PE.TRK.Base.Providers.Concrete
{
  public class TrackingCtrMaterialProviderBase : ITrackingCtrMaterialProviderBase
  {
    protected readonly ITrackingStorageProviderBase StorageProvider;
    protected readonly ITrackingEventStorageProviderBase EventStorageProvider;

    public TrackingCtrMaterialProviderBase(ITrackingStorageProviderBase storageProvider,
      ITrackingEventStorageProviderBase eventStorageProvider)
    {
      StorageProvider = storageProvider;
      EventStorageProvider = eventStorageProvider;
    }

    public virtual CtrMaterialBase GetMaterialByPlaceOccupied(int trackingPointAssetCode)
    {
      return StorageProvider.Materials.Values
        .OrderByDescending(x => x.StartDate)
        .FirstOrDefault(x => x.StartDate.HasValue && x.TrackingPoints
          .Any(x => x.HeadReceived && !x.TailReceived && x.AssetCode == trackingPointAssetCode));
    }

    public virtual CtrMaterialBase CreateMaterial()
    {
      CtrMaterial material = BuildMaterial();

      StorageProvider.Materials.AddOrUpdate(material.Id, material, (key, value) => material);

      return material;
    }

    public virtual void SetTrackingPointsForDividedMaterial(CtrMaterialBase parentCtrMaterial, CtrMaterialBase childCtrMaterial, int startingPointAssetCode, DateTime operationDate)
    {
      var currentTrackingPoint = parentCtrMaterial.TrackingPoints.First(x => x.AssetCode == startingPointAssetCode);

      var trackingPointsAfterShear = GetTrackingPointsAfterTrackingPoint(parentCtrMaterial, currentTrackingPoint);
      var trackingPoints = GetTrackingPointsWithHeadReceived(parentCtrMaterial);

      int stepAssetCode = 0;
      var childMaterialId = childCtrMaterial.MaterialInfo.MaterialId;

      try
      {
        foreach (var item in trackingPoints)
        {
          NotificationController.Debug($"Parent tracking points before divide: HEAD: {(item.HeadReceived ? 1 : 0)} | TAIL: {(item.TailReceived ? 1 : 0)}");
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, "Tracking points before divide not logged.");
      }

      foreach (var item in trackingPoints)
      {
        //Set tracking points after shear
        if (trackingPointsAfterShear.Contains(item))
        {
          if (stepAssetCode != item.StepAssetCode)
          {
            stepAssetCode = item.StepAssetCode;
            var parentStep = parentCtrMaterial.Steps
              .First(x => x.AssetCode == stepAssetCode);

            var childStep = childCtrMaterial.Steps
              .First(x => x.AssetCode == stepAssetCode);

            childStep.SetHeadReceivedFirstDate(parentStep.HeadReceivedFirstDate);
            childStep.SetTailReceivedLastDate(parentStep.TailReceivedLastDate);

            if (stepAssetCode != currentTrackingPoint.StepAssetCode)
            {
              parentStep.SetHeadReceivedFirstDate(null);
              parentStep.SetTailReceivedLastDate(null);
            }
          }

          var childTrackingPoint = childCtrMaterial.TrackingPoints
            .First(x => x.AssetCode == item.AssetCode);

          if (item.AssetCode == startingPointAssetCode)
          {
            childTrackingPoint.ChangeHeadReceived(true,
              item.HeadReceivedDate,
              childMaterialId);

            item.ChangeHeadReceived(false,
              null);
          }
          else
          {
            childTrackingPoint.ChangeHeadReceived(item.HeadReceived,
              item.HeadReceivedDate,
              childMaterialId);

            childTrackingPoint.ChangeTailReceived(item.TailReceived,
              item.TailReceivedDate,
              childMaterialId);

            item.ChangeHeadReceived(false, null);

            item.ChangeTailReceived(false, null);
          }
        }
        //Set tracking points before shear
        else
        {
          try
          {
            if (stepAssetCode != item.StepAssetCode)
            {
              stepAssetCode = item.StepAssetCode;
              var parentStep = parentCtrMaterial.Steps
                .First(x => x.AssetCode == stepAssetCode);

              var childStep = childCtrMaterial.Steps
                .First(x => x.AssetCode == stepAssetCode);

              childStep.SetHeadReceivedFirstDate(parentStep.HeadReceivedFirstDate);
              childStep.SetTailReceivedLastDate(parentStep.HeadReceivedFirstDate.HasValue && !parentStep.TailReceivedLastDate.HasValue ?
                operationDate : parentStep.TailReceivedLastDate);
            }

            var childTrackingPoint = childCtrMaterial.TrackingPoints
              .First(x => x.AssetCode == item.AssetCode);

            childTrackingPoint.ChangeHeadReceived(true,
            item.HeadReceivedDate,
            childMaterialId);

            childTrackingPoint.ChangeTailReceived(true,
              item.HeadReceivedDate.HasValue && !item.TailReceivedDate.HasValue ? operationDate : item.TailReceivedDate,
              childMaterialId);
          }
          catch (Exception ex)
          {
            NotificationController.LogException(ex, "Problem while assigning previous points");
          }
        }
      }

      TaskHelper.FireAndForget(() =>
      {
        try
        {
          foreach (var item in trackingPoints)
          {
            NotificationController.Debug($"Parent tracking points: HEAD: {(item.HeadReceived ? 1 : 0)} | TAIL: {(item.TailReceived ? 1 : 0)}");
          }

          foreach (var item in childCtrMaterial.TrackingPoints)
          {
            NotificationController.Debug($"Child tracking points: HEAD: {(item.HeadReceived ? 1 : 0)} | TAIL: {(item.TailReceived ? 1 : 0)}");
          }
        }
        catch (Exception ex)
        {
          NotificationController.LogException(ex, "Tracking points after divide not logged.");
        }
      });
    }

    protected virtual List<TrackingPoint> GetTrackingPointsAfterTrackingPoint(CtrMaterialBase ctrMaterial,
      TrackingPoint startingPoint)
    {
      return ctrMaterial.TrackingPoints
        .Where(x => x.Sequence >= startingPoint.Sequence &&
          x.HeadReceived)
        .ToList();
    }

    protected virtual List<TrackingPoint> GetTrackingPointsWithHeadReceived(CtrMaterialBase ctrMaterial)
    {
      return ctrMaterial.TrackingPoints
        .Where(x => x.HeadReceived)
        .ToList();
    }

    /// <summary>
    ///   MaterialHasOccupiedTheTrackingPoint
    /// </summary>
    /// <param name="trackingPointAssetCode"></param>
    /// <param name="areaAssetCode"></param>
    /// <param name="placeOccupied"></param>
    /// <param name="operationDate"></param>
    /// <returns></returns>
    public virtual CtrMaterialBase MaterialHasOccupiedTheTrackingPoint(int trackingPointAssetCode, int areaAssetCode,
      bool placeOccupied, DateTime operationDate)
    {
      try
      {
        if (StorageProvider.Materials == null ||
            StorageProvider.Materials.Values.Count(wd => !wd.StartDate.HasValue) <= 1)
        {
          CreateMaterial();
        }

        CtrMaterialBase material = placeOccupied
          ? GetMaterialForEventPlaceOccupied(trackingPointAssetCode)
          : GetMaterialForEventPlaceNotOccupied(trackingPointAssetCode);

        if (material != null)
        {
          if (!material.StartDate.HasValue)
          {
            material.SetStartDate(operationDate);
          }

          material.ProcessSingleTrackingPoint(trackingPointAssetCode, placeOccupied, operationDate);
        }

        return material;
      }
      catch (TrackingException ex)
      {
        TrackingLogHelper.LogTrackingException(ex);
        return null;
      }
    }

    /// <summary>
    ///   Remove material from all CTR areas
    /// </summary>
    /// <param name="materialId"></param>
    /// <returns></returns>
    public virtual void RemoveCtrMaterial(long materialId)
    {
      CtrMaterialBase material = GetCtrMaterialById(materialId);

      if (material != null)
      {
        material.RemoveMaterial();
        NotificationController.Warn($"Ctr material with Id: {materialId} was successfully removed");
      }
      else
      {
        NotificationController.Warn($"RemoveCtrMaterial - material with Id: {materialId} not found");
      }
    }

    /// <summary>
    ///   Get last not assigied ctr tracking material
    /// </summary>
    /// <returns>Tracking material</returns>
    public virtual CtrMaterialBase GetLastNotAssignedCtrMaterial()
    {
      return StorageProvider.Materials.Values
        .OrderByDescending(x => x.StartDate)
        .FirstOrDefault(m => m.StartDate.HasValue && m.MaterialInfo?.MaterialId == 0);
    }

    public virtual CtrMaterialBase GetLastVisitedMaterialByPointAssetCode(int pointAssetCode)
    {
      var result = StorageProvider.Materials.Values
        .OrderByDescending(x => x.StartDate)
        .FirstOrDefault(x => x.StartDate.HasValue &&
          x.TrackingPoints
            .Any(tp => tp.AssetCode == pointAssetCode &&
              tp.HeadReceived));

      if (result == null && StorageProvider.LastRemovedMaterial.TrackingPoints.Any(x => x.AssetCode == pointAssetCode && x.HeadReceived))
      {
        return StorageProvider.LastRemovedMaterial;
      }

      return result;
    }

    /// <summary>
    ///   GetMaterialForEvent
    /// </summary>
    /// <param name="trackingPointAssetCode"></param>
    /// <returns></returns>
    protected virtual CtrMaterialBase GetMaterialForEventPlaceOccupied(int trackingPointAssetCode)
    {
      // Getting materials to check
      var materialsToCheck = StorageProvider.Materials.Values
        .Where(wd => wd.StartDate.HasValue)
        .ToList();

      CtrMaterialBase searchedMaterial = null;
      List<CtrMaterialBase> materialsToBeUsed = new List<CtrMaterialBase>();

      foreach (var materialShouldBeRemovedToCheck in materialsToCheck)
      {
        if (materialShouldBeRemovedToCheck.ShouldBeRemoved(out TrackingPointBase lastTrackingPoint, out bool isFinished))
        {
          if (isFinished || (lastTrackingPoint != null && lastTrackingPoint.AssetCode == trackingPointAssetCode))
          {
            materialShouldBeRemovedToCheck.RemoveMaterial();
            continue;
          }
        }

        materialsToBeUsed.Add(materialShouldBeRemovedToCheck);
      }

      materialsToBeUsed = GetMaterialsToBeUsedForPlaceOccupied(trackingPointAssetCode, materialsToBeUsed, out bool signalIgnored);

      if (signalIgnored)
        return null;

      if (materialsToBeUsed.Any())
      {
        searchedMaterial = materialsToBeUsed
          .OrderByDescending(m => m.StartDate)
          .FirstOrDefault(x => x.MaterialInfo.ParentMaterialId.HasValue);

        if (searchedMaterial == null)
          searchedMaterial = materialsToBeUsed.OrderByDescending(m => m.StartDate).FirstOrDefault();
      }
      else
      {
        if (trackingPointAssetCode <= StorageProvider.MaxAssetCodeForNonInitializedMaterialBeingUsed)
        {
          RemoveInvalidStartedMaterials(trackingPointAssetCode);

          return GetNotInitializedNewMaterial();
        }
      }

      return searchedMaterial;
    }

    /// <summary>
    ///   GetMaterialForEvent
    /// </summary>
    /// <param name="trackingPointAssetCode"></param>
    /// <returns></returns>
    protected virtual CtrMaterialBase GetMaterialForEventPlaceNotOccupied(int trackingPointAssetCode)
    {
      // Getting materials to check
      var materialsToCheck = StorageProvider.Materials.Values
        .Where(wd => wd.StartDate.HasValue)
        .ToList();

      CtrMaterialBase searchedMaterial = null;

      List<CtrMaterialBase> materialsToBeUsed = GetMaterialsToBeUsedForPlaceNotOccupied(trackingPointAssetCode, materialsToCheck);

      if (materialsToBeUsed.Any())
      {
        searchedMaterial = materialsToBeUsed.OrderByDescending(m => m.StartDate).FirstOrDefault();
      }

      return searchedMaterial;
    }

    /// <summary>
    ///   Method for build a material
    /// </summary>
    /// <returns></returns>
    protected virtual CtrMaterial BuildMaterial()
    {
      TrackingLogHelper.LogTrackingInformation("Creating new material");
      CtrMaterial material = new CtrMaterial(StorageProvider, EventStorageProvider);

      List<Step> steps = new List<Step>();
      var millTrackingAreas = StorageProvider.TrackingConfiguration.TrackingCtrAreas;

      foreach (var area in millTrackingAreas)
      {
        if (!steps.Any(x => x.AssetCode == area.AreaAssetCode))
        {
          var trackingPoints = new List<TrackingPoint>();

          foreach (var point in area.TrackingPoints)
          {
            if (point.IsShear)
              trackingPoints.Add(new ShearTrackingPoint(point.OccupiedFeatureCode,
                point.AssetCode,
                area.AreaAssetCode,
                point.OrderSeq,
                EventStorageProvider));
            else
              trackingPoints.Add(new TrackingPoint(point.OccupiedFeatureCode,
                point.AssetCode,
                area.AreaAssetCode,
                point.OrderSeq,
                EventStorageProvider));
          }

          material.AddTrackingPoints(trackingPoints);

          steps.Add(new Step(area.AreaAssetCode, EventStorageProvider));
        }
      }

      foreach (var area in millTrackingAreas)
      {
        Step step = steps.First(x => x.AssetCode == area.AreaAssetCode);
        step.AddPreviousSteps(steps
          .Where(x => area.PreviousAreas
          .Any(y => y.AreaAssetCode == x.AssetCode))
          .ToList());
      }

      foreach (Step step in steps)
      {
        material.AddStep(step);
        TrackingLogHelper.LogTrackingInformation($"Step {step.AssetCode} was added to the material");
      }

      return material;
    }

    protected virtual CtrMaterialBase GetNotInitializedNewMaterial()
    {
      return StorageProvider.Materials.Values
        .OrderByDescending(wd => wd.CreatedDate)
        .FirstOrDefault(wd => !wd.StartDate.HasValue);
    }

    protected virtual void RemoveInvalidStartedMaterials(int trackingPointAssetCode)
    {
      StorageProvider.Materials.Values.ToList().ForEach(material =>
      {
        if (material.TrackingPoints.Any(x => x.HeadReceived && !x.TailReceived && x.AssetCode == trackingPointAssetCode))
        {
          NotificationController.Error($"Removed material: {material.MaterialInfo.MaterialId} because of Occupied tracking point: {trackingPointAssetCode}");
          material.RemoveMaterial();
        }
      });
    }

    protected virtual List<CtrMaterialBase> GetMaterialsToBeUsedForPlaceOccupied(int trackingPointAssetCode, List<CtrMaterialBase> materialsToCheck, out bool signalIgnored)
    {
      List<CtrMaterialBase> result = new List<CtrMaterialBase>();
      signalIgnored = false;
      short checkAmount = 3;
      short maxBadAmount = 2;
      foreach (CtrMaterialBase material in materialsToCheck)
      {
        var trackingPointsToCheck = new List<TrackingPoint>();
        int previousBadTpCounter = 0;
        bool isOk = true;

        TrackingPoint currentTrackingPoint = material.TrackingPoints.First(x => x.AssetCode == trackingPointAssetCode);

        if (currentTrackingPoint.HeadReceived && !currentTrackingPoint.TailReceived)
        {
          NotificationController.Warn($"HeadReceived for material: {material.MaterialInfo.MaterialId} for tracking point {trackingPointAssetCode} was already done. Ignoring signal");
          signalIgnored = true;
          break;
        }

        if (currentTrackingPoint.HeadReceived && currentTrackingPoint.TailReceived)
        {
          NotificationController.Warn($"HeadReceived for material: {material.MaterialInfo.MaterialId} for tracking point {trackingPointAssetCode} was already done. Ignoring material from check");
          continue;
        }

        // TODO - ignore such material at all?
        int nextBadTpCounter = material.TrackingPoints.Count(x => x.Sequence > currentTrackingPoint.Sequence && x.HeadReceived);

        if (nextBadTpCounter > maxBadAmount)
        {
          NotificationController.Warn($"For material: {material.MaterialInfo.MaterialId} there are material next with occupied signal");
          continue;
        }

        Step currentStep = material.Steps.First(x => x.AssetCode == currentTrackingPoint.StepAssetCode);

        trackingPointsToCheck.AddRange(material.TrackingPoints
            .Where(x => x.StepAssetCode == currentTrackingPoint.StepAssetCode &&
            x.Sequence < currentTrackingPoint.Sequence)
            .OrderByDescending(x => x.Sequence)
            .Take(checkAmount)
            .ToList());

        if (trackingPointsToCheck.Count < checkAmount && currentStep.PreviousSteps.Any())
        {
          var previousStepAssetCodes = currentStep.PreviousSteps.Select(x => x.AssetCode).ToList();

          var stepToCheck = material.Steps
            .FirstOrDefault(x => previousStepAssetCodes.Contains(x.AssetCode) &&
            x.HeadReceivedFirstDate.HasValue);

          if (stepToCheck != null)
          {
            trackingPointsToCheck.AddRange(material.TrackingPoints
            .Where(x => x.StepAssetCode == stepToCheck.AssetCode &&
              x.Sequence < currentTrackingPoint.Sequence)
            .OrderByDescending(x => x.Sequence)
            .Take(checkAmount - trackingPointsToCheck.Count)
            .ToList());
          }

          if (stepToCheck == null && previousStepAssetCodes.Any())
          {
            NotificationController.Warn($"Found missing previous area processing for Place Occupied: {trackingPointAssetCode}");
            continue;
          }
        }

        foreach (TrackingPoint tp in trackingPointsToCheck)
        {
          if (previousBadTpCounter >= maxBadAmount)
          {
            isOk = false;

            break;
          }

          if (tp.HeadReceived)
          {
            break;
          }

          if (!tp.HeadReceived)
          {
            previousBadTpCounter++;
          }
        }

        if (!isOk)
        {
          NotificationController.Warn($"For material: {material.MaterialInfo.MaterialId} there are too many wrong signals");
          continue;
        }

        result.Add(material);
      }

      return result;
    }

    protected virtual List<CtrMaterialBase> GetMaterialsToBeUsedForPlaceNotOccupied(int trackingPointAssetCode, List<CtrMaterialBase> materialsToCheck)
    {
      List<CtrMaterialBase> result = new List<CtrMaterialBase>();

      short checkAmount = 3;
      //short maxBadAmount = 2;
      foreach (CtrMaterialBase material in materialsToCheck)
      {
        var trackingPointsToCheck = new List<TrackingPoint>();
        //int previousBadTpCounter = 0;
        //bool isOk = true;

        TrackingPoint currentTrackingPoint = material.TrackingPoints.First(x => x.AssetCode == trackingPointAssetCode);

        if (!currentTrackingPoint.HeadReceived)
        {
          NotificationController.Warn($"For material {material.MaterialInfo.MaterialId}: Not Occupied signal for tracking point {trackingPointAssetCode} cannot be proceed without HeadReceived");
          continue;
        }

        if (currentTrackingPoint.TailReceived)
        {
          NotificationController.Warn($"For material {material.MaterialInfo.MaterialId}: Not Occupied signal for tracking point {trackingPointAssetCode} was already done");
          continue;
        }

        //// TODO - ignore such material at all?
        //int nextBadTpCounter = material.TrackingPoints.Count(x => x.Sequence > currentTrackingPoint.Sequence && x.TailReceived);

        //if (nextBadTpCounter > maxBadAmount)
        //{
        //  NotificationController.Warn($"For material: {material.MaterialInfo.MaterialId} there are material next with not occupied signal");
        //  continue;
        //}

        Step currentStep = material.Steps.First(x => x.AssetCode == currentTrackingPoint.StepAssetCode);

        trackingPointsToCheck.AddRange(material.TrackingPoints
            .Where(x => x.StepAssetCode == currentTrackingPoint.StepAssetCode &&
            x.Sequence < currentTrackingPoint.Sequence)
            .OrderByDescending(x => x.Sequence)
            .Take(checkAmount)
            .ToList());

        if (trackingPointsToCheck.Count < checkAmount && currentStep.PreviousSteps.Any())
        {
          var previousStepAssetCodes = currentStep.PreviousSteps.Select(x => x.AssetCode).ToList();

          var stepToCheck = material.Steps
            .FirstOrDefault(x => previousStepAssetCodes.Contains(x.AssetCode));

          if (stepToCheck != null)
          {
            trackingPointsToCheck.AddRange(material.TrackingPoints
            .Where(x => x.StepAssetCode == stepToCheck.AssetCode &&
              x.Sequence < currentTrackingPoint.Sequence)
            .OrderByDescending(x => x.Sequence)
            .Take(checkAmount - trackingPointsToCheck.Count)
            .ToList());
          }

          if (stepToCheck == null && previousStepAssetCodes.Any())
          {
            NotificationController.Warn($"Found missing previous area processing for Place not Occupied: {trackingPointAssetCode}");
            continue;
          }
        }

        //foreach (TrackingPoint tp in trackingPointsToCheck)
        //{
        //  if (previousBadTpCounter >= maxBadAmount)
        //  {
        //    isOk = false;

        //    break;
        //  }

        //  if (tp.TailReceived)
        //  {
        //    break;
        //  }

        //  if (!tp.TailReceived)
        //  {
        //    previousBadTpCounter++;
        //  }
        //}

        //if (!isOk)
        //{
        //  NotificationController.Warn($"For material: {material.MaterialInfo.MaterialId} there are too many wrong signals");
        //  continue;
        //}

        result.Add(material);
      }

      return result;
    }

    /// <summary>
    ///   Get ctr tracking material by Id
    /// </summary>
    /// <param name="materialId"></param>
    /// <returns>Tracking material</returns>
    protected virtual CtrMaterialBase GetCtrMaterialById(long materialId)
    {
      CtrMaterialBase material =
        StorageProvider.Materials.Values.FirstOrDefault(m => m.MaterialInfo.MaterialId == materialId);

      return material;
    }

    /// <summary>
    ///   Get first assigned ctr material
    /// </summary>
    /// <returns>Tracking material</returns>
    public virtual CtrMaterialBase GetMaterialOnTrackingPointByAreaAssetCode(int assetCode)
    {
      CtrMaterialBase result = null;

      StorageProvider.Materials.Values
        .Where(x => x.StartDate.HasValue)
        .OrderBy(x => x.StartDate)
        .ToList()
        .ForEach(material =>
        {
          if (result is not null)
            return;

          foreach (var item in material.TrackingPoints.Where(x => x.StepAssetCode == assetCode).OrderByDescending(x => x.Sequence))
          {
            if (material.TrackingPoints.Any(x => x.AssetCode == item.AssetCode && x.HeadReceived && !x.TailReceived))
            {
              result = material;
              break;
            }
          }
        });

      return result;
    }

    /// <summary>
    ///   Get first material before selected tracking point
    /// </summary>
    /// <returns>Tracking material</returns>
    public virtual CtrMaterialBase GetMaterialBeforePointByPointAssetCode(int assetCode)
    {
      CtrMaterialBase result = null;

      StorageProvider.Materials.Values
        .Where(x => x.StartDate.HasValue)
        .OrderBy(x => x.StartDate)
        .ToList()
        .ForEach(material =>
        {
          if (result is not null)
            return;

          var assetOrderSeq = material.TrackingPoints.First(x => x.AssetCode == assetCode).Sequence;
          foreach (var item in material.TrackingPoints.Where(x => x.Sequence < assetOrderSeq).OrderByDescending(x => x.Sequence))
          {
            if (material.TrackingPoints.Any(x => x.AssetCode == item.AssetCode && x.HeadReceived && !x.TailReceived))
            {
              result = material;
              break;
            }
          }
        });

      return result;
    }
  }
}
