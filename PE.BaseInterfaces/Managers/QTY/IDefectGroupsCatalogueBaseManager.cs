using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.QTY;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.QTY
{
  public interface IDefectGroupsCatalogueBaseManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddDefectGroupAsync(DCDefectGroup DefectGroup);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateDefectGroupAsync(DCDefectGroup DefectGroup);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteDefectGroupAsync(DCDefectGroup DefectGroup);
  }
}
