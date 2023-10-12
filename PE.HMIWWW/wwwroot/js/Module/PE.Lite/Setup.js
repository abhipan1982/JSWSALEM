let CurrentElement;
var lastSelectedRow = null;
var lastUpdatedElementRow = null;
let lastSelectedIndex;
var kendoGrid;

var columns = [];
var button_array = $('.arrow-categories');

function onElementSelect(e) {
  hideCategories();

  if ($('.k-i-arrow-left').length) {
    button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
    $('.more').show(100);
    $('.less').hide(100);
  }

  kendoGrid = $('#SearchGrid').data('kendoGrid');
  lastSelectedRow = kendoGrid.select();

  $('#SetupDetails').addClass('loading-overlay');
  let grid = e.sender;
  let selectedRow = grid.select();
  let selectedItem = grid.dataItem(selectedRow);
  if (lastUpdatedElementRow && (lastSelectedRow[0].outerHTML === lastUpdatedElementRow[0].outerHTML)) {
    lastUpdatedElementRow = null;
    return;
  }
  let dataToSend = {
    TelegramId: selectedItem.TelegramId
  };
  CurrentElement = {
    TelegramId: selectedItem.TelegramId
  };
  let url = "/Setup/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function onSetupSelect(e) {
  hideCategories();

  if ($('.k-i-arrow-left').length) {
    button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
    $('.more').show(100);
    $('.less').hide(100);
  }

  kendoGrid = $('#SearchGrid').data('kendoGrid');
  lastSelectedRow = kendoGrid.select();

  $('#SetupDetails').addClass('loading-overlay');
  let grid = e.sender;
  let selectedRow = grid.select();
  let selectedItem = grid.dataItem(selectedRow);
  lastSelectedIndex = selectedRow[0].rowIndex;
  if (lastUpdatedElementRow && (lastSelectedRow[0].outerHTML === lastUpdatedElementRow[0].outerHTML)) {
    lastUpdatedElementRow = null;
    return;
  }
  let dataToSend = {
    setupId: selectedItem.SetupId
  };
  CurrentElement = {
    TelegramId: selectedItem.SetupId
  };
  let url = "/Setup/FindSetupProperties";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView, onError);


}

function GetSelectedSetupIdFromSearchGrid() {
  kendoGrid = $('#SearchGrid').data('kendoGrid');
  let selectedRow = kendoGrid.select();
  if (selectedRow.length > 0) {
    let selectedItem = kendoGrid.dataItem(selectedRow);
    return selectedItem.SetupId;
  }
  return 0;
}

function GetSelectedSetupTypeFromSearchGrid() {
  kendoGrid = $('#SearchGrid').data('kendoGrid');
  let selectedRow = kendoGrid.select();
  if (selectedRow.length > 0) {
    let selectedItem = kendoGrid.dataItem(selectedRow);
    return selectedItem.SetupTypeId;
  }
  return 0;
}

function setElementDetailsPartialView(partialView) {
  $('#SetupDetails').removeClass('loading-overlay');
  $('#SetupDetails').html(partialView);
}

function onError(data) {
  console.log(data);
}

function valueEditor(container, options) {

  switch (options.model.DataType) {
    case 'BOOL':
      let checked = '';
      if (options.model.Value === '1') checked = 'checked';
      $('<input type="checkbox"  id="Value"  ' + checked + '>').appendTo(container);
      break;
    case 'REAL':
      $('<input type="number" step=".1"  id="Value" value=' + options.model.Value + '>').appendTo(container);
      break;
    case 'INT':
    case 'DINT':
      $('<input type="number" step="1"  id="Value"  value=' + options.model.Value + '>').appendTo(container);
      break;
    default:
      $('<input type="text"  id="Value"  value=' + options.model.Value + '>').appendTo(container);
      break;
  }
}

function updateValuesSave(data) {

  lastUpdatedElementRow = kendoGrid.select();

  let value;
  if (data.model.DataType === 'BOOL') {
    if ($('#Value').prop('checked') === true) {
      value = "1";
    }
    else {
      value = "0";
    }
  }
  else
    value = $("#Value").val();
  let dataToSend = {
    ElementId: data.model.ElementId,
    Value: value,
    DataType: data.model.DataType,
    TelegramId: data.model.TelegramId,
    TelegramStructureIndexString: data.model.TelegramStructureIndexString
  };
  AjaxReqestHelper(Url('Setup', 'UpdateValues'), dataToSend, RefreshData, RefreshData);

}


function updateSetupSave(e) {

  let value;
  if (e.model.DataType === 'BOOL') {
    if ($('#Value').prop('checked') === true) {
      value = "1";
    }
    else {
      value = "0";
    }
    e.model.set('Value', value);
  }
  else
    value = $("#Value").val();

  let dataToSend = {
    InstructionId: e.model.InstructionId,
    Value: value,
    DataType: e.model.DataType
  };
}

function RefreshData() {
  let tree = $("#TelegramStructureTree").data("kendoTreeList");
  tree.dataSource.read();
  $('.k-command-cell>button').html('<span class="k-icon edit-button-ico"></span>');


  $.when(() => {
    kendoGrid.dataSource.read();
    kendoGrid.refresh();
  }).then(() => {
    kendoGrid.select(lastSelectedRow);

  });
}

function RefreshSetupData() {
  let grid = $("#SearchGrid").data("kendoGrid");
  grid.dataSource.read();
  kendoGrid.refresh();
}

function SelectLastRow() {
  if (kendoGrid !== undefined) {
    kendoGrid.select("tr:eq(" + lastSelectedIndex + ")");
  }
  PropmtSearchMessageIfEmpty();
}


function onBound() {
  let tree = $("#TelegramStructureTree").data("kendoTreeList");
  let treeData = tree.dataSource.view();

  for (let i = 0; i < treeData.length; i++) {
    let currentUid = treeData[i].uid;
    if (treeData[i].DataType === "STRUCT") {
      var currenRow = tree.table.find("tr[data-uid='" + currentUid + "']");
      var editButton = $(currenRow).find(".edit-button");
      editButton.hide();
    }
  }
}

function SendTelegram() {
  AjaxReqestHelper(Url('Setup', 'SendTelegram'), { telegramId: CurrentElement.TelegramId }, function () { });
}

function onEdit(e) {
  $('.k-grid-update').html('<span class="k-icon k-i-check"></span>').css({ 'min-width': 0 });
  $('.k-grid-cancel').html('<span class="k-icon k-i-check"></span>').css({ 'min-width': 0 });

  $('.k-grid-cancel').on('click', function () {
    $('.k-command-cell>button').html('<span class="k-icon edit-button-ico"></span>').css({ 'min-width': 0 });
  });
  ReplaceWithCorrectInputType(e);
}
function ReplaceWithCorrectInputType(e) {
  let dataType = e.model.DataType;

  console.log("model", e.model);

  let input = $(e.container).find('input:not(.not-editable');
  if (e.model.IsRequired === true && dataType != 'BOOL') {
    $(input).attr("required", true);
  }
  $("input").on("keydown", function (e) {
    SaveSetupValues(e);
  });
  switch (dataType) {
    case 'REAL':
      $(input).attr('type', 'number');
      $(input).attr('min', e.model.RangeFrom);
      $(input).attr('max', e.model.RangeTo);
      $(input).attr('step', '0.1');
      break;
    case 'INT':
    case 'DINT':
      $(input).attr('type', 'number');
      $(input).attr('min', e.model.RangeFrom);
      $(input).attr('max', e.model.RangeTo);
      $(input).attr('step', '1');
      break;
    case 'BOOL':
      $(input).attr('class', 'k-checkbox');
      $(input).attr('type', 'checkbox');
      if (e.model.Value == "1")
        $(input).attr('checked', 'checked');
      break;
  }
}

function CreateNewVersion() {
  PromptMessage("New Version", "Create new version of this telegram?", function () { return CreateNewVersionConfirmed(CurrentElement.TelegramId) });

}

function CreateNewVersionConfirmed(telegramId) {
  return AjaxReqestHelper(Url("Setup", "CreateNewVersion"), { telegramId: telegramId }, RefreshData, RefreshData);
}

function DeleteTelegram() {
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "?", function () { return DeleteTelegramConfirmed(CurrentElement.TelegramId) });

}

function DeleteTelegramConfirmed(telegramId) {
  return AjaxReqestHelper(Url("Setup", "DeleteTelegram"), { telegramId: telegramId }, RefreshData, RefreshData);
}

function UpdateSetupFromL1() {
  if (CurrentElement !== undefined) {
    PromptMessage("Update setup from L1", "Update setup from L1?", function () { return UpdateSetupFromL1Confiremd(CurrentElement.TelegramId) });
  }
  else {
    WarningMessage(Translations["MESSAGE_SelectMaterial"]);
  }

}

function UpdateSetupFromL1Confiremd(telegramId) {
  return AjaxReqestHelper(Url("Setup", "UpdateSetupFromL1"), { telegramId: telegramId }, RefreshData, RefreshData);
}

function sendFilters() {
  let listOfFilters = [];
  let filters = $('#filters-pane').find('input');
  for (var i = 0; i < filters.length; i++) {
    listOfFilters.push({ ParameterName: "ww", ParameterValue: filters[i].value });
  }
  let dataToSend = JSON.stringify({ 'listOfFilters': listOfFilters });

  $.ajax({
    contentType: 'application/json',
    dataType: 'json',
    type: 'POST',
    url: Url("Setup", "FindIdOfInstruction"),
    data: dataToSend,
    success: function () {
    },
    failure: function (response) {
    }
  });
}

function EditSetupPopup() {
  let setupId = GetSelectedSetupIdFromSearchGrid();
  if (setupId > 0) {
    OpenInPopupWindow({
      controller: "Setup",
      method: "SetupEditPopup",
      width: 500,
      data: { setupId: setupId },
      afterClose: RefreshSetupData
    });
  }
}

function CloneSetupPopup() {
  let setupId = GetSelectedSetupIdFromSearchGrid();
  if (setupId > 0) {
    OpenInPopupWindow({
      controller: "Setup",
      method: "SetupCopyPopup",
      width: 500,
      data: { setupId: setupId },
      afterClose: RefreshSetupData
    });
  }
}

function CreateSetupPopup(setupType) {
  if (setupType > 0) {
    OpenInPopupWindow({
      controller: "Setup",
      method: "SetupCreatePopup",
      width: 500,
      data: { setupType: setupType },
      afterClose: RefreshSetupData
    });
  }
}

function DeleteSetup() {
  let setupId = GetSelectedSetupIdFromSearchGrid();
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
    let dataToSend = {
      setupId: setupId
    };
    let targetUrl = '/Setup/DeleteSetup';
    AjaxReqestHelper(targetUrl, dataToSend, RefreshSetupData, function () { console.log('deleteSetup- failed'); });
  });
}

function PropmtSearchMessageIfEmpty() {
  if ($('#SearchGrid').data('kendoGrid').dataSource.view().length === 0) {
    $('#SetupDetails').addClass('loading-overlay');
    $('#SetupDetails').html('<div class="row justify-content-center plug">'
      + '<div class= "col-6 align-self-center" >'
      + '<img src="~/css/Shared/plug.png" />'
      + '<p class="mt-5 plug-info">' + Translations['INFO_SelectPlugInfo'] + '</p>'
      + '</div>'
      + '</div>');
  }
}

function SaveSetupValues(e) {
  //get the pressed key code
  var code = (e.keyCode ? e.keyCode : e.which);
  console.log(code);
  if (code === 13) { //Enter keycode
    setTimeout(function () {
      $("#SetupPropertiesGrid").data("kendoGrid").saveRow();
    });
  }
  if (code === 27) { //Enter keycode
    $('.k-grid-cancel').click();
  }
}
$(document).ready(function () {

});