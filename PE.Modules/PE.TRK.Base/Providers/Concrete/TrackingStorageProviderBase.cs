using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.TRK.Base.Models._Base;
using PE.TRK.Base.Models.Configuration.Concrete;
using PE.TRK.Base.Models.TrackingComponents.Collections.Abstract;
using PE.TRK.Base.Models.TrackingComponents.Collections.Concrete;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Base.Providers.Concrete
{
  public class TrackingStorageProviderBase : ITrackingStorageProviderBase
  {
    public TrackingStorageProviderBase()
    {
    }

    public ConcurrentDictionary<string, CtrMaterialBase> Materials { get; set; } = new ConcurrentDictionary<string, CtrMaterialBase>();

    public CtrMaterialBase LastRemovedMaterial { get; set; }

    public TrackingConfigurationBase TrackingConfiguration { get; set; } = new TrackingConfigurationBase();

    /// <summary>
    /// AssetCode is a key, AssetBase is a value
    /// </summary>
    public Dictionary<int, AssetBase> AssetsDictionary { get; set; } = new Dictionary<int, AssetBase>();

    public Dictionary<int, TrackingProcessingAreaBase> TrackingAreas { get; set; } = new Dictionary<int, TrackingProcessingAreaBase>();

    public Dictionary<int, List<TrackingInstruction>> TrackingInstructionsDictionary { get; set; } = new Dictionary<int, List<TrackingInstruction>>();

    public CtrGreyListArea CtrGreyArea => TrackingAreas.First(x => x.Value is CtrGreyListArea).Value as CtrGreyListArea;
    public TransportListArea Transport => TrackingAreas.First(x => x.Value is TransportListArea).Value as TransportListArea;

    public TrackingUnChargeableUnDischargeableNonPositionRelatedListAbstractBase ChargingGrid => TrackingAreas.First(x => x.Value.AreaAssetCode == TrackingArea.CHG_AREA).Value as TrackingUnChargeableUnDischargeableNonPositionRelatedListAbstractBase;

    public TrackingNonPositionRelatedListAbstractBase EnterTable => TrackingAreas.First(x => x.Value.AreaAssetCode == TrackingArea.ENTER_TABLE_AREA).Value as TrackingNonPositionRelatedListAbstractBase;

    public TrackingNonPositionRelatedListAbstractBase Garret => TrackingAreas.First(x => x.Value.AreaAssetCode == TrackingArea.GARRET_AREA).Value as TrackingNonPositionRelatedListAbstractBase;

    public FurnaceBase Furnace => TrackingAreas.First(x => x.Value is FurnaceBase).Value as FurnaceBase;

    public Rake Rake => TrackingAreas.First(x => x.Value.AreaAssetCode == TrackingArea.RAKE_AREA).Value as Rake;

    public Dictionary<int, MVHAsset> FeatureCodeAssetDictionary { get; set; } = new Dictionary<int, MVHAsset>();

    public long MaxAssetCodeForNonInitializedMaterialBeingUsed { get; set; }

    public double WeightLossFactor { get; set; }

    public bool ProcessOCR { get; set; }
    public int DischargeTemperatureFeatureCode { get; set; }
    public int DischargeTemperatureAreaAssetCode {get ; set; }
  }
}
