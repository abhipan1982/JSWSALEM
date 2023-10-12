RegisterMethod(HmiRefreshKeys.Roll, RefreshData);

function AddNew() {
  OpenInPopupWindow({
    controller: "ActualStandConfiguration",
    method: "AddStandConfigurationDialog",
    width: 320,
    afterClose: RefreshData
  });
}
function EditData(itemId) {

  OpenInPopupWindow({
    controller: "ActualStandConfiguration",
    method: "EditStandConfigurationDialog",
    width: 500,
    data: { id: itemId },
    afterClose: RefreshData
  });

}
function OpenRollsetInfoPopup(itemId) {
  OpenInPopupWindow({
    controller: "GrindingTurning",
    method: "TurningInfoPopupDialog",
    width: 400,
    top: 100,
    data: { id: itemId },
    afterClose: RefreshData
  });
}
function MountCassette(itemId) {
  OpenInPopupWindow({
    controller: "ActualStandConfiguration",
    method: "MountCassetteDialog",
    width: 500,
    data: { id: itemId },
    afterClose: RefreshData
  });
}

function MountEmptyCassette(itemId) {
  OpenInPopupWindow({
    controller: "ActualStandConfiguration",
    method: "MountEmptyCassetteDialog",
    width: 500,
    data: { id: itemId },
    afterClose: RefreshData
  });
}

function DismountCassette(itemId) {
  OpenInPopupWindow({
    controller: "ActualStandConfiguration",
    method: "DismountCassetteDialog",
    width: 500,
    data: { id: itemId },
    afterClose: RefreshData
  });
}
function SwapCassettes(itemId) {
  OpenInPopupWindow({
    controller: "ActualStandConfiguration",
    method: "SwapCassetteDialog",
    width: 300,
    data: { id: itemId },
    afterClose: RefreshData
  });
}
// for mounting rollset to  empty cassette whichone is in Stand but only for swapRollSet
function MountRollSet(itemId, positionId) {
  OpenInPopupWindow({
    controller: "ActualStandConfiguration",
    method: "MountRollSetDialog",
    width: 540,
    data: { id: itemId, param: positionId },
    afterClose: RefreshData
  });
}
// for mounting rollset to  empty cassette whichone is in Stand - popup to Mounted rollset
function OpenAssembleCassetteForm(itemId) {
  OpenInPopupWindow({
    controller: "ActualStandConfiguration",
    method: "AssembleCassetteAndRollsetDialog",
    width: 500,
    data: { id: itemId },
    afterClose: RefreshData
  });
}



function OpenCasstteInfoPopup(itemId) {
  OpenInPopupWindow({
    controller: "ActualStandConfiguration",
    method: "CassetteInfoPopupDialog",
    width: 1300,
    data: { id: itemId },
    afterClose: RefreshData
  });
}
function SwapStands(itemId) {
  OpenInPopupWindow({
    controller: "ActualStandConfiguration",
    method: "SwapStandDialog",
    width: 340,
    data: { id: itemId },
    afterClose: RefreshData
  });
}

function Delete(itemId) {
  var functionName = Delete2Confirm;
  var action = 'DeleteStandConfiguration';
  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], "", () => { return functionName(itemId, action); });
}

function Delete2Confirm(itemId, action) {

  var url = serverAddress + "/ActualStandConfiguration/" + action;
  var data = { Id: itemId };

  AjaxReqestHelper(url, data, RefreshData);
}
// for mounting rollset to  empty cassette whichone is in Stand


function StylingAfterCompare(param) {
  if (param > 100) {
    return "background: #e61c1c;text-align: right;color:white;";  //red
  }
  else if (param > 90) {
    return "background: #ef8438;text-align: right;color:white;";  //amber
  }
  else if (param > 80) {
    return "background: #d5b500; text-align: right;color:white;"; //yellow
  }
  else {
    return "text-align: right;";
  }
}


function RefreshData() {
  let asgrid = $('#ActualStandConfiguration').data('kendoGrid');
  asgrid.dataSource.read();
  asgrid.refresh();

  let pcgrid = $('#PassChangeActualData').data('kendoGrid');
  pcgrid.dataSource.read();
  pcgrid.refresh();
}
