using System;
using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.DBA;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.Models.DataContracts.Internal.DBA;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.Interfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IProdManager : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCBatchDataStatus> ProcessBatchDataAsync(DCL3L2BatchData dataToSend);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateSteelgradeAsync(DCSteelgrade steelgrade);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateSteelgradeAsync(DCSteelgrade steelgrade);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteSteelgradeAsync(DCSteelgrade steelgradeId);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateWorkOrderAsync(DCWorkOrder workOrder);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateWorkOrderAsync(DCWorkOrder workOrder);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateMaterialAsync(DCMaterial dcMaterial);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateMaterialAsync(DCMaterial dcMaterial);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateHeatAsync(DCHeat heat);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> EditHeatAsync(DCHeat heat);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateProductCatalogueAsync(DCProductCatalogue productCatalogue);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateProductCatalogueAsync(DCProductCatalogue productCatalogue);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteProductCatalogueAsync(DCProductCatalogue pCatId);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteMaterialCatalogueAsync(DCMaterialCatalogue mCatId);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteWorkOrderAsync(DCWorkOrder workOrder);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateScrapGroupAsync(DCScrapGroup scrapGroup);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateScrapGroupAsync(DCScrapGroup scrapGroup);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteScrapGroupAsync(DCScrapGroup scrapGroup);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateSteelFamilyAsync(DCSteelFamily dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateSteelFamilyAsync(DCSteelFamily dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteSteelFamilyAsync(DCSteelFamily dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> EditMaterialNumberAsync(DCWorkOrderMaterials dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> SendWorkOrderReportAsync(DCWorkOrderConfirmation dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> SendProductReportAsync(DCRawMaterial dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CheckShiftsWorkOrderStatusses(DCShiftCalendarId message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCProductData> ProcessCoilProductionEndAsync(DCCoilData data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCProductData> ProcessBundleProductionEndAsync(DCBundleData data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessWorkOrderStatus(DCRawMaterial message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddTestWorkOrderToScheduleAsync(DCTestSchedule dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateCanceledWorkOrderAsync(DCWorkOrderCancel workOrder);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateUnCanceledWorkOrderAsync(DCWorkOrderCancel workOrder);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateBlockedWorkOrderAsync(DCWorkOrderBlock workOrder);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateUnBlockedWorkOrderAsync(DCWorkOrderBlock workOrder);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> EndOfWorkOrderAsync(WorkOrderId message);
  }
}
