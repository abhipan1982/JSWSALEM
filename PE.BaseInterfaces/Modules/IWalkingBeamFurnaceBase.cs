using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.WBF;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IWalkingBeamFurnaceBase : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessMaterialChangeInFurnaceEventAsync(DCFurnaceRawMaterials data);
    
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessMaterialDischargeEventAsync(DCFurnaceMaterialDischarge data);
  }
}
