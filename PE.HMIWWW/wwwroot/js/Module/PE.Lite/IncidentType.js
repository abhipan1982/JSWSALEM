RegisterMethod(HmiRefreshKeys.Equipment, reloadKendoGrid);

var columns = ["IncidentTypeDescription", "CreatedTs", "LastUpdateTs"];
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

	$('#IncidentTypeDetails').addClass('loading-overlay');
	var grid = e.sender;
	var selectedRow = grid.select();
	var selectedItem = grid.dataItem(selectedRow);
	var dataToSend = {
		ElementId: selectedItem.IncidentTypeId
	};

	CurrentElement = {
		ElementId: selectedItem.IncidentTypeId
	};

	var url = "/IncidentType/ElementDetails";
	AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
	saveGridState(this);
}


function setElementDetailsPartialView(partialView) {
	$('#IncidentTypeDetails').removeClass('loading-overlay');
	$('#IncidentTypeDetails').html(partialView);
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

	$('#IncidentTypeDetails').addClass('loading-overlay');
	var grid = e.sender;
	var selectedRow = grid.select();
	var selectedItem = grid.dataItem(selectedRow);
	var dataToSend = {
		ElementId: itemId
	};

	CurrentElement = {
		ElementId: itemId
	};

	var url = "/IncidentType/ElementDetails";
	AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}


function AddNew() {
	OpenInPopupWindow({
		controller: "IncidentType", method: "AddNewIncidentTypePopupAsync", width: 600, afterClose: reloadKendoGrid
	});
}

function ModifyIncidentDetails(id) {
	OpenInPopupWindow({
		controller: "IncidentType", method: "ModifyIncidentTypeDetailsPopupAsync", width: 600, data: { id: id },afterClose: reloadKendoGrid
	});
}

function OnIncidentTypeIdChange(e) {
	var elementId = e.sender.dataItem().Value;
	$.ajax({
		url: "/IncidentType/GetIncidentTypeDetails",
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

function EditIncidentType(id) {
	OpenInPopupWindow({
		controller: "IncidentType", method: "ModifyIncidentTypePopupAsync", width: 600, data: { id: id }, afterClose: reloadKendoGrid
	});
}

function AddIncidentType() {
	OpenInPopupWindow({
		controller: "IncidentType", method: "AddNewIncidentTypePopupAsync", width: 600, afterClose: reloadKendoGrid
	});
}


function DeleteIncidentType(itemId) {
	var functionName = DeleteIncidentType2Confirm;
	var action = 'DeleteIncidentType';
	PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action) });
}

function DeleteIncidentType2Confirm(itemId, action) {

	var url = serverAddress + "/IncidentType/" + action;
	var data = { Id: itemId };

	AjaxReqestHelper(url, data, RefreshData);
}
function EditRecommendedAction(id) {
	OpenInPopupWindow({
		controller: "IncidentType", method: "ModifyRecommendedActionPopupAsync", width: 600, data: { id: id }, afterClose: reloadKendoGrid
	});
}

function AddRecommendedAction() {
	OpenInPopupWindow({
		controller: "IncidentType", method: "AddNewRecommendedActionPopupAsync", width: 600, afterClose: reloadKendoGrid
	});
}

function DeleteRecommendedAction(itemId) {
	var functionName = DeleteRecommendedAction2Confirm;
	var action = 'DeleteRecommendedAction';
	PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action) });
}

function DeleteRecommendedAction2Confirm(itemId, action) {

	var url = serverAddress + "/IncidentType/" + action;
	var data = { Id: itemId };

	AjaxReqestHelper(url, data, RefreshData);
}
