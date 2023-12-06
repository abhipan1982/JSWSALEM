using System;
using System.Threading.Tasks;
using PE.BaseInterfaces.Modules;
using PE.BaseModels.DataContracts.Internal.DBA;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.DC;
using SMF.Module.Core;
using PE.Interfaces.Managers;
using PE.Interfaces.Modules;
using PE.Models.DataContracts.Internal.PRM;
using PE.PRM.Base.Module.Communication;
using PE.Models.DataContracts.Internal.DBA;
using System.ServiceModel.Channels;

namespace PE.PRM.ProdManager.Communication
{
  // public abstract class ExternalAdapter : ModuleBaseExternalAdapter<IProdManager>, IProdManager
  //public class ExternalAdapter<T> : ExternalAdapterBase<T>, IProdManager where T : class, IProdManager
  public class ExternalAdapter : ModuleBaseExternalAdapter<IProdManager>, IProdManager
  {
    protected readonly ExternalAdapterHandler HandlerPC;
    #region ctor
    public ExternalAdapter(ExternalAdapterHandler handler) :base(handler)
    {
      HandlerPC = handler;
    }
    #endregion
    public Task<DataContractBase> CreateProductCatalogueEXTAsync(DCProductCatalogueEXT productCatalogue)
    {
      // throw new NotImplementedException();
      return HandleIncommingMethod(HandlerPC.CreateProductCatalogueEXTAsync, productCatalogue);
    }


    public Task<DataContractBase> UpdateProductCatalogueEXTAsync(DCProductCatalogueEXT productCatalogue)
    {
      // throw new NotImplementedException();
      return HandleIncommingMethod(HandlerPC.UpdateProductCatalogueEXTAsync, productCatalogue);
    }





    public Task<DataContractBase> DeleteProductCatalogueEXTAsync(DCProductCatalogueEXT productCatalogue)
    {
      //throw new NotImplementedException();
      return HandleIncommingMethod(HandlerPC.DeleteProductCatalogueEXTAsync, productCatalogue);
    }



    public Task<DataContractBase> UpdateSteelgradeAsyncEXT(DCSteelgradeEXT dcSteelgrade)
    {
      return HandleIncommingMethod(HandlerPC.UpdateSteelgradeAsyncEXT, dcSteelgrade);
    }



    public Task<DataContractBase> CreateSteelgradeAsyncEXT(DCSteelgradeEXT dcSteelgrade)
    {
      return HandleIncommingMethod(HandlerPC.CreateSteelgradeAsyncEXT, dcSteelgrade);
    }

    public Task<DataContractBase> DeleteSteelgradeAsyncEXT(DCSteelgradeEXT dcSteelgrade)
    {
      return HandleIncommingMethod(HandlerPC.DeleteSteelgradeAsyncEXT, dcSteelgrade);
    }


    public Task<DataContractBase> UpdateSteelFamilyAsyncEXT(DCSteelFamilyEXT dc)
    {
      return HandleIncommingMethod(HandlerPC.UpdateSteelFamilyAsyncEXT, dc);
    }

    public Task<DataContractBase> CreateSteelFamilyAsyncEXT(DCSteelFamilyEXT dc)
    {
      return HandleIncommingMethod(HandlerPC.CreateSteelFamilyAsyncEXT, dc);
    }


    public Task<DataContractBase> DeleteSteelFamilyAsyncEXT(DCSteelFamilyEXT dc)
    {
      return HandleIncommingMethod(HandlerPC.DeleteSteelFamilyAsyncEXT, dc);
    }


    public Task<DataContractBase> CreateHeatAsyncEXT(DCHeatEXT dcHeat)
    {
      return HandleIncommingMethod(HandlerPC.CreateHeatAsyncEXT, dcHeat);
    }


    public Task<DataContractBase> EditHeatAsyncEXT(DCHeatEXT dcHeat)
    {
      return HandleIncommingMethod(HandlerPC.EditHeatAsyncEXT, dcHeat);
    }




    public Task<DataContractBase> CreateWorkOrderAsyncEXT(DCWorkOrderEXT dcWorkOrder)
    {
      return HandleIncommingMethod(HandlerPC.CreateWorkOrderAsyncEXT, dcWorkOrder);
    }



    public Task<DataContractBase> UpdateWorkOrderAsyncEXT(DCWorkOrderEXT dcWorkOrder)
    {
      return HandleIncommingMethod(HandlerPC.UpdateWorkOrderAsyncEXT, dcWorkOrder);
    }



    public Task<DataContractBase> UpdateMaterialAsyncEXT(DCMaterialEXT dcMaterial)
    {
      return HandleIncommingMethod(HandlerPC.UpdateMaterialAsyncEXT, dcMaterial);
    }


    public Task<DataContractBase> CreateMaterialAsyncEXT(DCMaterialEXT dcMaterial)
    {
      return HandleIncommingMethod(HandlerPC.CreateMaterialAsyncEXT, dcMaterial);
    }


    public Task<DataContractBase> CreateMaterialCatalogueAsyncEXT(DCMaterialCatalogueEXT dcMaterialCatalogue)
    {
      return HandleIncommingMethod(HandlerPC.CreateMaterialCatalogueAsyncEXT, dcMaterialCatalogue);
    }

    public Task<DataContractBase> UpdateMaterialCatalogueAsyncEXT(DCMaterialCatalogueEXT dcMaterialCatalogue)
    {
      return HandleIncommingMethod(HandlerPC.UpdateMaterialCatalogueAsyncEXT, dcMaterialCatalogue);
    }

    public Task<DataContractBase> DeleteMaterialCatalogueAsyncEXT(DCMaterialCatalogueEXT dcMaterialCatalogue)
    {
      return HandleIncommingMethod(HandlerPC.DeleteMaterialCatalogueAsyncEXT, dcMaterialCatalogue);
    }


    public Task<DataContractBase> DeleteWorkOrderAsyncEXT(DCWorkOrderEXT dcWorkOrder)
    {
      return HandleIncommingMethod(HandlerPC.DeleteWorkOrderAsyncEXT, dcWorkOrder);
    }

    public Task<DCBatchDataStatus> ProcessBatchDataAsync(DCL3L2BatchDataDefinition dataToSend)
    {
      return HandleIncommingMethod(HandlerPC.ProcessBatchDataAsync, dataToSend);
    }
























































    public Task<DataContractBase> AddTestWorkOrderToScheduleAsync(DCTestSchedule dc)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> CheckShiftsWorkOrderStatusses(DCShiftCalendarId message)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> CreateHeatAsync(DCHeat heat)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> CreateMaterialAsync(DCMaterial dcMaterial)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> CreateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> CreateProductCatalogueAsync(DCProductCatalogue productCatalogue)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> CreateScrapGroupAsync(DCScrapGroup scrapGroup)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> CreateSteelFamilyAsync(DCSteelFamily dc)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> CreateSteelgradeAsync(DCSteelgrade steelgrade)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> CreateWorkOrderAsync(DCWorkOrder workOrder)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> DeleteMaterialCatalogueAsync(DCMaterialCatalogue mCatId)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> DeleteProductCatalogueAsync(DCProductCatalogue pCatId)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> DeleteScrapGroupAsync(DCScrapGroup scrapGroup)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> DeleteSteelFamilyAsync(DCSteelFamily dc)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> DeleteSteelgradeAsync(DCSteelgrade steelgradeId)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> DeleteWorkOrderAsync(DCWorkOrder workOrder)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> EditHeatAsync(DCHeat heat)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> EditMaterialNumberAsync(DCWorkOrderMaterials dc)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> EndOfWorkOrderAsync(WorkOrderId message)

    {

      throw new System.NotImplementedException();

    }



    //public Task<DCBatchDataStatus> ProcessBatchDataAsync(DCL3L2BatchData dataToSend)

    //{

    //  throw new System.NotImplementedException();

    //}



    public Task<DCProductData> ProcessBundleProductionEndAsync(DCBundleData data)

    {

      throw new System.NotImplementedException();

    }



    public Task<DCProductData> ProcessCoilProductionEndAsync(DCCoilData data)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> ProcessWorkOrderStatus(DCRawMaterial message)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> SendProductReportAsync(DCRawMaterial dc)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> SendWorkOrderReportAsync(DCWorkOrderConfirmation dc)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> UpdateBlockedWorkOrderAsync(DCWorkOrderBlock workOrder)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> UpdateCanceledWorkOrderAsync(DCWorkOrderCancel workOrder)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> UpdateMaterialAsync(DCMaterial dcMaterial)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> UpdateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> UpdateProductCatalogueAsync(DCProductCatalogue productCatalogue)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> UpdateScrapGroupAsync(DCScrapGroup scrapGroup)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> UpdateSteelFamilyAsync(DCSteelFamily dc)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> UpdateSteelgradeAsync(DCSteelgrade steelgrade)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> UpdateUnBlockedWorkOrderAsync(DCWorkOrderBlock workOrder)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> UpdateUnCanceledWorkOrderAsync(DCWorkOrderCancel workOrder)

    {

      throw new System.NotImplementedException();

    }



    public Task<DataContractBase> UpdateWorkOrderAsync(DCWorkOrder workOrder)

    {

      throw new System.NotImplementedException();

    }

    public virtual Task<PE.Models.DataContracts.Internal.DBA.DCWorkOrderStatus> ProcessWorkOrderDataAsync(DCL3L2WorkOrderDefinition message)
    {
      throw new System.NotImplementedException();
    }

   
  }
}
