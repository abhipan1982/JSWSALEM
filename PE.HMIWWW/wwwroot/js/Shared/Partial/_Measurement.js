const _Measurement = new class {

  GoToMeasurement(measurementId) {
    if (!measurementId) {
      console.error('Missing data: measurementId');
      return;
    }
    let dataToSend = { measurementId: measurementId };
    openSlideScreen('RawMaterial', 'MeasurementDetails', dataToSend);
  }

  GoToMeasurements(rawMaterialId) {
    if (!rawMaterialId) {
      console.error('Missing data: rawMaterialId');
      return;
    }
    let dataToSend = { RawMaterialId: rawMaterialId };
    openSlideScreen('Measurements', 'GetMeasurementsBody', dataToSend);
  }
}