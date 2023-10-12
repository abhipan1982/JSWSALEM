RegisterMethod(HmiRefreshKeys.L3TransferTableWorkOrders, RefreshWorkOrderData);
RegisterMethod(HmiRefreshKeys.L3TransferTableSteelGrades, RefreshSteelGradeData);
RegisterMethod(HmiRefreshKeys.L3TransferTableWorkOrderReports, RefreshWorkOrderReports);
RegisterMethod(HmiRefreshKeys.L3TransferTableProductReports, RefreshProductReports);

function RefreshWorkOrderData() {
  var workOrder = $("#L3TransferTableWorkOrder").data("kendoGrid");
  workOrder.dataSource.read();
  workOrder.refresh();

  RefresData();
}

function RefreshSteelGradeData() {
  var steelGrade = $("#L3TransferTableSteelGrade").data("kendoGrid");
  steelGrade.dataSource.read();
  steelGrade.refresh();

  RefresData();
}

function RefreshWorkOrderReports() {
  var steelGrade = $("#L3TransferTableWorkOrderReports").data("kendoGrid");
  steelGrade.dataSource.read();
  steelGrade.refresh();

  RefresData();
}

function RefreshProductReports() {
  var steelGrade = $("#L3TransferTableProductReports").data("kendoGrid");
  steelGrade.dataSource.read();
  steelGrade.refresh();

  RefresData();
}

function RefresData() {
  var general = $("#L3TransferTableGeneral").data("kendoGrid");
  general.dataSource.read();
  general.refresh();
}

function GoToWorkOrderDetails(Id) {
  let popupTitle = Translations["NAME_L3CommunicationStatus"];
  let dataToSend = {
    counterId: Id
  };
  openSlideScreen('L3CommStatus', 'WorkOrderDetails', dataToSend, popupTitle);
}

function EditWorkOrderDefinitionPopup(id) {
  OpenInPopupWindow({
    controller: "L3CommStatus", method: "WorkOrderDefinitionEditPopup", width: 1250, data: { counterId: id }, afterClose: RefreshWorkOrderData
  });
}

function CreateWorkOrderDefinitionPopup() {
  OpenInPopupWindow({
    controller: "L3CommStatus", method: "WorkOrderDefinitionCreatePopup", width: 1250, afterClose: RefreshWorkOrderData
  });
}

function ImportWorkOrderDefinitionPopup() {
  OpenInPopupWindow({
    controller: "L3CommStatus", method: "WorkOrderDefinitionImportPopup", width: 1250, afterClose: RefreshWorkOrderData
  });
}

function DeleteWorkOrderDefinition(itemId) {
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "?", function () { return DeleteWorkOrderDefinitionConfirmed(itemId) });
}

function DeleteWorkOrderDefinitionConfirmed(itemId) {
  return AjaxReqestHelper(Url("L3CommStatus", "DeleteWorkOrderDefinition"), { id: itemId }, RefreshWorkOrderData, RefreshWorkOrderData);
}

function GoToWorkOrderReportDetails(Id) {
  let dataToSend = {
    counterId: Id
  };
  openSlideScreen('L3CommStatus', 'WorkOrderReportDetails', dataToSend, Translations["NAME_Details"]);
}

function ResetWorkOrderReport(itemId) {
  return AjaxReqestHelper(Url("L3CommStatus", "ResetWorkOrderReport"), { id: itemId }, RefreshWorkOrderData, RefreshWorkOrderData);
}

function GoToProductReportDetails(Id) {
  let dataToSend = {
    counterId: Id
  };
  openSlideScreen('L3CommStatus', 'ProductReportDetails', dataToSend, Translations["NAME_Details"]);
}

function ResetProductReport(itemId) {
  return AjaxReqestHelper(Url("L3CommStatus", "ResetProductReport"), { id: itemId }, RefreshWorkOrderData, RefreshWorkOrderData);
}

function ShowOnlyFailedRecords() {
  let filter = {
    logic: "or",
    filters: [
      { field: "CommStatus", operator: "eq", value: -1 },
      { field: "CommStatus", operator: "eq", value: -2 }
    ]
  };
  setGridFilter($("#L3TransferTableWorkOrder"), filter);
  setGridFilter($("#L3TransferTableSteelGrade"), filter);
  setGridFilter($("#L3TransferTableWorkOrderReports"), filter);
  setGridFilter($("#L3TransferTableProductReports"), filter);
}

function ShowAllRecords() {
  setGridFilter($("#L3TransferTableWorkOrder"), {});
  setGridFilter($("#L3TransferTableSteelGrade"), {});
  setGridFilter($("#L3TransferTableWorkOrderReports"), {});
  setGridFilter($("#L3TransferTableProductReports"), {});
}

function setGridFilter(element, filter) {
  let grid = element.data('kendoGrid');

  if (grid) {
    grid.dataSource.filter(filter);
  }
}

function ShowValidationDetails(validationCheck, commMessage) {

  var rows = '';
  validationCheck = validationCheck || '';
  commMessage = commMessage || '';
  validationCheck = validationCheck == 'null' ? '' : validationCheck;
  commMessage = commMessage == 'null' ? '' : commMessage;
  if (validationCheck == '' && commMessage == '') {
    WarningMessage(Translations["MESSAGE_NoResults"]);
  }
  else {
    if (validationCheck != '') {
      let validationCheckJSON = JSON.parse(validationCheck.toString());
      for (item in validationCheckJSON) {
        if (validationCheckJSON[item] == 0) {
          let itemTransation = Translations["NAME_" + item + ""];
          rows += '<span>' + itemTransation + '<img class="ml-1 mr-3" src="../../css/Functions/Small/false.png">' + '</span>'
        }
      }
    }
    //ErrorMessage('<b>' + Translations["MESSAGE_L3ValidationResult"] + '</b> </br> ' + validationCheck + ' </br> ' + commMessage);
    ErrorMessage('<b>' + Translations["MESSAGE_L3ValidationResult"] + '</b> </br> ' + rows + ' </br> ' + commMessage);
  }
}

function onError(e) {
  console.log("Error (" + e.operation + ") :: ");

  if (JSON.parse(e.XMLHttpRequest.response).Errors) {
    ErrorMessage(JSON.parse(e.XMLHttpRequest.response).Errors);
  }
}
