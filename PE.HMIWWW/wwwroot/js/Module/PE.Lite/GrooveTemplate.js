RegisterMethod(HmiRefreshKeys.Roll, RefreshData);

function AddNew() {
  OpenInPopupWindow({
    controller: "GrooveTemplate",
    method: "AddGrooveTemplateDialog",
    width: 1200,
    afterClose: RefreshData
  });
}
function EditData(itemId) {
  OpenInPopupWindow({
    controller: "GrooveTemplate",
    method: "EditGrooveTemplateDialog",
    width: 1200,
    data: { id: itemId },
    afterClose: RefreshData
  });
}

function Delete(itemId) {
  var functionName = Delete2Confirm;
  var action = 'DeleteGrooveTemplate';
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action); });
}

function Delete2Confirm(itemId, action) {

  var url = serverAddress + "/GrooveTemplate/" + action;
  var data = { Id: itemId };

  AjaxReqestHelper(url, data, RefreshData);
}


function RefreshData() {
  $("#GrooveTemplateGrid").data("kendoGrid").dataSource.read();
  $("#GrooveTemplateGrid").data("kendoGrid").refresh();
}
