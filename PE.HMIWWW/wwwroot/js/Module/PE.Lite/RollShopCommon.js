function CassetteInfo(cassetteId) {
  openSlideScreen("Cassette", "GetCasseteInfo", { casseteId: cassetteId });
}

function RollSetInfo(rollsetId) {
  openSlideScreen("GrindingTurning", "RollSetHistoryPopupDialog", { id: rollsetId });
}

function PassChangeGroove(itemId) {
  OpenInPopupWindow({
    controller: "ActualStandConfiguration",
    method: "PassChangeGrooveDialog",
    width: 1000,
    data: { id: itemId },
    afterClose: CommonRefresh
  });
}

function CommonRefresh() {
  $('.k-grid').data("kendoGrid").dataSource.read();
  closeSlideScreen();

}

function SwapRollSet(itemId, positionId) {
  OpenInPopupWindow({
    controller: "ActualStandConfiguration",
    method: "SwapRollSetDialog",
    width: 540,
    data: { id: itemId, param: positionId },
    afterClose: CommonRefresh
  });
}

function DismountRollSet(itemId, positionId) {
  OpenInPopupWindow({
    controller: "ActualStandConfiguration",
    method: "DismountRollSetDialog",
    width: 540,
    data: { id: itemId, param: positionId },
    afterClose: CommonRefresh
  });
}