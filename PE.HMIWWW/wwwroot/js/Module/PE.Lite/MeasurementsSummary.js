let CurrentElement;
let CurrentFeature;
let selectedWOId;

var columns = ["ToBeCompletedBeforeTs", "WorkOrderCreatedTs", "ProductCatalogueName", "HeatName", "CreatedInL3", "ToBeCompletedBefore", "MaterialCatalogueName", "IsTestOrder"];

var button_array = $('.arrow-categories');

function onElementSelect(e) {
  hideCategories();

  if ($('.k-i-arrow-left').length) {
    button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
    $('.more').show(100);
    $('.less').hide(100);
  }

  $('#WorkOrderDetails').addClass('loading-overlay');
  var grid = e.sender;
  var selectedRow = grid.select();
  var selectedItem = grid.dataItem(selectedRow);
  selectedWOId = selectedItem.WorkOrderId;
  var dataToSend = {
    WorkOrderId: selectedItem.WorkOrderId
  };

  CurrentElement = {
    WorkOrderId: selectedItem.WorkOrderId
  };

  CurrentFeature = {
    FeatureId: null,
  };

  var url = "/MeasurementsSummary/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

onMeasurementSelect = function (e) {

  let grid = e.sender;
  let selectedRow = grid.select();
  let selectedItem = grid.dataItem(selectedRow);

  let dataToSend;

  CurrentFeature = {
    FeatureId: selectedItem.FeatureId,
  };

  let url = "/MeasurementsSummary/MeasurementsSummaryChart";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialViewMeasurements);
}

function setElementDetailsPartialViewMeasurements(partialView) {
  $('#chart').removeClass('loading-overlay');
  $('#chart').html(partialView);
}




function setElementDetailsPartialView(partialView) {
  $('#WorkOrderDetails').removeClass('loading-overlay');
  $('#WorkOrderDetails').html(partialView);
}

function GoToMaterial(materialId) {
  let dataToSend = {
    MaterialId: materialId
  };
  openSlideScreen('Material', 'ElementDetails', dataToSend);
}


function reloadKendoGrid() {
  let grid = $('#SearchGrid').data('kendoGrid');
  grid.dataSource.read();
  grid.refresh();

  onElementReload();
}


function onAdditionalData() {
  return {
    text: $("#FKHeatIdRef").val()
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

function getCurrentWorkOrderId() {
  return { workOrderId: CurrentElement.WorkOrderId };
}


function GoToMeasurement(MeasurementId) {
  let dataToSend = {
    measurementId: MeasurementId
  };
  openSlideScreen('RawMaterial', 'MeasurementDetails', dataToSend);
}



//-------

function colorEmptyL3MaterialRow() {
  var grid = $("#SearchGrid").data("kendoGrid");
  var data = grid.dataSource.data();
  $.each(data, function (i, row) {
    if (row.MaterialName == null) {
      $('tr[data-uid="' + row.Id + '"] ').css({ "background-color": "#f95554", "color": "#fff" });
    }
  });
}

//
$('.less').hide(100);

function showHideCategories() {
  var button = $('.show-hide-categories');
  var grid = $("#SearchGrid").data("kendoGrid");
  var button_array = $('.arrow-categories');
  if (button.hasClass('off')) {
    $('.more').hide(100);
    $('.less').show(100);
    $('.element-details').hide(100);
    columns.forEach(function (element) {
      grid.showColumn(element);
    });
    $('.grid-filter').toggleClass('col-11 col-3', 750, function () {

    });
  }
  if (button.hasClass('on')) {
    $('.more').show(100);
    $('.less').hide(100);
    columns.forEach(function (element) {
      grid.hideColumn(element);
    });
    $('.grid-filter').toggleClass('col-11 col-3', 650, function () {

      $('.element-details').show(650);
    });
  }
  button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
  button.toggleClass('off on');
}

function hideCategories() {
  if (!$(".grid-filter").is(":animated")) {
    var button = $('.show-hide-categories');
    if (button.hasClass('on')) {
      var grid = $("#SearchGrid").data("kendoGrid");
      var button_array = $('.arrow-categories');
      button_array.toggleClass('right left');
      button.toggleClass('on off');
      columns.forEach(function (element) {
        grid.hideColumn(element);
      });
      $('.grid-filter').toggleClass('col-11 col-3', 650, function () {
        $('.element-details').show(650);
      });
    }
  }
}

function dataSourceChange(e) {
  var arrow = $('.arrow-categories');
  var currentFilter = this.filter();
  if (currentFilter == undefined || currentFilter.length < 1) {
    if (arrow.hasClass('arrow-yellow')) {
      arrow.toggleClass('arrow-yellow arrow-white');
    }
  }
  else {
    if (arrow.hasClass('arrow-white')) {
      arrow.toggleClass('arrow-yellow arrow-white');
    }
  }
}
