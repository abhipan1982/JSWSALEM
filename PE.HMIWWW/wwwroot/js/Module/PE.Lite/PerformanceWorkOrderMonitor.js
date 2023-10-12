var columns = ["WorkOrderCreatedInL3Ts", "ToBeCompletedBeforeTs", "WorkOrderCreatedTs", "WorkOrderStartTs", "WorkOrderEndTs"];
var button_array = $('.arrow-categories');


function onElementSelect(e) {
  $('#WorkOrderDetails').addClass('loading-overlay');
  const grid = e.sender;
  const selectedRow = grid.select();
  const selectedItem = grid.dataItem(selectedRow);

  const workOrderId = selectedItem.WorkOrderId

  AjaxReqestHelperSilentWithoutDataType('/PerformanceWorkOrderMonitor/ElementDetails',
    { WorkOrderId: workOrderId }, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  $('#WorkOrderDetails').removeClass('loading-overlay');
  $('#WorkOrderDetails').html(partialView);
}
