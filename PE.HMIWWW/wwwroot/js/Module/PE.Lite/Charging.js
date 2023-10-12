
$(document).ready(function () {
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

      //setLaneCommunication(refreshData.IsLaneStopped);
      //setSlowProduction(refreshData.IsSlowProduction);
      refreshData = refreshData.Areas.reduce(function (map, obj) {
        map[obj.AreaId] = obj;
        return map;
      }, {});

      LastAreaState = refreshData;
      ShowMaterialsInArea();
    });

    new Timer(() => AjaxGetDataHelper(Url("Visualization", "RequestLastMaterialPosition")), 500);
  }
  catch (err) {
    console.log(err);
  }
}
function onConnectionLost(timer) {
  timer.reset();
  //ErrorNotification(Translations["MESSAGE_outdated"]);
}

function ShowMaterialsInArea() {

  let dataToSend = _Charging.GetMaterialsInArea(this);
  AjaxReqestHelperSilent(Url("Charging", "GetChargingRawMateralsList"), dataToSend, WriteData, WriteData);
}

function WriteData(data) {
  var grid = $("#ChargingRawMaterialsList").data("kendoGrid");
  grid.dataSource.data(data);
  grid.refresh();
}
