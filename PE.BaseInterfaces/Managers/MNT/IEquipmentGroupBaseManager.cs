using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.MNT;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.MNT
{
  public interface IEquipmentGroupBaseManager
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateEquipmentGroupAsync(DCEquipmentGroup message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteEquipmentGroupAsync(DCEquipmentGroup message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateEquipmentGroupAsync(DCEquipmentGroup message);
  }
}
