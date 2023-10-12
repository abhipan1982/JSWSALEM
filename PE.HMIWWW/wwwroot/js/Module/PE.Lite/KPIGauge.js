function createGauge(gaugeId, kpiValue, maxValue, minValue, alarmValueTo, warningValueTo, gaugeDirection, labelPosition) {
  $(gaugeId).kendoRadialGauge({
    pointer: {
      value: parseFloat(kpiValue),
      color: '#8a8a8a',
    },
    scale: {
      startAngle: -30,
      endAngle: 210,
      max: maxValue,
      labels: {
        position: labelPosition || "inside"
      },
      ranges: [
        {
          from: parseFloat(minValue),
          to: parseFloat(alarmValueTo),
          color: gaugeDirection == 1 ? "#ef4b48" : "#6ab514"
        },
        {
          from: parseFloat(alarmValueTo),
          to: parseFloat(warningValueTo),
          color: "#f8c401"
        },
        {
          from: parseFloat(warningValueTo),
          to: parseFloat(maxValue),
          color: gaugeDirection == 1 ? "#6ab514" : "#ef4b48"
        }
      ]
    }
  });
}
