RegisterMethod(HmiRefreshKeys.ScrapGroup, RefreshData);

function RefreshData() {
    reloadKendoGrid();
}

function EditScrapGroupPopup(id) {
    OpenInPopupWindow({
      controller: "ScrapGroupCatalogue", method: "ScrapGroupEditPopup", width: 1250, data: { ScrapGroupId: id }, afterClose: reloadKendoGrid
    });
}

function CreateScrapGroupPopup() {
    OpenInPopupWindow({
      controller: "ScrapGroupCatalogue", method: "ScrapGroupCreatePopup", width: 1250, data: { }, afterClose: reloadKendoGrid
    });
}

function reloadKendoGrid() {
    let grid = $('#ScrapGroupList').data('kendoGrid');
    grid.dataSource.read();
    grid.refresh();
}

function Delete(itemId) {
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
        let dataToSend = {
            itemId: itemId
        };
      let targetUrl = '/ScrapGroupCatalogue/DeleteScrapGroup';

        AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('deleteScrapGroup - failed'); });
    });
}

function GoToDetails(Id) {
  let dataToSend = {
    Id: Id
  };
  openSlideScreen('ScrapGroupCatalogue', 'ElementDetails', dataToSend, Translations.Details);
}

function checkScrapGroupCodeValidity(errorMsg, initialValue ) {
  let sgcode = $('#ScrapGroupCode').val();
  if (initialValue === undefined || sgcode !== initialValue)
  {
    let dataToSend = {
      code: sgcode
    };
    let targetUrl = '/ScrapGroupCatalogue/ValidateScrapGroupCode';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#ScrapGroupCode').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#ScrapGroupCode').next().hide();
        }
      },
      function () { console.log('checkScrapGroupCodeValidity - failed'); });
  }
}

function checkScrapGroupNameValidity(errorMsg, initialValue) {
  let sgname = $('#ScrapGroupName').val();
  if (initialValue === undefined || sgname !== initialValue)
  {
    let dataToSend = {
      name: sgname
    };
    let targetUrl = '/ScrapGroupCatalogue/ValidateScrapGroupName';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#ScrapGroupName').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#ScrapGroupName').next().hide();
        }
      },
      function () { console.log('checkScrapGroupNameValidity - failed'); });
  }
}