CurrentElement = {
  ProductId: null,
  WorkOrderId: null,
  EnumInspectionResult: null
};

var columns = ["ProductRollingDate", "CreatedTs", "ProductCatalogueName", "SteelgradeCode", "SteelgradeName", "HeatName", "ProductRollingDate", "DefectsNumber", "RawMaterialId", "EnumInspectionResult"];
var button_array = $('.arrow-categories');

function onElementSelect(e) {
  hideCategories();

  if ($('.k-i-arrow-left').length) {
    button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
    $('.more').show(100);
    $('.less').hide(100);
  }

  $('#ProductsDetails').addClass('loading-overlay');
  let grid = e.sender;
  let selectedRow = grid.select();
  let selectedItem = grid.dataItem(selectedRow);
  let dataToSend = {
    ProductId: selectedItem.ProductId
  };

  CurrentElement = {
    ProductId: selectedItem.ProductId,
    WorkOrderId: selectedItem.WorkOrderId,
    EnumInspectionResult: selectedItem.EnumInspectionResult
  };

  let url = "/Products/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  $('#ProductsDetails').removeClass('loading-overlay');
  $('#ProductsDetails').html(partialView);
}

function selectRow() {
  const grid = $('#SearchGrid').data("kendoGrid");
  let gridData = grid.dataSource.view();
  let id = getUrlParameter('productId');
  //if (id != null) {
  for (let i = 0; i < gridData.length; i++) {
    let currentItem = gridData[i];
    if (currentItem.ProductId === id) {
      let currenRow = grid.table.find("tr[data-uid='" + currentItem.uid + "']");
      grid.select(currenRow);
      break;
    }
  }
  //}
}

function AssignDefectsPopup() {
  OpenInPopupWindow({
    controller: "Products", method: "AssignDefectsPopup", width: 480, data: { productId: CurrentElement.ProductId }
  });
}

//function PrintLabel(workOrderId, seqNumber) {
//  const url = "/LabelPrinter/PrintLabels";
//  const parameters = {
//    WorkOrderId: workOrderId,
//    OrderSeqMin: seqNumber,
//    OrderSeqMax: seqNumber
//  }

//  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], "", () => {
//    AjaxReqestHelper(url, parameters);
//  });
//}

