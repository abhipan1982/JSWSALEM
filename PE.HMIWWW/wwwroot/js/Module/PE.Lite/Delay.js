RegisterMethod(HmiRefreshKeys.Delay, reloadKendoGrid);

function DelayEditPopup(id) {
  OpenInPopupWindow({
    controller: "Delays", method: "DelayEditPopup", width: 522, data: { id: id }, afterClose: reloadKendoGrid
  });
}

function DelayCreatePopup() {
  OpenInPopupWindow({
    controller: "Delays", method: "DelayCreatePopup", width: 600, data: {}, afterClose: reloadKendoGrid
  });
}

function DelayDividePopup(id) {
  OpenInPopupWindow({
    controller: "Delays", method: "DelayDividePopup", width: 600, data: { delayId: id }, afterClose: reloadKendoGrid
  });
}

//delays summary

$(document).ready(function () {
  onInit();
});

function onInit() {
  onSelectDateListener();
  getDelaysSummaryForToday();
  $('#today-btn').addClass("selected_button");
}

function onSelectDateListener() {
  const chartPanelButtons = document.querySelectorAll('.chartPanel_button');
  chartPanelButtons.forEach((el) => {
    el.addEventListener('click', (e) => {
      chartPanelButtons.forEach((el) => {
        $(el).removeClass("selected_button");
      });
      $(e.target).addClass("selected_button");
    });
  })
}

function getDateRange() {
  return {
    startDateTime: DATE_RANGE.dateStart.toISOString(),
    endDateTime: DATE_RANGE.dateEnd.toISOString()
  }
}

const DATE_RANGE = {
  dateStart: new Date(),
  dateEnd: new Date(),
};

const COLORS = ['#c13a48', '#e04847', '#f99b37', '#fbc62c', '#c9d930', '#5479c0'];

const DURATION_CHART = {
  InOperation: '#1b7dab',
  Delays: '#fa9c23',
  Maintenance: '#4a647d'
};

const MICRO_DURATION_CHART = {
  InOperation: '#1b7dab',
  MicroDelays: '#fac823'
};


function getDelaysDurationPercent(delaysDurationSummary, totalTime) {
  const delaysDurationPercent = {};
  for (const key in delaysDurationSummary) {
    delaysDurationPercent[key] = calculatePercentageOfNumber(delaysDurationSummary[key], totalTime);
  }
  return delaysDurationPercent;
}

function getWorktimeDurationHMS(partTimesSec) {
  const worktimeDurationHMS = {};
  for (const key in partTimesSec) {
    worktimeDurationHMS[key] = calculateSecondsToHMS(partTimesSec[key]);
  }
  return worktimeDurationHMS;
}

function getDelaysCategoriesPercent(delaysCategoriesSummary, delaysTotalNumber) {
  const delaysCategoriesPercent = {};
  for (const key in delaysCategoriesSummary) {
    delaysCategoriesPercent[key] = calculatePercentageOfNumber(delaysCategoriesSummary[key], delaysTotalNumber);
  }
  return delaysCategoriesPercent;
}

function calculatePercentageOfNumber(partNumber, totalNumber) {
  if (totalNumber == 0) {
    return 0;
  }

  return ((partNumber / totalNumber) * 100).toFixed(2);
}


function setYesterdayDateRange() {
  DATE_RANGE.dateStart = new Date();
  DATE_RANGE.dateStart.setDate(new Date().getDate() - 1);
  DATE_RANGE.dateStart.setHours(0, 0, 0, 0);
  DATE_RANGE.dateEnd = new Date();
  DATE_RANGE.dateEnd.setHours(0, 0, 0, 0);
  setDatePickerFromRange();

  let dateDisplayedFormat = new Date();
  dateDisplayedFormat.setDate(new Date().getDate() - 1);
  dateDisplayedFormat = dateDisplayedFormat.toLocaleDateString();

  $("#data-description").text(dateDisplayedFormat);
}

function setTodayDateRange() {
  DATE_RANGE.dateStart = new Date();
  DATE_RANGE.dateStart.setHours(0, 0, 0, 0);
  DATE_RANGE.dateEnd = new Date();
  DATE_RANGE.dateEnd.setDate(new Date().getDate() + 1);
  DATE_RANGE.dateEnd.setHours(0, 0, 0, 0);
  setDatePickerFromRange();

  let dateDisplayedFormat = new Date();
  dateDisplayedFormat = dateDisplayedFormat.toLocaleDateString();

  $("#data-description").text(dateDisplayedFormat);
}

function setLastWeekDateRange() {
  DATE_RANGE.dateStart = new Date();
  DATE_RANGE.dateStart.setDate(new Date().getDate() - 7);
  DATE_RANGE.dateStart.setHours(0, 0, 0, 0);
  DATE_RANGE.dateEnd = new Date();
  DATE_RANGE.dateEnd.setDate(new Date().getDate() + 1);
  DATE_RANGE.dateEnd.setHours(0, 0, 0, 0);
  setDatePickerFromRange();

  let dateStartDisplayedFormat = new Date();
  dateStartDisplayedFormat.setDate(new Date().getDate() - 6);
  dateStartDisplayedFormat = dateStartDisplayedFormat.toLocaleDateString();

  let dateEndDisplayedFormat = new Date();
  dateEndDisplayedFormat = dateEndDisplayedFormat.toLocaleDateString();

  $("#data-description").text(dateStartDisplayedFormat + "  -  " + dateEndDisplayedFormat);
}

function getDelaysSummaryForToday() {
  setTodayDateRange();
  getDelaysData();
}

function getDelaysSummaryForYesterday() {
  setYesterdayDateRange();
  getDelaysData();
}

function getDelaysSummaryForRange() {
  setRangeFromDatePicker();
  if (DATE_RANGE.dateStart < DATE_RANGE.dateEnd) {
    let dateStartDisplayedFormat = DATE_RANGE.dateStart.toLocaleString();
    let dateEndDisplayedFormat = DATE_RANGE.dateEnd.toLocaleString();
    $("#data-description").text(dateStartDisplayedFormat + "  -  " + dateEndDisplayedFormat);

    getDelaysData();
  } else {
    displayDateRangeError(Translations["ERROR_InvalidRange"] + ":");
  }
}

function displayDateRangeError(message) {
  $('.dateRangeError').text(message);
}

function getDelaysSummaryForLastWeek() {
  setLastWeekDateRange();
  $.when(getDelaysData()).done();
}

function getDelaysData() {
  displayDateRangeError('');
  let delaysSummary = getDelaysSummaryRequest();
  let delaysDurationSum = delaysSummary[0].Delays + delaysSummary[0].Maintenance + delaysSummary[0].InOperation;
  if (delaysDurationSum) {
    $(".nodata-message").empty();
    reloadKendoGrid();
    reloadCharts(delaysSummary);
  } else {
    $("#categoriesChart").empty();
    $("#durationChart").empty();
    $(".nodata-message").text(Translations["MESSAGE_NoResults"]);
    reloadKendoGrid();
  }
}


function setRangeFromDatePicker() {
  let datetimepickerFrom = $("#datetimepicker-from").data("kendoDateTimePicker");
  let datetimepickerFromValue = datetimepickerFrom.value();

  let datetimepickerTo = $("#datetimepicker-to").data("kendoDateTimePicker");
  let datetimepickerToValue = datetimepickerTo.value();

  DATE_RANGE.dateStart = datetimepickerFromValue;
  DATE_RANGE.dateEnd = datetimepickerToValue;
}

function setDatePickerFromRange() {
  let datetimepickerFrom = $("#datetimepicker-from").data("kendoDateTimePicker");
  datetimepickerFrom.value(DATE_RANGE.dateStart);

  let datetimepickerTo = $("#datetimepicker-to").data("kendoDateTimePicker");
  datetimepickerTo.value(DATE_RANGE.dateEnd);
}

function getDelaysSummaryRequest() {
  $('.charts').addClass('loading-overlay');
  let dataToSend = {
    startDateTime: DATE_RANGE.dateStart.toISOString(),
    endDateTime: DATE_RANGE.dateEnd.toISOString(),
  };
  let targetUrl = '/Delays/GetDelaysSummary';
  let delaysSummary = AjaxGetDataHelper(targetUrl, dataToSend);

  setTimeout(function () {
    $('.charts').removeClass('loading-overlay');
  }, 400);

  return delaysSummary;
}

function reloadCharts(delaysSummary) {
  let $durationChart = $("#durationChart");
  let $durationMicroChart = $("#durationMicroChart");
  let $categoriesChart = $("#categoriesChart");

  let worktimeDurationSec = delaysSummary[0];
  let delaysCategoriesDurationSec = delaysSummary[1];
  let delaysCategoriesId = delaysSummary[2];
  let worktimeMicroDurationSec = delaysSummary[3];

  let delaysCategoriesCount = Object.keys(delaysCategoriesId).length;

  let totalDuration = worktimeDurationSec.InOperation + worktimeDurationSec.Delays + worktimeDurationSec.Maintenance;
  let delaysTotalDuration = worktimeDurationSec.Delays + worktimeDurationSec.Maintenance;
  let delaysDurationPercent = getDelaysDurationPercent(worktimeDurationSec, totalDuration);
  let totalMicroDuration = worktimeMicroDurationSec.InOperation + worktimeMicroDurationSec.MicroDelays;
  let delaysMicroDurationPercent = getDelaysDurationPercent(worktimeMicroDurationSec, totalMicroDuration);
  let categoriesSumHMS = getWorktimeDurationHMS(delaysCategoriesDurationSec);
  let worktimeSumHMS = getWorktimeDurationHMS(worktimeDurationSec);
  let worktimeMicroSumHMS = getWorktimeDurationHMS(worktimeMicroDurationSec);
  let delaysCategoriesPercent = getDelaysCategoriesPercent(delaysCategoriesDurationSec, delaysTotalDuration);

  let durationSummary = [];
  let durationMicroSummary = [];
  let categoriesSummary = [];

  let delaysEmpty = Object.values(delaysDurationPercent).every((val, i, arr) => val == 0);
  let delaysMicroEmpty = Object.values(delaysMicroDurationPercent).every((val, i, arr) => val == 0);

  for (const key in delaysDurationPercent) {
    if (delaysEmpty || delaysDurationPercent[key] > 0) {
      let ob = {
        category: Translations["NAME_" + key + ""] + ' - ' + worktimeSumHMS[key],
        value: delaysDurationPercent[key],
        color: DURATION_CHART[key]
      };
      durationSummary.push(ob);
    }
  }
  for (const key in delaysMicroDurationPercent) {
    if (delaysMicroEmpty || delaysMicroDurationPercent[key] > 0) {
      let ob = {
        category: Translations["NAME_" + key + ""] + ' - ' + worktimeMicroSumHMS[key],
        value: delaysMicroDurationPercent[key],
        color: MICRO_DURATION_CHART[key]
      };
      durationMicroSummary.push(ob);
    }
  }

  for (const key in delaysCategoriesPercent) {
    let ob = {
      category: key + ' - ' + categoriesSumHMS[key],
      value: delaysCategoriesPercent[key],
      color: GetColorModulo16(delaysCategoriesId[key])
    };
    categoriesSummary.push(ob);
  }

  if (delaysCategoriesCount > 13) {
    $("#categoriesChart").css("width", "830px")
  } else {
    $("#categoriesChart").css("width", "560px")
  }
  createDelaysChart($durationChart, durationSummary, Translations["NAME_DurationSummary"]);
  createDelaysChart($durationMicroChart, durationMicroSummary, Translations["NAME_MicroDurationSummary"]);
  createDelaysChart($categoriesChart, categoriesSummary, Translations["NAME_CategoriesSummary"]);
}

function createDelaysChart($el, data, title) {
  $el.kendoChart({
    title: {
      position: "bottom",
      text: title,
      color: "#6d8292"
    },
    legend: {
      visible: true,
      margin: {
        right: 5
      },
      labels: {
        margin: {
          top: 2,
          bottom: 2,
          right: 20
        }
      }
    },
    chartArea: {
      background: ""
    },
    seriesDefaults: {
      labels: {
        visible: false,
        background: "transparent",
        template: "#= category # \n #= value#%"
      }
    },
    series: [{
      type: "pie",
      padding: 5,
      startAngle: 15,
      overlay: {
        gradient: "none"
      },
      data: data
    }],
    tooltip: {
      visible: true,
      template: "#= category #",
      //template: "#= category # \n #= value#%"
      //format: "{0}%"
    }
  });
}

function reloadKendoGrid() {
  let gridDelaysPlanned = $('#DelayPlannedList').data('kendoGrid');
  let gridDelaysUnplanned = $('#DelayUnplannedList').data('kendoGrid');
  let gridDelaysActive = $('#DelayActive').data('kendoGrid');

  if (gridDelaysPlanned) {
    gridDelaysPlanned.dataSource.read();
    gridDelaysPlanned.refresh();
  }
  if (gridDelaysUnplanned) {
    gridDelaysUnplanned.dataSource.read();
    gridDelaysUnplanned.refresh();
  }
  if (gridDelaysActive) {
    gridDelaysActive.dataSource.read();
    gridDelaysActive.refresh();
  }
}

function calculateSecondsToHMS(durationSeconds) {
  let hours = 0;
  let minutes = 0;
  let seconds = 0;

  while (durationSeconds >= 3600) {
    hours++;
    durationSeconds = durationSeconds - 3600;
  }
  while (durationSeconds >= 60) {
    minutes++;
    durationSeconds = durationSeconds - 60;
  }

  seconds = durationSeconds;

  return (prependZero(hours) + ':' + prependZero(minutes) + ':' + prependZero(seconds));
}

function prependZero(number) {
  if (number < 10)
    return "0" + number;
  else
    return number;
}
