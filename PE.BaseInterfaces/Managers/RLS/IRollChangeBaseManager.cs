using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.RLS;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.RLS
{
  public interface IRollChangeBaseManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RollChangeActionAsync(DCRollChangeOperationData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RollSetToCassetteAction(DCRollSetToCassetteAction dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DCRollDiameters> GetActualRollsOnStandsDiameterAsync(DataContractBase message);
  }
}
