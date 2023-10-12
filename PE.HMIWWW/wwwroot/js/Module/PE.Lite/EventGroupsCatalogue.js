RegisterMethod(HmiRefreshKeys.EventGroupsCatalogue, RefreshData);

function RefreshData() {
    reloadKendoGrid();
}

function AddEventGroupsCataloguePopup() {
    OpenInPopupWindow({
      controller: "EventCategoryGroupsCatalogue", method: "EventGroupsCatalogueAddPopup", width: 600, data: {  }, afterClose: reloadKendoGrid
    });
}

function EditEventGroupsCataloguePopup(id) {
        OpenInPopupWindow({
          controller: "EventCategoryGroupsCatalogue", method: "EventGroupsCatalogueEditPopup", width: 600, data: { eventCatalogueId: id }, afterClose: reloadKendoGrid
        });
    }

function reloadKendoGrid() {
    let grid = $('#EventGroupsCatalogueList').data('kendoGrid');
    grid.dataSource.read();
    grid.refresh();
}

function DeleteEventGroupsCatalogue(itemId) {
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
        let dataToSend = {
            itemId: itemId
        };
      let targetUrl = '/EventCategoryGroupsCatalogue/DeleteEventGroupsCatalogue';

        AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('deleteEventGroupsCatalogue - failed'); });
    });
}

function GoToDetails(Id) {
  let dataToSend = {
    Id: Id
  };
  openSlideScreen('EventCategoryGroupsCatalogue', 'ElementDetails', dataToSend, Translations.Details);
}

function checkEventGroupCodeValidity(errorMsg, initialValue) {
  let sgcode = $('#EventGroupCode').val();
  if (initialValue === undefined || sgcode !== initialValue) {
    let dataToSend = {
      code: sgcode
    };
    let targetUrl = '/EventCategoryGroupsCatalogue/ValidateEventGroupsCode';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#EventGroupCode').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#EventGroupCode').next().hide();
        }
      },
      function () { console.log('checkEventGroupCodeValidity - failed'); });
  }
}

function checkEventGroupNameValidity(errorMsg, initialValue) {
  let sgname = $('#EventGroupName').val();
  if (initialValue === undefined || sgname !== initialValue) {
    let dataToSend = {
      name: sgname
    };
    let targetUrl = '/EventCategoryGroupsCatalogue/ValidateEventGroupsName';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#EventGroupName').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#EventGroupName').next().hide();
        }
      },
      function () { console.log('checkEventGroupNameValidity - failed'); });
  }
}
