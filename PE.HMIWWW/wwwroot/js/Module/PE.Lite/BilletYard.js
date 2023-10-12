let CurrentElement = {};

const columns = ["SteelgradeCode"];
const button_array = $('.arrow-categories');
const YardsContainerEl = $('#Yards');

const _DirectionDict = {
  Left2Right4Up2Down: 'Left2Right4Up2Down',
  Left2Right4Down2Up: 'Left2Right4Down2Up',
  Right2Left4Up2Down: 'Right2Left4Up2Down',
  Right2Left4Down2Up: 'Right2Left4Down2Up'
}



const Search = new class {
  constructor() {
    this.isSearchMode = false;
    this.yardId = null;
    this.heatId = null;
  }

  clear() {
    this.isSearchMode = false;
    this.yardId = null;
    this.heatId = null;
  }
  setData(yardId, heatId) {
    this.isSearchMode = true;
    this.yardId = yardId;
    this.heatId = heatId;
  }
  getData() {
    return {
      yardId: this.yardId,
      heatId: this.heatId,
    }
  }
}


const _TransferModeDict = {
  Undefined: 0,
  RecToLoc: 1,
  LocToLoc: 2,
  LocToCh: 3,
  ChToLoc: 4,
  Scrap: 5,
  Unscrap: 6,
}

const MaterialTransfer = new class {

  constructor() {
    this.Mode = _TransferModeDict.Undefined;
    this.HeatId = null;
    this.LocationId = null;
    this.HeaMatNumber = null;
    this.MaterialsNumber = null;
    this.FreeSpacesNumber = null;
    this.SourceLocationId = null;
    this.SourceYardId = null;
    this.WorkOrderId = null;
    this.WorkOrderChargedNumber = null;
    this.RequiredMaterialsNr = null;
  }

  clear() {
    this.HeatId = null;
    this.LocationId = null;
    this.HeaMatNumber = null;
    this.WorkOrderId = null;
    this.FreeSpacesNumber = null;
    this.SourceLocationId = null;
    this.SourceYardId = null;
    this.MaterialsNumber = null;
    this.WorkOrderChargedNumber = null;
    this.RequiredMaterialsNr = null;
  };
  clearMode() {
    this.Mode = _TransferModeDict.Undefined;
  }
  setTransferMode(transferMode) {
    this.Mode = transferMode;
  }
  setHeatData(heatId, heatMatNr, yardId, locationId) {
    this.HeatId = heatId;
    this.HeaMatNumber = heatMatNr;
    this.SourceLocationId = locationId;
    this.SourceYardId = yardId;
  };
  setLocationData(locationId, freeSpacesNr) {
    this.LocationId = locationId;
    this.FreeSpacesNumber = freeSpacesNr;
  };
  setWorkOrderData(workOrderId, requiredMaterials, workOrderChargedNumber) {
    this.WorkOrderId = workOrderId;
    this.RequiredMaterialsNr = requiredMaterials;
    this.WorkOrderChargedNumber = workOrderChargedNumber
  };
  isSourceEqualToDestination() {
    return this.LocationId == this.SourceLocationId ? true : false;
  };
  getHeatToLocationData() {
    return {
      HeatId: this.HeatId,
      SourceLocationId: this.SourceLocationId,
      SourceYardId: this.SourceYardId,
      LocationId: this.LocationId,
      MaterialsNumber: this.MaterialsNumber
    }
  };
  getHeatToChargingData() {
    return {
      HeatId: this.HeatId,
      SourceLocationId: this.SourceLocationId,
      WorkOrderId: this.WorkOrderId,
      MaterialsNumber: this.MaterialsNumber
    }
  };
  getHeatBackFromChargingData() {
    return {
      HeatId: this.HeatId,
      TargetLocationId: this.LocationId,
      WorkOrderId: this.WorkOrderId,
      MaterialsNumber: this.MaterialsNumber
    }
  };
  setMaterialsNumber(materialNumber) {
    this.MaterialsNumber = materialNumber;
  }
  getMaxMaterialsNumberLoc() {
    return {
      MaterialsNumber: this.HeaMatNumber > this.FreeSpacesNumber ? this.FreeSpacesNumber : this.HeaMatNumber
    }
  }
  getMaxMaterialsNumberSch() {
    return {
      MaterialsNumber: this.HeaMatNumber > this.RequiredMaterialsNr ? this.RequiredMaterialsNr : this.HeaMatNumber
    }
  }
};


$(document).ready(() => {

})

function searchInLocations(currentYardId) {
  if (!Search.isSearchMode) return;
  if (!Search.yardId || Search.yardId != currentYardId) return;

  const locationsIds = getLocationsBySearchData(Search.getData());

  if (locationsIds) {
    highlightLocations(locationsIds);
  }
}

function highlightLocations(locationsIds) {
  const locationsEl = $('.location');

  if (locationsEl.length > 0) {
    for (let i = 0; i < locationsEl.length; i++) {
      if (locationsIds.includes($(locationsEl[i]).data('location'))) {
        $(locationsEl[i]).addClass('location-active');
      }
    }
  }
}

function getLocationsBySearchData(parameters) {
  if (!parameters) {
    console.error('Missing data: search parameters');
    return;
  }
  let data = AjaxGetDataHelper(Url("BilletYard", "GetSearchedLocationsIds"), parameters);
  if (!data) {
    return null;
  }
  return data.LocationIds;
}

function GoToYard(id, isReception, isYard, isChargingGrid, isScrapped) {
    if (isReception) {
        GoToReception();
    } else if (isYard) {
        GoToYardMap(id);
    } else if (isChargingGrid) {
        GoToChargingGrid();
    } else if (isScrapped) {
        GoToScrapped();
    }
}

function GoToReception() {
  const url = "/BilletYard/GetRecepcionView";
  AjaxReqestHelperSilentWithoutDataType(url, null, setElementDetailsPartialView);
}

function GoToChargingGrid() {
  const url = "/BilletYard/GetChargingGridView";
  AjaxReqestHelperSilentWithoutDataType(url, null, setElementDetailsPartialView);
}

function GoToScrapped() {
  const url = "/BilletYard/GetScrappedView";
  AjaxReqestHelperSilentWithoutDataType(url, null, setElementDetailsPartialView);
}

function GoToYardMap(id) {
  if (!id) {
    console.error('Missing data: yardId');
    return;
  }
  const dataToSend = { id: id };
  YardsContainerEl.addClass('loading-overlay');
  const url = "/BilletYard/GetYardMapView";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementYardMapView);
}

function GoToYardsList() {
  YardsContainerEl.addClass('loading-overlay');
  const url = "/BilletYard/GetYardsView";
  AjaxReqestHelperSilentWithoutDataType(url, null, setElementDetailsPartialView);
}

function GoToStack(id) {
  if (!id) {
    console.error('Missing data: locationId');
    return;
  }
  const dataToSend = { id: id };
  openSlideScreen('BilletYard', 'GetStackContenPartialtView', dataToSend, Translations["NAME_Location"] );
}

function setElementYardMapView(partialView) {
  YardsContainerEl.html(partialView);
}

function OpenRelocationPopup() {
  openSlideScreen('BilletYard', 'GetRelocationPartialView', null, Translations["NAME_Relocation"]);
}

function OpenRegisterInYardPopup() {
  openSlideScreen('BilletYard', 'GetRegisterInYardPartialView', null, Translations["NAME_RegisterInYard"]);
}

function OpenBackToYardPopup() {
  openSlideScreen('BilletYard', 'GetBackToYardPartialView', null, Translations["BUTTON_BackToYard"]);
}

function OpenSchedulePopup() {
  openSlideScreen('BilletYard', 'GetSchedulePartialView', null, Translations["NAME_LoadOnChargingGrid"]);
}

function EditHeatPopup(id) {
  if (!id) {
    InfoMessage('Select item from the stack');
    return;
  }
  OpenInPopupWindow({
    controller: "Heat", method: "HeatEditPopup", width: 1250, data: { id: id }, afterClose: reload
  });
}

function onSelectRecepcionSearchGridEl(e) {
  let grid = e.sender;
  let selectedRow = grid.select();
  let selectedItem = grid.dataItem(selectedRow);

  MaterialTransfer.clear();
  MaterialTransfer.setHeatData(selectedItem.HeatId, selectedItem.NumberOfMaterials, selectedItem.YardId, null);

  ColorLocationList(selectedItem.NumberOfMaterials, 0);
}

function SelectScheduleFirstWO() {
  let grid = $("#ScheduleSearchGrid").data("kendoGrid");
  if (!grid) return;

  let gridData = grid.dataSource.view();
  let firstRow = grid.table.find("tbody tr:first");
  let firstRowData = gridData[0] || {};
  $(firstRow).addClass('k-state-selected');
  MaterialTransfer.setWorkOrderData(firstRowData.WorkOrderId, firstRowData.RequiredMaterialsNumber);
  MaterialTransfer.setHeatData(firstRowData.HeatId, firstRowData.RequiredMaterialsNumber, firstRowData.YardId, null);
  ColorLocationListToCharge(firstRowData.HeatId, firstRowData.RequiredMaterialsNr)
}

//function SelectChargingGridFirstWO() {
//  let grid = $("#ChargingSearchGrid").data("kendoGrid");
//  if (!grid) return;

//  let gridData = grid.dataSource.view();
//  let firstRow = grid.table.find("tbody tr:first");
//  let firstRowData = gridData[0];
//  $(firstRow).addClass('k-state-selected');

//  MaterialTransfer.setWorkOrderData(firstRowData.WorkOrderId, firstRowData.OnChargingMaterialsNumber);
//  MaterialTransfer.setHeatData(firstRowData.HeatId, firstRowData.OnChargingMaterialsNumber, firstRowData.YardId, null);
//}




function ColorLocationListToCharge(heatId, matNumber) {
  const grid = $("#HeatsInLocationsList").data("kendoGrid");
  const gridGroups = grid.dataSource.view();

  for (let j = 0; j < gridGroups.length; j++) {
    for (let k = 0; k < gridGroups[j].items.length; k++) {
      let groupData = gridGroups[j].items[k];
      for (let i = 0; i < groupData.items.length; i++) {
        let currentUid = groupData.items[i].uid;
        let currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");

        if (groupData.items[i].HeatId != MaterialTransfer.HeatId) {
          $(currenRow).addClass('unavailable cursor-blocked').removeClass('recommended available warning');

        } else if (groupData.items[i].IsFirstInQueue && groupData.items[i].NumberOfMaterials >= MaterialTransfer.RequiredMaterialsNr) {
          $(currenRow).addClass('recommended').removeClass('unavailable cursor-blocked available warning');
        } else if (groupData.items[i].IsFirstInQueue) {
          $(currenRow).addClass('available').removeClass('recommended unavailable cursor-blocked warning');
        } else {
          $(currenRow).addClass('warning cursor-blocked').removeClass('recommended available unavailable');
        }
      }
    }
  }
}

function onSelectHeatInLocationGridEl(e) {
  let grid = e.sender;
  let selectedRow = grid.select();
  let selectedItem = grid.dataItem(selectedRow);

  MaterialTransfer.setHeatData(selectedItem.HeatId, selectedItem.NumberOfMaterials, selectedItem.YardId, selectedItem.AssetId);

  if (!selectedItem.IsFirstInQueue) {
    MaterialTransfer.clear();
    ClearLocationList();
    return;
  }
  ColorLocationList(selectedItem.NumberOfMaterials, selectedItem.AssetId);
}


function onSelectChargingGridtWO(e) {
  let grid = e.sender;
  let selectedRow = grid.select();
  let selectedItem = grid.dataItem(selectedRow);

  MaterialTransfer.setWorkOrderData(selectedItem.WorkOrderId, selectedItem.OnChargingMaterialsNumber);
  MaterialTransfer.setHeatData(selectedItem.HeatId, selectedItem.OnChargingMaterialsNumber, selectedItem.YardId, null);
  ColorLocationList(selectedItem.OnChargingMaterialsNumber, null);
}


function OnDestinateLocationSelect(locationId, freeSpaces) {
  if (!MaterialTransfer.HeatId) {
    InfoMessage(Translations["INFO_SelectHeatToTransfer"]);
    return;
  }

  MaterialTransfer.setLocationData(locationId, freeSpaces);

  if (MaterialTransfer.isSourceEqualToDestination() || !freeSpaces) return;

  GetNrOfMatPopup(MaterialTransfer.getMaxMaterialsNumberLoc());
}

function onSelectHeatToCharging(locId, heatId, heatMatNr) {
  MaterialTransfer.setHeatData(heatId, heatMatNr, null, locId);
  GetNrOfMatPopup(MaterialTransfer.getMaxMaterialsNumberSch());
}

function GetNrOfMatPopup(matNr) {
  OpenInPopupWindow({
    controller: "BilletYard", method: "GetNumberOfMaterialsPopup", width: 500, data: matNr, handleResponse: HandleMaterialsNumber
  });
}


function HandleMaterialsNumber(materialsNumber) {
  if (!materialsNumber) {
    console.error('Missing data: materials number');
    return;
  }
  MaterialTransfer.setMaterialsNumber(materialsNumber);

  switch (MaterialTransfer.Mode) {
    case _TransferModeDict.RecToLoc:
      TransferHeatIntoLocationRequest();
      break;
    case _TransferModeDict.LocToLoc:
      TransferHeatIntoLocationRequest();
      break;
    case _TransferModeDict.LocToCh:
      TransferHeatToChargingRequest();
      break;
    case _TransferModeDict.ChToLoc:
      TransferHeatBackFromChargingRequest();
      break;
    case _TransferModeDict.Scrap:
      TransferHeatToScrappedRequest();
      break;
    case _TransferModeDict.Unscrap:
      UnscrapMaterialsToRecepcionRequest();
      break;
    default: break;
  }
}

function TransferHeatIntoLocationRequest() {
  const url = "/BilletYard/TransferHeatIntoLocation";
  AjaxReqestHelper(url, MaterialTransfer.getHeatToLocationData(), reload, handleError);
}

function TransferHeatToChargingRequest() {
  const url = "/BilletYard/TransferHeatIntoChargingGrid";
  AjaxReqestHelper(url, MaterialTransfer.getHeatToChargingData(), reload, handleError);
}

function TransferHeatBackFromChargingRequest() {
  const url = "/BilletYard/TransferHeatFromChargingGrid";
  AjaxReqestHelper(url, MaterialTransfer.getHeatBackFromChargingData(), reload, handleError);
}

function TransferHeatToScrappedRequest() {
  const url = "/BilletYard/ScrapMaterials";
  AjaxReqestHelper(url, MaterialTransfer.getHeatToLocationData(), reload, handleError);
}

function UnscrapMaterialsToRecepcionRequest() {
  const url = "/BilletYard/UnscrapMaterials";
  AjaxReqestHelper(url, MaterialTransfer.getHeatToLocationData(), reload, handleError);
}

function reload() {
  MaterialTransfer.clear();
  reloadLocationsGrid();
  reloadHeatSearchGrid();
  reloadHeatsInLocationsGrid();
  reloadSchedulesGrid();
  reloadChargingSearchGrid();
  reloadRecepcionGrid();
  reloadChargingGrid();
  reloadScrapped();
  reloadStack();
}

function handleError(e) {
  ErrorMessage(e.responseJSON.Data.Errors);
}

function ScrapMaterials(heatId, heatMatNr, yardId, locationId, isHeatFirstInQueue = null) {
  if (isHeatFirstInQueue != null && !isHeatFirstInQueue) {
    InfoMessage('Operation unavailable');
    return;
  }
  if (!heatId) {
    InfoMessage('Select item from the stack');
    return;
  }
  MaterialTransfer.setTransferMode(_TransferModeDict.Scrap);
  MaterialTransfer.setHeatData(heatId, heatMatNr, yardId, locationId);
  GetNrOfMatPopup({ MaterialsNumber: heatMatNr });
}

function UnscrapMaterials(heatId, heatMatNr, yardId) {
  MaterialTransfer.setTransferMode(_TransferModeDict.Unscrap);
  MaterialTransfer.setHeatData(heatId, heatMatNr, yardId, null);
  GetNrOfMatPopup({ MaterialsNumber: heatMatNr });
}

let $stackElSelected;

function OnStackMaterialSelect($el, matId, heatId, groupId, locationId) {
  if ($stackElSelected) {
    $stackElSelected.removeClass('stackEl-selected');
  }
  $stackElSelected = $el;
  $el.addClass('stackEl-selected');
  reloadMaterialPanel(matId);
  reloadHeatPanel(heatId, groupId, locationId);
}

function reloadMaterialPanel(id) {
  if (!id) {
    console.error('Missing data: materialId');
    return;
  }
  const dataToSend = { id: id };
  const url = "/BilletYard/GetMaterialPanelPartialView";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setMaterialPanelPartialView);
}

function reloadHeatPanel(heatId, groupId, locationId) {
  if (!id) {
    console.error('Missing data: heatId');
    return;
  }
  const dataToSend = {
    HeatId: heatId,
    GroupId: groupId,
    LocationId: locationId,
  };
  const url = "/BilletYard/GetHeatPanelPartialView";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setHeatPanelPartialView);
}

function setMaterialPanelPartialView(partialView) {
  $('#materialPanel').removeClass('loading-overlay').html(partialView);
}

function setHeatPanelPartialView(partialView) {
  $('#heatPanel').removeClass('loading-overlay').html(partialView);
}

function OpenMaterialDetailsPopup(id) {
  if (!id) return;
  let dataToSend = {
    MaterialId: id
  };
  openSlideScreen('Material', 'ElementDetails', dataToSend, Translations["NAME_MaterialDetails"]);
}

function OpenHeatDetailsPopup(id) {
  if (!id) return;
  let dataToSend = {
    heatId: id
  };
  openSlideScreen('Heat', 'ElementDetails', dataToSend, Translations["NAME_HeatDetails"]);
}

function OnSearchGridElSelect(e) {
  const grid = e.sender;
  const selectedRow = grid.select();
  const selectedItem = grid.dataItem(selectedRow);

  if (!selectedItem) return;

  Search.setData(selectedItem.YardId, selectedItem.HeatId);
  if (selectedItem.YardId == 11000) { //to refactor
    GoToReception();
  } else if (selectedItem.YardId == 19000) {
    GoToChargingGrid();
  } else if (selectedItem.YardId == 12000) {
    GoToScrapped();
  } else {
    GoToYardMap(selectedItem.YardId);
  }
}

function OnScheduleElSelect(e) {
  const grid = e.sender;
  const selectedRow = grid.select();
  const selectedItem = grid.dataItem(selectedRow);
}

function ClearSearch() {

  ClearSearchedHeatRecepcion();
  ClearSearchedHeatCharging();
  ClearSearchedHeatScrapped();

  Search.clear();
  const locationsEl = $('.location');

  if (locationsEl.length > 0) {
    for (let i = 0; i < locationsEl.length; i++) {
      $(locationsEl[i]).removeClass('location-active');
    }
  }
  $("#SearchGrid").data("kendoGrid").clearSelection();
}

function GetHeatsOfMaterials(materials) {
  let heats = materials.map(item => ({ HeatId: item.HeatId, HeatName: item.HeatName }));
  return Array.from(new Set(heats.map(item => item.HeatId))).map(HeatId => { return heats.find(x => x.HeatId === HeatId) })
}

function CreateHeatsLegend(heats) {
  const legendContainerEl = $('#heatsLegend');
  for (let i = 0; i < heats.length; i++) {
    legendContainerEl.append(GetHeatLegendEl(heats[i]));
  }
}

function GetHeatLegendEl(heat) {
  return '<div class="mt-2"><div class="heats-legend-square mr-1" style="background-color: ' + GetColorModulo16(heat.HeatId) + '; "></div>' + heat.HeatName + '</div>'
}

function CreateStack(locationId, sizeX, sizeY, fillDirection) {
  const locationWithMaterials = GetMaterialsInLocation(locationId);
  DrawStack(sizeX, sizeY);
  if (locationWithMaterials) {
    FillStack(locationWithMaterials.Materials, sizeX, sizeY, fillDirection);
    CreateHeatsLegend(GetHeatsOfMaterials(locationWithMaterials.Materials));
  }
}

function DrawStack(sizeX, sizeY) {
  let stackContainerEl = $('#stackContainer');
  for (let i = 0; i < sizeY; i++) {
    let stackLayerEl = $(GetNewStackLayerEl());
    for (let j = 0; j < sizeX; j++) {
      let stackSpaceEl = GetNewStackSpaceEl();
      stackLayerEl.append(stackSpaceEl);
    }
    stackContainerEl.append(stackLayerEl);
  }
  let width = sizeX * 60 + 40;
  stackContainerEl.css('width', width);
}

function GetNewStackSpaceEl() {
  return '<div class="stack-space"></div>'
}

function GetNewStackLayerEl() {
  return '<div class="stack-layer"></div>'
}

function FillStack(materials, sizeX, sizeY, fillDirection) {
  switch (fillDirection) {
    case _DirectionDict.Left2Right4Up2Down:
      FillLeft2Right4Up2Down(materials, sizeX, sizeY);
      break;
    case _DirectionDict.Left2Right4Down2Up:
      FillLeft2Right4Down2Up(materials, sizeX, sizeY);
      break;
    case _DirectionDict.Right2Left4Up2Down:
      FillRight2Left4Up2Down(materials, sizeX, sizeY);
      break;
    case _DirectionDict.Right2Left4Down2Up:
      FillRight2Left4Down2Up(materials, sizeX, sizeY);
      break;
    default:
      break;
  }
}

function FillLeft2Right4Down2Up(materials, sizeX, sizeY) {
  const layersEl = $('.stack-layer');
  if (!layersEl.length) return;

  for (let i = sizeY - 1; i >= 0; i--) {
    let layerPlaces = $(layersEl[i]).children();
    let positionY = sizeY - i;
    for (let j = 0; j < sizeX; j++) {
      let positionX = j + 1;
      let material = getMatOnPosition(materials, positionX, positionY);
      if (material) {
        $(layerPlaces[j]).append(GetNewMatInStackEl(material));
      }
    }
  }
}

function FillRight2Left4Down2Up(materials, sizeX, sizeY) {
  const layersEl = $('.stack-layer');
  if (!layersEl.length) return;

  for (let i = sizeY - 1; i >= 0; i--) {
    let layerPlaces = $(layersEl[i]).children();
    let positionY = sizeY - i;
    for (let j = sizeX - 1; j >= 0; j--) {
      let positionX = sizeX - j;
      let material = getMatOnPosition(materials, positionX, positionY);
      if (material) {
        $(layerPlaces[j]).append(GetNewMatInStackEl(material));
      }
    }
  }
}

function FillRight2Left4Up2Down(materials, sizeX, sizeY) { }

function FillLeft2Right4Up2Down(materials, sizeX, sizeY) { }

function GetNewMatInStackEl(material) {
  return '<div class="w-100 h-100 cursor-pointer" style="background-color:' + GetColorModulo16(material.HeatId) + ';" onclick="OnStackMaterialSelect($(this), ' + material.MaterialId + ', ' + material.HeatId + ', ' + material.GroupNo + ', ' + material.LocationId + ')"></div>'
}

function getMatOnPosition(materials, positionX, positionY) {
  return materials.find(m => m.PositionX == positionX && m.PositionY == positionY)
}


function GetMaterialsInLocation(locationId) {
  if (!locationId) return null;
  
  let dataToSend = {
    locationId: locationId
  };
  let data = AjaxGetDataHelper(Url("BilletYard", "GetMaterialsInLocation"), dataToSend);
  if (!data) return null;

  return data;
}

//--CreateMaterialsPopup--

function OpenAddMaterialsPopup() {
  OpenInPopupWindow({
    controller: "BilletYard", method: "GetAddMaterialsPopup", width: 520, data: null, afterClose: reloadRecepcionGrid
  });
}

function filterSteelgrades() {
  return {
    heatId: $("#FKHeatId").val()
  };
}

function HeatSelected(e) {
  var dataItem = this.dataItem(e.item.index());
  $("#FKHeatId").val(dataItem.HeatId);
}

function onAdditionalData() {
  return {
    text: $("#Heat").val()
  };
}

function displayMessage() {
  var validator = $("#form").kendoValidator().data("kendoValidator");

  if (validator.validate()) {
    $('#error').css("display", "none");
  } else {
    $('#error').css("display", "block");
    $('#popup-footer')
      .css('display', 'block')
      .css('background-color', 'rgb(206, 0, 55)');
  }
}

function CalculateMaterialsWeight() {}

//-- End AddHeatPopup--

function CreateHeat() {
  OpenInPopupWindow({
    controller: "Heat", method: "HeatWithMaterialsCreatePopup", width: 900, afterClose: reload
  });
}

function ColorSearchedHeatRecepcion() {
  if (!Search.isSearchMode) return;
  if (!Search.yardId || Search.yardId != 3017) return;

  const grid = $("#RecepcionHeatsGrid").data("kendoGrid");
  if (!grid) return;
  let gridData = grid.dataSource.view();

  for (let i = 0; i < gridData.length; i++) {
    let currentUid = gridData[i].uid;
    let currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");

    if (gridData[i].HeatId == Search.heatId) {
      $(currenRow).addClass('available');
    }
  }
}

function ClearSearchedHeatRecepcion() {
  if (!Search.yardId || Search.yardId != 3017) return;

  const grid = $("#RecepcionHeatsGrid").data("kendoGrid");
  if (!grid) return;
  let gridData = grid.dataSource.view();

  for (let i = 0; i < gridData.length; i++) {
    let currentUid = gridData[i].uid;
    let currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");

    if (gridData[i].HeatId == Search.heatId) {
      $(currenRow).removeClass('available');
    }
  }
}


function ColorSearchedHeatCharging() {
  if (!Search.isSearchMode) return;
  if (!Search.yardId || Search.yardId != 1100) return;

  const grid = $("#ChargingHeatsGrid").data("kendoGrid");
  if (!grid) return;
  let gridData = grid.dataSource.view();

  for (let i = 0; i < gridData.length; i++) {
    let currentUid = gridData[i].uid;
    let currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");

    if (gridData[i].HeatId == Search.heatId) {
      $(currenRow).addClass('available');
    }
  }
}

function ClearSearchedHeatCharging() {
  if (!Search.yardId || Search.yardId != 1100) return;

  const grid = $("#ChargingHeatsGrid").data("kendoGrid");
  if (!grid) return;
  let gridData = grid.dataSource.view();

  for (let i = 0; i < gridData.length; i++) {
    let currentUid = gridData[i].uid;
    let currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");

    if (gridData[i].HeatId == Search.heatId) {
      $(currenRow).removeClass('available');
    }
  }
}


function ColorSearchedHeatScrapped() {
  if (!Search.isSearchMode) return;
  if (!Search.yardId || Search.yardId != 3020) return;

  const grid = $("#ScrappedGrid").data("kendoGrid");
  if (!grid) return;
  let gridData = grid.dataSource.view();

  for (let i = 0; i < gridData.length; i++) {
    let currentUid = gridData[i].uid;
    let currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");

    if (gridData[i].HeatId == Search.heatId) {
      $(currenRow).addClass('available');
    }
  }
}

function ClearSearchedHeatScrapped() {
  if (!Search.yardId || Search.yardId != 3020) return;

  const grid = $("#ScrappedGrid").data("kendoGrid");
  if (!grid) return;
  let gridData = grid.dataSource.view();

  for (let i = 0; i < gridData.length; i++) {
    let currentUid = gridData[i].uid;
    let currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");

    if (gridData[i].HeatId == Search.heatId) {
      $(currenRow).removeClass('available');
    }
  }
}


function ColorLocationList(heatMaterialsNr, heatSourceLocation) {
  const grid = $("#LocationList").data("kendoGrid");
  const gridGroups = grid.dataSource.view();
  for (let j = 0; j < gridGroups.length; j++) {
    let groupData = gridGroups[j];
    for (var i = 0; i < groupData.items.length; i++) {
      let currentUid = groupData.items[i].uid;
      let currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");
      $currenRow = $(currenRow);

      if (groupData.items[i].AssetId == heatSourceLocation || groupData.items[i].FreeSpace == 0) {
        $currenRow.addClass('unavailable');
        $currenRow.removeClass('available recommended');
      } else if (groupData.items[i].FreeSpace >= heatMaterialsNr && groupData.items[i].HeatIdInLastGroup == MaterialTransfer.HeatId) {
        $currenRow.addClass('recommended');
        $currenRow.removeClass('available unavailable');
      } else if (groupData.items[i].FreeSpace >= heatMaterialsNr) {
        $currenRow.addClass('available');
        $currenRow.removeClass('unavailable recommended');
      } else {
        $currenRow.removeClass('available unavailable recommended');
      }
    }
  }
}

function ColorHeatListByQueueFlag() {
  const grid = $("#HeatsInLocationsList").data("kendoGrid");
  const gridGroups = grid.dataSource.view();

  for (let j = 0; j < gridGroups.length; j++) {
    for (let k = 0; k < gridGroups[j].items.length; k++) {
      let groupData = gridGroups[j].items[k];
      for (var i = 0; i < groupData.items.length; i++) {
        let currentUid = groupData.items[i].uid;
        let currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");

        if (!groupData.items[i].IsFirstInQueue) {
          $(currenRow).addClass('unavailable cursor-blocked');
        } else {
          $(currenRow).removeClass('unavailable cursor-blocked');
        }
      }
    }
  }
}

function ClearLocationList() {
  const grid = $("#LocationList").data("kendoGrid");
  const gridGroups = grid.dataSource.view();
  for (let j = 0; j < gridGroups.length; j++) {
    let groupData = gridGroups[j];
    for (var i = 0; i < groupData.items.length; i++) {
      let currentUid = groupData.items[i].uid;
      let currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");
      $(currenRow).removeClass('available unavailable recommended');
    }
  }
}

function reloadLocationsGrid() {
  let grid = $('#LocationList').data('kendoGrid');
  if (grid) {
    grid.dataSource.read();
    grid.refresh();
  }
}

function reloadHeatSearchGrid() {
  let grid = $('#RecepcionSearchGrid').data('kendoGrid');
  if (grid) {
    grid.dataSource.read();
    grid.refresh();
  }
}

function reloadHeatsInLocationsGrid() {
  let grid = $('#HeatsInLocationsList').data('kendoGrid');
  if (grid) {
    grid.dataSource.read();
    grid.refresh();
  }
}

function reloadSchedulesGrid() {
  let grid = $('#ScheduleSearchGrid').data('kendoGrid');
  if (grid) {
    grid.dataSource.read();
    grid.refresh();
  }
}

function reloadChargingSearchGrid() {
  let grid = $('#ChargingSearchGrid').data('kendoGrid');
  if (grid) {
    grid.dataSource.read();
    grid.refresh();
  }
}

function reloadRecepcionGrid() {
  let grid = $('#RecepcionHeatsGrid').data('kendoGrid');
  if (grid) {
    grid.dataSource.read();
    grid.refresh();
  }
}

function reloadChargingGrid() {
  let grid = $('#ChargingHeatsGrid').data('kendoGrid');
  if (grid) {
    grid.dataSource.read();
    grid.refresh();
  }
}

function reloadScrapped() {
  let grid = $('#ScrappedGrid').data('kendoGrid');
  if (grid) {
    grid.dataSource.read();
    grid.refresh();
  }
}

function reloadStack() {
  const locationContentEl = $('#locationContent');
  if (!locationContentEl) return;
  locationContentEl.addClass('loading-overlay');
  reloadLocationContent();
}

function setLocationPartialView(partialView) {
  const locationContentEl = $('#locationContent');
  locationContentEl.removeClass('loading-overlay');
  locationContentEl.replaceWith(partialView);
}


function GoToYardsList() {
  YardsContainerEl.addClass('loading-overlay');
  const url = "/BilletYard/GetYardsView";
  AjaxReqestHelperSilentWithoutDataType(url, null, setElementDetailsPartialView);
}

function GoToStack(id) {
  if (!id) {
    console.error('Missing data: locationId');
    return;
  }
  const dataToSend = { id: id };
  openSlideScreen('BilletYard', 'GetStackContenPartialtView', dataToSend, Translations["NAME_Location"]);
}

function setElementDetailsPartialView(partialView) {
  YardsContainerEl.removeClass('loading-overlay');
  YardsContainerEl.html(partialView);
}

function displayLegend($legendWindowEl) {
  if ($legendWindowEl.css('display') === 'none') {
    $legendWindowEl.show();
  } else {
    $legendWindowEl.hide();
  }
}

function GoToMaterial(materialId) {
  let dataToSend = {
    MaterialId: materialId
  };
  openSlideScreen('Material', 'ElementDetails', dataToSend, Translations["NAME_MaterialDetails"]);
}

function GoToWorkOrder(workOrderId) {
  let dataToSend = {
    WorkOrderId: workOrderId
  };
  openSlideScreen('WorkOrder', 'ElementDetails', dataToSend, Translations["NAME_WorkOrderDetails"]);
}
