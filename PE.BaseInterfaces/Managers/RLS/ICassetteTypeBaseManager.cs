using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.RLS;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.RLS
{
  public interface ICassetteTypeBaseManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertCassetteTypeAsync(DCCassetteTypeData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateCassetteTypeAsync(DCCassetteTypeData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteCassetteTypeAsync(DCCassetteTypeData dc);
  }
}
