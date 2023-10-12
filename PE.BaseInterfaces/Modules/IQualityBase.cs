using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.QTY;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IQualityBase : IBaseModule
  {
    #region Defect Catalogue

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateDefectCatalogueAsync(DCDefectCatalogue defectCatalogue);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddDefectCatalogueAsync(DCDefectCatalogue defectCatalogue);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteDefectCatalogueAsync(DCDefectCatalogue defectCatalogue);

    #endregion Defect Catalogue

    #region Defect Groups Catalogue

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateDefectGroupAsync(DCDefectGroup dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddDefectGroupAsync(DCDefectGroup dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteDefectGroupAsync(DCDefectGroup dc);

    #endregion Defect Groups Catalogue

    #region Defect Categories Catalogue

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateDefectCatalogueCategoryAsync(DCDefectCatalogueCategory dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddDefectCatalogueCategoryAsync(DCDefectCatalogueCategory dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteDefectCatalogueCategoryAsync(DCDefectCatalogueCategory dc);

    #endregion Defect Categories Catalogue

    #region Defect operations

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AssignQualityAsync(DCQualityAssignment message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AssignRawMaterialQualityAsync(DCRawMaterialQuality message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AssignRawMaterialFinalQuality(DCRawMaterialQuality message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> EditRawMaterialQualityAsync(DCRawMaterialQuality message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteDefectAsync(DCDefect message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> EditDefectAsync(DCDefect dcDefect);

    #endregion Defect operations
  }
}
