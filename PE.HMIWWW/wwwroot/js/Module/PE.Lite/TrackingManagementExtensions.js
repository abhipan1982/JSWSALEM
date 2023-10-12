const _AreasPrinter = new class {
  PrintArea(trackingArea) {
    let element = '';

    try {
      if (trackingArea.IsChangeable) {
        if (!trackingArea.Columns || trackingArea.Columns < 0)
          throw "There is a problem with number of columns in area " + trackingArea.Name + " [" + trackingArea.Code + "].";
        if ((trackingArea.ElementsInColumn * trackingArea.Columns) < trackingArea.Positions)
          throw "There is a problem with number of elements in column in area " + trackingArea.Name + " [" + trackingArea.Code + "]. Number of positions is: " + trackingArea.Positions + " and number of elements is: " + (trackingArea.ElementsInColumn * trackingArea.Columns);

        //Prepare full column's visible slots
        for (let i = 1; i < trackingArea.Columns; i++) {
          element += '<div class="table-area-container">';
          element += '<div class="table-area-content table-column-material isdroptargetArea" data-area="' + trackingArea.Code + '" data-area-name="' + trackingArea.Title + '">';
          for (let j = _TrackingManagementHelpers.GetFirstElementNumberInColumn(trackingArea.ElementsInColumn, i); j <= (trackingArea.ElementsInColumn * i); j++) {
            element += '<div class="table-row tab-row is-row-target is-table-row-double" data-sequence="' + j + '"></div>';
          }
          element += '</div>';
          element += '<div class="table-area-content table-row-sequence">';
          for (let j = _TrackingManagementHelpers.GetFirstElementNumberInColumn(trackingArea.ElementsInColumn, i); j <= (trackingArea.ElementsInColumn * i); j++) {
            element += '<div class="table-row is-table-row-double">' + j + '</div>';
          }
          element += '</div>';
          element += '</div>';
        }

        //Prepare last column's visible slots
        element += '<div class="table-area-container">';
        element += '<div class="table-area-content table-column-material isdroptargetArea" data-area="' + trackingArea.Code + '" data-area-name="' + trackingArea.Title + '">';
        for (let i = _TrackingManagementHelpers.GetFirstElementNumberInLastColumn(trackingArea.Positions, trackingArea.ElementsInColumn, trackingArea.Columns); i < _TrackingManagementHelpers.GetNumberOfElementsInLastColumn(trackingArea.Positions); i++) {
          element += '<div class="table-row tab-row is-row-target is-table-row-double" data-sequence="' + i + '"></div>';
        }

        if (!trackingArea.IsPositionBased && trackingArea.IsDroppable) {
          //Add empty slot for drop on last position
          element += '<div class="table-row tab-row row-target is-table-row-double " data-sequence="0"></div>';
        }

        element += '</div>';
        element += '<div class="table-area-content table-row-sequence">';
        for (let i = _TrackingManagementHelpers.GetFirstElementNumberInLastColumn(trackingArea.Positions, trackingArea.ElementsInColumn, trackingArea.Columns); i < _TrackingManagementHelpers.GetNumberOfElementsInLastColumn(trackingArea.Positions); i++) {
          element += '<div class="table-row is-table-row-double">' + i + '</div>';
        }

        if (!trackingArea.IsPositionBased && trackingArea.IsDroppable) {
          //Add empty slot for drop on last position
          element += '<div class="table-row is-table-row-double"></div>';
        }

        element += '</div>';
        element += '</div>';
      } else {
        const standardColumnsLimit = trackingArea.Columns - 1;
        const lastColumnLimit = trackingArea.Positions === trackingArea.ElementsInColumn ? trackingArea.Positions : trackingArea.Positions % trackingArea.ElementsInColumn;
        let j = 1;

        for (let i = 0; i < trackingArea.Columns; i++) {
          element += '<div class="table-area-container">';
          element += '<div class="table-area-content table-column-material area"' + trackingArea.Code + '" data-area-name="' + trackingArea.Title + '">';
          for (j; j < (trackingArea.Positions + 1); j++) {
            element += '<div class="table-row tab-row" data-sequence="' + j + '"></div>';
            if (j === (trackingArea.ElementsInColumn * (i + 1)) && i !== standardColumnsLimit) break;
          }

          i === standardColumnsLimit ? j -= lastColumnLimit : j -= (trackingArea.ElementsInColumn - 1);
          element += '</div>';
          element += '<div class="table-area-content table-row-sequence">';
          for (j; j < (trackingArea.Positions + 1); j++) {
            element += '<div class="table-row">' + j + '</div>';
            if (j === (trackingArea.ElementsInColumn * (i + 1)) && i !== standardColumnsLimit) {
              j++;
              break;
            }
          }
          element += '</div></div>';
        }
        element += '</div>';
      }
      element = _TrackingManagementHelpers.ReplaceAreaProperties(element, trackingArea.IsDroppable, trackingArea.IsDoubleArea);
    } catch (e) {
      console.error(e);
    }

    return element;
  }

  PrintAreaWithMaterials(trackingArea, materials) {
    let element = '';

    try {
      if (trackingArea.IsChangeable) {
        if (!trackingArea.Columns || trackingArea.Columns < 0)
          throw "There is a problem with number of columns in area " + trackingArea.Name + " [" + trackingArea.Code + "].";
        if ((trackingArea.ElementsInColumn * trackingArea.Columns) < trackingArea.Positions)
          throw "There is a problem with number of elements in column in area " + trackingArea.Name + " [" + trackingArea.Code + "]. Number of positions is: " + trackingArea.Positions + " and number of elements is: " + (trackingArea.ElementsInColumn * trackingArea.Columns);

        //Prepare full column's visible slots
        for (let i = 1; i < trackingArea.Columns; i++) {
          element += '<div class="table-area-container">';
          element += '<div class="table-area-content table-column-material isdroptargetArea" data-area="' + trackingArea.Code + '" data-area-name="' + trackingArea.Title + '">';
          for (let j = _TrackingManagementHelpers.GetFirstElementNumberInColumn(trackingArea.ElementsInColumn, i); j <= (trackingArea.ElementsInColumn * i); j++) {
            element += '<div class="table-row tab-row is-row-target is-table-row-double" data-sequence="' + j + '">'
            const materialsOnPosition = _TrackingManagementHelpers.FindMaterialOnPosition(materials, j);
            if (materialsOnPosition && materialsOnPosition.length && materialsOnPosition.length > 0) {
              for (let k = 0; k < materialsOnPosition.length; k++) {
                if (!materialsOnPosition[k].IsVirtual) {
                  if (trackingArea.IsDraggable)
                    element += _TrackingManagementHelpers.CreateDraggableMaterialEl(materialsOnPosition[k].MaterialName, materialsOnPosition[k].RawMaterialId, trackingArea.Code);
                  else
                    element += _TrackingManagementHelpers.CreateUndraggableMaterialEl(materialsOnPosition[k].MaterialName, materialsOnPosition[k].RawMaterialId, trackingArea.Code);
                }
              }
            }
            element += '</div>';
          }
          element += '</div>';
          element += '<div class="table-area-content table-row-sequence">';
          for (let j = _TrackingManagementHelpers.GetFirstElementNumberInColumn(trackingArea.ElementsInColumn, i); j <= (trackingArea.ElementsInColumn * i); j++) {
            element += '<div class="table-row is-table-row-double">' + j + '</div>';
          }
          element += '</div>';
          element += '</div>';
        }

        //Prepare last column's visible slots
        let rowsInLastColumn = 1;
        element += '<div class="table-area-container">';
        element += '<div class="table-area-content table-column-material isdroptargetArea" data-area="' + trackingArea.Code + '" data-area-name="' + trackingArea.Title + '">';
        if (!trackingArea.IsExpandable && materials.length > (trackingArea.ElementsInColumn * trackingArea.Columns)) {
          const firstElementNumber = _TrackingManagementHelpers.GetFirstElementNumberInLastColumn(trackingArea.Positions, trackingArea.ElementsInColumn, trackingArea.Columns);
          for (let i = 0; i < _TrackingManagementHelpers.GetLowerLimitForNotExpandableArea(trackingArea.ElementsInColumn); i++) {
            element += '<div class="table-row tab-row is-row-target is-table-row-double" data-sequence="' + (i + firstElementNumber) + '">';
            const materialsOnPosition = _TrackingManagementHelpers.FindMaterialOnPosition(materials, i + firstElementNumber);
            if (materialsOnPosition && materialsOnPosition.length && materialsOnPosition.length > 0) {
              for (let k = 0; k < materialsOnPosition.length; k++) {
                if (trackingArea.IsDraggable)
                  element += _TrackingManagementHelpers.CreateDraggableMaterialEl(materialsOnPosition[k].MaterialName, materialsOnPosition[k].RawMaterialId, trackingArea.Code);
                else
                  element += _TrackingManagementHelpers.CreateUndraggableMaterialEl(materialsOnPosition[k].MaterialName, materialsOnPosition[k].RawMaterialId, trackingArea.Code);
              }
            }
            element += '</div>';
          }
          element += '<div class="table-row tab-row is-table-row-double" data-sequence="-1">';
          element += _TrackingManagementHelpers.CreateUndraggableMaterialEl("...", -1, trackingArea.Code);
          element += '</div>';
          for (let i = _TrackingManagementHelpers.GetUpperLimitForNotExpandableArea(materials.length, trackingArea.ElementsInColumn); i < materials.length; i++) {
            element += '<div class="table-row tab-row is-row-target is-table-row-double" data-sequence="' + (i + 1) + '">';
            const materialsOnPosition = _TrackingManagementHelpers.FindMaterialOnPosition(materials, i + 1);
            if (materialsOnPosition && materialsOnPosition.length && materialsOnPosition.length > 0) {
              for (let k = 0; k < materialsOnPosition.length; k++) {
                if (trackingArea.IsDraggable)
                  element += _TrackingManagementHelpers.CreateDraggableMaterialEl(materialsOnPosition[k].MaterialName, materialsOnPosition[k].RawMaterialId, trackingArea.Code);
                else
                  element += _TrackingManagementHelpers.CreateUndraggableMaterialEl(materialsOnPosition[k].MaterialName, materialsOnPosition[k].RawMaterialId, trackingArea.Code);
              }
            }
            element += '</div>';
          }
        } else {
          for (let i = _TrackingManagementHelpers.GetFirstElementNumberInLastColumn(trackingArea.Positions, trackingArea.ElementsInColumn, trackingArea.Columns); i < _TrackingManagementHelpers.GetNumberOfElementsInLastColumn(trackingArea.Positions); i++) {
            rowsInLastColumn++;
            element += '<div class="table-row tab-row is-row-target is-table-row-double" data-sequence="' + i + '">';
            const materialsOnPosition = _TrackingManagementHelpers.FindMaterialOnPosition(materials, i);
            if (materialsOnPosition && materialsOnPosition.length && materialsOnPosition.length > 0) {
              for (let k = 0; k < materialsOnPosition.length; k++) {
                if (!materialsOnPosition[k].IsVirtual) {
                  if (trackingArea.IsDraggable)
                    element += _TrackingManagementHelpers.CreateDraggableMaterialEl(materialsOnPosition[k].MaterialName, materialsOnPosition[k].RawMaterialId, trackingArea.Code);
                  else
                    element += _TrackingManagementHelpers.CreateUndraggableMaterialEl(materialsOnPosition[k].MaterialName, materialsOnPosition[k].RawMaterialId, trackingArea.Code);
                }
              }
            }
            element += '</div>';
          }
        }

        if (!trackingArea.IsPositionBased && trackingArea.IsDroppable) {
          //Add empty slot for drop on last position
          element += '<div class="table-row tab-row row-target is-table-row-double" data-sequence="0"></div>';
          rowsInLastColumn++;
        }

        let overloadedMaterials = [];
        let virtualMaterials = [];
        for (let i = 0; i < materials.length; i++) {
          const material = materials[i];
          if (material.PositionOrder > trackingArea.Positions && !material.IsVirtual)
            overloadedMaterials.push(material);
          if (material.IsVirtual)
            virtualMaterials.push(material);
        }

        if (!trackingArea.IsPositionBased && trackingArea.IsExpandable && overloadedMaterials.length > 0) {
          //Prepare last column's overloaded materials
          if (overloadedMaterials.length > 1) {
            element += '<div class="table-row tab-row is-table-row-double" data-sequence="-1">';
            element += _TrackingManagementHelpers.CreateUndraggableMaterialEl("...", -1, trackingArea.Code);
            element += '</div>';
            rowsInLastColumn++;
          }
          element += '<div class="table-row tab-row is-table-row-double" data-sequence="' + overloadedMaterials[overloadedMaterials.length - 1].PositionOrder + '">';
          const material = overloadedMaterials[overloadedMaterials.length - 1];
          if (material) {
            if (trackingArea.IsDraggable)
              element += _TrackingManagementHelpers.CreateDraggableMaterialEl(material.MaterialName, material.RawMaterialId, trackingArea.Code);
            else
              element += _TrackingManagementHelpers.CreateUndraggableMaterialEl(material.MaterialName, material.RawMaterialId, trackingArea.Code);
          }
          element += '</div>';
          rowsInLastColumn++;
        }

        let virtualMaterialsInPositionBased = [];
        if (trackingArea.IsExpandable && virtualMaterials.length > 0) {
          //Prepare last column's virtual materials
          const availablePositions = trackingArea.ElementsInColumn - rowsInLastColumn;
          let virtualMaterialsLength = virtualMaterials.length;
          if (!trackingArea.IsPositionBased && virtualMaterials.length > availablePositions) {
            for (let i = 0; i < virtualMaterialsLength - availablePositions; i++) {
              virtualMaterials.shift();
            }
            element += '<div class="table-row tab-row is-table-row-double" data-sequence="-2">';
            element += _TrackingManagementHelpers.CreateUndraggableMaterialEl("...", -2, trackingArea.Code);
            element += '</div>';
          }
          virtualMaterialsLength = virtualMaterials.length;
          for (let i = 0; i < virtualMaterialsLength; i++) {
            if (!trackingArea.IsPositionBased) {
              const material = virtualMaterials[i];
              if (material) {
                element += '<div class="table-row tab-row is-table-row-double" data-sequence="' + material.PositionOrder + '">'
                if (trackingArea.IsDraggable)
                  element += _TrackingManagementHelpers.CreateDraggableMaterialEl(material.MaterialName, material.RawMaterialId, trackingArea.Code);
                else
                  element += _TrackingManagementHelpers.CreateUndraggableMaterialEl(material.MaterialName, material.RawMaterialId, trackingArea.Code);
                element += '</div>';
              }
            } else {
              const material = virtualMaterials[0];
              if (material) {
                virtualMaterialsInPositionBased.push(material);
                const materialsOnPosition = _TrackingManagementHelpers.FindMaterialOnPosition(virtualMaterials, material.PositionOrder);
                if (materialsOnPosition && materialsOnPosition.length && materialsOnPosition.length > 0) {
                  element += '<div class="table-row tab-row is-table-row-double" data-sequence="' + material.PositionOrder + '">'
                  for (let k = 0; k < materialsOnPosition.length; k++) {
                    if (trackingArea.IsDraggable)
                      element += _TrackingManagementHelpers.CreateDraggableMaterialEl(materialsOnPosition[k].MaterialName, materialsOnPosition[k].RawMaterialId, trackingArea.Code);
                    else
                      element += _TrackingManagementHelpers.CreateUndraggableMaterialEl(materialsOnPosition[k].MaterialName, materialsOnPosition[k].RawMaterialId, trackingArea.Code);
                    virtualMaterials.shift();
                  }
                  element += '</div>';
                }
              }
            }
          }
        }
        element += '</div>';

        if (virtualMaterialsInPositionBased.length > 0)
          virtualMaterials = virtualMaterialsInPositionBased;

        //Prepare last column's visible slots
        element += '<div class="table-area-content table-row-sequence">';
        if (!trackingArea.IsExpandable && materials.length > (trackingArea.ElementsInColumn * trackingArea.Columns)) {
          const firstElementNumber = _TrackingManagementHelpers.GetFirstElementNumberInLastColumn(trackingArea.Positions, trackingArea.ElementsInColumn, trackingArea.Columns);
          for (let i = 0; i < _TrackingManagementHelpers.GetLowerLimitForNotExpandableArea(trackingArea.ElementsInColumn); i++) {
            element += '<div class="table-row is-table-row-double">' + (i + firstElementNumber) + '</div>';
          }
          element += '<div class="table-row is-table-row-double">OV</div>';
          for (let i = _TrackingManagementHelpers.GetUpperLimitForNotExpandableArea(materials.length, trackingArea.ElementsInColumn); i < materials.length; i++) {
            let positionSeq = "OV";
            const materialsOnPosition = _TrackingManagementHelpers.FindMaterialOnPosition(materials, i + 1);
            const material = materialsOnPosition[0];
            if (material) {
              positionSeq = trackingArea.IsVirtual ? "V" : material.PositionOrder;
            }
            element += '<div class="table-row is-table-row-double">' + positionSeq + '</div>';
          }
        } else {
          for (let i = _TrackingManagementHelpers.GetFirstElementNumberInLastColumn(trackingArea.Positions, trackingArea.ElementsInColumn, trackingArea.Columns); i < _TrackingManagementHelpers.GetNumberOfElementsInLastColumn(trackingArea.Positions); i++) {
            element += '<div class="table-row is-table-row-double">' + i + '</div>';
          }
        }

        if (!trackingArea.IsPositionBased && trackingArea.IsDroppable) {
          //Add empty slot for drop on last position
          element += '<div class="table-row is-table-row-double"></div>';
          rowsInLastColumn++;
        }

        if (!trackingArea.IsPositionBased && trackingArea.IsExpandable && overloadedMaterials.length > 0) {
          //Prepare last column's overloaded materials
          if (overloadedMaterials.length > 1) {
            element += '<div class="table-row is-table-row-double">OV</div>';
          }
          element += '<div class="table-row is-table-row-double">OV</div>';
        }

        if (trackingArea.IsExpandable && virtualMaterials.length > 0) {
          //Prepare last column's virtual materials
          const availablePositions = trackingArea.ElementsInColumn - rowsInLastColumn;
          let virtualMaterialsLength = virtualMaterials.length;
          if (virtualMaterials.length > availablePositions) {
            element += '<div class="table-row is-table-row-double">V</div>';
          }
          for (let i = 0; i < virtualMaterialsLength; i++) {
            element += '<div class="table-row is-table-row-double">V</div>';
          }
        }

        element += '</div>';
        element += '</div>';
      } else {
        const standardColumnsLimit = trackingArea.Columns - 1;
        const lastColumnLimit = trackingArea.Positions === trackingArea.ElementsInColumn ? trackingArea.Positions : trackingArea.Positions % trackingArea.ElementsInColumn;
        let j = 1;

        for (let i = 0; i < trackingArea.Columns; i++) {
          element += '<div class="table-area-container">';
          element += '<div class="table-area-content table-column-material area"' + trackingArea.Code + '" data-area-name="' + trackingArea.Title + '">';
          for (j; j < (trackingArea.Positions + 1); j++) {
            element += '<div class="table-row tab-row" data-sequence="' + j + '"></div>';
            if (j === (trackingArea.ElementsInColumn * (i + 1)) && i !== standardColumnsLimit) break;
          }

          i === standardColumnsLimit ? j -= lastColumnLimit : j -= (trackingArea.ElementsInColumn - 1);
          element += '</div>';
          element += '<div class="table-area-content table-row-sequence">';
          for (j; j < (trackingArea.Positions + 1); j++) {
            element += '<div class="table-row">' + j + '</div>';
            if (j === (trackingArea.ElementsInColumn * (i + 1)) && i !== standardColumnsLimit) {
              j++;
              break;
            }
          }
          element += '</div></div>';
        }
        element += '</div>';
      }
      element = _TrackingManagementHelpers.ReplaceAreaProperties(element, trackingArea.IsDroppable, trackingArea.IsDoubleArea);
    } catch (e) {
      console.error(e);
    }

    return element;
  }

  PrintLayerArea(trackingArea) {
    let element = "";

    element += '<div class="table-area-header layers" data-area="' + trackingArea.Code + '" data-area-name="' + trackingArea.Title + '">';
    element += trackingArea.Title;
    element += '</div>';
    element += '<div class="table-area area layers-area droptargetArea" data-area="' + trackingArea.Code + '" data-area-name="' + trackingArea.Title + '">';
    element += '</div></div>';

    return element;
  }

  PrintAreaHeader(trackingArea) {
    let element = '';
    element += '<div class="table-area">';
    element += '<div class="table-area-header ' + trackingArea.CssClassName + '" data-area="' + trackingArea.Code + '" data-area-name="' + trackingArea.Title + '">';
    element += trackingArea.Title;
    if (trackingArea.IsPositionBased) {
      element += '<span class="move-action-arrows">';
      element += '<i class="arrow up cursor-pointer" onclick="_TrackingManagementActions.MoveMaterialsUp(this)" title="' + Translations["NAME_MoveBackward"] + '"></i>';
      element += '<i class="arrow down cursor-pointer" onclick="_TrackingManagementActions.MoveMaterialsDown(this)" title="' + Translations["NAME_MoveForward"] + '"></i>';
      element += '</span >';
    }
    element += '</div>';
    element += '<div class="areas area" data-area="' + trackingArea.Code + '" data-area-name="' + trackingArea.Title + '">';

    return element;
  }

  PrintAreaHeaderWithMaterials(trackingArea, materialsNumber) {
    let element = '';
    element += '<div class="table-area">';
    element += '<div class="table-area-header ' + trackingArea.CssClassName + '" data-area="' + trackingArea.Code + '" data-area-name="' + trackingArea.Title + '">';
    const areaTitle = materialsNumber > (trackingArea.Positions + trackingArea.VirtualPositions) ?
      trackingArea.Title + ' [' + materialsNumber + ' ' + Translations["MESSAGE_Of"] + ' ' + trackingArea.Positions + ']' :
      trackingArea.Title;
    element += areaTitle;
    if (trackingArea.IsPositionBased) {
      element += '<span class="move-action-arrows">';
      element += '<i class="arrow up cursor-pointer" onclick="_TrackingManagementActions.MoveMaterialsUp(this)" title="' + Translations["NAME_MoveBackward"] + '"></i>';
      element += '<i class="arrow down cursor-pointer" onclick="_TrackingManagementActions.MoveMaterialsDown(this)" title="' + Translations["NAME_MoveForward"] + '"></i>';
      element += '</span >';
    }
    element += '</div>';
    element += '<div class="areas area" data-area="' + trackingArea.Code + '" data-area-name="' + trackingArea.Title + '">';

    return element;
  }

  PlaceMaterialsInArea(trackingArea, areaElement, positions) {
    if (!positions)
      throw ("There is a problem while refreshing areas. Area " + trackingArea.Name + " [" + trackingArea.Code + "] " + "did not receive refresh data.");

    if (trackingArea.IsExpandable || trackingArea.IsChangeable || trackingArea.IsDroppable || trackingArea.IsDraggable) {
      const targetHTML = $('#' + trackingArea.Name);
      if (!targetHTML)
        throw ("There is a problem while placeMaterialsInArea. Area with name: " + trackingArea.Name + "[" + trackingArea.Code + "] " + "cannot be found on the screen by it's id.");
      $(targetHTML).empty();
      let areaHTML = _AreasPrinter.PrintAreaHeaderWithMaterials(trackingArea, positions.length);
      areaHTML += _AreasPrinter.PrintAreaWithMaterials(trackingArea, positions);
      areaHTML += '</div></div>';
      $(targetHTML).append(areaHTML);
    } else {
      for (let i = 1; i < trackingArea.Positions + 1; i++) {
        let row = $(areaElement).find("[data-sequence='" + i + "']")[0];
        if (row) {
          $(row).empty();
        }
      }
      for (let i = 0; i < positions.length; i++) {
        if (positions[i].RawMaterialId) {
          let row = $(areaElement).find("[data-sequence='" + positions[i].PositionOrder + "']")[0];
          if (row) {
            $(row).append(_TrackingManagementHelpers.CreateUndraggableMaterialEl(positions[i].MaterialName, positions[i].RawMaterialId, trackingArea.Code));
          }
        }
      }
    }
  }
}

const _TrackingManagementHelpers = new class {
  GetTrackingAreaByCode(trackingAreas, areaCode) {
    return trackingAreas.find(obj => {
      return obj.Code === areaCode
    })
  }

  GetFirstElementNumberInColumn(elementsInColumn, iteration) {
    return (elementsInColumn * iteration) - (elementsInColumn - 1)
  }

  GetFirstElementNumberInLastColumn(positions, elementsInColumn, columns) {
    if (columns == 1)
      return 1;
    return positions + 1 - (positions - ((columns - 1) * elementsInColumn));
  }

  GetNumberOfElementsInLastColumn(positions) {
    return positions + 1;
  }

  GetFirstVirtualElementNumberInLastColumn(positions, isExpandable) {
    if (isExpandable)
      return positions + 2;
    else
      return positions + 1;
  }

  GetNumberOfHiddenVirtualElements(elementsInColumn, columns, isDroppable) {
    let result = elementsInColumn * columns;
    return isDroppable ? result : result + 1;
  }

  ReplaceAreaProperties(element, isDroppable, isDoubleArea) {
    var droppableRegex = /isdroptargetArea/ig;
    var rowTargetRegex = /is-row-target/ig;
    var doubleAreaRegex = /is-table-row-double/ig;
    if (isDroppable) {
      element = element.replaceAll(droppableRegex, "droptargetArea");
      element = element.replaceAll(rowTargetRegex, "row-target");
    } else {
      element = element.replaceAll(droppableRegex, "");
      element = element.replaceAll(rowTargetRegex, "");
    }

    if (isDoubleArea) {
      element = element.replaceAll(doubleAreaRegex, "table-row-double");
    } else {
      element = element.replaceAll(doubleAreaRegex, "");
    }

    return element;
  }

  GetAreaElement(areaElements, areaCode) {
    if (!areaCode) return;

    for (let i = 0; i < areaElements.length; i++) {
      if (areaCode === $(AreaElements[i]).data('area'))
        return areaElements[i];
    }

    return null;
  }

  GetNameToDisplay(name, maxNameLength) {
    return name.length > maxNameLength ? "<div class='longText' title='" + name + "' data-last-letters='" + name.substring(name.length - 3) + "'>" + name + "</div>" : name;
  }


  CreateDraggableMaterialEl(matName, rawMatId, areaCode) {
    if (rawMatId < 0)
      return '<div onmousedown="_Visualization.ShowMaterialsInArea()" data-area-code="' + areaCode + '" data-mat-id="' + rawMatId + '" data-mat="' + (matName || rawMatId) + '" class="mat-name draggable nowrap-text" id="draggable-' + draggableIndex++ + '"> ' + '<span class="delete-icon" onclick="_TrackingManagementActions.DeleteMaterial(this)">x</span> <span class="material" style="width: 88%">' + (_TrackingManagementHelpers.GetNameToDisplay((matName || rawMatId), 21)) + '</span></div>'
    else
      return '<div onmousedown="_TrackingManagementHelpers.OnMaterialSelected(this)" data-area-code="' + areaCode + '" data-mat-id="' + rawMatId + '" data-mat="' + (matName || rawMatId) + '" class="mat-name draggable nowrap-text" id="draggable-' + draggableIndex++ + '"> ' + '<span class="delete-icon" onclick="_TrackingManagementActions.DeleteMaterial(this)">x</span> <span class="material" style="width: 88%">' + (_TrackingManagementHelpers.GetNameToDisplay((matName || rawMatId), 21)) + '</span></div>'
  }

  CreateUndraggableMaterialEl(matName, rawMatId, areaCode) {
    if (rawMatId < 0)
      return '<div onmousedown="_Visualization.ShowMaterialsInArea()" data-area-code="' + areaCode + '" data-mat-id="' + rawMatId + '" data-mat="' + (matName || rawMatId) + '" class="mat-name nowrap-text"> ' + '<span class="delete-icon"></span> <span class="material" style="width: 93%">' + (_TrackingManagementHelpers.GetNameToDisplay((matName || rawMatId), 23)) + '</span></div>'
    else
      return '<div onmousedown="_TrackingManagementHelpers.OnMaterialSelected(this)" data-area-code="' + areaCode + '" data-mat-id="' + rawMatId + '" data-mat="' + (matName || rawMatId) + '" class="mat-name nowrap-text"> ' + '<span class="delete-icon"></span> <span class="material" style="width: 93%">' + (_TrackingManagementHelpers.GetNameToDisplay((matName || rawMatId), 23)) + '</span></div>'
  }

  CompareObjects(obj1, obj2) {
    return Object.keys(obj1).length === Object.keys(obj2).length && Object.keys(obj1).every(p => obj1[p] === obj2[p]);
  }

  CompareArraysOfObjects(a1, a2) {
    return a1.length === a2.length && a1.every((o, idx) => _TrackingManagementHelpers.CompareObjects(o, a2[idx]));
  }

  IsAreaStateChanged(areaState_current, areaState_new) {
    return !_TrackingManagementHelpers.CompareArraysOfObjects(areaState_current, areaState_new);
  }

  OnMaterialClickEvent() {
    $('.table-row').on('click', function () {
      if ($(this).attr('data-sequence')) {
        if (selectedRow) {
          _TrackingManagementHelpers.SetNotSelectedRow();
        }
        if ($(this).children().attr('data-mat-id')) {
          _TrackingManagementHelpers.SetSelectedRow($(this));
        }
      } else {
        _TrackingManagementHelpers.SetNotSelectedRow();
      }
    })
  }

  OnMaterialSelected(el) {
    currentMatId = $(el).data('mat-id');
    currentMat = $(el).data('mat');
    rowStart = $(el).parent();
    areaStart = rowStart.parent();
    areaStartCode = areaStart.data('area')
    rowStartSeqNumber = rowStart.data('sequence');
  }

  DraggableOnDragStart(e) {
  }

  DroptargetOnDragEnter(e) {
    $(e.dropTarget).addClass('drag-hover');
  }

  DroptargetOnDragLeave(e) {
    $(e.dropTarget).removeClass('drag-hover');
  }

  DroptargetOnDrop(e) {
    $(e.dropTarget).removeClass('drag-hover');
    let $mat = $(e.draggable)[0].currentTarget;
    let matStartPosition = $mat.parent().data('sequence');
    let matStartAreaCode = $mat.parent().parent().data('area');
    let materialsInStartArea = _TrackingAreas.GetLastAreaState()[matStartAreaCode].Materials;
    for (let i = 0; i < materialsInStartArea.length; i++) {
      if (currentMatId == materialsInStartArea[i].RawMaterialId) {
        matStartPosition = materialsInStartArea[i].PositionOrder;
        break;
      }
    }
    let rowEnd = $(e.dropTarget);
    let areaEnd = rowEnd.parent();
    let areaEndCode = areaEnd.data('area');
    let areaEndName = areaEnd.data('area-name');
    let rowEndSeqNumber = rowEnd.data('sequence');

    if (matStartAreaCode == areaEndCode && matStartPosition == rowEndSeqNumber) {
      return;
    }

    _TrackingManagementActions.MoveMaterial(matStartAreaCode, matStartPosition, areaEndCode, rowEndSeqNumber, currentMatId, currentMat, areaEndName);
  }

  DraggableOnDragEnd(e) {

  }

  InitDropTarget() {
    let droptargetAreas = $('.droptargetArea');
    for (let i = 0; i < droptargetAreas.length; i++) {
      $(droptargetAreas[i]).kendoDropTargetArea({
        filter: ".row-target",
        dragenter: _TrackingManagementHelpers.DroptargetOnDragEnter,
        dragleave: _TrackingManagementHelpers.DroptargetOnDragLeave,
        drop: _TrackingManagementHelpers.DroptargetOnDrop
      });
    }
  }

  InitDragDrop() {
    for (let i = 0; i <= MAX_MatDrag; i++) {
      let $el = $("#draggable-" + i);
      if ($el.length) {
        $el.kendoDraggable({
          hint: function (el) {
            return $("#draggable-" + i + " .material").clone();
          },
          dragstart: _TrackingManagementHelpers.DraggableOnDragStart,
          dragend: _TrackingManagementHelpers.DraggableOnDragEnd
        });
      } else return;
    }
  }

  ClearDragDrop() {
    for (let i = 0; i <= MAX_MatDrag; i++) {
      let $el = $("#draggable-" + i);
      if ($el.length && $el.data("kendoDraggable")) {
        $el.data("kendoDraggable").destroy();
      } else return;
    }
  }

  PlaceLayers(areaId, areaElement, trackingArea, positions) {
    if (!positions)
      throw ("There is a problem while refreshing areas. Area " + trackingArea.Name + " [" + trackingArea.Code + "] " + "did not receive refresh data.");

    for (let i = 0; i < positions.length; i++) {
      $(areaElement).append(_TrackingManagementHelpers.CreateLayerEl(areaId, positions[i]));
    }
  }

  CreateLayerEl(areaId, layerData) {
    let statusClass = '';
    let statusAction = '';

    if (layerData.IsEmpty) {
      statusClass = 'status-empty';
    } else if (layerData.IsForming) {
      statusClass = 'status-progress';
      statusAction = 'action-finish" onclick="_TrackingActions.FinishLayerAction(' + layerData.Id + ',' + areaId + ') ';
    } else if (layerData.IsFormed) {
      statusClass = 'status-ready';
      statusAction = 'action-transfer" onclick="_TrackingActions.TransferLayerAction(' + layerData.Id + ',' + areaId + ') ';
    }

    let layerEl =
      '<div class="table-area mt-1">' +
      '<div id="' + layerData.Id + '" class="table-area-header nowrap-text layer cursor-pointer" data-area="" onclick="_TrackingManagementActions.GoToLayer(this)">' + layerData.Name + '</div>' +
      '<div class="table-area-container">' +
      '<div class="table-area-content table-row-sequence" id="">' +
      '<div class="table-row">' +
      '<div class="layer-status ' + statusAction + '"></div>' +
      '</div>' +
      '</div>' +
      '<div class="table-area-content table-column-layer" data-area="' + areaId + '" data-area-name="' + layerData.Name + '">' +
      '<div class="table-row tab-row row-target layer-materialsSum" data-sequence="' + layerData.PositionOrder + '">materials sum: ' + layerData.MaterialsSum + ' </div>' +
      '</div>' +
      '<div class="table-area-content table-row-sequence" id="">' +
      '<div class="table-row">' +
      '<div class="layer-status ' + statusClass + '"></div>' +
      '</div>' +
      '</div>' +
      '</div>' +
      '</div>';

    return layerEl;
  }

  SendRequestWithConfirmation(action, parameters = null, message = '', onSuccessMethod = null) {

    if (!action) return;

    let url = Url("TrackingManagement", action);

    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], message, () => {
      AjaxReqestHelperSilentWithoutDataType(url, parameters, _TrackingManagementHelpers.OnSuccessMethod);
    });
  }

  OnSuccessMethod() { }

  SetLaneCommunication(isLaneStopped) {
    switchLaneProdBtnKendo.check(isLaneStopped);
  }

  SetSlowProduction(isSlowProduction) {
    switchSlowProdBtnKendo.check(isSlowProduction);
  }

  FindMaterialOnPosition(materials, positionNumber) {
    let result = [];
    for (var i = 0; i < materials.length; i++) {
      const material = materials[i];
      if (material.PositionOrder === positionNumber)
        result.push(material);
    }

    return result;
  }

  SetSelectedRow(selectedEl) {
    selectedRow = selectedEl;
    selectedRow.addClass('selected-row');
    CurrentElement = {
      RawMaterialId: selectedRow.children().attr('data-mat-id'),
    };
  }

  SetNotSelectedRow() {
    if (!selectedRow) return;
    selectedRow.removeClass('selected-row');
    selectedRow = null;
    CurrentElement = {
      RawMaterialId: null,
    };
  }

  ClearLayers(areas, areaElements) {
    draggableIndex = 0;
    for (let i = 0; i < areas.length; i++) {
      if (areas[i].Code === TrackingAreaKeys.LAYER_AREA) {
        let areaElement = _TrackingManagementHelpers.GetAreaElement(areaElements, areas[i].Code);
        $(areaElement).empty();
      }
    }
  }

  IsTrackingStateChanged(areasState_current, areasState_new) {
    for (let i = 0; i < areasState_new.length; i++) {
      let areaState = areasState_current.find((el, idx) => {
        if (el.AreaId == areasState_new[i].AreaId) return true;
      })

      if (areasState_new[i].AreaId === TrackingAreaKeys.LAYER_AREA) {
        if (areaState && _TrackingManagementHelpers.IsAreaStateChanged(areaState.Layers, areasState_new[i].Layers)) return true;
      } else {
        if ((areaState && _TrackingManagementHelpers.IsAreaStateChanged(areaState.Materials, areasState_new[i].Materials)) || !areaState) {
          return true;
        }
      }
    }
    return false;
  }

  InitAreas(areas, areasOnHMI) {
    for (let i = 0; i < areasOnHMI.length; i++) {
      try {
        const key = areasOnHMI[i];
        const trackingArea = _TrackingManagementHelpers.GetTrackingAreaByCode(areas, TrackingAreaKeys[key]);
        if (!trackingArea)
          throw ("There is a problem while initiating areas. Area " + trackingArea.Code + " is not found. Check AREAS array.");
        let targetHTML = $('#' + trackingArea.Name);
        if (!targetHTML)
          throw ("There is a problem while initiating areas. Area " + trackingArea.Name + " [" + trackingArea.Code + "] " + "cannot be found on screen. Check if area in on HMI.");
        if ((trackingArea.Positions + trackingArea.VirtualPositions) > (trackingArea.ElementsInColumn * trackingArea.Columns))
          throw ("There is a problem while initiating areas. Area " + trackingArea.Name + " [" + trackingArea.Code + "] " + "does not have enought space to display materials. Number of regular and virtual positions is " + (trackingArea.Positions + trackingArea.VirtualPositions) + " but number of available positions on HMI is " + trackingArea.ElementsInColumn * trackingArea.Columns + ".");
        let areaHTML = "";
        if (trackingArea.Code === TrackingAreaKeys.LAYER_AREA) {
          areaHTML = _AreasPrinter.PrintLayerArea(trackingArea);
        }
        else {
          areaHTML = _AreasPrinter.PrintAreaHeader(trackingArea);
          areaHTML += _AreasPrinter.PrintArea(trackingArea);
          areaHTML += '</div></div>';
        }
        $(targetHTML).empty();
        $(targetHTML).append(areaHTML);
      } catch (e) {
        console.error(e);
      }
    }
  }

  RefreshAreas(areasState_new, areas, lastAreaState) {
    _TrackingManagementHelpers.ClearDragDrop();
    _TrackingManagementHelpers.SetNotSelectedRow();
    _TrackingManagementHelpers.ClearLayers(areas, AreaElements);

    for (let i = 0; i < areas.length; i++) {
      try {
        const trackingArea = _TrackingManagementHelpers.GetTrackingAreaByCode(areas, areas[i].Code);
        if (!trackingArea)
          throw ("There is a problem while refreshing areas. Area " + trackingArea.Code + " is not found. Check AREAS array.");
        let areaElement = _TrackingManagementHelpers.GetAreaElement(AreaElements, trackingArea.Code);
        if (!areaElement)
          throw ("There is a problem while refreshing areas. Area " + trackingArea.Name + " [" + trackingArea.Code + "] " + "cannot be found on screen. Check if area in on HMI.");
        if (trackingArea.Positions + trackingArea.VirtualPositions > (trackingArea.elementsInColumn * trackingArea.ElementsInColumn))
          throw ("There is a problem while refreshing areas. Area " + trackingArea.Name + " [" + trackingArea.Code + "] " + "does not have enought space to display materials. Number of regular and virtual positions is " + trackingArea.Positions + trackingArea.VirtualPositions + " but number of available positions on HMI is " + trackingArea.elementsInColumn * trackingArea.ElementsInColumn + ".");
        let newAreaState = areasState_new[areas[i].Code] || {};
        if (lastAreaState[trackingArea.Code] && lastAreaState[trackingArea.Code].HashCode && newAreaState.HashCode && lastAreaState[trackingArea.Code].HashCode === trackingArea.HashCode)
          continue;
        if (newAreaState.AreaId === TrackingAreaKeys.LAYER_AREA) {
          _TrackingManagementHelpers.PlaceLayers(newAreaState.AreaId, areaElement, trackingArea, newAreaState.Layers);
        } else {
          _AreasPrinter.PlaceMaterialsInArea(trackingArea, areaElement, newAreaState.Materials);
        }
      } catch (e) {
        console.error(e);
      }
    }

    _TrackingManagementHelpers.OnMaterialClickEvent();
    _TrackingManagementHelpers.InitDropTarget();
    _TrackingManagementHelpers.InitDragDrop();
  }

  InitAreaElements() {
    return $('.area');
  }

  GetLowerLimitForNotExpandableArea(elementsInColumn) {
    return Math.ceil(elementsInColumn / 2) - 1;
  }

  GetUpperLimitForNotExpandableArea(materialsNumber, elementsInColumn) {
    return materialsNumber - Math.floor(elementsInColumn / 2);
  }
}

const _TrackingManagementActions = new class {
  MoveMaterialsUp(el) {
    let assetCode = $(el).parent().parent().data('area');
    let action = "MoveAllMaterialsInAreaUp";
    let message = "Move backward all materials in area (Line should be stopped for tracking management)";

    let parameters = {
      AssetCode: assetCode
    };

    _TrackingManagementHelpers.SendRequestWithConfirmation(action, parameters, message);
  }

  MoveMaterialsDown(el) {

    let assetCode = $(el).parent().parent().data('area');
    let action = "MoveAllMaterialsInAreaDown";

    let message = "Move forward all materials in area (Line should be stopped for tracking management)";
    let parameters = {
      AssetCode: assetCode
    };

    _TrackingManagementHelpers.SendRequestWithConfirmation(action, parameters, message);
  }

  DeleteMaterial(el) {
    let matId = $(el).parent().data('mat-id');
    let matArea = $(el).parent().data('area-code');
    let parameters = {
      RawMaterialId: matId,
      AreaCode: matArea
    };

    OpenInPopupWindow({
      controller: "TrackingManagement", method: "RemoveRawMaterialPopup", width: 600, data: parameters
    });
  }

  MoveMaterial(dragAssetCode, dragOrderSeq, assetCode, orderSeq, rawMaterialId, matName, areaName) {
    let action = "UpdateMaterialPosition";
    let message = Translations["NAME_Material"] + ": " + matName + ", " + Translations["NAME_Position"] + ": " + (orderSeq === 0 ? Translations["NAME_Last"] : orderSeq) + ", " + Translations["NAME_Area"] + ": " + areaName;

    let parameters = {
      DragAssetCode: dragAssetCode,
      DragOrderSeq: dragOrderSeq,
      DropAssetCode: assetCode,
      DropOrderSeq: orderSeq,
      RawMaterialId: rawMaterialId
    }

    _TrackingManagementHelpers.SendRequestWithConfirmation(action, parameters, message);
  }

  AssignDefectsPopup() {
    if (!CurrentElement.RawMaterialId) {
      InfoMessage(Translations["MESSAGE_SelectElement"]);
      return;
    }
    OpenInPopupWindow({
      controller: "RawMaterial", method: "AssignDefectsPopup", width: 480, data: { rawMaterialId: CurrentElement.RawMaterialId }
    });
  }

  StartSlowProductionAction() {
    let action = 'StartSlowProduction';
    sendTrackingRequestWithConfirmation(action, "", null, () => { setSlowProduction(true) });
  }

  StopSlowProductionAction() {
    let action = 'StopSlowProduction';
    sendTrackingRequestWithConfirmation(action, "", null, () => { setSlowProduction(false) });
  }

  StartLaneCommunicationAction() {
    let action = 'StartLaneCommunication';
    sendTrackingRequestWithConfirmation(action, "", null, () => { setLaneCommunication(false) });
  }

  StopLaneCommunicationAction() {
    let action = 'StopLaneCommunication';
    sendTrackingRequestWithConfirmation(action, "", null, () => { setLaneCommunication(true) });
  }

  RefreshQuality() {
    let dataToSend = {
      RawMaterialId: CurrentElement.RawMaterialId
    };

    let url = "/InspectionStation/QualityView";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, _TrackingManagementActions.SetQualityPartialView);

    refreshSearchGrid();
  }

  SetQualityPartialView(partialView) {
    $('#quality-data').html(partialView);
  }

  GoToLayer(e) {
    let dataToSend = {
      layerId: e.id
    };
    openSlideScreen('Visualization', 'GetLayerViewById', dataToSend);
  }
}
