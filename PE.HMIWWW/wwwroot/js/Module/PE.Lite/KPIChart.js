function getKPIValues(kpiValueId) {
  return AjaxGetDataHelper(Url("KPI", "GetKPIValues"), { kpiValueId: kpiValueId });
}

function beautifyDate(dateString) {
  const date = new Date(dateString);
  return date.getFullYear() + "-" + date.getMonth() + "-" + date.getDay() +
    " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
}

function displayKPIChart(kpiValueId) {
  const data = getKPIValues(kpiValueId);
  const kpiValues = data.KPIOverviews;

  var maxValue = 0;
  var unit = "";
  var enumGaugeDirection = 1;

  const materialsChartData = [];

  if (kpiValues.length > 0) {
    maxValue = kpiValues[0].MaxValue;
    unit = "[" + kpiValues[0].UnitSymbol + "]";
    enumGaugeDirection = kpiValues[0].EnumGaugeDirection;
  }

  for (let i = 0; i < kpiValues.length; i++) {
    materialsChartData.push({
      time: beautifyDate(kpiValues[i].KPITime),
      value: kpiValues[i].KPIValue,
      alarmTo: kpiValues[i].AlarmTo,
      warningTo: kpiValues[i].WarningTo,
      okTo: kpiValues[i].MaxValue,
    });
  }

  createKPIChart(materialsChartData, maxValue, enumGaugeDirection, unit);
}

function createKPIChart(materialData, maxValue, enumGaugeDirection = 1, unit = "") {
  $("#kpi-chart").kendoChart({
    rednerAs: "canvas",
    dataSource: {
      data: materialData
    },
    valueAxis: {
      min: 0,
      max: maxValue,
      title: {
        text: unit
      }
    },
    categoryAxis: {
      labels: {
        rotation: "auto"
      }
    },
    tooltip: {
      visible: true
    },
    series: [
      {
        type: "line",
        field: "value",
        categoryField: "time",
        color: "#0A197C"
      },
      {
        type: "area",
        field: "okTo",
        categoryField: "time",
        color: enumGaugeDirection == 1 ? "#CEFF9A" : "#FFBB88",
        opacity: 1,
        highlight: {
          visible: false,
        },
        tooltip: {
          visible: false,
        }
      },
      { 
        type: "area",
        field: "warningTo",
        categoryField: "time",
        color: "#FBF966",
        opacity: 1,
        highlight: {
          visible: false,
        },
        tooltip: {
          visible: false,
        }
      },
      {
        type: "area",
        field: "alarmTo",
        categoryField: "time",
        color: enumGaugeDirection == 1 ? "#FFBB88" : "#CEFF9A",
        opacity: 1,
        highlight: {
           visible: false,
        },
        tooltip: {
          visible: false,
        }
      }
    ],
    pannable: {
      lock: "y"
    },
    zoomable: {
      mousewheel: {
        lock: "y"
      },
      selection: {
        lock: "y"
      }
    }
  });
}
