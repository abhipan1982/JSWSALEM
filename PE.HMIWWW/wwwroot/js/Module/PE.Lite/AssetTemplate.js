let CurrentElement = {
  AssetTemplateId: null,
  IsZone: null,
};

var columns = ["EnumTrackingAreaType"];
var button_array = $('.arrow-categories');

//THIS METHOD WILL BE CALLED BY SYSTEM (SERVER) IN CASE DATA CHANGE, NAME IS IMPORTANT !!!
function refreshData() {
  let grid = $("#SearchGrid").data("kendoGrid");
  grid.dataSource.read();
  grid.refresh();
}

function onElementSelect(e) {
  hideCategories();

  if ($('.k-i-arrow-left').length) {
    button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
    $('.more').show(100);
    $('.less').hide(100);
  }

  $('#AssetTemplateDetails').addClass('loading-overlay');
  let grid = e.sender;
  let selectedRow = grid.select();
  let selectedItem = grid.dataItem(selectedRow);
  let dataToSend = {
    assetTemplateId: selectedItem.AssetTemplateId
  };

  CurrentElement = {
    AssetTemplateId: selectedItem.AssetTemplateId,
    IsZone: selectedItem.IsZone,
  };

  let url = "/MillConfigurator/AssetTemplate/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  $('#AssetTemplateDetails').html(partialView);
  $('#AssetTemplateDetails').removeClass('loading-overlay');
}

function onElementReload() {
  if (CurrentElement.AssetTemplateId) {
    let dataToSend = {
      assetTemplateId: CurrentElement.AssetTemplateId
    };

    var url = "/MillConfigurator/AssetTemplate/ElementDetails";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
  }
}

function createAssetTemplate() {
  OpenInPopupWindow({
    controller: "MillConfigurator/AssetTemplate", method: "AssetTemplateCreatePopup", width: 800, afterClose: reloadKendoGrid
  });
}

function createAreaTemplate() {
  OpenInPopupWindow({
    controller: "MillConfigurator/AssetTemplate", method: "AreaTemplateCreatePopup", width: 800, afterClose: reloadKendoGrid
  });
}

function editAssetTemplate() {
  if (!CurrentElement.AssetTemplateId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  OpenInPopupWindow({
    controller: "MillConfigurator/AssetTemplate", method: "AssetTemplateEditPopup", data: { assetTemplateId: CurrentElement.AssetTemplateId }, width: 800, afterClose: reloadKendoGrid
  });
}

function cloneAssetTemplate() {
  if (!CurrentElement.AssetTemplateId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  OpenInPopupWindow({
    controller: "MillConfigurator/AssetTemplate", method: "AssetTemplateClonePopup", data: { assetTemplateId: CurrentElement.AssetTemplateId }, width: 800, afterClose: reloadKendoGrid
  });
}

function templatesAssignment() {
  if (!CurrentElement.AssetTemplateId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  if (CurrentElement.IsZone) {
    WarningMessage(Translations["MESSAGE_AssetCannotBeZoneOrArea"]);
    return;
  }

  OpenInPopupWindow({
    controller: "MillConfigurator/FeatureTemplate", method: "TemplatesAssignPopup", data: { assetTemplateId: CurrentElement.AssetTemplateId }, width: 1800, afterClose: reloadKendoGrid
  });
}

function reloadKendoGrid() {
  let grid = $('#SearchGrid').data('kendoGrid');
  grid.dataSource.read();
  grid.refresh();

  onElementReload();
}

function selectRow() {
  var grid = $('#SearchGrid').data("kendoGrid");
  var gridData = grid.dataSource.view();
  var id = CurrentElement.AssetTemplateId;
  if (id != null) {
    for (let i = 0; i < gridData.length; i++) {
      let currentItem = gridData[i];
      if (currentItem.AssetTemplateId === id) {
        let currenRow = grid.table.find("tr[data-uid='" + currentItem.uid + "']");
        $(currenRow).addClass('k-state-selected');
        break;
      }
    }
  }
}

function deleteAssetTemplate() {
  if (!CurrentElement.AssetTemplateId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
    let dataToSend = {
      assetTemplateId: CurrentElement.AssetTemplateId
    };
    let targetUrl = "/MillConfigurator/AssetTemplate/DeleteAssetTemplate";

    AjaxReqestHelper(targetUrl, dataToSend, refreshData, function () { console.log('DeleteAssetTemplate - failed'); });
  });
}

function assignTemplates(featureTemplateId, assetTemplateId) {
  if (!assetTemplateId)
    assetTemplateId = CurrentElement.AssetTemplateId;

  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], "", () => {
    let dataToSend = {
      featureTemplateId: featureTemplateId,
      assetTemplateId: assetTemplateId,
    };
    let targetUrl = "/MillConfigurator/FeatureTemplate/AssignTemplates";

    AjaxReqestHelper(targetUrl, dataToSend, refreshAssignedTemplatesData, function () { console.log('AssignTemplates - failed'); });
  });
}

function refreshAssignedTemplatesData() {
  refreshAssignedFeaturesData();
  let grid = $("#AssignedGrid").data("kendoGrid");
  grid.dataSource.read();
  grid.refresh();
  grid = $("#UnassignedGrid").data("kendoGrid");
  grid.dataSource.read();
  grid.refresh();
}

function refreshAssignedFeaturesData() {
  let grid = $("#AssignedFeaturesList").data("kendoGrid");
  grid.dataSource.read();
  grid.refresh();
}

function onRowReorder(e) {
  var grid = e.sender,
    dataSource = grid.dataSource,
    externalGrid, externalDataItem;

  console.log(e);

  let element = grid.dataItem(e.row);
  if (e.oldIndex === -1) {
    e.preventDefault();
    externalGrid = e.row.parents(".k-grid").data("kendoGrid");
    externalDataItem = externalGrid.dataItem(e.row);
    element = externalDataItem;
    externalGrid.dataSource.remove(externalDataItem);
    dataSource.insert(e.newIndex, externalDataItem.toJSON());
  }

  assignTemplates(element.FeatureTemplateId);
}
