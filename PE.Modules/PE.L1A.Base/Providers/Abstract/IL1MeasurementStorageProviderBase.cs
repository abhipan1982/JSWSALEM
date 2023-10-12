using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.L1A.Base.Models;
using PE.L1A.Base.Models.MongoEntity;

namespace PE.L1A.Base.Providers.Abstract
{
  public interface IL1MeasurementStorageProviderBase
  {
    Task<IList<Measurement>> GetMeasurementsWithSamplesByDates(DcGetMeasurementsCriteria criteria);
    Task ProcessSaveMeasurements();
    void ProcessMeasurement(FeatureMeasurement measurement);
    Task<double?> GetFirstSampleUntilDate(int featureCode, DateTime dateToTs);
  }
}
