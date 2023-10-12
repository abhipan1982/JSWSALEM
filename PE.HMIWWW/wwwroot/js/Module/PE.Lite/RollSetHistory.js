let CurrentElement = {
  RollSetId: null,
};

var columns = ["EnumRollSetStatus", "UpperRollName",
  "BottomRollName", "MountedTs", "DismountedTs"];

var button_array = $('.arrow-categories');

function onElementSelect(e) {
  hideCategories();

  if ($('.k-i-arrow-left').length) {
    button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
    $('.more').show(100);
    $('.less').hide(100);
  }

  $('#RollSetHistoryDetails').addClass('loading-overlay');
  let grid = e.sender;
  let selectedRow = grid.select();
  let selectedItem = grid.dataItem(selectedRow);
  let dataToSend = {
    RollSetId: selectedItem.RollSetId
  };

  CurrentElement = {
    RollSetId: selectedItem.RollSetId,
  };

  let url = "/RollSetHistory/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  $('#RollSetHistoryDetails').removeClass('loading-overlay');
  $('#RollSetHistoryDetails').html(partialView);
}

function selectRow() {
  var grid = $('#SearchGrid').data("kendoGrid");
  var gridData = grid.dataSource.view();
  var id = getUrlParameter('Id');
  for (var i = 0; i < gridData.length; i++) {
    var currentItem = gridData[i];
    if (currentItem.RollSetId === id) {
      var currenRow = grid.table.find("tr[data-uid='" + currentItem.uid + "']");
      grid.select(currenRow);
      break;
    }
  }
}

function dataBoundHandler() {
  selectRowAfterBack(this);
}

function reloadKendoGrid() {
  let grid = $('#SearchGrid').data('kendoGrid');
  grid.dataSource.read();
  grid.refresh();
}
