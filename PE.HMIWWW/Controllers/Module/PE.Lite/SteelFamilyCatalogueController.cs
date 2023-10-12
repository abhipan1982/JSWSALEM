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

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class SteelFamilyCatalogueController : BaseController
  {
    private readonly ISteelFamilyService _service;

    public SteelFamilyCatalogueController(ISteelFamilyService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/SteelFamily/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetSteelFamilyList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetSteelFamilyList(ModelState, request));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> UpdateSteelFamilyGroup(VM_SteelFamily steelFamily)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.UpdateSteelFamily(ModelState, steelFamily));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public Task<ActionResult> SteelFamilyEditPopup(long steelFamilyId)
    {
      return PreparePopupActionResultFromVm(() => _service.GetSteelFamily(ModelState, steelFamilyId),
        "~/Views/Module/PE.Lite/SteelFamily/_SteelFamilyEditPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> CreateSteelFamily(VM_SteelFamily steelFamily)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.CreateSteelFamily(ModelState, steelFamily));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public Task<ActionResult> SteelFamilyCreatePopup()
    {
      VM_SteelFamily result = new VM_SteelFamily();
      return PreparePopupActionResultFromVm(() => result,
        "~/Views/Module/PE.Lite/SteelFamily/_SteelFamilyCreatePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long id)
    {
      return PrepareActionResultFromVm(() => _service.GetSteelFamilyDetails(ModelState, id),
        "~/Views/Module/PE.Lite/SteelFamily/_SteelFamilyBody.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Delete)]
    public Task<JsonResult> DeleteSteelFamily(long itemId)
    {
      VM_SteelFamily steelFamily = new VM_SteelFamily {Id = itemId};
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.DeleteSteelFamily(ModelState, steelFamily));
    }

    public JsonResult ServerFiltering_GetSteelFamilyParents()
    {
      IList<VM_SteelFamily> steelFamilys = _service.GetSteelFamilies();

      return Json(steelFamilys);
    }

    [HttpPost]
    public async Task<JsonResult> ValidateSteelFamilyCode(string code)
    {
      bool result = await _service.ValidateSteelFamilyCode(code);
      return Json(result);
    }

    [HttpPost]
    public async Task<JsonResult> ValidateSteelFamilyName(string name)
    {
      bool result = await _service.ValidateSteelFamilyName(name);
      return Json(result);
    }
  }
}
