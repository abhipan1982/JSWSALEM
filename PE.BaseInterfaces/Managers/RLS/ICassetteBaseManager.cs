using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.RLS;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.RLS
{
  public interface ICassetteBaseManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertCassetteAsync(DCCassetteData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateCassetteAsync(DCCassetteData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteCassetteAsync(DCCassetteData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DismountCassetteAsync(DCCassetteData dc);
  }
}
