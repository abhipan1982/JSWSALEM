using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
//using PE.BaseDbEntity.Providers;
using PE.BaseInterfaces.Managers.PRM;
using PE.BaseInterfaces.SendOffices.PRM;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.Common;
using PE.PRM.Base.Handlers;
using SMF.Core.DC;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Extensions;
using SMF.Core.Helpers;
using SMF.Core.Infrastructure;
using SMF.Core.Interfaces;
using SMF.Core.Notification;
using PE.Interfaces.Managers.PRM;

using PE.Models.DataContracts.Internal.PRM;
using PE.DbEntity.Models;
using PE.DbEntity.PEContext;
using PE.DbEntity.Providers;
using PE.PRM.Base.Managers;
using PE.PRM.ProdManager.Handler;
using PE.DbEntity.HmiModels;
using SMF.Module.Core.Interfaces;

namespace PE.PRM.ProdManager
{
  public class CatalogueManager : BaseManager, ICatalogueManager
  {
    #region members

    //protected readonly IProdManagerCatalogueBaseSendOffice SendOffice;
    protected readonly IContextProvider<PEContext> Context;
    protected readonly ICustomContextProvider<PECustomContext> CustomContext;
    protected readonly ICustomContextProvider<HmiContext> HmiContext;
    #endregion

    #region ctor    

    public CatalogueManager(
      IModuleInfo moduleInfo,
      //IProdManagerCatalogueBaseSendOffice sendOffice,
      //IContextProvider<PEContext> context,
      ICustomContextProvider<PECustomContext> customContext,//@av
      ICustomContextProvider<HmiContext> hmiContext)//@av
      //SteelgradeHandlerBase steelgradeHandler,
      //MaterialCatalogueHandlerBase materialCatalogueHandler,
      ////ProductCatalogueHandler productCatalogueHandler,
      //WorkOrderHandlerBase workOrderHandler,
      //HeatHandlerBase heatHandler,
      //MaterialHandlerBase materialHandler,
      //ScrapGroupHandlerBase scrapGroupHandler,
      //SteelFamilyHandlerBase steelFamilyHandler,
      //BilletYardHandlerBase billetYardHandler)
      : base(moduleInfo)
    {
      //SendOffice = sendOffice;
      //Context = context;
      CustomContext = customContext;
      HmiContext = hmiContext;
      //SteelgradeHandler = steelgradeHandler;
      //MaterialCatalogueHandler = materialCatalogueHandler;
      //ProductCatalogueHandler = productCatalogueHandler;
      //WorkOrderHandler = workOrderHandler;
      //HeatHandler = heatHandler;
      //MaterialHandler = materialHandler;
      //ScrapGroupHandler = scrapGroupHandler;
      //SteelFamilyHandler = steelFamilyHandler;
      //BilletYardHandler = billetYardHandler;
    }

    #endregion

    #region handlers

    protected readonly SteelgradeHandlerBase SteelgradeHandler;
    protected readonly MaterialCatalogueHandlerBase MaterialCatalogueHandler;
   // protected readonly ProductCatalogueHandlerBase ProductCatalogueHandler;
    protected readonly ProductCatalogueHandler ProductCatalogueHandler = new ProductCatalogueHandler();//Av@
    protected readonly WorkOrderHandlerBase WorkOrderHandler;
    protected readonly HeatHandlerBase HeatHandler;
    protected readonly MaterialHandlerBase MaterialHandler;
    protected readonly ScrapGroupHandlerBase ScrapGroupHandler;
    protected readonly SteelFamilyHandlerBase SteelFamilyHandler;
    protected readonly BilletYardHandlerBase BilletYardHandler;

    #endregion

    #region func



    public virtual async Task<DataContractBase> CreateProductCatalogueEXTAsync(DCProductCatalogueEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = CustomContext.Create();


        PRMProductCatalogue productCatalogue1 = null;
        //productCatalogue1 = await ProductCatalogueHandler.GetProductCatalogueByNameEXTAsync(ctx, dc.Name);

        if (productCatalogue1 is not null)
          throw new InternalModuleException($"Product catalogue {dc.Name} already exists.",
            AlarmDefsBase.ProductCatalogueAlreadyExists, dc.Name);

        //productCatalogue1 = ProductCatalogueHandler.CreateProductCatalogue(dc);
        ctx.PRMProductCatalogues.Add(productCatalogue1);
        await ctx.SaveChangesAsync();

        HmiRefresh(HMIRefreshKeys.ProductCatalogue);




        PRMProductCatalogueEXT productCatalogue = null;
        //productCatalogue = await ProductCatalogueHandler.GetProductCatalogueByNameEXTAsync(ctx , dc.Name);

        if (productCatalogue is not null)
          throw new InternalModuleException($"Product catalogue {dc.Name} already exists.",
            AlarmDefsBase.ProductCatalogueAlreadyExists, dc.Name);

        productCatalogue = ProductCatalogueHandler.CreateProductCatalogue(dc);
        ctx.PRMProductCatalogueEXTs.Add(productCatalogue);
        await ctx.SaveChangesAsync();

        HmiRefresh(HMIRefreshKeys.ProductCatalogue);












      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while creating product catalogue {dc.Name}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while creating product catalogue {dc.Name}.");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while creating product catalogue {dc.Name}.");
      }

      return result;
    }





    public virtual async Task<DataContractBase> UpdateProductCatalogueEXTAsync(DCProductCatalogueEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = CustomContext.Create();
               //Av@
        PRMProductCatalogue productCatalogue1 =
            await ProductCatalogueHandler.GetProductCatalogueByIdEXTAsync1(ctx, dc.Id);

        if (productCatalogue1 is null)
          throw new InternalModuleException($"Product catalogue {dc.Name} [{dc.Id}] not found.", AlarmDefsBase.ProductCatalogueNotFound);

        ProductCatalogueHandler.UpdateProductCatalogue(null, productCatalogue1, dc);

        await ctx.SaveChangesAsync();

        HmiRefresh(HMIRefreshKeys.ProductCatalogue);





        PRMProductCatalogueEXT productCatalogue =await ProductCatalogueHandler.GetProductCatalogueByIdEXTAsync(ctx, dc.Id);

        if (productCatalogue is null)
          throw new InternalModuleException($"Product catalogue {dc.Name} [{dc.Id}] not found.", AlarmDefsBase.ProductCatalogueNotFound);

        ProductCatalogueHandler.UpdateProductCatalogue(productCatalogue, null, dc);

        await ctx.SaveChangesAsync();

        HmiRefresh(HMIRefreshKeys.ProductCatalogue);


      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while updating product catalogue {dc.Name} [{dc.Id}].");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while updating product catalogue {dc.Name} [{dc.Id}].");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ProductCatalogueUpdate,
          $"Unexpected error while updating product catalogue {dc.Name} [{dc.Id}].", dc.Name);
      }

      return result;
    }




























    public Task<DataContractBase> CreateSteelgradeCatalogueAsync(DCSteelgrade steelgrade)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> UpdateSteelgradeCatalogueAsync(DCSteelgrade steelgrade)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> DeleteSteelgradeAsync(DCSteelgrade steelgrade)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> CreateHeatAsync(DCHeat heat)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> EditHeatAsync(DCHeat heat)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> CreateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> UpdateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> DeleteMaterialCatalogueAsync(DCMaterialCatalogue mCat)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> CreateProductCatalogueAsync(DCProductCatalogue productCatalogue)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> UpdateProductCatalogueAsync(DCProductCatalogue productCatalogue)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> DeleteProductCatalogueAsync(DCProductCatalogue pCat)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> DeleteWorkOrderAsync(DCWorkOrder workOrder)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> CreateMaterialAsync(DCMaterial material)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> UpdateMaterialAsync(DCMaterial material)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> CreateScrapGroupCatalogueAsync(DCScrapGroup scrapGroup)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> UpdateScrapGroupCatalogueAsync(DCScrapGroup scrapGroup)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> DeleteScrapGroupAsync(DCScrapGroup scrapGroup)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> CreateSteelFamilyCatalogueAsync(DCSteelFamily dc)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> UpdateSteelFamilyCatalogueAsync(DCSteelFamily dc)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> DeleteSteelFamilyAsync(DCSteelFamily dc)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> EditMaterialNumberAsync(DCWorkOrderMaterials workOrderMaterials)
    {
      throw new NotImplementedException();
    }
    #endregion
  }
}
