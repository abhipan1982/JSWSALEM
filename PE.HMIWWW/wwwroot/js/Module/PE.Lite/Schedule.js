RegisterMethod(HmiRefreshKeys.Schedule, RefreshData);
$restrictedRows = 0;
var noHint = $.noop;

function RefreshData() {
  if ($("#ScheduleList").length > 0) {
    $("#ScheduleList").data("kendoGrid").dataSource.read();
    $("#ScheduleList").data("kendoGrid").refresh();
  }

  if ($("#UnassignedWorkOrders").length > 0) {
    $("#UnassignedWorkOrders").data("kendoGrid").dataSource.read();
    $("#UnassignedWorkOrders").data("kendoGrid").refresh();
  }
}

function removeItemFromSchedule(scheduleId, workOrderId, orderSeq) {
  let dataToSend = {
    scheduleId: scheduleId,
    workOrderId: workOrderId,
    orderSeq: orderSeq
  };
  let targetUrl = '/Schedule/RemoveItemFromSchedule';

  PromptMessage(Translations["MESSAGE_ConfirmDeleteFromSchedule"], '', () => {
    AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('removeItemFromSchedule - failed'); });
  });
}

function moveItemInSchedule(scheduleId, workOrderId, orderSeq, newIndex) {
  let dataToSend = {
    scheduleId: scheduleId,
    workOrderId: workOrderId,
    seqId: orderSeq,
    newIndex: newIndex
  };
  let targetUrl = '/Schedule/MoveItemInSchedule';

  AjaxReqestHelper(targetUrl, dataToSend, RefreshData, RefreshData);
}


function createTestSchedule(scheduleId) {
  OpenInPopupWindow({
    controller: 'Schedule',
    method: 'CloneSchedulePopup',
    heigth: 830,
    width: 500,
    data: { scheduleId: scheduleId },
    afterClose: RefreshData
  });
}

function editMaterialNumber(workOrderId) {
  OpenInPopupWindow({
    controller: 'WorkOrder',
    method: 'EditMaterialNumberPopup',
    heigth: 830,
    width: 500,
    data: { workOrderId: workOrderId },
    afterClose: RefreshData
  });
}

function GoToWorkOrderDetails(workOrderId) {

  let dataToSend = {
    workOrderId: workOrderId
  };
  openSlideScreen('WorkOrder', 'ElementDetails', dataToSend);
}

function GoToUnasignedWorkOrder() {
  openSlideScreen('Schedule', 'PreparePratialForSchedule');
}

function EditMaterialCataloguePopup(id) {
  OpenInPopupWindow({
    controller: "BilletCatalogue", method: "EditMaterialCataloguePopup", width: 600, data: { id: id }, afterClose: reloadKendoGrid
  });
}

function reloadKendoGrid() {
  let grid = $('#UnassignedWorkOrders').data('kendoGrid');
  grid.dataSource.read();
  grid.refresh();
}

function ColorRowInTable() {
  $restrictedRows = 0;
  var grid = $("#ScheduleList").data("kendoGrid");
  var gridData = grid.dataSource.view();

  for (var i = 0; i < gridData.length; i++) {
    var currentUid = gridData[i].uid;
    var currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");

    $(currenRow).addClass('status-' + gridData[i].EnumWorkOrderStatus);
  }
}

function placeholder(element) {
  return element.clone().addClass("k-state-hover").css("opacity", 0.65);
}
function onChange(e) {
  let grid = $("#ScheduleList").data("kendoGrid");
  var gridData = grid.dataSource.view();
  var target = gridData[e.newIndex];
  //var after_target = gridData[e.newIndex + 1];
  let dataItem = grid.dataSource.getByUid(e.item.data("uid"));

  if ((dataItem.IsScheduledStatus && target.IsScheduledStatus) || (target.IsScheduledStatus && dataItem.IsTestOrder)) {

    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], '', () => {
      //let oldIndex = e.oldIndex,
      //  newIndex = e.newIndex,
      //  data = grid.dataSource.data
      let newIndex = e.newIndex;

      grid.dataSource.remove(dataItem);
      grid.dataSource.insert(newIndex + $restrictedRows, dataItem);

      let dataToSend = {
        scheduleId: dataItem.ScheduleId,
        workOrderId: dataItem.WorkOrderId,
        seqId: dataItem.ScheduleOrderSeq,
        newIndex: target.ScheduleOrderSeq
      };
      AjaxReqestHelper(Url("Schedule", "MoveItemInSchedule"), dataToSend, RefreshData, RefreshData);
      $('.sweet-alert .cancel').off("click");
    }, RefreshData);
    handleCancel($('.sweet-alert .cancel'), RefreshData);
  } else {
    InfoMessage(Translations["MESSAGE_ActionNotAvailable"]);
    RefreshData();
  }
}

function handleCancel(btn, onEventFunction) {
  btn.click(() => {
    onEventFunction();
    btn.off("click");
  })
}

function displayScheduleLegendWindow() {
  let legendWindow = $('#scheduleLegend');
  if (legendWindow.css('display') === 'none') {
    legendWindow.show();
  } else {
    legendWindow.hide();
  }
}

function displayButtons(direction, scheduleId) {
  try {
    let grid = $("#ScheduleList").data("kendoGrid");
    let gridData = grid.dataSource.view();
    let availableRows = 0;
    let lastNotScheduled = 0;

    for (let i = 0; i < gridData.length; i++) {
      if (!gridData[i].MaterialsNumber || gridData[i].MaterialsNumber > gridData[i].RawMaterialsParents)
        availableRows++;
    }

    if (availableRows < 2)
      return false;

    // first with MaterialsNumber > RawMaterialsParents
    let actual = false;

    for (let i = 0; i < gridData.length; i++) {
      if (gridData[i].ScheduleId !== scheduleId) {
        if (!gridData[i].MaterialsNumber || !actual && gridData[i].MaterialsNumber > gridData[i].RawMaterialsParents)
          actual = true;
        if (!gridData[i].IsScheduledStatus)
          lastNotScheduled = gridData[i].ScheduleOrderSeq;
      } else {
        if (!gridData[i].IsScheduledStatus && !gridData[i].IsTestOrder)
          return false;

        switch (direction) {
          case 1:
            if (actual && !gridData[i].MaterialsNumber)
              return true;
            else if (!actual && gridData[i].RawMaterialsParents === 0)
              return false
            else if (!actual && gridData[i].MaterialsNumber > gridData[i].RawMaterialsParents)
              return false;
            else if (gridData[i-1] && actual && lastNotScheduled === gridData[i-1].ScheduleOrderSeq)
              return false;
            else
              return true;
          case -1:
            if (!gridData[i].MaterialsNumber) return true;

            if (i === gridData.length - 1)
              return false;
            else
              return true;
          default:
            console.log('displayButtons - wrong direction value');
            return false;
        }
      }
    }
  } catch (e) {
    console.error('displayButtons -' + e);
    return false;
  }
}

function changeItemSequence(direction, scheduleId, workOrderId, orderSeq) {
  try {
    let moveItem = function () {
      let grid = $("#ScheduleList").data("kendoGrid");
      let gridData = grid.dataSource.view();

      switch (direction) {
        case 1:
          moveItemInSchedule(scheduleId, workOrderId, orderSeq, orderSeq - 1);
          break;
        case -1:
          moveItemInSchedule(scheduleId, workOrderId, orderSeq, orderSeq + 1);
          break;
        case 2:
          for (let i = 0; i < gridData.length; i++) {
            if (gridData[i].IsScheduledStatus) {
              moveItemInSchedule(scheduleId, workOrderId, orderSeq, gridData[i].ScheduleOrderSeq);
              break;
            }
          }
          break;
        case -2:
          if (gridData.length) {
            moveItemInSchedule(scheduleId, workOrderId, orderSeq, gridData[gridData.length - 1].ScheduleOrderSeq);
          }
          break;
        default:
          console.log('moveItem - wrong direction value');
      }
    };
    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], "", moveItem);
  } catch (e) {
    console.error('moveItem -' + e);
  }
}

function EndOfWorkOrder(workOrderId) {
  let dataToSend = {
    workOrderId: workOrderId
  };

  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_ConfirmEndOfWorkOrder"], () => {
    AjaxReqestHelper(Url("Schedule", "EndOfWorkOrder"), dataToSend, RefreshData, function () { console.log('EndOfWorkOrder - failed'); });
  });
}
