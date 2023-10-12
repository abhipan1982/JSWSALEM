let CurrentElement;
//let materialDetails;
let materialsToAssign;

var columns = ["RawMaterialCreatedTs", "RawMaterialStartTs", "RawMaterialEndTs", "ProductCreatedTs", "RollingStartTs", "RollingEndTs"];
var button_array = $('.arrow-categories');

RegisterMethod(HmiRefreshKeys.RawMaterialDetails, RefreshData);

//THIS METHOD WILL BE CALLED BY SYSTEM (SERVER) IN CASE DATA CHANGE, NAME IS IMPORTANT !!!
function RefreshData() {
  let url = "/RawMaterial/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, CurrentElement.RawMaterialId, setElementDetailsPartialView);
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
  };

  //materialDetails = {
  //  currentRawMaterial: selectedItem.Id
  //}


  let url = "/Measurements/GetMeasurementsBody";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function getCurrentRawMaterialId() {
  return { RawMaterialId: CurrentElement.RawMaterialId }
}

function setElementDetailsPartialView(partialView) {
  $('#RawMaterialDetails').html(partialView);
  $('#RawMaterialDetails').removeClass('loading-overlay');

  initChartId();
}

function initChartId() {
  console.log('initChartId');
  $('.measurements-chart').each((el, index) => {
    el.attr('id', 'chartId' + index);
  });
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


