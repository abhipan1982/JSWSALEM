let CurrentElement = {};
let noHint = $.noop;

const columns = ["HeatName", "HeatWeight", "SteelgradeCode"];
const button_array = $('.arrow-categories');
const YardsContainerEl = $('#Yards');

class Product {

  constructor(locationId, productId) {
    this.SourceLocationId = locationId || null;
    this.ProductsIds = [];
    this.ProductsIds.push(productId);
  }
  addProductId(productId) {
    this.ProductsIds.push(productId);
  }

  deleteProductId(productId) {
    this.ProductsIds = this.ProductsIds.filter(x => x !== productId);
  }
}

const ProductsRelocation = new class {
  constructor() {
    this.WeightToTransferSum = 0;
    this.TargetLocationId = null;
    this.Products = [];
  }

  addProduct(sourceLocationId, productId, productWeight) {
    this.WeightToTransferSum += productWeight;
    if (this.Products.length) {
      let index = this.Products.findIndex(x => x.SourceLocationId === sourceLocationId);
      if (index >= 0) {
        this.Products[index].addProductId(productId);
        return;
      }
    }
    this.Products.push(new Product(sourceLocationId, productId));
  }
  removeProduct(sourceLocationId, productId, productWeight) {
    this.WeightToTransferSum -= productWeight;
    if (this.Products.length) {
      let index = this.Products.findIndex(x => x.SourceLocationId === sourceLocationId);
      if (index >= 0) {
        this.Products[index].deleteProductId(productId);
      }
    }
  }
  setTargetLocation(targetLocationId) {
    this.TargetLocationId = targetLocationId;
  }
  getData() {
    this.Products = this.Products.filter(x => x.ProductsIds.length !== 0);
    return {
      TargetLocationId: this.TargetLocationId,
      Products: this.Products
    }
  }
  clear() {
    this.WeightToTransferSum = 0;
    this.TargetLocationId = null;
    while (this.Products.length) {
      this.Products.pop();
    }
  }
}

const Search = new class {
  constructor() {
    this.isSearchMode = false;
    this.yardId = null;
    this.workOrderId = null;
  }

  clear() {
    this.isSearchMode = false;
    this.yardId = null;
    this.workOrderId = null;
  }
  setData(yardId, workOrderId) {
    this.isSearchMode = true;
    this.yardId = yardId;
    this.workOrderId = workOrderId;
  }
  getData() {
    return {
      yardId: this.yardId,
      workOrderId: this.workOrderId,
    }
  }
}

const WorkOrder = new class {
  constructor() {
    this.workOrderId = null;
  }

  clear() {
    this.workOrderId = null;
  }
  setData(woId) {
    this.workOrderId = woId;
  }
  getData() {
    return {
      workOrderId: this.workOrderId,
    }
  }
}

function ClearSearch() {
  Search.clear();
  const locationsEl = $('.location');

  if (locationsEl.length > 0) {
    for (let i = 0; i < locationsEl.length; i++) {
      $(locationsEl[i]).removeClass('location-active');
    }
  }
  $("#SearchGrid").data("kendoGrid").clearSelection();
}

function OnSearchGridElSelect(e) {
  const grid = e.sender;
  const selectedRow = grid.select();
  const selectedItem = grid.dataItem(selectedRow);

  if (!selectedItem) return;

  Search.setData(selectedItem.YardId, selectedItem.WorkOrderId);
  GoToYardMap(selectedItem.YardId);
}

function searchInLocations(currentYardId) {
  if (!Search.isSearchMode) return;
  if (!Search.yardId || Search.yardId != currentYardId) return;

  const locationsIds = getLocationsBySearchData(Search.getData());

  if (locationsIds) {
    highlightLocations(locationsIds);
  }
}

function getLocationsBySearchData(parameters) {
  if (!parameters) {
    console.error('Missing data: search parameters');
    return;
  }
  let data = AjaxGetDataHelper(Url("ProductYard", "GetSearchedLocationsIds"), parameters);
  if (!data) {
    return null;
  }
  return data.LocationIds;
}

function highlightLocations(locationsIds) {
  const locationsEl = $('.location');

  if (locationsEl.length > 0) {
    for (let i = 0; i < locationsEl.length; i++) {
      if (locationsIds.includes($(locationsEl[i]).data('location'))) {
        $(locationsEl[i]).addClass('location-active');
      }
    }
  }
}

function OpenLocationSchedulePopup() {
  WorkOrder.clear();
  openSlideScreen('ProductYard', 'GetLocationSchedulePartialView', null, Translations["NAME_LocationScheduling"]);
}

function OpenReadyForShipmentPopup() {
  WorkOrder.clear();
  openSlideScreen('ProductYard', 'GetShipmentPartialView', null, Translations["NAME_WOsReadyForShipment"]);
}

function OpenRelocationPopup() {
  ProductsRelocation.clear();
  openSlideScreen('ProductYard', 'GetRelocationPartialView', null, Translations["NAME_Relocation"]);
}

function GoToYardsList() {
  YardsContainerEl.addClass('loading-overlay');
  const url = "/ProductYard/GetYardsView";
  AjaxReqestHelperSilentWithoutDataType(url, null, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  YardsContainerEl.removeClass('loading-overlay');
  YardsContainerEl.html(partialView);
}

function GoToYardMap(id) {
  if (!id) {
    console.error('Missing data: yardId');
    return;
  }
  const dataToSend = { id: id };
  YardsContainerEl.addClass('loading-overlay');
  const url = "/ProductYard/GetYardMapView";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function GoToLocation(id) {
  if (!id) {
    console.error('Missing data: locationId');
    return;
  }
  const dataToSend = { id: id };
  openSlideScreen('ProductYard', 'GetPileContenPartialtView', dataToSend, Translations["NAME_Location"]);
}

function GoToWorkOrder(workOrderId) {
  let dataToSend = {
    WorkOrderId: workOrderId
  };
  openSlideScreen('WorkOrder', 'ElementDetails', dataToSend, Translations["NAME_Details"]);
}

function GoToProducts(workOrderId, locationId) {
  let dataToSend = {
    WorkOrderId: workOrderId,
    AssetId: locationId
  };
  openSlideScreen('ProductYard', 'GetProductsInLocationView', dataToSend, Translations["NAME_WOProductsInLoc"]);
}

function GoToProductDetails(productId) {
  CurrentElement.ProductId = productId;
  let dataToSend = {
    ProductId: productId
  };
  openSlideScreen('Products', 'ElementDetails', dataToSend, Translations["NAME_Details"]);
}

function DispatchWorkOrder() {
  if (!WorkOrder.workOrderId) {
    InfoMessage(Translations["INFO_SelectWO"]);
    return;
  }

  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], "", () => {
    AjaxReqestHelper(Url("ProductYard", "DispatchWorkOrder"), WorkOrder.getData(), reloadAfterDispatch);
  });
}

function reloadAfterDispatch() {
  WorkOrder.clear();
  reloadReadyWOGrid();
  reloadLocationsByWOGrid();
}

function placeholder(element) {
  return element.clone().addClass("").css("opacity", 0.65);
}

function onLocationOrderChange(e) {
  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], '', () => {
    let grid = $("#LocationsScheduleList").data("kendoGrid"),
      oldIndex = e.oldIndex,
      newIndex = e.newIndex,
      dataItem = grid.dataSource.getByUid(e.item.data("uid")),
      newOrder = grid.dataSource.at(newIndex).FillOrderSeq;

    grid.dataSource.remove(dataItem);
    grid.dataSource.insert(newIndex, dataItem);
    let dataToSend = {
      locationId: dataItem.AssetId,
      oldIndex: dataItem.FillOrderSeq,
      newIndex: newOrder
    };
    AjaxReqestHelper(Url("ProductYard", "ReorderLocationSeq"), dataToSend, reloadLocSeqGrid, reloadLocSeqGrid);
    $('.sweet-alert .cancel').off("click");
  });
  handleCancel($('.sweet-alert .cancel'), reloadLocSeqGrid);
}

function onSelectProductInLocationGridEl(e) {
  const grid = e.sender;
  const selectedRow = grid.select();
  const selectedItem = grid.dataItem(selectedRow);
  const $selectedRow = $(selectedRow);

  if ($selectedRow.hasClass('selected-row')) {
    $selectedRow.removeClass('selected-row k-state-selected');
    ProductsRelocation.removeProduct(selectedItem.AssetId, selectedItem.ProductId, selectedItem.ProductWeight);
    if (!ProductsRelocation.getData().Products.length) ProductsRelocation.clear();
  } else {
    $selectedRow.addClass('selected-row').removeClass('k-state-selected');
    ProductsRelocation.addProduct(selectedItem.AssetId, selectedItem.ProductId, selectedItem.ProductWeight);
  }

  ColorTargetLocationList(ProductsRelocation.WeightToTransferSum);
}

function OnTargetLocationSelect(assetId, assetFreeWeight) {
  const source = ProductsRelocation.getData();
  if (source.Products.length == 0) {
    InfoMessage(Translations["INFO_ProductTransfer"]);
    return;
  }
  if (assetFreeWeight < ProductsRelocation.WeightToTransferSum) return;
  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], '', () => {
    ProductsRelocation.setTargetLocation(assetId);
    relocateProductsAction();
  });
}

function ColorTargetLocationList(weight) {
  const grid = $("#TargetLocationsList").data("kendoGrid");
  const gridGroups = grid.dataSource.view();

  for (let j = 0; j < gridGroups.length; j++) {
    let groupData = gridGroups[j];
    for (let i = 0; i < groupData.items.length; i++) {
      let currentUid = groupData.items[i].uid;
      let currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");

      if (groupData.items[i].WeightFree < weight) {
        debugger;
        $(currenRow).addClass('unavailable');
      } else {
        debugger;
        $(currenRow).removeClass('unavailable');
      }
    }
  }
}

function ColorLocSeqGridFirstRow() {
  let grid = $("#LocationsScheduleList").data("kendoGrid");
  if (!grid) return;

  let gridData = grid.dataSource.view();
  let firstRow = grid.table.find("tbody tr:first");
  let firstRowData = gridData[0];
  $(firstRow).addClass('available');
}

function OnReadyWOSelect(e) {
  const grid = e.sender;
  const selectedRow = grid.select();
  const selectedItem = grid.dataItem(selectedRow);

  if (!selectedItem) return;

  WorkOrder.setData(selectedItem.WorkOrderId);
  reloadLocationsByWOGrid();
  $('#dispatchLocations').hide();
  $('#dispatchLocationsFiltered').show();
}

function getWOData() {
  return WorkOrder.getData();
}

function reloadLocationsByWOGrid() {
  let grid = $('#LocationsByWOList').data('kendoGrid');
  if (grid) {
    grid.dataSource.read();
    grid.refresh();
  }
}

function reloadReadyWOGrid() {
  let grid = $('#WOReadyGrid').data('kendoGrid');
  if (grid) {
    grid.dataSource.read();
    grid.refresh();
  }
}

function reloadLocSeqGrid() {
  let grid = $('#LocationsScheduleList').data('kendoGrid');
  if (grid) {
    grid.dataSource.read();
    grid.refresh();
  }
}

function reloadProductsInLocationsGrid() {
  let grid = $('#ProductsInLocationsList').data('kendoGrid');
  if (grid) {
    grid.dataSource.read();
    grid.refresh();
  }
}

function reloadTargetLocationGrid() {
  let grid = $('#TargetLocationsList').data('kendoGrid');
  if (grid) {
    grid.dataSource.read();
    grid.refresh();
  }
}

function handleCancel(btn, onEventFunction) {
  btn.click(() => {
    onEventFunction();
    btn.off("click");
  })
}

function reloadRelocation() {
  ProductsRelocation.clear();
  reloadProductsInLocationsGrid();
  reloadTargetLocationGrid();
}

function relocateProductsAction() {
  const url = '/ProductYard/ProductsRelocation';
  let data = ProductsRelocation.getData();
  let productsToSend = data.Products;
  let productIds = [];
  let locationIds = [];
  for (let i = 0; i < productsToSend.length; i++) {
    for (let j = 0; j < productsToSend[i].ProductsIds.length; j++) {
      productIds.push(productsToSend[i].ProductsIds[j]);
      locationIds.push(locationId = productsToSend[i].SourceLocationId);
    }
  }
  let dataToSend = {
    targetLocationId: data.TargetLocationId,
    sourceLocations: locationIds,
    products: productIds
  };
  //sendRequestWithData(dataToSend, url, reloadRelocation)
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, reloadRelocation)
}

//function sendRequestWithData(dataToSend, url, onSuccessCustomMethod) {
//  RequestStarted();
//  $.ajax({
//    type: 'POST',
//    url: url,
//    traditional: true,
//    data: dataToSend,
//    //contentType: 'application/json',
//    complete: RequestFinished,
//    success: function (data) {
//      PositiveResultNotification();
//      try {
//        onSuccessCustomMethod(data);
//        ModuleWarningHandler(data);
//      }
//      catch (ex) { console.log(ex); }
//    },
//    error: function (data) {
//      PeErrorHandler(data);
//      try {
//        onErrorCustomMethod();
//      }
//      catch (ex) { console.log(ex); }
//    }
//  });
//}

function displayLegend($legendWindowEl) {
  if ($legendWindowEl.css('display') === 'none') {
    $legendWindowEl.show();
  } else {
    $legendWindowEl.hide();
  }
}

function refreshYard() {
  let url = "/ProductYard/GetYardsView";
  AjaxReqestHelperSilentWithoutDataType(url, null, setYardPartialView);
}

function setYardPartialView(partialView) {
  $('#Yards').html(partialView);
}

