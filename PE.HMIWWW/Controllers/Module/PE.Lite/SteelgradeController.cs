using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.SteelFamily;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class SteelgradeController : BaseController
  {
    private readonly ISteelgradeService _service;

    public SteelgradeController(ISteelgradeService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Steelgrade/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetSteelgradeList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetSteelgradeList(ModelState, request));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> UpdateSteelgrade(VM_Steelgrade steelgrade)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.UpdateSteelgrade(ModelState, steelgrade));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public Task<ActionResult> SteelgradeEditPopup(long steelgradeId)
    {
      return PreparePopupActionResultFromVm(() => _service.GetSteelgrade(ModelState, steelgradeId),
        "~/Views/Module/PE.Lite/Steelgrade/_SteelgradeEditPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> CreateSteelgrade(VM_Steelgrade steelgrade)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.CreateSteelgrade(ModelState, steelgrade));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public Task<ActionResult> SteelgradeCreatePopup()
    {
      VM_Steelgrade result = new VM_Steelgrade();
      return PreparePopupActionResultFromVm(() => result,
        "~/Views/Module/PE.Lite/Steelgrade/_SteelgradeCreatePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long id)
    {
      return PrepareActionResultFromVm(() => _service.GetSteelgradeDetails(ModelState, id),
        "~/Views/Module/PE.Lite/Steelgrade/_SteelgradeBody.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Delete)]
    public Task<JsonResult> DeleteSteelgrade(long itemId)
    {
      VM_Steelgrade steelgrade = new VM_Steelgrade {Id = itemId};
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(
        () => _service.DeleteSteelgrade(ModelState, steelgrade));
    }

    public JsonResult ServerFiltering_GetSteelgrades(long? heatId)
    {
      IList<VM_Steelgrade> heats = _service.GetSteelgradesByHeat(heatId);

      return Json(heats);
    }

    [HttpPost]
    public async Task<JsonResult> ValidateSteelgradeCode(string code)
    {
      bool result = await _service.ValidateSteelgradeCode(code);
      return Json(result);
    }

    [HttpPost]
    public async Task<JsonResult> ValidateSteelgradeName(string name)
    {
      bool result = await _service.ValidateSteelgradeName(name);
      return Json(result);
    }

    public JsonResult ServerFiltering_GetSteelgradeParents()
    {
      IList<VM_Steelgrade> steelgradeParents = _service.GetSteelgradeParents();

      return Json(steelgradeParents);
    }

    public JsonResult ServerFiltering_GetSteelFamilies()
    {
      IList<VM_SteelFamily> steelFamilys = _service.GetSteelFamilies();

      return Json(steelFamilys);
    }
  }
}
