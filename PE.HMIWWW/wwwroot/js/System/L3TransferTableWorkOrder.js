RegisterMethod(HmiRefreshKeys.L3TransferTable, RefreshData);

//THIS METHOD WILL BE CALLED BY SYSTEM (SERVER) IN CASE DATA CHANGE, NAME IS IMPORTANT !!!
function RefreshData() {
    var workOrder = $("#L3TransferTableWorkOrder").data("kendoGrid");
    workOrder.dataSource.read();
    workOrder.refresh();

    var general = $("#L3TransferTableGeneral").data("kendoGrid");
    general.dataSource.read();
    general.refresh();
}

function GoToWorkOrderDetails(Id) {
  let dataToSend = {
    counterId: Id
  };
  openSlideScreen('L3CommStatus', 'WorkOrderDetails', dataToSend, Translations.Details);
}

function EditWorkOrderDefinitionPopup(id) {
    OpenInPopupWindow({
        controller: "L3CommStatus", method: "WorkOrderDefinitionEditPopup", width: 1250, data: { counterId: id }, afterClose: RefreshData
    });
}

function DeleteWorkOrderDefinition(itemId) {
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "?", function () { return DeleteWorkOrderDefinitionConfirmed(itemId) });
}

function DeleteWorkOrderDefinitionConfirmed(itemId) {
    return AjaxReqestHelper(Url("L3CommStatus", "DeleteWorkOrderDefinition"), { id: itemId }, RefreshWorkOrderData, RefreshWorkOrderData);
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
}

function ShowAllRecords() {
  setGridFilter($("#L3TransferTableWorkOrder"), {});
}

function setGridFilter(element, filter) {
  let grid = element.data('kendoGrid');

  if (grid) {
    grid.dataSource.filter(filter);
  }
}

function ShowValidationDetails(validationCheck, commMessage) {
  validationCheck = validationCheck || '';
  commMessage = commMessage || '';
  validationCheck = validationCheck == 'null' ? '' : validationCheck;
  commMessage = commMessage == 'null' ? '' : commMessage;
  ErrorMessage('<b>' + Translations["MESSAGE_L3ValidationResult"] + '</b> </br> ' + validationCheck + ' </br> ' + commMessage);
}
