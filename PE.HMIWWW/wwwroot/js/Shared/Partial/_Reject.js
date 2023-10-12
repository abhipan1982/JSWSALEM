const _Reject = new class {
  RejectRawMaterialPopup(rawMaterialId, onSuccess = () => { }) {
    if (!rawMaterialId) {
      console.error('Missing data: rawMaterialId');
      return;
    }

    OpenInPopupWindow({
      controller: "Visualization", method: "RejectRawMaterialPopup", width: 480, data: { id: rawMaterialId }, afterClose: onSuccess
    });
  }

  UnReject(rawMaterialId, onSuccess = () => { }) {
    if (!rawMaterialId) {
      console.error('Missing data: rawMaterialId');
      return;
    }

    let url = 'Visualization/UnRejectAction';
    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], '', () => {
      AjaxReqestHelper(url, { rawMaterialId: rawMaterialId }, onSuccess);
    });
  }
}

const _Reject_WorkOrder = new class {

  refreshRejects(workOrderId) {
    reloadGrid('RejectsList');
    reloadGrid('MaterialGrading');
    if (workOrderId) {
      _WorkOrder_SendL3.refreshWorkOrderL3Details(workOrderId);
    }
  }
}
