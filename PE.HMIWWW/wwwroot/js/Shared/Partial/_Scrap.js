const _Scrap = new class {

  EditScrapPopup(rawMaterialId, onSuccess = () => { }) {
    if (!rawMaterialId) {
      console.error('Missing data: rawMaterialId');
      return;
    }

    OpenInPopupWindow({
      controller: "Visualization", method: "EditScrapPopup", width: 400, data: { id: rawMaterialId }, afterClose: onSuccess
    });
  }

  PartialScrapPopup(rawMaterialId, onSuccess = () => { }) {
    if (!rawMaterialId) {
      console.error('Missing data: rawMaterialId');
      return;
    }

    OpenInPopupWindow({
      controller: "Visualization", method: "PartialScrapPopup", width: 400, data: { id: rawMaterialId }, afterClose: onSuccess
    });
  }

  ScrapPopup(rawMaterialId, onSuccess = () => { }) {
    if (!rawMaterialId) {
      console.error('Missing data: rawMaterialId');
      return;
    }

    OpenInPopupWindow({
      controller: "Visualization", method: "ScrapPopup", width: 400, data: { id: rawMaterialId }, afterClose: onSuccess
    });
  }

  UnScrap(rawMaterialId, onSuccess = () => { }) {
    if (!rawMaterialId) {
      console.error('Missing data: rawMaterialId');
      return;
    }

    let url = 'Visualization/UnscrapAction';
    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], '', () => {
      AjaxReqestHelper(url, { rawMaterialId: rawMaterialId }, onSuccess);
    });
  }
}

const _Scrap_WorkOrder = new class {

  refreshScraps(workOrderId) {
    reloadGrid('ScrapList');
    reloadGrid('MaterialGrading');
    if (workOrderId) {
      _WorkOrder_SendL3.refreshWorkOrderL3Details(workOrderId);
    }
  }


}


const _Scrap_EditPopup = new class {

  onAssetId1stSelect(e) {
    $("#AssetId1st").val(e.dataItem.AssetId);
    $("#AssetId2nd").data("kendoDropDownList").dataSource.read();
  }

  filterAssetId2nd() {
    return {
      AssetId1st: $("#AssetId1st").val()
    };
  }
}
