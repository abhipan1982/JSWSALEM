RegisterMethod(HmiRefreshKeys.RawMaterialDetails, RefreshData);

let CurrentElement = {
  RawMaterialId: null
};

var columns = ["EnumRawMaterialStatus", "RawMaterialStartTs", "RawMaterialEndTs", "RollingStartTs", "RollingEndTs", "ProductCreatedTs"];
var button_array = $('.arrow-categories');

//THIS METHOD WILL BE CALLED BY SYSTEM (SERVER) IN CASE DATA CHANGE, NAME IS IMPORTANT !!!
function RefreshData() {
  let url = "/RawMaterial/ElementDetails";
  let dataToSend = {
    RawMaterialId: CurrentElement.RawMaterialId
  };
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
  $("#SearchGrid").data("kendoGrid").dataSource.read();
  $("#SearchGrid").data("kendoGrid").refresh();
}

function onElementSelect(e) {
  hideCategories();

  if ($('.k-i-arrow-left').length) {
    button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
    $('.more').show(100);
    $('.less').hide(100);
  }

  $('#RawMaterialDetails').addClass('loading-overlay');
  let grid = e.sender;
  let selectedRow = grid.select();
  let selectedItem = grid.dataItem(selectedRow);
  let dataToSend = {
    RawMaterialId: selectedItem.RawMaterialId
  };

  CurrentElement = {
    RawMaterialId: selectedItem.RawMaterialId,
    RawMaterialName: selectedItem.RawMaterialName
  };

  let url = "/RawMaterial/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  $('#RawMaterialDetails').html(partialView);
  $('#RawMaterialDetails').removeClass('loading-overlay');
}

function selectRow() {
  let grid = $("#SearchGrid").data("kendoGrid");
  let data = grid.dataSource.data();
  for (let i = 0; i < data.length; i++) {
    const materialId = data[i].RawMaterialId;
    if (CurrentElement.RawMaterialId && materialId === CurrentElement.RawMaterialId) {
      $('tr[data-uid="' + data[i].uid + '"]').addClass('k-state-selected');
    }
  }
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

function GoToRawMaterial(RawMaterialId) {
  let popupTitle = Translations["NAME_RawMaterial"];
  let dataToSend = {
    RawMaterialId: RawMaterialId
  };
  openSlideScreen('RawMaterial', 'ElementDetails', dataToSend, popupTitle);
}
