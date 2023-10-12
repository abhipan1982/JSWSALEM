using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface ITrackingBase : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessScrapMessageAsync(DCL1ScrapData dc);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ReplaceMaterialPosition(DCMoveMaterial materialPosition);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CollectionMoveBackward(DCUpdateArea area);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CollectionMoveForward(DCUpdateArea area);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> FurnaceDischargeForReject(DataContractBase dc);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ChargingGridCharge(DataContractBase dc);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ChargingGridUnCharge(DataContractBase dc);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> FurnaceCharge(DataContractBase dc);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> FurnaceUnCharge(DataContractBase dc);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DischargeForRolling(DataContractBase dc);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UnDischargeFromRolling(DataContractBase dc);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> TransferLayer(DataContractBase dc);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ChargeMaterialOnFurnaceExitAsyncAsync(DCChargeMaterialOnFurnaceExit dc);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> FinishLayerAsync(DCLayer dc);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> TransferLayerAsync(DCLayer dc);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessTrackingPointSignalAsync(DcTrackingPointSignal message);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessAggregatedTrackingPointSignalsAsync(DcAggregatedTrackingPointSignal message);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RejectRawMaterial(DCRejectMaterialData message);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RemoveMaterialFromTracking(DCRemoveMaterial message);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> HardRemoveMaterialFromTracking(DCHardRemoveMaterial message);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> MarkAsReady(DCMaterialReady message);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProductUndo(DCProductUndo message);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UnassignMaterial(DCMaterialUnassign dc);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AssignMaterial(DCMaterialAssign dc);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateBundleAsync(DCBundleData dc);
  }
}
