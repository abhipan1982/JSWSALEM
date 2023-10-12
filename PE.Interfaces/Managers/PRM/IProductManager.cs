using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseInterfaces.Managers.PRM;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.DC;

namespace PE.Interfaces.Managers.PRM
{
  public interface IProductManager:IProductBaseManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DCProductData> ProcessCoilProductionFinishAsync(DCCoilData materialData);
    Task<DCProductData> ProcessBundleProductionFinishAsync(DCBundleData materialData);
  }
}
