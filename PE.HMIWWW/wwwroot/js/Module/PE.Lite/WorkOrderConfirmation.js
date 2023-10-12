let CurrentElement;

var columns = ["StartTime", "EndTime", "PlannedEndTime"];

var button_array = $('.arrow-categories');

function onElementSelect(e) {
    hideCategories();

    if ($('.k-i-arrow-left').length) {
        button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
        $('.more').show(100);
        $('.less').hide(100);
    }

    $('#ShiftDetails').addClass('loading-overlay');
    var grid = e.sender;
    var selectedRow = grid.select();
    var selectedItem = grid.dataItem(selectedRow);
    var dataToSend = {
        ShiftId: selectedItem.ShiftId
    };

    CurrentElement = {
        ShiftId: selectedItem.ShiftId
    };

    var url = "/WorkOrderConfirmation/ElementDetails";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function onElementReload() {
    var dataToSend = {
        ShiftId: CurrentElement.ShiftId
    };

    var url = "/WorkOrderConfirmation/ElementDetails";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
    $('#ShiftDetails').removeClass('loading-overlay');
    $('#ShiftDetails').html(partialView);


  let chart = $("#chart").data("kendoChart");
    
    if (chart) {
      chart.options.series[0].data[0].visible = false;
      for (let i = 0; i < $("#chart").data("kendoChart").options.series[0].data.length; i++) {
        if ($("#chart").data("kendoChart").options.series[0].data[i].value === 0 ||
          $("#chart").data("kendoChart").options.series[0].data[i].value === null) {
          $("#chart").data("kendoChart").options.series[0].data[i].visible = false;
          $("#chart").data("kendoChart").options.series[0].data[i].visibleInLegend = false;
          $("#chart").data("kendoChart").options.series[0].overlay.gradient = false;
        }
      }
    }
}


//function AddNewHeat() {
//    ClosePopup();
//    let grid = $('#FKHeatIdRef');
//    grid.hide();
//    OpenInPopupWindow({
//        controller: "Heat", method: "HeatCreatePopup", width: 1250, afterClose: reloadKendoGrid
//    });
//}

function reloadKendoGrid() {
    let grid = $('#SearchGrid').data('kendoGrid');
    grid.dataSource.read();
    grid.refresh();

    onElementReload();

    //let materials = $('#MaterialGrading').data('kendoGrid');
    //materials.dataSource.read();
    //materials.refresh();

    let workorderbody = $('#WorkOrderBody').data('kendoTabStrip');
    workorderbody.reload();
    workorderbody.refresh();
    
}

function onAdditionalData() {
    return {
        text: $("#Heat").val()
    };
}

//heat autocomplete autohide
$(function () {

    $('#error').css("display", "none");
    $("#form").kendoValidator().data("kendoValidator");

    $('.k-autocomplete').css("width", "150px");

  $('.k-autocomplete input').keydown(function () {
    $('.k-autocomplete').animate({
      width: 400
    }, 200, function () {
      // Animation complete.
    });
  });

  $('.k-autocomplete input').focusout(function () {
    $('.k-autocomplete').animate({
      width: 150
    }, 400, function () {
      // Animation complete.
    });
  });


});

function displayMessage() {
    var validator = $("#form").kendoValidator().data("kendoValidator");

    if (validator.validate()) {
        $('#error').css("display", "none");
    } else {
        $('#error').css("display", "block");
        $('#popup-footer')
            .css('display', 'block')
            .css('background-color', 'rgb(206, 0, 55)');
    }
}

$(function () {

    $('#error').css("display", "none");
    $("#form").kendoValidator().data("kendoValidator");

});

function ColorRowInTable() {
  var grid = $("#SearchGrid").data("kendoGrid");
  var gridData = grid.dataSource.view();

  for (var i = 0; i < gridData.length; i++) {
    var currentUid = gridData[i].uid;
    var currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");
    var rowColor, rowBgColor;
    if (gridData[i].IsBlocked) {
      rowBgColor = '#00B5E2';
      rowColor = 'white';

      currenRow.css({ 'background': rowBgColor });
      currenRow.css({ 'color': rowColor });
    }

  }

}

function ShowDelaysWindow(shiftId, workOrderId) {
    let dataToSend = {
        ShiftId: shiftId,
        WorkOrderId: workOrderId
    };
    openSlideScreen('Delays', 'ElementDetails', dataToSend);
}

function DelayEditPopup(id) {
    OpenInPopupWindow({
        controller: "Delays", method: "DelayEditPopup", width: 600, data: { id: id }, afterClose: reloadKendoGrid
    });
}

function SendWorkOrderToL3WithConfirmationPopup() {
    if (!CurrentElement.ShiftId) {
        InfoMessage(Translations["MESSAGE_SelectElement"]);
        return;
    }
    let url = URL("WorkOrderConfirmation", "SendWorkOrderToL3");

    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], message, () => {
        AjaxReqestHelperSilentWithoutDataType(url, { shiftId: CurrentElement.ShiftId }, onSuccessMethod);
    });
}
