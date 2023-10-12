using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.PEContext;
using PE.BaseDbEntity.Providers;
using PE.BaseDbEntity.TransferModels;
using PE.BaseInterfaces.Managers.DBA;
using PE.BaseInterfaces.SendOffices.DBA;
using PE.BaseModels.DataContracts.External.DBA;
using PE.BaseModels.DataContracts.Internal.DBA;
using PE.Common;
using PE.DBA.Base.Handlers;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Extensions;
using SMF.Core.Helpers;
using SMF.Core.Infrastructure;
using SMF.Core.Interfaces;
using SMF.Core.Notification;

namespace PE.DBA.Base.Managers
{
  public class L3DbCommunicationBaseManager : BaseManager, IL3DBCommunicationBaseManager
  {
    #region handlers

    protected readonly DataTransferHandlerBase DataTransferHandler;

    #endregion

    #region members

    protected readonly IDbAdapterBaseSendOffice SendOffice;
    private readonly IContextProvider<TransferContext> _context;

    #endregion

    #region ctor

    public L3DbCommunicationBaseManager(IModuleInfo moduleInfo, IDbAdapterBaseSendOffice sendOffice, IContextProvider<TransferContext> context) : base(moduleInfo)
    {
      SendOffice = sendOffice;
      _context = context;
      DataTransferHandler = new DataTransferHandlerBase();
    }

    #endregion

    #region interface

    public virtual async Task TransferWorkOrderDataFromTransferTableToAdapterAsync()
    {
      try
      {
        using (TransferContext ctx = _context.Create())
        {
          List<DCL3L2WorkOrderDefinitionExt> dcList = await ExtractDataFromTransferTableAsync(ctx);

          foreach (DCL3L2WorkOrderDefinitionExt dc in dcList)
          {
            DCL3L2WorkOrderDefinition internalDc = dc.ToInternal() as DCL3L2WorkOrderDefinition;
            SendOfficeResult<DCWorkOrderStatus> result =
              await SendOffice.SendWorkOrderDataToAdapterAsync(internalDc);

            if (result.OperationSuccess)
            {
              NotificationController.Info(String.Format("Work Order: {0} sent to adapter successfully",
                dc.WorkOrderName));

              try
              {
                await DataTransferHandler.UpdateTransferTableCommStatusesAsync(ctx, result.DataConctract);

                await ctx.SaveChangesAsync();
              }
              catch (Exception ex)
              {
                NotificationController.LogException(ex,
                  "Error during Work Order Definition {WorkOrderName} updating after sucess response from PRM",
                  dc.WorkOrderName);
              }
            }
            else
            {
              throw new InternalModuleException($"Error during transfering work order {dc.WorkOrderName} to adapter.", AlarmDefsBase.TransferDataFromTransferTableToAdapter);
            }
          }

          if (dcList.Any())
          {
            NotificationController.Info($"Received list of WO definitions ({dcList.Count})");
            HmiRefresh(HMIRefreshKeys.L3TransferTableWorkOrders);
          }
        }
      }
      catch (InternalModuleException ex)
      {
        NotificationController.LogException(ex, ex.Message);
        NotificationController.RegisterAlarm(AlarmDefsBase.TransferDataFromTransferTableToAdapter, ex.Message);
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, $"Error while transfering data from transfer table to adapter");
        NotificationController.RegisterAlarm(AlarmDefsBase.TransferDataFromTransferTableToAdapter,
          $"Error while transfering data from transfer table to adapter");
      }
    }

    public virtual async Task UpdateWorkOrdesWithTimeoutAsync()
    {
      try
      {
        await using var ctx = _context.Create();
        bool isUpdated = await DataTransferHandler.UpdateWorkOrdesWithTimeoutAsync(ctx);
        if (isUpdated)
        {
          await ctx.SaveChangesAsync();
          HmiRefresh(HMIRefreshKeys.L3TransferTableWorkOrders);
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, $"Error while updating work orders with timeout in transfer table");
        NotificationController.RegisterAlarm(AlarmDefsBase.TimeoutDuringProcessingWorkOrderTransferTable,
          $"Error while updating work orders with timeout in transfer table");
      }
    }

    public virtual async Task<DataContractBase> CreateWorkOrderDefinitionAsync(DCL3L2WorkOrderDefinitionExt dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = _context.Create();
        L3L2WorkOrderDefinition wod = new L3L2WorkOrderDefinition();

        DataTransferHandler.UpdateWorkOrderDefinition(wod, dc);

        await ctx.L3L2WorkOrderDefinitions.AddAsync(wod);
        await ctx.SaveChangesAsync();

        HmiRefresh(HMIRefreshKeys.L3TransferTableWorkOrders);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderDefinitionUpdateError,
          $"Unique key violation while updating work order definition.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderDefinitionUpdateError,
          $"Existing reference key violation while updating work order definition.");
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderDefinitionUpdateError,
          $"Unexpected error while updating work order definition.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> UpdateWorkOrderDefinitionAsync(DCL3L2WorkOrderDefinitionExt dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = _context.Create();
        L3L2WorkOrderDefinition wod = await DataTransferHandler.GetWorkOrderDefinitionByIdAsync(ctx, dc.Counter);

        if (wod.CommStatus != CommStatus.ProcessingError.Value &&
            wod.CommStatus != CommStatus.ValidationError.Value)
        {
          NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderDefinitionNotUpdatable,
            $"Work order definition {dc.WorkOrderName} status prevents update", dc.WorkOrderName);
          NotificationController.Warn(
            $"Work order definition {dc.WorkOrderName} status prevents update");
          return result;
        }

        DataTransferHandler.UpdateWorkOrderDefinition(wod, dc);

        await ctx.SaveChangesAsync();

        HmiRefresh(HMIRefreshKeys.L3TransferTableWorkOrders);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderDefinitionUpdateError,
          $"Unique key violation while updating work order definition.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderDefinitionUpdateError,
          $"Existing reference key violation while updating work order definition.");
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderDefinitionUpdateError,
          $"Unexpected error while updating work order definition.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> DeleteWorkOrderDefinitionAsync(DCL3L2WorkOrderDefinitionExt dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = _context.Create();
        L3L2WorkOrderDefinition wod = await DataTransferHandler.GetWorkOrderDefinitionByIdAsync(ctx, dc.Counter);

        if (wod == null ||
            (wod.CommStatus != CommStatus.ProcessingError.Value &&
             wod.CommStatus != CommStatus.ValidationError.Value))
        {
          NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderDefinitionAlreadyProcessed,
            "Can not remove work order definition which is already processed.");
          NotificationController.Error($"[CRITICAL] fun {MethodHelper.GetMethodName()} failed.");
        }

        ctx.L3L2WorkOrderDefinitions.Remove(wod);
        await ctx.SaveChangesAsync();

        HmiRefresh(HMIRefreshKeys.L3TransferTableWorkOrders);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderDefinitionDeleteError,
          $"Unique key violation while removing work order definition.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderDefinitionDeleteError,
          $"Existing reference key violation while removing work order definition.");
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderDefinitionDeleteError,
          $"Unexpected error while removing work order definition.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> ResetWorkOrderReportAsync(DCL2L3WorkOrderReportExt workOrderReport)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = _context.Create();
        L2L3WorkOrderReport wod =
          await DataTransferHandler.GetWorkOrderReportByIdAsync(ctx, workOrderReport.Counter);

        if (wod == null ||
            (wod.CommStatus != CommStatus.ProcessingError.Value &&
             wod.CommStatus != CommStatus.ValidationError.Value))
        {
          NotificationController.Error($"[CRITICAL] fun {MethodHelper.GetMethodName()} failed.");
        }

        wod.CommStatus = CommStatus.New.Value;
        await ctx.SaveChangesAsync();

        HmiRefresh(HMIRefreshKeys.L3TransferTableWorkOrderReports);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderReportNotResetted,
          $"Unique key violation while resetting work order report.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderReportNotResetted,
          $"Existing reference key violation while resetting work order report.");
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderReportNotResetted,
          $"Unexpected error while resetting work order report.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> ResetProductReportAsync(DCL2L3ProductReportExt dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = _context.Create();
        var cr = await DataTransferHandler.GetProductReportByIdAsync(ctx, dc.Counter);

        if (cr == null ||
            (cr.CommStatus != CommStatus.ProcessingError.Value &&
             cr.CommStatus != CommStatus.ValidationError.Value))
        {
          NotificationController.Error($"[CRITICAL] fun {MethodHelper.GetMethodName()} failed.");
        }

        cr.CommStatus = CommStatus.New.Value;
        await ctx.SaveChangesAsync();

        HmiRefresh(HMIRefreshKeys.L3TransferTableProductReports);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ProductReportNotResetted,
          $"Unique key violation while resetting product report.", dc.ProductName);
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ProductReportNotResetted,
          $"Existing reference key violation while resetting product report.", dc.ProductName);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ProductReportNotResetted,
          $"Unexpected error while resetting product report.", dc.ProductName);
      }

      return result;
    }

    public virtual async Task<DataContractBase> CreateWorkOrderReport(DCL2L3WorkOrderReport dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = _context.Create();
        L2L3WorkOrderReport workOrderReport = DataTransferHandler.CreateWorkOrderReport(ctx, dc);
        DataTransferHandler.AddWorkOrderReportToTransferTable(dc, ctx, workOrderReport);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.L3TransferTableWorkOrderReports);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderReportNotSent,
          $"Unique key violation while creating work order report.", dc.ProductName);
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderReportNotSent,
          $"Existing reference key violation while creating work order report.", dc.ProductName);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderReportNotSent,
          $"Unexpected error while creating work order report.", dc.ProductName);
      }

      return result;
    }

    public virtual async Task<DataContractBase> CreateProductReport(DCL2L3ProductReport dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = _context.Create();
        var coilReport = DataTransferHandler.CreateCoilReport(ctx, dc);
        DataTransferHandler.AddCoilReportToTransferTable(dc, ctx, coilReport);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.L3TransferTableProductReports);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ProductReportNotSent,
          $"Unique key violation while creating product report for product {dc.ProductName}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ProductReportNotSent,
          $"Existing reference key violation while creating product report for product {dc.ProductName}.");
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ProductReportNotSent,
          $"Unexpected error while creating product report for product {dc.ProductName}.");
      }

      return result;
    }

    #endregion

    #region private

    private async Task<List<DCL3L2WorkOrderDefinitionExt>> ExtractDataFromTransferTableAsync(TransferContext ctx)
    {
      IEnumerable<L3L2WorkOrderDefinition> toBeTransfered = await DataTransferHandler.GetDataToBeTransfered(ctx);

      List<DCL3L2WorkOrderDefinitionExt> dcList = new List<DCL3L2WorkOrderDefinitionExt>();

      foreach (L3L2WorkOrderDefinition data in toBeTransfered)
      {
        data.CommStatus = CommStatus.BeingProcessed.Value;
        data.UpdatedTs = DateTime.Now;

        NotificationController.Debug(string.Format("Preparing WorkOrder {0}.", data.WorkOrderName));

        DCL3L2WorkOrderDefinitionExt dc = null;

        try
        {
          dc = new DCL3L2WorkOrderDefinitionExt(data);
        }
        catch (Exception ex)
        {
          // In case of any errors during creating the DC
          NotificationController.LogException(ex,
            $"[INVALID DB DATA] - {MethodBase.GetCurrentMethod().ReflectedType.Name} when creating DC");
        }

        if (dc != null)
        {
          dcList.Add(dc);
        }
      }

      await ctx.SaveChangesAsync();

      return dcList;
    }

    #endregion
  }
}
