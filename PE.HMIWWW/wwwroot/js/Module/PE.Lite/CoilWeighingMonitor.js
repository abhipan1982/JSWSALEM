RegisterMethod(HmiRefreshKeys.CoilWeighingMonitor, reloadWeighingData);

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

    var weighingArea = data[TrackingAreaKeys.COIL_WEIGHING_AREA];
    if (weighingArea && weighingArea.Materials.length) {
      let lastMaterial = weighingArea.Materials[weighingArea.Materials.length - 1];
      if (lastMaterial)
        rawMaterialId = lastMaterial.RawMaterialId;
    }
  }

  let dataToSend = {
    rawMaterialId: rawMaterialId
  };
  let url = "/CoilWeighingMonitor/GetMaterialOnWeight";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setMaterialDetailsPanelPartialView);
}

function setMaterialDetailsPanelPartialView(partialView) {
  $('#material-details-panel').html(partialView);
}
