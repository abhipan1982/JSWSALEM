using System;
using PE.TRK.Base.Models.TrackingEntities.Abstract;

namespace PE.TRK.Base.Providers.Abstract
{
  public interface ITrackingCtrMaterialProviderBase
  {
    CtrMaterialBase GetMaterialByPlaceOccupied(int trackingPointAssetCode);
    CtrMaterialBase CreateMaterial();
    void RemoveCtrMaterial(long materialId);
    CtrMaterialBase MaterialHasOccupiedTheTrackingPoint(int trackingPointAssetCode, int areaAssetCode,
      bool placeOccupied, DateTime operationDate);

    CtrMaterialBase GetLastNotAssignedCtrMaterial();

    CtrMaterialBase GetMaterialOnTrackingPointByAreaAssetCode(int assetCode);

    CtrMaterialBase GetMaterialBeforePointByPointAssetCode(int assetCode);

    CtrMaterialBase GetLastVisitedMaterialByPointAssetCode(int pointAssetCode);

    void SetTrackingPointsForDividedMaterial(CtrMaterialBase parentCtrMaterial, CtrMaterialBase childCtrMaterial, int value, DateTime operationDate);
  }
}
