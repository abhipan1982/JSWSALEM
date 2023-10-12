using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.MNT;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IMaintenanceBase : IBaseModule
  {
    #region Equipment Groups Processing
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateEquipmentGroupAsync(DCEquipmentGroup message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteEquipmentGroupAsync(DCEquipmentGroup message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateEquipmentGroupAsync(DCEquipmentGroup message);
    #endregion

    #region Equipment processing

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateEquipmentAsync(DCEquipment message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteEquipmentAsync(DCEquipment message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateEquipmentAsync(DCEquipment message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateEquipmentStatusAsync(DCEquipment message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CloneEquipmentAsync(DCEquipment message);

    #endregion

    #region Equipment usage accumulation

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AccumulateEquipmentUsageAsync(DCEquipmentAccu dc);

    #endregion
  }
}
