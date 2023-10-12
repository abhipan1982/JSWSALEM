using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.QTY;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.QTY
{
  public interface IDefectCatalogueBaseManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateDefectCatalogueAsync(DCDefectCatalogue defectCatalogue);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddDefectCatalogueAsync(DCDefectCatalogue defectCatalogue);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteDefectCatalogueAsync(DCDefectCatalogue defectCatalogue);
  }
}
