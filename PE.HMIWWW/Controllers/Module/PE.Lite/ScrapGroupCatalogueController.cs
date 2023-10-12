using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.ScrapGroup;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class ScrapGroupCatalogueController : BaseController
  {
    private readonly IScrapService _service;

    public ScrapGroupCatalogueController(IScrapService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Scrap/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetScrapGroupList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetScrapGroupList(ModelState, request));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> UpdateScrapGroup(VM_ScrapGroup scrapgroup)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.UpdateScrapGroup(ModelState, scrapgroup));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public Task<ActionResult> ScrapGroupEditPopup(long scrapgroupId)
    {
      return PreparePopupActionResultFromVm(() => _service.GetScrapGroup(ModelState, scrapgroupId),
        "~/Views/Module/PE.Lite/Scrap/_ScrapGroupEditPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> CreateScrapGroup(VM_ScrapGroup scrapgroup)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.CreateScrapGroup(ModelState, scrapgroup));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public Task<ActionResult> ScrapGroupCreatePopup()
    {
      VM_ScrapGroup result = new VM_ScrapGroup();
      return PreparePopupActionResultFromVm(() => result, "~/Views/Module/PE.Lite/Scrap/_ScrapGroupCreatePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long id)
    {
      return PrepareActionResultFromVm(() => _service.GetScrapGroupDetails(ModelState, id),
        "~/Views/Module/PE.Lite/Scrap/_ScrapGroupBody.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Delete)]
    public Task<JsonResult> DeleteScrapGroup(long itemId)
    {
      VM_ScrapGroup scrapgroup = new VM_ScrapGroup {ScrapGroupId = itemId};
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(
        () => _service.DeleteScrapGroup(ModelState, scrapgroup));
    }

    public JsonResult ServerFiltering_GetScrapGroups()
    {
      IList<VM_ScrapGroup> scrapGroups = _service.GetScrapGroups();

      return Json(scrapGroups);
    }

    [HttpPost]
    public async Task<JsonResult> ValidateScrapGroupCode(string code)
    {
      bool result = await _service.ValidateScrapGroupCode(code);
      return Json(result);
    }

    [HttpPost]
    public async Task<JsonResult> ValidateScrapGroupName(string name)
    {
      bool result = await _service.ValidateScrapGroupName(name);
      return Json(result);
    }
  }
}
