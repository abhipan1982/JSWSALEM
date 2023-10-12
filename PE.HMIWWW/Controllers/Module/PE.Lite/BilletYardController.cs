using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.BilletYard;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class BilletYardController : BaseController
  {
    private readonly IBilletYardService _billetYardService;

    #region ctor

    public BilletYardController(IBilletYardService service)
    {
      _billetYardService = service;
    }

    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public async Task<ActionResult> Index()
    {
      IList<VM_BilletYard> yards = await _billetYardService.GetYards();
      return View(yards);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public async Task<ActionResult> GetYardMapView(long id)
    {
      VM_BilletYardDetails locations = await _billetYardService.GetLocations(id);
      return PartialView("~/Views/Module/PE.Lite/BilletYard/_YardMap.cshtml", locations);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult GetRecepcionView()
    {
      return PartialView("~/Views/Module/PE.Lite/BilletYard/_Reception.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult GetChargingGridView()
    {
      return PartialView("~/Views/Module/PE.Lite/BilletYard/_ChargingGrid.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult GetScrappedView()
    {
      return PartialView("~/Views/Module/PE.Lite/BilletYard/_Scrapped.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public async Task<ActionResult> GetYardsView()
    {
      IList<VM_BilletYard> yards = await _billetYardService.GetYards();
      return PartialView("~/Views/Module/PE.Lite/BilletYard/_Yards.cshtml", yards);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult GetStackContenPartialtView(long id)
    {
      return PartialView("~/Views/Module/PE.Lite/BilletYard/_StackContent.cshtml",
        _billetYardService.GetLocationWithMaterials(id));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult GetBackToYardPartialView()
    {
      return PartialView("~/Views/Module/PE.Lite/BilletYard/_BackToYard.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult GetSchedulePartialView()
    {
      return PartialView("~/Views/Module/PE.Lite/BilletYard/_Schedule.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult GetRelocationPartialView()
    {
      return PartialView("~/Views/Module/PE.Lite/BilletYard/_Relocation.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult GetHeatsInLocalizationPartialView(long id)
    {
      return PartialView("~/Views/Module/PE.Lite/BilletYard/_HeatsInLocalizationGrid.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult GetHeatPanelPartialView(long heatId, short groupId, long locationId)
    {
      return PartialView("~/Views/Module/PE.Lite/BilletYard/_HeatDetailsPanel.cshtml",
        _billetYardService.GetHeat(heatId, groupId, locationId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult GetMaterialPanelPartialView(long id)
    {
      return PartialView("~/Views/Module/PE.Lite/BilletYard/_MaterialDetailsPanel.cshtml",
        _billetYardService.GetMaterial(id));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public async Task<ActionResult> GetHeatsOnYards([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _billetYardService.GetHeatsOnYards(ModelState, request));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public async Task<ActionResult> GetMaterialsInLocation(long locationId)
    {
      return await TaskPrepareJsonResultFromVm<VM_BilletLocationDetails, Task<VM_BilletLocationDetails>>(() =>
        _billetYardService.GetMaterialsInLocation(locationId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetHeatsInReception([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _billetYardService.GetHeatsInReception(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetHeatGroupsInLocations([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _billetYardService.GetHeatGroupsInLocations(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetHeatsInScrap([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _billetYardService.GetHeatsInScrap(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetHeatsInChargingGrid([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _billetYardService.GetHeatsInChargingGrid(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetLocations([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _billetYardService.GetLocations(ModelState, request));
    }

    public Task<JsonResult> GetScheduleGrid([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _billetYardService.GetScheduleGrid(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetCharginGridHeatsGrid([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(
        () => _billetYardService.GetCharginGridHeatsGrid(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public async Task<ActionResult> GetAddMaterialsPopup()
    {
      ViewBag.MaterialCataloguesList = await _billetYardService.GetMaterialCataloguesList();
      return PartialView("~/Views/Module/PE.Lite/BilletYard/_AddMaterialsPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult GetNumberOfMaterialsPopup(int materialsNumber)
    {
      return PartialView("~/Views/Module/PE.Lite/BilletYard/_NumberOfMaterialsPopup.cshtml", materialsNumber);
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> AddMaterials(VM_Materials data)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _billetYardService.AddMaterials(ModelState, data));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> TransferHeatIntoLocation(VM_HeatIntoLocation data)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _billetYardService.TransferHeatIntoLocation(ModelState, data));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult GetRegisterInYardPartialView()
    {
      return PartialView("~/Views/Module/PE.Lite/BilletYard/_RegisterInYard.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public async Task<ActionResult> GetSearchedLocationsIds(long heatId, long yardId)
    {
      return await TaskPrepareJsonResultFromVm<VM_SearchResult, Task<VM_SearchResult>>(() =>
        _billetYardService.SearchLocationIds(ModelState, heatId, yardId));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> TransferHeatIntoChargingGrid(VM_HeatIntoChargingGrid data)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _billetYardService.TransferHeatIntoChargingGrid(ModelState, data));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> TransferHeatFromChargingGrid(VM_HeatFromChargingGrid data)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _billetYardService.TransferHeatFromChargingGrid(ModelState, data));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> ScrapMaterials(VM_MaterialsScrap data)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _billetYardService.ScrapMaterials(ModelState, data));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> UnscrapMaterials(VM_MaterialsScrap data)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _billetYardService.UnscrapMaterials(ModelState, data));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> CreateHeatWithMaterials(VM_Heat heat)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _billetYardService.CreateHeatWithMaterials(ModelState, heat));
    }
  }
}
