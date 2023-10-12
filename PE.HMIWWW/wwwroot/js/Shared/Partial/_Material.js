const _Material = new class {

  GoToMaterial(materialId) {
    if (!materialId) {
      console.error('Missing data: materialId');
      return;
    }
    let dataToSend = {
      MaterialId: materialId
    };
    openSlideScreen('Material', 'ElementDetails', dataToSend, Translations["NAME_Material"]);
  }
}

const _Material_Assignment = new class {

  AssignMaterial(e, materialId, onSuccess = () => { }) {

    let $currentMaterialDetailsView = $('#MaterialDetailsBody_' + materialId);
    $currentMaterialDetailsView.addClass('loading-overlay');

    let grid = e.sender;
    let selectedRow = grid.select();
    let selectedElement = grid.dataItem(selectedRow);
    let materialsToAssign = {
      RawMaterialId: selectedElement.RawMaterialId,
      L3MaterialId: materialId,
    };

    let renderMaterialPartial = () => {
      let dataToSend = {
        materialId: materialId
      };
      let url = "/Material/ElementDetails";
      AjaxReqestHelperSilentWithoutDataType(url, dataToSend, afterAssignment);
    }

    let afterAssignment = (partialView) => {
      $currentMaterialDetailsView.replaceWith(partialView);
      if ($('#MaterialDetails').length) {
        RefreshData();
      }
      $currentMaterialDetailsView.removeClass('loading-overlay');
      onSuccess();
    }

    let assignMaterials = function (callback) {
      if (callback === false) {
        $currentMaterialDetailsView.removeClass('loading-overlay');
      } else {
        let url = "/RawMaterial/AssignRawMaterial";
        AjaxReqestHelperSilentWithoutDataType(url, materialsToAssign, renderMaterialPartial);
      }
    };

    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_MaterialAssign"], assignMaterials);
  }

  UnassignMaterial(rawMaterialId, materialId, onSuccess = () => { }) {

    let $currentMaterialDetailsView = $('#MaterialDetailsBody_' + materialId);
    $currentMaterialDetailsView.addClass('loading-overlay');

    let materialToUnassign = {
      RawMaterialId: rawMaterialId
    };

    let renderMaterialPartial = () => {
      let dataToSend = {
        materialId: materialId
      };
      let url = "/Material/ElementDetails";
      AjaxReqestHelperSilentWithoutDataType(url, dataToSend, afterUnassignment);
    }

    let afterUnassignment = (partialView) => {
      $currentMaterialDetailsView.replaceWith(partialView);
      if ($('#MaterialDetails').length) {
        RefreshData();
      }
      $currentMaterialDetailsView.removeClass('loading-overlay');
      onSuccess();
    }

    let unassignMaterials = function (callback) {
      if (callback === false) {
        $currentMaterialDetailsView.removeClass('loading-overlay');
      } else {
        let url = "/RawMaterial/UnassignRawMaterial";
        AjaxReqestHelperSilentWithoutDataType(url, materialToUnassign, renderMaterialPartial);
      }
    };

    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_MaterialUnassign"], unassignMaterials);
  }
}
