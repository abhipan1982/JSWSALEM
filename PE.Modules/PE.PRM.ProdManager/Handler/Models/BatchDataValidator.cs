using System;
using System.Threading.Tasks;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.DBA;
using PE.Core.UniversalValidator;
using PE.Models.DataContracts.Internal.DBA;
using PE.PRM.Base.Managers;

using PE.PRM.ProdManager.Handlers;
using SMF.Core.Notification;

namespace PE.PRM.ProdManager.Handler.Models
{
  public class BatchDataValidator : ValidatorBase<BatchDataValidationResult>
  {
    #region Fields

    protected readonly SteelgradeHandler _steelgradeHandler;
    protected readonly CustomerHandler _customerHandler;
    protected readonly ProductCatalogueHandler _productCatalogueHandler;
    protected readonly MaterialCatalogueHandler _materialCatalogueHandler;
    protected readonly HeatHandler _heatHandler;
    protected readonly WorkOrderHandler _workOrderHandler;
    protected readonly PEContext _ctx;
    protected readonly ValidatedBatchData _batchData;
    protected readonly bool _useExistingProductCatalogueForProcessingTransferObject;
    protected readonly bool _useExistingMaterialCatalogueForProcessingTransferObject;

    #endregion

    #region ctor

    public BatchDataValidator(PEContext ctx,
      SteelgradeHandler steelgradeHandler,
      ProductCatalogueHandler productCatalogueHandler,
      CustomerHandler customerHandler,
      MaterialCatalogueHandler materialCatalogueHandler,
      HeatHandler heatHandler,
      WorkOrderHandler workOrderHandler,
      ValidatedBatchData batchData,
      bool useExistingProductCatalogueForProcessingTransferObject,
      bool useExistingMaterialCatalogueForProcessingTransferObject)
    {
      _ctx = ctx;
      _steelgradeHandler = steelgradeHandler;
      _productCatalogueHandler = productCatalogueHandler;
      _customerHandler = customerHandler;
      _materialCatalogueHandler = materialCatalogueHandler;
      _heatHandler = heatHandler;
      _workOrderHandler = workOrderHandler;
      _batchData = batchData;
      _useExistingProductCatalogueForProcessingTransferObject = useExistingProductCatalogueForProcessingTransferObject;
      _useExistingMaterialCatalogueForProcessingTransferObject =useExistingMaterialCatalogueForProcessingTransferObject;
    }

    #endregion

    public ValidatorBase<BatchDataValidationResult> Initialize(DCBatchDataStatus batchDataStatus)
    {
      Result.DcBatchDataStatus = batchDataStatus;

      return this;
    }

    #region Validation methods

    public virtual async Task<BatchDataValidationResult> ValidateSteelGrade()
    {
      if (!string.IsNullOrWhiteSpace(_batchData.SteelgradeCode))
      {
        Result.Steelgrade = await _steelgradeHandler.GetSteelgradeByCodeAsyncEXT(_ctx, _batchData.SteelgradeCode);
      }

      if (Result.Steelgrade != null)
      {
        return Result;
      }

      SteelgradeNotValid();

      return Result;
    }

    public virtual Task<BatchDataValidationResult> ValidateOrderDeadline()
    {
      if (_batchData.OrderDeadline >= DateTime.Now)
      {
        return Task.FromResult(Result);
      }

      OrderDeadlineNotValid();

      return Task.FromResult(Result);
    }

    public virtual async Task<BatchDataValidationResult> ValidateOrCustomer()
    {
      if (string.IsNullOrWhiteSpace(_batchData.CustomerName))
      {
        return Result;
      }

      Result.Customer =
        await _customerHandler.GetCustomerByNameOrCreateNewAsync(_ctx, _batchData.CustomerName,
          Result.DcBatchDataStatus);

      if (Result.Customer != null)
      {
        return Result;
      }

      CustomerNotValid();

      return Result;
    }

    public virtual async Task<BatchDataValidationResult> ValidateProductCatalogue()
    {
      if (!string.IsNullOrWhiteSpace(_batchData.ProductCatalogue))
      {
        Result.ProductCatalogue =
          await _productCatalogueHandler.GetProductCatalogueByNameAsync(_ctx, _batchData.ProductCatalogue);
      }

      if (Result.ProductCatalogue != null)
      {
        return Result;
      }

      if (_useExistingProductCatalogueForProcessingTransferObject)
      {
        ExistingProductCatalogueNotValid();
      }
      else
      {
        Result.ProductCatalogue =
          await _productCatalogueHandler.GetProductCatalogueByItsParameters(_ctx,
            _batchData.OutputShapeSymbol,
            _batchData.OutputThickness,
            _batchData.OutputWidth);

        if (Result.ProductCatalogue != null)
        {
          return Result;
        }

        ExistingProductCatalogueWithParametersNotValid();
      }

      return Result;
    }

    public virtual async Task<BatchDataValidationResult> ValidateOrCreateMaterialCatalogue()
    {
      if (!string.IsNullOrWhiteSpace(_batchData.MaterialCatalogue))
      {
        Result.MaterialCatalogue =
          await _materialCatalogueHandler.GetMaterialCatalogueByNameAsyncEXT(_ctx, _batchData.MaterialCatalogue);
      }

      if (Result.MaterialCatalogue != null)
      {
        var shapeCorrect = string.IsNullOrWhiteSpace(_batchData.InputShapeSymbol) ||
          string.Equals(Result.MaterialCatalogue.FKShape.ShapeCode, _batchData.InputShapeSymbol, StringComparison.InvariantCultureIgnoreCase);

        var thicknessCorrect = _batchData.InputThickness >= Result.MaterialCatalogue.ThicknessMin &&
          _batchData.InputThickness <= Result.MaterialCatalogue.ThicknessMax;
        if (!shapeCorrect || !thicknessCorrect)
        {
          ExistingMaterialCatalogueNotFit();
        }

        return Result;
      }

      if (_useExistingMaterialCatalogueForProcessingTransferObject)
      {
        ExistingMaterialCatalogueNotValid();
      }
      else
      {
        try
        {
          Result.MaterialCatalogue =
            await _materialCatalogueHandler.GetExistingMaterialCatalogueByParametersAsync(_ctx, _batchData);
        }
        catch (InvalidOperationException e)
        {
          MoreThanOneMaterialCatalogueWithParametersExist();
        }

        if (Result.MaterialCatalogue == null)
        {
          try
          {
            Result.MaterialCatalogue = await _materialCatalogueHandler.CreateMaterialCatalogueByParametersAsync(_ctx, _batchData);
            _ctx.PRMMaterialCatalogues.Add(Result.MaterialCatalogue);

            MaterialCatalogueWithParametersCreated();
          }
          catch (Exception e)
          {
            CannotCreateMaterialCatalogueWithParameters(e);
          }
        }
      }

      return Result;
    }

    public virtual async Task<BatchDataValidationResult> ValidateOrCreateHeat()
    {
      Result.Heat = await _heatHandler.GetHeatByNameAsyncEXT(_ctx, _batchData.HeatName);

      if (Result.Heat != null)
      {
        if (Result.Heat.FKSteelgradeId != null && Result.Heat.FKSteelgradeId != Result.Steelgrade.SteelgradeId)
        {
          ExistingHeatNotValid();
        }

        return Result;
      }

      Result.Heat = await _heatHandler.CreateNewHeatAsync(_ctx, Result.Steelgrade.SteelgradeId,
        _batchData.HeatName);
      _ctx.PRMHeats.Add(Result.Heat);

      NotificationController.Info("[WO:{workorder}] New heat for : {heat} creating...", _batchData.WorkOrderName,
        _batchData.HeatName);
      Result.DcBatchDataStatus.BackMessage += " Heat created; ";


      return Result;
    }

    public virtual async Task<BatchDataValidationResult> ValidateParentWorkOrder()
    {
      if (string.IsNullOrWhiteSpace(_batchData.PreviousWorkOrderName))
      {
        return Result;
      }

      Result.ParentWorkOrder =
        await _workOrderHandler.GetWorkOrderByNameAsyncEXT(_ctx, _batchData.PreviousWorkOrderName);

      if (Result.ParentWorkOrder != null)
      {
        return Result;
      }

      ParentWorkOrderNotFound();

      return Result;
    }

    public virtual Task<BatchDataValidationResult> ValidateTargetWorkOrderWeight()
    {
      if (_batchData.TargetWorkOrderWeight > _batchData.TargetWorkOrderWeightMax ||
        _batchData.TargetWorkOrderWeight < _batchData.TargetWorkOrderWeightMin)
      {
        TargetWorkOrderWeightNotValid();
      }

      return Task.FromResult(Result);
    }
    #endregion

    #region protected methods

    protected virtual void SteelgradeNotValid()
    {
      NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotCreated,
        $"Error while updating Work Order {0} - Steelgrade not found {_batchData.SteelgradeCode} ",
        _batchData.WorkOrderName);
      NotificationController.Error(
        $"[WO:{_batchData.WorkOrderName}] Steelgrade not found {_batchData.SteelgradeCode}");
      Result.DcBatchDataStatus.BackMessage += $"Steelgrade not found {_batchData.SteelgradeCode}. ";
      Result.DcBatchDataStatus.Status = CommStatus.ValidationError;

      Result.SetValid(false);
    }

    protected virtual void OrderDeadlineNotValid()
    {
      NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotCreated,
        $"Error while updating Work Order {0} - Deadline {_batchData.OrderDeadline} cannot be before today",
        _batchData.WorkOrderName);
      NotificationController.Error(
        $"[WO:{_batchData.WorkOrderName}] Deadline {_batchData.OrderDeadline} cannot be before today");
      Result.DcBatchDataStatus.BackMessage += $"Deadline {_batchData.OrderDeadline} cannot be before today. ";
      Result.DcBatchDataStatus.Status = CommStatus.ValidationError;
      Result.SetValid(false);
    }

    protected virtual void CustomerNotValid()
    {
      NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotCreated,
        $"Error while updating Work Order {0} - Customer not found {_batchData.CustomerName}",
        _batchData.WorkOrderName);
      NotificationController.Error(
        $"[WO:{_batchData.WorkOrderName}] Customer not found {_batchData.CustomerName}");
      Result.DcBatchDataStatus.BackMessage += $"Customer not found {_batchData.CustomerName}. ";
      Result.DcBatchDataStatus.Status = CommStatus.ValidationError;

      Result.SetValid(false);
    }

    protected virtual void ExistingProductCatalogueNotValid()
    {
      NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotCreated,
        $"Error while updating Work Order {0} - Product catalogue not found {_batchData.ProductCatalogue}",
        _batchData.WorkOrderName);
      NotificationController.Error(
        $"[WO:{_batchData.WorkOrderName}] Product catalogue not found {_batchData.ProductCatalogue}");
      Result.DcBatchDataStatus.BackMessage += $"Product catalogue not found {_batchData.ProductCatalogue}. ";
      Result.DcBatchDataStatus.Status = CommStatus.ValidationError;

      Result.SetValid(false);
    }

    protected virtual void ExistingProductCatalogueWithParametersNotValid()
    {
      NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotCreated,
        $"Error while updating Work Order {0} - Product catalogue not found " +
        $"OutputShapeSymbol: {_batchData.OutputShapeSymbol}, " +
        $"OutputThickness: {_batchData.OutputThickness}," +
        $"OutputWidth: {_batchData.OutputWidth}", _batchData.WorkOrderName);

      NotificationController.Error(
        $"[WO:{_batchData.WorkOrderName}] Product catalogue not found  " +
        $"OutputShapeSymbol: {_batchData.OutputShapeSymbol}, " +
        $"OutputThickness: {_batchData.OutputThickness}," +
        $"OutputWidth: {_batchData.OutputWidth}");
      Result.DcBatchDataStatus.BackMessage += $"Product catalogue not found  " +
                                              $"OutputShapeSymbol: {_batchData.OutputShapeSymbol}, " +
                                              $"OutputThickness: {_batchData.OutputThickness}," +
                                              $"OutputWidth: {_batchData.OutputWidth}. ";

      Result.DcBatchDataStatus.Status = CommStatus.ValidationError;

      Result.SetValid(false);
    }

    protected virtual void ExistingMaterialCatalogueNotValid()
    {
      NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotCreated,
        $"Error while updating Work Order {0} - Material catalogue not found {_batchData.MaterialCatalogue}",
        _batchData.WorkOrderName);
      NotificationController.Error(
        $"[WO:{_batchData.WorkOrderName}] Material catalogue not found {_batchData.MaterialCatalogue}");
      Result.DcBatchDataStatus.BackMessage += $"Material catalogue not found {_batchData.MaterialCatalogue}. ";
      Result.DcBatchDataStatus.Status = CommStatus.ValidationError;

      Result.SetValid(false);
    }

    protected virtual void MoreThanOneMaterialCatalogueWithParametersExist()
    {
      NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotCreated,
        $"Error while updating Work Order {0} - There has been found more than one material catalogue for parameters:" +
        $"InputThickness: {_batchData.InputThickness}, " +
        $"InputShapeSymbol: {_batchData.InputShapeSymbol}," +
        $"InputLength: {_batchData.InputLength}," +
        $"InputWidth: {_batchData.InputWidth}.",
        _batchData.WorkOrderName);

      NotificationController.Error(
        $"[WO:{_batchData.WorkOrderName}] There has been found more than one material catalogue for parameters:" +
        $"InputThickness: {_batchData.InputThickness}, " +
        $"InputShapeSymbol: {_batchData.InputShapeSymbol}," +
        $"InputLength: {_batchData.InputLength}," +
        $"InputWidth: {_batchData.InputWidth}.");

      Result.DcBatchDataStatus.BackMessage +=
        $"There has been found more than one material catalogue for parameters:" +
        $"InputThickness: {_batchData.InputThickness}, " +
        $"InputShapeSymbol: {_batchData.InputShapeSymbol}," +
        $"InputLength: {_batchData.InputLength}," +
        $"InputWidth: {_batchData.InputWidth}.";
      Result.DcBatchDataStatus.Status = CommStatus.ValidationError;

      Result.SetValid(false);
    }

    protected virtual void CannotCreateMaterialCatalogueWithParameters(Exception e)
    {
      NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotCreated,
        $"Error while updating Work Order {0} - Material catalogue cannot be created for parameters:" +
        $"InputThickness: {_batchData.InputThickness}, " +
        $"InputShapeSymbol: {_batchData.InputShapeSymbol}," +
        $"InputLength: {_batchData.InputLength}," +
        $"InputWidth: {_batchData.InputWidth}.",
        _batchData.WorkOrderName);

      NotificationController.LogException(e, $"Material catalogue cannot be created:" +
                                             $"InputThickness: {_batchData.InputThickness}, " +
                                             $"InputShapeSymbol: {_batchData.InputShapeSymbol}," +
                                             $"InputLength: {_batchData.InputLength}," +
                                             $"InputWidth: {_batchData.InputWidth}.");

      Result.DcBatchDataStatus.BackMessage += $" Material catalogue " +
                                              $"InputThickness: {_batchData.InputThickness}, " +
                                              $"InputShapeSymbol: {_batchData.InputShapeSymbol}," +
                                              $"InputLength: {_batchData.InputLength}," +
                                              $"InputWidth: {_batchData.InputWidth}. cannot be created ";
      Result.SetValid(false);
    }

    protected virtual void MaterialCatalogueWithParametersCreated()
    {
      NotificationController.Info($"[WO:{_batchData.WorkOrderName}] Material Catalogue" +
                                  $"InputThickness: {_batchData.InputThickness}, " +
                                  $"InputShapeSymbol: {_batchData.InputShapeSymbol}," +
                                  $"InputLength: {_batchData.InputLength}," +
                                  $"InputWidth: {_batchData.InputWidth}." +
                                  $" creating...");
      Result.DcBatchDataStatus.BackMessage += $" Material catalogue " +
                                              $"InputThickness: {_batchData.InputThickness}, " +
                                              $"InputShapeSymbol: {_batchData.InputShapeSymbol}," +
                                              $"InputLength: {_batchData.InputLength}," +
                                              $"InputWidth: {_batchData.InputWidth}. created ";
    }

    protected virtual void ParentWorkOrderNotFound()
    {
      NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotCreated,
        $"Error while updating Work Order {0} - Parent WorkOrder not found {_batchData.PreviousWorkOrderName}",
        _batchData.WorkOrderName);
      NotificationController.Error(
        $"[WO:{_batchData.WorkOrderName}] Parent WorkOrder not found {_batchData.PreviousWorkOrderName}");
      Result.DcBatchDataStatus.BackMessage += $"Parent WorkOrder not found {_batchData.PreviousWorkOrderName}. ";
      Result.DcBatchDataStatus.Status = CommStatus.ValidationError;

      Result.SetValid(false);
    }

    protected virtual void TargetWorkOrderWeightNotValid()
    {
      var msg = $"TargetWeight {_batchData.TargetWorkOrderWeight} must be in range [{_batchData.TargetWorkOrderWeightMin} - {_batchData.TargetWorkOrderWeightMax}]";
      NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotCreated,
        $"Error while updating Work Order {0} - {msg}",
        _batchData.WorkOrderName);
      NotificationController.Error(
        $"[WO:{_batchData.WorkOrderName}]  {msg}");
      Result.DcBatchDataStatus.BackMessage += $" {msg}. ";
      Result.DcBatchDataStatus.Status = CommStatus.ValidationError;
      Result.SetValid(false);
    }

    protected virtual void ExistingHeatNotValid()
    {
      var msg = $"Heat Steelgrade ({Result.Heat.FKSteelgradeId}) is different from WO Steelgrade ({Result.Steelgrade.SteelgradeId})";
      NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotCreated,
        $"Error while updating Work Order {0} - {msg}",
        _batchData.WorkOrderName);
      NotificationController.Error(
        $"[WO:{_batchData.WorkOrderName}]  {msg}");
      Result.DcBatchDataStatus.BackMessage += $" {msg}. ";
      Result.DcBatchDataStatus.Status = CommStatus.ValidationError;
      Result.SetValid(false);
    }

    protected virtual void ExistingMaterialCatalogueNotFit()
    {
      var msg = $"Material catalogue ({Result.MaterialCatalogue.FKShape.ShapeCode}, [{Result.MaterialCatalogue.ThicknessMin} - {Result.MaterialCatalogue.ThicknessMax}])"
        + $" does not fit ({_batchData.InputShapeSymbol}, {_batchData.InputThickness})";
      NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotCreated,
        $"Error while updating Work Order {0} - {msg}",
        _batchData.WorkOrderName);
      NotificationController.Error(
        $"[WO:{_batchData.WorkOrderName}]  {msg}");
      Result.DcBatchDataStatus.BackMessage += $" {msg}. ";
      Result.DcBatchDataStatus.Status = CommStatus.ValidationError;
      Result.SetValid(false);
    }

    #endregion
  }
}
