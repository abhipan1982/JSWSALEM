
let MaterialData = {};

//function getCurrentRawMaterialIdExample() {
//  return { RawMaterialId: 82944 }
//}

function onSIASMeasurementsInit(rawMaterialId) {
  let data = GetSIASResults('/Measurements/GetSIASmeasurementsData', rawMaterialId);
  getMaterialDetails();
  updateSIASMeasurements();
  //setColors(data);
  updatePQIGrid(data);
  updateDefectsGrid(data);
  createPQIchart(data);
  createDefectsChart(data);
}

function GetSIASResults(url, rawMaterialId) {
  let targetUrl = url;
  let dataToSend = {
    rawMaterialId: rawMaterialId
  };
  data = AjaxGetDataHelper(targetUrl, dataToSend);
  setColorsStructure(data);
  if (data) return data;
}

function getMaterialDetails() {
  //if (!materialDetails.currentRawMaterial) return;

  //materialDetails.currentRawMaterial = 82944;

  let parameters = { rawMaterialId: CurrentElement.RawMaterialId }
  let data = AjaxGetDataHelper(Url("Visualization", "GetTrackingMaterialDetails"), parameters);

  if (data) {
    MaterialData.materialId = data.MaterialId;
    MaterialData.materialName = data.MaterialName;
    MaterialData.workOrderId = data.WorkOrderId;
    MaterialData.workOrderName = data.WorkOrderName;
    MaterialData.heatId = data.HeatId;
    MaterialData.heatName = data.HeatName;
    MaterialData.steelgradeId = data.SteelgradeId;
    MaterialData.steelgradeName = data.SteelgradeName;
    MaterialData.meltShopId = data.meltShopId;
    MaterialData.isHeatConsistent = data.IsHeatConsistent;
  }
}

function updateSIASMeasurements() {
  $("#L3Material").append("<span id='material-value' class='value-link cursor-pointer'>" + MaterialData.materialName + "</span>");
  $("#workOrder").append("<span id='workorder-value' class='value-link cursor-pointer'>" + MaterialData.workOrderName + "</span>");
  $("#heat").append("<span id='heat-value' class='value-link cursor-pointer'>" + MaterialData.heatName + "</span>");
  $("#steelgrade").append("<span id='steelgrade-value' class='value-link cursor-pointer'>" + MaterialData.steelgradeName + "</span>");

  $("#material-value").click(function () { GoToMaterialSIAS(MaterialData.materialId); return false; });
  $("#workorder-value").click(function () { GoToWorkOrderSIAS(MaterialData.workOrderId); return false; });
  $("#heat-value").click(function () { GoToHeatSIAS(MaterialData.heatId); return false; });
  $("#steelgrade-value").click(function () { GoToSteelgradeSIAS(MaterialData.steelgradeId); return false; });
}

function GoToWorkOrderSIAS(id) {
  let dataToSend = {
    WorkOrderId: id
  };
  openSlideScreen('WorkOrder', 'ElementDetails', dataToSend);
}

function GoToHeatSIAS(id) {
  let dataToSend = {
    heatId: id
  };
  openSlideScreen('Heat', 'ElementDetails', dataToSend);
}

function GoToSteelgradeSIAS(id) {
  let dataToSend = {
    Id: id
  };
  openSlideScreen('Steelgrade', 'ElementDetails', dataToSend);
}

function GoToMaterialSIAS(id) {
  let dataToSend = {
    MaterialId: id
  };
  openSlideScreen('Material', 'ElementDetails', dataToSend);
}

function getDataByProperty(property) {
  if (!property) return;
  let path;
  switch (property) {
    case PROPERTIES.Axis1:
      path = '/Measurements/GetAxis1MeasurementsData';
      break;
    case PROPERTIES.Axis2:
      path = '/Measurements/GetAxis2MeasurementsData';
      break;
    case PROPERTIES.Axis3:
      path = '/Measurements/GetAxis3MeasurementsData';
      break;
    case PROPERTIES.Axis4:
      path = '/Measurements/GetAxis4MeasurementsData';
      break;
    case PROPERTIES.Axis5:
      path = '/Measurements/GetAxis5MeasurementsData';
      break;
    case PROPERTIES.Axis6:
      path = '/Measurements/GetAxis6MeasurementsData';
      break;
    case PROPERTIES.Ovality:
      path = '/Measurements/GetOvalityMeasurementsData';
      break;
    default:
      break;
  }
  let data = GetMeasurementResults(path);
  return data;
}

function GetMeasurementResults(path) {
  let targetUrl = path;
  let dataToSend = {
    rawMaterialId: CurrentElement.RawMaterialId
  };
  data = AjaxGetDataHelper(targetUrl, dataToSend);
  if (data) return data;
}


//

const PROPERTIES = { //add translations
  Axis1: 'Axis1',
  Axis2: 'Axis2',
  Axis3: 'Axis3',
  Axis4: 'Axis4',
  Axis5: 'Axis5',
  Axis6: 'Axis6',
  Ovality: 'Ovality'
};

let initProperty = PROPERTIES.Axis1;

const GAUGES = {
  standI4: 'I4',
  standI8: 'I8',
  standC19: 'C19',
  morganBlock: 'MB'
};



const GAUGES_NUMBER = Object.keys(GAUGES).length;
const GAUGES_KEYS = Object.keys(GAUGES);

const COLORS = {
  standI4: '#ff6800',
  standI8: '#0096ff',
  standC19: '#1c099e',
  morganBlock: '#ffb400'
};

let activeMenuEl;
let chartEl;
let gaugesTable;

function initDOMElements() {
  activeMenuEl = $('.chartMenuEl-active');
  chartEl = $("#gaugesChart");
  //gaugesTable = $('.gaugesTable');
}


function reloadChart(property) {
  if (!property) throw "Parameter is empty!";

  let chartConfig = {
    title: '',
    //xAxisMax: null,
    //yAxisMax: null,
    series: []
  };

  //let maxValuesArray = [];
  data = getDataByProperty(property);
  chartConfig.title = data.featureName;

  //chartConfig.xAxisMax = 70;



  if (data) {
    for (i = 0; i < GAUGES_NUMBER; i++) {
      let gaugeSeries = {
        name: '',
        color: '',
        visible: true,
        data: []
      };

      if (data[GAUGES_KEYS[i]]) {
        chartConfig.unit = data[GAUGES_KEYS[i]].unit;
        gaugeSeries.data = data[GAUGES_KEYS[i]].data;

        if (!gaugeSeries.data) {
          chartConfig.series.push(gaugeSeries);
          gaugeSeries.visible = true;
          gaugeSeries.name = GAUGES[GAUGES_KEYS[i]];
          gaugeSeries.color = '#919191';
          $('#' + GAUGES_KEYS[i] + '-min').text('-');
          $('#' + GAUGES_KEYS[i] + '-avg').text('-');
          $('#' + GAUGES_KEYS[i] + '-max').text('-');
          //gaugeSeries.visible = false;
          //$('#' + GAUGES_KEYS[i] + '-min').parent().hide();
          //$('#' + GAUGES_KEYS[i] + '-avg').parent().hide();
          //$('#' + GAUGES_KEYS[i] + '-max').parent().hide();
        }
        else {
          chartConfig.series.push(gaugeSeries);
          gaugeSeries.visible = true;
          gaugeSeries.name = GAUGES[GAUGES_KEYS[i]];
          gaugeSeries.color = COLORS[GAUGES_KEYS[i]];
          //$('#' + GAUGES_KEYS[i] + '-min').parent().show();
          //$('#' + GAUGES_KEYS[i] + '-avg').parent().show();
          //$('#' + GAUGES_KEYS[i] + '-max').parent().show();
          $('#' + GAUGES_KEYS[i] + '-min').text((data[GAUGES_KEYS[i]].min).toFixed(2));
          $('#' + GAUGES_KEYS[i] + '-avg').text((data[GAUGES_KEYS[i]].average).toFixed(2));
          $('#' + GAUGES_KEYS[i] + '-max').text((data[GAUGES_KEYS[i]].max).toFixed(2));
          //maxValuesArray.push(data[GAUGES_KEYS[i]].max);

        }

      }
    }
    //chartConfig.yAxisMax = getMax(maxValuesArray) + 0.5;

  }
  createChart(chartConfig);
}

function getMax(array) {
  return Math.max(...array);
}

function onPropertySelect(rawMaterialId) {

  $('.chartMenuEl').click(function () {
    activeMenuEl.removeClass('chartMenuEl-active');
    activeMenuEl = $(this);
    activeMenuEl.addClass('chartMenuEl-active');
    let property = activeMenuEl.data('property');

    try {
      reloadChart(property, rawMaterialId);
    } catch (e) {
      console.error(e);
    }

  });
}

function createChart(chartConfig) {
  chartEl.kendoChart({
    title: {
      text: chartConfig.title
    },
    legend: {
      visible: true,
      position: "bottom",
      margin: {
        left: 20
      },
      inactiveItems: {
        labels: {
          color: "#000"
        }
      },
      orientation: "horizontal",
      width: 1000,
      labels: {
        font: "15px sans-serif",
        margin: {
          top: 10,
          left: 5,
          right: 50,
          bottom: 10
        }
      }
    },
    chartArea: {
      height: 530
    },
    seriesDefaults: {
      type: "scatterLine"
    },
    series: chartConfig.series,
    render: function (e) {
      var el = e.sender.element;
      el.find("text:contains(I4)")
        .parent()
        .prev("path")
        .attr("stroke-width", 15)
        .attr("d", "M1 19 l15 0");
      el.find("text:contains(I8)")
        .parent()
        .prev("path")
        .attr("stroke-width", 15)
        .attr("d", "M1 19 l15 0");
      el.find("text:contains(C19)")
        .parent()
        .prev("path")
        .attr("stroke-width", 15)
        .attr("d", "M1 19 l15 0");
      el.find("text:contains(MB)")
        .parent()
        .prev("path")
        .attr("stroke-width", 15)
        .attr("d", "M1 19 l15 0");

      el.find("text:contains(I4)")
        .parent().parent().parent().parent().addClass("gaugeChartLegend");
    },
    xAxis: {
      min: 0,
      labels: {
        format: "{0}" + Translations["NAME_Second"]
      },
      title: {
        text: Translations["NAME_Time"],
        position: "right"
      }
    },
    yAxis: {
      min: 0,
      labels: {
        format: "{0:n2}"
      },
      title: {
        text: Translations["NAME_Value"] + " [" + Translations["UNIT_Length"] + "]"
      }
    },
    tooltip: {
      visible: true,
      format: "{1}"
    }
  });
}

//SIAS Visualization

let dataColors;

function onSIASinit() {
  let data = GetResults('/Measurements/GetSIASmeasurementsData');
  updateSIASdetails();
  //setColors(data);
  updatePQIGrid(data);
  updateDefectsGrid(data);
  createPQIchart(data);
  createDefectsChart(data);
}

function GetResults(url) {
  let targetUrl = url;
  let dataToSend = {
    rawMaterialId: CurrentElement.RawMaterialId
  };
  data = AjaxGetDataHelper(targetUrl, dataToSend);
  setColorsStructure(data);
  if (data) return data;
}

function setColorsStructure(data) {
  if (!data) return;
  dataColors = {
    Split0: data.PQIresults[0].color,
    Split1: data.PQIresults[1].color,
    Split2: data.PQIresults[2].color,
    Long_1: data.DefectsResults[0].color,
    Long_2: data.DefectsResults[1].color,
    Long_3: data.DefectsResults[2].color,
    Trans_1: data.DefectsResults[3].color,
    Trans_2: data.DefectsResults[4].color,
    Trans_3: data.DefectsResults[5].color,
    Rep_1: data.DefectsResults[6].color,
    Rep_2: data.DefectsResults[7].color,
    Rep_3: data.DefectsResults[8].color
  }
  displayChartDataColors();
}

function displayChartDataColors() {
  $('#split0-color').css('background-color', dataColors.Split0);
  $('#split1-color').css('background-color', dataColors.Split1);
  $('#split2-color').css('background-color', dataColors.Split2);

  $('#long_1-color').css('background-color', dataColors.Long_1);
  $('#long_2-color').css('background-color', dataColors.Long_2);
  $('#long_3-color').css('background-color', dataColors.Long_3);

  $('#trans_1-color').css('background-color', dataColors.Trans_1);
  $('#trans_2-color').css('background-color', dataColors.Trans_2);
  $('#trans_3-color').css('background-color', dataColors.Trans_3);

  $('#rep_1-color').css('background-color', dataColors.Rep_1);
  $('#rep_2-color').css('background-color', dataColors.Rep_2);
  $('#rep_3-color').css('background-color', dataColors.Rep_3);
}


function updatePQIGrid(data) {
  $("#split0-value").append(data.Split0);
  $("#split1-value").append(data.Split1);
  $("#split2-value").append(data.Split2);
  $("#PQITotal-value").append(data.PQItotal);

  $("#split0-value").addClass(data.Split0Status);
  $("#split1-value").addClass(data.Split1Status);
  $("#split2-value").addClass(data.Split2Status);
  $("#PQITotal-value").addClass(data.PQItotalStatus);
}

function updateDefectsGrid(data) {
  $("#lang_1").append(data.Lang_1);
  $("#lang_2").append(data.Lang_2);
  $("#lang_3").append(data.Lang_3);
  $("#trans_1").append(data.Trans_1);
  $("#trans_2").append(data.Trans_2);
  $("#trans_3").append(data.Trans_3);
  $("#rep_1").append(data.Rep_1);
  $("#rep_2").append(data.Rep_2);
  $("#rep_3").append(data.Rep_3);
  $("#defectsTotal").append(data.DefectsTotal);
}

function createPQIchart(data) {
  let element = $("#pqiChart");
  let title = data.TitlePQI;
  let results = data.PQIresults;
  createSIASChart(element, title, results);
}

function createDefectsChart(data) {
  let element = $("#defectsChart");
  let title = data.TitleDefects;
  let results = data.DefectsResults;
  createSIASChart(element, title, results);
}

function createSIASChart(element, title, data) {
  element.kendoChart({
    title: {
      position: "bottom",
      text: title
    },
    legend: {
      visible: false
    },
    chartArea: {
      background: ""
    },
    seriesDefaults: {
      labels: {
        visible: true,
        background: "transparent",
        template: "#= category #: \n #= value#"
      }
    },
    series: [{
      type: "pie",
      startAngle: 150,
      data: data,
      overlay: {
        gradient: false
      }
    }],
    tooltip: {
      visible: true,
      format: "{0}"
    },
  });
}
