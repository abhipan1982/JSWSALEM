const unAssignedMsg = Translations["NAME_Unassigned"];
const rawMatMsg = Translations["NAME_RawMaterial"];

let CurrentElement = {
  RawMaterialId: null
};

const areasTypes = {
  MULTI: 'multi',
  DOUBLE: 'double',
  LAYERS: 'layers',
  RAKE: 'rake'
};

const MatActions = {
  REMOVE: 'REMOVE',
  REJECT: 'REJECT',
  RETRACK_FURNACE: 'RETRACK_FURNACE',
  MAT_READY: 'MAT_READY',
  SCRAP: 'SCRAP',
  UN_SCRAP: 'UN_SCRAP',
  PARTIAL_SCRAP: 'PARTIAL_SCRAP',
  ADD_DEFECTS: 'ADD_DEFECTS'
};

var areas = [];
var materialDetails = {};

const WIDTH_100 = '100%';
const WIDTH_50 = '50%';
let material;
let width;

let legendWindow;
let selectedTrackingMaterialEl;
let layerSelectedId;

$(document).ready(function () {
  legendWindow = $('#coolingnLegend');
  initZones();
  initRakePositions();
  initMaterialDetails();
  initRefreshHandler();
  onMaterialActionSelect();
});

function initZones() {
  const htmlAreas = $('.zone');
  htmlAreas.each((i, el) => {
    areas.push({
      id: el.id,
      area: el,
      materialsNumber: $(el).find('.materials_number'),
      materialsArea: $(el).find('.materials'),
      layersArea: $(el).find('.layers'),
      ModeProductionSignalIcon: $(el).find('.production-icon'),
      ModeAdjustSignalIcon: $(el).find('.adjust-icon'),
      ModeSimulationSignalIcon: $(el).find('.sim-icon'),
      ModeAreaStartedSignalIcon: $(el).find('.start-icon'),
      materials: [], // list of material ids in area
      layers: [], // list of layers ids in area
      materialObjectList: [], // list of materials in area
      layersObjectList: [], // list of materials in area
      type: $(el).data('type'),
    })
  });

  $('.rightToLeft .materials_area').each((i, el) => {
    $(el).addClass('rightToLeft');
  });

  onMaterialSelect();
}

function initMaterialDetails() {
  materialDetails.currentRawMaterial = null; /// selected material id
  materialDetails.area = null;
  materialDetails.isMaterialAssigned = false;
  materialDetails.mesurementsInitialized = false;
  materialDetails.scraped = false;
  materialDetails.color = '';
  materialDetails.DOMElement = null;
  materialDetails.data = {};
  materialDetails.fields = {
    scrap: $('#scrap'),
    unScrap: $('#unScrap'),
    color: $('#material-color'),
    materialName: $('#materialName'),
    workOrderName: $('#workOrderName'),
    heatName: $('#heatName'),
    steelgradeName: $('#steelgradeName'),
  };

  $("#materialNameDetails").click(function () { _RawMaterial.GoToRawMaterial(CurrentElement.RawMaterialId); return false; });
  materialDetails.fields.workOrderName.click(function () { _WorkOrder.GoToWorkOrder(materialDetails.data.workOrderId); return false; });
  materialDetails.fields.heatName.click(function () { _Heat.GoToHeat(materialDetails.data.heatId); return false; });
  materialDetails.fields.steelgradeName.click(function () { _Steelgrade.GoToSteelgrade(materialDetails.data.steelgradeId); return false; });
}

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

      refreshAreas(refreshData);
      refreshMeasaurements();
      reloadAreasGrids();
    });

    new Timer(() => AjaxGetDataHelper(Url("Visualization", "RequestLastMaterialPosition")), 500);
  }
  catch (err) {
    console.log(err);
  }
}

function onConnectionLost(timer) {
  timer.reset();
  ErrorNotification(Translations["MESSAGE_outdated"]);
}

function initRakePositions() {
  const trackingArea = _TrackingAreas.GetTrackingAreaByCode(TrackingAreaKeys.RAKE_AREA);
  let rakeAreaEl = $(".zone-rake .zone_body");

  for (let i = 1; i <= trackingArea.TrackingAreaPositions; i++) {
    let rakePositionEl = '<div class="rakePosition data-position="' + i + '">' + '<div class="rakePosition-material" data-sequence="' + i + '" ></div>' + '<div class="rakePosition-number">' + i + '</div>' + '</div>';
    rakeAreaEl.append(rakePositionEl);
  }
}

function getLastMaterial(newAreasState) {

  while (Object.keys(newAreasState).length) {
    let lastAreaKey = Object.keys(newAreasState).pop();
    newAreasState[lastAreaKey].Materials.sort((a, b) => (a.PositionOrder > b.PositionOrder) ? -1 : 1);
    let lastMatIndex = newAreasState[lastAreaKey].Materials.length - 1;
    if (lastMatIndex >= 0) {
      return [newAreasState[lastAreaKey].Materials[lastMatIndex], lastAreaKey];
    } else {
      delete newAreasState[lastAreaKey];
    }
  }
  return null;
}

function onMaterialSelect() {
  $(".tracking_visualization").on('click', '.material', function () {
    materialDetails.DOMElement = $(this);
    materialDetails.currentRawMaterial = materialDetails.DOMElement.data('mat-id');
    materialDetails.area = materialDetails.DOMElement.closest('.area').data('area');
    materialDetails.DOMElement.data('area', materialDetails.area);
    CurrentElement = {
      RawMaterialId: materialDetails.currentRawMaterial
    };
    materialDetails.IsScrap = materialDetails.DOMElement.data('scraped');
    refreshMeasaurements();
    refreshMaterialDetails(materialDetails.DOMElement.css('background-color'));
  });
}

function setScrapIconInMaterialPanel(IsScrap = null) {
  if (IsScrap === null) { return; }
  if (IsScrap) {
    materialDetails.fields.unScrap.css('display', 'block');
    materialDetails.fields.scrap.css('display', 'none');
  } else {
    materialDetails.fields.unScrap.css('display', 'none');
    materialDetails.fields.scrap.css('display', 'block');
  }
}

function refreshMaterialDetails(color) {
  if (!materialDetails.currentRawMaterial) { clearMaterialDetails(); return; }
  setScrapIconInMaterialPanel(materialDetails.IsScrap);

  let parameters = { rawMaterialId: materialDetails.currentRawMaterial }
  let data = AjaxGetDataHelper(Url("Visualization", "GetTrackingMaterialDetails"), parameters);

  if (!data) { clearMaterialDetails(); return; }
  populateMaterialDetails(data, color || 'transparent');
}

function refreshMeasaurements() {
  if (!materialDetails.currentRawMaterial) { return; }

  let parameters = { rawMaterialId: materialDetails.currentRawMaterial }
  let data = AjaxGetDataHelper(Url("Measurements", "GeMaterialMeasurements"), parameters);

  if (!data) { clearMeasurements(); return; }
  populateMeasaurements(data);
}

function onMaterialSelectOutsideTracking(rawMaterialId, areaId) {
  closeSlideScreen();
  materialDetails.DOMElement = null;
  materialDetails.currentRawMaterial = rawMaterialId;
  materialDetails.area = areaId;

  CurrentElement = {
    RawMaterialId: rawMaterialId
  };

  materialDetails.color = GetColorModulo16(rawMaterialId);
  refreshMaterialDetails(materialDetails.color);
  refreshMeasaurements();
}

function getNewMatAreaId(materialId, newAreasState) {
  let newAreaId = null;

  $.each(newAreasState, function (key, value) {
    for (i = 0; i < value.Materials.length; i++) {
      if (materialId == value.Materials[i].RawMaterialId) {
        newAreaId = key;
        return;
      }
    }
  });
  return newAreaId;
}

function refreshAreas(newAreasState) {
  for (let i = 0; i < areas.length; i++) {
    let newAreaState = newAreasState[areas[i].id] || {};

    if (newAreaState.AreaId == TrackingAreaKeys.LAYER_AREA) {
      displayLayers(areas[i], newAreaState.Layers || []);
      continue;
    }

    switch (areas[i].type) {
      case areasTypes.MULTI:
        displayMaterialsInZone_multiple(areas[i], newAreaState.Materials || [], newAreaState.Signals || null);
        break;
      case areasTypes.DOUBLE:
        displayMaterialsInZone_double(areas[i], newAreaState.Materials || [], newAreaState.Signals || null);
        break;
      case areasTypes.RAKE:
        displayMaterialsInZone_rake(areas[i], newAreaState.Materials || [], newAreaState.Signals || null);
        break;
      default:
        break;
    }

    LastAreaState = newAreasState;
  }

  if (materialDetails.currentRawMaterial != null) {
    updateMaterialPanel(materialDetails.currentRawMaterial, newAreasState);
  }
}

function updateMaterialPanel(rawMaterialId, newAreasState) {
  let areaId = getNewMatAreaId(rawMaterialId, newAreasState);

  if (areaId) {
    materialDetails.area = areaId;
    if (materialDetails.DOMElement) {
      materialDetails.DOMElement.data('area', areaId)
    }
    return;
  }

  let newMatWithAreaArray = getLastMaterial(newAreasState);
  if (newMatWithAreaArray != null) {
    materialDetails.IsScrap = newMatWithAreaArray[0].IsScrap;
    onMaterialSelectOutsideTracking(newMatWithAreaArray[0].RawMaterialId, newMatWithAreaArray[1]);
  } else {
    clearMaterialDetails();
    clearMeasurements();
    materialDetails.currentRawMaterial = null;
    materialDetails.area = null;
    CurrentElement = {
      RawMaterialId: null
    };
  }
}

function setSignals(area, signals) {
  if (!signals) return;
  signals.ModeProduction ? area.ModeProductionSignalIcon.addClass('active') : area.ModeProductionSignalIcon.removeClass('active');
  signals.ModeAdjust ? area.ModeAdjustSignalIcon.addClass('active') : area.ModeAdjustSignalIcon.removeClass('active');
  signals.Simulation ? area.ModeSimulationSignalIcon.addClass('active') : area.ModeSimulationSignalIcon.removeClass('active');
  signals.AutomaticRelease ? area.ModeAreaStartedSignalIcon.addClass('active') : area.ModeAreaStartedSignalIcon.removeClass('active');
  //signals.Empty ? area.ModeAreaStartedSignalIcon.addClass('active') : area.ModeAreaStartedSignalIcon.removeClass('active');
  //signals.CobbleDetected ? area.ModeAreaStartedSignalIcon.addClass('active') : area.ModeAreaStartedSignalIcon.removeClass('active');
  //signals.ModeLocal ? area.ModeAreaStartedSignalIcon.addClass('active') : area.ModeAreaStartedSignalIcon.removeClass('active');
  //signals.CobbleDetectionSelected ? area.ModeAreaStartedSignalIcon.addClass('active') : area.ModeAreaStartedSignalIcon.removeClass('active');
}

function displayMaterialsInZone_double(area, materialsList, signals) {
  if (signals) {
    setSignals(area, signals);
  }

  if (compareArraysOfObjects(area.materialObjectList, materialsList)) return;

  area.materialsArea.empty();
  area.materialsNumber.text(0);
  area.materials = [];

  materialsList.sort((a, b) => (a.PositionOrder > b.PositionOrder) ? -1 : 1);

  area.materialsNumber.text(materialsList.length);

  if (materialsList.length === 1) {
    width = WIDTH_100;
  } else if (materialsList.length === 2) {
    width = WIDTH_50;
  }

  for (let i = 0; i < materialsList.length; i++) {
    materialsList[i].backgroundColor = GetColorModulo16(materialsList[i].RawMaterialId);
    materialsList[i].textColor = GetTextColorModulo16(materialsList[i].RawMaterialId);
    material = createMaterialElement(width, materialsList[i], area.id, false);
    area.materialsArea.append(material);

    area.materials.push(materialsList[i].RawMaterialId);
    area.materialObjectList.push(materialsList[i]);

    if (materialsList[i].RawMaterialId === materialDetails.currentRawMaterial) {
      if (materialsList[i].MaterialName && !materialDetails.isMaterialAssigned) {
        refreshMaterialDetails(materialDetails.color);
      }
    }
  }
}

function displayMaterialsInZone_multiple(area, materialsList, signals) {

  if (compareArraysOfObjects(area.materialObjectList, materialsList)) return;

  area.materialsArea.empty();
  area.materialsNumber.text(0);
  area.materials = [];

  if (signals) {
    setSignals(area, signals);
  }

  width = WIDTH_100;

  materialsList.sort((a, b) => (a.PositionOrder > b.PositionOrder) ? -1 : 1);
  area.materialsNumber.text(materialsList.length);

  if (materialsList.length <= 5) {
    for (let i = 0; i < materialsList.length; i++) {
      materialsList[i].backgroundColor = GetColorModulo16(materialsList[i].RawMaterialId);
      materialsList[i].textColor = GetTextColorModulo16(materialsList[i].RawMaterialId);
      material = createMaterialElement(width, materialsList[i], area.id, true);
      area.materialsArea.append(material);
      area.materials.push(materialsList[i].RawMaterialId);
      area.materialObjectList.push(materialsList[i]);
    }
    return;
  }

  materialsList = getFirstAndLastMaterials(materialsList);

  for (let i = 0; i < materialsList.length; i++) {
    if (i === 2) {
      area.materialsArea.append(createMoreMaterialsButton());
      area.materialsArea.find('.moreMaterials_button').attr('id', area.id);
    }
    materialsList[i].backgroundColor = GetColorModulo16(materialsList[i].RawMaterialId);
    materialsList[i].textColor = GetTextColorModulo16(materialsList[i].RawMaterialId);
    material = createMaterialElement(WIDTH_100, materialsList[i], area.id, true);
    area.materialsArea.append(material);
    area.materials.push(materialsList[i].RawMaterialId);
    area.materialObjectList.push(materialsList[i]);

    if (materialsList[i].RawMaterialId === materialDetails.currentRawMaterial) {
      if (materialsList[i].MaterialName && !materialDetails.isMaterialAssigned) {
        refreshMaterialDetails(materialDetails.color);
      }
    }
  }
}

const groupBy = key => array =>
  array.reduce((objectsByKeyValue, obj) => {
    const value = obj[key];
    objectsByKeyValue[value] = (objectsByKeyValue[value] || []).concat(obj);
    return objectsByKeyValue;
  }, {});

function displayMaterialsInZone_rake(area, materialsList) {

  if (compareArraysOfObjects(area.materialObjectList, materialsList)) return;

  //area.materialsArea.empty();
  area.materialsNumber.text(0);
  area.materials = [];

  let rakePositions = $('.rake-materials .rakePosition');
  for (let i = 0; i < rakePositions.length; i++) {
    $(rakePositions[i]).find('.rakePosition-material').empty();
  }

  materialsList.sort((a, b) => (a.PositionOrder > b.PositionOrder) ? -1 : 1);
  //width = WIDTH_100;

  let height;
  const groupByPositionOrder = groupBy('PositionOrder');
  let materialsListGrouped = groupByPositionOrder(materialsList);
  let occupiedPositions = Object.keys(materialsListGrouped);

  area.materialsNumber.text(materialsList.length);

  for (let i = 0; i < occupiedPositions.length; i++) {
    for (let j = 0; j < materialsListGrouped[occupiedPositions[i]].length; j++) {
      if (materialsListGrouped[occupiedPositions[i]].length > 1) {
        height = '50%';
      } else {
        height = '100%';
      }
      materialsListGrouped[occupiedPositions[i]][j].backgroundColor = GetColorModulo16(materialsListGrouped[occupiedPositions[i]][j].RawMaterialId);
      materialsListGrouped[occupiedPositions[i]][j].textColor = GetTextColorModulo16(materialsListGrouped[occupiedPositions[i]][j].RawMaterialId);
      material = createMaterialElementForRake(height, materialsListGrouped[occupiedPositions[i]][j], area.id);
      let materialPosition = area.materialsArea.find('div[data-sequence="' + materialsListGrouped[occupiedPositions[i]][j].PositionOrder + '"]');
      materialPosition.append(material);
    }
  }
}

function createMaterialElementForRake(height, material, areaId) {
  let materialText = material.MaterialName || material.RawMaterialId;
  let materialTextEl = '<div class="text-vertical">' + materialText + '</div>'
  let orderSeqEl = "";
  let scrapClass = '';

  let fontSizeClass = '';
  if (height == '50%') {
    fontSizeClass = ' font-sm';
  }

  if (material.IsPartialScrap) {
    scrapClass = 'scrap_material';
    return $("<div class='material nowrap-text cursor-pointer " + scrapClass + fontSizeClass + " 'data-area='" + areaId + "' data-mat-id='" + material.RawMaterialId + "' data-scraped='" + material.IsScrap + "' style='width: 100%; height:" + height + "; background-color:" + material.backgroundColor + "; color:" + material.textColor + "; background-size: calc(100%) calc(" + material.ScrapPercent + "%)'></div>").append(orderSeqEl).append(materialTextEl);
  } else if (material.IsScrap) {
    scrapClass = 'scrap_material';
  }

  return $("<div class='material nowrap-text cursor-pointer " + scrapClass + fontSizeClass + " 'data-area='" + areaId + "' data-mat-id='" + material.RawMaterialId + "' data-scraped='" + material.IsScrap + "' style='width: 100%; height:" + height + "; background-color:" + material.backgroundColor + "; color:" + material.textColor + "'></div>").append(orderSeqEl).append(materialTextEl);
}

function displayLayers(area, layersList) {

  let layer;

  if (compareArraysOfObjects(area.layersObjectList, layersList)) return;

  area.layersArea.empty();
  area.materialsNumber.text(0);
  area.layers = [];

  layersList.sort((a, b) => (a.PositionOrder < b.PositionOrder) ? -1 : 1);

  for (let i = 0; i < layersList.length; i++) {
    layer = createLayerElement(area.id, layersList[i], area.id, true);
    area.layersArea.append(layer);
    area.layers.push(layersList[i].Id);
    area.layersObjectList.push(layersList[i]);
  }
}

function createLayerElement(areaCode, layer, areaId) {

  let layerText = layer.Name || layer.Id;
  let statusAction = '';
  let statusClass = '';

  if (layer.IsEmpty) {
    statusClass = 'status-empty';
  } else if (layer.IsForming) {
    statusClass = 'status-progress';
    statusAction = 'action-finish" onclick="_TrackingActions.FinishLayerAction(' + layer.Id + ',' + areaCode + ') ';
  } else if (layer.IsFormed) {
    statusClass = 'status-ready';
    statusAction = 'action-transfer" onclick="_TrackingActions.TransferLayerAction(' + layer.Id + ',' + areaCode + ') ';
  }

  let layerEl = '<div id="' + layer.Id + '" class="zone_header nowrap-text layer-position layer cursor-pointer data-area="' + areaId + '" data-layer-id="' + layer.Id + '">' +
    //'<div class="' + statusAction + '"></div>' +
    '<div class="materials_number" onclick="GoToLayer(' + layer.Id + ')">' + layer.MaterialsSum + '</div>' +
    '<div class="layer_action ' + statusAction + '"></div>' +
    '<div class="layer_name" onclick="GoToLayer(' + layer.Id + ')">' + layerText + '</div>' +
    '<div class="layer-status ' + statusClass + '" onclick="GoToLayer(' + layer.Id + ')"></div>' +
    '</div>';

  return $(layerEl);
}

function setMaterialScraped(materialId) {
  for (let i = 0; i < areas.length; i++) {
    for (let j = 0; j < areas[i].materialsArea.length; j++) {
      $(areas[i].materialsArea[j]).each(function () {
        if ($(this).data('mat-id') === materialId) {
          $(this).data('scraped', true).addClass("scrap_material");
        }
      });
    }
  }
}

function setMaterialUnscraped(materialId) {
  for (let i = 0; i < areas.length; i++) {
    for (let j = 0; j < areas[i].materialsArea.length; j++) {
      $(areas[i].materialsArea[j]).each(function () {
        if ($(this).data('mat-id') === materialId) {
          $(this).data('scraped', false).removeClass("scrap_material");
        }
      });
    }
  }
}

function onMaterialActionSelect() {
  let url;
  let onSuccessMethod = null;
  $(".material_action").on('click', function () {
    let action = $(this).data('action');
    switch (action) {
      case MatActions.REMOVE:
        url = "/Visualization/RemoveFromTrackingAction";
        break;
      case MatActions.REJECT:
        RejectRawMaterialPopup();
        return;
      case MatActions.SCRAP:
        ScrapPopup();
        return;
      case MatActions.UN_SCRAP:
        onSuccessMethod = () => {
          setScrapIconInMaterialPanel(false); setMaterialUnscraped(materialDetails.currentRawMaterial);
        }
        url = "/Visualization/UnscrapAction";
        break;
      case MatActions.MAT_READY:
        url = "/Visualization/MaterialReadyAction";
        break;
      case MatActions.PARTIAL_SCRAP:
        PartialScrapPopup();
        return;
      case MatActions.ADD_DEFECTS:
        AssignDefectsPopup();
        return;
    }
    sendMaterialActionRequest(url, materialDetails.currentRawMaterial, onSuccessMethod || null);
  });
}

function sendMaterialActionRequest(url, rawMaterialId, onSuccessMethod) {
  if (!CurrentElement.RawMaterialId) {
    InfoMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }
  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], "", () => {

    let parameters = { rawMaterialId: rawMaterialId }
    AjaxReqestHelperSilentWithoutDataType(url, parameters, onSuccessMethod);
  });
}

function redirectToSchedule() {
  window.location.href = '/Schedule';

}

function redirectToProduction() {
  window.location.href = '/Products';
}

function sendTrackingActionRequest(url, onSuccessMethod) {
  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], "", () => {
    AjaxReqestHelperSilentWithoutDataType(url, null, onSuccessMethod);
  });
}

function getFirstAndLastMaterials(materialsList) {
  const lastIndex = materialsList.length - 1;
  return [materialsList[0], materialsList[1], materialsList[lastIndex - 1], materialsList[lastIndex]];
}

function createMaterialElement(width, material, areaId, displayOrder = false) {

  let materialText = material.MaterialName || material.RawMaterialId;
  let orderSeqEl = "";
  let scrapClass = '';

  if (displayOrder) {
    orderSeqEl = "<span class='material-sequence'>" + material.PositionOrder + "</span>";
  }

  if (material.IsPartialScrap) {
    scrapClass = 'scrap_material';
    return $("<div class='material cursor-pointer " + scrapClass + " 'data-area='" + areaId + "' data-mat-id='" + material.RawMaterialId + "' data-scraped='" + material.IsScrap + "' style='width: " + width + "; background-color:" + material.backgroundColor + "; color:" + material.textColor + "; background-size: calc(" + material.ScrapPercent + "%) calc(100%) '></div>").append(orderSeqEl).append(materialText);
  } else if (material.IsScrap) {
    scrapClass = 'scrap_material';
  }

  return $("<div class='material cursor-pointer " + scrapClass + " 'data-area='" + areaId + "' data-mat-id='" + material.RawMaterialId + "' data-scraped='" + material.IsScrap + "' style='width: " + width + "; background-color:" + material.backgroundColor + "; color:" + material.textColor + "'></div>").append(orderSeqEl).append(materialText);
}

function createMoreMaterialsButton() {
  return $("<div class='material moreMaterials_button' onclick='GoToMaterialsInArea(this)'>...</div>")
}


function compareArrays(array1, array2) {
  return array1.length === array2.length && array1.every((value, index) => value === array2[index]);
}

function compareObjects(obj1, obj2) {
  return Object.keys(obj1).length === Object.keys(obj2).length && Object.keys(obj1).every(p => obj1[p] === obj2[p]);
}

function compareArraysOfObjects(a1, a2) {
  return a1.length === a2.length && a1.every((o, idx) => compareObjects(o, a2[idx]));
}

function populateMaterialDetails(data, color = 'transparent') {
  materialDetails.color = color;

  materialDetails.fields.color.css('background-color', color);
  materialDetails.fields.materialName.text(data.MaterialName || rawMatMsg + ': ' + materialDetails.currentRawMaterial);
  materialDetails.fields.workOrderName.text(data.WorkOrderName || unAssignedMsg);
  materialDetails.fields.heatName.text(data.HeatName || unAssignedMsg);
  materialDetails.fields.steelgradeName.text((data.SteelgradeName || unAssignedMsg) + ' (' + (data.SteelgradeCode || unAssignedMsg) + ')');
  materialDetails.isMaterialAssigned = data.MaterialId ? true : false;
  materialDetails.data.materialId = data.MaterialId;
  materialDetails.data.MaterialName = data.MaterialName;
  materialDetails.data.workOrderId = data.WorkOrderId;
  materialDetails.data.heatId = data.HeatId;
  materialDetails.data.steelgradeId = data.SteelgradeId;
}

function clearMaterialDetails() {
  materialDetails.fields.color.css('background-color', 'transparent');
  materialDetails.fields.materialName.text("-");
  materialDetails.fields.workOrderName.text("-");
  materialDetails.fields.heatName.text("-");
  materialDetails.fields.steelgradeName.text("-");
  materialDetails.isMaterialAssigned = false;
  materialDetails.data.materialId = null;
  materialDetails.data.workOrderId = null;
  materialDetails.data.heatId = null;
  materialDetails.data.steelgradeId = null;
}

function populateMeasaurements(data) {
  if (!materialDetails.mesurementsInitialized) {
    initializeMeasaurements(data);
  }

  for (let area = 0; area < data.length; area++) {
    for (let meas = 0; meas < data[area].AreaMeasurements.length; meas++) {
      let newValue = data[area].AreaMeasurements[meas].MeasurementValue;
      newValue = newValue || newValue === 0 ? newValue : "-";

      materialDetails.mesurements[data[area].AreaMeasurements[meas].FeatureCode].text(newValue);
    }
  }
}

function clearMeasurements() {
  $.each(materialDetails.mesurements, function (key, value) {
    value.text("-");
  });
}

function initializeMeasaurements(data) {
  materialDetails.mesurements = {};

  var measurementAreas = $(".measurements_data");
  for (let i = 0; i < data.length; i++) {
    let area = createAreaMeasaurementsElement(data[i])
    measurementAreas.append(area);
  }

  materialDetails.mesurementsInitialized = !!Object.keys(materialDetails.mesurements).length;
}

function createAreaMeasaurementsElement(area) {
  var result =
    $("<div class=\"measurements_zone\">" +
      "  <div class=\"measurements_zone_header\">" + area.AreaCode + "</div>" +
      "  <div class=\"measurements_zone_body\">" +
      "  </div>" +
      "</div>");

  var areaBody = result.find(".measurements_zone_body");

  for (let i = 0; i < area.AreaMeasurements.length; i++) {

    let measurementElement =
      $("    <div class=\"row form-group m-0 measurement_row\">" +
        "      <div class=\"col-8 text-left\">" +
        "        <label class=\"control-label\" style=\"width:100%; white-space:nowrap; overflow:hidden; text-overflow:ellipsis; direction:rtl;\">" + area.AreaMeasurements[i].FeatureName + " [" + area.AreaMeasurements[i].UnitSymbol + "]" + "</label>" +
        "      </div>" +
        "      <div class=\"col-4\">" +
        "        <p class=\"measurements_value\">-</p>" +
        "      </div>" +
        "    </div>");

    materialDetails.mesurements[area.AreaMeasurements[i].FeatureCode] = measurementElement.find(".measurements_value");

    areaBody.append(measurementElement);
  }

  return result;
}

function GoToWorkOrderFromGrid(workOrderId) {

  let dataToSend = {
    WorkOrderId: workOrderId
  };
  openSlideScreen('WorkOrder', 'ElementDetails', dataToSend, null);
}

function GoToHeat() {
  if (!materialDetails.data.heatId) {
    InfoMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  let dataToSend = {
    heatId: materialDetails.data.heatId
  };
  openSlideScreen('Heat', 'ElementDetails', dataToSend, materialDetails.data.MaterialName);
}

function GoToMaterialsInArea(e) {
  _Visualization.ShowMaterialsInArea(e);
}

function GoToLayer(id) {
  let dataToSend = {
    layerId: id
  };
  openSlideScreen('Visualization', 'GetLayerViewById', dataToSend);
}

function GoToMeasurements() {
  if (!materialDetails.currentRawMaterial) {
    InfoMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  let dataToSend = { RawMaterialId: materialDetails.currentRawMaterial };

  openSlideScreen('Measurements', 'GetMeasurementsBody', dataToSend);
}

function GetSelecteRawMatId() {
  if (!materialDetails.currentRawMaterial) {
    InfoMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }
  return { RawMaterialId: materialDetails.currentRawMaterial }
}

function displayLegendWindow() {
  if (legendWindow.css('display') === 'none') {
    legendWindow.show();
  } else {
    legendWindow.hide();
  }
}

function reloadAreasGrids() {
  let areasNames = ['EnterTable', 'Rake']; /* <= test - change to get areas from db*/
  for (let i = 0; i < areasNames.length; i++) {
    let areaGrid = $('#' + areasNames[i]);
    if (areaGrid.length > 0) {
      let grid = areaGrid.data('kendoGrid');
      grid.dataSource.read();
      grid.refresh();
    }
  }
}
