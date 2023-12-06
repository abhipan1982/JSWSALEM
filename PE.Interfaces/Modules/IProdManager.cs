using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseInterfaces.Modules;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.Models.DataContracts.Internal.DBA;
using PE.Models.DataContracts.Internal.PRM;
using SMF.Core.DC;

namespace PE.Interfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IProdManager : IProdManagerBase
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateProductCatalogueEXTAsync(DCProductCatalogueEXT productCatalogue);


    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateProductCatalogueEXTAsync(DCProductCatalogueEXT productCatalogue);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteProductCatalogueEXTAsync(DCProductCatalogueEXT productCatalogue);



    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateSteelgradeAsyncEXT(DCSteelgradeEXT dcSteelgrade);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateSteelgradeAsyncEXT(DCSteelgradeEXT dcSteelgrade);


    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteSteelgradeAsyncEXT(DCSteelgradeEXT dcSteelgrade);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateSteelFamilyAsyncEXT(DCSteelFamilyEXT dc);


    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateSteelFamilyAsyncEXT(DCSteelFamilyEXT dc);


    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteSteelFamilyAsyncEXT(DCSteelFamilyEXT dc);




    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateHeatAsyncEXT(DCHeatEXT dcHeat);


    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> EditHeatAsyncEXT(DCHeatEXT dcHeat);


    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateWorkOrderAsyncEXT(DCWorkOrderEXT dcWorkOrder);


    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateWorkOrderAsyncEXT(DCWorkOrderEXT dcWorkOrder);


    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateMaterialAsyncEXT(DCMaterialEXT dcMaterial);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateMaterialCatalogueAsyncEXT(DCMaterialCatalogueEXT dcMaterialCatalogue);


    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateMaterialCatalogueAsyncEXT(DCMaterialCatalogueEXT dcMaterialCatalogue);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteMaterialCatalogueAsyncEXT(DCMaterialCatalogueEXT dcMaterialCatalogue);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteWorkOrderAsyncEXT(DCWorkOrderEXT dcWorkOrder);


    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateMaterialAsyncEXT(DCMaterialEXT dcMaterial);
    Task<DCBatchDataStatus> ProcessBatchDataAsync(DCL3L2BatchDataDefinition dataToSend); //Added by AP



    //[OperationContract]
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> CreateMaterialAsyncEXT(DCMaterialEXT dcMaterial);
  }

}


