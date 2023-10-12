using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.QTY;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.QTY
{
  public interface IDefectCatalogueCategoryBaseManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddDefectCatalogueCategoryAsync(DCDefectCatalogueCategory DefectCatalogueCategory);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateDefectCatalogueCategoryAsync(DCDefectCatalogueCategory DefectCatalogueCategory);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteDefectCatalogueCategoryAsync(DCDefectCatalogueCategory DefectCatalogueCategory);
  }
}
