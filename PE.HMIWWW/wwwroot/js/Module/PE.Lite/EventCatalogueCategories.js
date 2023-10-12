RegisterMethod(HmiRefreshKeys.EventCatalogueCategories, RefreshData);

function RefreshData() {
  reloadKendoGrid();
}

function EventCatalogueCategoriesAddPopup() {
  OpenInPopupWindow({
    controller: "EventCatalogueCategories", method: "EventCatalogueCategoriesAddPopup", width: 600, data: {}, afterClose: reloadKendoGrid
  });
}

function EventCatalogueCategoriesEditPopup(id) {
  OpenInPopupWindow({
    controller: "EventCatalogueCategories", method: "EventCatalogueCategoriesEditPopup", width: 600, data: { eventCatalogueCategoryId: id }, afterClose: reloadKendoGrid
  });
}

function reloadKendoGrid() {
  let grid = $('#EventCatalogueCategoriesList').data('kendoGrid');
  grid.dataSource.read();
  grid.refresh();
}

function Delete(itemId) {
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
    let dataToSend = {
      itemId: itemId
    };
    let targetUrl = '/EventCatalogueCategories/DeleteEventCatalogueCategory';

    AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('deleteEventCatalogueCategories - failed'); });
  });
}

function GoToDetails(Id) {
  let dataToSend = {
    Id: Id
  };
  openSlideScreen('EventCatalogueCategories', 'ElementDetails', dataToSend, Translations.Details);
}

function checkEventCatalogueCategoryCodeValidity(errorMsg, initialValue) {
  let sgcode = $('#EventCatalogueCategoryCode').val();
  if (initialValue === undefined || sgcode !== initialValue) {
    let dataToSend = {
      code: sgcode
    };
    let targetUrl = '/EventCatalogueCategories/ValidateEventCategoriesCode';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#EventCatalogueCategoryCode').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#EventCatalogueCategoryCode').next().hide();
        }
      },
      function () { console.log('checkEventCatalogueCategoryCodeValidity - failed'); });
  }
}

function checkEventCatalogueCategoryNameValidity(errorMsg, initialValue) {
  let sgname = $('#EventCatalogueCategoryName').val();
  if (initialValue === undefined || sgname !== initialValue) {
    let dataToSend = {
      name: sgname
    };
    let targetUrl = '/EventCatalogueCategories/ValidateEventCategoriesName';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#EventCatalogueCategoryName').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#EventCatalogueCategoryName').next().hide();
        }
      },
      function () { console.log('checkEventCatalogueCategoryNameValidity - failed'); });
  }
}
