using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.EVT;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.EVT
{
  public interface IEventGroupsCatalogueManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddEventGroupAsync(DCEventGroup delayGroup);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateEventGroupAsync(DCEventGroup delayGroup);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteEventGroupAsync(DCEventGroup delayGroup);
  }
}
