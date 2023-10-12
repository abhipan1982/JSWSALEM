RegisterMethod(HmiRefreshKeys.Setup, refreshData);

let CurrentElement = {
  ConfigurationId: null,
  ConfigurationName: null
};

var columns = ["ConfigurationCreatedTs"];
var button_array = $('.arrow-categories');

//THIS METHOD WILL BE CALLED BY SYSTEM (SERVER) IN CASE DATA CHANGE, NAME IS IMPORTANT !!!
function refreshData() {
  let url = "/SetupConfiguration/ElementDetails";
  let dataToSend = {
    configurationId: CurrentElement.ConfigurationId
  };
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
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

  $('#SetupConfigurationDetails').addClass('loading-overlay');
  let grid = e.sender;
  let selectedRow = grid.select();
  let selectedItem = grid.dataItem(selectedRow);
  let dataToSend = {
    configurationId: selectedItem.ConfigurationId
  };

  CurrentElement = {
    ConfigurationId: selectedItem.ConfigurationId,
    ConfigurationName: selectedItem.SetupConfigurationName
  };

  let url = "/SetupConfiguration/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  $('#SetupConfigurationDetails').html(partialView);
  $('#SetupConfigurationDetails').removeClass('loading-overlay');
}

function onElementReload() {
  let dataToSend = {
    configurationId: selectedItem.ConfigurationId
  };

  var url = "/SetupConfiguration/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function createSetupConfiguration() {
  OpenInPopupWindow({
    controller: "SetupConfiguration", method: "SetupConfigurationCreatePopup", width: 1000, afterClose: refreshData
  });
}

function editSetupConfiguration() {
  try {
    OpenInPopupWindow({
      controller: "SetupConfiguration", method: "SetupConfigurationEditPopup", width: 1000, data: { configurationId: CurrentElement.ConfigurationId }, afterClose: refreshData
    });
  } catch (e) {
    if (e instanceof TypeError) {
      WarningMessage(Translations["MESSAGE_SelectElement"]);
    }
  }
}

function deleteSetupConfiguration() {
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
    let dataToSend = {
      configurationId: CurrentElement.ConfigurationId
    };
    let targetUrl = '/SetupConfiguration/DeleteSetupConfiguration';

    AjaxReqestHelper(targetUrl, dataToSend, refreshData);
  });
}

function sendSetupConfiguration(steelgradeRelated) {
  let message = steelgradeRelated ? Translations["MESSAGE_SteelgradeRelatedSetupsWillBeSent"] : Translations["MESSAGE_AllSetupsWillBeSent"];

  PromptMessage(Translations["MESSAGE_SendSetupConfirm"], message, () => {
    let dataToSend = {
      configurationId: CurrentElement.ConfigurationId,
      steelgradeRelated: steelgradeRelated
    };
    let targetUrl = '/SetupConfiguration/SendSetupConfiguration';

    AjaxReqestHelper(targetUrl, dataToSend, refreshData);
  });
}

function cloneSetupConfiguration() {
  try {
    OpenInPopupWindow({
      controller: "SetupConfiguration", method: "SetupConfigurationClonePopup", width: 700, data: { configurationId: CurrentElement.ConfigurationId }, afterClose: refreshData
    });
  } catch (e) {
    if (e instanceof TypeError) {
      WarningMessage(Translations["MESSAGE_SelectElement"]);
    }
  }
}

function createSetupConfigurationVersion() {
  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], "", () => {
    let dataToSend = {
      configurationId: CurrentElement.ConfigurationId,
    };
    let targetUrl = '/SetupConfiguration/CreateSetupConfigurationVersion';

    AjaxReqestHelper(targetUrl, dataToSend, refreshData);
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
  var id = CurrentElement.ConfigurationId;
  if (id != null) {
    for (let i = 0; i < gridData.length; i++) {
      let currentItem = gridData[i];
      if (currentItem.ConfigurationId === id) {
        let currenRow = grid.table.find("tr[data-uid='" + currentItem.uid + "']");
        $(currenRow).addClass('k-state-selected');
        break;
      }
    }
  }
}