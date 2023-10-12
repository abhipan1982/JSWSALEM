using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Defect;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class DefectCatalogueCategoriesController : BaseController
  {
    private readonly IDefectCatalogueCategoriesService _service;

    public DefectCatalogueCategoriesController(IDefectCatalogueCategoriesService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public ActionResult Index()
    {
      ViewBag.EnumAssignmentType = _service.GetEnumAssignmentTypeList();
      return View("~/Views/Module/PE.Lite/DefectCatalogueCategories/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public async Task<ActionResult> GetDefectCatalogueCategoriesList([DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = _service.GetDefectCatalogueCategoriesList(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_Quality,
      RightLevel.Update)]
    public async Task<ActionResult> AddDefectCatalogueCategoriesAsync(VM_DefectCatalogueCategory vm)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.AddDefectCatalogueCategoryAsync(ModelState, vm));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_Quality,
      RightLevel.Update)]
    public Task<ActionResult> DefectCatalogueCategoriesAddPopup()
    {
      ViewBag.EnumAssignmentType = _service.GetEnumAssignmentTypeList();
      VM_DefectCatalogueCategory result = new VM_DefectCatalogueCategory();
      return PreparePopupActionResultFromVm(() => result,
        "~/Views/Module/PE.Lite/DefectCatalogueCategories/_DefectCatalogueCategoriesAddPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_Quality,
      RightLevel.Update)]
    public async Task<ActionResult> UpdateDefectCatalogueCategoriesAsync(VM_DefectCatalogueCategory vm)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.UpdateDefectCatalogueCategoriesAsync(ModelState, vm));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_Quality,
      RightLevel.Update)]
    public Task<ActionResult> DefectCatalogueCategoriesEditPopup(long defectCatalogueCategoryId)
    {
      ViewBag.EnumAssignmentType = _service.GetEnumAssignmentTypeList();
      return PreparePopupActionResultFromVm(
        () => _service.GetDefectCatalogueCategory(ModelState, defectCatalogueCategoryId),
        "~/Views/Module/PE.Lite/DefectCatalogueCategories/_DefectCatalogueCategoriesEditPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Defect, Constants.SmfAuthorization_Module_Quality,
      RightLevel.Delete)]
    public Task<JsonResult> DeleteDefectCatalogueCategory(long itemId)
    {
      VM_DefectCatalogueCategory defectCat = new VM_DefectCatalogueCategory {Id = itemId};
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.DeleteDefectCatalogueCategoryAsync(ModelState, defectCat));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long id)
    {
      return PrepareActionResultFromVm(() => _service.GetDefectCatalogueCategory(ModelState, id),
        "~/Views/Module/PE.Lite/DefectCatalogueCategories/_DefectCatalogueCategoriesBody.cshtml");
    }

    public JsonResult ServerFiltering_GetDefectGroups()
    {
      IList<VM_DefectGroupsCatalogue> steelGroups = _service.GetDefectGroups();

      return Json(steelGroups);
    }

    public JsonResult ServerFiltering_GetEnumAssignmentType()
    {
      SelectList list = _service.GetEnumAssignmentTypeList();

      return Json(list);
    }

    [HttpPost]
    public async Task<JsonResult> ValidateDefectCategoriesCode(string code)
    {
      bool result = await _service.ValidateDefectCategoriesCode(code);
      return Json(result);
    }

    [HttpPost]
    public async Task<JsonResult> ValidateDefectCategoriesName(string name)
    {
      bool result = await _service.ValidateDefectCategoriesName(name);
      return Json(result);
    }
  }
}
