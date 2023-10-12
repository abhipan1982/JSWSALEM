$(function () {
  $("#configurator").hide();
  $("#pivotgrid").css('width', '100%');
  //let sumIcons = $("#configurator").find('.k-i-sum');
  //sumIcons.each(function (index) {
  //  $(this).parent().html('<span class="k-icon k-i-sum"></span>' + Translations["NAME_Values"]);
  //})
});

var collapsed = {
  columns: [],
  rows: []
};

const COLORS = ['#c13a48', '#e04847', '#f99b37', '#fbc62c', '#c9d930', '#5479c0'];

let rejectedMeasurements = [];

function collapseMember(e) {
  var axis = collapsed[e.axis];
  var path = e.path;

  if (indexOfPath(path, axis) === -1) {
    axis.push(path);
  }
}

function expandMember(e) {
  var axis = collapsed[e.axis];
  var index = indexOfPath(e.path, axis);

  if (index !== -1) {
    axis.splice(index, 1);
  }
}

$(document).ready(function () {
  let grid = $('#pivotgrid').data('kendoPivotGrid');
  grid.bind("collapseMember", collapseMember);
  grid.bind("expandMember", expandMember);
})

//function flatten the tree of tuples that datasource returns
function flattenTree(tuples) {
  tuples = tuples.slice();
  var result = [];
  var tuple = tuples.shift();
  var idx, length, spliceIndex, children, member;

  while (tuple) {
    //required for multiple measures
    if (tuple.dataIndex !== undefined) {
      result.push(tuple);
    }

    spliceIndex = 0;
    for (idx = 0, length = tuple.members.length; idx < length; idx++) {
      member = tuple.members[idx];
      children = member.children;
      if (member.measure) {
        [].splice.apply(tuples, [0, 0].concat(expandMeasures(children, tuple)));
      } else {
        [].splice.apply(tuples, [spliceIndex, 0].concat(children));
      }
      spliceIndex += children.length;
    }

    tuple = tuples.shift();
  }

  return result;
}

function clone(tuple, dataIndex) {
  var members = tuple.members.slice();

  return {
    dataIndex: dataIndex,
    members: $.map(members, function (m) {
      return $.extend({}, m, { children: [] });
    })
  };
}

function replaceLastMember(tuple, member) {
  tuple.members[tuple.members.length - 1] = member;
  return tuple;
}

function expandMeasures(measures, tuple) {
  return $.map(measures, function (measure) {
    return replaceLastMember(clone(tuple, measure.dataIndex), measure);
  });
}

//Check whether the tuple has been collapsed
function isCollapsed(tuple, collapsed) {
  var members = tuple.members;

  for (var idx = 0, length = collapsed.length; idx < length; idx++) {
    var collapsedPath = collapsed[idx];
    if (indexOfPath(fullPath(members, collapsedPath.length - 1), [collapsedPath]) !== -1) {
      return true;
    }
  }

  return false;
}

//Work with tuple names/captions
function indexOfPath(path, paths) {
  var path = path.join(",");

  for (var idx = 0; idx < paths.length; idx++) {
    if (paths[idx].join(",") === path) {
      return idx;
    }
  }
  return -1;
}

function fullPath(members, idx) {
  var path = [];
  var parentName = members[idx].parentName;
  idx -= 1;

  while (idx > -1) {
    path.push(members[idx].name);
    idx -= 1;
  }

  path.push(parentName);
  return path;
}

function memberCaption(member) {
  if (member.levelName == "MEASURES") {
    return member.name;
  }
  if (member.caption == "All") {
    let splited = member.levelName.split("&");
    let res = '';
    for (let i = 0; i < splited.length; i++) {
      if (Translations["NAME_" + splited[i] + ""]) {
        res += Translations["NAME_" + splited[i] + ""] + "-";
      } else {
        res += splited[i];
      }
    }
    return res;
  }
  return member.caption
}

function extractCaption(members) {
  if (members.length === 1 && members[0].caption == "All") {
    return null;
  }
  return $.map(members, memberCaption).join(" - ");
}

function fullPathCaptionName(members, dLength, idx) {
  for (let i = 0; i < members.length; i++) {
    //if (members[i].caption == "All")
    //  return null;
    if (members[i].levelName == "MEASURES") {
      if (rejectedMeasurements.includes(members[i].name)) {
        return null;
      }
    }
  }

  let result = extractCaption(members.slice(0, idx + 1));

  if (result == null) {
    return null;
  }

  let measureName = extractCaption(members.slice(dLength, members.mLength));

  if (measureName) {
    result += " - " + measureName;
  }

  return result;
}

function createMeasurementsCheckbox() {
  const checkboxList = $('#checkboxMeasurementsList');
  const grid = $('#pivotgrid').data('kendoPivotGrid');
  const filters = grid.measureFields[0].children;

  const measurementTitle = "<div class='label'>" + Translations["NAME_Measures"] + ": </div>"

  if (filters.length > 1) {
    checkboxList.append(measurementTitle);
    for (let i = 0; i < filters.length; i++) {
      let measName = filters[i].innerText;
      let checkboxEl = '<input onclick="onMeasurementCheck(this)" type="checkbox" class="mt-1 ml-3 mr-1" id="' + measName + '" name="' + measName + '" value="' + measName + '" checked><label for="' + measName + '">' + measName + '</label><br>'
      checkboxList.append(checkboxEl);
    }
  }
}

function onMeasurementCheck(el) {
  if (!el.checked) {
    rejectedMeasurements.push(el.value);
  } else {
    rejectedMeasurements = rejectedMeasurements.filter(item => item != el.value);
  }
  initChart(convertData());
}

//the main function that convert PivotDataSource data into understandable for the Chart widget format
function convertData() {

  let grid = $('#pivotgrid').data('kendoPivotGrid');

  dataSource = grid.dataSource;
  var columnDimensionsLength = dataSource.columns().length;
  var rowDimensionsLength = dataSource.rows().length;

  var columnTuples = flattenTree(dataSource.axes().columns.tuples || [], collapsed.columns);
  var rowTuples = flattenTree(dataSource.axes().rows.tuples || [], collapsed.rows);
  var rowTuple, columnTuple;
  var idx = 0;
  var result = { data: [], title: '' };

  let filters = grid.measureFields[0].children;
  if (filters.length == 1) {
    result.title = filters[0].innerText;
  }

  var columnsLength = columnTuples.length;

  for (var i = 0; i < rowTuples.length; i++) {
    rowTuple = rowTuples[i];

    if (!isCollapsed(rowTuple, collapsed.rows)) {
      for (var j = 0; j < columnsLength; j++) {
        columnTuple = columnTuples[j];

        var data = dataSource.data();
        if (!isCollapsed(columnTuple, collapsed.columns)) {
          if (idx > columnsLength && idx % columnsLength !== 0) {
            for (var ri = 0; ri < rowTuple.members.length; ri++) {
              for (var ci = 0; ci < columnTuple.members.length; ci++) {
                //do not add root tuple members, e.g. [CY 2005, All Employees]
                //Add only children
                if (!columnTuple.members[ci].parentName || !rowTuple.members[ri].parentName) {
                  continue;
                }
                let column = fullPathCaptionName(columnTuple.members, columnDimensionsLength, ci);
                let row = fullPathCaptionName(rowTuple.members, rowDimensionsLength, ri);

                if (column != null && row != null) {
                  result.data.push({
                    measure: Number(data[idx].value),
                    column: column,
                    row: row,
                  });
                }
              }
            }
          }
        }
        idx += 1;

      }
    }
  }
  collapsed = {
    columns: [],
    rows: []
  };

  return result;
}


function initChart(data) {

  var chart = $("#chart").data("kendoChart");

  if (!chart) {
    $("#chart").kendoChart({
      tooltip: {
        visible: true,
        //template: "#= series.name # - #= kendo.toString(category) #: #= value #"
        template: "#= series.name #: <b> #= kendo.format('{0:N3}', value) # </b>"
      },
      title: {
        text: data.title
      },
      dataSource: {
        data: data.data,
        group: "row"
      },
      seriesColors: [COLORS[1], COLORS[2], COLORS[4], COLORS[5], COLORS[3], COLORS[0]],
      series: [{
        type: "column",
        overlay: {
          gradient: false,
        },
        field: "measure",
        name: "#= group.value #",
        categoryField: "column"
      }],
      legend: {
        position: "bottom"
      },
      categoryAxis: {
        labels: {
          rotation: 310
        },
      },
      valueAxis: {
        labels: {
          format: "{0}"
        }
      },
      dataBound: function (e) {
        var categoryAxis = e.sender.options.categoryAxis;

        if (categoryAxis && categoryAxis.categories) {
          categoryAxis.categories.sort();
        }
      }
    });
  } else {
    chart.dataSource.data(data.data);
  }
}

var isExpandFiltersBtnAdded = false;

function replaceColumnsNames(e) {

  //var fields = e.sender.columnFields.add(this.rowFields).add(this.measureFields);
  const settings = $(".k-pivot-configurator-settings");
  settings.find(".k-header")
    .each(function (_, item) {
      item = $(item);

      const text = item.data("name");
      const translation = Translations["NAME_" + text + ""];

      if (translation) {
        item.contents().eq(0).replaceWith(translation);
      } else {
        //console.error('Missing translation for ' + text + ' in _JsTranslations.cshtml file.')
      }
    });

  const columns = $(".k-settings-columns");
  columns.find(".k-button")
    .each(function (_, item) {
      item = $(item);

      const text = item.data("name");
      const translation = Translations["NAME_" + text + ""];

      if (translation) {
        item.contents().eq(0).replaceWith(translation);
      } else {
        //console.error('Missing translations for ' + text + ' in _JsTranslations.cshtml file.')
      }
    });

  const rows = $(".k-settings-rows");
  rows.find(".k-button")
    .each(function (_, item) {
      item = $(item);

      const text = item.data("name");
      const translation = Translations["NAME_" + text + ""];

      if (translation) {
        item.contents().eq(0).replaceWith(translation);
      } else {
        //console.error('Missing translations for ' + text + ' in _JsTranslations.cshtml file.')
      }
    });

}

$(function () {
  $("#exportPDF").click(function () {
    $("#pivotgrid").getKendoPivotGrid().saveAsPDF();
  });
  $("#exportExcel").click(function () {
    $("#pivotgrid").getKendoPivotGrid().saveAsExcel();
  });
});

function openChart() {
  rejectedMeasurements = [];
  let popupTitle = Translations["NAME_DataAnalysis"];
  openSlideScreen('DataAnalysis', 'DataChartView', null, popupTitle);
}

function filtersExpand() {
  var button_arrow = $('#conf-arrow');
  if ($("#configurator").css("display") == 'none') {
    $("#pivotgrid").css('width', '81.5%');
    $("#configurator").show();
    let sumIcons = $("#configurator").find('.k-i-sum');
    sumIcons.each(function (index) {
      $(this).parent().html('<span class="k-icon k-i-sum"></span>' + Translations["NAME_Values"]);
    })
  } else {
    $("#pivotgrid").css('width', '100%');
    $("#configurator").hide();
  }
  button_arrow.toggleClass('k-i-arrow-60-right k-i-arrow-45-down-right');
}

//Manual measures calculation:

function Duration_DataSum(value, state, context) {
  let dataItem = context.dataItem;
  if (!dataItem.DelayDuration) return;
  let DelayDuration = dataItem.DelayDuration
  state.DelayDuration = (state.DelayDuration || 0) + DelayDuration;
}

function SecondsToHMS(state) {
  if (state.DelayDuration == 0) {
    return 0;
  }
  let durationSeconds = Math.floor(state.DelayDuration);
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


//function MaterialsWeight_DataSum(value, state, context) {
//  let dataItem = context.dataItem;
//  if (!dataItem.MaterialsWeight) return;
//  let MaterialsWeight = parseFloat(dataItem.MaterialsWeight.toPrecision(8));
//  state.MaterialsWeight = parseFloat(((state.MaterialsWeight || 0) + MaterialsWeight).toPrecision(10));
//}

//function MaterialWeight_Prec(state) {
//  if (state.MaterialsWeight == 0) {
//    return 0;
//  } else {
//    return state.MaterialsWeight;
//  }
//}