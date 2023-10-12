using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.RLS;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.RLS
{
  public interface IRollTypeBaseManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertRollTypeAsync(DCRollTypeData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateRollTypeAsync(DCRollTypeData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteRollTypeAsync(DCRollTypeData dc);
  }
}
