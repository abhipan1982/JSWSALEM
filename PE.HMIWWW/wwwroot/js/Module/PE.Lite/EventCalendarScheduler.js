let switchRefreshEventsBtn;
let switchRefreshEventsEl;
let switchRefreshEventsBtnKendo;

let selectedEventTypes = [];
let currentDate;

$(document).ready(() => {
  initRefreshMode();
  handleRefresh();
  initShiftsSelect();
  //  setInterval(function () {
  //    refreshData();
  //}, 10000);

})

function handleRefresh() {
  try {
    SignalrConnection.on("System2HmiRefresh", (refreshData) => {
      let name = refreshData.RefreshKey.split("_")[0];
      let id = refreshData.RefreshKey.split("_")[1];
      if (name == HmiRefreshKeys.Event && id && id != "" && switchRefreshEventsBtn[0].checked && isEventTypeSelected(id)) {
        new Timer(() => refreshDataWithEventType(id), 500);
      }
    });

  }
  catch (err) {
    console.log(err);
  }
}

function initShiftsSelect(){
  $("#select_shift").kendoDropDownList({
    dataSource: [{ id: 0, value: 'Full day' }, { id: 1, value: '7:00 - 15:00' }, { id: 2, value: '15:00 - 23:00' }, { id: 3, value: '23:00 - 7:00' }],
    dataTextField: "value",
    dataValueField: "id",
    select: onSelectChange,
  });
}

function initRefreshMode() {
  switchRefreshEventsEl = $("#refreshSelect");
  switchRefreshEventsBtn = $("#refreshSelectBtn");
  switchRefreshEventsBtnKendo = switchRefreshEventsBtn.kendoSwitch({ checked: true }).data("kendoSwitch");
  switchRefreshEventsBtnKendo.readonly(!switchRefreshEventsBtnKendo.element.attr("readonly"));
  switchRefreshEventsEl.click(() => {
    switchRefreshEventsBtn[0].checked ? setRefreshEvents(false) : setRefreshEvents(true);
  })
}

function setRefreshEvents(isChecked) {
  switchRefreshEventsBtnKendo.check(isChecked);
}


function getColorBasedOnHour(date) {
  var difference = date.getTime() - kendo.date.getDate(date);
  var hours = difference / kendo.date.MS_PER_HOUR;

  if (hours >= 7 && hours < 15) {
    return "#edf6f7";
  } else if (hours >= 15 && hours < 23) {
    return "#fffaf0";
  } else {
    return "#f5f6ff";
  }
}



function onSelectChange(e) {
  let value = e.dataItem.id;
  let scheduler = $("#scheduler").data("kendoScheduler");
  let start;
  let end;

  if (value == 1) {
    start = new Date("2013/6/13 07:00 AM");
    end = new Date("2013/6/13 03:00 PM");
    scheduler.options.workDayStart = start;
    scheduler.options.workDayEnd = end;
    scheduler.options.showWorkHours = true;
  } else if (value == 2) {
    start = new Date("2013/6/13 3:00 PM");
    end = new Date("2013/6/13 11:00 PM");
    scheduler.options.workDayStart = start;
    scheduler.options.workDayEnd = end;
    scheduler.options.showWorkHours = true;
  } else if (value == 3) {
    start = new Date("2013/6/13 11:00 PM");
    end = new Date("2013/6/13 07:00 AM");
    scheduler.options.workDayStart = start;
    scheduler.options.workDayEnd = end;
    scheduler.options.showWorkHours = true;
  } else {
    scheduler.options.showWorkHours = false;
  }

  scheduler.view(scheduler.view().name);
}

$("#scheduler").on("click", ".k-event", function (e) {
  let uid = e.currentTarget.dataset.uid;
  var scheduler = $("#scheduler").data("kendoScheduler");
  var model = scheduler.occurrenceByUid(uid);
  //OpenPopup(model.link, model.linkId)
});



function scheduler_navigate(e) {
  let date = e.date;
  let month = date.getMonth();
  let year = date.getFullYear();

  if (currentDate && currentDate.month != month) {
    if (selectedEventTypes.length) {

      let scheduler = $("#scheduler").data("kendoScheduler");
      scheduler.setDataSource([]);

      selectedEventTypes.forEach((x) => {
        addEventsData(x.EventType, date);
      });
    }
  }
  currentDate = { month: month, year: year };
}

function refreshData() {
  let scheduler = $("#scheduler").data("kendoScheduler");
  let date = scheduler.date();

  if (selectedEventTypes.length) {
    scheduler.setDataSource([]);

    selectedEventTypes.forEach((x) => {
      addEventsData(x.EventType, date);
    });
  }
}

function refreshDataWithEventType(eventTypeId) {
  let scheduler = $("#scheduler").data("kendoScheduler");
  let date = scheduler.date();
  updateEventTypeData(eventTypeId, date);
}

function isEventTypeSelected(eventTypeId) {
  return selectedEventTypes.some((x) => {
    return x.EventType == eventTypeId;
  });
}

function addToSelectedRow(el) {
  selectedEventTypes.push({
    EventType: el.EventType,
    EventColor: el.EventColor,
    EditLink: el.EditLink,
    EventName: el.EventName,
    Editable: el.Editable
  })
}

function deselectEvent(el) {
  selectedEventTypes = selectedEventTypes.filter(item => item.EventType !== el.EventType);
}

function scheduler_databound() {
  if (!$('#calendar-wrap').length) {
    $(function () {
      $("#scheduler").kendoTooltip({
        filter: ".k-event",
        position: "top",
        width: 250,
        content: kendo.template($('#template').html())
      });
    });
  }

  $("#scheduler > table.k-scheduler-layout").wrap("<div id='calendar-wrap' style='max-height: 698px; overflow-y: auto;'></div>");

  let elements = $("#scheduler").find(".k-slot-cell");

  $("#scheduler").find(".k-scheduler-group-cell").each(function () {
    var element = $(this);
    let elementText; //"fileName&eventType"
    let fileName;
    let eventType;
    if (element != null) {
      if (element[0].innerText && element[0].innerText != "") {
        elementText = element[0].innerText.split('&');
        fileName = elementText[0];
        eventType = elementText[1];
        element.html("<img title='" + eventType + "' src='/css/Functions/Small/" + fileName + "'>");
      }
    }
  });

}

function onEventSelect(e) {
  var eventTypeData = this.dataItem(this.select());
  var scheduler = $("#scheduler").data("kendoScheduler");
  let date = scheduler.date();

  var grid = e.sender;
  var selectedRow = grid.select();

  if ($(selectedRow).hasClass('selected-event')) {
    $(selectedRow).removeClass('selected-event');
    $(selectedRow).removeClass('k-state-selected');
    removeEventTypeFromCalendar(eventTypeData.EventType);
    removeEvents(eventTypeData.EventType);
    deselectEvent(eventTypeData);
  } else {
    $(selectedRow).addClass('selected-event');
    addEventTypeToCalendar(eventTypeData)
    addEventsData(eventTypeData.EventType, date);
    addToSelectedRow(eventTypeData);
  }
}

function removeEventTypeFromCalendar(eventTypeId) {
  let scheduler = $("#scheduler").data("kendoScheduler");
  let resource = scheduler.resources[0].dataSource;
  let resourceList = resource._data;
  let eventTypeIdx = -1;

  resourceList.forEach((element, index) => {
    if (element.Value == eventTypeId)
      eventTypeIdx = index;
  })

  if (eventTypeIdx != -1)
    resource.remove(resource.at(eventTypeIdx));

  if (!resourceList.length) {
    resource.add({ Text: "", Value: 0, Color: "" });
  }

  scheduler.view(scheduler.view().name);
}

function addEventTypeToCalendar(eventTypeData) {

  let scheduler = $("#scheduler").data("kendoScheduler");
  let resource = scheduler.resources[0];

  if (resource.dataSource.at(0).Value == 0)
    resource.dataSource.remove(resource.dataSource.at(0)); //remove initial EventType

  resource.dataSource.add({ Text: eventTypeData.EventIcon + "&" + eventTypeData.EventName, Value: eventTypeData.EventType, Color: eventTypeData.EventColor });
  scheduler.view(scheduler.view().name);
}

function removeEvents(eventTypeId) {
  let scheduler = $("#scheduler").data("kendoScheduler");
  let eventsResult = scheduler.dataSource.options.data || [];
  eventsResult = eventsResult.filter(x => x.EventTypeId != eventTypeId);

  scheduler.setDataSource(eventsResult);
}

function updateEventTypeData(eventTypeId, date) {

  let scheduler = $("#scheduler").data("kendoScheduler");
  let data = AjaxGetDataHelper(Url("EventCalendar", "GetEventCalendarSchedulerData"), { eventId: eventTypeId, date: formatDate(date) });
  let currentEvents = scheduler.dataSource.options.data || [];
  let eventsResult = currentEvents.filter(x => x.EventTypeId != eventTypeId);

  data.Data.forEach((el) => {
    eventsResult.push({
      description: el.Description,
      id: el.EventId,
      linkId: el.Id,
      link: el.Link,
      isOngoing: el.IsOngoing,
      assetName: el.AssetName,
      end: el.End,
      endTimezone: el.EndTimezone,
      EventId: el.EventId,
      EventTypeId: el.EventTypeId,
      isAllDay: el.IsAllDay,
      recurrenceException: el.RecurrenceException,
      recurrenceID: el.RecurrenceID,
      recurrenceRule: el.RecurrenceRule,
      start: el.Start,
      startTimezone: el.StartTimezone,
      title: el.Title
    });
  });

  scheduler.setDataSource(eventsResult);
}



function addEventsData(eventTypeId, date) {

  let scheduler = $("#scheduler").data("kendoScheduler");
  let data = AjaxGetDataHelper(Url("EventCalendar", "GetEventCalendarSchedulerData"), { eventId: eventTypeId, date: formatDate(date) });
  let eventsResult = scheduler.dataSource.options.data || [];

  data.Data.forEach((el) => {
    eventsResult.push({
      description: el.Description,
      id: el.EventId,
      linkId: el.Id,
      link: el.Link,
      isOngoing: el.IsOngoing,
      assetName: el.AssetName,
      end: el.End,
      endTimezone: el.EndTimezone,
      EventId: el.EventId,
      EventTypeId: el.EventTypeId,
      isAllDay: el.IsAllDay,
      recurrenceException: el.RecurrenceException,
      recurrenceID: el.RecurrenceID,
      recurrenceRule: el.RecurrenceRule,
      start: el.Start,
      startTimezone: el.StartTimezone,
      title: el.Title
    });
  });

  scheduler.setDataSource(eventsResult);
}

//function change_date() {
//  var scheduler = $("#scheduler").data("kendoScheduler");
//    var options = scheduler.options;
//    scheduler.destroy();
//    options.startTime = new Date("2013/6/13 12:00 AM");
//    options.date = new Date("2013/6/13 12:00 AM");
//    $("#scheduler").empty().kendoScheduler(options);
//}

function scheduler_eventType_remove(eventID) {
  let scheduler = $("#scheduler").data("kendoScheduler");
  let resource = scheduler.resources[0].dataSource;
  let resourceList = resource._data;

  let eventToRemoveIndex = -1;
  resourceList.forEach((element, index) => {
    if (element.Value == eventID) {
      eventToRemoveIndex = index;
    }
  })

  if (eventToRemoveIndex != -1) {
    resource.remove(resource.at(eventToRemoveIndex));
  }
  scheduler.view(scheduler.view().name);
}

function readData() {
  var scheduler = $("#scheduler").data("kendoScheduler");
  scheduler.dataSource.read();
  scheduler.view(scheduler.view().name)
}

function changeData() {
  var scheduler = $("#scheduler").data("kendoScheduler");
  //var dataSource = new kendo.data.SchedulerDataSource({
  //  data: [
  //    {
  //      description: "Desc",
  //      end: new Date("2022/1/12 09:00 AM"),
  //      endTimezone: null,
  //      EventId: 24217,
  //      EventTypeId: 2,
  //      isAllDay: false,
  //      recurrenceException: null,
  //      recurrenceID: null,
  //      recurrenceRule: null,
  //      start: new Date("2022/1/12 08:00 AM"),
  //      startTimezone: null,
  //      title: "Meeting with customers"
  //    }
  //  ]
  //});
  scheduler.dataSource.add({
    description: "Desc",
    end: new Date("2022/1/12 09:00 AM"),
    endTimezone: null,
    EventId: 24217,
    EventTypeId: 2,
    isAllDay: false,
    recurrenceException: null,
    recurrenceID: null,
    recurrenceRule: null,
    start: new Date("2022/1/12 08:00 AM"),
    startTimezone: null,
    title: "Meeting with customers"
  });
  scheduler.setDataSource(dataSource);
}


function formatDate(date) {
  var d = new Date(date);
  var hour = d.getHours() < 10 ? '0' + d.getHours() : d.getHours();
  var year = d.getFullYear();
  var min = d.getMinutes() < 10 ? '0' + d.getMinutes() : d.getMinutes();
  var day = d.getDate() < 10 ? '0' + d.getDate() : d.getDate();
  var month = d.getMonth() < 9 ? '0' + (d.getMonth() + 1) : d.getMonth() + 1;

  return year + '-' + month + '-' + day + " " + hour + ':' + min;// + ' ' + day + "-" + month + '-' + year;
}

  //function OpenPopup(link, id) {
  //var split = link.split("/");
  //  OpenInPopupWindow({
  //    controller: split[2],
  //    method: split[4],
  //    width: 480,
  //    data: { id: id },
  //    //afterClose: RefreshTable
  //  });
  //}


function showHideCategories() {
  var button = $('.show-hide-categories');
  var grid = $("#filters").data("kendoGrid");
  var button_array = $('.arrow-categories');
  if (button.hasClass('off')) {
    $(".filter-pane").stop().animate({ width: '20%' }, 300);
    $("#event_calendar").stop().animate({ width: '80%' }, 300, () => {
      var scheduler = $("#scheduler").data("kendoScheduler");
      scheduler.view(scheduler.view().name);
    });

  }
  if (button.hasClass('on')) {
    $(".filter-pane").stop().animate({ width: '5%' }, 300);
    $("#event_calendar").stop().animate({ width: '95%' }, 300, () => {
      var scheduler = $("#scheduler").data("kendoScheduler");
      scheduler.view(scheduler.view().name);
    });

  }
  button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
  button.toggleClass('off on');
}

function hideCategories() {
  if (!$(".grid-filter").is(":animated")) {
    var button = $('.show-hide-categories');
    if (button.hasClass('on')) {
      var grid = $("#filters").data("kendoGrid");
      var button_array = $('.arrow-categories');
      button_array.toggleClass('right left');
      button.toggleClass('on off');
      columns.forEach(function (element) {
        grid.hideColumn(element);
      });
      $('.grid-filter').toggleClass('col-11 col-3', 650, function () {
        $('.element-details').show(650);
      });
    }
  }
}
