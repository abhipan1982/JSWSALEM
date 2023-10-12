RegisterMethod(HmiRefreshKeys.WorkOrder, reloadAllData);

let CurrentElement={
  WorkOrderId: null,
  EnumWorkOrderStatus: null
};

var columns = ["WorkOrderCreatedInL3Ts", "ToBeCompletedBeforeTs", "WorkOrderCreatedTs", "WorkOrderStartTs", "WorkOrderEndTs", "ProductCatalogueName", "HeatName", "CreatedInL3", "ToBeCompletedBefore", "MaterialCatalogueName", "IsTestOrder"];

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
    EnumWorkOrderStatus: selectedItem.EnumWorkOrderStatus
  };

  var url = "/WorkOrder/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function onElementReload() {
  var dataToSend = {
    WorkOrderId: CurrentElement.WorkOrderId
  };

  var url = "/WorkOrder/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function handleError(e) {
  ErrorMessage(e.responseJSON.Data.Errors);
}

function reloadAllData() {
  reloadKendoGrid();
  onElementReload();
}

function setElementDetailsPartialView(partialView) {
  $('#WorkOrderDetails').removeClass('loading-overlay');
  $('#WorkOrderDetails').html(partialView);


  let chart = $("#chart").data("kendoChart");

  if (chart) {
    chart.options.series[0].data[0].visible = false;
    for (let i = 0; i < $("#chart").data("kendoChart").options.series[0].data.length; i++) {
      if ($("#chart").data("kendoChart").options.series[0].data[i].value === 0 ||
        $("#chart").data("kendoChart").options.series[0].data[i].value === null) {
        $("#chart").data("kendoChart").options.series[0].data[i].visible = false;
        $("#chart").data("kendoChart").options.series[0].data[i].visibleInLegend = false;
        $("#chart").data("kendoChart").options.series[0].overlay.gradient = false;
      }
    }
  }
}

function GoToMaterial(materialId) {
  let dataToSend = {
    MaterialId: materialId
  };
  openSlideScreen('Material', 'ElementDetails', dataToSend);
}

function AddNew() {
  OpenInPopupWindow({
    controller: "WorkOrder", method: "WorkOrderCreatePopup", width: 1250, afterClose: reloadKendoGrid
  });
}



function EditWorkOrder() {
  try {
    OpenInPopupWindow({
      controller: "WorkOrder", method: "WorkOrderEditPopup", width: 1250, data: { id: CurrentElement.WorkOrderId, byMaterial: false }, afterClose: onElementReload
    });
  } catch (e) {
    if (e instanceof TypeError) {
      WarningMessage(Translations["MESSAGE_SelectMaterial"]);
    }

  }
}


function SendReport() {
  try {
    $('#WorkOrderDetails').addClass('loading-overlay');

    let currentOrder = function (callback) {
      if (callback === false) {
        $('#WorkOrderDetails').removeClass('loading-overlay');
      } else {
        let url = "/WorkOrder/SendReport";
        dataToSend = {
          workOrderId: CurrentElement.WorkOrderId
        };
          AjaxReqestHelper(url, dataToSend, setElementDetailsPartialView, setElementDetailsPartialView);
        $("#SearchGrid").data("kendoGrid").dataSource.read();
        $("#SearchGrid").data("kendoGrid").refresh();
      }
    };
    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], "", currentOrder);
  } catch (e) {
      $('#WorkOrderDetails').removeClass('loading-overlay');
    if (e instanceof TypeError) {
      WarningMessage(Translations["MESSAGE_SelectMaterial"]);
    }
  }
}

//function AddNewHeat() {
//    ClosePopup();
//    let grid = $('#FKHeatIdRef');
//    grid.hide();
//    OpenInPopupWindow({
//        controller: "Heat", method: "HeatCreatePopup", width: 1250, afterClose: reloadKendoGrid
//    });
//}

function reloadKendoGrid() {
  let grid = $('#SearchGrid').data('kendoGrid');
  grid.dataSource.read();
  grid.refresh();

  onElementReload();

  //let materials = $('#MaterialGrading').data('kendoGrid');
  //materials.dataSource.read();
  //materials.refresh();

  //let workorderbody = $('#WorkOrderBody').data('kendoTabStrip');
  //workorderbody.reload();
  //workorderbody.refresh();

}

function Delete() {
  $('#WorkOrderDetails').addClass('loading-overlay');

  let currentOrder = function (callback) {
    if (callback === false) {
      $('#WorkOrderDetails').removeClass('loading-overlay');
    } else {
      let url = "/WorkOrder/DeleteWorkOrder";
      dataToSend = {
        workOrderId: CurrentElement.WorkOrderId
      };
      AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
      $("#SearchGrid").data("kendoGrid").dataSource.read();
      $("#SearchGrid").data("kendoGrid").refresh();
    }
  };
  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_MaterialScrap"], currentOrder);
}

function onAdditionalData() {
  return {
    text: $("#Heat").val(),
    isTest: $("#IsTestOrder").is(':checked')
  };
}

//heat autocomplete autohide
$(function () {

  $('#error').css("display", "none");
  $("#form").kendoValidator().data("kendoValidator");

  $('.k-autocomplete').css("width", "150px");

  $('.k-autocomplete input').keydown(function () {
    $('.k-autocomplete').animate({
      width: 400
    }, 200, function () {
      // Animation complete.
    });
  });

  $('.k-autocomplete input').focusout(function () {
    $('.k-autocomplete').animate({
      width: 150
    }, 400, function () {
      // Animation complete.
    });
  });


});

function displayMessage() {
  var validator = $("#form").kendoValidator().data("kendoValidator");

  if (validator.validate()) {
    $('#error').css("display", "none");
  } else {
    $('#error').css("display", "block");
    $('#popup-footer')
      .css('display', 'block')
      .css('background-color', 'rgb(206, 0, 55)');
  }
}

$(function () {

  $('#error').css("display", "none");
  $("#form").kendoValidator().data("kendoValidator");

});

function ShowBilletCatalogueDetailsAfterSelect() {
  var billetCatalogId = $("#FKMaterialCatalogueId")[0].value;

  if (billetCatalogId !== "") {
    var data = AjaxGetDataHelper(Url("WorkOrder", "GetBilletCatalogueDetails"), { billetCatalogId: billetCatalogId });
    $(".billet-details").show();
    $("#BilletCatalogueWeightMin").val(data.WeightMin);
    $("#BilletCatalogueWeightMax").val(data.WeightMax);
    $("#CatalogueWeightMin").text(data.WeightMin);
    $("#CatalogueWeightMax").text(data.WeightMax);
  }
  else {
    $(".billet-details").hide();
  }
}

function ShowProductCatalogueDetailsAfterSelect() {
  var productCatalogId = $("#FKProductCatalogueId")[0].value;

  if (productCatalogId !== "") {
    var data = AjaxGetDataHelper(Url("WorkOrder", "GetProductCatalogDetails"), { productCatalogId: productCatalogId });
    $(".product-details").show();
    $("#Thickness").text(data.Thickness);
    $("#Width").text(data.Width);
    $("#Shape").text(data.Shape);
  }
  else {
    $(".product-details").hide();
  }
}

function HeatSelected(e) {
  if (e.item) {
    var dataItem = this.dataItem(e.item.index());
    $("#FKHeatId").val(dataItem.HeatId);
  } else {
    $("#FKHeatId").val(null);
  }

  $("#FKSteelgradeId").data("kendoDropDownList").dataSource.read();
}

function filterSteelgrades() {
  return {
    heatId: $("#FKHeatId").val()
  };
}

function onSteelgradeLoad(e) {
    var ds = this.dataSource.data();
    if (ds.length > 0) {
        this.select(1);
    }
}

function CalculateMaterialsWeight() {
  var materialNumber = $("#MaterialsNumber").data("kendoNumericTextBox").value();
  var totalWeight = $("#TargetOrderWeight").data("kendoNumericTextBox").value();

  if (!totalWeight) $("#TargetOrderWeight").val(null);

  if (materialNumber && totalWeight) {
    $(".material-weight").show();
    var newWeight = parseFloat(totalWeight / materialNumber);

    if (newWeight) {
      $("#material-weight-value").text(newWeight.toFixed(3));
      $("#BilletWeight").val(newWeight);
    } else {
      $("#material-weight-value").text('-');
      $("#BilletWeight").val(null);
    }
  }
  else {
    $(".material-weight").hide();
  }
}

//legend
function displayWorkOrderLegendWindow() {
  let legendWindow = $('#workorder-legend');
  if (legendWindow.css('display') === 'none') {
    legendWindow.show();
  } else {
    legendWindow.hide();
  }
}

function CancelWorkOrder() {

  let grid = $("#SearchGrid").data("kendoGrid");
  let data = grid.dataSource.data();

  for (let i = 0; i < data.length; i++) {
    const workOrderId = data[i].WorkOrderId;
    if (CurrentElement.WorkOrderId == workOrderId) {
      if (!data[i].IsNewStatus) {
        WarningMessage(Translations["MESSAGE_SelectOrderWithStatusNew"]);
        return;
      }
    }
  }

  dataToSend = {
    workOrderId: CurrentElement.WorkOrderId
  };
  let url = 'WorkOrder/CancelWorkOrder';
  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_WorkOrderWillBeCancel"], () => {
    AjaxReqestHelper(url, dataToSend, reloadKendoGrid);
  });
}

function UnCancelWorkOrder() {

  let grid = $("#SearchGrid").data("kendoGrid");
  let data = grid.dataSource.data();
  let uncancelable = false;

  for (let i = 0; i < data.length; i++) {
    const workOrderId = data[i].WorkOrderId;
    if (CurrentElement.WorkOrderId == workOrderId && data[i].UnCancellable) {
      uncancelable = true;
    }
  }
  if (!uncancelable) {
    WarningMessage(Translations["MESSAGE_SelectOrderWithStatusCancelled"]);
    return;
  }
  dataToSend = {
    workOrderId: CurrentElement.WorkOrderId
  };
  let url = 'WorkOrder/UnCancelWorkOrder';
  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_WorkOrderWillBeUnCancel"], () => {
    AjaxReqestHelper(url, dataToSend, reloadKendoGrid, handleError);
  });
}

function BlockWorkOrder() {

  let grid = $("#SearchGrid").data("kendoGrid");
  let data = grid.dataSource.data();

  for (let i = 0; i < data.length; i++) {
    const workOrderId = data[i].WorkOrderId;
    if (CurrentElement.WorkOrderId == workOrderId) {
      if (!data[i].IsNewStatus) {
        WarningMessage(Translations["MESSAGE_SelectOrderWithStatusNew"]);
        return;
      }
    }
  }

  dataToSend = {
    workOrderId: CurrentElement.WorkOrderId
  };
  let url = 'WorkOrder/BlockWorkOrder';
  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_WorkOrderWillBeBlock"], () => {
    AjaxReqestHelper(url, dataToSend, reloadKendoGrid, handleError);
  });
}

function UnBlockWorkOrder() {

  let grid = $("#SearchGrid").data("kendoGrid");
  let data = grid.dataSource.data();
  let unblockable = false;

  for (let i = 0; i < data.length; i++) {
    const workOrderId = data[i].WorkOrderId;
    if (CurrentElement.WorkOrderId == workOrderId && data[i].UnBlockable) {
      unblockable = true;
    }
  }
  if (!unblockable) {
    WarningMessage(Translations["MESSAGE_SelectOrderWithGrayHighlight"]);
    return;
  }
  dataToSend = {
    workOrderId: CurrentElement.WorkOrderId
  };
  let url = 'WorkOrder/UnBlockWorkOrder';
  PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_WorkOrderWillBeUnBlock"], () => {
    AjaxReqestHelper(url, dataToSend, reloadKendoGrid, handleError);
  });
}

