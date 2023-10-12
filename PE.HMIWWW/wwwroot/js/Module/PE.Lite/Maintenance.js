//RegisterMethod(HmiRefreshKeys.Equipment, reloadKendoGrid);

var columns = ["Model", "AcquireDate", "DisposeDate", "DeviceGroup", "Components", "Status", "SerialNumber", "InstalationCycle"];
var button_array = $('.arrow-categories');
let CurrentElement;
let SelectedDeviceId = 0;
$('#InventorySection').hide();
$('#InventoryFunctions').hide();

const DATE_RANGE = {
	dateStart: new Date(),
	dateEnd: new Date(),
};
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

		});
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

	$('#MaintenanceDetailsFull').addClass('loading-overlay');
	var grid = e.sender;
	var selectedRow = grid.select();
	var selectedItem = grid.dataItem(selectedRow);
	var dataToSend = {
		ElementId: selectedItem.DeviceId
	};

	CurrentElement = {
		ElementId: selectedItem.DeviceId
	};

	SelectedDeviceId = selectedItem.DeviceId;

	var url = "/Maintenance/ElementDetailsInventory";
	AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView2);
	saveGridState(this);
}

function OnInstalledHide() {
	$('#InstalledSection').hide();
	$('#InventorySection').show();
	$('#InstalledFunctions').hide();
	$('#InventoryFunctions').show();
	$(".less").hide();
	}

function OnInventoryHide() {
	$('#InventorySection').hide();
	$('#InstalledSection').show();
	$('#InstalledFunctions').show();
	$('#InventoryFunctions').hide();
}

function getIncidentsData() {
	let datetimepickerFrom = $("#datetimepicker-from").data("kendoDateTimePicker");
	let datetimepickerFromValue = datetimepickerFrom.value();

	let datetimepickerTo = $("#datetimepicker-to").data("kendoDateTimePicker");
	let datetimepickerToValue = datetimepickerTo.value();

	DATE_RANGE.dateStart = datetimepickerFromValue;
	DATE_RANGE.dateEnd = datetimepickerToValue;

	let dateStartDisplayedFormat = DATE_RANGE.dateStart.toLocaleString();
	let dateEndDisplayedFormat = DATE_RANGE.dateEnd.toLocaleString();
	$.ajax({
		url: "/Maintenance/GetIncidentsForDevice",
		data: {
			deviceId: model.DeviceId,
			startDateTime: DATE_RANGE.dateStart.toISOString(),
			endDateTime: DATE_RANGE.dateEnd.toISOString(),
		},
		success: function (result) {
			let data = [];
			for (let i = 0; i < result.Data.length; i++) {
				let item = result.Data[i];
				console.log({
					"category": "1",
					"start": getDateIfDate(item.StartTime),
					"end": getDateIfDate(item.EndTime),
					"color": "#009000",
					"task": item.FKIncidentTypeId
				});
				debugger;
				data.push({
					"category": "1",
					"start": getDateIfDate(item.StartTime),
					"end": getDateIfDate(item.EndTime),
					"color": item.CategoryColor,
					"task": item.FKIncidentTypeId
				})
			}
			chart.data = data;
		}
	});
	setFilterOnGrid();
}

function getIncidentsSummaryForRange() {
	let datetimepickerFrom = $("#datetimepicker-from").data("kendoDateTimePicker");
	let datetimepickerFromValue = datetimepickerFrom.value();

	let datetimepickerTo = $("#datetimepicker-to").data("kendoDateTimePicker");
	let datetimepickerToValue = datetimepickerTo.value();

	DATE_RANGE.dateStart = datetimepickerFromValue;
	DATE_RANGE.dateEnd = datetimepickerToValue;

	let dateStartDisplayedFormat = DATE_RANGE.dateStart.toLocaleString();
	let dateEndDisplayedFormat = DATE_RANGE.dateEnd.toLocaleString();
	$.ajax({
		url: "/Maintenance/GetIncidentsForDevice",
		data: {
			deviceId: model.DeviceId,
	    startDateTime: DATE_RANGE.dateStart.toISOString(),
		endDateTime: DATE_RANGE.dateEnd.toISOString(),
		},
		success: function (result) {
			let data = [];
			for (let i = 0; i < result.Data.length; i++) {
				let item = result.Data[i];
				console.log({
					"category": "1",
					"start": getDateIfDate(item.StartTime),
					"end": getDateIfDate(item.EndTime),
					"color": "#009000",
					"task": item.FKIncidentTypeId
				});
				debugger;
				data.push({
					"category": "1",
					"start": getDateIfDate(item.StartTime),
					"end": getDateIfDate(item.EndTime),
					"color": item.CategoryColor,
					"task": item.FKIncidentTypeId
				})
			}
			chart.data = data;
		}
	});

}

function getIncidentsSummaryForToday() {
	setTodayDateRange();
	getIncidentsData();
	setFilterOnGrid();
}

function getIncidentsSummaryForYesterday() {
	setYesterdayDateRange();
	getIncidentsData();
	setFilterOnGrid();
}

function getIncidentsSummaryForLastWeek() {
	setLastWeekDateRange();
	getIncidentsData();
	setFilterOnGrid();
}

function setYesterdayDateRange() {
	DATE_RANGE.dateStart = new Date();
	DATE_RANGE.dateStart.setDate(new Date().getDate() - 1);
	DATE_RANGE.dateStart.setHours(0, 0, 0, 0);
	DATE_RANGE.dateEnd = new Date();
	DATE_RANGE.dateEnd.setHours(0, 0, 0, 0);

	let dateDisplayedFormat = new Date();
	dateDisplayedFormat.setDate(new Date().getDate() - 1);
	dateDisplayedFormat = dateDisplayedFormat.toLocaleDateString();

	$("#data-description").text(dateDisplayedFormat);

	$("#datetimepicker-from").kendoDateTimePicker({
		value: new Date(DATE_RANGE.dateStart.getFullYear(), DATE_RANGE.dateStart.getMonth(), DATE_RANGE.dateStart.getDate(), 0, 0, 0)
	});
	$("#datetimepicker-to").kendoDateTimePicker({
		value: new Date(DATE_RANGE.dateEnd.getFullYear(), DATE_RANGE.dateEnd.getMonth(), DATE_RANGE.dateEnd.getDate(), 0, 0, 0)
	});
}

function setTodayDateRange() {
	DATE_RANGE.dateStart = new Date();
	DATE_RANGE.dateStart.setHours(0, 0, 0, 0);
	DATE_RANGE.dateEnd = new Date();
	DATE_RANGE.dateEnd.setDate(new Date().getDate() + 1);
	DATE_RANGE.dateEnd.setHours(0, 0, 0, 0);

	let dateDisplayedFormat = new Date();
	dateDisplayedFormat = dateDisplayedFormat.toLocaleDateString();

	$("#data-description").text(dateDisplayedFormat);

	$("#datetimepicker-from").kendoDateTimePicker({
		value: new Date(DATE_RANGE.dateStart.getFullYear(), DATE_RANGE.dateStart.getMonth(), DATE_RANGE.dateStart.getDate(), 0, 0, 0)
	});
	$("#datetimepicker-to").kendoDateTimePicker({
		value: new Date(DATE_RANGE.dateEnd.getFullYear(), DATE_RANGE.dateEnd.getMonth(), DATE_RANGE.dateEnd.getDate(), 0, 0, 0)
	});

}

function setLastWeekDateRange() {
	DATE_RANGE.dateStart = new Date();
	DATE_RANGE.dateStart.setDate(new Date().getDate() - 5);
	DATE_RANGE.dateStart.setHours(0, 0, 0, 0);
	DATE_RANGE.dateEnd = new Date();
	DATE_RANGE.dateEnd.setDate(new Date().getDate() + 1);
	DATE_RANGE.dateEnd.setHours(0, 0, 0, 0);

	let dateStartDisplayedFormat = new Date();
	dateStartDisplayedFormat.setDate(new Date().getDate() - 6);
	dateStartDisplayedFormat = dateStartDisplayedFormat.toLocaleDateString();

	let dateEndDisplayedFormat = new Date();
	dateEndDisplayedFormat = dateEndDisplayedFormat.toLocaleDateString();

	$("#data-description").text(dateStartDisplayedFormat + "  -  " + dateEndDisplayedFormat);

	$("#datetimepicker-from").kendoDateTimePicker({
		value: new Date(DATE_RANGE.dateStart.getFullYear(), DATE_RANGE.dateStart.getMonth(), DATE_RANGE.dateStart.getDate(), 0, 0, 0)
	});
	$("#datetimepicker-to").kendoDateTimePicker({
		value: new Date(DATE_RANGE.dateEnd.getFullYear(), DATE_RANGE.dateEnd.getMonth(), DATE_RANGE.dateEnd.getDate(), 0, 0, 0)
	});
}

function setFilterOnGrid() {
	var ds = null;
	if ($("#IncidentGrid").length > 0) {
		ds = $("#IncidentGrid").data("kendoGrid").dataSource;
    }
	if ($("#IncidentGridInventory").length > 0) {
		ds = $("#IncidentGridInventory").data("kendoGrid").dataSource;
	}
	var curr_filters = null;
	if (ds.filter() != null) {
		curr_filters = [];
	}
	if (curr_filters == null) {
		curr_filters = [];
	}

	//create new filter object
	var new_filter = { field: "StartTime", operator: "gte", value: new Date(DATE_RANGE.dateStart.getFullYear(), DATE_RANGE.dateStart.getMonth(), DATE_RANGE.dateStart.getDate(), 0, 0, 0) } ;
	//add new_filter to filters
	curr_filters.push(new_filter);
	//apply the filters
	ds.filter(curr_filters);
	var new_filter2 = { field: "EndTime", operator: "lte", value: new Date(DATE_RANGE.dateEnd.getFullYear(), DATE_RANGE.dateEnd.getMonth(), DATE_RANGE.dateEnd.getDate(), 0, 0, 0) };
	//add new_filter to filters
	curr_filters.push(new_filter2);
	//apply the filters
	ds.filter(curr_filters);
}

function OnTreeElementSelect(e) {
	//hideCategories();
	var node = e.node;
	var itemId = e.node.getAttribute("data-id");
	var uid = $(node).closest("li").data("uid");
	var item = this.dataSource.getByUid(uid);
	itemId = item.id;
	if (itemId != 0) {
		if ($('.k-i-arrow-left').length) {
			button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
			$('.more').show(100);
			$('.less').hide(100);
		}

		$('#MaintenanceDetails').addClass('loading-overlay');
		var grid = e.sender;
		var selectedRow = grid.select();
		var selectedItem = grid.dataItem(selectedRow);
		var dataToSend = {
			ElementId: itemId
		};

		CurrentElement = {
			ElementId: itemId
		};

		SelectedDeviceId = itemId;

		var url = "/Maintenance/ElementDetails";
		AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
	}
	else {
		var url = "/Maintenance/ElementDetailsEmpty";
		var dataToSend = {
			ElementId: 0
		};
		AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
    }
	//saveGridState(this);
}

function setElementDetailsPartialView(partialView) {
	$('#MaintenanceDetails').removeClass('loading-overlay');
	$('#MaintenanceDetails').html(partialView);
	//setTabStripsStates();
}

function setElementDetailsPartialView2(partialView) {
	$('#MaintenanceDetailsFull').removeClass('loading-overlay');
	$('#MaintenanceDetailsFull').html(partialView);
	setTabStripsStates();
}


function reloadKendoGrid() {
	let grid = $('#EquipmentGrid').data('kendoGrid');
	grid.dataSource.read();
	grid.refresh();
}

function Grid_OnRowSelect(e) {
	if ($('.k-i-arrow-left').length) {
		button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
		$('.more').show(100);
		$('.less').hide(100);
	}

	$('#MaintenanceDetails').addClass('loading-overlay');
	var grid = e.sender;
	var selectedRow = grid.select();
	var selectedItem = grid.dataItem(selectedRow);
	var dataToSend = {
		ElementId: itemId
	};

	CurrentElement = {
		ElementId: itemId
	};

	SelectedDeviceId = itemId;

	var url = "/Maintenance/ElementDetails";	
	AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}
function ColorRowInTable() {
	var grid = $("#EquipmentGrid").data("kendoGrid");
	var gridData = grid.dataSource.view();

	for (var i = 0; i < gridData.length; i++) {
		var currentUid = gridData[i].uid;
		var currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");
		var rowColor, rowBgColor;
		if (gridData[i].IsOverdue) {
			rowBgColor = 'red';
			rowColor = 'white';

			currenRow.css({ 'background': rowBgColor });
			currenRow.css({ 'color': rowColor });
		}
		else if(gridData[i].IsWarned) {
			rowBgColor = '#fcbe03';
			rowColor = 'white';

			currenRow.css({ 'background': rowBgColor });
			currenRow.css({ 'color': rowColor });
		}
	}
}

//Temp-------------------------------------------------------------
function EditEquipmentPopup(id) {
	OpenInPopupWindow({
		controller: "Equipment", method: "EquipmentEditPopupAsync", width: 600, data: { id: id }, afterClose: reloadKendoGrid
	});
}

function EditEquipmentStatusPopup(id) {
	OpenInPopupWindow({
		controller: "Equipment", method: "EquipmentStatusEditPopupAsync", width: 600, data: { id: id }, afterClose: reloadKendoGrid
	});
}

function CloneEquipmentPopup(id) {
	OpenInPopupWindow({
		controller: "Equipment", method: "EquipmentClonePopupAsync", width: 600, data: { id: id }, afterClose: reloadKendoGrid
	});
}
//------------------------------------------------------------------

//Device--------------------------------------------------------
function AddNewDevice() {
	OpenInPopupWindow({
		controller: "Maintenance", method: "AddDevicePopupAsync", width: 600, afterClose: reloadKendoGrid
	});
}
function EditDevice() {
	if (SelectedDeviceId != 0) {
		OpenInPopupWindow({
			controller: "Maintenance", method: "EditDevicePopupAsync", width: 600, data: { id: SelectedDeviceId }, afterClose: reloadKendoGrid
		});
	}
}
function InstallDevice() {
	if (SelectedDeviceId != 0) {
		OpenInPopupWindow({
			controller: "Maintenance", method: "InstallDevicePopupAsync", width: 600, height:800, data: { id: SelectedDeviceId }, afterClose: reloadKendoGrid
		});
	}
}
function UninstallDevice() {
	if (SelectedDeviceId != 0) {
		var functionName = UninstallDevice2Confirm;
		var action = 'UninstallDevice';
		PromptMessage(Translations["MESSAGE_UninstallDeviceConfirm"], "", () => { return functionName(itemId, action) });
	}
}

function UninstallDevice2Confirm(itemId, action) {

	var url = serverAddress + "/Maintenance/" + action;
	var data = { Id: itemId };

	AjaxReqestHelper(url, data, RefreshData);
}
//--------------------------------------------------------------



//Components--------------------------------------------------------
function AddNewComponent() {
	OpenInPopupWindow({
		controller: "Maintenance", method: "AddComponentPopupAsync", width: 600, afterClose: reloadKendoGrid
	});
}

function ModifyComponent() {
	var id = 
	OpenInPopupWindow({
		controller: "Maintenance", method: "ModifyComponentPopupAsync", width: 600, data: { id: id }, afterClose: reloadKendoGrid
	});
}

function AddComponent() {
	OpenInPopupWindow({
		controller: "Maintenance", method: "AddModifyComponentPopupAsync", width: 600, afterClose: reloadKendoGrid
	});
}

function DeleteComponent(itemId) {
	var functionName = DeleteComponent2Confirm;
	var action = 'DeleteComponent';
	PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action) });
}

function DeleteComponent2Confirm(itemId, action) {

	var url = serverAddress + "/Maintenance/" + action;
	var data = { Id: itemId };

	AjaxReqestHelper(url, data, RefreshData);
}
//---------------------------------------------------------------

//Actions--------------------------------------------------------
function AddNewAction() {
	OpenInPopupWindow({
		controller: "Maintenance", method: "AddActionPopupAsync", width: 600, afterClose: reloadKendoGrid
	});
}

function ModifyAction(id) {
	OpenInPopupWindow({
		controller: "Maintenance", method: "ModifyActionPopupAsync", width: 600, data: { id: id }, afterClose: reloadKendoGrid
	});
}
function DeleteAction(itemId) {
	var functionName = DeleteAction2Confirm;
	var action = 'DeleteAction';
	PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action) });
}

function DeleteAction2Confirm(itemId, action) {

	var url = serverAddress + "/Maintenance/" + action;
	var data = { Id: itemId };

	AjaxReqestHelper(url, data, RefreshData);
}
//--------------------------------------------------------------


function ClearFilters() {
	if ($("#IncidentGrid").length > 0) {
		ds = $("#IncidentGrid").data("kendoGrid").dataSource;
	}
	if ($("#IncidentGridInventory").length > 0) {
		ds = $("#IncidentGridInventory").data("kendoGrid").dataSource;
	}
	var curr_filters = null;
	if (ds.filter() != null) {
		curr_filters = [];
	}
	if (curr_filters == null) {
		curr_filters = [];
	}

	//apply the filters
	ds.filter(curr_filters);
}

function ModifyIncidentDetails(id) {
	OpenInPopupWindow({
		controller: "Maintenance", method: "ModifyIncidentDetailsPopupAsync", width: 600, data: { id: id },afterClose: reloadKendoGrid
	});
}

function OnIncidentTypeIdChange(e) {
	var elementId = e.sender.dataItem().Value;
	$.ajax({
		url: "/Maintenance/GetIncidentTypeDetails",
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


