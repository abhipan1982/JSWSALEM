let LastAreaState = [];

const _Visualization = new class {

  ShowMaterialsInArea(e) {
    let materialIds = [];
    let materialIdsInFurnace = [];
    for (let i = 0; i < TrackingAreas.length; i++) {
      if (LastAreaState[TrackingAreas[i].TrackingAreaCode] !== null && LastAreaState[TrackingAreas[i].TrackingAreaCode]) {
        let materialsInArea = LastAreaState[TrackingAreas[i].TrackingAreaCode].Materials;
        if (materialsInArea.length) {
          if (TrackingAreas[i].TrackingAreaCode === TrackingAreaKeys.FCE_AREA) {
            for (let j = 0; j < materialsInArea.length; j++) {
              materialIdsInFurnace.push(materialsInArea[j].RawMaterialId);
            }
          }
          else {
            for (let j = 0; j < materialsInArea.length; j++) {
              materialIds.push(materialsInArea[j].RawMaterialId);
            }
          }
        }
      }
    }

    let dataToSend = {
      materialsInAreas: materialIds,
      materialsInFurnace: materialIdsInFurnace,
      selected: e.id
    }
    openSlideScreen('Visualization', 'GetQueueAreasView', dataToSend);
  }

}

const _TrackingAreas = new class {

  GetTrackingAreaByCode(areaCode) {
    return TrackingAreas.find(({ TrackingAreaCode }) => TrackingAreaCode === areaCode);
  }

  SelectAreaTab(tabItem) {
    let tabsEl = $("#MaterialsInArea");
    if (tabsEl.length > 0) {
      var areasTabStrip = tabsEl.kendoTabStrip().data("kendoTabStrip");
      areasTabStrip.select('#' + tabItem);
      if (areasTabStrip.select().length == 0) {
        areasTabStrip.select(0);
      }
    }
  }

  GetLastAreaState() {
    return LastAreaState;
  }

}

const _TrackingActions = new class {

  ChargeIntoChargingGridAction() {
    let url = Url('TrackingManagement', 'ChargeIntoChargingGrid');
    SendRequestWithConfirmation(url);
  }

  UnchargeFromChargingGridAction() {
    let url = Url('TrackingManagement', 'UnchargeFromChargingGrid');
    SendRequestWithConfirmation(url);
  }

  ChargeIntoFurnaceAction() {
    let url = Url('TrackingManagement', 'ChargeIntoFurnace');
    SendRequestWithConfirmation(url);
  }

  UnchargeFromFurnaceAction() {
    let url = Url('TrackingManagement', 'UnchargeFromFurnace');
    SendRequestWithConfirmation(url);
  }

  DischargeForRollingAction() {
    let url = Url('TrackingManagement', 'DischargeForRolling');
    SendRequestWithConfirmation(url);
  }

  UndischargeFromMillAction() {
    let url = Url('TrackingManagement', 'UndischargeFromRolling');
    SendRequestWithConfirmation(url);
  }

  FinishLayerAction(layer, area) {
    let url = Url('TrackingManagement', 'FinishLayer');
    let dataToSend = {
      layerId: layer,
      areaCode: area
    };
    SendRequestWithConfirmation(url, dataToSend);
  }

  TransferLayerAction(layer, area) {
    let dataToSend = {
      layerId: layer,
      areaCode: area
    };
    let url = Url('TrackingManagement', 'TransferLayer');
    SendRequestWithConfirmation(url, dataToSend);
  }

  ChargeMaterialOnFurnaceExitPopup() {
    OpenInPopupWindow({
      controller: "TrackingManagement", method: "ChargeMaterialOnFurnaceExitPopup", width: 480
    });
  }

}
