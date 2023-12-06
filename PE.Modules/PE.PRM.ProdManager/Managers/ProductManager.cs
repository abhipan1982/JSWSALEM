using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseInterfaces.Managers.PRM;
using PE.BaseInterfaces.SendOffices.PRM;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.Common;
using PE.Helpers;
using PE.PRM.Base.Handlers;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Extensions;
using SMF.Core.Helpers;
using SMF.Core.Infrastructure;
using SMF.Core.Interfaces;

namespace PE.PRM.Base.Managers
{
  public class ProductManager : BaseManager, PE.Interfaces.Managers.PRM.IProductManager
  {
    #region members

    protected readonly IProdManagerProductBaseSendOffice SendOffice;

    #endregion

    #region handlers

    protected readonly MaterialHandlerBase MaterialHandler;
    protected readonly ProductHandlerBase ProductHandler;
    protected readonly WorkOrderHandlerBase WorkOrderHandler;

    #endregion

    #region ctor

    public ProductManager(IModuleInfo moduleInfo, IProdManagerProductBaseSendOffice sendOffice, ProductHandlerBase productHandler, MaterialHandlerBase materialHandler, WorkOrderHandlerBase workOrderHandler) : base(moduleInfo)
    {
      SendOffice = sendOffice;
      MaterialHandler = materialHandler;
      ProductHandler = productHandler;
      WorkOrderHandler = workOrderHandler;
    }

    #endregion

    /// <summary>
    ///   After production end trigger hits, module have to create bundle. Product can be created with or without work order relation
    /// </summary>
    /// <param name="dc"></param>
    /// <returns></returns>
    public virtual async Task<DCProductData> ProcessBundleProductionFinishAsync(DCBundleData dc)
    {
      DCProductData result = null;

      try
      {
        await using var ctx = new PEContext();
        var productName = "";

        if (dc.WorkOrderId.HasValue)
        {
          var lastProduct = await ProductHandler.GetLastProductInWorkOrder(ctx, dc.WorkOrderId.Value);

          if (lastProduct is not null)
          {
            var productNumber = Convert.ToInt16(lastProduct.ProductName[(lastProduct.ProductName.LastIndexOf('_') + 1)..]) + 1;
            productName = $"B_{lastProduct.FKWorkOrder.WorkOrderName}_{productNumber}";
          }
          else
          {
            var workOrderName = (await WorkOrderHandler.GetWorkOrderByIdAsync(ctx, dc.WorkOrderId.Value)).WorkOrderName;
            productName = $"B_{workOrderName}_1";
          }
        }
        else
          productName = $"B_{dc.Date:yyyyMMddHHmmssfff}";

        if (string.IsNullOrEmpty(productName))
          throw new InternalModuleException($"Product name was not generated. ", AlarmDefsBase.CannotGenerateProductName);

        var product = ProductHandler.CreateProduct(productName, dc.WorkOrderId, dc.OverallWeight, dc.Date, dc.BarsCounter, dc.BundleWeightSource);

        await ctx.PRMProducts.AddAsync(product);
        await ctx.SaveChangesAsync();

        result = new DCProductData { ProductId = product.ProductId };

        if (dc.WorkOrderId.HasValue)
          TaskHelper.FireAndForget(async () => await SendOffice.CalculateWorkOrderKPIsAsync(new() { WorkOrderId = dc.WorkOrderId.Value }),
            "CalculateWorkOrderKPIsAsync has failed.");

        HmiRefresh(HMIRefreshKeys.Products);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while creating product");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while creating product");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ProductCreationFailed,
          $"Unexpected error while creating product");
      }

      return result;
    }

    /// <summary>
    ///   After production end trigger hits, module have to create product for produced material. Product can be created with or without work order relation
    /// </summary>
    /// <param name="dc"></param>
    /// <returns></returns>
    public virtual async Task<DCProductData> ProcessCoilProductionFinishAsync(DCCoilData dc)
    {
      DCProductData result = null;

      try
      {
        await using var ctx = new PEContext();
        var productName = "";
        long? workOrderId = null;

        if (dc.FKMaterialId is not null)
        {
          var material = await MaterialHandler.GetMaterialByIdAsync(ctx, dc.FKMaterialId.Value);
          workOrderId = material.FKWorkOrderId;
          productName = material.MaterialName;
          material.FKWorkOrder.WorkOrderEndTs = DateTime.Now;
        }

        if (!workOrderId.HasValue)
        {
          var rawMaterial = await MaterialHandler.GetRawMaterialByIdAsync(ctx, dc.RawMaterialId);
          productName = rawMaterial.RawMaterialName;
        }

        if (string.IsNullOrEmpty(productName))
          throw new InternalModuleException($"Product name was not generated. ", AlarmDefsBase.CannotGenerateProductName);

        var product = ProductHandler.CreateProduct(productName, workOrderId, dc.OverallWeight, dc.Date, 1, WeightSource.Undefined);

        ctx.PRMProducts.Add(product);
        await ctx.SaveChangesAsync();

        result = new DCProductData { ProductId = product.ProductId, RawMaterialId = dc.RawMaterialId };

        if (workOrderId.HasValue)
          TaskHelper.FireAndForget(async () => await SendOffice.CalculateWorkOrderKPIsAsync(new() { WorkOrderId = workOrderId.Value }),
            "CalculateWorkOrderKPIsAsync has failed.");

        HmiRefresh(HMIRefreshKeys.Products);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while creating product");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while creating product");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ProductCreationFailed,
          $"Unexpected error while creating product");
      }

      return result;
    }
  }
}
