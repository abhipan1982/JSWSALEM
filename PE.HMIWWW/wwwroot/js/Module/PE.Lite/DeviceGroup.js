RegisterMethod(HmiRefreshKeys.Equipment, reloadKendoGrid);

var columns = ["DeviceGroupDescription", "CreatedTs", "LastUpdateTs"];
var button_array = $('.arrow-categories');
let CurrentElement;


function showHideCategories() {
	var button = $('.show-hide-categories');
	var grid = $("#SearchGrid").data("kendoGrid");
	var button_array = $('.arrow-categories');
	if (button.hasClass('off')) {
		$('.more').hide(100);
		$('.less').show(100);	
		$('.element-details').hide(100);
		columns.forEach(function (element) {
			grid.showColumn(element);
		});
		$('.grid-filter').toggleClass('col-11 col-3', 750, function () {

		})
	}
	if (button.hasClass('on')) {
		$('.more').show(100);
		$('.less').hide(100);
		columns.forEach(function (element) {
			grid.hideColumn(element);
		});
		$('.grid-filter').toggleClass('col-11 col-3', 650, function () {

			$('.element-details').show(650);
		});
	}
	button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
	button.toggleClass('off on');
}
function hideCategories() {
	if (!$(".grid-filter").is(":animated")) {
		var button = $('.show-hide-categories');
		if (button.hasClass('on')) {
			var grid = $("#SearchGrid").data("kendoGrid");
			var button_array = $('.arrow-categories');
			button_array.toggleClass('right left');
			button.toggleClass('on off');
			columns.forEach(function (element) {
				grid.hideColumn(element);
			});
			$('.grid-filter').toggleClass('col-11 col-3', 650, function () {
				$('.element-details').show(650);
			});
		}
	}
}
function onElementSelect(e) {
	hideCategories();

	if ($('.k-i-arrow-left').length) {
		button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
		$('.more').show(100);
		$('.less').hide(100);
	}

	$('#DeviceGroupDetails').addClass('loading-overlay');
	var grid = e.sender;
	var selectedRow = grid.select();
	var selectedItem = grid.dataItem(selectedRow);
	var dataToSend = {
		ElementId: selectedItem.DeviceGroupId
	};

	CurrentElement = {
		ElementId: selectedItem.DeviceGroupId
	};

	var url = "/DeviceGroup/ElementDetails";
	AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
	saveGridState(this);
}


function setElementDetailsPartialView(partialView) {
	debugger;
	$('#DeviceGroupDetails').removeClass('loading-overlay');
	$('#DeviceGroupDetails').html(partialView);
	setTabStripsStates();
}


function reloadKendoGrid() {
	let grid = $('#SearchGrid').data('kendoGrid');
	grid.dataSource.read();
	grid.refresh();
}

function Grid_OnRowSelect(e) {
	if ($('.k-i-arrow-left').length) {
		button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
		$('.more').show(100);
		$('.less').hide(100);
	}

	$('#DeviceGroupDetails').addClass('loading-overlay');
	var grid = e.sender;
	var selectedRow = grid.select();
	var selectedItem = grid.dataItem(selectedRow);
	var dataToSend = {
		ElementId: itemId
	};

	CurrentElement = {
		ElementId: itemId
	};

	var url = "/DeviceGroup/ElementDetails";
	AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}


function AddNew() {
	OpenInPopupWindow({
		controller: "DeviceGroup", method: "AddNewDeviceGroupPopupAsync", width: 600, afterClose: reloadKendoGrid
	});
}

function EditDeviceGroup(id) {
	OpenInPopupWindow({
		controller: "DeviceGroup", method: "ModifyDeviceGroupDetailsPopupAsync", width: 600, data: { id: id },afterClose: reloadKendoGrid
	});
}

function OnDeviceGroupIdChange(e) {
	var elementId = e.sender.dataItem().Value;
	$.ajax({
		url: "/DeviceGroup/GetDeviceGroupDetails",
		data: {
			incidentTypeId: elementId
		},
		success: function (result) {
			let item = result.Data[0];
			$("#incidentTypeCode").html(item.IncidentTypeCode);
			$("#IncidentTypeDescription").html(item.IncidentTypeDescription);

		}
	});
}

function dataSourceChange() {

}

function Delete(id) {
	PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
		let dataToSend = {
			id: id
		};
		let targetUrl = '/Equipment/DeleteEquipmentAsync';

		AjaxReqestHelper(targetUrl, dataToSend, reloadKendoGrid, function () { console.log('DeleteEquipment - failed'); });
	});
}

function ShowHistory(id) {
	let dataToSend = {
		id: id
	};
	openSlideScreen('Equipment', 'ShowEquipmentHistory', dataToSend);
}

function DeleteDeviceGroup(itemId) {
	var functionName = DeleteDeviceGroup2Confirm;
	var action = 'DeleteDeviceGroup';
	PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action) });
}

function DeleteDeviceGroup2Confirm(itemId, action) {

	var url = serverAddress + "/DeviceGroup/" + action;
	var data = { Id: itemId };

	AjaxReqestHelper(url, data, RefreshData);
}

function RefreshData() {

}
