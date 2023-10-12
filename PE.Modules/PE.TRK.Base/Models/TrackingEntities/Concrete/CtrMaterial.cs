using System;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Base.Models.TrackingEntities
{
  /// <summary>
  ///   Material object - main object for every material
  /// </summary>
  [Serializable]
  public class CtrMaterial : CtrMaterialBase
  {
    public CtrMaterial(ITrackingStorageProviderBase storageProvider, ITrackingEventStorageProviderBase eventStorageProvider) : base(storageProvider, eventStorageProvider)
    {
    }
  }
}
