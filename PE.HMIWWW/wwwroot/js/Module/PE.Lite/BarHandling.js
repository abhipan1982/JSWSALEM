const unAssignedMsg = Translations["NAME_Unassigned"];
const rawMatMsg = Translations["NAME_RawMaterial"];

let CurrentElement = {
  RawMaterialId: null
};

const areasTypes = {
  MULTI: 'multi',
  DOUBLE: 'double',
  LAYERS: 'layers'
};

const MatActions = {
  UNASSIGN: 'UNASSIGN',
  REJECT: 'REJECT',
  QUALITY: 'QUALITY',
  MAT_READY: 'MAT_READY',
  SCRAP: 'SCRAP',
  UN_SCRAP: 'UN_SCRAP',
  ADD_DEFECTS: 'ADD_DEFECTS'
};

const ActionsConfig = {
  reject: {
    el: null,
    AvailableForAreas: [TrackingAreaKeys.FCE_AREA],
    isPopupOpen: false
  }
}

var areas = [];
var materialDetails = {};

const WIDTH_100 = '100%';
const WIDTH_50 = '50%';
let material;
let width;

let selectedTrackingMaterialEl;

$(document).ready(function () {
  initZones();
  initActionsEl();
  initMaterialDetails();
  initRefreshHandler();
  onMaterialActionSelect();
  //simulateRefresh();
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
      Grading: $(el).find('.right_status'),
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
    steelgradeCode: $('#steelgradeCode'),
    steelgradeName: $('#steelgradeName'),
    slittingFactor: $('#slittingFactor')
  };

  $("#materialNameDetails").click(function () { _RawMaterial.GoToRawMaterial(CurrentElement.RawMaterialId); return false; });
  materialDetails.fields.workOrderName.click(function () { _WorkOrder.GoToWorkOrder(materialDetails.data.workOrderId); return false; });
  materialDetails.fields.heatName.click(function () { _Heat.GoToHeat(materialDetails.data.heatId); return false; });
  materialDetails.fields.steelgradeName.click(function () { _Steelgrade.GoToSteelgrade(materialDetails.data.steelgradeId); return false; });
  materialDetails.fields.steelgradeCode.click(function () { _Steelgrade.GoToSteelgrade(materialDetails.data.steelgradeId); return false; });
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

function initActionsEl() {
  ActionsConfig.reject.el = $('#reject');
}

function setMatActionDisactive(el) {
  el.addClass('action-disactive');
}

function setMatActionActive(el) {
  el.removeClass('action-disactive');
}

function setActionAvailability(currentMatArea) {
  if (!currentMatArea) return;

  if (!ActionsConfig.reject.AvailableForAreas.some(area => area == currentMatArea)) {
    setMatActionDisactive(ActionsConfig.reject.el);
    if (ActionsConfig.reject.isPopupOpen && $(".rejectPopupForm").length) {
      ClosePopup();
    }
    ActionsConfig.reject.isPopupOpen = false;
  }
  else
    setMatActionActive(ActionsConfig.reject.el);
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
    setActionAvailability(materialDetails.DOMElement.closest('.area').data('area'));
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

function onMaterialSelectOutsideTracking(rawMaterialId, areaId = null, isScrap = null) {
  closeSlideScreen();
  materialDetails.DOMElement = null;
  materialDetails.currentRawMaterial = rawMaterialId;
  materialDetails.area = areaId;
  materialDetails.IsScrap = isScrap;
  setActionAvailability(areaId);
  //setActionsForArea(materialDetails.area);

  CurrentElement = {
    RawMaterialId: rawMaterialId
  };

  materialDetails.color = GetColorModulo16(rawMaterialId);
  refreshMaterialDetails(materialDetails.color);
  refreshMeasaurements();
}

function getNewMatAreaId(materialId, newAreasState) {
  //let isMaterialInNewState = false;
  let newAreaId = null;

  $.each(newAreasState, function (key, value) {
    for (i = 0; i < value.Materials.length; i++) {
      if (materialId == value.Materials[i].RawMaterialId) {
        //isMaterialInNewState = true;
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
    switch (areas[i].type) {
      case areasTypes.MULTI:
        displayMaterialsInZone_multiple(areas[i], newAreaState.Materials || [], newAreaState.Signals || null);
        break;
      case areasTypes.DOUBLE:
        displayMaterialsInZone_double(areas[i], newAreaState.Materials || [], newAreaState.Signals || null);
        break;
      case areasTypes.LAYERS:
        displayLayers(areas[i], newAreaState.Layers || []);
        break;
      default:
        break;
    }
  }

  LastAreaState = newAreasState;

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
    setActionAvailability(areaId);
    return;
  }

  let newMatWithAreaArray = getLastMaterial(newAreasState);
  if (newMatWithAreaArray != null) {
    materialDetails.IsScrap = newMatWithAreaArray[0].IsScrap;
    onMaterialSelectOutsideTracking(newMatWithAreaArray[0].RawMaterialId, newMatWithAreaArray[1]);
  } else {
    clearMaterialDetails();
    clearMeasurements();
    setMatActionActive(ActionsConfig.reject.el);
    materialDetails.currentRawMaterial = null;
    materialDetails.area = null;
    CurrentElement = {
      RawMaterialId: null
    };
  }
}

function setSignals(area, signals) {
  if (!signals) return;

  if (signals.Grading) {
    displayAreaGrading(area, signals.Grading);
  } else {
    area.Grading.empty();
    area.Grading.hide();
  }

  signals.ModeProduction ? area.ModeProductionSignalIcon.addClass('active') : area.ModeProductionSignalIcon.removeClass('active');
  signals.ModeAdjust ? area.ModeAdjustSignalIcon.addClass('active') : area.ModeAdjustSignalIcon.removeClass('active');
  signals.Simulation ? area.ModeSimulationSignalIcon.addClass('active') : area.ModeSimulationSignalIcon.removeClass('active');
  signals.AutomaticRelease ? area.ModeAreaStartedSignalIcon.addClass('active') : area.ModeAreaStartedSignalIcon.removeClass('active');
  //signals.Empty ? area.ModeAreaStartedSignalIcon.addClass('active') : area.ModeAreaStartedSignalIcon.removeClass('active');
  //signals.CobbleDetected ? area.ModeAreaStartedSignalIcon.addClass('active') : area.ModeAreaStartedSignalIcon.removeClass('active');
  //signals.ModeLocal ? area.ModeAreaStartedSignalIcon.addClass('active') : area.ModeAreaStartedSignalIcon.removeClass('active');
  //signals.CobbleDetectionSelected ? area.ModeAreaStartedSignalIcon.addClass('active') : area.ModeAreaStartedSignalIcon.removeClass('active');
}

function displayAreaGrading(area, grading) {
  let icon = '';
  switch (grading) {
    case 1:
    case 2: icon = 'check_circle'; break;
    case 3: icon = 'report_problem'; break;
    case 4:
    case 5: icon = 'cancel'; break;
    default: icon = 'report_off'; break;
  }
  area.Grading.empty();
  area.Grading.append("<div class='area-grading'>"
    + "<div class='area-grading-content'>"
    + "<i class='material-icons area-grading-icon specified-grading-color-" + grading + "'>" + icon + "</i>"
    + "<span class='area-grading-text'>" + grading + "</span>"
    + "</div>"
    + "</div>");

  area.Grading.show();
}

function displayMaterialsInZone_double(area, materialsList, signals) {
  let width;
  if (signals) {
    setSignals(area, signals);
  }

  if (compareArraysOfObjects(area.materialObjectList, materialsList)) return;

  area.materialsArea.empty();
  area.materialsNumber.text(0);
  area.materials = [];
  area.materialObjectList = [];

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

    let textMaxLength = width == WIDTH_50 ? 16 : 32;
    materialsList[i].displayName = getNameToDisplay((materialsList[i].MaterialName || materialsList[i].RawMaterialId), textMaxLength)

    if (materialsList[i].Grading) {
      materialsList[i].GradingPositionCss = width == WIDTH_50 ? 'material-grading-top' : 'material-grading-top right-margin';
    }

    material = createMaterialElement(width, materialsList[i], area.id, false);
    area.materialsArea.append(material);

    area.materials.push(materialsList[i].RawMaterialId);
    area.materialObjectList.push(materialsList[i]);

    if (materialsList[i].RawMaterialId === materialDetails.currentRawMaterial) {
      setScrapIconInMaterialPanel(materialsList[i].IsScrap);
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
  area.materialObjectList = [];

  if (signals) {
    setSignals(area, signals);
  }

  materialsList.sort((a, b) => (a.PositionOrder > b.PositionOrder) ? -1 : 1);

  area.materialsNumber.text(materialsList.length);

  if (materialsList.length <= 5) {
    for (let i = 0; i < materialsList.length; i++) {
      materialsList[i].backgroundColor = GetColorModulo16(materialsList[i].RawMaterialId);
      materialsList[i].textColor = GetTextColorModulo16(materialsList[i].RawMaterialId);

      let textMaxLength = materialsList[i].Grading ? 27 : 29;
      let textWidth = materialsList[i].Grading ? '90%' : '100%';
      materialsList[i].displayName = getNameToDisplay((materialsList[i].MaterialName || materialsList[i].RawMaterialId), textMaxLength, textWidth)

      if (materialsList[i].Grading) {
        materialsList[i].GradingPositionCss = 'material-grading-right';
      }

      material = createMaterialElement(WIDTH_100, materialsList[i], area.id, true);
      area.materialsArea.append(material);
      area.materials.push(materialsList[i].RawMaterialId);
      area.materialObjectList.push(materialsList[i]);

      if (materialsList[i].RawMaterialId === materialDetails.currentRawMaterial) {
        setScrapIconInMaterialPanel(materialsList[i].IsScrap);
        if (materialsList[i].MaterialName && !materialDetails.isMaterialAssigned) {
          refreshMaterialDetails(materialDetails.color);
        }
      }
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

    let textMaxLength = materialsList[i].Grading ? 27 : 29;
    let textWidth = materialsList[i].Grading ? '90%' : '100%';
    materialsList[i].displayName = getNameToDisplay((materialsList[i].MaterialName || materialsList[i].RawMaterialId), textMaxLength, textWidth)

    if (materialsList[i].Grading) {
      materialsList[i].GradingPositionCss = 'material-grading-right';
    }

    material = createMaterialElement(WIDTH_100, materialsList[i], area.id, true);
    area.materialsArea.append(material);
    area.materials.push(materialsList[i].RawMaterialId);
    area.materialObjectList.push(materialsList[i]);

    if (materialsList[i].RawMaterialId === materialDetails.currentRawMaterial) {
      setScrapIconInMaterialPanel(materialsList[i].IsScrap);
      if (materialsList[i].MaterialName && !materialDetails.isMaterialAssigned) {
        refreshMaterialDetails(materialDetails.color);
      }
    }
  }
}

function displayLayers(area, layersList) {

  let layer;

  if (compareArraysOfObjects(area.layersObjectList, layersList)) return;

  area.layersArea.empty();
  area.materialsNumber.text(0);
  area.layers = [];

  layersList.sort((a, b) => (a.PositionOrder < b.PositionOrder) ? -1 : 1);

  for (let i = 0; i < layersList.length; i++) {
    layersList[i].displayName = getNameToDisplay((layersList[i].Name || layersList[i].Id), 26, '92%')
    layer = createLayerElement(layersList[i], area.id, true);
    area.layersArea.append(layer);
    area.layers.push(layersList[i].Id);
    area.layersObjectList.push(layersList[i]);
  }
}

function createLayerElement(layer, areaId) {
  let layerText = layer.displayName || layer.Name || layer.Id;

  let statusClass = '';

  if (layer.IsEmpty) {
    statusClass = 'status-empty';
  } else if (layer.IsForming) {
    statusClass = 'status-progress';
  } else if (layer.IsFormed) {
    statusClass = 'status-ready';
  }

  let layerEl = '<div id="' + layer.Id + '" class="zone_header layer-position nowrap-text layer cursor-pointer" data-area="' + areaId + '" data-layer-id="' + layer.Id + '" onclick="GoToLayer(this)">' +
    '<div class="materials_number">' + layer.MaterialsSum + '</div>' +
    '<div class="layer_name">' + layerText + '</div>' +
    '<div class="layer-status ' + statusClass + '"></div>' +
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
      case MatActions.REJECT:
        RejectTrackingRawMaterialPopup();
        return;
      case MatActions.QUALITY:
        QualityTrackingRawMaterialPopup();
        return;
      case MatActions.SCRAP:
        ScrapPopup();
        return;
      case MatActions.UN_SCRAP:
        onSuccessMethod = () => {
          setScrapIconInMaterialPanel(false); /*setMaterialUnscraped(materialDetails.currentRawMaterial);*/
        }
        url = "/Visualization/UnscrapAction";
        break;
      case MatActions.MAT_READY:
        _RawMaterial.MaterialReadyView(materialDetails.currentRawMaterial);
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

function editDefectPopup(defectId) {
  if (!defectId) return;

  OpenInPopupWindow({
    controller: "InspectionStation", method: "DefectEditPopup", width: 480, data: { id: defectId }, afterClose: refreshDefects
  });
}

function refreshDefects() {
  $("#DefectList").data("kendoGrid").dataSource.read();
  $("#DefectList").data("kendoGrid").refresh();
  reloadMaterialList();
}

function redirectToSchedule() {
  window.location.href = '/Schedule';
  return;
}

function redirectToProduction() {
  window.location.href = '/Products';
  return;
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

function createMaterialElement(width, material, areaId = '', displayOrder = false) {

  let materialText = material.displayName || material.MaterialName || material.RawMaterialId;
  let orderSeqEl = "";
  let gradingEl = "";
  let scrapClass = '';

  if (displayOrder) {
    orderSeqEl = "<span class='material-sequence'>" + material.PositionOrder + "</span>";
  }

  if (material.Grading) {
    let icon = '';
    switch (material.Grading) {
      case 1:
      case 2: icon = 'check_circle'; break;
      case 3: icon = 'report_problem'; break;
      case 4:
      case 5: icon = 'cancel'; break;
      default: icon = 'report_off'; break;
    }

    if (material.GradingPositionCss) {
      gradingEl = "<div class='material-grading " + material.GradingPositionCss + "'>"
        + "<div class='material-grading-arrow'></div>"
        + "<div class='material-grading-content'>"
        + "<i class='material-icons material-grading-icon specified-grading-color-" + material.Grading + "'>" + icon + "</i>"
        + "<span class='material-grading-text'>" + material.Grading + "</span>"
        + "</div>"
        + "</div>"
    }
  }

  if (material.IsPartialScrap) {
    scrapClass = 'scrap_material';
    return $("<div class='material cursor-pointer nowrap-text " + scrapClass + " 'data-area='" + areaId + "' data-mat-id='" + material.RawMaterialId + "' data-scraped='" + material.IsScrap + "' style='width: " + width + "; background-color:" + material.backgroundColor + "; color:" + material.textColor + "; background-size: calc(" + material.ScrapPercent + "%) calc(100%) '></div>").append(orderSeqEl).append(materialText).append(gradingEl);
  } else if (material.IsScrap) {
    scrapClass = 'scrap_material';
    return $("<div class='material cursor-pointer nowrap-text " + scrapClass + " 'data-area='" + areaId + "' data-mat-id='" + material.RawMaterialId + "' data-scraped='" + material.IsScrap + "' style='width: " + width + "; background-color:" + material.backgroundColor + "; color:" + material.textColor + "; background-size: calc(" + material.ScrapPercent + "%) calc(100%) '></div>").append(orderSeqEl).append(materialText).append(gradingEl);
  }

  return $("<div class='material cursor-pointer nowrap-text" + scrapClass + " 'data-area='" + areaId + "' data-mat-id='" + material.RawMaterialId + "' data-scraped='" + material.IsScrap + "' style='width: " + width + "; background-color:" + material.backgroundColor + "; color:" + material.textColor + "'></div>").append(orderSeqEl).append(materialText).append(gradingEl);
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

function getNameToDisplay(name, maxNameLength, width = '100%') {
  return name.length > maxNameLength ? "<div class='longText' title='" + name + "' style='width: " + width + "' data-last-letters='" + name.substring(name.length - 3) + "'>" + name + "</div>" : name;
}

function populateMaterialDetails(data, color = 'transparent') {
  materialDetails.color = color;
  materialDetails.fields.color.css('background-color', color);
  materialDetails.fields.materialName.html((getNameToDisplay((data.MaterialName || rawMatMsg + ': ' + materialDetails.currentRawMaterial), 31)));
  materialDetails.fields.workOrderName.text(data.WorkOrderName || unAssignedMsg);
  materialDetails.fields.heatName.text(data.HeatName || unAssignedMsg);
  materialDetails.fields.steelgradeName.text((data.SteelgradeName || unAssignedMsg));
  materialDetails.fields.steelgradeCode.text((data.SteelgradeCode || unAssignedMsg));
  materialDetails.fields.slittingFactor.text(data.SlittingFactor || unAssignedMsg);
  materialDetails.isMaterialAssigned = data.MaterialId ? true : false;
  materialDetails.data.materialId = data.MaterialId;
  materialDetails.data.MaterialName = data.MaterialName;
  materialDetails.data.workOrderId = data.WorkOrderId;
  materialDetails.data.heatId = data.HeatId;
  materialDetails.data.steelgradeId = data.SteelgradeId;
  materialDetails.data.slittingFactor = data.SlittingFactor;
}

function clearMaterialDetails() {
  materialDetails.fields.color.css('background-color', 'transparent');
  materialDetails.fields.materialName.text("-");
  materialDetails.fields.workOrderName.text("-");
  materialDetails.fields.heatName.text("-");
  materialDetails.fields.steelgradeName.text("-");
  materialDetails.fields.slittingFactor.text("-");
  materialDetails.isMaterialAssigned = false;
  materialDetails.data.materialId = null;
  materialDetails.data.workOrderId = null;
  materialDetails.data.heatId = null;
  materialDetails.data.steelgradeId = null;
  materialDetails.data.slittingFactor = null;
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

function GoToMaterialsInArea(e) {
  _Visualization.ShowMaterialsInArea(e);
}

function GoToLayer(e) {
  let dataToSend = {
    layerId: e.id
  };
  openSlideScreen('Visualization', 'GetLayerViewById', dataToSend);
}

function goToSchedule() {
  openSlideScreen('Schedule', 'GetSchedulePartial');
}

function GoToProductionHistory() {
  //openSlideScreen('Schedule', 'GetSchedulePartial');
}

function GetSelecteRawMatId() {
  if (!materialDetails.currentRawMaterial) { return; }
  return { RawMaterialId: materialDetails.currentRawMaterial }
}

function AssignDefectsPopup() {
  if (!CurrentElement.RawMaterialId) {
    InfoMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }
  OpenInPopupWindow({
    controller: "RawMaterial", method: "AssignDefectsPopup", width: 480, data: { rawMaterialId: CurrentElement.RawMaterialId }
  });
}

function ScrapPopup() {
  if (!CurrentElement.RawMaterialId) {
    InfoMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  _Scrap.ScrapPopup(CurrentElement.RawMaterialId, () => { setScrapIconInMaterialPanel(true) });
}

function RejectTrackingRawMaterialPopup() {
  if (!CurrentElement.RawMaterialId) {
    InfoMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  _Reject.RejectRawMaterialPopup(CurrentElement.RawMaterialId, () => { ActionsConfig.reject.isPopupOpen = false; });
  ActionsConfig.reject.isPopupOpen = true;
}

function QualityTrackingRawMaterialPopup() {
  if (!CurrentElement.RawMaterialId) {
    InfoMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }
  OpenInPopupWindow({
    controller: "Inspection", method: "QualityAddPopup", width: 600, data: { rawMaterialId: CurrentElement.RawMaterialId }, afterClose: refreshQuality
  });
}

function closeShopAction() {
  let action = 'CloseWorkShop';
  sendTrackingRequestWithConfirmation(action);
}

function startSlowProductionAction() {
  let action = 'StartSlowProduction';
  sendTrackingRequestWithConfirmation(action, null, "", () => { setSlowProduction(true) });
}

function stopSlowProductionAction() {
  let action = 'StopSlowProduction';
  sendTrackingRequestWithConfirmation(action, null, "", () => { setSlowProduction(false) });
}

function startLaneCommunicationAction() {
  let action = 'StartLaneCommunication';
  sendTrackingRequestWithConfirmation(action, null, "", () => { setLaneCommunication(false) });
}

function stopLaneCommunicationAction() {
  let action = 'StopLaneCommunication';
  sendTrackingRequestWithConfirmation(action, null, "", () => { setLaneCommunication(true) });
}

function transferLayerAction() {
  let action = 'TransferLayer';
  let url = Url("TrackingManagement", action);
  sendTrackingRequestWithConfirmation(action);
}

function sendTrackingRequestWithConfirmation(action, parameters = null, message = '', onSuccessMethod = null) {
  if (!action) return;
  let url = Url("TrackingManagement", action);

  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], message, () => {
    AjaxReqestHelperSilentWithoutDataType(url, parameters, onSuccessMethod);
  });
}

function deleteDefect(rawMaterialId) {
  if (!rawMaterialId) return;

  let parameters = {
    defectId: rawMaterialId
  }
  let url = 'InspectionStation/DeleteDefect'
  PromptMessage(Translations["MESSAGE_deleteConfirm"], '', () => {
    AjaxReqestHelperSilentWithoutDataType(url, parameters, refreshDefects);
  });
}

function refreshRejects() {
  $("#RejectsList").data("kendoGrid").dataSource.read();
  $("#RejectsList").data("kendoGrid").refresh();
  reloadMaterialList();
}


function EditScrapPopup(rawMaterialId) {
  if (!rawMaterialId) return;

  OpenInPopupWindow({
    controller: "Visualization", method: "EditScrapPopup", width: 400, data: { id: rawMaterialId }, afterClose: refreshScraps
  });
}

function refreshScraps() {
  $("#ScrapList").data("kendoGrid").dataSource.read();
  $("#ScrapList").data("kendoGrid").refresh();
  reloadMaterialList();
}

function reloadMaterialList() {
  let $materialList = $('#MaterialGrading');
  if ($materialList.length) {
    let materialsGrid = $materialList.data('kendoGrid');
    materialsGrid.dataSource.read();
    materialsGrid.refresh();
  }
}

function refreshQuality() {
  let dataToSend = {
    RawMaterialId: CurrentElement.RawMaterialId
  };

  let url = "/InspectionStation/QualityView";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setQualityPartialView);

  refreshSearchGrid();
}

function reloadAreasGrids() {
  let areasNames = ['EnterTable', 'Rake']; /* <= names of areas from db*/
  for (let i = 0; i < areasNames.length; i++) {
    let areaGrid = $('#' + areasNames[i]);
    if (areaGrid.length > 0) {
      let grid = areaGrid.data('kendoGrid');
      grid.dataSource.read();
      grid.refresh();
    }
  }
}
