var requestStatusBar;

function InitNotification () {

    requestStatusBar = new $.peekABar();
    SignalrConnection.on("System2HmiNotification", (notification) => {
        try {
            RefreshData();
        }
        catch (ex) {
            console.trace(ex);
        }

        if (notification.NotificationTarget === 1) {
            if (notification.NotificationType == 1) {
                SuccessNotification(notification.NotivicationText);
            }
            else
                ErrorNotification(notification.NotivicationText);
        }
    });
}





//ERROR MESSAGES
function WarningMessage(message) {
	swal({
    title: Translations["warning"], text: message, icon: "warning", confirmButtonText: "OK"
	});
}
function ErrorMessage(message) {
  let htmlElement = document.createElement("div");
  htmlElement.innerHTML = message;
	swal({
    title: Translations["error"],
    icon: "error",
    confirm: {
      text: "OK",
      value: true,
      visible: true,
      className: "",
      closeModal: true
    },
    content: htmlElement
	});
}
function InfoMessage(message) {
  let htmlElement = document.createElement("div");
  htmlElement.innerHTML = message;
	swal({
    title: Translations["info"],
    icon: "info",
    confirm: {
      text: "OK",
      value: true,
      visible: true,
      className: "",
      closeModal: true
    },
    content: htmlElement
	});
}
function SuccessMessage(message) {
	swal({
    title: Translations["success"], text: message, icon: "success", timer: 2000,
    confirm: {
      text: "OK",
      value: true,
      visible: true,
      className: "",
      closeModal: true
    }
	});
}
function PromptMessage(question, information, onConfirm, onCancel) {
  //swal({
  //  title: question,
  //  text: information,
  //  type: "warning",
  //  showCancelButton: true,
  //  confirmButtonColor: "#f2a900",
  //  confirmButtonText: Translations["yes"],
  //  cancelButtonText: Translations["cancel"],
  //  closeOnConfirm: true,
  //  closeOnCancel: true
  //}, functionToRun);

  swal({
    title: question,
    text: information,
    icon: "warning",
    buttons: true,
    dangerMode: true,
  })
    .then((clicked) => {
      if (clicked) {
        onConfirm();
      } else {
        if (onCancel) onCancel();
      }
    });

	//http://t4t5.github.io/sweetalert/
	//swal({ title: "An input!", text: "Write something interesting:", type: "input", showCancelButton: true, closeOnConfirm: false, animation: "slide-from-top", inputPlaceholder: "Write something" }, function (inputValue) { if (inputValue === false) return false; if (inputValue === "") { swal.showInputError("You need to write something!"); return false } swal("Nice!", "You wrote: " + inputValue, "success"); });
}
function AlarmMessage(message, alarmType, confirmation, functionToRun) {

    if (confirmation)
    {
        if (alarmType == 3)
            alarmTypeText = "error";
        else if (alarmType == 2)
            alarmTypeText = "warning";
        else if (alarmType == 1)
            alarmTypeText = "info";

      swal({ title: Translations[("alarmtype_" + alarmType)], text: message, type: alarmTypeText, showCancelButton: true, confirmButtonColor: "#f2a900", confirmButtonText: Translations["alarmConfirm"], cancelButtonText: Translations["close"], closeOnConfirm: true })
        .then(confirmed => {
          if (confirmed) {
            functionToRun();
          }
        });
    }
    else
    {
        if (alarmType == 3)
            ErrorMessage(message);
        else if (alarmType == 2)
            WarningMessage(message);
        else if (alarmType == 1)
            InfoMessage(message);
    }

}
//END OF ERROR MESSAGES



//NOTIFICATION MESSAGES

function ErrorNotification(messageText) {
	var notification = $("#notification").data("kendoNotification");
	notification.show({
	    title: Translations["error"],
		message: messageText
	}, "error");
}
function SuccessNotification(messageText) {
	var notification = $("#notification").data("kendoNotification");
	notification.show({
	    title: Translations["success"],
		message: messageText
	}, "success");
}

function PositiveResultNotification() {
        SuccessNotification(Translations["operationSuccessful"]);
}

function NegativeResultNotification() {
  ErrorNotification(Translations["operationFailed"]);
}

//END OF NOTIFICATION MESSAGES

//REQUEST STATUS MESSAGES

function RequestStarted() {
  $('body').css('pointer-events', 'none');
	requestStatusBar.show({
		html: '<div><img src="/css/System/Img/ajax-loader.gif" /></div>',
		backgroundColor: '#f2a900'
	});
}
function RequestFinished() {
  $('body').css('pointer-events', 'auto');
	requestStatusBar.hide();
}


//END OF REQUEST STATUS MESSAGES


