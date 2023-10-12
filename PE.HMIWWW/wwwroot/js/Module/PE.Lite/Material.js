let CurrentElement = {
  MaterialId: null,
  RawMaterialId: null,
  WorkOrderName: null
};

var columns = ["MaterialCreatedTs", "MaterialStartTs", "MaterialEndTs", "MaterialCatalogueName", "WorkOrderName", "MaterialIsAssigned"];
var button_array = $('.arrow-categories');

//THIS METHOD WILL BE CALLED BY SYSTEM (SERVER) IN CASE DATA CHANGE, NAME IS IMPORTANT !!!
function RefreshData() {
  let url = "/Material/ElementDetails";
  let dataToSend = {
    MaterialId: CurrentElement.MaterialId,
    WorkOrderName: CurrentElement.WorkOrderName
  };
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
  $("#SearchGrid").data("kendoGrid").dataSource.read();
  $("#SearchGrid").data("kendoGrid").refresh();
}

function onElementSelect(e) {
  hideCategories();

  if ($('.k-i-arrow-left').length) {
    button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
    $('.more').show(100);
    $('.less').hide(100);
  }

  $('#MaterialDetails').addClass('loading-overlay');
  let grid = e.sender;
  let selectedRow = grid.select();
  let selectedItem = grid.dataItem(selectedRow);
  let dataToSend = {
    MaterialId: selectedItem.MaterialId,
    WorkOrderName: selectedItem.WorkOrderName
  };

  CurrentElement = {
    MaterialId: selectedItem.MaterialId,
    RawMaterialId: selectedItem.RawMaterialId,
    WorkOrderName: selectedItem.WorkOrderName
  };

  let url = "/Material/ElementDetails";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  $('#MaterialDetails').removeClass('loading-overlay');
  $('#MaterialDetails').html(partialView);
}

function colorEmptyL3MaterialAndSelectRow() {
  let grid = $("#SearchGrid").data("kendoGrid");
  let data = grid.dataSource.data();
  for (let i = 0; i < data.length; i++) {
    const isAssigned = data[i].MaterialIsAssigned;
    const materialId = data[i].MaterialId;
    if (CurrentElement.MaterialId && materialId === CurrentElement.MaterialId) {
      $('tr[data-uid="' + data[i].uid + '"]').addClass('k-state-selected');
    } else if (!isAssigned) {
      $('tr[data-uid="' + data[i].uid + '"]').css({ "background-color": "#f95554", "color": "#fff" });
    }
  }
}

function EditWorkOrder() {
  try {
    if (CurrentElement.MaterialId === null) {
      WarningMessage(Translations["MESSAGE_SelectMaterial"]);
    }
    else if (CurrentElement.WorkOrderName === null) {
      WarningMessage(Translations["MESSAGE_MaterialNotAssignedToWorkOrder"]);
    }
    else {
      OpenInPopupWindow({
        controller: "WorkOrder", method: "WorkOrderEditPopup", width: 1250, data: { id: CurrentElement.MaterialId, byMaterial: true }, afterClose: reloadKendoGrid
      });
      PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_MaterialScrap"], scrapMaterial);
    }
  } catch (e) {
    if (e instanceof TypeError) {
      WarningMessage(Translations["MESSAGE_SelectMaterial"]);
    }

  }
}

function dataBoundHandler() {
  selectRowAfterBack(this);
}

function reloadKendoGrid() {
  let grid = $('#SearchGrid').data('kendoGrid');
  grid.dataSource.read();
  grid.refresh();
}

function AssignDefectsPopup() {
  if (!CurrentElement.MaterialId) {
    InfoMessage(Translations["MESSAGE_SelectElement"]);
    return;
  }

  if (!CurrentElement.RawMaterialId) {
    InfoMessage(Translations["MESSAGE_MaterialUnassignInfo"]);
    return;
  }
  OpenInPopupWindow({
    controller: "RawMaterial", method: "AssignDefectsPopup", width: 480, data: { rawMaterialId: CurrentElement.RawMaterialId }
  });
}

//===========

function ShowProductCatalogueDetailsAfterSelect() {
  var productCatalogId = $("#FKProductCatalogueId")[0].value;

  if (productCatalogId !== "") {
    var data = AjaxGetDataHelper(Url("WorkOrder", "GetProductCatalogDetails"), { productCatalogId: productCatalogId });
    $(".product-details").show();
    $("#Thickness").text(data.Thickness);
    $("#Width").text(data.Width);
    $("#Steelgrade").text(data.Steelgrade);
    $("#Shape").text(data.Shape);
  }
  else {
    $(".product-details").hide();
  }
}

function CalculateMaterialsWeight() {
  var materialNumber = $("#MaterialsNumber").val();
  var weight = $("#BilletWeight").val();

  if (!weight) $("#BilletWeight").val(null);

  if (materialNumber && weight) {
    $(".material-weight").show();
    var newWeight = parseFloat(weight * materialNumber);

    if (newWeight) {
      $("#material-weight-value").text(newWeight.toFixed(3));
    } else {
      $("#material-weight-value").text('-');
    }
  }
  else {
    $(".material-weight").hide();
  }
}

function HeatSelected(e) {
  var dataItem = this.dataItem(e.item.index());
  $("#FKHeatId").val(dataItem.HeatId);

  $("#FKSteelgradeId").data("kendoDropDownList").dataSource.read();
}

function filterSteelgrades() {
  return {
    heatId: $("#FKHeatId").val()
  };
}

function disableInputs() {
  var wostatus = $("#EnumWorkOrderStatus").data("kendoDropDownList").value();
  if (wostatus > 1) {
    $("#WorkOrderName").attr("readonly", true);
    $("#TargetOrderWeight").data("kendoNumericTextBox").readonly();
    $("#ToBeCompletedBefore").data("kendoDatePicker").readonly();
    $("#IsTestOrder").attr("readonly", true).attr("hidden", true).click(function () { return false; });
    $("#FKProductCatalogueId").data("kendoDropDownList").readonly();
    $("#FKMaterialCatalogueId").data("kendoDropDownList").readonly();
    $("#Heat").data("kendoAutoComplete").readonly();
    $("#FKSteelgradeId").data("kendoDropDownList").readonly();
    $("#MaterialsNumber").data("kendoNumericTextBox").readonly();
    $("#ExtraLabelInformation").attr("readonly", true);
    $("#NextAggregate").attr("readonly", true);
    $("#OperationCode").attr("readonly", true);
    $("#QualityPolicy").attr("readonly", true);
    $("#FKCustomerId").data("kendoDropDownList").readonly();
  }
  if (wostatus == 3) {
    // TODOIK show alert: only mat number is editable
    $("#MaterialsNumber").data("kendoNumericTextBox").readonly(false);
  }
}

function onAdditionalData() {
  return {
    text: $("#Heat").val()
  };
}
