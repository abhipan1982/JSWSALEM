let CurrentElement = {
  AlarmDefinitionId: null,
};

var columns = ["DefinitionCreated", "DefinitionDescription", "IsStandard", "IsPopupShow", "IsToConfirm", "CategoryDescription"];
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

  $('#AlarmCreatorDetails').addClass('loading-overlay');
  let grid = e.sender;
  let selectedRow = grid.select();
  let selectedItem = grid.dataItem(selectedRow);
  let dataToSend = {
    alarmDefinitionId: selectedItem.AlarmDefinitionId
  };

  CurrentElement = {
    AlarmDefinitionId: selectedItem.AlarmDefinitionId,
  };

  let url = "/AlarmManagement/AlarmCreator/ElementDetails";
  sendRequest(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  $('#AlarmCreatorDetails').html(partialView);
  $('#AlarmCreatorDetails').removeClass('loading-overlay');
}

function onElementReload() {
  let dataToSend = {
    alarmDefinitionId: CurrentElement.AlarmDefinitionId
  };

  var url = "/AlarmManagement/AlarmCreator/ElementDetails";
  sendRequest(url, dataToSend, setElementDetailsPartialView);
}

function createAlarmDefinition() {
  openModal({
    controller: "AlarmManagement/AlarmCreator", method: "AlarmDefinitionCreatePopup", width: 1250, afterClose: reloadKendoGrid
  });
}

function editAlarmDefinition() {
  if (!CurrentElement.AlarmDefinitionId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  openModal({
    controller: "AlarmManagement/AlarmCreator", method: "AlarmDefinitionEditPopup", data: { alarmDefinitionId: CurrentElement.AlarmDefinitionId }, width: 1250, afterClose: reloadKendoGrid
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
  var id = CurrentElement.AlarmDefinitionId;
  if (id != null) {
    for (let i = 0; i < gridData.length; i++) {
      let currentItem = gridData[i];
      if (currentItem.AlarmDefinitionId === id) {
        let currenRow = grid.table.find("tr[data-uid='" + currentItem.uid + "']");
        $(currenRow).addClass('k-state-selected');
        break;
      }
    }
  }
}

function deleteAlarmDefinition() {
  if (!CurrentElement.AlarmDefinitionId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  let dataToSend = {
    alarmDefinitionId: CurrentElement.AlarmDefinitionId
  };

  var url = "/AlarmManagement/AlarmCreator/DeleteAlarmDefinition";
  sendRequest(url, dataToSend, refreshData);
}

function refreshAlarms() {
  var url = "/AlarmManagement/AlarmCreator/RefreshAlarms";
  sendRequest(url, null, refreshData);
}

function onModuleChange(e, element, isStandard) {
  const dataItem = element.dataItem(e.item);

  const moduleCodeString = {
    moduleCode: dataItem.Value,
    isStandard: isStandard
  };

  const definitionCode = fetchData("/AlarmManagement/AlarmCreator/GenerateAlarmCodeByModuleCode", moduleCodeString);
  $("#DefinitionCode").data("kendoTextBox").value(definitionCode);

  let categoryDropDown = $("#FKAlarmCategoryId").data("kendoDropDownList");
  if (dataItem.Value == "SYS") {
    categoryDropDown.select(categoryDropDown.ul.children().eq(categoryDropDown.dataSource.data().length - 1));
  } else {
    categoryDropDown.select(categoryDropDown.ul.children().eq(0));
  }
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

function openModal(config) {
  RequestStarted();
  var popupUrl = serverAddress + '/' + config.controller + '/' + config.method;
  $.ajax({
    type: 'GET',
    dataType: 'html',
    data: (config.data != null ? config.data : {}),
    url: popupUrl,
    complete: RequestFinished,
    success: function (result) {

      var icon = '<img src="/css/Functions/Big/' + (config.icon != null ? config.icon : "edit-white") + '.png"/>';
      var options =
      {
        modal: 0,
        type: 'html',
        afterOpen: popupShowFunction,
        afterClose: config.afterClose !== null ? config.afterClose : undefined,
        beforeClose: function () { popupIsColsed = true; },
        error: popupError,
        content: result,
        width: config.width !== null ? config.width : 400,
        height: config.height !== null ? config.height : 'auto',
        preloaderContent: '<img src="/css/System/Img/ajax-loader-white-bg.gif" class="preloader">'
      };
      if (popupIsColsed) {
        popupIsColsed = false;
        currentPopup = new $.Popup(options);
        currentPopup.open();
        currentPopup.handleResponse = config.handleResponse;
        $('.popup-header-img:not(.info-header-img)').append(icon);

        $('.popup-input').removeClass('k-textbox');


      }
    },
    error: PePopupErrorHandler
  });
}

function sendConfirmedRequest(question, information, functionToRun) {
  swal({
    title: question,
    text: information,
    icon: "warning",
    buttons: true,
    dangerMode: true,
  })
    .then((clicked) => {
      if (clicked) {
        functionToRun();
      } else {
        refreshAlarmRolesGrid();
      }
    });
}

function refreshAlarmRolesGrid() {
  $("#AlarmRolesGrid").data("kendoGrid").dataSource.read();
  $("#AlarmRolesGrid").data("kendoGrid").refresh();
}

function fetchData(targetUrl, dataToSend) {
  var result;
  $.ajax({
    type: 'POST',
    dataType: 'json',
    url: targetUrl,
    async: false,
    data: dataToSend !== null ? dataToSend : undefined,
    success: function (data) {
      result = data;
    },
    error: function (data) {
      console.log("ERROR during request on url: " + targetUrl);
    }
  });
  return result;
}

function generateAlarmDefinition() {
  openModal({
    controller: "AlarmManagement/AlarmCreator", method: "AlarmDefinitionGeneratePopup", width: 1250
  });
}

function displayGeneratedDefinitions(data) {
  RequestFinished();
  let element = document.getElementById("definitions");
  element.value = data;
}
