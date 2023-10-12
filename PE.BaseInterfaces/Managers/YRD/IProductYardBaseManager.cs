using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.YRD;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.YRD
{
  public interface IProductYardBaseManager
  {
    Task<DataContractBase> DispatchWorkOrder(DCWorkOrderToDispatch message);
    Task<DataContractBase> RelocateProducts(DCProductRelocation message);
    Task<DataContractBase> ReorderLocationSeq(DCProductYardLocationOrder message);
    Task<DataContractBase> ProcessProduct(DCProductInYard message);
  }
}
