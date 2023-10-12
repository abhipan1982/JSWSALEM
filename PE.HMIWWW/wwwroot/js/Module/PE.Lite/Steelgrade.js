RegisterMethod(HmiRefreshKeys.Steelgrade, RefreshData);

function RefreshData() {
  reloadKendoGrid();
}

function EditSteelgradePopup(id) {
  OpenInPopupWindow({
    controller: "Steelgrade", method: "SteelgradeEditPopup", width: 1250, data: { steelgradeId: id }, afterClose: reloadKendoGrid
  });
}

function CreateSteelgradePopup() {
  OpenInPopupWindow({
    controller: "Steelgrade", method: "SteelgradeCreatePopup", width: 1250, data: {}, afterClose: reloadKendoGrid
  });
}

function reloadKendoGrid() {
  let grid = $('#SteelgradeList').data('kendoGrid');
  grid.dataSource.read();
  grid.refresh();
}

function GoToDetails(Id) {
  let dataToSend = {
    Id: Id
  };
  openSlideScreen('Steelgrade', 'ElementDetails', dataToSend, Translations.Details);
}

function Delete(itemId) {
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
    let dataToSend = {
      itemId: itemId
    };
    let targetUrl = '/Steelgrade/DeleteSteelgrade';

    AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('deleteSteelgrade - failed'); });
  });
}

function checkSteelgradeCodeValidity(errorMsg, initialValue) {
  let sgcode = $('#SteelgradeCode').val();
  if (initialValue === undefined || sgcode !== initialValue) {
    let dataToSend = {
      code: sgcode
    };
    let targetUrl = '/Steelgrade/ValidateSteelgradeCode';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#SteelgradeCode').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#SteelgradeCode').next().hide();
        }
      },
      function () { console.log('checkSteelgradeCodeValidity - failed'); });
  }
}

function checkSteelgradeNameValidity(errorMsg, initialValue) {
  let sgname = $('#SteelgradeName').val();
  if (initialValue === undefined || sgname !== initialValue) {
    let dataToSend = {
      name: sgname
    };
    let targetUrl = '/Steelgrade/ValidateSteelgradeName';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#SteelgradeName').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#SteelgradeName').next().hide();
        }
      },
      function () { console.log('checkSteelgradeNameValidity - failed'); });
  }
}