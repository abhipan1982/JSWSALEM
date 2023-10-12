RegisterMethod(HmiRefreshKeys.Furnace, ReadData);

var timer;
let pyrometersGridEl;
let infoPanel;
let legendWindow;
let zone;
let CurrentElement;
//const TotalPostionsNumber = 82;

$(document).ready(function () {
  initRefreshHandler();
  initElements();
  //createPyrometersPositions();
  ReadData();
  initMaterialInfo();
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

      refreshData = refreshData.Areas.reduce(function (map, obj) {
        map[obj.AreaId] = obj;
        return map;
      }, {});

      LastAreaState = refreshData;
      ShowMaterialsInChargingArea();
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

function initElements() {
  pyrometersGridEl = $('#pyrometers-grid');
  legendWindow = $('.legend-window');
  zoneEl = $('.zone-body');
  infoPanel = {
    rawMatName: $('#lbl_rawname'),
    timeInFurnace: $('#lbl_timeInFurnace'),
    length: $('#lbl_length'),
    weight: $('#lbl_weight'),
    WOName: $('#lbl_woname'),
    heat: $('#lbl_heat'),
    stealgrade: $('#lbl_steelgrade'),
  }
}

function displayLegendWindow() {
  if (legendWindow.css('display') === 'none') {
    legendWindow.show();
  } else {
    legendWindow.hide();
  }
}

function initMaterialInfo() {
  zoneEl.on('mouseover', '.occupied-position', function () {
    const info_pane = $('.info-pane');
    let foo = $(this);
    timer = setTimeout(function () {
      let heat = "-"; if (foo.attr('data-heat') !== "null") heat = foo.attr('data-heat');
      let steelGrade = "-"; if (foo.attr('data-steelgrade') !== "null") steelGrade = foo.attr('data-steelgrade');
      let workOrder = "-"; if (foo.attr('data-workorder') !== "null") workOrder = foo.attr('data-workorder');
      let weight = "-"; if (foo.attr('data-weight') !== "null") weight = foo.attr('data-weight');
      let length = "-"; if (foo.attr('data-length') !== "null") length = foo.attr('data-length');
      let timeInFurnace = "-"; if (foo.attr('data-timeinfurnace') !== "null") timeInFurnace = foo.attr('data-timeinfurnace');


      infoPanel.rawMatName.html(foo.attr('data-rawmaterialname'));
      infoPanel.timeInFurnace.html(timeInFurnace);
      infoPanel.length.html(length);
      infoPanel.weight.html(weight);
      infoPanel.WOName.html(workOrder);
      infoPanel.heat.html(heat);
      infoPanel.stealgrade.html(steelGrade);

      let left = foo.offset().left;
      if (foo.position().left + info_pane.width() < 1920)
        info_pane.css({ 'left': left + 35 });
      else
        info_pane.css({ 'left': left - info_pane.width() - 35 });

      info_pane.show();
    }, 1000);
  });

  zoneEl.on('mouseout', '.raw-material', function () {
    const info_pane = $('.info-pane');
    info_pane.hide();
    clearTimeout(timer);
  });
}

function ReadData() {
  AjaxReqestHelperSilent(Url("Furnace", "GetMaterialInFurnace"), {}, PrintData, PrintData);
 // AjaxReqestHelperSilent(Url("Furnace", "GetFurnaceTemperature"), {}, insertPyrometers, insertPyrometers);
  RefresTables();
}

function RefresTables() {
  var storage = $("#StorageTable").data("kendoGrid");
  if (storage) {
    storage.dataSource.read();
    storage.refresh();
  }
}

function PrintData(data) {
  zoneEl.find('div').remove();
  $('.ribbon-strip').find('div').remove();

  for (var i = 0; i < data.length; i++) {
    let place = ".zone-body";
    let pointner = "";
    if (data[i].WorkOrderId !== null && data[i].HeatId !== null && data[i].SteelGradeId)
      pointner = "clickable";

    let newMaterial = "";

    let textColor = GetTextColorModulo16(data[i].RawMaterialId);
    let bgColor = GetColorModulo16(data[i].RawMaterialId);

    if (data[i].RawMaterialId !== 0 && data[i].RawMaterialId !== null) {
      newMaterial = '<div id = "' + data[i].RawMaterialId + '"' +
        'onClick="OpenSlideScreen(' + data[i].RawMaterialId + ',' + data[i].WorkOrderId + ',' + data[i].SteelGradeId + ',' + data[i].HeatId + ')"' +
        'class= "raw-material occupied-position ' + pointner + '"' +
        'data-workorder="' + data[i].WorkOrderName + '"' +
        'data-rawmaterialname="' + data[i].RawMaterialName + '"' +
        'data-timeinfurnace="' + data[i].TimeInFurnace + '"' +
        'data-length="' + data[i].Length + '"' +
        'data-weight="' + data[i].Weight + '"' +
        'data-steelgrade="' + data[i].SteelgradeName + '"' +
        'data-heat="' + data[i].HeatName + '"' +
        'data-woid="' + data[i].WorkOrderId + '"' +
        'data-heatid="' + data[i].HeatId + '"' +
        'data-steelgradeid="' + data[i].SteelGradeId + '"' +
        '>' +
        "<div class='material-labels' style='color: " + textColor + "; background-color:" + bgColor + "'>" +
        //"<div class='raw-temp'>" + Math.round(data[i].SetupTemp * 100) / 100 + "</div>" +
        "<div class='raw-name'>" + data[i].RawMaterialName + "</div>" +
        "</div>" +
        //'<div class="temp-chart"><div class="temp-value w-100"></div></div>' +
        //"<div class=seq-number><p>" + data[i].Position + "</p></div>" +
        '</div>';
    }
    else {
      newMaterial = '<div ' +
        'class= "raw-material empty-position">' +
        '<div class="empty-position"></div>' +
        //'<div class="temp-chart"><div class="temp-value w-100"></div></div>'+
        //"<div class=seq-number><p>" + data[i].Position + "</p></div>" +
        '</div>';
    }

    $(place).append(newMaterial);

    //GetColor(data[i].State);
    //if (data[i].RawMaterialId !== 0 && data[i].RawMaterialId !== null) {
    //  PlannedTemp(data[i].SetupTemp);
    //}
    //CurrentTemp(data[i].CurrentTemp);

    if (data[i].WorkOrderId !== null) {
      PrintWorkOrderRibbon(data[i].WorkOrderId, data[i].WorkOrderName);
    }
    if (data[i].HeatId !== null) {
      PrintHeatRibbon(data[i].HeatId, data[i].HeatName);
    }
  }
}

function OpenSlideScreen(id, workorderid, steelgradeid, heatid) {
  CurrentElement = {
    RawMaterialId: id,
  };
  let dataToSend = {
    RawMaterialId: id
  };
  let popupTitle = Translations["NAME_RawMaterial"];
  openSlideScreen('RawMaterial', 'ElementDetails', dataToSend, popupTitle);
}

function OpenSlideWorkOrderScreen(workorderid) {
  let dataToSend = {
    workOrderId: workorderid
  };
  openSlideScreen('WorkOrder', 'ElementDetails', dataToSend);
}
function OpenSlideHeatScreen(heatId) {
  let dataToSend = {
    heatId: heatId
  };
  openSlideScreen('Heat', 'ElementDetails', dataToSend);
}

function GetColor(state) {
  //let bgColor = GetColorModulo16(state);
  //let textColor = GetTextColorModulo16(state);

  //$('.raw-material > div.material-labels').last().css({ 'background-color': bgColor });
  //$('.raw-material > div.material-labels').last().css({ 'color': textColor });
  $('.raw-material > div.material-labels > div.raw-temp').last().css({ 'background-color': 'rgb(255,255,255,0.4)' });
}
//                      Test       Temp       Time       Ready      Ox start   Oxidized
//const colorBgArray = ['#b5b5b5', '#b5b5b5', '#00a8cc', '#f01c31', '#5bbb21', '#a34a28', '#1b262c'];
//const colorTextArray = ['#ffffff', '#ffffff', '#ffffff', '#ffffff', '#ffffff', '#ffffff', '#ffffff'];

//function GetMatBackgroundColor(index) {
//  return colorBgArray[index];
//}

//function GetMatTextColor(index) {
//  return colorTextArray[index];
//}



function PrintWorkOrderRibbon(workOrderId, workOrderName) {
  let woDiv = $('*[data-workorderid="' + workOrderId + '"');
  if (woDiv.length > 0) {
    if ($('div.work-order').last().attr('data-workorderid') == workOrderId) {
      let width = $('.raw-material:not(.empty-position)').last().offset().left - woDiv.last().offset().left;
      woDiv.last().width(width + 10);
    }
    else {
      let left = $('div.raw-material:not(.empty-position)').last().offset().left - $('.zone-body').offset().left - 3;
      let color = GetColorModulo16(workOrderId);
      $('.ribbon-strip.work-order-ribbon').append("<div onclick='OpenSlideWorkOrderScreen(" + workOrderId + ")' class='work-order detail-block' title=" + workOrderName + " style='width:18px;' data-workorderid = '" + workOrderId + "'>" + workOrderName + "</div>");
      $('*[data-workorderid="' + workOrderId + '"').last().css({ 'left': left });
      $('*[data-workorderid="' + workOrderId + '"').last().css({ 'background-color': color });
    }
  }
  else {
    let left = $('div.raw-material:not(.empty-position)').last().offset().left - $('.zone-body').offset().left - 3;
    let color = GetColorModulo16(workOrderId);
    $('.ribbon-strip.work-order-ribbon').append("<div onclick='OpenSlideWorkOrderScreen(" + workOrderId + ")' class='work-order detail-block' title=" + workOrderName + " style='width:18px;' data-workorderid = '" + workOrderId + "'>" + workOrderName + "</div>");
    $('*[data-workorderid="' + workOrderId + '"').last().css({ 'left': left });
    $('*[data-workorderid="' + workOrderId + '"').last().css({ 'background-color': color });
  }

}

function PrintHeatRibbon(heatId, heatName) {
  let heatDiv = $('div.heat[data-heatid="' + heatId + '"');
  if (heatDiv.length > 0) {
    if ($('div.heat').last().attr('data-heatid') == heatId) {
      let width = $('.raw-material:not(.empty-position)').last().offset().left - heatDiv.last().offset().left;
      heatDiv.last().width(width + 10);
    }
    else {
      let left = $('div.raw-material:not(.empty-position)').last().offset().left - $('.zone-body').offset().left - 3;
      let color = GetColorModulo16(heatId);
      $('.ribbon-strip.heat-ribbon').append("<div onclick='OpenSlideHeatScreen(" + heatId + ")' title=" + heatName + " class='heat detail-block' style='width: 18px; ' data-heatid = '" + heatId + "'>" + heatName + "</div>");
      $('.detail-block[data-heatid="' + heatId + '"').last().css({ 'left': left });
      $('.detail-block[data-heatid="' + heatId + '"').last().css({ 'background-color': color });
    }
  }
  else {
    let left = $('div.raw-material:not(.empty-position)').last().offset().left - $('.zone-body').offset().left - 3;
    let color = GetColorModulo16(heatId);
    $('.ribbon-strip.heat-ribbon').append("<div onclick='OpenSlideHeatScreen(" + heatId + ")' title=" + heatName + " class='heat detail-block' style='width:18px;' data-heatid = '" + heatId + "'>" + heatName + "</div>");
    $('.detail-block[data-heatid="' + heatId + '"').last().css({ 'left': left });
    $('.detail-block[data-heatid="' + heatId + '"').last().css({ 'background-color': color });
  }
}

function GetLEft() {

}

//function createPyrometersPositions() {
//  for (let i = 1; i <= TotalPostionsNumber; i++) {
//      let fieldEl = "<div class='pyrometer-position' data-sequence='" + i + "'></div>";
//      pyrometersGridEl.append(fieldEl);
//  }
//}

//function insertPyrometers(pyrometersData) {
//  clearPyrometers();
//  let pyrometersDataArray = [
//    {
//      position: pyrometersData.Position1,
//      temp: Math.floor(pyrometersData.Temperature1)
//    },
//    {
//      position: pyrometersData.Position2,
//      temp: Math.floor(pyrometersData.Temperature2)
//    },
//    {
//      position: pyrometersData.Position3,
//      temp: Math.floor(pyrometersData.Temperature3)
//    },
//    {
//      position: pyrometersData.Position4,
//      temp: Math.floor(pyrometersData.Temperature4)
//    }
//  ];

//  pyrometersData = pyrometersDataArray;

//  for (let i = 0; i < pyrometersData.length; i++) {
//    let positionEl = pyrometersGridEl.find("[data-sequence='" + pyrometersData[i].position + "']");
//    if (positionEl) {
//      let pyrometerEl = "<div class='pyrometer'><div class='pyrometer-arrow'></div><span class='pyrometer-temp'>" + pyrometersData[i].temp + Translations["UNIT_Temperature"] + "</div></div>";
//      $(positionEl).append(pyrometerEl);
//    }
//  }
//}

//function clearPyrometers() {
//  pyrometersGridEl.find('.pyrometer').remove();
//}

//furnace actions
//function StepForwardAction() {
//  let action = 'StepForward';
//  sendRequestWithConfirmation(action);
//}

//function StepBackwardAction() {
//  let action = 'StepBackward';
//  sendRequestWithConfirmation(action);
//}

function sendRequestWithConfirmation(action, parameters = null, message = '', onSuccessMethod = null) {

  if (!action) return;

  let url = Url("Furnace", action);

  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], message, () => {
    AjaxReqestHelperSilentWithoutDataType(url, parameters, onSuccessMethod);
  });
}

function CurrentTemp(temp) {
  let percentage = ((temp * 100) / maxFurnaceTemp);
  if (percentage > 100) percentage = 100;
  $('.raw-material').last().find('div.temp-chart > .temp-value').css({ "background-color": "#ffd500" });
  $('.raw-material').last().find('div.temp-chart > .temp-value').css({ "height": percentage + "%" });
  $('.raw-material').last().find('div.temp-chart > .temp-value').html('<div>' + temp + '</div>');
}

function PlannedTemp(temp) {
  let percentage = ((temp * 100) / maxFurnaceTemp);
  if (percentage > 100) percentage = 100;
  $('.raw-material').last().find('div.temp-chart').append('<div class="temp-pick" style="height:' + percentage + '%"></div>');

}

function GoToWorkOrder(workOrderId) {
  let dataToSend = {
    WorkOrderId: workOrderId
  };
  openSlideScreen('WorkOrder', 'ElementDetails', dataToSend);
}

function GoToHeat(heatId) {
  let dataToSend = {
    heatId: heatId
  };
  openSlideScreen('Heat', 'ElementDetails', dataToSend);
}

function GoToSteelgrade(steelgradeId) {
  let dataToSend = {
    Id: steelgradeId
  };
  openSlideScreen('Steelgrade', 'ElementDetails', dataToSend);
}

function GoToMeasurement(MeasurementId) {
  let dataToSend = {
    measurementId: MeasurementId
  };
  openSlideScreen('RawMaterial', 'MeasurementDetails', dataToSend);
}

function GoToHistory(RawMaterialStepId) {
  let popupTitle = Translations["NAME_RawMaterial"];
  let dataToSend = {
    rawMaterialStepId: RawMaterialStepId
  };
  openSlideScreen('RawMaterial', 'HistoryDetails', dataToSend, popupTitle);
}

function ShowMaterialsInChargingArea() {
  let dataToSend = _Charging.GetMaterialsInArea(this);
  AjaxReqestHelperSilent(Url("Furnace", "GetMaterialsInChargingArea"), dataToSend, WriteData, WriteData);
}

function WriteData(data) {
  var grid = $("#ChargingTable").data("kendoGrid");
  grid.dataSource.data(data);
  grid.refresh();
}
