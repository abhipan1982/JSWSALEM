
function onElementSelect(e) {
  hideCategories();

  if ($('.k-i-arrow-left').length) {
    button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
    $('.more').show(100);
    $('.less').hide(100);
  }

  $('#RawMaterialDetails').addClass('loading-overlay');
  const grid = e.sender;
  const selectedRow = grid.select();

  CurrentElement = {
    RawMaterialId: grid.dataItem(selectedRow).RawMaterialId,
  };

  const dataToSend = {
    RawMaterialId: CurrentElement.RawMaterialId
  };

  const url = "/MeasurementAnalysis/GetMaterialMeasurementsBody";
  AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
  $('#RawMaterialDetails').html(partialView);
  $('#RawMaterialDetails').removeClass('loading-overlay');
}

am5.ready(function () {

  let chart;

  onMeasurementSelect = function (e) {
    const grid = e.sender;
    const selectedRow = grid.select();
    CurrentElement.FeatureId = grid.dataItem(selectedRow).FeatureId;
    const url = "/MeasurementAnalysis/GetMeasurement";
    const dataToSend = { rawMaterialId: CurrentElement.RawMaterialId, featureId: CurrentElement.FeatureId };
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, handleChartData);
  }

  function handleChartData(chartData) {
    if (chart)
      chart.dispose();

    if (!chartData) {
      $("#chartdiv").hide();
      $("#chart-nodata").show();
      $("#chart-nodata").text("No data to display");
      return;
    }

    $("#chartdiv").show();
    $("#chart-nodata").hide();

    const samples = [];

    if (chartData.Feature.IsSampledFeature && chartData.Feature.IsLengthRelated) {
      chartData.Measurement.Samples.forEach((x) => {
        samples.push({ valueX: x.Length, valueY: x.Value });
      });
    } else if (chartData.Feature.IsSampledFeature && !chartData.Feature.IsLengthRelated) {
      chartData.Measurement.Samples.forEach((x) => {
        samples.push({ valueX: x.Date, valueY: x.Value });
      });
    } else if (!chartData.Feature.IsSampledFeature) {
      samples.push({ valueX: 0, valueY: chartData.Measurement.Min });
      samples.push({ valueX: 1, valueY: chartData.Measurement.Max });
    }

    createChart(chartData.Measurement.Min,
      chartData.Measurement.Max,
      chartData.Measurement.Avg, samples,
      chartData.Feature.IsLengthRelated,
      chartData.Feature.IsSampledFeature,
      chartData.Feature.FeatureName,
      chartData.Material.RawmMaterialName,
      chartData.Feature.Unit)
  }

  function createChart(min, max, avg, samples = [], isLengthRelated, isSampledFeature, featureName, rawMaterialName, unit) {

    chart = am4core.create("chartdiv", am4charts.XYChart);
    chart.data = samples;
    let xAxis;

    if (isLengthRelated)
      xAxis = chart.xAxes.push(new am4charts.ValueAxis());
    else
      xAxis = chart.xAxes.push(new am4charts.DateAxis());

    if (!isSampledFeature)
      xAxis.disabled = true;

    xAxis.renderer.grid.template.location = 0;
    xAxis.renderer.minGridDistance = 30;

    const valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
    valueAxis.tooltip.disabled = true;

    const topContainer = chart.plotContainer.createChild(am4core.Container);
    topContainer.layout = "vertical";
    topContainer.paddingBottom = 0;
    topContainer.width = 150;

    let yAxes = chart.yAxes.getIndex(0);
    if (max)
      createIndicator(yAxes, "max", max, "#FF000099", topContainer, unit);
    if (avg)
      createIndicator(yAxes, "avg", avg, "#00FF0099", topContainer, unit);
    if (min)
      createIndicator(yAxes, "min", min, "#00BFFF99", topContainer, unit);

    createSeries("Material: " + rawMaterialName + " | Feature: " + featureName + " | Unit: " + unit, chart, isLengthRelated);

    chart.mouseWheelBehavior = "zoomX";
    chart.legend = new am4charts.Legend();
    chart.cursor = new am4charts.XYCursor();
  }

  function createIndicator(valueAxis, text, value, color, labelContainer, unit) {
    const range = valueAxis.axisRanges.create();
    range.value = value;
    range.grid.stroke = am4core.color(color);
    range.grid.strokeWidth = 2;
    range.grid.strokeOpacity = 1;
    range.label.text = text + ": " + value + " " + unit;
    range.grid.strokeWidth = 2;
    range.grid.strokeOpacity = 1;
    range.label.inside = true;
    range.label.fill = "#000";
    range.label.verticalCenter = "bottom";
  }


  function createSeries(name, chart, isLengthRelated) {
    const series = chart.series.push(new am4charts.LineSeries());
    series.dataFields.valueY = "valueY";
    series.name = name;
    series.tooltip.getFillFromObject = false;
    series.tooltip.background.propertyFields.stroke = "lineColor";
    series.tooltip.autoTextColor = false;
    series.tooltip.label.fill = am4core.color("#222");
    series.strokeWidth = 2;

    if (isLengthRelated) {
      series.dataFields.valueX = "valueX";
      series.tooltipText = "{valueX}: [b]{valueY}[/]";
    } else {
      series.dataFields.dateX = "valueX";
      series.tooltipText = "{dateX}: [b]{valueY}[/]";
    }

    const bullet = series.bullets.push(new am4charts.CircleBullet());
    bullet.circle.stroke = am4core.color("#fff");
    bullet.circle.strokeWidth = 2;
  }
});
