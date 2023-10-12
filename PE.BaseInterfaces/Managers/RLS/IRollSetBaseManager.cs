using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.RLS;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.RLS
{
  public interface IRollSetBaseManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertRollSetAsync(DCRollSetData dc);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> UpdateRollSetAsync(DCRollSetData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AssembleRollSetAsync(DCRollSetData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DisassembleRollSetAsync(DCRollSetData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteRollSetAsync(DCRollSetData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateRollSetStatusAsync(DCRollSetData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ConfirmRollSetStatusAsync(DCRollSetData dc);
  }
}
