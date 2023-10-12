using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Asset;
using PE.HMIWWW.ViewModel.Module.Lite.Furnace;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using SMF.HMIWWW.UnitConverter;
using SMF.Module.UnitConverter;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class FurnaceController : BaseController
  {
    private readonly IFurnaceService _furnaceService;

    public FurnaceController(IFurnaceService furnaceService)
    {
      _furnaceService = furnaceService;
    }

    // GET: Furnace
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Furnace, Constants.SmfAuthorization_Module_WalkingBeamFurnace,
      RightLevel.View)]
    public ActionResult Index()
    {
      ViewBag.MaxTemp = UOMHelper.SI2Local(GetParameter("FurnaceMaxTemp")?.ValueInt, VM_Resources.UNIT_Temperature);

      return View("~/Views/Module/PE.Lite/Furnace/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Furnace, Constants.SmfAuthorization_Module_WalkingBeamFurnace,
      RightLevel.View)]
    public JsonResult GetMaterialInFurnace()
    {
      return Json(_furnaceService.GetMaterialInFurnace());
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Furnace, Constants.SmfAuthorization_Module_WalkingBeamFurnace,
      RightLevel.View)]
    public List<VM_RawMaterialOverview> GetMaterialsInChargingArea([DataSourceRequest] DataSourceRequest request, List<long> materialsInAreas)
    {
      return _furnaceService.GetMaterialsInChargingArea(ModelState, materialsInAreas);
    }
    public PartialViewResult MaterialDetails(VM_Furnace model)
    {
      if (model.HeatId != null)
      {
        model.HeatOverview = _furnaceService.GetHeatDetails(ModelState, (long)model.HeatId);
      }

      if (model.WorkOrderId != null)
      {
        model.WorkOrderOverview = _furnaceService.GetWorkOrderDetails(ModelState, (long)model.WorkOrderId);
      }

      return PartialView("~/Views/Module/PE.Lite/Furnace/_RawMaterialDetails.cshtml", model);
    }


    #region Actions

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Furnace, Constants.SmfAuthorization_Module_WalkingBeamFurnace,
      RightLevel.Update)]
    public Task<JsonResult> StepForward()
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _furnaceService.StepForward(ModelState));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Furnace, Constants.SmfAuthorization_Module_WalkingBeamFurnace,
      RightLevel.Update)]
    public Task<JsonResult> StepBackward()
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _furnaceService.StepBackward(ModelState));
    }

    #endregion
  }
}
