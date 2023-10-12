RegisterMethod(HmiRefreshKeys.Roll, RefreshData);

function ConfigRollset(itemId) {
  OpenInPopupWindow({
    controller: "GrindingTurning",
    method: "ConfigRollSetDialog",
    width: 1200,
    data: { id: itemId },
    afterClose: RefreshData
  });
}

function RefreshData() {
  $("#ScheduledRollSetGrid").data("kendoGrid").dataSource.read();
  $("#ScheduledRollSetGrid").data("kendoGrid").refresh();
  $("#PlannedRollSetGrid").data("kendoGrid").dataSource.read();
  $("#PlannedRollSetGrid").data("kendoGrid").refresh();
}

function EditData(itemId) {
  OpenInPopupWindow({
    controller: "GrindingTurning",
    method: "EditRollSetDialog",
    width: 600,
    data: { id: itemId },
    afterClose: RefreshData
  });
}

function AssembleRs(itemId) {
  OpenInPopupWindow({
    controller: "GrindingTurning",
    method: "AssembleRollSetDialog",
    width: 600,
    data: { id: itemId },
    afterClose: RefreshData
  });
}

function DisassembleRs(itemId) {
  OpenInPopupWindow({
    controller: "GrindingTurning",
    method: "DisassembleRollSetDialog",
    width: 600,
    data: { id: itemId },
    afterClose: RefreshData
  });
}

function ConfirmRollset(itemId) {
  OpenInPopupWindow({
    controller: "GrindingTurning",
    method: "ConfirmRollSetDialog",
    width: 1000,
    data: { id: itemId },
    afterClose: RefreshData
  });
}

function CancelRollset(itemId) {
  var functionName = CancelRollset2Confirm;
  var action = 'CancelRollset';
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action); });
}

function CancelRollset2Confirm(itemId, action) {
  AjaxReqestHelper(Url("GrindingTurning", action), { Id: itemId }, RefreshData);
}

function Delete(itemId) {
  var functionName = Delete2Confirm;
  var action = 'DeleteRollset';
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action); });
}

function Delete2Confirm(itemId, action) {
  AjaxReqestHelper(Url("RollsetManagement", action), { Id: itemId }, RefreshData);
}

function OpenTurningInfoPopup(itemId) {
  openSlideScreen("GrindingTurning", "RollSetHistoryPopupDialog", { id: itemId });
}

function OpenTurningForConfirmInfoPopup(itemId) {
  let dataToSend = {
    id: itemId
  };
  openSlideScreen("GrindingTurning", "TurningForConfirmInfoPopupDialog", dataToSend);
}

function HistoryRollset(itemId) {
  OpenInPopupWindow({
    controller: "GrindingTurning",
    method: "RollSetHistoryPopupDialog",
    width: 380,
    height: 700,
    data: { id: itemId },
    afterClose: RefreshData
  });
}



