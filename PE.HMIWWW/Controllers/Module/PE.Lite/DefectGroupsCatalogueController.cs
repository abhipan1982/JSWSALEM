using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Defect;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class DefectGroupsCatalogueController : BaseController
  {
    private readonly IDefectGroupsCatalogueService _service;

    public DefectGroupsCatalogueController(IDefectGroupsCatalogueService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/DefectGroupsCatalogue/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public async Task<ActionResult> GetDefectGroupsCatalogueList([DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = _service.GetDefectGroupList(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> AddDefectGroupsCatalogueAsync(VM_DefectGroupsCatalogue defectGroupsCatalogue)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.AddDefectGroupAsync(ModelState, defectGroupsCatalogue));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public Task<ActionResult> DefectGroupsCatalogueAddPopup()
    {
      VM_DefectGroupsCatalogue result = new VM_DefectGroupsCatalogue();
      return PreparePopupActionResultFromVm(() => result,
        "~/Views/Module/PE.Lite/DefectGroupsCatalogue/_DefectGroupsCatalogueAddPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long id)
    {
      return PrepareActionResultFromVm(() => _service.GetDefectGroup(ModelState, id),
        "~/Views/Module/PE.Lite/DefectGroupsCatalogue/_DefectGroupsCatalogueBody.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> UpdateDefectGroupsCatalogueAsync(VM_DefectGroupsCatalogue defectGroupsCatalogue)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.UpdateDefectGroupAsync(ModelState, defectGroupsCatalogue));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public Task<ActionResult> DefectGroupsCatalogueEditPopup(long defectCatalogueId)
    {
      return PreparePopupActionResultFromVm(() => _service.GetDefectGroup(ModelState, defectCatalogueId),
        "~/Views/Module/PE.Lite/DefectGroupsCatalogue/_DefectGroupsCatalogueEditPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Delete)]
    public Task<JsonResult> DeleteDefectGroupsCatalogue(long itemId)
    {
      VM_DefectGroupsCatalogue defectCat = new VM_DefectGroupsCatalogue {DefectGroupId = itemId};
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.DeleteDefectGroupAsync(ModelState, defectCat));
    }
    //public JsonResult ServerFiltering_GetDefectGroups()
    //{
    //  IList<VM_DefectGroupsCatalogue> SteelGroups = _service.GetDefectGroups();

    //  return Json(SteelGroups);
    //}


    [HttpPost]
    public async Task<JsonResult> ValidateDefectGroupsCode(string code)
    {
      bool result = await _service.ValidateDefectGroupsCode(code);
      return Json(result);
    }

    [HttpPost]
    public async Task<JsonResult> ValidateDefectGroupsName(string name)
    {
      bool result = await _service.ValidateDefectGroupsName(name);
      return Json(result);
    }
  }
}
