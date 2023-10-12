RegisterMethod(HmiRefreshKeys.Equipment, reloadKendoGrid);

var columns = [ "CreatedTs", "LastUpdateTs"];
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

	$('#RecommendedActionDetails').addClass('loading-overlay');
	var grid = e.sender;
	var selectedRow = grid.select();
	var selectedItem = grid.dataItem(selectedRow);
	var dataToSend = {
		ElementId: selectedItem.RecommendedActionId
	};

	CurrentElement = {
		ElementId: selectedItem.RecommendedActionId
	};

	var url = "/RecommendedAction/ElementDetails";
	AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
	saveGridState(this);
}


function setElementDetailsPartialView(partialView) {
	$('#RecommendedActionDetails').removeClass('loading-overlay');
	$('#RecommendedActionDetails').html(partialView);
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

	$('#RecommendedActionDetails').addClass('loading-overlay');
	var grid = e.sender;
	var selectedRow = grid.select();
	var selectedItem = grid.dataItem(selectedRow);
	var dataToSend = {
		ElementId: itemId
	};

	CurrentElement = {
		ElementId: itemId
	};

	var url = "/RecommendedAction/ElementDetails";
	AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}


function AddNew() {
	OpenInPopupWindow({
		controller: "RecommendedAction", method: "AddNewRecommendedActiontPopupAsync", width: 600, afterClose: reloadKendoGrid
	});
}

function ModifyIncidentDetails(id) {
	OpenInPopupWindow({
		controller: "RecommendedAction", method: "ModifyRecommendedActionDetailsPopupAsync", width: 600, data: { id: id },afterClose: reloadKendoGrid
	});
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

function EditRecommendedAction(id) {
	OpenInPopupWindow({
		controller: "RecommendedAction", method: "ModifyRecommendedActionPopupAsync", width: 600, data: { id: id }, afterClose: reloadKendoGrid
	});
}

function AddRecommendedAction() {
	OpenInPopupWindow({
		controller: "RecommendedAction", method: "AddNewRecommendedActionPopupAsync", width: 600, afterClose: reloadKendoGrid
	});
}


function DeleteRecommendedAction(itemId) {
	var functionName = DeleteRecommendedAction2Confirm;
	var action = 'DeleteRecommendedAction';
	PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action) });
}

function DeleteRecommendedAction2Confirm(itemId, action) {

	var url = serverAddress + "/RecommendedAction/" + action;
	var data = { Id: itemId };

	AjaxReqestHelper(url, data, RefreshData);
}
