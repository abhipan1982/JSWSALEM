const _Defect = new class {

  editDefectPopup(defectId, onSuccess = () => { }) {
    if (!defectId) {
      console.error('Missing data: defectId');
      return;
    }
    OpenInPopupWindow({
      controller: "InspectionStation", method: "DefectEditPopup", width: 480, data: { id: defectId }, afterClose: onSuccess
    });
  }

  deleteDefect(defectId, onSuccess = () => { }) {
    if (!defectId) {
      console.error('Missing data: defectId');
      return;
    }

    let url = 'InspectionStation/DeleteDefect'
    PromptMessage(Translations["MESSAGE_deleteConfirm"], '', () => {
      AjaxReqestHelperSilentWithoutDataType(url, { defectId: defectId }, onSuccess);
    });
  }
}