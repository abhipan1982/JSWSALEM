using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EFCoreExtensions;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseDbEntity.Providers;
using PE.BaseInterfaces.Managers.PRM;
using PE.BaseInterfaces.SendOffices.PRM;
using PE.BaseModels.DataContracts.Internal.DBA;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.Common;
using PE.Core;
using PE.Helpers;
using PE.PRM.Base.Handlers;
using PE.PRM.Base.Handlers.Models;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Extensions;
using SMF.Core.Helpers;
using SMF.Core.Infrastructure;
using SMF.Core.Interfaces;
using SMF.Core.Notification;
using SMF.Module.Core;
using SMF.Module.Parameter;
using PE.Interfaces.Managers.PRM;
using PE.PRM.Base.Managers;
using PE.DbEntity.PEContext;



using PE.Models.DataContracts.Internal.PRM;
using PE.DbEntity.Models;
using PE.DbEntity.Providers;
using PE.PRM.ProdManager.Handler;
using PE.PRM.ProdManager.Handlers;
using PE.DbEntity.HmiModels;
using SMF.Module.Core.Interfaces;
using PE.Models.DataContracts.Internal.DBA;
using PE.Models.DataContracts.External.DBA;
using PE.BaseModels.DataContracts.Internal.PPL;
using PE.PRM.ProdManager.Handler.Models;

namespace PE.PRM.ProdManager
{
  public class BatchDataManager : BaseManager,IWorkOrderManager 
  {
    #region fields

    protected readonly IProdManagerWorkOrderBaseSendOffice SendOffice;
    protected readonly IContextProvider<PEContext> Context;
    protected readonly ICustomContextProvider<PECustomContext> CustomContext;
    protected readonly ICustomContextProvider<HmiContext> HmiContext;
    protected internal bool UseExistingMaterialCatalogueForProcessingTransferObject;
    protected internal bool UseExistingProductCatalogueForProcessingTransferObject;
    protected internal ValidatedBatchData workOrderData;
    private readonly Nito.AsyncEx.AsyncLock _mutex = new Nito.AsyncEx.AsyncLock();

    #region handlers

    protected readonly ProductCatalogueHandler ProductCatalogueHandler;
    protected readonly HeatHandler HeatHandler;
    protected readonly SteelgradeHandler SteelgradeHandler;
    protected readonly MaterialCatalogueHandler MaterialCatalogueHandler;
    protected readonly MaterialHandler MaterialHandler;
    protected readonly WorkOrderHandler WorkOrderHandler = new WorkOrderHandler();
    protected readonly PE.PRM.ProdManager.Handlers.CustomerHandler CustomerHandler;
    protected readonly BilletYardHandler BilletYardHandler;    
    protected readonly BatchDataTransferValidationHandler BatchDataTransferValidationHandler;

    #endregion

    #endregion

    #region ctor

    public BatchDataManager(IModuleInfo moduleInfo, IProdManagerWorkOrderBaseSendOffice sendOffice, IContextProvider<PEContext> context, ICustomContextProvider<PECustomContext> customContext,//@av
      ICustomContextProvider<HmiContext> hmiContext) :
      base(moduleInfo)
    {
      SendOffice = sendOffice;
      CustomContext = customContext;
      HmiContext = hmiContext;
      Context = context;
      ProductCatalogueHandler = new ProductCatalogueHandler();
      HeatHandler = new HeatHandler();
      SteelgradeHandler = new SteelgradeHandler();
      MaterialCatalogueHandler = new MaterialCatalogueHandler();
      MaterialHandler = new MaterialHandler();
      WorkOrderHandler = new WorkOrderHandler();
      CustomerHandler = new PE.PRM.ProdManager.Handlers.CustomerHandler();
      BilletYardHandler = new BilletYardHandler();    

      if (!UnitTestDetector.IsInUnitTest)
      {
        ModuleController.ParametersChanged += ModuleController_ParametersChanged;

        UseExistingMaterialCatalogueForProcessingTransferObject =
          ParameterController.GetParameterStatic("MaterialCatalogueIsUsed").ValueInt.GetValueOrDefault() == 1;
        UseExistingProductCatalogueForProcessingTransferObject =
          ParameterController.GetParameterStatic("ProductCatalogueIsUsed").ValueInt.GetValueOrDefault() == 1;
      }
    }
    //Av@281123
    protected virtual void ModuleController_ParametersChanged(object sender, ParametersChangedEventArgs e)
    {
      UseExistingMaterialCatalogueForProcessingTransferObject =
        ParameterController.GetParameterStatic("MaterialCatalogueIsUsed").ValueInt.GetValueOrDefault() == 1;
      UseExistingProductCatalogueForProcessingTransferObject =
        ParameterController.GetParameterStatic("ProductCatalogueIsUsed").ValueInt.GetValueOrDefault() == 1;
    }
    #endregion

    #region func


    //@av
    public virtual async Task<DataContractBase> CreateWorkOrderAsyncEXT(DCWorkOrderEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        WorkOrderHandler.CreateWorkOrderByUserEXT(ctx, dc);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.Schedule);
        HmiRefresh(HMIRefreshKeys.BundleWeighingMonitor);
        NotificationController.Info($"Work order {dc.WorkOrderName} has been created by user.");
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while creating batch data {dc.WorkOrderName}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while creating batch data {dc.WorkOrderName}.");
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.WorkOrderNotCreated,
          $"Unexpected error while creating batch data {dc.WorkOrderName}.", dc.WorkOrderName);
      }

      return result;
    }

    public virtual async Task<DataContractBase> UpdateWorkOrderAsyncEXT(DCWorkOrderEXT dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        PRMWorkOrder workOrder = await WorkOrderHandler.GetWorkOrderByIdAsyncEXT(ctx, dc.WorkOrderId);
        if (workOrder is null)
          throw new InternalModuleException($"Work order {dc.WorkOrderName} [{dc.WorkOrderId}] was not found.",
            AlarmDefsBase.WorkOrderNotFound, dc.WorkOrderName);

        WorkOrderHandler.UpdateWorkOrderByUserEXT(ctx, workOrder, dc);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.Schedule);
        HmiRefresh(HMIRefreshKeys.BundleWeighingMonitor);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while updating batch data {dc.WorkOrderName} [{dc.WorkOrderId}].");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while updating batch data {dc.WorkOrderName} [{dc.WorkOrderId}].");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.WorkOrderNotUpdated,
          $"Unexpected error while updating batch data {dc.WorkOrderName} [{dc.WorkOrderId}].", dc.WorkOrderName);
      }

      return result;
    }


    //Av@281123

    public virtual async Task<DCBatchDataStatus> ProcessWorkOrderDataAsync(DCL3L2BatchDataDefinition dc)
    {
      DCBatchDataStatus backMsg = new DCBatchDataStatus { Counter = dc.Counter };

      if (!BatchDataTransferValidationHandler.ValidateBatchDataDefinition(dc, backMsg, UseExistingMaterialCatalogueForProcessingTransferObject, UseExistingProductCatalogueForProcessingTransferObject, out ValidatedBatchData workOrderData))
      {
        backMsg.Status = CommStatus.ValidationError;
        return backMsg;
      }

      // Work order creating
      try
      {
        await using var ctx = CustomContext.Create();
        var dbValidationResult = await ValidateTransferDataOnDatabase(ctx, workOrderData, backMsg);

        if (!dbValidationResult.IsValid)
        {
          return dbValidationResult.DcBatchDataStatus;
        }

        MVHAsset reception = await BilletYardHandler.GetReceptionEXT(ctx);
        List<PRMMaterial> materials = MaterialHandler.CreateMaterials(reception,
          workOrderData.WorkOrderName,
          workOrderData.NumberOfBillets,
          dbValidationResult.MaterialCatalogue,
          dbValidationResult.Heat,
          workOrderData.BilletWeight,
          workOrderData.InputThickness,
          workOrderData.InputLength,
          workOrderData.InputWidth);

        PRMWorkOrder workOrder = await WorkOrderHandler.GetWorkOrderByNameAsyncEXT(ctx, workOrderData.WorkOrderName);

        if (workOrder != null) // if batch data with this name don't exists
        {
          NotificationController.Info($"Work order: {workOrder.WorkOrderName} exists");
          if (workOrder.EnumWorkOrderStatus <= WorkOrderStatus.Scheduled) // if batch data can be updated
          {
            MaterialHandler.DeleteOldMaterialsAfterWoUpdate(ctx, workOrder.WorkOrderId);
            workOrder = await WorkOrderHandler.UpdateWorkOrderAsync(ctx,
              dbValidationResult.ProductCatalogue.ProductCatalogueId,
              dbValidationResult.Customer,
              dbValidationResult.MaterialCatalogue,
              materials,
              dbValidationResult.Heat,
              dbValidationResult.Steelgrade,
              dbValidationResult.ParentWorkOrder,
              workOrderData);

            NotificationController.Info("[WO:{workorder}] Work order updating...", workOrder.WorkOrderName);
          }
          else
          {
            NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotUpdatable,
              $"Work order [{workOrder.WorkOrderName}] status does not allow it to update", workOrder.WorkOrderName);
            NotificationController.Warn(
              $"Work order {workOrder.WorkOrderName} [{workOrder.WorkOrderId}] status {workOrder.EnumWorkOrderStatus.Value} does not allow it to update", workOrder.WorkOrderId);
            backMsg.Status = CommStatus.ValidationError;
            return backMsg;
          }
        }
        else
        {
          workOrder = WorkOrderHandler.CreateWorkOrder(ctx,
            workOrderData.AmISimulated,
            workOrderData.TargetWorkOrderWeight,
            materials,
            dbValidationResult.Steelgrade,
            dbValidationResult.Heat,
            dbValidationResult.MaterialCatalogue,
            dbValidationResult.ProductCatalogue.ProductCatalogueId,
            dbValidationResult.Customer,
            workOrderData.L3CreatedTs,
            workOrderData.WorkOrderName,
            workOrderData.ExternalWorkOrderName,
            workOrderData.OrderDeadline,
            workOrderData.BundleWeightMin,
            workOrderData.BundleWeightMax,
            workOrderData.TargetWorkOrderWeightMin,
            workOrderData.TargetWorkOrderWeightMax);

          workOrder.FKHeat = dbValidationResult.Heat;
          workOrder.FKParentWorkOrder = dbValidationResult.ParentWorkOrder;
          ctx.PRMWorkOrders.Add(workOrder);

          NotificationController.Info($"Creating batch data {workOrder.WorkOrderName}...");
        }

        backMsg.Status = CommStatus.OK;
        var saveChangesWithValidationResult = await ctx.SaveChangesWithValidationAsync();

        if (!string.IsNullOrWhiteSpace(saveChangesWithValidationResult.ErrorValidationMessages))
        {
          backMsg.Status = CommStatus.ValidationError;
          backMsg.BackMessage += saveChangesWithValidationResult.ErrorValidationMessages;

          return backMsg;
        }

        HmiRefresh(HMIRefreshKeys.WorkOrder);

        //--------------******************** S I M U L A T I O N ******************** ---------------------------------------
        if (workOrderData.AmISimulated)
        {
          //SendOfficeResult<DataContractBase> result =
          //  await SendOffice.AutoScheduleWorkOrderAsync(new DCWorkOrderToSchedule
          //  {
          //    WorkOrderId = workOrder.WorkOrderId
          //  });

          //if (result.OperationSuccess)
          //{
          //  NotificationController.Info("[WO:{workorder}] Forwarding batch data to schedule module - success",
          //    workOrder.WorkOrderName);
          //}
          //else
          //{
          //  NotificationController.Error(
          //    "Forwarding batch data to schedule module - error. Work order will not be scheduled automatically");
          //}
        }
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while creating batch data {dc.BatchNo}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while creating batch data {dc.BatchNo}.");
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.WorkOrderNotCreated,
          $"Unexpected error while creating batch data {dc.BatchNo}.", dc.BatchNo);
      }

      return backMsg;
    }

    private async Task<BatchDataValidationResult> ValidateTransferDataOnDatabase(PECustomContext ctx,
      ValidatedBatchData batchData, DCBatchDataStatus backMsg)
    {
      var validator = new BatchDataValidator(ctx,
        SteelgradeHandler,
        ProductCatalogueHandler,
        CustomerHandler,
        MaterialCatalogueHandler,
        HeatHandler,
        WorkOrderHandler,
        workOrderData,
        UseExistingProductCatalogueForProcessingTransferObject,
        UseExistingMaterialCatalogueForProcessingTransferObject);

      return await validator
        .Initialize(backMsg)
        .AddValidatorAsync(validator.ValidateSteelGrade)
        .AddValidatorAsync(validator.ValidateOrderDeadline)
        .AddValidatorAsync(validator.ValidateProductCatalogue)
        .AddValidatorAsync(validator.ValidateOrCreateMaterialCatalogue)
        .AddValidatorAsync(validator.ValidateOrCreateHeat)
        .AddValidatorAsync(validator.ValidateOrCustomer)
        .AddValidatorAsync(validator.ValidateParentWorkOrder)
        .AddValidatorAsync(validator.ValidateTargetWorkOrderWeight)
        .RunAsync();
    }







    public Task<DCWorkOrderStatusExt> ProcessWorkOrderDataAsync(DCL3L2WorkOrderDefinitionExtMOD message)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> CreateWorkOrderAsync(DCWorkOrderEXT workOrder)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> UpdateWorkOrderAsync(DCWorkOrderEXT workOrder)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> SendWorkOrderReportAsync(DCWorkOrderConfirmation workOrder)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> SendProductReportAsync(DCRawMaterial coil)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> CheckShiftsWorkOrderStatusses(DCShiftCalendarId arg)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> ProcessWorkOrderStatus(DCRawMaterial arg)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> AddTestWorkOrderToScheduleAsync(DCTestSchedule dc)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> UpdateCanceledWorkOrderAsync(DCWorkOrderCancel dc)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> UpdateUnCanceledWorkOrderAsync(DCWorkOrderCancel dc)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> UpdateBlockedWorkOrderAsync(DCWorkOrderBlock dc)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> UpdateUnBlockedWorkOrderAsync(DCWorkOrderBlock dc)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> EndOfWorkOrderAsync(WorkOrderId dc)
    {
      throw new NotImplementedException();
    }

    //public Task<Models.DataContracts.Internal.DBA.DCWorkOrderStatus> ProcessBatchDataAsync(DCL3L2WorkOrderDefinitionMOD message)
    //{
    //  throw new NotImplementedException();
    //}
    //Added on 06/12/2023
    public virtual async Task<DCBatchDataStatus> ProcessBatchDataAsync(DCL3L2BatchDataDefinition message)
    {
      DCBatchDataStatus backMsg = new DCBatchDataStatus { Counter = message.Counter };

      if (!BatchDataTransferValidationHandler.ValidateBatchDataDefinition(message, backMsg, UseExistingMaterialCatalogueForProcessingTransferObject, UseExistingProductCatalogueForProcessingTransferObject, out ValidatedBatchData batchData))
      {
        backMsg.Status = CommStatus.ValidationError;
        return backMsg;
      }

      // Work order creating
      try
      {
        await using var ctx = CustomContext.Create();
        var dbValidationResult = await ValidateTransferDataOnDatabase(ctx, batchData, backMsg);

        if (!dbValidationResult.IsValid)
        {
          return dbValidationResult.DcBatchDataStatus;
        }

        MVHAsset reception = await BilletYardHandler.GetReceptionEXT(ctx);
        List<PRMMaterial> materials = MaterialHandler.CreateMaterials(reception,
          workOrderData.WorkOrderName,
          workOrderData.NumberOfBillets,
          dbValidationResult.MaterialCatalogue,
          dbValidationResult.Heat,
          workOrderData.BilletWeight,
          workOrderData.InputThickness,
          workOrderData.InputLength,
          workOrderData.InputWidth);

        PRMWorkOrder workOrder = await WorkOrderHandler.GetWorkOrderByNameAsyncEXT(ctx, workOrderData.WorkOrderName);

        if (workOrder != null) // if batch data with this name don't exists
        {
          NotificationController.Info($"Work order: {workOrder.WorkOrderName} exists");
          if (workOrder.EnumWorkOrderStatus <= WorkOrderStatus.Scheduled) // if batch data can be updated
          {
            MaterialHandler.DeleteOldMaterialsAfterWoUpdate(ctx, workOrder.WorkOrderId);
            workOrder = await WorkOrderHandler.UpdateWorkOrderAsync(ctx,
              dbValidationResult.ProductCatalogue.ProductCatalogueId,
              dbValidationResult.Customer,
              dbValidationResult.MaterialCatalogue,
              materials,
              dbValidationResult.Heat,
              dbValidationResult.Steelgrade,
              dbValidationResult.ParentWorkOrder,
              workOrderData);

            NotificationController.Info("[WO:{workorder}] Work order updating...", workOrder.WorkOrderName);
          }
          else
          {
            NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotUpdatable,
              $"Work order [{workOrder.WorkOrderName}] status does not allow it to update", workOrder.WorkOrderName);
            NotificationController.Warn(
              $"Work order {workOrder.WorkOrderName} [{workOrder.WorkOrderId}] status {workOrder.EnumWorkOrderStatus.Value} does not allow it to update", workOrder.WorkOrderId);
            backMsg.Status = CommStatus.ValidationError;
            return backMsg;
          }
        }
        else
        {
          workOrder = WorkOrderHandler.CreateWorkOrder(ctx,
            workOrderData.AmISimulated,
            workOrderData.TargetWorkOrderWeight,
            materials,
            dbValidationResult.Steelgrade,
            dbValidationResult.Heat,
            dbValidationResult.MaterialCatalogue,
            dbValidationResult.ProductCatalogue.ProductCatalogueId,
            dbValidationResult.Customer,
            workOrderData.L3CreatedTs,
            workOrderData.WorkOrderName,
            workOrderData.ExternalWorkOrderName,
            workOrderData.OrderDeadline,
            workOrderData.BundleWeightMin,
            workOrderData.BundleWeightMax,
            workOrderData.TargetWorkOrderWeightMin,
            workOrderData.TargetWorkOrderWeightMax);

          workOrder.FKHeat = dbValidationResult.Heat;
          workOrder.FKParentWorkOrder = dbValidationResult.ParentWorkOrder;
          ctx.PRMWorkOrders.Add(workOrder);

          NotificationController.Info($"Creating batch data {workOrder.WorkOrderName}...");
        }

        backMsg.Status = CommStatus.OK;
        var saveChangesWithValidationResult = await ctx.SaveChangesWithValidationAsync();

        if (!string.IsNullOrWhiteSpace(saveChangesWithValidationResult.ErrorValidationMessages))
        {
          backMsg.Status = CommStatus.ValidationError;
          backMsg.BackMessage += saveChangesWithValidationResult.ErrorValidationMessages;

          return backMsg;
        }

        HmiRefresh(HMIRefreshKeys.WorkOrder);

        //--------------******************** S I M U L A T I O N ******************** ---------------------------------------
        if (batchData.AmISimulated)
        {
          SendOfficeResult<DataContractBase> result =
            await SendOffice.AutoScheduleWorkOrderAsync(new DCWorkOrderToSchedule
            {
              WorkOrderId = workOrder.WorkOrderId
            });

          if (result.OperationSuccess)
          {
            NotificationController.Info("[WO:{workorder}] Forwarding batch data to schedule module - success",
              workOrder.WorkOrderName);
          }
          else
          {
            NotificationController.Error(
              "Forwarding batch data to schedule module - error. Work order will not be scheduled automatically");
          }
        }
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), message, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while creating batch data {message.BatchNo}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), message, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while creating batch data {message.BatchNo}.");
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), message, AlarmDefsBase.WorkOrderNotCreated,
          $"Unexpected error while creating batch data {message.BatchNo}.", message.BatchNo);
      }

      return backMsg;
    }

    public Task<BaseModels.DataContracts.Internal.DBA.DCWorkOrderStatus> ProcessWorkOrderDataAsync(DCL3L2WorkOrderDefinition message)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> CreateWorkOrderAsync(DCWorkOrder workOrder)
    {
      throw new NotImplementedException();
    }

    public Task<DataContractBase> UpdateWorkOrderAsync(DCWorkOrder workOrder)
    {
      throw new NotImplementedException();
    }
  }
}
#endregion
