using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.YRD;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.YRD
{
  public interface IBilletYardBaseManager
  {
    Task<DataContractBase> TransferHeatIntoLocationAsync(DCTransferHeatLocation message);
    Task<DataContractBase> TransferHeatIntoChargingGridAsync(DCTransferHeatToChargingGrid dc);
    Task<DataContractBase> CreateMaterialInReceptionAsync(DCCreateMaterialInReception message);
    Task<DataContractBase> ScrapMaterials(DCScrapMaterial message);
    Task<DataContractBase> UnscrapMaterials(DCUnscrapMaterial message);
    Task<DataContractBase> CreateHeatWithMaterials(DCCreateMaterialWithHeatInReception message);
    Task<DataContractBase> MaterialConnected(DCL3MaterialConnect message);
  }
}
