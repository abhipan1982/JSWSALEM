RegisterMethod(HmiRefreshKeys.Roll, RefreshData);


function AddNew() {
  OpenInPopupWindow({
    controller: "RollType",
    method: "AddRollTypeDialog",
    width: 600,
    afterClose: RefreshData
  });
}
function EditData(itemId) {
  OpenInPopupWindow({
    controller: "RollType",
    method: "EditRollTypeDialog",
    width: 600,
    data: { id: itemId },
    afterClose: RefreshData
  });
}

function Delete(itemId) {
  let functionName = Delete2Confirm;
  let action = 'DeleteRollType';
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action); });
}

function Delete2Confirm(itemId, action) {
  let url = Url("RollType", action);
  let data = { Id: itemId };
  AjaxReqestHelper(url, data, RefreshData);
}

function RefreshData() {
  $("#RollTypesGrid").data("kendoGrid").dataSource.read();
  $("#RollTypesGrid").data("kendoGrid").refresh();
}
