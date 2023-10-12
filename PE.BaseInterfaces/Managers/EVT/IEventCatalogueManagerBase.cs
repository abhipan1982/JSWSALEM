using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.EVT;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.EVT
{
  public interface IEventCatalogueManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddEventCatalogueAsync(DCEventCatalogue delayCatalogue);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateEventCatalogueAsync(DCEventCatalogue delayCatalogue);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteEventCatalogueAsync(DCEventCatalogue delayCatalogue);
  }
}
