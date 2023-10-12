using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.PRM
{
  public interface IProductBaseManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DCProductData> ProcessCoilProductionFinishAsync(DCCoilData materialData);
    Task<DCProductData> ProcessBundleProductionFinishAsync(DCBundleData materialData);
  }
}
