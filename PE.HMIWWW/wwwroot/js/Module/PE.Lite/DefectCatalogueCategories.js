RegisterMethod(HmiRefreshKeys.DefectCatalogueCategories, RefreshData);

function RefreshData() {
  reloadKendoGrid();
}

function DefectCatalogueCategoriesAddPopup() {
  OpenInPopupWindow({
    controller: "DefectCatalogueCategories", method: "DefectCatalogueCategoriesAddPopup", width: 600, data: {}, afterClose: reloadKendoGrid
  });
}

function DefectCatalogueCategoriesEditPopup(id) {
  OpenInPopupWindow({
    controller: "DefectCatalogueCategories", method: "DefectCatalogueCategoriesEditPopup", width: 600, data: { DefectCatalogueCategoryId: id }, afterClose: reloadKendoGrid
  });
}

function reloadKendoGrid() {
  let grid = $('#DefectCatalogueCategoriesList').data('kendoGrid');
  grid.dataSource.read();
  grid.refresh();
}

function Delete(itemId) {
  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
    let dataToSend = {
      itemId: itemId
    };
    let targetUrl = '/DefectCatalogueCategories/DeleteDefectCatalogueCategory';

    AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('deleteDefectCatalogueCategories - failed'); });
  });
}

function GoToDetails(Id) {
  let dataToSend = {
    Id: Id
  };
  openSlideScreen('DefectCatalogueCategories', 'ElementDetails', dataToSend, Translations.Details);
}

function checkDefectCategoryCodeValidity(errorMsg, initialValue) {
  let sgcode = $('#DefectCategoryCode').val();
  if (initialValue === undefined || sgcode !== initialValue) {
    let dataToSend = {
      code: sgcode
    };
    let targetUrl = '/DefectCatalogueCategories/ValidateDefectCategoriesCode';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#DefectCategoryCode').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#DefectCategoryCode').next().hide();
        }
      },
      function () { console.log('checkDefectCatalogueCodeValidity - failed'); });
  }
}

function checkDefectCategoryNameValidity(errorMsg, initialValue) {
  let sgname = $('#DefectCategoryName').val();
  if (initialValue === undefined || sgname !== initialValue) {
    let dataToSend = {
      name: sgname
    };
    let targetUrl = '/DefectCatalogueCategories/ValidateDefectCategoriesName';
    AjaxReqestHelperSilent(
      targetUrl,
      dataToSend,
      function (response) {
        if (response) {
          $('#DefectCategoryName').next().show().addClass("k-widget k-tooltip k-tooltip-validation k-invalid-msg field-validation-error").text(errorMsg);
        }
        else {
          $('#DefectCategoryName').next().hide();
        }
      },
      function () { console.log('checkDefectCatalogueNameValidity - failed'); });
  }
}