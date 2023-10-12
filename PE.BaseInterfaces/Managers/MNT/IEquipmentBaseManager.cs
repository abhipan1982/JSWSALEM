using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.MNT;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.MNT
{
  public interface IEquipmentBaseManager
  {
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

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> GenerateAlarms();
  }
}
