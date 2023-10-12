RegisterMethod(HmiRefreshKeys.Roll, RefreshData);

function AddNew() {
  OpenInPopupWindow({
    controller: "RollSetManagement",
    method: "AddRollSetDialog",
    width: 500,
    afterClose: RefreshData
  });
}
function EditData(itemId) {
  OpenInPopupWindow({
    controller: "RollSetManagement",
    method: "EditRollSetDialog",
    width: 500,
    data: { id: itemId },
    afterClose: RefreshData
  });
}
function AssembleRs(itemId) {
  OpenInPopupWindow({
    controller: "RollSetManagement",
    method: "AssembleRollSetDialog",
    width: 600,
    data: { id: itemId },
    afterClose: RefreshData
  });
}
function DisassembleRs(itemId) {
  OpenInPopupWindow({
    controller: "RollSetManagement",
    method: "DisassembleRollSetDialog",
    width: 600,
    data: { id: itemId },
    afterClose: RefreshData
  });
}
function ConfirmAssembleRS(itemId) {
  var functionName = ConfirmAssembleRS2Confirm;
  var action = 'ConfirmAssembleRollSet';
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action); });
}


function ConfirmAssembleRS2Confirm(itemId, action) {

  var url = serverAddress + "/RollsetManagement/" + action;
  var data = { Id: itemId };

  AjaxReqestHelper(url, data, RefreshData);
}

function CancelAssembleRS(itemId) {
  var functionName = CancelAssembleRS2Confirm;
  var action = 'CancelAssembleRollSet';
  PromptMessage("@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDelete", "@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmRollSetCancelInfo", function () { return functionName(itemId, action) });
}

function CancelAssembleRS2Confirm(itemId, action) {

  var url = serverAddress + "/RollsetManagement/" + action;
  var data = { Id: itemId };

  AjaxReqestHelper(url, data, RefreshData);
}

function Delete(itemId) {
  var functionName = Delete2Confirm;
  var action = 'DeleteRollset';
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action) });
}

function Delete2Confirm(itemId, action) {

  var url = serverAddress + "/RollsetManagement/" + action;
  var data = { Id: itemId };

  AjaxReqestHelper(url, data, RefreshData);
}

// 2 functions for refresh list of rolls
function findRoll() {
  return {
    RollSetId: $("#RollSetId").val()
  };
}

function onChange(e) {
  console.log(e);
  console.warn(e.sender.element.attr('id'));
  let prevValue = e.sender_old;
  console.error(prevValue);
}


function RefreshData() {
  $("#RollSetManagementGrid").data("kendoGrid").dataSource.read();
  $("#RollSetManagementGrid").data("kendoGrid").refresh();
}

function GetRollSet(Id) {
  $.ajax({
    url: serverAddress + "/RollSetManagement/GetEmptyRollList",
    type: 'POST',
    data: 'id=' + Id,
    success: function (result) {
      $('#furnace-body-' + Id).html(result);
    },
    complete: function (r) {
      //when connection with server over
    },
    error: function (error) {
      CoreHandleError(error.status, error.statusText, "AjaxAssembleRoll", true, null, null);
    }
  });
}
