RegisterMethod(HmiRefreshKeys.Equipment, reloadKendoGrid);

function reloadKendoGrid() {
  let grid = $('#EquipmentGrid').data('kendoGrid');
  grid.dataSource.read();
  grid.refresh();
}

function colorRowInTable() {
  var grid = $("#EquipmentGrid").data("kendoGrid");
  var gridData = grid.dataSource.view();

  for (var i = 0; i < gridData.length; i++) {
    var currentUid = gridData[i].uid;
    var currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");
    var rowColor, rowBgColor;
    if (gridData[i].IsInactive) {
      rowBgColor = '#808080';
      rowColor = 'white';

      currenRow.css({ 'background': rowBgColor });
      currenRow.css({ 'color': rowColor });
    }
    else if (gridData[i].IsOverdue) {
      rowBgColor = 'red';
      rowColor = 'white';

      currenRow.css({ 'background': rowBgColor });
      currenRow.css({ 'color': rowColor });
    }
    else if (gridData[i].IsWarned) {
      rowBgColor = '#fcbe03';
      rowColor = 'white';

      currenRow.css({ 'background': rowBgColor });
      currenRow.css({ 'color': rowColor });
    }
  }
}

function editEquipmentPopup(id) {
  OpenInPopupWindow({
    controller: "Equipment", method: "EquipmentEditPopup", width: 500, data: { id: id }, afterClose: reloadKendoGrid
  });
}

function editEquipmentStatusPopup(id) {
  OpenInPopupWindow({
    controller: "Equipment", method: "EquipmentStatusEditPopup", width: 500, data: { id: id }, afterClose: reloadKendoGrid
  });
}

function cloneEquipmentPopup(id) {
  OpenInPopupWindow({
    controller: "Equipment", method: "EquipmentClonePopup", width: 500, data: { id: id }, afterClose: reloadKendoGrid
  });
}

function addEquipmentPopup() {
  OpenInPopupWindow({
    controller: "Equipment", method: "EquipmentCreatePopup", width: 500, afterClose: reloadKendoGrid
  });
}

function deleteEquipment(id) {
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
    let dataToSend = {
      id: id
    };
    let targetUrl = '/Equipment/DeleteEquipment';

    AjaxReqestHelper(targetUrl, dataToSend, reloadKendoGrid, function () { console.log('DeleteEquipment - failed'); });
  });
}

function showEquipmentHistory(id) {
  let dataToSend = {
    id: id
  };
  openSlideScreen('Equipment', 'ShowEquipmentHistory', dataToSend);
}
