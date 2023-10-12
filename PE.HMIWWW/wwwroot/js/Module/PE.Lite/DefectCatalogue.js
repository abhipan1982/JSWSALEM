RegisterMethod(HmiRefreshKeys.DefectCatalogue, RefreshData);

function RefreshData() {
    reloadKendoGrid();
}

function AddDefectCataloguePopup() {
        OpenInPopupWindow({
            controller: "DefectCatalogue", method: "DefectCatalogueAddPopup", width: 600, data: {}, afterClose: reloadKendoGrid
        });
    }

function EditDefectCataloguePopup(id) {
        OpenInPopupWindow({
            controller: "DefectCatalogue", method: "DefectCatalogueEditPopup", width: 600, data: { id: id }, afterClose: reloadKendoGrid
        });
    }

function reloadKendoGrid() {
    let grid = $('#DefectCatalogueList').data('kendoGrid');
    grid.dataSource.read();
    grid.refresh();
}

function Delete(itemId) {
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
        let dataToSend = {
            itemId: itemId
        };
        let targetUrl = '/DefectCatalogue/DeleteDefect';

        AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('deleteDefect - failed'); });
    });
}

function GoToDetails(Id) {
  let dataToSend = {
    Id: Id
  };
    openSlideScreen('DefectCatalogue', 'ElementDetails', dataToSend, Translations['NAME_DefectCatalogue']);
}

function checkDefectCodeValidity(errorMsg, initialValue) {
  let sgcode = $('#DefectCatalogueCode').val();
  if (initialValue === undefined || sgcode !== initialValue) {
    let dataToSend = {
      code: sgcode
    };
    let targetUrl = '/DefectCatalogue/ValidateDefectCode';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#DefectCatalogueCode').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#DefectCatalogueCode').next().hide();
        }
      },
      function () { console.log('checkDefectCodeValidity - failed'); });
  }
}

function checkDefectNameValidity(errorMsg, initialValue) {
  let sgname = $('#DefectCatalogueName').val();
  if (initialValue === undefined || sgname !== initialValue) {
    let dataToSend = {
      name: sgname
    };
    let targetUrl = '/DefectCatalogue/ValidateDefectName';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#DefectCatalogueName').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#DefectCatalogueName').next().hide();
        }
      },
      function () { console.log('checkDefectNameValidity - failed'); });
  }
}