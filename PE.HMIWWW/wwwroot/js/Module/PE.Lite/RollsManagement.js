RegisterMethod(HmiRefreshKeys.Roll, RefreshData);

function AddNew() {
  OpenInPopupWindow({
    controller: "RollsManagement",
    method: "AddRollDialog",
    width: 600,
    afterClose: RefreshData
  });
}
function EditData(itemId) {

  OpenInPopupWindow({
    controller: "RollsManagement",
    method: "EditRollDialog",
    width: 600,
    data: { id: itemId },
    afterClose: RefreshData
  });
}
function ScrapRoll(itemId) {
  OpenInPopupWindow({
    controller: "RollsManagement",
    method: "ScrapRollDialog",
    width: 600,
    data: { id: itemId },
    afterClose: RefreshData
  });
}

function Delete(itemId) {
  let functionName = Delete2Confirm;
  let action = 'DeleteRoll';
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action); });

}

function Delete2Confirm(itemId, action) {
  let url = Url("RollsManagement", action);
  let data = { Id: itemId };
  AjaxReqestHelper(url, data, RefreshData);
}


function RefreshData() {
  $("#RollsManagementGrid").data("kendoGrid").dataSource.read();
  $("#RollsManagementGrid").data("kendoGrid").refresh();
}
