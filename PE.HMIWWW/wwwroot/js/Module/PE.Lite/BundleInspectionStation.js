RegisterMethod(HmiRefreshKeys.InspectionStation, reloadAllData);

let CurrentElement = {
  RawMaterialId: null
};

var columns = ["ProductCreatedTs", "HeatName", "WorkOrderName"];
var button_array = $('.arrow-categories');

function onElementSelect(e) {
  hideCategories();

  if ($('.k-i-arrow-left').length) {
    button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
    $('.more').show(100);
    $('.less').hide(100);
  }
  $('#InspectionDetails').addClass('loading-overlay');
  var grid = e.sender;
  var selectedRow = grid.select();
  var selectedItem = grid.dataItem(selectedRow);
  var dataToSend = {
    RawMaterialId: selectedItem.RawMaterialId
  };

  CurrentElement = {
    RawMaterialId: selectedItem.RawMaterialId
  };

  var url = "/BundleInspectionStation/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  $('#InspectionDetails').removeClass('loading-overlay');
  $('#InspectionDetails').html(partialView);


  let chart = $("#chart").data("kendoChart");

  if (chart) {
    chart.options.series[0].data[0].visible = false;
    for (let i = 0; i < $("#chart").data("kendoChart").options.series[0].data.length; i++) {
      if ($("#chart").data("kendoChart").options.series[0].data[i].value === 0 ||
        $("#chart").data("kendoChart").options.series[0].data[i].value === null) {
        $("#chart").data("kendoChart").options.series[0].data[i].visible = false;
        $("#chart").data("kendoChart").options.series[0].data[i].visibleInLegend = false;
        $("#chart").data("kendoChart").options.series[0].overlay.gradient = false;
      }
    }
  }
}

function GoToMaterial(materialId) {
  let dataToSend = {
    MaterialId: materialId
  };
  openSlideScreen('Material', 'ElementDetails', dataToSend);
}

function GoToHistory(RawMaterialStepId) {
  let popupTitle = Translations["NAME_RawMaterial"];
  let dataToSend = {
    rawMaterialStepId: RawMaterialStepId
  };
  openSlideScreen('RawMaterial', 'HistoryDetails', dataToSend, popupTitle);
}

function EditWorkOrder() {
  try {
    OpenInPopupWindow({
      controller: "WorkOrder", method: "WorkOrderEditPopup", width: 1250, data: { id: CurrentElement.WorkOrderId, byMaterial: false }, afterClose: onElementReload
    });
  } catch (e) {
    if (e instanceof TypeError) {
      WarningMessage(Translations["MESSAGE_SelectMaterial"]);
    }

  }
}

function reloadKendoGrid() {
  refreshKendoGrid("InspectionStationSearchGrid");

  onElementReload();

  //let materials = $('#MaterialGrading').data('kendoGrid');
  //materials.dataSource.read();
  //materials.refresh();

  //let workorderbody = $('#WorkOrderBody').data('kendoTabStrip');
  //workorderbody.reload();
  //workorderbody.refresh();

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

//heat autocomplete autohide
$(function () {

  $('#error').css("display", "none");
  $("#form").kendoValidator().data("kendoValidator");

  $('.k-autocomplete').css("width", "150px");

  $('.k-autocomplete input').keydown(function () {
    $('.k-autocomplete').animate({
      width: 400
    }, 200, function () {
      // Animation complete.
    });
  });

  $('.k-autocomplete input').focusout(function () {
    $('.k-autocomplete').animate({
      width: 150
    }, 400, function () {
      // Animation complete.
    });
  });


});

function AddQualityPopup() {
  if (!CurrentElement.RawMaterialId) {
    InfoMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  OpenInPopupWindow({
    controller: "Inspection", method: "QualityAddPopup", width: 600, data: { rawMaterialId: CurrentElement.RawMaterialId }, afterClose: refreshQuality
  });
}

function AssignDefectsPopup() {
  if (!CurrentElement.RawMaterialId) {
    InfoMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }
  OpenInPopupWindow({
    controller: "RawMaterial", method: "AssignDefectsPopup", width: 480, data: { rawMaterialId: CurrentElement.RawMaterialId }, afterClose: afterDefectUpdate
  });
}

function editDefectPopup(defectId) {
  if (!defectId) return;

  OpenInPopupWindow({
    controller: "BundleInspectionStation", method: "DefectEditPopup", width: 480, data: { id: defectId }, afterClose: afterDefectUpdate
  });
}

function deleteDefect(itemId) {
  let onSuccessMethod = null;
  let parameters = {
    defectId: itemId
  }
  let url = 'BundleInspectionStation/DeleteDefect'
  PromptMessage(Translations["MESSAGE_deleteConfirm"], '', () => {
    AjaxReqestHelperSilentWithoutDataType(url, parameters, afterDefectUpdate);
  });
}

function selectRow() {
  var grid = $('#SearchGrid').data("kendoGrid");
  var gridData = grid.dataSource.view();
  var id = CurrentElement.RawMaterialId;
  if (id != null) {
    for (let i = 0; i < gridData.length; i++) {
      let currentItem = gridData[i];
      if (currentItem.RawMaterialId === id) {
        let currenRow = grid.table.find("tr[data-uid='" + currentItem.uid + "']");
        //grid.select(currenRow);
        $(currenRow).addClass('k-state-selected');
        break;
      }
    }
  }
}

//refresh

function reloadAllData() {
  refreshSearchGrid();
  onElementReload();
}

function refreshSearchGrid() {
  $("#SearchGrid").data("kendoGrid").dataSource.read();
  $("#SearchGrid").data("kendoGrid").refresh();
}

function onElementReload() {/* refresh all tabs*/
  if (!CurrentElement.RawMaterialId) return;

  var dataToSend = {
    RawMaterialId: CurrentElement.RawMaterialId
  };

  var url = "/BundleInspectionStation/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}


function refreshQuality() {
  let dataToSend = {
    RawMaterialId: CurrentElement.RawMaterialId
  };

  let url = "/BundleInspectionStation/QualityView";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setQualityPartialView);

  refreshSearchGrid();
}

function setQualityPartialView(partialView) {
  $('#quality-data').html(partialView);
}


function refreshScrap() {
  let dataToSend = {
    id: CurrentElement.RawMaterialId
  };
  let url = "/BundleInspectionStation/ScrapByRawMaterialView";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setScrapPartialView);
}
function setScrapPartialView(partialView) {
  $('#scrap-data').html(partialView);
}


function afterDefectUpdate() {
  refreshDefects();
  refreshSearchGrid();
}
function refreshDefects() {
  refreshKendoGrid("IStationDefectsGrid");
}
