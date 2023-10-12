var columns = ["FeatureUnitSymbol", "FeatureCode", "AssetName", "AssetCode"];
var button_array = $('.arrow-categories');

let CurrentElement;

function onElementSelect(e) {
    $('#MeasurementDetails').addClass('loading-overlay');
    let grid = e.sender;
    let selectedRow = grid.select();
    let selectedItem = grid.dataItem(selectedRow);
    let dataToSend = {
        featureId: selectedItem.FeatureId
    } ;

    CurrentElement = {
        featureId: selectedItem.FeatureId
    };

    let url = "/ConsumptionMonitoring/GetFeatureDetails";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
    $('#MeasurementDetails').removeClass('loading-overlay');
    $('#MeasurementDetails').html(partialView);
}

function selectRow() {
    const grid = $('#FeaturesSearchGrid').data("kendoGrid");
    let gridData = grid.dataSource.view();
    let id = getUrlParameter('featureId');
    for (let i = 0; i < gridData.length; i++) {
        let currentItem = gridData[i];
        if (currentItem.ProductId === id) {
            let currentRow = grid.table.find("tr[data-uid='" + currentItem.uid + "']");
            grid.select(currentRow);
            break;
        }
    }
}


//START: ConsumptionMonitoring details

function initData() {
  const monthFrom = new Date();
  monthFrom.setMonth(monthFrom.getMonth() - 1);
  const monthTo = new Date();
  sendMeasurementRequest(monthFrom.toISOString(), monthTo.toISOString(), featureId, handleData);
}

function sendMeasurementRequest(from, to, id, onSuccess) {
  const dataToSend = {
    featureId: id,
    dateFrom: from,
    dateTo: to,
  }

  AjaxReqestHelperSilent(Url("ConsumptionMonitoring", "GetMeasurementData"), dataToSend, onSuccess);
}

function onDateRangeChange() {
  const datepickerTo = $("#datetimepickerTo").data("kendoDateTimePicker").value();
  const datepickerFrom = $("#datetimepickerFrom").data("kendoDateTimePicker").value();

  const dateRange = {
    from: new Date(datepickerFrom),
    to: new Date(datepickerTo)
  }

  if (validFormDateRange(dateRange))
    sendMeasurementRequest(kendo.toString(datepickerFrom, 'F'), kendo.toString(datepickerTo, 'F'), featureId, handleData);
}

function validFormDateRange(dateRange) {

  let isValid = true;
  const errors = [];
  const differenceInDaysFromTo = (dateRange.to.getTime() - dateRange.from.getTime()) / (1000 * 3600 * 24);

  if (differenceInDaysFromTo >= 32) {
    errors.push("Date range is too long");
    isValid = false;
  }

  if (differenceInDaysFromTo < 0) {
    errors.push("Start date should be before end date");
    isValid = false;
  }

  displayError(errors);
  return isValid;
}

function displayError(messages) {
  const errorContainer = $("#date_error");
  errorContainer.html("");
  messages.forEach((msg) => {
    errorContainer.append("<div>" + msg + "</div>")
  })
}

//END: ConsumptionMonitoring details
