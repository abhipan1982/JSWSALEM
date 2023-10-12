const legendDiv = $("#legenddiv");
const legendIcon = $("#chart_legend");
let chart;
let root = am5.Root.new("chartdiv");
let legendRoot = am5.Root.new("legenddiv");
let legend;
let xAxis;

legendIcon.click((e) => {
  toggleChartLegend();
})

//root.setThemes([
//  am5themes_Animated.new(root)
//]);
//
//legendRoot.setThemes([
//  am5themes_Animated.new(legendRoot),
//  am5xy.DefaultTheme.new(legendRoot)
//]);

function groupDataForManyFeatures(chartData) {
  selectedFeatureIds.forEach((id) => {
    let featureMeasurements = chartData.Measurements.filter(m => m.Feature.FeatureId == id);
    featureMeasurements.forEach((el) => {
      let samples = [];
      let yRenderer = am5xy.AxisRendererY.new(root, {});
      let yaxis = createAxis(yRenderer);
      

      el.Measurement.Samples.forEach((x) => {
        //samples.push({ valueX: x.date, valueY: x.value });
        let valueX = el.Feature.IsLengthRelated ? x.Length : new Date(x.Date);
        samples.push({ valueX: valueX, valueY: x.Value });
      });

      createSeries(yaxis, el.Feature.FeatureName + ' | ' + el.Feature.Unit, samples, yRenderer);
    })
  });
  setLegend();
}

function toggleChartLegend() {
  if (legendDiv.is(":hidden")) {
    legendDiv.show();
  } else {
    legendDiv.hide();
  }
}

function setLegend() {
  legend.itemContainers.template.states.create("hover", {});

  legend.itemContainers.template.events.on("pointerover", function (e) {
    e.target.dataItem.dataContext.hover();
  });
  legend.itemContainers.template.events.on("pointerout", function (e) {
    e.target.dataItem.dataContext.unhover();
  });

  legend.data.setAll(chart.series.values);
}

function handleData(data) {
  createChart();
  groupDataForManyFeatures(data);
  chart.appear(1000, 100);
}

function createRange(value, color, yAxis) {
  var rangeDataItem = yAxis.makeDataItem({
    value: value,
    endValue: null
  });

  var range = yAxis.createAxisRange(rangeDataItem);

  rangeDataItem.get("label").setAll({
    fill: am5.color(0xffffff),
    text: value,
    background: am5.RoundedRectangle.new(root, {
      fill: color
    })
  });

  rangeDataItem.get("grid").setAll({
    stroke: color,
    strokeOpacity: 1,
    location: 1
  });
}

function destroyChart() {
  if (chart)
    chart.dispose();

  if (legend)
    legend.dispose();
}


function createChart() {
  destroyChart();

  chart = root.container.children.push(
    am5xy.XYChart.new(root, {
      layout: root.verticalLayout,
      focusable: true,
      panX: true,
      panY: true,
      wheelX: "panX",
      wheelY: "zoomX",
      pinchZoomX: true
    })
  );

  legend = legendRoot.container.children.push(
    am5.Legend.new(legendRoot, {
      centerY: am5.percent(50),
      y: am5.percent(50),
      height: am5.percent(100),
      verticalScrollbar: am5.Scrollbar.new(legendRoot, {
        orientation: "vertical"
      })
    })
  );

  //const featureType = { lengthRelated: false };
  //if (featureType.lengthRelated === false) {
  xAxis = chart.xAxes.push(
    am5xy.DateAxis.new(root, {
      maxDeviation: 0,
      groupData: true,
      baseInterval: {
        timeUnit: "millisecond",
        count: 1
      },
      renderer: am5xy.AxisRendererX.new(root, {}),
      tooltip: am5.Tooltip.new(root, {}),
    })
  );
  //xAxis.dateFormats.setKey("day", "MMMM dt");


  var easing = am5.ease.linear;
  chart.get("colors").set("step", 3);

  var cursor = chart.set("cursor", am5xy.XYCursor.new(root, {
    xAxis: xAxis,
    behavior: "none"
  }));

  cursor.lineY.set("visible", false);

  // add scrollbar
  chart.set("scrollbarX", am5.Scrollbar.new(root, {
    orientation: "horizontal"
  }));
}


function createAxis(yRenderer, min = null, max = null) {
  let yAxis;

  if (min != null && max != null) {
    yAxis = chart.yAxes.push(
      am5xy.ValueAxis.new(root, {
        maxDeviation: 1,
        min: min,
        max: max,
        renderer: yRenderer
      })
    );
  } else {
    yAxis = chart.yAxes.push(
      am5xy.ValueAxis.new(root, {
        maxDeviation: 1,
        renderer: yRenderer
      })
    );
  }

  if (chart.yAxes.indexOf(yAxis) > 0) {
    yAxis.set("syncWithAxis", chart.yAxes.getIndex(0));
  }

  return yAxis;
}

function createSeries(yAxis, name, samples, yRenderer) {
  var series = chart.series.push(
    am5xy.LineSeries.new(root, {
      name: name,
      xAxis: xAxis,
      yAxis: yAxis,
      valueYField: "valueY",
      valueXField: "valueX",
      tooltip: am5.Tooltip.new(root, {
        pointerOrientation: "horizontal",
        labelText: "{valueY}"
      })
    })
  );

  //series.fills.template.setAll({ fillOpacity: 0.2, visible: true });
  series.strokes.template.setAll({ strokeWidth: 2 });

  yRenderer.grid.template.set("strokeOpacity", 0.05);
  yRenderer.labels.template.set("fill", series.get("fill"));
  yRenderer.setAll({
    stroke: series.get("fill"),
    strokeOpacity: 1,
    opacity: 1
  });

  const featureType = { lengthRelated: false };

  if (featureType.lengthRelated === false) {
    series.data.processor = am5.DataProcessor.new(root, {
      dateFormat: "yyyy-MM-dd",
      dateFields: ["valueX"]
    });
  }

  series.set("setStateOnChildren", true);
  series.states.create("hover", {});

  series.mainContainer.set("setStateOnChildren", true);
  series.mainContainer.states.create("hover", {});

  series.strokes.template.states.create("hover", {
    strokeWidth: 4
  });

  series.bullets.push(function () {
    return am5.Bullet.new(root, {
      sprite: am5.Circle.new(root, {
        radius: 3,
        fill: series.get("fill")
      }
      )
    });
  });

  series.data.setAll(samples);
}


$(document).ready(() => {
  initSelect();
})

selectedMaterialIds = [];
selectedFeatureIds = [];


function selectItem(item, array) {
  array.push(item);
}

function deselectItem(item, array) {
  return array.filter(x => x != item);
}

function initSelect() {

}

function onCompChange() { }

function onFeatureChange(e) { }

function clearSelectedFeatures() {
  selectedFeatureIds.forEach((id) => {
    $("#" + 'feature' + "_" + id).remove();
  });
  selectedFeatureIds = [];
}

function sendMeasurementRequest(featureIds = []) {
  var startDate = $("#start").data("kendoDateTimePicker").value(),
    endDate = $("#end").data("kendoDateTimePicker").value();

  if (!featureIds.length || !startDate || !endDate || startDate.getTime() == endDate.getTime())
    return;

  const dataToSend = {
    featureIds: featureIds,
    startDate: startDate.toISOString(),
    endDate: endDate.toISOString()
  }
  AjaxReqestHelperSilent(Url("Historian", "GetMeasurements"), dataToSend, handleData);

  //AjaxReqestHelperSilent(Url("MeasurementAnalysis", "GetMeasurements"), dataToSend, handleData);
}

function displayMeasurements() { }

function onFeatureSelect(e) {
  const grid = e.sender;
  const selectedItem = grid.dataItem(grid.select());

  closeSlideScreen();

  if (selectedFeatureIds.includes(selectedItem.FeatureId))
    return;

  addSelectedElementToList(selectedItem.FeatureName, selectedItem.FeatureId, $("#selected_features"), 'feature')
  selectItem(selectedItem.FeatureId, selectedFeatureIds);
  if (selectedFeatureIds.length) sendMeasurementRequest(selectedFeatureIds);
}

function onMaterialSelect(e) {
  const grid = e.sender;
  const selectedItem = grid.dataItem(grid.select());
  closeSlideScreen();
  addSelectedElementToList(selectedItem.RawMaterialName, selectedItem.RawMaterialId, $("#selected_materials"), 'material')
  selectItem(selectedItem.RawMaterialId, selectedMaterialIds);
  //if (selectedMaterialIds.length && selectedFeatureIds.length) sendMeasurementRequest(selectedMaterialIds, selectedFeatureIds);
}


function addSelectedElementToList(name, id, $component, type) {
  $component.append('<div class="el" data-id="id" id="' + type + '_' + id + '">' +
    '<div class="el-name">' + name + '</div>' +
    '<div class="el-remove">x</div>' +
    '</div>');

  $("#" + type + "_" + id + " .el-remove").click((el) => {
    $("#" + type + "_" + id).remove();
    if (type == 'feature') {
      selectedFeatureIds = deselectItem(id, selectedFeatureIds);
    }
    else if (type == 'material') {
      selectedMaterialIds = deselectItem(id, selectedMaterialIds);
    }

    if (!selectedFeatureIds.length || !selectedMaterialIds.length)
      destroyChart();
    else
      sendMeasurementRequest(selectedMaterialIds, selectedFeatureIds);
  })
}

function OpenFeatureGridPopup() {
  if (selectedFeatureIds.length > 0 && selectedMaterialIds.length > 1) {
    InfoMessage('Only one Feature can be selected for many Materials');
    return;
  }
  openSlideScreen("MeasurementAnalysis", "GetFeatureTabsView");
}

function OpenMaterialGridPopup() {
  if (selectedFeatureIds.length > 1 && selectedMaterialIds.length > 0) {
    InfoMessage('Only one Material can be selected for many Features');
    return;
  }
  openSlideScreen("MeasurementAnalysis", "GetMaterialGridView");
}

function OpenWOGridPopup() {
  if (selectedFeatureIds.length > 1 && selectedMaterialIds.length > 0) {
    InfoMessage('Only one Material can be selected for many Features');
    return;
  }
  openSlideScreen("MeasurementAnalysis", "GetWorkOrderGridView");
}

function onWorkOrderSelect(e) {
  const grid = e.sender;
  const selectedItem = grid.dataItem(grid.select());
  closeSlideScreen();

  const dataToSend = {
    workOrderId: selectedItem.WorkOrderId,
  }

  AjaxReqestHelperSilent(Url("WorkOrder", "GetMaterialsListByWorkOrderId"), dataToSend, handleMaterialList);
}

function handleMaterialList(materialList) { }

function getFeatureType() {
  return { lengthRelated: null };
}

function startChange() {
  var endPicker = $("#end").data("kendoDateTimePicker"),
    startDate = this.value();
  var maxDate = new Date(new Date(startDate).setHours(startDate.getHours() + 24));

  if (startDate) {
    startDate = new Date(startDate);
    endPicker.min(startDate);
    endPicker.max(maxDate);
    endPicker.value(startDate);
    sendMeasurementRequest(selectedFeatureIds);
  }
}

function endChange() {
  var endDate = this.value();

  if (endDate) {
    sendMeasurementRequest(selectedFeatureIds);
  }
}


let chartData = {
  "Measurements": [
    {
      "Material": {
        "RawmMaterialId": 861,
        "RawmMaterialName": "WO_2022_SF02_111"
      },
      "Feature": {
        "FeatureId": 5301074,
        "FeatureName": "SH12.MEAS.VEL_CUT_111",
        "IsLengthRelated": false,
        "IsSampledFeature": true,
        "Unit": "m/s"
      },
      "Measurement": {
        "Min": -0.05734251,
        "Max": -0.05734251,
        "Avg": -0.057342510000000693,
        "Samples": [{
          "date": new Date(2018, 3, 20, 10, 0, 0, 111),
          "value": 90
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 112),
          "value": 102
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 113),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 114),
          "value": 68
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 115),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 116),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 117),
          "value": 68
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 118),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 119),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 120),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 121),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 122),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 123),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 124),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 125),
          "value": 68
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 126),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 127),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 128),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 129),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 130),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 131),
          "value": 68
        }],
      },
      "ModuleWarningMessage": null
    },
    {
      "Material": {
        "RawmMaterialId": 861,
        "RawmMaterialName": "WO_2022_SF02_222"
      },
      "Feature": {
        "FeatureId": 5400524,
        "FeatureName": "SH16.MEAS.VEL_CUT_222",
        "IsLengthRelated": false,
        "IsSampledFeature": true,
        "Unit": "m/s"
      },
      "Measurement": {
        "Min": -1.901243,
        "Max": 2.765755,
        "Avg": 0.070933211383537254,
        "Samples": [{
          "date": new Date(2018, 3, 20, 10, 0, 0, 111),
          "value": 90
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 112),
          "value": 102
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 113),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 114),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 115),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 116),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 117),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 118),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 119),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 120),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 121),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 122),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 123),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 124),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 125),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 126),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 127),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 128),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 129),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 130),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 131),
          "value": 65
        }],
      },
      "ModuleWarningMessage": null
    },
    {
      "Material": {
        "RawmMaterialId": 8621,
        "RawmMaterialName": "WO_2022_SF02_33223"
      },
      "Feature": {
        "FeatureId": 5400524,
        "FeatureName": "SH16.MEAS.VEL_CUT_32233",
        "IsLengthRelated": false,
        "IsSampledFeature": true,
        "Unit": "m/s"
      },
      "Measurement": {
        "Min": -1.901243,
        "Max": 2.765755,
        "Avg": 0.070933211383537254,
        "Samples": [{
          "date": new Date(2018, 3, 20, 10, 0, 0, 131),
          "value": 65
        }]
      }
    },
    {
      "Material": {
        "RawmMaterialId": 8621,
        "RawmMaterialName": "WO_2022_SF02_33223"
      },
      "Feature": {
        "FeatureId": 5400524,
        "FeatureName": "SH16.MEAS.VEL_CUT_32233",
        "IsLengthRelated": false,
        "IsSampledFeature": true,
        "Unit": "m/s"
      },
      "Measurement": {
        "Min": -1.901243,
        "Max": 2.765755,
        "Avg": 0.070933211383537254,
        "Samples": [{
          "date": new Date(2018, 3, 20, 10, 0, 0, 131),
          "value": 65
        }]
      }
    },
    {
      "Material": {
        "RawmMaterialId": 861,
        "RawmMaterialName": "WO_2022_SF02_333"
      },
      "Feature": {
        "FeatureId": 5400524,
        "FeatureName": "SH16.MEAS.VEL_CUT_333",
        "IsLengthRelated": false,
        "IsSampledFeature": true,
        "Unit": "m/s"
      },
      "Measurement": {
        "Min": -1.901243,
        "Max": 2.765755,
        "Avg": 0.070933211383537254,
        "Samples": [{
          "date": new Date(2018, 3, 20, 10, 0, 0, 111),
          "value": 90
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 112),
          "value": 102
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 113),
          "value": 44
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 114),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 115),
          "value": 33
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 116),
          "value": 44
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 117),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 118),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 119),
          "value": 44
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 120),
          "value": 33
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 121),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 122),
          "value": 44
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 123),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 124),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 125),
          "value": 44
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 126),
          "value": 33
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 127),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 128),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 129),
          "value": 65
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 130),
          "value": 33
        }, {
          "date": new Date(2018, 3, 20, 10, 0, 0, 131),
          "value": 44
        }],
      },
      "ModuleWarningMessage": null
    }
  ],
  "ModuleWarningMessage": null
}
