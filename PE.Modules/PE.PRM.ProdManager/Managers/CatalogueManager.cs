using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseDbEntity.Providers;
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
using PE.PRM.ProdManager.Handlers;//Av@271123

using PE.DbEntity.HmiModels;
using SMF.Module.Core.Interfaces;

namespace PE.PRM.ProdManager
{
  public class CatalogueManager : BaseManager, ICatalogueManager
  {
    #region members

    protected readonly IProdManagerCatalogueBaseSendOffice SendOffice;
    protected readonly IContextProvider<PEContext> Context;
    protected readonly ICustomContextProvider<PECustomContext> CustomContext;
    protected readonly ICustomContextProvider<HmiContext> HmiContext;
    #endregion

    #region ctor    

    public CatalogueManager(
      IModuleInfo moduleInfo,
      //IProdManagerCatalogueBaseSendOffice sendOffice,
      IContextProvider<PEContext> context,
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
      Context = context;
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

    protected readonly SteelgradeHandler SteelgradeHandler = new SteelgradeHandler();//Av@
    protected readonly MaterialCatalogueHandler MaterialCatalogueHandler = new MaterialCatalogueHandler();//AV@
    protected readonly ProductCatalogueHandler ProductCatalogueHandler = new ProductCatalogueHandler();//Av@
    protected readonly WorkOrderHandler WorkOrderHandler = new WorkOrderHandler();//Av@
    protected readonly HeatHandler HeatHandler = new HeatHandler();//Av@271123

    protected readonly MaterialHandler MaterialHandler = new MaterialHandler();//AV@

    protected readonly ScrapGroupHandlerBase ScrapGroupHandler;
    protected readonly SteelFamilyHandler SteelFamilyHandler = new SteelFamilyHandler();//Av@
    protected readonly BilletYardHandler BilletYardHandler = new BilletYardHandler();//Av@

    #endregion

    #region func



    public virtual async Task<DataContractBase> CreateProductCatalogueEXTAsync(DCProductCatalogueEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = CustomContext.Create();


        PRMProductCatalogue productCatalogue1 = null;
        productCatalogue1 = await ProductCatalogueHandler.GetProductCatalogueByNameEXTAsync1(ctx , dc.Name);

        if (productCatalogue1 is not null)
          throw new InternalModuleException($"Product catalogue {dc.Name} already exists.",
            AlarmDefsBase.ProductCatalogueAlreadyExists, dc.Name);

        productCatalogue1 = ProductCatalogueHandler.CreateProductCatalogue1(dc);
        ctx.PRMProductCatalogues.Add(productCatalogue1);
        await ctx.SaveChangesAsync();





        var temp_id=ctx.PRMProductCatalogues .Where(t=>t.ProductCatalogueName== dc.Name).Select(t => t.ProductCatalogueId)
            .FirstOrDefault();




        //HmiRefresh(HMIRefreshKeys.ProductCatalogue);




        //PRMProductCatalogueEXT productCatalogue = null;
        //productCatalogue = await ProductCatalogueHandler.GetProductCatalogueByNameEXTAsync(ctx, dc.Name);

        //if (productCatalogue is not null)
        //  throw new InternalModuleException($"Product catalogue {dc.Name} already exists.",
        //    AlarmDefsBase.ProductCatalogueAlreadyExists, dc.Name);

        //productCatalogue = ProductCatalogueHandler.CreateProductCatalogue(dc);
        //ctx.PRMProductCatalogueEXTs.Add(productCatalogue);
        //await ctx.SaveChangesAsync();

        //HmiRefresh(HMIRefreshKeys.ProductCatalogue);



        PRMProductCatalogueEXT productCatalogue = null;
        //productCatalogue = await ProductCatalogueHandler.GetProductCatalogueByNameEXTAsync(ctx, temp_id);

        //if (productCatalogue is not null)
        //  throw new InternalModuleException($"Product catalogue {dc.Id} already exists.",
        //    AlarmDefsBase.ProductCatalogueAlreadyExists, dc.Id);

        productCatalogue = ProductCatalogueHandler.CreateProductCatalogue(dc,temp_id);
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





















    public virtual async Task<DataContractBase> DeleteProductCatalogueEXTAsync(DCProductCatalogueEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = CustomContext.Create();




        PRMProductCatalogueEXT productCatalogue =
            await ProductCatalogueHandler.GetProductCatalogueByIdEXTAsync(ctx, dc.Id);
        if (productCatalogue is null)
          throw new InternalModuleException($"Product catalogue {dc.Name} [{dc.Id}] not found.", AlarmDefsBase.ProductCatalogueNotFound);

        ctx.PRMProductCatalogueEXTs.Remove(productCatalogue);
        await ctx.SaveChangesAsync();





        PRMProductCatalogue productCatalogue1 =
            await ProductCatalogueHandler.GetProductCatalogueByIdEXTAsync1(ctx, dc.Id);
        if (productCatalogue1 is null)
          throw new InternalModuleException($"Product catalogue {dc.Name} [{dc.Id}] not found.", AlarmDefsBase.ProductCatalogueNotFound);

        ctx.PRMProductCatalogues.Remove(productCatalogue1);
        await ctx.SaveChangesAsync();

        

        HmiRefresh(HMIRefreshKeys.ProductCatalogue);









      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while removing product catalogue [{dc.Id}].");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while removing product catalogue [{dc.Id}].");
      }

      return result;
    }


    public virtual async Task<DataContractBase> UpdateSteelgradeCatalogueAsyncEXT(DCSteelgradeEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        PRMSteelgrade steelgrade = await SteelgradeHandler.GetSteelgradeByCodeAsyncEXT(ctx, dc.SteelgradeCode);
        if (steelgrade is null)
          throw new InternalModuleException($"Steelgrade {dc.SteelgradeCode} not found.", AlarmDefsBase.RecordNotFound);
        SteelgradeHandler.UpdateSteelgrade(steelgrade, dc);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.Steelgrade);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while updating steelgrade {dc.SteelgradeCode}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while updating steelgrade {dc.SteelgradeCode}.");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.SteelgradeUpdate,
          $"Unexpected error while updating steelgrade {dc.SteelgradeCode}.", dc.SteelgradeCode);
      }

      return result;
    }



    public virtual async Task<DataContractBase> CreateSteelgradeCatalogueAsyncEXT(DCSteelgradeEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        PRMSteelgrade steelgrade = await SteelgradeHandler.GetSteelgradeByCodeAsyncEXT(ctx, dc.SteelgradeCode);
        if (steelgrade is not null)
          throw new InternalModuleException($"Steelgrade {dc.SteelgradeCode} already exists.", AlarmDefsBase.SteelgradeAlreadyExists, dc.SteelgradeCode);

        steelgrade = SteelgradeHandler.CreateSteelgrade(dc);
        ctx.PRMSteelgrades.Add(steelgrade);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.Steelgrade);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation for steelgrade {dc.SteelgradeCode}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation for steelgrade {dc.SteelgradeCode}.");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error for steelgrade {dc.SteelgradeCode}.");
      }

      return result;
    }








    public virtual async Task<DataContractBase> DeleteSteelgradeAsyncEXT(DCSteelgradeEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        PRMSteelgrade item = await SteelgradeHandler.GetSteelgradeByIdAsyncEXT(ctx, dc.Id);
        if (item is null)
          throw new InternalModuleException($"Steelgrade {dc.SteelgradeCode} not found.", AlarmDefsBase.RecordNotFound);

        ctx.PRMSteelgrades.Remove(item);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.Steelgrade);
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while removing steelgrade [{dc.Id}].");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while removing steelgrade [{dc.Id}].");
      }

      return result;
    }








    public virtual async Task<DataContractBase> UpdateSteelFamilyCatalogueAsyncEXT(DCSteelFamilyEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        //Verify code uniqueness
        PRMSteelGroup steelGroup = await SteelFamilyHandler.GetSteelFamilyByIdAsyncEXT(ctx, dc.SteelFamilyId);
        PRMSteelGroup steelGroupByCode = await SteelFamilyHandler.GetSteelFamilyByCodeAsyncEXT(ctx, dc.SteelFamilyCode);
        if (steelGroup is null)
          throw new InternalModuleException($"Scrap family {dc.SteelFamilyId} not found.", AlarmDefsBase.RecordNotFound);
        if (steelGroupByCode != null && steelGroup.SteelGroupId != steelGroupByCode.SteelGroupId)
          throw new InternalModuleException($"Steel family {dc.SteelFamilyCode} [{dc.SteelFamilyId}] already exists.",
            AlarmDefsBase.SteelFamilyAlreadyExists, dc.SteelFamilyCode);

        //Remove 'default' flag from any other item
        if (dc.IsDefault)
        {
          await SteelFamilyHandler.RemoveIsDefaultFromDefaultItem(ctx);
        }

        SteelFamilyHandler.UpdateSteelFamily(steelGroup, dc);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.SteelFamily);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while updating steel family {dc.SteelFamilyCode} [{dc.SteelFamilyId}].");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while updating steel family {dc.SteelFamilyCode} [{dc.SteelFamilyId}].");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while updating steel family {dc.SteelFamilyCode} [{dc.SteelFamilyId}].");
      }

      return result;
    }













    public virtual async Task<DataContractBase> CreateSteelFamilyCatalogueAsyncEXT(DCSteelFamilyEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        //Verify code uniqueness
        PRMSteelGroup steelGroup = await SteelFamilyHandler.GetSteelFamilyByCodeAsyncEXT(ctx, dc.SteelFamilyCode);
        if (steelGroup is not null)
          throw new InternalModuleException($"Steel family {dc.SteelFamilyCode} already exists.",
            AlarmDefsBase.SteelFamilyAlreadyExists, dc.SteelFamilyCode);

        if (dc.IsDefault)
        {
          await SteelFamilyHandler.RemoveIsDefaultFromDefaultItem(ctx);
        }

        steelGroup = SteelFamilyHandler.CreateSteelFamily(dc);
        ctx.PRMSteelGroups.Add(steelGroup);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.SteelFamily);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while creating steel family {dc.SteelFamilyCode}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while creating steel family {dc.SteelFamilyCode}.");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while creating steel family {dc.SteelFamilyCode}.");
      }

      return result;
    }



    public virtual async Task<DataContractBase> DeleteSteelFamilyAsyncEXT(DCSteelFamilyEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        PRMSteelGroup item = await SteelFamilyHandler.GetSteelFamilyByIdAsyncEXT(ctx, dc.SteelFamilyId);
        if (item is null)
          throw new InternalModuleException($"Steel family {dc.SteelFamilyId} not found.", AlarmDefsBase.RecordNotFound);
        ctx.PRMSteelGroups.Remove(item);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.SteelFamily);
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while removing steel family [{dc.SteelFamilyId}].");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while removing steel family [{dc.SteelFamilyId}].");
      }

      return result;
    }






    public virtual async Task<DataContractBase> CreateHeatAsyncEXT(DCHeatEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        HeatHandler.CreateHeatByUserEXT(ctx, dc);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.Heat);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while creating heat {dc.HeatName}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while creating heat {dc.HeatName}.");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.HeatNotCreated,
          $"Unexpected error while creating heat {dc.HeatName}.");
      }

      return result;
    }





    public virtual async Task<DataContractBase> EditHeatAsyncEXT(DCHeatEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        PRMHeat heat = await HeatHandler.GetHeatByNameAsyncEXT(ctx, dc.HeatName);
        if (heat is null)
          throw new InternalModuleException($"Heat {dc.HeatName} not found.", AlarmDefsBase.RecordNotFound);

        HeatHandler.UpdateHeat(heat, dc);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.Heat);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while updating heat {dc.HeatName}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while updating heat {dc.HeatName}.");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while updating heat {dc.HeatName}.");
      }

      return result;
    }







    public virtual async Task<DataContractBase> UpdateMaterialAsyncEXT(DCMaterialEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx =Context.Create();
        long workOrderId = await WorkOrderHandler.WorkOrderIdByNameAsyncEXT(ctx, dc.FKWorkOrderIdRef);
        if (workOrderId == 0)
          throw new InternalModuleException($"Work order {dc.FKWorkOrderIdRef} not found", AlarmDefsBase.RecordNotFound);

        int previousMaterialsCount = ctx.PRMMaterials
          .Count(x => x.FKWorkOrderId == workOrderId);
        PRMWorkOrder workOrder = ctx.PRMWorkOrders
          .FirstOrDefault(x => x.WorkOrderId == workOrderId);
        if (workOrder is null)
          throw new InternalModuleException($"Work order [{workOrderId}] not found", AlarmDefsBase.RecordNotFound);
        MVHAsset reception = await BilletYardHandler.GetReceptionEXT(ctx);
        if (reception is null)
          throw new InternalModuleException("Billet reception asset not found in database", AlarmDefsBase.BilletReceptionNotFound);

        long previousHeat = workOrder.FKHeatId ?? 0;

        if (previousHeat != dc.FKHeatId || previousMaterialsCount != dc.MaterialsNumber)
        {
          MaterialHandler.DeleteOldMaterialsAfterWoUpdate(ctx, workOrderId);
          MaterialHandler.CreateMaterialByUserEXT(ctx, dc, reception, workOrderId);

          await ctx.SaveChangesAsync();
        }

        HmiRefresh(HMIRefreshKeys.Schedule);
        HmiRefresh(HMIRefreshKeys.BundleWeighingMonitor);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while updating materials by work order {dc.FKWorkOrderIdRef}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while updating materials by work order {dc.FKWorkOrderIdRef}.");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while updating materials by work order {dc.FKWorkOrderIdRef}.");
      }

      return result;
    }



















    public virtual async Task<DataContractBase> CreateMaterialCatalogueAsyncEXT(DCMaterialCatalogueEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        PRMMaterialCatalogue catalogue = null;
        catalogue = await MaterialCatalogueHandler.GetMaterialCatalogueByNameAsyncEXT(ctx, dc.MaterialCatalogueName);

        if (catalogue is not null)
          throw new InternalModuleException($"Material catalogue {dc.MaterialCatalogueName} already exists.",
            AlarmDefsBase.MaterialCatalogueAlreadyExists, dc.MaterialCatalogueName);

        catalogue = MaterialCatalogueHandler.CreateMaterialCatalogueEXT(dc);
        ctx.PRMMaterialCatalogues.Add(catalogue);

        await ctx.SaveChangesAsync();

        HmiRefresh(HMIRefreshKeys.MaterialCatalogue);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while creating material catalogue {dc.MaterialCatalogueName}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while creating material catalogue {dc.MaterialCatalogueName}.");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while creating material catalogue {dc.MaterialCatalogueName}.");
      }

      return result;
    }



    public virtual async Task<DataContractBase> UpdateMaterialCatalogueAsyncEXT(DCMaterialCatalogueEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = CustomContext.Create();
        PRMMaterialCatalogue catalogue =
            await MaterialCatalogueHandler.GetMaterialCatalogueByIdEXT(ctx, dc.MaterialCatalogueId);
        if (catalogue is null)
          throw new InternalModuleException($"Material catalogue {dc.MaterialCatalogueName} [{dc.MaterialCatalogueId}] not found.",
            AlarmDefsBase.RecordNotFound);

        MaterialCatalogueHandler.UpdateMaterialCatalogueEXT(catalogue, dc);

        await ctx.SaveChangesAsync();

        HmiRefresh(HMIRefreshKeys.MaterialCatalogue);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while updating material catalogue {dc.MaterialCatalogueName} [{dc.MaterialCatalogueId}].");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while updating material catalogue {dc.MaterialCatalogueName} [{dc.MaterialCatalogueId}].");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.MaterialCatalogueUpdate,
          $"Unexpected error while updating material catalogue {dc.MaterialCatalogueName} [{dc.MaterialCatalogueId}].", dc.MaterialCatalogueName);
      }

      return result;
    }



    public virtual async Task<DataContractBase> DeleteMaterialCatalogueAsyncEXT(DCMaterialCatalogueEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        PRMMaterialCatalogue catalogue = await MaterialCatalogueHandler.GetMaterialCatalogueByIdEXT(ctx,
          dc.MaterialCatalogueId);
        if (catalogue is null)
          throw new InternalModuleException($"Material catalogue [{dc.MaterialCatalogueId}] not found.", AlarmDefsBase.RecordNotFound);

        ctx.PRMMaterialCatalogues.Remove(catalogue);
        await ctx.SaveChangesAsync();

        HmiRefresh(HMIRefreshKeys.MaterialCatalogue);
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while removing material catalogue [{dc.MaterialCatalogueId}].");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while removing material catalogue [{dc.MaterialCatalogueId}].");
      }

      return result;
    }





    public virtual async Task<DataContractBase> CreateMaterialAsyncEXT(DCMaterialEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        long workOrderId = await WorkOrderHandler.WorkOrderIdByNameAsyncEXT(ctx, dc.FKWorkOrderIdRef);
        if (workOrderId == 0)
          throw new InternalModuleException($"Work order {dc.FKWorkOrderIdRef} not found.", AlarmDefsBase.RecordNotFound);
        MVHAsset reception = await BilletYardHandler.GetReceptionEXT(ctx);
        if (reception is null)
          throw new InternalModuleException("Billet reception asset not found in database", AlarmDefsBase.BilletReceptionNotFound);

        MaterialHandler.CreateMaterialByUserEXT(ctx, dc, reception, workOrderId);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.Schedule);
        HmiRefresh(HMIRefreshKeys.BundleWeighingMonitor);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while creating material for work order {dc.FKWorkOrderIdRef}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while creating material for work order {dc.FKWorkOrderIdRef}.");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while creating material for work order {dc.FKWorkOrderIdRef}.");
      }

      return result;
    }






























    public virtual async Task<DataContractBase> DeleteWorkOrderAsyncEXT(DCWorkOrderEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        if (string.IsNullOrEmpty(dc.WorkOrderName))
          throw new InternalModuleException($"Work order name is empty.", AlarmDefsBase.WorkOrderNameEmptyForRemoveOperation);

        await using var ctx = Context.Create();
        PRMWorkOrder workOrder = await WorkOrderHandler.GetWorkOrderByNameAsyncEXT(ctx, dc.WorkOrderName);

        if (workOrder is null)
          throw new InternalModuleException($"Work order {dc.WorkOrderName} not found.", AlarmDefsBase.RecordNotFound);

        ctx.PRMWorkOrders.Remove(workOrder);
        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.Schedule);
        HmiRefresh(HMIRefreshKeys.BundleWeighingMonitor);
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while removing work order {dc.WorkOrderName}.");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.WorkOrderNotDeleted,
          $"Unexpected error while removing product work order {dc.WorkOrderName}.", dc.WorkOrderName);
      }

      return result;
    }












































    //public Task<DataContractBase> CreateSteelgradeCatalogueAsync(DCSteelgrade steelgrade)
    //{
    //  throw new NotImplementedException();
    //}

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

    public Task<DataContractBase> CreateSteelgradeCatalogueAsync(DCSteelgrade steelgrade)
    {
      throw new NotImplementedException();
    }
    #endregion
  }
}
