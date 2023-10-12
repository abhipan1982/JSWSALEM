let CurrentElement = {
  RawMaterialId: null
};

var columns = ["RollingStartTs", "RollingEndTs", "ProductCreatedTs", "MaterialCreatedTs", "MaterialStartTs", "MaterialEndTs", "WorkOrderStartTs", "WorkOrderEndTs", "LastUpdateTs", "CreatedTs", "MaterialCatalogueName", "WorkOrderName", "MaterialIsAssigned"];

function onElementSelect(e) {
  hideCategories();

  if ($('.k-i-arrow-left').length) {
    button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
    $('.more').show(100);
    $('.less').hide(100);
  }

  $('#MaterialDetails').addClass('loading-overlay');
  let grid = e.sender;
  let selectedRow = grid.select();
  let selectedItem = grid.dataItem(selectedRow);

  CurrentElement = {
    RawMaterialId: selectedItem.RawMaterialId
  };

  let dataToSend = {
    RawMaterialId: CurrentElement.RawMaterialId
  };

  let url = Url("QualityExpert", "MaterialGradingInformation");
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  $('#MaterialDetails').removeClass('loading-overlay');
  $('#MaterialDetails').html(partialView);
  loadMaterialGrading();
}

function setCurrValueImage(currValue) {
  switch (currValue) {
    case 1:
    case 2: return '<i class=\"material-icons grading-status-grid\">check_circle</i>';
    case 3: return '<i class=\"material-icons grading-status-grid\">report_problem</i>';
    case 4:
    case 5: return '<i class=\"material-icons grading-status-grid\">remove_circle</i>';
    default: return '<i class=\"material-icons grading-status-grid\">report_off</i>';
  }
}

function setAltOrForcedValueImage(forcedValue, altValue) {
  if (forcedValue) return '<i class="material-icons grading-status-grid">lock</i>';
  else if (altValue) return '<i class="material-icons grading-status-grid">build</i>';
  return "";
}

function forceValueDialog(ratingId) {
  OpenInPopupWindow({
    controller: "QualityExpert", method: "ForceValueDialog", width: 400, data: { ratingId: ratingId }, afterClose: RefreshData
  });
}

function showRatingDetailsDialog(ratingId) {
  OpenInPopupWindow({
    controller: "QualityExpert", method: "RatingDetailsDialog", width: 1450, data: { ratingId: ratingId }//, afterClose: onElementReload
  });
}

function compensationStateIcon(state, compensationId, ratingId) {
  if (state)
    return '<i class="material-icons compensation-status-ico compensation-selected" onClick="toggleCompensation(' + compensationId + ',' + ratingId + ',' + state + ')">check_box</i>';
  else
    return '<i class="material-icons compensation-status-ico compensation-not-selected" onClick="toggleCompensation(' + compensationId + ',' + ratingId + ',' + state + ')">indeterminate_check_box</i>';
}

function toggleCompensation(compensationId, ratingId, isChosen) {
  var dataToSend = {
    compensationId: compensationId,
    ratingId: ratingId,
    isChosen: !isChosen
  };
  AjaxReqestHelper(Url("QualityExpert", "ToggleCompensation"), dataToSend, compensationChanged, compensationChanged);
}

function compensationChanged() {
  ClosePopup();
  RefreshData();
}

function RefreshData() {
  let dataToSend = {
    RawMaterialId: CurrentElement.RawMaterialId
  };

  let url = Url("QualityExpert", "MaterialGradingInformation");
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function loadMaterialGrading() {
  $("#SpecifiedGradingValue").load($("#SpecifiedGradingValue").data("url"));
}

function onChildInfoSelect(el) {
  var gradingUrl = Url("QualityExpert", "GetMaterialGrading") + "?RawMaterialId=" + el.id;
  $("#SpecifiedGradingValue").load(gradingUrl);
  $(".active-material-vis").removeClass("active-material-vis");
  $(el).addClass("active-material-vis");

  var gradingDetailsUrl = Url("QualityExpert", "GetMaterialGradingPerAsset") + "?RawMaterialId=" + el.id;
  var grid = $("#MaterialGrading").data("kendoGrid");
  grid.dataSource.transport.options.read.url = gradingDetailsUrl;
  grid.dataSource.read();
}
