using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.DC;

namespace PE.TRK.Base.Managers.Abstract
{
  public interface ITrackingManagerBase
  {
    Task PrintStateAsync();
    Task SendMaterialPosition(long elapsedMillis);
    Task<DataContractBase> MarkAsReady(DCMaterialReady message);
    Task<DataContractBase> ProcessScrapMessageAsync(DCL1ScrapData message);
    Task<DataContractBase> RemoveMaterialFromArea(DCRemoveMaterial message);
    Task<DataContractBase> HardRemoveMaterialFromTracking(DCHardRemoveMaterial message);
    Task<DataContractBase> ProductUndo(DCProductUndo message);
    Task<DataContractBase> UnassignMaterial(DCMaterialUnassign message);
    Task<DataContractBase> AssignMaterial(DCMaterialAssign message);
    Task<DataContractBase> ChargingGridUnCharge(DataContractBase arg);
    Task<DataContractBase> ChargingGridCharge(DataContractBase arg);
    Task<DataContractBase> FurnaceUnCharge(DataContractBase message);
    Task<DataContractBase> FurnaceCharge(DataContractBase message);
    Task<DataContractBase> FurnaceDischarge(DataContractBase message);
    Task<DataContractBase> FurnaceUnDischarge(DataContractBase message);
    Task<DataContractBase> FurnaceChargeOnExit(DCChargeMaterialOnFurnaceExit message);
    Task<DataContractBase> CollectionMoveForward(DCUpdateArea arg);
    Task<DataContractBase> CollectionMoveBackward(DCUpdateArea arg);
    Task<DataContractBase> ReplaceMaterialPosition(DCMoveMaterial arg);
    Task<DataContractBase> ProcessOCRMessageAsync(DataContractBase dc);
    Task<DataContractBase> CreateBundleAsync(DCBundleData dc);
  }
}
