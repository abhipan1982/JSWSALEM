let CurrentElement = {
  FeatureTemplateId: null,
};

var columns = ["FKExtUnitOfMeasureId", "FKDataTypeId", "EnumCommChannelType", "EnumAggregationStrategy"];
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

  $('#FeatureTemplateDetails').addClass('loading-overlay');
  let grid = e.sender;
  let selectedRow = grid.select();
  let selectedItem = grid.dataItem(selectedRow);
  let dataToSend = {
    featureTemplateId: selectedItem.FeatureTemplateId
  };

  CurrentElement = {
    FeatureTemplateId: selectedItem.FeatureTemplateId,
  };

  let url = "/MillConfigurator/FeatureTemplate/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  $('#FeatureTemplateDetails').html(partialView);
  $('#FeatureTemplateDetails').removeClass('loading-overlay');
}

function onElementReload() {
  if (CurrentElement.FeatureTemplateId) {
    let dataToSend = {
      featureTemplateId: CurrentElement.FeatureTemplateId
    };

    var url = "/MillConfigurator/FeatureTemplate/ElementDetails";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
  }
}

function createFeatureTemplate() {
  OpenInPopupWindow({
    controller: "MillConfigurator/FeatureTemplate", method: "FeatureTemplateCreatePopup", width: 1250, afterClose: reloadKendoGrid
  });
}

function editFeatureTemplate() {
  if (!CurrentElement.FeatureTemplateId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  OpenInPopupWindow({
    controller: "MillConfigurator/FeatureTemplate", method: "FeatureTemplateEditPopup", data: { featureTemplateId: CurrentElement.FeatureTemplateId }, width: 1250, afterClose: reloadKendoGrid
  });
}

function cloneFeatureTemplate() {
  if (!CurrentElement.FeatureTemplateId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  OpenInPopupWindow({
    controller: "MillConfigurator/FeatureTemplate", method: "FeatureTemplateClonePopup", data: { featureTemplateId: CurrentElement.FeatureTemplateId }, width: 1250, afterClose: reloadKendoGrid
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
  var id = CurrentElement.FeatureTemplateId;
  if (id != null) {
    for (let i = 0; i < gridData.length; i++) {
      let currentItem = gridData[i];
      if (currentItem.FeatureTemplateId === id) {
        let currenRow = grid.table.find("tr[data-uid='" + currentItem.uid + "']");
        $(currenRow).addClass('k-state-selected');
        break;
      }
    }
  }
}

function deleteFeatureTemplate() {
  if (!CurrentElement.FeatureTemplateId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
    let dataToSend = {
      featureTemplateId: CurrentElement.FeatureTemplateId
    };
    let targetUrl = "/MillConfigurator/FeatureTemplate/DeleteFeatureTemplate";

    AjaxReqestHelper(targetUrl, dataToSend, refreshData, function () { console.log('DeleteFeatureTemplate - failed'); });
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
