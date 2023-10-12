using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
//using PE.BaseDbEntity.PEContext;
using PE.BaseDbEntity.Providers;
using PE.BaseInterfaces.Managers.DBA;
using PE.Interfaces.SendOffices.DBA;
using PE.BaseModels.DataContracts.External.DBA;
using PE.BaseModels.DataContracts.Internal.DBA;
using PE.Common;
using PE.DBA.Base.Managers;
using PE.DBA.DataBaseAdapter.Handlers;
using PE.DbEntity.PEContext;
using PE.DbEntity.TransferModels;
using PE.Interfaces.Managers.DBA;
using PE.Models.DataContracts.External.DBA;
using PE.Models.DataContracts.Internal.DBA;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Extensions;
using SMF.Core.Helpers;
using SMF.Core.Infrastructure;
using SMF.Core.Interfaces;
using SMF.Core.Notification;
using SMF.DbEntity.Models;

namespace PE.DBA.DataBaseAdapter.Managers
{
  public class L3DBCommunicationManager : BaseManager,IL3DBCommunicationManager
  {
    #region handlers

    protected readonly DataTransferHandler DataTransferHandler;

    #endregion

    #region members

    protected readonly IDbAdapterSendOffice SendOffice;
    private readonly IContextProvider<TransferCustomContext> _context;

    #endregion

    #region ctor

    public L3DBCommunicationManager(IModuleInfo moduleInfo, IDbAdapterSendOffice sendOffice, IContextProvider<TransferCustomContext> context) : base(moduleInfo)
    {
      SendOffice = sendOffice;
      _context = context;
      DataTransferHandler = new DataTransferHandler();
    }

    #endregion

    #region interface
    //Added by Abhishek on 26092023
    public virtual async Task TransferBatchDataFromTransferTableToAdapterAsync()
    {
      try
      {
        using (TransferCustomContext ctx = _context.Create())
        {
          List<DCL3L2BatchData> dcList = await ExtractDataFromTransferTableAsync(ctx);

          foreach (DCL3L2BatchData dc in dcList)
          {
            DCL3L2BatchData internalDc = dc.ToInternal() as DCL3L2BatchData;
            SendOfficeResult<DCBatchDataStatus> result =
              await SendOffice.SendBatchDataToAdapterAsync(internalDc);

            if (result.OperationSuccess)
            {
              NotificationController.Info(String.Format("Batch Data: {0} sent to adapter successfully",
                dc.ProductName));

              try
              {
                await DataTransferHandler.UpdateTransferTableCommStatusesAsync(ctx, result.DataConctract);

                await ctx.SaveChangesAsync();
              }
              catch (Exception ex)
              {
                NotificationController.LogException(ex,
                  "Error during Batch Data Definition {BatchDataName} updating after sucess response from PRM",
                  dc.ProductName);
              }
            }
            else
            {
              throw new InternalModuleException($"Error during transfering work order {dc.ProductName} to adapter.", AlarmDefsBase.TransferDataFromTransferTableToAdapter);
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

    public virtual async Task UpdateBatchDataWithTimeoutAsync()
    {
      try
      {
        await using var ctx = _context.Create();
        bool isUpdated = await DataTransferHandler.UpdateBatchDataWithTimeoutAsync(ctx);
        if (isUpdated)
        {
          await ctx.SaveChangesAsync();
          HmiRefresh(HMIRefreshKeys.L3TransferTableWorkOrders);
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, $"Error while updating Batch Data with timeout in transfer table");
        NotificationController.RegisterAlarm(AlarmDefsBase.TimeoutDuringProcessingWorkOrderTransferTable,
          $"Error while updating batch data with timeout in transfer table");
      }
    }

    public virtual async Task<DCL3L2BatchData> CreateBatchDataAsync(DCL3L2BatchData dc)
    {
      DCL3L2BatchData result = new DCL3L2BatchData();

      try
      {
        await using var ctx = _context.Create();
        L3L2BatchDataDefinition wod = new L3L2BatchDataDefinition();

        DataTransferHandler.UpdateWorkOrderDefinition(wod, dc);

        await ctx.L3L2BatchDataDefinitions.AddAsync(wod);
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
    //Added by AP on 14-08-2023
    public virtual async Task<DCL3L2BatchData> UpdateBatchDataAsync(DCL3L2BatchData dc)
    {
      DCL3L2BatchData result = new DCL3L2BatchData();

      try
      {
        await using var ctx = _context.Create();
        L3L2BatchDataDefinition wod = await DataTransferHandler.GetWorkOrderDefinitionByIdAsync(ctx, dc.Counter);

        if (wod.CommStatus != CommStatus.ProcessingError.Value &&
            wod.CommStatus != CommStatus.ValidationError.Value)
        {
          NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderDefinitionNotUpdatable,
            $"Work order definition {dc.ProductName} status prevents update", dc.ProductName);
          NotificationController.Warn(
            $"Work order definition {dc.ProductName} status prevents update");
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

    //public virtual async Task<DataContractBase> DeleteWorkOrderDefinitionAsync(DCL3L2BatchDataExt dc)
    //{
    //  DataContractBase result = new DataContractBase();

    //  try
    //  {
    //    await using var ctx = _context.Create();
    //    L3L2BatchDatum wod = await DataTransferHandler.GetWorkOrderDefinitionByIdAsync(ctx, dc.Counter);

    //    if (wod == null ||
    //        (wod.CommStatus != CommStatus.ProcessingError.Value &&
    //         wod.CommStatus != CommStatus.ValidationError.Value))
    //    {
    //      NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderDefinitionAlreadyProcessed,
    //        "Can not remove work order definition which is already processed.");
    //      NotificationController.Error($"[CRITICAL] fun {MethodHelper.GetMethodName()} failed.");
    //    }

    //    ctx.L3L2BatchData.Remove(wod);
    //    await ctx.SaveChangesAsync();

    //    HmiRefresh(HMIRefreshKeys.L3TransferTableWorkOrders);
    //  }
    //  catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
    //  {
    //    ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderDefinitionDeleteError,
    //      $"Unique key violation while removing work order definition.");
    //  }
    //  catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
    //  {
    //    ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderDefinitionDeleteError,
    //      $"Existing reference key violation while removing work order definition.");
    //  }
    //  catch (Exception ex)
    //  {
    //    ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderDefinitionDeleteError,
    //      $"Unexpected error while removing work order definition.");
    //  }

    //  return result;
    //}

    //public virtual async Task<DataContractBase> CreateWorkOrderReport(DCL2L3WorkOrderReport dc)
    //{
    //  DataContractBase result = new DataContractBase();

    //  try
    //  {
    //    await using var ctx = _context.Create();
    //    L2L3WorkOrderReport workOrderReport = DataTransferHandler.CreateWorkOrderReport(ctx, dc);
    //    DataTransferHandler.AddWorkOrderReportToTransferTable(dc, ctx, workOrderReport);

    //    await ctx.SaveChangesAsync();
    //    HmiRefresh(HMIRefreshKeys.L3TransferTableWorkOrderReports);
    //  }
    //  catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
    //  {
    //    ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderReportNotSent,
    //      $"Unique key violation while creating work order report.", dc.ProductName);
    //  }
    //  catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
    //  {
    //    ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderReportNotSent,
    //      $"Existing reference key violation while creating work order report.", dc.ProductName);
    //  }
    //  catch (Exception ex)
    //  {
    //    ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.WorkOrderReportNotSent,
    //      $"Unexpected error while creating work order report.", dc.ProductName);
    //  }

    //  return result;
    //}

    //public virtual async Task<DataContractBase> CreateProductReport(DCL2L3ProductReport dc)
    //{
    //  DataContractBase result = new DataContractBase();

    //  try
    //  {
    //    await using var ctx = _context.Create();
    //    var coilReport = DataTransferHandler.CreateCoilReport(ctx, dc);
    //    DataTransferHandler.AddCoilReportToTransferTable(dc, ctx, coilReport);

    //    await ctx.SaveChangesAsync();
    //    HmiRefresh(HMIRefreshKeys.L3TransferTableProductReports);
    //  }
    //  catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
    //  {
    //    ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ProductReportNotSent,
    //      $"Unique key violation while creating product report for product {dc.ProductName}.");
    //  }
    //  catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
    //  {
    //    ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ProductReportNotSent,
    //      $"Existing reference key violation while creating product report for product {dc.ProductName}.");
    //  }
    //  catch (Exception ex)
    //  {
    //    ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ProductReportNotSent,
    //      $"Unexpected error while creating product report for product {dc.ProductName}.");
    //  }

    //  return result;
    //}

    //#endregion

    //#region private

    private async Task<List<DCL3L2BatchData>> ExtractDataFromTransferTableAsync(TransferCustomContext ctx)
    {
      IEnumerable<L3L2BatchDataDefinition> toBeTransfered = await DataTransferHandler.GetDataToBeTransfered(ctx);

      List<DCL3L2BatchData> dcList = new List<DCL3L2BatchData>();

      foreach (L3L2BatchDataDefinition data in toBeTransfered)
      {
        data.CommStatus = CommStatus.BeingProcessed.Value;
        data.UpdatedTs = DateTime.Now;

        NotificationController.Debug(string.Format("Preparing WorkOrder {0}.", data.PO_NO));

        DCL3L2BatchData dc = null;

        try
        {
          dc = new DCL3L2BatchData(data);
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
