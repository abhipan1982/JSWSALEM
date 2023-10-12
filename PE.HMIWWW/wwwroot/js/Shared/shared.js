RegisterMethod(HmiRefreshKeys.ActiveBypasses, handleRefresh_ActiveBypassesWidget);

function reloadGrid(gridName = '') {
  const $grid = $('#' + gridName);
  if (!$grid.length) return;
  try {
    const gridData = $grid.data('kendoGrid');
    gridData.dataSource.read();
    gridData.refresh();
  } catch (error) {
    console.error(error);
  }
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

function handleRefresh_ActiveBypassesWidget() {
  $('#activeBypassesWidget').addClass('loading-overlay');
  AjaxReqestHelperSilentWithoutDataType("/Bypass/GetActiveBypassesWidgetView", {}, setWidget_ActiveBypassesWidget);
}

function setWidget_ActiveBypassesWidget(partialView) {
  $('#activeBypassesWidget').removeClass('loading-overlay');
  $('#activeBypassesWidget').html(partialView);
}
