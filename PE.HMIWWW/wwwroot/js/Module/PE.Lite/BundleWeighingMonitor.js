RegisterMethod(HmiRefreshKeys.BundleWeighingMonitor, reloadWeighingData);

let LastWeighingState;

$(document).ready(function () {
  refreshMaterialDetailsPanel();
  initRefreshHandler();
});

function initRefreshHandler() {
  var timer = new Timer(onConnectionLost, 35000);

  SignalrConnection.onreconnected(function (state) {
    timer.reset();
    AjaxGetDataHelper(Url("Visualization", "RequestLastMaterialPosition"));
  });

  try {
    SignalrConnection.addToGroup("L1MaterialPositionRefresh");
    SignalrConnection.on("L1MaterialPositionRefresh", (refreshData) => {
      timer.reset();

      console.log(new Date(), refreshData);

      LastWeighingState = refreshData;
      reloadWeighingData();
    });

    new Timer(() => AjaxGetDataHelper(Url("Visualization", "RequestLastMaterialPosition")), 500);
  } catch (err) {
    console.log(err);
  }
}

function reloadWeighingData() {
  try {
    refreshMaterialDetailsPanel();

    $("#WeighingOverveiwBeforeScale").data("kendoGrid").dataSource.read({ materialPosition: LastWeighingState });
    $("#WeighingOverveiwBeforeScale").data("kendoGrid").refresh();

    $("#WeighingOverveiwAfterScale").data("kendoGrid").dataSource.read({ materialPosition: LastWeighingState });
    $("#WeighingOverveiwAfterScale").data("kendoGrid").refresh();
  } catch (err) {
    console.log(err);
  }
}

function onConnectionLost(timer) {
  timer.reset();
  ErrorNotification(Translations["MESSAGE_outdated"]);
}

function onSelect(e) {
  const grid = e.sender;
  const selectedRow = grid.select();
  const selectedItem = grid.dataItem(selectedRow);
  const rawMaterialId = selectedItem.RawMaterialId

  const url = "/LabelPrinter/GetLabePartialViewForRawMaterial";

  const dataToSend = {
    ImageHeight: 700,
    ImageWidth: 500,
    RawMaterialId: rawMaterialId
  }

  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setLabel);
}

function colorActiveWOrkOrder(e) {
  let data = e.sender.dataSource.data();
  for (var i = 0; i < data.length; i++) {
    if (i + 1 === data.length) {
      $('tr[data-uid="' + data[i].uid + '"]').css({ "background-color": "#77ce48", "color": "#fff" });
      contniue;
    } else {
      $('tr[data-uid="' + data[i].uid + '"]').css({ "background-color": "#6ba4b8", "color": "#fff" });
    }
  }
}

function setLabel(partialView) {
  $('#label_wrapper').html(partialView);
}

function refreshMaterialDetailsPanel() {
  let rawMaterialId = null;

  if (LastWeighingState) {
    let data = LastWeighingState.Areas.reduce(function (map, obj) {
      map[obj.AreaId] = obj;
      return map;
    }, {});

    var weighingArea = data[TrackingAreaKeys.BAR_WEIGHING_AREA];
    console.log(weighingArea);
    if (weighingArea && weighingArea.Materials.length) {
      let lastMaterial = weighingArea.Materials[weighingArea.Materials.length - 1];
      if (lastMaterial)
        rawMaterialId = lastMaterial.RawMaterialId;
    }
  }

  let dataToSend = {
    rawMaterialId: rawMaterialId
  };
  let url = "/BundleWeighingMonitor/GetMaterialOnWeight";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setMaterialDetailsPanelPartialView);
}

function setMaterialDetailsPanelPartialView(partialView) {
  $('#material-details-panel').html(partialView);
}

function getMaterialOnWeight() {
  let rawMaterialId = null;

  if (LastWeighingState) {
    let data = LastWeighingState.Areas.reduce(function (map, obj) {
      map[obj.AreaId] = obj;
      return map;
    }, {});

    var weighingArea = data[TrackingAreaKeys.BAR_WEIGHING_AREA];
    if (weighingArea && weighingArea.Materials.length) {
      let lastMaterial = weighingArea.Materials[weighingArea.Materials.length - 1];
      if (lastMaterial)
        rawMaterialId = lastMaterial.RawMaterialId;
    }

    return {
      rawMaterialId: rawMaterialId
    };
  }
}

function addQualityPopup(rawMaterialId) {
  if (!rawMaterialId) {
    InfoMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  OpenInPopupWindow({
    controller: "Inspection", method: "QualityAddPopup", width: 600, data: { rawMaterialId: rawMaterialId }, afterClose: reloadWeighingData
  });
}

function assignDefectsPopup(rawMaterialId) {
  if (!rawMaterialId) {
    InfoMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }
  OpenInPopupWindow({
    controller: "RawMaterial", method: "AssignDefectsPopup", width: 480, data: { rawMaterialId: rawMaterialId }
  });
}

function endOfWorkOrder() {
  var data = $("#WeighingOverveiwBeforeScale").data("kendoGrid").dataSource.data().sort(sortByOrderSeq);

  if (!data.length || data.length === 0 || data[0].WorkOrderId === 0 || !data[0].IsEndOfWorkOrderPossible) {
    InfoMessage(Translations["MESSAGE_CannotFindWorkOrderToFinish"]);
    return;
  }

  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_ConfirmEndOfWorkOrder"], () => {
    AjaxReqestHelper(Url("Schedule", "EndOfWorkOrder"), { workOrderId: data[0].WorkOrderId }, reloadWeighingData, function () { console.log('EndOfWorkOrder - failed'); });
  });
}

function createBundle() {
  var data = $("#WeighingOverveiwBeforeScale").data("kendoGrid").dataSource.data().sort(sortByOrderSeq);

  if (!data.length || data.length === 0 || data[0].WorkOrderId === 0) {
    InfoMessage(Translations["MESSAGE_CannotFindWorkOrderForBundleCreation"]);
    return;
  }

  _Product.BundleCreatePopup(data[0].WorkOrderId);
}

function sortByOrderSeq(a, b) {
  if (a.ScheduleOrderSeq < b.ScheduleOrderSeq) {
    return -1;
  }
  if (a.ScheduleOrderSeq > b.ScheduleOrderSeq) {
    return 1;
  }
  return 0;
}
