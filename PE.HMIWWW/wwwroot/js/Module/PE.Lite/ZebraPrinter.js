
var columns = ["ProductCatalogueName", "HeatName", "CreatedInL3", "ToBeCompletedBefore", "MaterialCatalogueName", "IsTestOrder"];

var button_array = $('.arrow-categories');

$(document).ready(() => {
  initRefreshHandler();
})


function handleRefresh(refreshData) {
  selectWorkOrder(refreshData.WorkOrderId, refreshData.ProductId);
}

function selectWorkOrder(wo, prod) {
  let workOrderId = wo;
  let productId = prod;

  CurrentElement = {
    WorkOrderId: workOrderId,
    ProductId: productId,
  };

  let grid = $("#SearchGrid").data("kendoGrid");
  let data = grid.dataSource.data();
  for (let i = 0; i < data.length; i++) {
    const woid = data[i].WorkOrderId;
    if (workOrderId == woid) {
      $('tr[data-uid="' + data[i].uid + '"]').addClass('k-state-selected');
      setWODetails(woid)
    } else {
      $('tr[data-uid="' + data[i].uid + '"]').removeClass('k-state-selected');
    }
  }
}

function setWODetails(woid) {
  hideCategories();

  $('#WorkOrderDetails').addClass('loading-overlay');

  var dataToSend = {
    WorkOrderId: woid
  };

  var url = "/LabelPrinter/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);

}

//function selectProduct() {
//  if (!CurrentElement.ProductId) return;

//  let grid = $("#WorkOrderProducts").data("kendoGrid");
//  let data = grid.dataSource.data();
//  for (let i = 0; i < data.length; i++) {
//    const prid = data[i].ProductId;
//    const woid = data[i].WorkOrderId;
//    if (prid == CurrentElement.ProductId && woid == CurrentElement.WorkOrderId) {
//      $('tr[data-uid="' + data[i].uid + '"]').addClass('k-state-selected');
//      setProductLabel(CurrentElement.ProductId);
//    } else {
//      $('tr[data-uid="' + data[i].uid + '"]').removeClass('k-state-selected');
//    }
//  }
//}

//function setProductLabel(productId) {
//  const parameters = {
//    productId: productId
//  }

//  AjaxReqestHelperSilentWithoutDataType('/LabelPrinter/LabelPreview', parameters, displayLabel);
//}

function initRefreshHandler() {
  try {
    SignalrConnection.addToGroup("CurrentLabelPrinting");
    SignalrConnection.on("CurrentLabelPrinting", (refreshData) => {
      handleRefresh(refreshData);
    });

  }
  catch (err) {
    console.log(err);
  }
}


function dataSourceChange() { }

function onElementSelect(e) {
  hideCategories();

  $('#WorkOrderDetails').addClass('loading-overlay');
  var grid = e.sender;
  var selectedRow = grid.select();
  var selectedItem = grid.dataItem(selectedRow);

  var dataToSend = {
    WorkOrderId: selectedItem.WorkOrderId
  };

  CurrentElement = {
    WorkOrderId: selectedItem.WorkOrderId,
    ProductId: null
  };

  var url = "/LabelPrinter/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  $('#WorkOrderDetails').removeClass('loading-overlay');
  $('#WorkOrderDetails').html(partialView);
}

function printLabel(productId) {
  const parameters = {
    productId: productId,
  }

  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], '', () => {
    AjaxReqestHelperSilentWithoutDataType('/LabelPrinter/PrintLabel', parameters, () => { });
  });
}

//function onProductSelect(e) {
//  const grid = e.sender;
//  const selectedRow = grid.select();
//  const selectedItem = grid.dataItem(selectedRow);

//  const parameters = {
//    productId: selectedItem.ProductId,
//  }

//  AjaxReqestHelperSilentWithoutDataType('/LabelPrinter/LabelPreview', parameters, displayLabel);
//}

//function displayLabel(res) {
//  if (res.ImageBase64) 
//    $('#labelPicture').first().attr("src", "data:image/png;base64, " + res.ImageBase64);
//  else
//    $('#labelPicture').first().attr("src", 'css/Shared/document_error.png');
//}
