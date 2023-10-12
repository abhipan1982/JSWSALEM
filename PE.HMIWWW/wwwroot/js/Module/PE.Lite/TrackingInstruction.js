let CurrentElement = {
  TrackingInstructionId: null,
};

var columns = ["FeatureCode", "SeqNo", "PointName", "TrackingInstructionValue", "EnumTrackingInstructionType"];
var button_array = $('.arrow-categories');

//THIS METHOD WILL BE CALLED BY SYSTEM (SERVER) IN CASE DATA CHANGE, NAME IS IMPORTANT !!!
function refreshData() {
  let grid = $("#SearchGrid").data("kendoGrid");
  grid.dataSource.read();
  grid.refresh();
}

function onElementSelect(e) {
  hideCategories();

  if ($('.k-i-arrow-left').length) {
    button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
    $('.more').show(100);
    $('.less').hide(100);
  }

  $('#TrackingInstructionDetails').addClass('loading-overlay');
  let grid = e.sender;
  let selectedRow = grid.select();
  let selectedItem = grid.dataItem(selectedRow);
  let dataToSend = {
    trackingInstructionId: selectedItem.TrackingInstructionId
  };

  CurrentElement = {
    TrackingInstructionId: selectedItem.TrackingInstructionId,
  };

  let url = "/MillConfigurator/TrackingInstruction/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  $('#TrackingInstructionDetails').html(partialView);
  $('#TrackingInstructionDetails').removeClass('loading-overlay');
}

function onElementReload() {
  if (CurrentElement.TrackingInstructionId) {
    let dataToSend = {
      trackingInstructionId: CurrentElement.TrackingInstructionId
    };

    var url = "/MillConfigurator/TrackingInstruction/ElementDetails";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
  }
}

function createTrackingInstruction() {
  OpenInPopupWindow({
    controller: "MillConfigurator/TrackingInstruction", method: "TrackingInstructionCreatePopup", width: 1250, afterClose: reloadKendoGrid
  });
}

function editTrackingInstruction(trackingInstructionId) {
  if (!trackingInstructionId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  OpenInPopupWindow({
    controller: "MillConfigurator/TrackingInstruction", method: "TrackingInstructionEditPopup", data: { trackingInstructionId: trackingInstructionId }, width: 1250, afterClose: reloadKendoGrid
  });
}

function cloneTrackingInstruction(trackingInstructionId) {
  if (!trackingInstructionId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  OpenInPopupWindow({
    controller: "MillConfigurator/TrackingInstruction", method: "TrackingInstructionClonePopup", data: { trackingInstructionId: trackingInstructionId }, width: 1250, afterClose: reloadKendoGrid
  });
}

function reloadKendoGrid() {
  let grid = $('#SearchGrid').data('kendoGrid');
  grid.dataSource.read();
  grid.refresh();

  onElementReload();
}

function selectRow() {
  var grid = $('#SearchGrid').data("kendoGrid");
  var gridData = grid.dataSource.view();
  var id = CurrentElement.TrackingInstructionId;
  if (id != null) {
    for (let i = 0; i < gridData.length; i++) {
      let currentItem = gridData[i];
      if (currentItem.TrackingInstructionId === id) {
        let currenRow = grid.table.find("tr[data-uid='" + currentItem.uid + "']");
        $(currenRow).addClass('k-state-selected');
        break;
      }
    }
  }
}

function deleteTrackingInstruction(trackingInstructionId) {
  if (!trackingInstructionId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
    let dataToSend = {
      trackingInstructionId: trackingInstructionId
    };
    let targetUrl = "/MillConfigurator/TrackingInstruction/DeleteTrackingInstruction";

    AjaxReqestHelper(targetUrl, dataToSend, refreshData, function () { console.log('DeleteTrackingInstruction - failed'); });
  });
}

function createBasicTrackingInstructions() {
  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_CreateBasicTrackingInstructionMessage"], () => {
    let targetUrl = "/MillConfigurator/TrackingInstruction/CreateBasicTrackingInstructions";

    AjaxReqestHelper(targetUrl, null, refreshData, function () { console.log('DeleteTrackingInstruction - failed'); });
  });
}

function sendRequest(targetUrl, dataToSend, onSuccessCustomMethod, onErrorCustomMethod) {
  $.ajax({
    type: 'POST',
    url: targetUrl,
    traditional: true,
    data: dataToSend,
    complete: RequestFinished,

    success: function (data) {
      console.log("Request on url: " + targetUrl + " SUCCESSFULL");
      try {
        onSuccessCustomMethod(data);
      }
      catch (ex) { console.log(ex); }
    },
    error: function (data) {
      console.log("ERROR during request on url: " + targetUrl);
      try {
        onErrorCustomMethod(data);
      }
      catch (ex) { console.log(ex); }
    }
  });
}

function colorSelectedRecord(e) {
  var grid = e.sender;
  var gridData = grid.dataSource.view();
  var id = CurrentElement.TrackingInstructionId;
  if (id != null) {
    for (let i = 0; i < gridData.length; i++) {
      let currentItem = gridData[i];
      if (currentItem.TrackingInstructionId === id) {
        let currenRow = grid.table.find("tr[data-uid='" + currentItem.uid + "']");
        $(currenRow).addClass('k-state-selected');
        break;
      }
    }
  }
}
