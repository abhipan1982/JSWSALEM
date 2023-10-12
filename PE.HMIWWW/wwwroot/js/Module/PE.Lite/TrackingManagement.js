const AREAS = [];
const AreasOnHMI = [];

for (let key in TrackingAreaKeys) {
  const hmiArea = document.getElementById(key);
  if (hmiArea)
    AreasOnHMI.push(key);
}

for (let i = 0; i < AreasOnHMI.length; i++) {
  const key = AreasOnHMI[i];
  try {
    const areaDetails = _TrackingAreas.GetTrackingAreaByCode(TrackingAreaKeys[key]);
    if (!areaDetails)
      throw ("There is a problem while getting area " + trackingArea.Code + ". It is present on HMI but it cannot be found in AREAS. Check AREAS array.");

    const elementsInColumn = 36;   //Full column size
    if (TrackingAreaKeys[key] === TrackingAreaKeys.FCE_AREA) {
      AREAS.push({
        Name: areaDetails.TrackingAreaName,
        Title: areaDetails.TrackingAreaTitle,
        Code: TrackingAreaKeys[key],
        IsChangeable: true,
        IsDraggable: true,
        IsDroppable: true,
        Positions: areaDetails.TrackingAreaPositions,
        VirtualPositions: areaDetails.TrackingAreaVirtualPositions,
        ElementsInColumn: elementsInColumn,
        Columns: 2,
        CssClassName: "furnace",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.LAYER_AREA) {
      AREAS.push({
        Name: areaDetails.TrackingAreaName,
        Title: areaDetails.TrackingAreaTitle,
        Code: TrackingAreaKeys[key],
        IsChangeable: true,
        IsDraggable: true,
        IsDroppable: true,
        Positions: areaDetails.TrackingAreaPositions,
        VirtualPositions: areaDetails.TrackingAreaVirtualPositions,
        ElementsInColumn: 3,
        Columns: 1,
        CssClassName: "layer",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.CHG_AREA) {
      AREAS.push({
        Name: areaDetails.TrackingAreaName,
        Title: areaDetails.TrackingAreaTitle,
        Code: TrackingAreaKeys[key],
        IsChangeable: true,
        IsDraggable: true,
        IsDroppable: true,
        Positions: areaDetails.TrackingAreaPositions,
        VirtualPositions: areaDetails.TrackingAreaVirtualPositions,
        ElementsInColumn: elementsInColumn,
        Columns: 1,
        CssClassName: "charging",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.RAKE_AREA) {
      AREAS.push({
        Name: areaDetails.TrackingAreaName,
        Title: areaDetails.TrackingAreaTitle,
        Code: TrackingAreaKeys[key],
        IsChangeable: true,
        IsDraggable: true,
        IsDroppable: true,
        Positions: areaDetails.TrackingAreaPositions,
        VirtualPositions: areaDetails.TrackingAreaVirtualPositions,
        ElementsInColumn: 18,
        Columns: 4,
        CssClassName: "rake",
        IsDoubleArea: true,
        IsPositionBased: true,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.TRANSPORT_AREA) {
      AREAS.push({
        Name: areaDetails.TrackingAreaName,
        Title: areaDetails.TrackingAreaTitle,
        Code: TrackingAreaKeys[key],
        IsChangeable: true,
        IsDraggable: true,
        IsDroppable: true,
        Positions: areaDetails.TrackingAreaPositions,
        VirtualPositions: areaDetails.TrackingAreaVirtualPositions,
        ElementsInColumn: 5,
        Columns: 1,
        CssClassName: "transport",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: false
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.ENTER_TABLE_AREA) {
      AREAS.push({
        Name: areaDetails.TrackingAreaName,
        Title: areaDetails.TrackingAreaTitle,
        Code: TrackingAreaKeys[key],
        IsChangeable: true,
        IsDraggable: true,
        IsDroppable: true,
        Positions: areaDetails.TrackingAreaPositions,
        VirtualPositions: areaDetails.TrackingAreaVirtualPositions,
        ElementsInColumn: 3,
        Columns: 1,
        CssClassName: "entry",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: false
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.GARRET_AREA) {
      AREAS.push({
        Name: areaDetails.TrackingAreaName,
        Title: areaDetails.TrackingAreaTitle,
        Code: TrackingAreaKeys[key],
        IsChangeable: true,
        IsDraggable: true,
        IsDroppable: true,
        Positions: areaDetails.TrackingAreaPositions,
        VirtualPositions: areaDetails.TrackingAreaVirtualPositions,
        ElementsInColumn: 3,
        Columns: 1,
        CssClassName: "garrets",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: false
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.GREY_AREA) {
      AREAS.push({
        Name: areaDetails.TrackingAreaName,
        Title: areaDetails.TrackingAreaTitle,
        Code: TrackingAreaKeys[key],
        IsChangeable: true,
        IsDraggable: true,
        IsDroppable: false,
        Positions: areaDetails.TrackingAreaPositions,
        VirtualPositions: areaDetails.TrackingAreaVirtualPositions,
        ElementsInColumn: areaDetails.TrackingAreaPositions,
        Columns: 1,
        CssClassName: "grey",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: false
      });
    }
    else {
      AREAS.push({
        Name: areaDetails.TrackingAreaName,
        Title: areaDetails.TrackingAreaTitle,
        Code: TrackingAreaKeys[key],
        IsChangeable: false,
        IsDraggable: false,
        IsDroppable: false,
        Positions: areaDetails.TrackingAreaPositions,
        VirtualPositions: areaDetails.TrackingAreaVirtualPositions,
        ElementsInColumn: 2,
        Columns: 1,
        CssClassName: "unchangeable-area",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: false
      });
    }
  } catch (e) {
    console.error(e);
  }
}

function initRefreshHandler() {
  var timer = new Timer(onConnectionLost, 36000);

  SignalrConnection.onreconnected(function (state) {
    timer.reset();
    AjaxGetDataHelper(Url("Visualization", "RequestLastMaterialPosition"));
  });

  try {
    SignalrConnection.addToGroup("L1MaterialPositionRefresh");
    SignalrConnection.on("L1MaterialPositionRefresh", (refreshData) => {
      timer.reset();

      console.log(new Date(), refreshData);

      _TrackingManagementHelpers.SetLaneCommunication(refreshData.IsLaneStopped);
      _TrackingManagementHelpers.SetSlowProduction(refreshData.IsSlowProduction);
      refreshData = refreshData.Areas.reduce(function (map, obj) {
        map[obj.AreaId] = obj;
        return map;
      }, {});

      _TrackingManagementHelpers.RefreshAreas(refreshData, AREAS, LastAreaState);
      LastAreaState = refreshData;
    });

    new Timer(() => AjaxGetDataHelper(Url("Visualization", "RequestLastMaterialPosition")), 500);
  }
  catch (e) {
    console.error(e);
  }
}

function onConnectionLost(timer) {
  timer.reset();
  ErrorNotification(Translations["MESSAGE_outdated"]);
}

$(document).ready(function () {
  _TrackingManagementHelpers.InitAreas(AREAS, AreasOnHMI);
  initTrackingModes();
  AreaElements = _TrackingManagementHelpers.InitAreaElements();
  initRefreshHandler();
  _TrackingManagementHelpers.OnMaterialClickEvent();
  _TrackingManagementHelpers.InitDropTarget();
});

let switchSlowProdBtn;
let switchLaneProdBtn;
let switchSlowProd;
let switchLaneProd;
let switchSlowProdBtnKendo;
let switchLaneProdBtnKendo;

const MAX_MatDrag = 190;

let CurrentElement = {
  RawMaterialId: null
};
let currentMatSelected;
let draggableIndex = 0;
let rowStart;
let rowStartSeqNumber;
let areaStart;
let areaStartCode;
let currentMatId;
let currentMat;
let AreaElements = [];
let selectedRow;

function initTrackingModes() {
  switchSlowProd = $("#slowProd");
  switchLaneProd = $("#laneProd");

  switchSlowProdBtn = $("#slowProdBtn");
  switchLaneProdBtn = $("#laneProdBtn");

  switchSlowProdBtnKendo = switchSlowProdBtn.kendoSwitch({ checked: false }).data("kendoSwitch");
  switchLaneProdBtnKendo = switchLaneProdBtn.kendoSwitch({ checked: true }).data("kendoSwitch");

  switchSlowProdBtnKendo.readonly(!switchSlowProdBtnKendo.element.attr("readonly"));
  switchLaneProdBtnKendo.readonly(!switchLaneProdBtnKendo.element.attr("readonly"));

  switchSlowProd.click(() => {
    switchSlowProdBtn[0].checked ? _TrackingManagementActions.StopSlowProductionAction() : _TrackingManagementActions.StartSlowProductionAction();
  })

  switchLaneProd.click(() => {
    switchLaneProdBtn[0].checked ? _TrackingManagementActions.StartLaneCommunicationAction() : _TrackingManagementActions.StopLaneCommunicationAction();
  })
}

function setCustomAreaHeader(trackingAreaCode) {
  const trackingArea = _TrackingManagementHelpers.GetTrackingAreaByCode(AREAS, trackingAreaCode);
  if (!trackingArea)
    throw ("There is a problem while setting custom header. Area " + trackingArea.Code + " is not found. Check AREAS array.");
  let areaHTML = document.getElementById(trackingArea.Name);
  if (!areaHTML)
    throw ("There is a problem while setting custom header. Area " + trackingArea.Code + " cannot be found on screen.Check if area in on HMI.");

  let element = '';

  //Your code here

  let header = areaHTML.getElementsByClassName("table-area-header")[0];
  header.innerHTML = element + header.innerHTML;
}
