using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.EVT;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.EVT
{
  public interface IEventCatalogueCategoryManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddEventCatalogueCategoryAsync(DCEventCatalogueCategory delayCatalogueCategory);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateEventCatalogueCategoryAsync(DCEventCatalogueCategory delayCatalogueCategory);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteEventCatalogueCategoryAsync(DCEventCatalogueCategory delayCatalogueCategory);
  }
}
