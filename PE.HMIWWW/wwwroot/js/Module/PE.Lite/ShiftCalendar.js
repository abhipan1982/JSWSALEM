
	function RefreshDataShiftCal() {
    	$("#scheduler").data("kendoScheduler").dataSource.read();
    	$("#scheduler").data("kendoScheduler").refresh();
}

function GenerateShiftDialogPopup() {
  OpenInPopupWindow({
    controller: "ShiftCalendar", method: "GenerateShiftDialog", width: 600, data: {}, afterClose: () => {}
  });
}


/* START: GENERATE SHIFTS FORM */

function handleDateChange() {
  const dateRange = formatDateForCalc($("#From").val(), $("#To").val());
  if (validFormDateRange(dateRange))
    generateShiftDays(dateRange);
}

function initShiftForm() {
  const formData = {};
  const date = new Date();
  date.setDate(date.getDate() + 1);
  formData["list[0].Date"] = date.toISOString();

  AjaxReqestHelperSilentWithoutDataType("ShiftCalendar/GenerateShiftLayoutForms", formData, (res) => { $('#shiftDayFormArr').html(res) });
}

function generateShiftDays(dateRange) {

  const differenceInDays = (dateRange.to.getTime() - dateRange.from.getTime()) / (1000 * 3600 * 24);
  const formData = {};

  for (let i = 0; i <= differenceInDays; i++) {
    let from = new Date(dateRange.from);
    from.setDate(from.getDate() + i + 1);
    formData["list[" + i + "].Date"] = from.toISOString();
  }

  AjaxReqestHelperSilentWithoutDataType("ShiftCalendar/GenerateShiftLayoutForms", formData, (res) => { $('#shiftDayFormArr').html(res) });
}

function formatDateForCalc(from, to) {

  from = from.replaceAll(".", "/").replaceAll("-", "/").replaceAll(" ", "/").split('/');
  to = to.replaceAll(".", "/").replaceAll("-", "/").replaceAll(" ", "/").split('/');

  const dateFormat = Translations["GLOB_ShortDate_FORMAT_Picker"]; // !The same Res as in data pickers
  const formatArr = dateFormat.replaceAll(".", "/").replaceAll("-", "/").replaceAll(" ", "/").split('/');

  const dateRange = {
    from: {},
    to: {}
  }

  formatArr.forEach((el, idx) => {
    if (el.includes('d')) {
      dateRange.from.d = from[idx];
      dateRange.to.d = to[idx];
    }

    if (el.includes('M')) {
      dateRange.from.M = from[idx];
      dateRange.to.M = to[idx];
    }

    if (el.includes('y')) {
      dateRange.from.y = from[idx];
      dateRange.to.y = to[idx];
    }

  })

  return {
    from: new Date(dateRange.from.y, Number(dateRange.from.M) - 1, dateRange.from.d), // the month is 0-indexed
    to: new Date(dateRange.to.y, Number(dateRange.to.M) - 1, dateRange.to.d)
  }
}

function validFormDateRange(dateRange) {

  let isValid = true;
  const errors = [];

  const minDate = new Date();
  minDate.setDate(minDate.getDate() + 1);

  const differenceInDaysFromTo = (dateRange.to.getTime() - dateRange.from.getTime()) / (1000 * 3600 * 24);
  const differenceInDaysFromMin = (minDate.getTime() - dateRange.from.getTime()) / (1000 * 3600 * 24);

  if (differenceInDaysFromMin > 1) {
    errors.push(Translations["ERROR_MinDateTommorow"]);
    isValid = false;
  }

  if (differenceInDaysFromTo > 14) {
    errors.push(Translations["ERROR_TwoWeeksExceeded"]);
    isValid = false;
  }

  if (differenceInDaysFromTo < 0) {
    errors.push(Translations["ERROR_EndBeforeStartDate"]);
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

/* END: GENERATE SHIFTS FORM */


