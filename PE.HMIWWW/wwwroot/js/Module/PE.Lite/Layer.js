let CurrentElement = {
  RawMaterialId: null
};

let materialsToAssign;

var columns = ["RawMaterialCreatedTs", "RawMaterialStartTs", "RawMaterialEndTs", "RollingStartTs", "RollingEndTs", "ProductCreatedTs", "CreatedTs", "LastUpdateTs"];
var button_array = $('.arrow-categories');

RegisterMethod(HmiRefreshKeys.LayerDetails, RefreshData);

//THIS METHOD WILL BE CALLED BY SYSTEM (SERVER) IN CASE DATA CHANGE, NAME IS IMPORTANT !!!
function RefreshData() {
  let url = "/Layer/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, CurrentElement.RawMaterialId, setElementDetailsPartialView);
  $("#SearchGrid").data("kendoGrid").dataSource.read();
  $("#SearchGrid").data("kendoGrid").refresh();
}


function onElementSelect(e) {
  hideCategories();

  $('#LayerDetails').addClass('loading-overlay');
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

  let url = "/Layer/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  $('#LayerDetails').html(partialView);
  $('#LayerDetails').removeClass('loading-overlay');
}

function colorEmptyL3MaterialRow() {
  var grid = $("#SearchGrid").data("kendoGrid");
  var data = grid.dataSource.data();
  $.each(data, function (i, row) {
    if (row.MaterialName == null) {
      $('tr[data-uid="' + row.RawMaterialId + '"] ').css({ "background-color": "#f95554", "color": "#fff" });
    }
  });
}

function GoToMeasurement(MeasurementId) {
  let dataToSend = {
    measurementId: MeasurementId
  };
  openSlideScreen('Layer', 'MeasurementDetails', dataToSend);
}

function GoToHistory(RawMaterialStepId) {
  let popupTitle = Translations["NAME_RawMaterial"];
  let dataToSend = {
    rawMaterialStepId: RawMaterialStepId
  };
  openSlideScreen('Layer', 'HistoryDetails', dataToSend, popupTitle);
}

function GoToRawMaterial(RawMaterialId) {
  let popupTitle = Translations["NAME_RawMaterial"];
  let dataToSend = {
    RawMaterialId: RawMaterialId
  };
  openSlideScreen('RawMaterial', 'ElementDetails', dataToSend, popupTitle);
}
