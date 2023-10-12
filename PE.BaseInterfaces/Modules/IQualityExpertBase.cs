using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.QEX;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IQualityExpertBase : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessMaterialAreaExitEventAsync(DCAreaRawMaterial dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ForceRatingValueAsync(DCRatingForce dataContract);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ToggleCompensationAsync(DCCompensationTrigger dataContract);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateMaterialGradingasync(DCRawMaterial dc);
  }
}
