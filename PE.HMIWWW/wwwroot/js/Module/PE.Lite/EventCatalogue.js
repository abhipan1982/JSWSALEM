RegisterMethod(HmiRefreshKeys.EventCatalogue, RefreshData);

function RefreshData() {
  reloadKendoGrid();
}

function AddEventCataloguePopup() {
  OpenInPopupWindow({
    controller: "EventsCatalogue", method: "EventCatalogueAddPopup", width: 600, data: {}, afterClose: reloadKendoGrid
  });
}

function EditEventCataloguePopup(id) {
  OpenInPopupWindow({
    controller: "EventsCatalogue", method: "EventCatalogueEditPopup", width: 600, data: { eventCatalogueId: id }, afterClose: reloadKendoGrid
  });
}

function reloadKendoGrid() {
  let grid = $('#EventCatalogueList').data('kendoGrid');
  grid.dataSource.read();
  grid.refresh();
}

function Delete(itemId) {
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
    let dataToSend = {
      itemId: itemId
    };
    let targetUrl = '/EventsCatalogue/DeleteEvent';

    AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('deleteEventCatalogue - failed'); });
  });
}

function GoToDetails(Id) {
  let dataToSend = {
    Id: Id
  };
  openSlideScreen('EventsCatalogue', 'ElementDetails', dataToSend, Translations.Details);
}

function checkEventCodeValidity(errorMsg, initialValue) {
  let sgcode = $('#EventCode').val();
  if (initialValue === undefined || sgcode !== initialValue) {
    let dataToSend = {
      code: sgcode
    };
    let targetUrl = '/EventsCatalogue/ValidateEventCode';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#EventCode').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#EventCode').next().hide();
        }
      },
      function () { console.log('checkEventCodeValidity - failed'); });
  }
}

function checkEventNameValidity(errorMsg, initialValue) {
  let sgname = $('#EventCatalogueName').val();
  if (initialValue === undefined || sgname !== initialValue) {
    let dataToSend = {
      name: sgname
    };
    let targetUrl = '/EventsCatalogue/ValidateEventName';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#EventCatalogueName').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#EventCatalogueName').next().hide();
        }
      },
      function () { console.log('checkEventGroupNameValidity - failed'); });
  }
}
