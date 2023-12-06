const AREAS = [];
const AreasOnHMI = [];

for (let key in TrackingAreaKeys) {
  const hmiArea = document.getElementById(key);
  if (hmiArea)
    AreasOnHMI.push(key);
}
/*av050723*/
//for (let key in CustomTrackingAreaKeys ) {
//  const hmiArea = document.getElementById(key);
//  if (hmiArea)
//    AreasOnHMI.push(key);
//}

for (let i = 0; i < AreasOnHMI.length; i++) {
  const key = AreasOnHMI[i];
  try {
    const areaDetails = _TrackingAreas.GetTrackingAreaByCode(TrackingAreaKeys[key]);
    
    if (!areaDetails)
      throw ("There is a problem while getting area " + trackingArea.Code + ". It is present on HMI but it cannot be found in AREAS. Check AREAS array.");
    const x = 36;
    const y = 33;
    const elementsInColumn = 31;   //Full column size
    if (TrackingAreaKeys[key] === TrackingAreaKeys.FCE_AREA_1) {
      
      AREAS.push({
        Name: areaDetails.TrackingAreaName,
        Title: areaDetails.TrackingAreaTitle,
        Code: TrackingAreaKeys[key],
        IsChangeable: true,
        IsDraggable: true,
        IsDroppable: true,
        Positions: areaDetails.TrackingAreaPositions,
        VirtualPositions: areaDetails.TrackingAreaVirtualPositions,
        
        ElementsInColumn: x,
        Columns: 4,
        CssClassName: "furnace",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }

    else if (TrackingAreaKeys[key] === TrackingAreaKeys.FCE_ENTRY_RT_AREA) {
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
        CssClassName: "FURNACE_ROLLER_ENTRY",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.FCE_EXIT_AREA) {
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
        CssClassName: "FURNACE_ROLLER_EXIT",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.TBT_AREA) {
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
        CssClassName: "TBT_OUT_GRID",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.REV_EXIT_AREA) {
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
        CssClassName: "REV_EXIT_AREA",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable:true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.CB1_ENTRY_RT) {
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
        CssClassName: "CB1_ENTRY_RT",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.COOL_AREA_1) {
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
        Columns: 3,
        CssClassName: "COOL_AREA_1",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.CHG_AREA_1) {
      AREAS.push({
        Name: areaDetails.TrackingAreaName,
        Title: areaDetails.TrackingAreaTitle,
        Code: TrackingAreaKeys[key],
        IsChangeable: true,
        IsDraggable: true,
        IsDroppable: true,
        Positions: areaDetails.TrackingAreaPositions,
        //VirtualPositions: areaDetails.TrackingAreaVirtualPositions,
        ElementsInColumn: elementsInColumn,
        Columns: 1,
        CssClassName: "charging",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }

    else if (TrackingAreaKeys[key] === TrackingAreaKeys.DSC_AREA) {
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
        CssClassName: "DESCALER",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }

    else if (TrackingAreaKeys[key] === TrackingAreaKeys.REV_AREA) {
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
        CssClassName: "REVERSIBLE",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.COOL_AREA_2_S1) {
      AREAS.push({
        Name: areaDetails.TrackingAreaName,
        Title: areaDetails.TrackingAreaTitle,
        Code: TrackingAreaKeys[key],
        IsChangeable: true,
        IsDraggable: true,
        IsDroppable: true,
        Positions: areaDetails.TrackingAreaPositions,
        VirtualPositions: areaDetails.TrackingAreaVirtualPositions,
        ElementsInColumn: x,
        Columns: 4,
        CssClassName: "COOL_2_S1",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.COOL_AREA_2_S2) {
      AREAS.push({
        Name: areaDetails.TrackingAreaName,
        Title: areaDetails.TrackingAreaTitle,
        Code: TrackingAreaKeys[key],
        IsChangeable: true,
        IsDraggable: true,
        IsDroppable: true,
        Positions: areaDetails.TrackingAreaPositions,
        VirtualPositions: areaDetails.TrackingAreaVirtualPositions,
        ElementsInColumn: y,
        Columns: 5,
        CssClassName: "COOL_2_S2",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.COOL_AREA_3_S1) {
      AREAS.push({
        Name: areaDetails.TrackingAreaName,
        Title: areaDetails.TrackingAreaTitle,
        Code: TrackingAreaKeys[key],
        IsChangeable: true,
        IsDraggable: true,
        IsDroppable: true,
        Positions: areaDetails.TrackingAreaPositions,
        VirtualPositions: areaDetails.TrackingAreaVirtualPositions,
        ElementsInColumn: y,
        Columns: 5,
        CssClassName: "COOL_3_S1",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.COOL_AREA_3_S2) {
      AREAS.push({
        Name: areaDetails.TrackingAreaName,
        Title: areaDetails.TrackingAreaTitle,
        Code: TrackingAreaKeys[key],
        IsChangeable: true,
        IsDraggable: true,
        IsDroppable: true,
        Positions: areaDetails.TrackingAreaPositions,
        VirtualPositions: areaDetails.TrackingAreaVirtualPositions,
        ElementsInColumn: y,
        Columns: 5,
        CssClassName: "COOL_3_S2",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.CM_AREA) {
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
        CssClassName: "CONTINUOUS",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.BTRT_AREA) {
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
        CssClassName: "BTRT",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.HOT_SAW_2) {
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
        CssClassName: "SAW",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.HOT_SAW_3A) {
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
        CssClassName: "SAW",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.HOT_SAW_3) {
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
        CssClassName: "SAW",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.CB2_ENTRY_RT) {
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
        CssClassName: "CB2_ENTRY_RT",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.CB2_EXIT_RT) {
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
        CssClassName: "CB2_EXIT_RT",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.CB3_EXIT_RT) {
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
        CssClassName: "CB3_EXIT_RT",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.CB3_ENTRY_RT) {
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
        CssClassName: "CB3_ENTRY_RT",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.SLOW_COOL_AREA) {
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
        CssClassName: "slow_cooling",
        IsDoubleArea: false,
        IsPositionBased: true,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.COOL_AREA_2) {
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
        CssClassName: "cool_2",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.COOL_AREA_3) {
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
        CssClassName: "cool_3",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.A_SAW_ENTRY1) {
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
        CssClassName: "A_SAW_ENTRY1",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.A_SAW_ENTRY_2) {
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
        CssClassName: "A_SAW_ENTRY_2",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.BUND_AREA_1) {
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
        CssClassName: "Bund_1",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.BUND_AREA_2) {
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
        CssClassName: "Bund_2",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.A_SAW_ENTRY_AREA) {
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
        CssClassName: "SAW_ENTRY",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
      });
    }
    else if (TrackingAreaKeys[key] === TrackingAreaKeys.A_SAW_EXIT_AREA) {
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
        CssClassName: "SAW_EXIT",
        IsDoubleArea: false,
        IsPositionBased: false,
        IsExpandable: true
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
