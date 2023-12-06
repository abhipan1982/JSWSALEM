using System;
using System.Globalization;
using PE.BaseModels.DataContracts.Internal.DBA;
using PE.Models.DataContracts.Internal.DBA;
using PE.PRM.Base.Managers;
using PE.PRM.ProdManager.Handler.Models;
using SMF.Core.Notification;

namespace PE.PRM.ProdManager.Handler
{
  public class BatchDataTransferValidationHandler
  {
    public bool ValidateBatchDataDefinition(DCL3L2BatchDataDefinition message,
      DCBatchDataStatus backMsg,
      bool useExistingMaterialCatalogueForProcessingTransferObject,
      bool useExistingProductCatalogueForProcessingTransferObject,
      out ValidatedBatchData workOrderDto)
    {
      bool isValid = true;
      double? bundleWeightMin = null;
      double? bundleWeightMax = null;
      string dateFomat = "ddMMyyyy";
      double inputThickness = 0d;
      double inputWidth = 0d;
      double inputLength = 0d;
      double outputThickness = 0d;
      double outputWidth = 0d;

      workOrderDto = new ValidatedBatchData();

      if (string.IsNullOrWhiteSpace(message.BatchNo))
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotValid,
          $"[WO:{message.BatchNo}]: empty value in {nameof(message.BatchNo)}", message.BatchNo, message.BatchNo, nameof(message.BatchNo));
        NotificationController.Error("[WO:{workorder}] Empty val {value}", message.BatchNo, nameof(message.BatchNo));
        backMsg.BackMessage += $"Empty value {nameof(message.BatchNo)}. ";
        isValid = false;
      }

      //if (!DateTime.TryParseExact(message.OrderDeadline, dateFomat,
      //  CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime deadline))
      //{
      //  NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotValid,
      //    $"[WO:{message.WorkOrderName}]: cannot parse value \"{message.OrderDeadline}\" in {nameof(message.OrderDeadline)}", message.WorkOrderName, message.OrderDeadline, nameof(message.OrderDeadline));
      //  NotificationController.Error("[WO:{workorder}] Error while parsing {field} val {value}", message.WorkOrderName, nameof(message.OrderDeadline), message.OrderDeadline);
      //  backMsg.BackMessage += $"Error parsing {nameof(message.OrderDeadline)} {message.OrderDeadline}. ";
      //  isValid = false;
      //}

      if (string.IsNullOrWhiteSpace(message.HeatName))
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotValid,
          $"[WO:{message.BatchNo}]: empty value in {nameof(message.HeatName)}", message.BatchNo, message.HeatName, nameof(message.HeatName));
        NotificationController.Error("[WO:{workorder}] Empty val {value}", message.BatchNo, nameof(message.HeatName));
        backMsg.BackMessage += $"Empty value {nameof(message.HeatName)}. ";
        isValid = false;
      }

      //if (!GetDouble(message.BilletWeight, out double billetWeight) || billetWeight <= 0)
      //{
      //  NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotValid,
      //    $"[WO:{message.WorkOrderName}]: cannot parse mandatory(UseExistingMaterialCatalogue) value \"{message.BilletWeight}\" in {nameof(message.BilletWeight)}", message.WorkOrderName, message.BilletWeight, nameof(message.BilletWeight));
      //  NotificationController.Error("[WO:{workorder}] Error while parsing mandatory(UseExistingMaterialCatalogue) {field} val {value}", message.WorkOrderName, nameof(message.BilletWeight), message.BilletWeight);
      //  backMsg.BackMessage += $"Error parsing mandatory(UseExistingMaterialCatalogue) {nameof(message.BilletWeight)} {message.BilletWeight}. ";
      //  isValid = false;
      //}

      //if (!Int16.TryParse(message.NumberOfBillets, out short numberOfBillets) || numberOfBillets <= 0)
      //{
      //  NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotValid,
      //    $"[WO:{message.WorkOrderName}]: cannot parse value \"{message.NumberOfBillets}\" in {nameof(message.NumberOfBillets)}", message.WorkOrderName, message.NumberOfBillets, nameof(message.NumberOfBillets));
      //  NotificationController.Error("[WO:{workorder}] Error while parsing {field} val {value}", message.WorkOrderName, nameof(message.NumberOfBillets), message.NumberOfBillets);
      //  backMsg.BackMessage += $"Error parsing {nameof(message.NumberOfBillets)} {message.NumberOfBillets}. ";
      //  isValid = false;
      //}

      //if (!string.IsNullOrWhiteSpace(message.BundleWeightMin))
      //{
      //  if (!double.TryParse(message.BundleWeightMin, out double bundleWeightMinTemp))
      //  {
      //    NotificationController.Warn("[WO:{workorder}] Cannot pars {field} val {value}", message.WorkOrderName, nameof(message.BundleWeightMin), message.BundleWeightMin);
      //    backMsg.BackMessage += $"Cannot pars {nameof(message.BundleWeightMin)} {message.BundleWeightMin}. ";
      //    isValid = false;
      //  }

      //  bundleWeightMin = bundleWeightMinTemp;
      //}

      //if (!string.IsNullOrWhiteSpace(message.BundleWeightMax))
      //{
      //  if (!double.TryParse(message.BundleWeightMax, out double bundleWeightMaxTemp))
      //  {
      //    NotificationController.Warn("[WO:{workorder}] Cannot pars {field} val {value}", message.WorkOrderName, nameof(message.BundleWeightMax), message.BundleWeightMax);
      //    backMsg.BackMessage += $"Cannot pars {nameof(message.BundleWeightMax)} {message.BundleWeightMax}. ";
      //    isValid = false;
      //  }

      //  bundleWeightMax = bundleWeightMaxTemp;
      //}

      //if (!GetDouble(message.TargetWorkOrderWeight, out double targetWorkOrderWeight) || targetWorkOrderWeight <= 0)
      //{
      //  NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotValid,
      //    $"[WO:{message.WorkOrderName}]: cannot parse value \"{message.TargetWorkOrderWeight}\" in {nameof(message.TargetWorkOrderWeight)}", message.WorkOrderName, message.TargetWorkOrderWeight, nameof(message.TargetWorkOrderWeight));
      //  NotificationController.Error("[WO:{workorder}] Error while parsing {field} val {value}", message.WorkOrderName, nameof(message.TargetWorkOrderWeight), message.TargetWorkOrderWeight);
      //  backMsg.BackMessage += $"Error parsing {nameof(message.TargetWorkOrderWeight)} {message.TargetWorkOrderWeight}. ";
      //  isValid = false;
      //}

      //if (!GetDouble(message.TargetWorkOrderWeightMin, out double targetWorkOrderWeightMin) || targetWorkOrderWeightMin <= 0)
      //{
      //  NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotValid,
      //    $"[WO:{message.WorkOrderName}]: cannot parse value \"{message.TargetWorkOrderWeightMin}\" in {nameof(message.TargetWorkOrderWeightMin)}", message.WorkOrderName, message.TargetWorkOrderWeightMin, nameof(message.TargetWorkOrderWeightMin));
      //  NotificationController.Error("[WO:{workorder}] Error while parsing {field} val {value}", message.WorkOrderName, nameof(message.TargetWorkOrderWeightMin), message.TargetWorkOrderWeightMin);
      //  backMsg.BackMessage += $"Error parsing {nameof(message.TargetWorkOrderWeightMin)} {message.TargetWorkOrderWeightMin}. ";
      //  isValid = false;
      //}

      //if (!GetDouble(message.TargetWorkOrderWeightMax, out double targetWorkOrderWeightMax) || targetWorkOrderWeightMin <= 0)
      //{
      //  NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotValid,
      //    $"[WO:{message.WorkOrderName}]: cannot parse value \"{message.TargetWorkOrderWeightMax}\" in {nameof(message.TargetWorkOrderWeightMax)}", message.WorkOrderName, message.TargetWorkOrderWeightMax, nameof(message.TargetWorkOrderWeightMax));
      //  NotificationController.Error("[WO:{workorder}] Error while parsing {field} val {value}", message.WorkOrderName, nameof(message.TargetWorkOrderWeightMax), message.TargetWorkOrderWeightMax);
      //  backMsg.BackMessage += $"Error parsing {nameof(message.TargetWorkOrderWeightMax)} {message.TargetWorkOrderWeightMax}. ";
      //  isValid = false;
      //}
      
      //if (string.IsNullOrWhiteSpace(message.SteelgradeCode))
      //{
      //  NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotValid,
      //    $"[WO:{message.WorkOrderName}]: empty value in {nameof(message.SteelgradeCode)}", message.WorkOrderName, message.SteelgradeCode, nameof(message.SteelgradeCode));
      //  NotificationController.Error("[WO:{workorder}] Empty val {value}", message.WorkOrderName, nameof(message.SteelgradeCode));
      //  backMsg.BackMessage += $"Empty value {nameof(message.SteelgradeCode)}. ";
      //  isValid = false;
      //}

      //if (useExistingMaterialCatalogueForProcessingTransferObject && string.IsNullOrWhiteSpace(message.MaterialCatalogueName))
      //{
      //  NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotValid,
      //    $"[WO:{message.WorkOrderName}]: empty value in {nameof(message.MaterialCatalogueName)}", message.WorkOrderName, message.MaterialCatalogueName, nameof(message.MaterialCatalogueName));
      //  NotificationController.Error("[WO:{workorder}] Empty val {value}", message.WorkOrderName, nameof(message.MaterialCatalogueName));
      //  backMsg.BackMessage += $"Empty value {nameof(message.MaterialCatalogueName)}. ";
      //  isValid = false;
      //}

      //if (useExistingProductCatalogueForProcessingTransferObject && string.IsNullOrWhiteSpace(message.ProductCatalogueName))
      //{
      //  NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotValid,
      //    $"[WO:{message.WorkOrderName}]: empty value in {nameof(message.ProductCatalogueName)}", message.WorkOrderName, message.ProductCatalogueName, nameof(message.ProductCatalogueName));
      //  NotificationController.Error("[WO:{workorder}] Empty val {value}", message.WorkOrderName, nameof(message.ProductCatalogueName));
      //  backMsg.BackMessage += $"Empty value {nameof(message.ProductCatalogueName)}. ";
      //  isValid = false;
      //}


      //if (!string.IsNullOrWhiteSpace(message.InputThickness) && (!GetDouble(message.InputThickness, out inputThickness) || inputThickness <= 0))
      //{
      //  NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotValid,
      //    $"[WO:{message.WorkOrderName}]: cannot parse value \"{message.InputThickness}\" in {nameof(message.InputThickness)}", message.WorkOrderName, message.InputThickness, nameof(message.InputThickness));
      //  NotificationController.Error("[WO:{workorder}] Error while parsing mandatory(!UseExistingMaterialCatalogue) {field} val {value}", message.WorkOrderName, nameof(message.InputThickness), message.InputThickness);
      //  backMsg.BackMessage += $"Error parsing mandatory(!UseExistingMaterialCatalogue) {nameof(message.InputThickness)} {message.InputThickness}. ";
      //  isValid = false;
      //}


      //if (!string.IsNullOrWhiteSpace(message.InputWidth) && (!GetDouble(message.InputWidth, out inputWidth) || inputWidth <= 0))
      //{
      //  NotificationController.Warn("[WO:{workorder}] Error while parsing {field} val {value}", message.WorkOrderName, nameof(message.InputWidth), message.InputWidth);
      //  backMsg.BackMessage += $"Error parsing {nameof(message.InputWidth)} {message.InputWidth}. ";
      //  isValid = false;
      //}

      //if (!string.IsNullOrWhiteSpace(message.BilletLength) && (!GetDouble(message.BilletLength, out inputLength) || inputLength <= 0))
      //{
      //  NotificationController.Warn("[WO:{workorder}] Error while parsing {field} val {value}", message.WorkOrderName, nameof(message.BilletLength), message.BilletLength);
      //  backMsg.BackMessage += $"Error parsing {nameof(message.BilletLength)} {message.BilletLength}. ";
      //  isValid = false;
      //}

      //if (!useExistingMaterialCatalogueForProcessingTransferObject && string.IsNullOrWhiteSpace(message.InputShapeSymbol))
      //{
      //  NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotValid,
      //    $"[WO:{message.WorkOrderName}]: empty value in {nameof(message.InputShapeSymbol)}", message.WorkOrderName, message.InputShapeSymbol, nameof(message.InputShapeSymbol));
      //  NotificationController.Error("[WO:{workorder}] Empty val {value}", message.WorkOrderName, nameof(message.InputShapeSymbol));
      //  backMsg.BackMessage += $"Empty value {nameof(message.InputShapeSymbol)}. ";
      //  isValid = false;
      //}

      //if (!useExistingProductCatalogueForProcessingTransferObject &&
      //    (string.IsNullOrWhiteSpace(message.OutputThickness) || !GetDouble(message.OutputThickness, out outputThickness) || outputThickness <= 0))
      //{
      //  NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotValid,
      //    $"[WO:{message.WorkOrderName}]: cannot parse value \"{message.OutputThickness}\" in {nameof(message.OutputThickness)}", message.WorkOrderName, message.OutputThickness, nameof(message.OutputThickness));
      //  NotificationController.Error("[WO:{workorder}] Error while parsing mandatory(!UseExistingProductCatalogue) {field} val {value}", message.WorkOrderName, nameof(message.OutputThickness), message.OutputThickness);
      //  backMsg.BackMessage += $"Error parsing mandatory(!UseExistingMaterialCatalogue) {nameof(message.OutputThickness)} {message.OutputThickness}. ";
      //  isValid = false;
      //}

      //if (!useExistingProductCatalogueForProcessingTransferObject &&
      //    (string.IsNullOrWhiteSpace(message.OutputWidth) || !GetDouble(message.OutputWidth, out outputWidth) || outputWidth <= 0))
      //{
      //  NotificationController.Warn("[WO:{workorder}] Error while parsing {field} val {value}", message.WorkOrderName, nameof(message.OutputWidth), message.OutputWidth);
      //  backMsg.BackMessage += $"Error parsing {nameof(message.OutputWidth)} {message.OutputWidth}. ";
      //  isValid = false;
      //}

      //if (!useExistingProductCatalogueForProcessingTransferObject && string.IsNullOrWhiteSpace(message.OutputShapeSymbol))
      //{
      //  NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotValid,
      //    $"[WO:{message.WorkOrderName}]: empty value in {nameof(message.OutputShapeSymbol)}", message.WorkOrderName, message.OutputShapeSymbol, nameof(message.OutputShapeSymbol));
      //  NotificationController.Error("[WO:{workorder}] Empty val {value}", message.WorkOrderName, nameof(message.OutputShapeSymbol));
      //  backMsg.BackMessage += $"Empty value {nameof(message.OutputShapeSymbol)}. ";
      //  isValid = false;
      //}

      if (!isValid)
        return isValid;

      //FillBatchDataDtoPropertiesFromL3L2Message(message,
      //  workOrderDto,
      //  //deadline,
      //  //billetWeight,
      //  //numberOfBillets,
      //  bundleWeightMin,
      //  bundleWeightMax,
      //  //targetWorkOrderWeight,
      //  //targetWorkOrderWeightMin,
      //  //targetWorkOrderWeightMax,
      //  inputThickness,
      //  inputWidth,
      //  inputLength,
      //  outputThickness,
      //  outputWidth);

      return isValid;
    }

    private void FillWorkOrderDtoPropertiesFromL3L2Message(DCL3L2BatchDataDefinition message,
      ValidatedBatchData workOrderDto, DateTime deadline, double billetWeight, short numberOfBillets,
      double? bundleWeightMin, double? bundleWeightMax, double targetWorkOrderWeight, double targetWorkOrderWeightMin,
      double targetWorkOrderWeightMax, double inputThickness, double? inputWidth, double? inputLength, double outputThickness,
      double? outputWidth)
    {
      //workOrderDto = message.WorkOrderName;
      //workOrderDto.ExternalWorkOrderName = message.ExternalWorkOrderName;
      //workOrderDto.PreviousWorkOrderName = message.PreviousWorkOrderName;
      workOrderDto.OrderDeadline = deadline;
      workOrderDto.HeatName = message.HeatName;
      workOrderDto.BilletWeight = billetWeight;
      workOrderDto.NumberOfBillets = numberOfBillets;
      workOrderDto.InputWorkOrderWeight = numberOfBillets * billetWeight;
      workOrderDto.CustomerName = message.CustomerName;
      workOrderDto.BundleWeightMin = bundleWeightMin;
      workOrderDto.BundleWeightMax = bundleWeightMax;
      workOrderDto.TargetWorkOrderWeight = targetWorkOrderWeight;
      workOrderDto.TargetWorkOrderWeightMin = targetWorkOrderWeightMin;
      workOrderDto.TargetWorkOrderWeightMax = targetWorkOrderWeightMax;
      //workOrderDto.MaterialCatalogue = message.MaterialCatalogueName;
      //workOrderDto.ProductCatalogue = message.ProductCatalogueName;
      //workOrderDto.SteelgradeCode = message.SteelgradeCode;
      workOrderDto.InputThickness = ConvertToMeters(inputThickness);
      //workOrderDto.InputThicknessString = message.InputThickness;
      workOrderDto.InputWidth = inputWidth.HasValue ? ConvertToMeters(inputWidth.Value) : (double?)null;
      //workOrderDto.InputWidthString = message.InputWidth;
      workOrderDto.InputLength = inputLength.Value; // TODOIK
      //workOrderDto.InputLengthString = message.BilletLength;
      //workOrderDto.InputShapeSymbol = message.InputShapeSymbol;
      workOrderDto.OutputThickness = ConvertToMeters(outputThickness);
     // workOrderDto.OutputThicknessString = message.OutputThickness;
      workOrderDto.OutputWidth = outputWidth.HasValue ? ConvertToMeters(outputWidth.Value) : (double?)null;
      //workOrderDto.OutputWidthString = message.OutputWidth;
      //workOrderDto.OutputShapeSymbol = message.OutputShapeSymbol;
      workOrderDto.L3CreatedTs = message.CreatedTs;
    }

    private double ConvertToMeters(double milimeters)
    {
      return milimeters / 1000d;
    }

    private bool GetDouble(string s, out double result)
    {
      // Try parsing in the current culture
      return double.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out result) ||
             // Then try in US english
             double.TryParse(s, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) ||
             // Then in neutral language
             double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
    }
  }
}
