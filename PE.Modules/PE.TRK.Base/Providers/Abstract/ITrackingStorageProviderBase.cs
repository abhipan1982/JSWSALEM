using System.Collections.Concurrent;
using System.Collections.Generic;
using PE.TRK.Base.Models._Base;
using PE.TRK.Base.Models.Configuration.Concrete;
using PE.TRK.Base.Models.TrackingComponents.Collections.Abstract;
using PE.TRK.Base.Models.TrackingComponents.Collections.Concrete;
using PE.TRK.Base.Models.TrackingEntities.Abstract;

namespace PE.TRK.Base.Providers.Abstract
{
  public interface ITrackingStorageProviderBase
  {
    ConcurrentDictionary<string, CtrMaterialBase> Materials { get; set; }

    CtrMaterialBase LastRemovedMaterial { get; set; }

    TrackingConfigurationBase TrackingConfiguration { get; set; }

    /// <summary>
    /// AssetCode is a key, AssetBase is a value
    /// </summary>
    Dictionary<int, AssetBase> AssetsDictionary { get; set; }


    Dictionary<int, TrackingProcessingAreaBase> TrackingAreas { get; set; }

    Dictionary<int, List<TrackingInstruction>> TrackingInstructionsDictionary { get; set; }

    CtrGreyListArea CtrGreyArea { get; }

    TransportListArea Transport { get; }

    TrackingUnChargeableUnDischargeableNonPositionRelatedListAbstractBase ChargingGrid { get; }

    TrackingNonPositionRelatedListAbstractBase EnterTable { get; }

    TrackingNonPositionRelatedListAbstractBase Garret { get; }

    FurnaceBase Furnace { get; }

    Rake Rake { get; }

    long MaxAssetCodeForNonInitializedMaterialBeingUsed { get; set; }

    double WeightLossFactor { get; set; }

    bool ProcessOCR { get; set; }

    int DischargeTemperatureFeatureCode { get; set; }

    int DischargeTemperatureAreaAssetCode { get; set; }
  }
}
