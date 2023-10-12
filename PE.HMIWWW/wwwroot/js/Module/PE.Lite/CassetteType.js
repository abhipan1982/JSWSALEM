RegisterMethod(HmiRefreshKeys.Roll, RefreshData);

function AddNew() {
  OpenInPopupWindow({
    controller: "CassetteType",
    method: "AddCassetteTypeDialog",
    width: 500,
    afterClose: RefreshData
  });
}
function EditData(itemId) {

  OpenInPopupWindow({
    controller: "CassetteType",
    method: "EditCassetteTypeDialog",
    width: 500,
    data: { id: itemId },
    afterClose: RefreshData
  });
}

function Delete(itemId) {
  var functionName = Delete2Confirm;
  var action = 'DeleteCassetteType';
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action) });
}

function Delete2Confirm(itemId, action) {

  var url = serverAddress + "/CassetteType/" + action;
  var data = { Id: itemId };

  AjaxReqestHelper(url, data, RefreshData);
}


function RefreshData() {
  $("#CassetteTypesGrid").data("kendoGrid").dataSource.read();
  $("#CassetteTypesGrid").data("kendoGrid").refresh();
}
