RegisterMethod(HmiRefreshKeys.SteelFamily, RefreshData);

function RefreshData() {
    reloadKendoGrid();
}

function EditSteelFamilyPopup(id) {
    OpenInPopupWindow({
      controller: "SteelFamilyCatalogue", method: "SteelFamilyEditPopup", width: 1250, data: { SteelFamilyId: id }, afterClose: reloadKendoGrid
    });
}

function CreateSteelFamilyPopup() {
    OpenInPopupWindow({
      controller: "SteelFamilyCatalogue", method: "SteelFamilyCreatePopup", width: 1250, data: { }, afterClose: reloadKendoGrid
    });
}

function reloadKendoGrid() {
    let grid = $('#SteelFamilyList').data('kendoGrid');
    grid.dataSource.read();
    grid.refresh();
}

function GoToDetails(Id) {
  let dataToSend = {
    Id: Id
  };
  openSlideScreen('SteelFamilyCatalogue', 'ElementDetails', dataToSend, Translations.Details);
}

function Delete(itemId) {
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
        let dataToSend = {
            itemId: itemId
        };
      let targetUrl = '/SteelFamilyCatalogue/DeleteSteelFamily';

        AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('deleteSteelFamily - failed'); });
    });
}

function checkSteelFamilyCodeValidity(errorMsg, initialValue ) {
  let sgcode = $('#Code').val();
  if (initialValue === undefined || sgcode !== initialValue)
  {
    let dataToSend = {
      code: sgcode
    };
    let targetUrl = '/SteelFamilyCatalogue/ValidateSteelFamilyCode';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#Code').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#Code').next().hide();
        }
      },
      function () { console.log('checkSteelFamilyCodeValidity - failed'); });
  }
}

function checkSteelFamilyNameValidity(errorMsg, initialValue) {
  let sgname = $('#Name').val();
  if (initialValue === undefined || sgname !== initialValue)
  {
    let dataToSend = {
      name: sgname
    };
    let targetUrl = '/SteelFamilyCatalogue/ValidateSteelFamilyName';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#Name').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#Name').next().hide();
        }
      },
      function () { console.log('checkSteelFamilyNameValidity - failed'); });
  }
}