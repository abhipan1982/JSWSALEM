RegisterMethod(HmiRefreshKeys.Roll, RefreshData);

function AddNew() {
  OpenInPopupWindow({
    controller: "Cassette",
    method: "AddCassetteDialog",
    width: 500,
    afterClose: RefreshData
  });
}
function EditData(itemId) {
  OpenInPopupWindow({
    controller: "Cassette",
    method: "EditCassetteDialog",
    width: 500,
    data: { id: itemId },
    afterClose: RefreshData
  });
}

function Delete(itemId) {
  var functionName = Delete2Confirm;
  var action = 'DeleteCassette';
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action); });
}

function Delete2Confirm(itemId, action) {
  var url = serverAddress + "/Cassette/" + action;
  var data = { Id: itemId };
  AjaxReqestHelper(url, data, RefreshData);
}

function DismountCassette(itemId) {
  var functionName = DismountCassette2Confirm;
  var action = 'DismountCassette';
  PromptMessage(Translations["MESSAGE_DismountConfirm"], "", () => { return functionName(itemId, action); });
}

function DismountCassette2Confirm(itemId, action) {
  var url = serverAddress + "/Cassette/" + action;
  var data = { Id: itemId };
  AjaxReqestHelper(url, data, RefreshData);
}

function RefreshData() {
  $("#CassetteGrid").data("kendoGrid").dataSource.read();
  $("#CassetteGrid").data("kendoGrid").refresh();
}
