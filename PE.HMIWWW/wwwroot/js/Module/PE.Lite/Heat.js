var columns = ["CreatedTs", "SteelgradeName", "LastUpdateTs", "SupplierName"];
var button_array = $('.arrow-categories');
let CurrentElement;

function onElementSelect(e) {
  hideCategories();

  if ($('.k-i-arrow-left').length) {
    button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
    $('.more').show(100);
    $('.less').hide(100);
  }


  $('#HeatDetails').addClass('loading-overlay');
  var grid = e.sender;
  var selectedRow = grid.select();
  var selectedItem = grid.dataItem(selectedRow);
  var dataToSend = {
    HeatId: selectedItem.HeatId
  };

  CurrentElement = {
    HeatId: selectedItem.HeatId
  };

  var url = "/Heat/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
  saveGridState(this);
}

function onElementReload() {

  var dataToSend = {
    HeatId: CurrentElement.HeatId
  };

  var url = "/Heat/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  $('#HeatDetails').removeClass('loading-overlay');
  $('#HeatDetails').html(partialView);
  setTabStripsStates();
}

function GoToMaterial(materialId) {
  let dataToSend = {
    MaterialId: materialId
  };
  openSlideScreen('Material', 'ElementDetails', dataToSend);
}

function GoToWorkOrder(workOrderId) {
  let dataToSend = {
    WorkOrderId: workOrderId
  };
  openSlideScreen('WorkOrder', 'ElementDetails', dataToSend);
}

function AddNew() {
  OpenInPopupWindow({
    controller: "Heat", method: "HeatCreatePopup", width: 1250, afterClose: reloadKendoGrid
  });
}

function EditHeat() {
  try {
    OpenInPopupWindow({
      controller: "Heat", method: "HeatEditPopup", width: 1250, data: { id: CurrentElement.HeatId }, afterClose: onElementReload
    });
  } catch (e) {
    if (e instanceof TypeError) {
      WarningMessage(Translations["MESSAGE_SelectMaterial"]);
    }

  }
}

function reloadKendoGrid() {
  let grid = $('#SearchGrid').data('kendoGrid');
  grid.dataSource.read();
  grid.refresh();
}

function onAdditionalData() {
  return {
    text: $("#FKMaterialCatalogueId").val()
  };
}