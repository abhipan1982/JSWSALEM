using System;
using System.Threading.Tasks;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using SMF.Core.DC;

namespace PE.TRK.Base.Managers.Abstract
{
  public interface ITrackingDispatcherManagerBase
  {
    void Init();
    void Start();

    void SetLabelTemplateCode(string newLabelCode);

    Task<ITrackingInstructionDataContractBase> ProcessLayerTransferredEventAsync(DateTime operationDate, int layerAssetCode, long? layerId = null, PEContext ctx = null);
    Task ProcessLayerFormFinishedEventAsync(int layerAssetCode, long? layerId = null, PEContext ctx = null);
    Task<DataContractBase> RejectRawMaterialAsync(DCRejectMaterialData message, PEContext ctx = null, DcTrackingPointSignal signal = null);
    void ReceiveTrackingPointSignals();
  }
}
