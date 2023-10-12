using System;
using System.Threading.Tasks;
using PE.BaseInterfaces.Modules;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.TRK.Base.Module.Communication
{
  public abstract class ModuleBaseExternalAdapter<T> : ExternalAdapterBase<T>, ITrackingBase where T : class, ITrackingBase
  {
    protected readonly ModuleBaseExternalAdapterHandler Handler;

    #region ctor

    public ModuleBaseExternalAdapter(ModuleBaseExternalAdapterHandler handler) : base()
    {
      Handler = handler;
    }

    #endregion

    public Task<DataContractBase> ChargeMaterialOnFurnaceExitAsyncAsync(DCChargeMaterialOnFurnaceExit dc)
    {
      return HandleIncommingMethod(Handler.ChargeWOOnFurnaceLastPosition, dc);
    }

    public Task<DataContractBase> FinishLayerAsync(DCLayer dc)
    {
      return HandleIncommingMethod(Handler.FinishLayerAsync, dc);
    }

    public Task<DataContractBase> TransferLayerAsync(DCLayer dc)
    {
      return HandleIncommingMethod(Handler.TransferLayerAsync, dc);
    }

    public Task<DataContractBase> ProcessTrackingPointSignalAsync(DcTrackingPointSignal message)
    {
      return HandleIncommingMethod(Handler.ProcessTrackingPointSignalAsync, message);
    }

    public Task<DataContractBase> ProcessAggregatedTrackingPointSignalsAsync(DcAggregatedTrackingPointSignal message)
    {
      return HandleIncommingMethod(Handler.SendAggregatedL1TrackingPointSignalsAsync, message);
    }
    public Task<DataContractBase> RemoveMaterialFromTracking(DCRemoveMaterial message)
    {
      return HandleIncommingMethod(Handler.RemoveMaterialFromArea, message);
    }
    public Task<DataContractBase> HardRemoveMaterialFromTracking(DCHardRemoveMaterial message)
    {
      return HandleIncommingMethod(Handler.HardRemoveMaterialFromTracking, message);
    }
    public Task<DataContractBase> MarkAsReady(DCMaterialReady message)
    {
      return HandleIncommingMethod(Handler.MarkAsReady, message);
    }
    public Task<DataContractBase> ProductUndo(DCProductUndo message)
    {
      return HandleIncommingMethod(Handler.ProductUndo, message);
    }
    public Task<DataContractBase> RejectRawMaterial(DCRejectMaterialData message)
    {
      return HandleIncommingMethod(Handler.RejectRawMaterial, message);
    }

    public Task<DataContractBase> CollectionMoveForward(DCUpdateArea dc)
    {
      return HandleIncommingMethod(Handler.CollectionMoveForward, dc);
    }

    public Task<DataContractBase> FurnaceDischargeForReject(DataContractBase dc)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> CollectionMoveBackward(DCUpdateArea dc)
    {
      return HandleIncommingMethod(Handler.CollectionMoveBackward, dc);
    }

    public Task<DataContractBase> FurnaceUnCharge(DataContractBase dc)
    {
      return HandleIncommingMethod(Handler.FurnaceUnCharge, dc);
    }

    public Task<DataContractBase> DischargeForRolling(DataContractBase dc)
    {
      return HandleIncommingMethod(Handler.FurnaceDischarge, dc);
    }

    public Task<DataContractBase> UnDischargeFromRolling(DataContractBase dc)
    {
      return HandleIncommingMethod(Handler.FurnaceUnDischarge, dc);
    }

    public Task<DataContractBase> TransferLayer(DataContractBase dc)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> FurnaceCharge(DataContractBase dc)
    {
      return HandleIncommingMethod(Handler.FurnaceCharge, dc);
    }

    public Task<DataContractBase> ProcessScrapMessageAsync(DCL1ScrapData dc)
    {
      return HandleIncommingMethod(Handler.ProcessScrapMessageAsync, dc);
    }

    public Task<DataContractBase> ReplaceMaterialPosition(DCMoveMaterial dc)
    {
      return HandleIncommingMethod(Handler.ReplaceMaterialPosition, dc);
    }

    public Task<DataContractBase> ChargingGridCharge(DataContractBase dc)
    {
      return HandleIncommingMethod(Handler.ChargingGridCharge, dc);
    }

    public Task<DataContractBase> ChargingGridUnCharge(DataContractBase dc)
    {
      return HandleIncommingMethod(Handler.ChargingGridUnCharge, dc);
    }

    public Task<DataContractBase> UnassignMaterial(DCMaterialUnassign dc)
    {
      return HandleIncommingMethod(Handler.UnassignMaterial, dc);
    }

    public Task<DataContractBase> AssignMaterial(DCMaterialAssign dc)
    {
      return HandleIncommingMethod(Handler.AssignMaterial, dc);
    }

    public Task<DataContractBase> CreateBundleAsync(DCBundleData dc)
    {
      return HandleIncommingMethod(Handler.CreateBundleAsync, dc);
    }
  }
}
