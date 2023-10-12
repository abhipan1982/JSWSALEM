const _RawMaterial = new class {

  GoToRawMaterial(rawMaterialId) {
    if (!rawMaterialId) {
      console.error('Missing data: rawMaterialId');
      return;
    }
    let dataToSend = {
      RawMaterialId: rawMaterialId
    };
    openSlideScreen('RawMaterial', 'ElementDetails', dataToSend, Translations["NAME_Material"]);
  }

  GoToHistory(rawMaterialStepId) {
    if (!rawMaterialStepId) {
      console.error('Missing data: rawMaterialStepId');
      return;
    }
    let dataToSend = {
      rawMaterialStepId: rawMaterialStepId
    };
    openSlideScreen('RawMaterial', 'HistoryDetails', dataToSend);
  }

  DeleteMaterialAction(rawMaterialId, onSuccessMethod = () => { }) {
    let message = "";

    let parameters = {
      RawMaterialId: rawMaterialId
    };

    let url = '/TrackingManagement/DeleteMaterial';

    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], message, () => {
      AjaxReqestHelper(url, parameters, onSuccessMethod);
    });
  }

  MaterialReadyView(rawMaterialId) {
    if (!rawMaterialId) {
      console.error('Missing data: rawMaterialId');
      return;
    }
    let dataToSend = {
      RawMaterialId: rawMaterialId
    };
    openSlideScreen('RawMaterial', 'GetMaterialReadyView', dataToSend, Translations["NAME_Material"]);
  }

  MaterialReadyPopup(rawMaterialId) {
    if (!rawMaterialId) {
      InfoMessage(Translations["MESSAGE_SelectElement"]);
      return;
    }
    OpenInPopupWindow({
      controller: "RawMaterial", method: "MaterialReadyPopup", width: 480, data: { rawMaterialId: rawMaterialId }
    });
  }

  ProductUndo(rawMaterialId, onSuccess = () => { }) {
    if (!rawMaterialId) {
      console.error('Missing data: rawMaterialId');
      return;
    }

    let url = 'Visualization/ProductUndoAction';
    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], '', () => {
      AjaxReqestHelper(url, { rawMaterialId: rawMaterialId }, onSuccess);
    });
  }
}

const _RawMaterial_Inspection = new class {

  GoToInspectionDetails(rawMaterialId) {
    if (!rawMaterialId) {
      console.error('Missing data: rawMaterialId');
      return;
    }
    let dataToSend = {
      RawMaterialId: rawMaterialId
    };
    openSlideScreen('Inspection', 'ElementDetails', dataToSend);
  }
}

const _RawMaterial_Defect = new class {

  AssignDefectsPopup() {
    if (!CurrentElement.RawMaterialId) {
      InfoMessage(Translations["MESSAGE_SelectElement"]);
      return;
    }
    OpenInPopupWindow({
      controller: "RawMaterial", method: "AssignDefectsPopup", width: 480,
      data: { rawMaterialId: CurrentElement.RawMaterialId }, afterClose: refreshDefects
    });
  }
}


const _RawMaterial_Assignment = new class {

  AssignRawMaterial(e, rawMaterialId, onSuccess = () => { }) {

    let $currentRawMaterialDetailsView = $('#RawMaterialDetailsBody_' + rawMaterialId);
    $currentRawMaterialDetailsView.addClass('loading-overlay');

    let grid = e.sender;
    let selectedRow = grid.select();
    let selectedElement = grid.dataItem(selectedRow);
    let materialsToAssign = {
      RawMaterialId: rawMaterialId,
      L3MaterialId: selectedElement.MaterialId,
    };

    let renderRawMaterialPartial = () => {
      let dataToSend = {
        rawMaterialId: rawMaterialId
      };
      let url = "/RawMaterial/ElementDetails";
      AjaxReqestHelperSilentWithoutDataType(url, dataToSend, afterAssignment);
    }

    let afterAssignment = (partialView) => {
      $currentRawMaterialDetailsView.replaceWith(partialView);
      if ($('#RawMaterialDetails').length) {
        RefreshData();
      }
      $currentRawMaterialDetailsView.removeClass('loading-overlay');
      onSuccess();
    }

    let assignMaterials = function (callback) {
      if (callback === false) {
        $currentRawMaterialDetailsView.removeClass('loading-overlay');
      } else {
        let url = "/RawMaterial/AssignRawMaterial";
        AjaxReqestHelperSilentWithoutDataType(url, materialsToAssign, renderRawMaterialPartial);
      }
    };

    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_MaterialAssign"], assignMaterials);
  }

  UnassignRawMaterial(rawMaterialId, onSuccess = () => { }) {

    let $currentRawMaterialDetailsView = $('#RawMaterialDetailsBody_' + rawMaterialId);
    $currentRawMaterialDetailsView.addClass('loading-overlay');

    let materialToUnassign = {
      RawMaterialId: rawMaterialId
    };

    let renderRawMaterialPartial = () => {
      let dataToSend = {
        rawMaterialId: rawMaterialId
      };
      let url = "/RawMaterial/ElementDetails";
      AjaxReqestHelperSilentWithoutDataType(url, dataToSend, afterUnassignment);
    }

    let afterUnassignment = (partialView) => {
      $currentRawMaterialDetailsView.replaceWith(partialView);
      if ($('#RawMaterialDetails').length) {
        RefreshData();
      }
      $currentRawMaterialDetailsView.removeClass('loading-overlay');
      onSuccess();

    }

    let unassignMaterials = function (callback) {
      if (callback === false) {
        $currentRawMaterialDetailsView.removeClass('loading-overlay');
      } else {
        let url = "/RawMaterial/UnassignRawMaterial";
        AjaxReqestHelperSilentWithoutDataType(url, materialToUnassign, renderRawMaterialPartial);
      }
    };

    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_MaterialUnassign"], unassignMaterials);
  }
}

function refreshDefects() {
  refreshKendoGrid("RawMaterialDefectList");
}
