
const _Charging = new class {

  GetMaterialsInArea(e) {
    let materialIds = [];
    for (let i = 0; i < TrackingAreas.length; i++) {
      if (LastAreaState[TrackingAreas[i].TrackingAreaCode] !== null && LastAreaState[TrackingAreas[i].TrackingAreaCode]) {
        let materialsInArea = LastAreaState[TrackingAreas[i].TrackingAreaCode].Materials;
        if (materialsInArea.length) {
          if (TrackingAreas[i].TrackingAreaCode === TrackingAreaKeys.CHG_AREA) {
            for (let j = 0; j < materialsInArea.length; j++) {
              materialIds.push(materialsInArea[j].RawMaterialId);
            }
          }
        }
      }
    }

    let dataToSend = {
      materialsInAreas: materialIds,
    }

    return dataToSend;
  }

  ChargeMaterialOnFurnaceSlider(e) {
    openSlideScreen('Charging', 'GetChargingFullList');
  }
}
