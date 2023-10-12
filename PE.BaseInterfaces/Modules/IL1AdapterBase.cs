using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.L1A;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IL1AdapterBase : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessMeasurementRequestAsync(DcRelatedToMaterialMeasurementRequest criteria);
    
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessAggregatedMeasurementRequestAsync(DcAggregatedMeasurementRequest data);
    
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DcRawMeasurementResponse> ProcessGetRawMeasurementsAsync(DcAggregatedMeasurementRequest data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DcMeasurementResponse> ProcessGetMeasurementValueAsync(DcMeasurementRequest data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DcNdrMeasurementResponse> ProcessNdrMeasurementRequestAsync(DcNdrMeasurementRequest data);

    
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> SendMillControlData(DCMillControlMessage data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ImportBypasses(DataContractBase dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ResendTrackingPointSignals(DataContractBase dc);
  }
}
