using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.DC;

namespace PE.TRK.Base.Managers.Abstract
{
  public interface ITrackingRawMaterialManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessTrackingEventAsync(DCTrackingEvent message);


    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessScrapMessageAsync(DCL1ScrapData message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessCutDataMessageAsync(DCL1CutData message);

    [FaultContract(typeof(ModuleMessage))]
    Task<WorkOrderId> ProcessL1BaseIdRequestAsync(WorkOrderId message);

    [FaultContract(typeof(ModuleMessage))]
    Task<WorkOrderId> ProcessDivisionMaterialMessageAsync(DCL1MaterialDivision message);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> ProcessFinishedMessageAsync(DCL1MaterialFinished message);
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> ChangeMaterialStatusAsync(DCNewMaterialStatus message);
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AssignMaterialsAsync(DCMaterialAssign message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UnassignMaterialAsync(DCMaterialAssign message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> MarkMaterialAsScrapAsync(DCMaterialMarkedAsScrap message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RejectRawMaterial(DCRejectMaterialData message);

    [FaultContract(typeof(ModuleMessage))]
    Task<LayerId> ProcessLayerCreationRequestAsync(LayerId message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DCMaterialsCountResult> ProcessGetMaterialsCountByLayerIdAsync(LayerId message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessSetLayerForMaterialsByMaterialIds(DCAppendRawMaterialsToLayerRequest message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessLayerFormFinished(LayerId message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessLayerTransferredEventAsync(LayerId message);
  }
}
