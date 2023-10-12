using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.MVH;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IMVHistoryBase : IBaseModule
  {
    #region L1

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> StoreSingleMeasurementAsync(DcMeasData message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> StoreMultipleMeasurementsAsync(DCMeasDataAggregated message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> StoreSingleSampleMeasurementAsync(DcMeasDataSample message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> StoreMultipleSampleMeasurementsAsync(DCMeasDataAggregatedSample message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCMeasDataAggregated> GetMeasurementsAsync(DCMeasRequest message);

    //[OperationContract]
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> ProcessL1MeasurementAsync(DCMeasData message);

    //[OperationContract]
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> ProcessL1AggregatedMeasurementAsync(DCAggregatedMeasData message);

    //[OperationContract]
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> ProcessL1SampleMeasurementAsync(DCMeasDataSample message);

    #endregion L1
  }
}
