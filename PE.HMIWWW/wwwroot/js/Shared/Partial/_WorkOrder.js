const _WorkOrder = new class {

  GoToWorkOrder(workOrderId) {
    if (!workOrderId) {
      console.error('Missing data: workOrderId');
      return;
    }
    openSlideScreen('WorkOrder', 'ElementDetails', { WorkOrderId: workOrderId }, Translations["NAME_WorkOrder"]);
  }

  AddWorkOrderToSchedule(workOrderId, onSuccess) {
    if (!workOrderId) {
      console.error('Missing data: workOrderId');
      return;
    }

    let dataToSend = {
      workOrderId: workOrderId
    };
    let targetUrl = '/Schedule/AddWorkOrderToSchedule';

    AjaxReqestHelper(targetUrl, dataToSend, onSuccess, function () { console.log('addWorkOrderToSchedule - failed'); });
  }

  ColorEmptyL3MaterialAndSelectRow() {
  let grid = $("#SearchGrid").data("kendoGrid");
  let data = grid.dataSource.data();

  for (let i = 0; i < data.length; i++) {
    const isBlocked = data[i].IsBlocked;
    const isCanceled = data[i].UnCancellable;

    //if ( CurrentElement.WorkOrderId && workOrderId == CurrentElement.WorkOrderId) {
    //  $('tr[data-uid="' + data[i].uid + '"]').addClass('k-state-selected');
    //}
    if (isCanceled) {
      $('tr[data-uid="' + data[i].uid + '"]').css({ "background-color": "#f95554", "color": "#fff" });
    }
    if (isBlocked) {
      $('tr[data-uid="' + data[i].uid + '"]').css({ "background-color": "#808080", "color": "#fff" });
    }
  }
}
}

const _WorkOrder_SendL3 = new class {

  GoToSendWorkOrderToL3(workOrderId) {
    if (!workOrderId) {
      console.error('Missing data: workOrderId');
      return;
    }
    openSlideScreen('WorkOrder', 'SendWorkOrderToL3View', { workOrderId: workOrderId }, Translations["NAME_WorkOrderDetails"]);
  }

  selectRejectsTab() {
    let woTabStrip = $("#WorkOrderToL3Body").data("kendoTabStrip");
    woTabStrip.select('#Rejects');
  }

  sendWorkOrderToL3(workOrderId, onSuccessMethod = () => { }) {
    if (!workOrderId) {
      console.error('Missing data: workOrderId');
      return;
    }

    let parameters = {
      WorkOrderId: workOrderId,
      IsEndOfWorkShop: false
    }

    _RequestWithErrorHandler("/WorkOrder/SendWorkOrderReport", parameters, onSuccessMethod);
  }

  refreshWorkOrderL3Details(workOrderId) {
    if (!workOrderId) {
      console.error('Missing data: workOrderId');
      return;
    }
    AjaxReqestHelperSilentWithoutDataType("/WorkOrder/SendL3DetailsView", { workOrderId: workOrderId }, this.setWorkOrderL3Details);
  }

  setWorkOrderL3Details(partialView) {
    let $sendL3DetailsView = $('#WorkOrderToL3Body-1');
    if ($sendL3DetailsView.length) {
      $sendL3DetailsView.html(partialView);
    }
  }
}

function _RequestWithErrorHandler(targetUrl, dataToSend, onSuccessCustomMethod) {
  RequestStarted();
  $.ajax({
    type: 'POST',
    dataType: "json",
    url: targetUrl,
    traditional: true,
    data: dataToSend,
    complete: () => { },
    success: (data) => {
      RequestFinished();
      PositiveResultNotification();
      try {
        onSuccessCustomMethod(data);
      }
      catch (ex) { console.log(ex); }
    },
    error: (data) => {
      RequestFinished();
      PeErrorHandler(data);
    }
  });
}
