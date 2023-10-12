RegisterMethod(HmiRefreshKeys.DefectGroupsCatalogue, RefreshData);

function RefreshData() {
    reloadKendoGrid();
}

function AddDefectGroupsCataloguePopup() {
    OpenInPopupWindow({
        controller: "DefectGroupsCatalogue", method: "DefectGroupsCatalogueAddPopup", width: 600, data: {  }, afterClose: reloadKendoGrid
    });
}

function EditDefectGroupsCataloguePopup(id) {
        OpenInPopupWindow({
          controller: "DefectGroupsCatalogue", method: "DefectGroupsCatalogueEditPopup", width: 600, data: { DefectCatalogueId: id }, afterClose: reloadKendoGrid
        });
    }

function reloadKendoGrid() {
    let grid = $('#DefectGroupsCatalogueList').data('kendoGrid');
    grid.dataSource.read();
    grid.refresh();
}

function DeleteDefectGroupsCatalogue(itemId) {
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
        let dataToSend = {
            itemId: itemId
        };
      let targetUrl = '/DefectGroupsCatalogue/DeleteDefectGroupsCatalogue';

        AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('deleteDefectGroupsCatalogue - failed'); });
    });
}

function GoToDetails(Id) {
  let dataToSend = {
    Id: Id
  };
  openSlideScreen('DefectGroupsCatalogue', 'ElementDetails', dataToSend, Translations.Details);
}

function checkDefectGroupCodeValidity(errorMsg, initialValue) {
  let sgcode = $('#DefectGroupCode').val();
  if (initialValue === undefined || sgcode !== initialValue) {
    let dataToSend = {
      code: sgcode
    };
    let targetUrl = '/DefectGroupsCatalogue/ValidateDefectGroupsCode';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#DefectGroupCode').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#DefectGroupCode').next().hide();
        }
      },
      function () { console.log('checkDefectGroupCodeValidity - failed'); });
  }
}

function checkDefectGroupNameValidity(errorMsg, initialValue) {
  let sgname = $('#DefectGroupName').val();
  if (initialValue === undefined || sgname !== initialValue) {
    let dataToSend = {
      name: sgname
    };
    let targetUrl = '/DefectGroupsCatalogue/ValidateDefectGroupsName';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#DefectGroupName').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#DefectGroupName').next().hide();
        }
      },
      function () { console.log('checkDefectGroupNameValidity - failed'); });
  }
}