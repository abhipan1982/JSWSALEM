const _Delay = new class {

  delayEditPopup(delayId, onSuccess = () => { }) {
    if (!delayId) {
      console.error('Missing data: delayId');
      return;
    }
    OpenInPopupWindow({
      controller: "Delays", method: "DelayEditPopup", width: 500, data: { id: delayId }, afterClose: onSuccess
    });
  }
}

const _Delay_EditPopup = new class {

  onEventCataloguCategorySelect(e) {
    $("#CatalogueCategoryId").val(e.dataItem.Value);
    $("#FkEventCatalogueId").data("kendoDropDownList").dataSource.read();
  }

  getEventCatalogueFilter() {
    return {
      eventCatalogueCategoryId: $("#CatalogueCategoryId").val()
    };
  }

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
