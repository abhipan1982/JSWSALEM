using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class HeatController : BaseController
  {
    private readonly IHeatService _heatService;

    public HeatController(IHeatService service)
    {
      _heatService = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Heat/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long heatId)
    {
      return PrepareActionResultFromVm(() => _heatService.GetHeatDetails(ModelState, heatId),
        "~/Views/Module/PE.Lite/Heat/_HeatBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> GetHeatOverviewByWorkOrderId(long workOrderId)
    {
      return PrepareActionResultFromVm(() => _heatService.GetHeatOverviewByWorkOrderId(ModelState, workOrderId),
        "~/Views/Module/PE.Lite/Heat/_HeatDetails.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetHeatSearchList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _heatService.GetHeatOverviewList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetMaterialsListByHeatId([DataSourceRequest] DataSourceRequest request, long heatId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _heatService.GetMaterialsListByHeatId(ModelState, request, heatId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetWorkOrderListByHeatId([DataSourceRequest] DataSourceRequest request, long heatId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _heatService.GetWorkOrderListByHeatId(ModelState, request, heatId));
    }


    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> CreateHeat(VM_Heat heat)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _heatService.CreateHeat(ModelState, heat));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> EditHeat(VM_Heat heat)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _heatService.EditHeat(ModelState, heat));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> HeatCreatePopup()
    {
      ViewBag.SupplierList = _heatService.GetSupplierList();
      ViewBag.MaterialList = _heatService.GetMaterialList();

      await Task.CompletedTask;

      return PartialView("~/Views/Module/PE.Lite/Heat/_HeatCreatePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> HeatWithMaterialsCreatePopup()
    {
      ViewBag.SupplierList = _heatService.GetSupplierList();
      //ViewBag.MaterialList = _heatService.GetMaterialList();
      ViewBag.MaterialCataloguesList = _heatService.GetMaterialList();
      await Task.CompletedTask;

      return PartialView("~/Views/Module/PE.Lite/BilletYard/_CreateHeatWithMaterialsPopup.cshtml", new VM_Heat());
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Heat, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> HeatEditPopup(long id)
    {
      ViewBag.SupplierList = _heatService.GetSupplierList();
      ViewBag.MaterialList = _heatService.GetMaterialList();
      return PartialView("~/Views/Module/PE.Lite/Heat/_HeatEditPopup.cshtml", await _heatService.GetHeat(id));
    }

    public async Task<JsonResult> ServerFiltering_GetHeats(string text, bool isTest)
    {
      IList<VM_HeatAutocomplete> heats = await _heatService.GetHeatsByAnyFeaure(text, isTest);

      return Json(heats);
    }
  }
}
