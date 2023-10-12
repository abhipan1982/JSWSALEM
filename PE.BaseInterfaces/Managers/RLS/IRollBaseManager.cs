using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.RLS;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.RLS
{
  public interface IRollBaseManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertRollAsync(DCRollData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateRollAsync(DCRollData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ScrapRollAsync(DCRollData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteRollAsync(DCRollData dc);


    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateStandConfigurationAsync(DCStandConfigurationData dc);
  }
}
