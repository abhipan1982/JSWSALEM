using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.QTY;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.QTY
{
  public interface IQualityAssignmentBaseManager
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AssignQualityAsync(DCQualityAssignment message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AssignRawMaterialQualityAsync(DCRawMaterialQuality message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AssignRawMaterialFinalQuality(DCRawMaterialQuality message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateRawMaterialQualityAsync(DCRawMaterialQuality message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> EditRawMaterialQualityAsync(DCRawMaterialQuality message);
  }
}
