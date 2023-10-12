RegisterMethod(HmiRefreshKeys.EquipmentGroups, reloadKendoGrid);

function reloadKendoGrid() {
  let grid = $('#Groups').data('kendoGrid');
  grid.dataSource.read();
  grid.refresh();
}

function editEquipmentGroupPopup(id) {
  OpenInPopupWindow({
    controller: "EquipmentGroups", method: "EquipmentGroupsEditPopup", width: 600, data: { id: id }, afterClose: reloadKendoGrid
  });
}

function addEquipmentGroupPopup() {
  OpenInPopupWindow({
    controller: "EquipmentGroups", method: "EquipmentGroupCreatePopup", width: 600, afterClose: reloadKendoGrid
  });
}

function deleteEquipmentGroup(id) {
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
    let dataToSend = {
      id: id
    };
    let targetUrl = '/EquipmentGroups/DeleteEquipmentGroup';

    AjaxReqestHelper(targetUrl, dataToSend, reloadKendoGrid, function () { console.log('DeleteEquipmentGroup - failed'); });
  });
}
