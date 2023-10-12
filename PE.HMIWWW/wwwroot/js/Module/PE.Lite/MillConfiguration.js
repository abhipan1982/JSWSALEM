let CurrentElement = {
  DragId: null
};

let PDFTitle = null;

function createAsset() {
  OpenInPopupWindow({
    controller: "MillConfigurator/MillConfiguration", method: "AssetCreatePopup", width: 1250, afterClose: refreshTrees
  });
}

function createArea() {
  OpenInPopupWindow({
    controller: "MillConfigurator/MillConfiguration", method: "AreaCreatePopup", width: 1250, afterClose: refreshTrees
  });
}

function createAssetType() {
  OpenInPopupWindow({
    controller: "MillConfigurator/MillConfiguration", method: "AssetTypeCreatePopup", width: 800, afterClose: refreshTrees
  });
}

function editAsset(assetId) {
  if (!assetId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  OpenInPopupWindow({
    controller: "MillConfigurator/MillConfiguration", method: "AssetEditPopup", data: { assetId: assetId }, width: 1250, afterClose: refreshTrees
  });
}

function editAssetType(assetTypeId) {
  if (!assetTypeId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  OpenInPopupWindow({
    controller: "MillConfigurator/MillConfiguration", method: "AssetTypeEditPopup", data: { assetTypeId: assetTypeId }, width: 800, afterClose: refreshTrees
  });
}

function cloneAsset(assetId) {
  if (!assetId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  OpenInPopupWindow({
    controller: "MillConfigurator/MillConfiguration", method: "AssetClonePopup", data: { assetId: assetId }, width: 1250, afterClose: refreshTrees
  });
}

function assetDetails(assetId) {
  if (!assetId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  let dataToSend = {
    assetId: assetId
  };

  openSlideScreen("MillConfigurator/MillConfiguration", "AssetDetails", dataToSend, Translations["NAME_Asset"]);
}

function assetTypeDetails(assetTypeId) {
  if (!assetTypeId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  let dataToSend = {
    assetTypeId: assetTypeId
  };

  openSlideScreen("MillConfigurator/MillConfiguration", "AssetTypeDetails", dataToSend, Translations["NAME_Asset"]);
}

function deleteAsset(assetId) {
  if (!assetId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
    let dataToSend = {
      assetId: assetId
    };
    let targetUrl = "/MillConfigurator/MillConfiguration/DeleteAsset";

    AjaxReqestHelper(targetUrl, dataToSend, closeSlideAndRefresh, function () { console.log('DeleteAsset - failed'); });
  });
}

function deleteAssetType(assetTypeId) {
  if (!assetTypeId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
    let dataToSend = {
      assetTypeId: assetTypeId
    };
    let targetUrl = "/MillConfigurator/MillConfiguration/DeleteAssetType";

    AjaxReqestHelper(targetUrl, dataToSend, closeSlideAndRefresh, function () { console.log('DeleteAssetType - failed'); });
  });
}

function createFeature(assetId) {
  if (!assetId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  OpenInPopupWindow({
    controller: "MillConfigurator/MillConfiguration", method: "FeatureCreatePopup", data: { assetId: assetId }, width: 1250, afterClose: refreshTrees
  });
}

function editFeature(featureId) {
  if (!featureId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  OpenInPopupWindow({
    controller: "MillConfigurator/MillConfiguration", method: "FeatureEditPopup", data: { featureId: featureId }, width: 1250, afterClose: reloadFeatureKendoGrid
  });
}

function cloneFeature(featureId) {
  if (!featureId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  OpenInPopupWindow({
    controller: "MillConfigurator/MillConfiguration", method: "FeatureClonePopup", data: { featureId: featureId }, width: 1250, afterClose: reloadFeatureKendoGrid
  });
}

function featureDetails(featureId) {
  if (!featureId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  let dataToSend = {
    featureId: featureId
  };

  openSlideScreen("MillConfigurator/MillConfiguration", "FeatureDetails", dataToSend, Translations["NAME_Feature"]);
}

function deleteFeature(featureId) {
  if (!featureId) {
    WarningMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
    let dataToSend = {
      featureId: featureId
    };
    let targetUrl = "/MillConfigurator/MillConfiguration/DeleteFeature";

    AjaxReqestHelper(targetUrl, dataToSend, reloadFeatureKendoGrid, function () { console.log('DeleteFeature - failed'); });
  });
}

function reloadFeatureKendoGrid() {
  let grid = $('#AssignedFeaturesListView').data('kendoGrid');
  grid.dataSource.read();
  grid.refresh();

  refreshTrees();
}

function refreshTemplate() {
  let templateId = $('#AssetTemplateId').data("kendoDropDownList").value();
  if (templateId != 0) {
    let dataToSend = {
      assetTemplateId: templateId
    };
    let url = "/MillConfigurator/AssetTemplate/ElementDetailsView";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setTemplatePartialView);
  }
}

function refreshTemplatesView() {
  let assetId = $('#AssetId').val();
  if (assetId != 0) {
    let dataToSend = {
      assetId: assetId
    };
    let url = "/MillConfigurator/MillConfiguration/ElementDetailsView";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setTemplatePartialView);
  }
}

function setTemplatePartialView(partialView) {
  $('#template-data').html(partialView);
}

function removeSpaces(e) {
  var match = e.key.match(/ /g);
  return match ? false : true;
}

function onAssignedDrag(e) {
  if (e.source.AssetId == 0)
    return;

  const path = e.originalEvent.composedPath();

  CurrentElement.AssetId = e.source.AssetId;

  if (e.sender.dataItem(path[1]))
    return;

  let destination = $("#UnassignedAssets").data("kendoTreeList").dataItem(path[1]);
  if (!destination)
    return;

  let source = e.source.OrderSeq;
  if (destination.OrderSeq < 0 && source > 0)
    e.setStatus("i-plus");
}

function onUnassignedDrag(e) {

  const path = e.originalEvent.composedPath();

  if (e.source.AssetId == 0)
    return;

  CurrentElement.AssetId = e.source.AssetId;

  if (e.sender.dataItem(path[1]))
    return;

  let destination = $("#AssignedAssets").data("kendoTreeList").dataItem(path[1]);
  if (!destination)
    return;

  let source = e.source.OrderSeq;
  if (destination.OrderSeq > 0 && source < 0)
    e.setStatus("i-plus");
}

function onUnassignedDragEnd(e) {
  if (e.source.AssetId == 0)
    return;

  const path = e.originalEvent.composedPath();

  if (!e.destination) {
    if (path[1]) {
      e.destination = $("#AssignedAssets").data("kendoTreeList").dataItem(path[1]);
    }

    if (!e.destination)
      return;

    let url = "/MillConfigurator/MillConfiguration/AssignAsset";
    let dataToSend = {
      dragAssetId: CurrentElement.AssetId,
      dropAssetId: e.destination.AssetId
    };
    AjaxReqestHelper(url, dataToSend, refreshTrees, refreshTrees);
  } else {
    let parentAssetId = 0;
    if (e.position == 'over') {
      dropAssetId = e.destination.AssetId;
      parentAssetId = e.destination.AssetId;
    } else {
      parentAssetId = e.destination.FKParentAssetId;
    }

    let url = "/MillConfigurator/MillConfiguration/ReorderUnassignedAssets";
    let dataToSend = {
      dragAssetId: CurrentElement.AssetId,
      parentAssetId: parentAssetId,
    };
    AjaxReqestHelper(url, dataToSend, refreshTrees, refreshTrees);
  }
}

function onAssignedDragEnd(e) {
  if (e.source.AssetId == 0) {
    return;
  }

  const path = e.originalEvent.composedPath();

  if (!e.destination) {
    if (path[1]) {
      e.destination = $("#UnassignedAssets").data("kendoTreeList").dataItem(path[1]);
    }

    if (!e.destination)
      return;

    let url = "/MillConfigurator/MillConfiguration/UnassignAsset";
    let dataToSend = {
      dragAssetId: CurrentElement.AssetId,
    };
    AjaxReqestHelper(url, dataToSend, refreshTrees, refreshTrees);
  } else {
    //let orderSeq = e.sender.dataSource._data.map(a => a.AssetId);
    let dropMode = 0;

    let parentAssetId = 0;

    if (e.position == 'over')
      dropMode = 1;
    else if (e.position == 'before')
      dropMode = 2;
    else if (e.position == 'after')
      dropMode = 3;

    if (e.position == 'over') {
      parentAssetId = e.destination.AssetId;
    } else {
      parentAssetId = e.destination.ParentAssetId;
    }

    let url = "/MillConfigurator/MillConfiguration/ReorderAssignedAssets";
    let dataToSend = {
      dragAssetId: CurrentElement.AssetId,
      dropAssetId: e.destination.AssetId,
      parentAssetId: parentAssetId,
      dropMode: dropMode
    };
    AjaxReqestHelper(url, dataToSend, refreshTrees, refreshTrees);
  }
}

//function onSelect(e) {
//  let elementId = e.sender.element[0].id;
//  let grid;

//  if (elementId == "AssignedAssets") {
//    grid = $("#UnassignedAssets").data("kendoTreeList");
//    grid.select().each(function () {
//      $(this).removeClass('k-state-selected');
//    });
//  } else if (elementId == "UnassignedAssets") {
//    grid = $("#AssignedAssets").data("kendoTreeList");
//    grid.select().each(function () {
//      $(this).removeClass('k-state-selected');
//    });
//  }

//  let tree = e.sender;
//  let selectedRow = tree.select();
//  var selectedItem = tree.dataItem(selectedRow);
//  CurrentElement.AssetId = selectedItem.AssetId;
//}

function refreshTrees() {
  $("#AssignedAssets").data("kendoTreeList").dataSource.read();
  $("#UnassignedAssets").data("kendoTreeList").dataSource.read();
}

function closeSlideAndRefresh() {
  $("#AssignedAssets").data("kendoTreeList").dataSource.read();
  $("#UnassignedAssets").data("kendoTreeList").dataSource.read();
  closeSlideScreen();
}

function verifyFeatures() {
  let targetUrl = "/MillConfigurator/MillConfiguration/VerifyFeatures";
  AjaxReqestHelper(targetUrl, null, refreshTrees, function () { console.log('VerifyFeatures - failed'); });
}

function exportFeatures() {
  window.location.href = "/MillConfigurator/MillConfiguration/ExportFeaturesToPDF";
}

function colorInactiveRecords(e) {
  let data = e.sender.dataSource.data();
  for (let i = 0; i < data.length; i++) {
    const isActive = data[i].IsActive;
    if (!isActive) {
      $('tr[data-uid="' + data[i].uid + '"]').css({ "background-color": "#808080", "color": "#fff" });
    }
  }
}

function getFeaturesPDF(documentTitle) {
  PDFTitle = documentTitle;
  let url = "/MillConfigurator/MillConfiguration/InterfaceDetailsPDF";
  AjaxReqestHelperSilentWithoutDataType(url, {}, openPDFWindow);
}

function openPDFWindow(PDFfile) {
  var title = document.title;
  document.title = PDFTitle;
  $('#PDF-container').html(PDFfile);
  window.print();
  document.title = title;
}
