using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Defect;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class RawMaterialController : BaseController
  {
    private readonly IRawMaterialService _materialService;

    public RawMaterialController(IRawMaterialService service)
    {
      _materialService = service;
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.RawMaterialStatuses = ListValuesHelper.GetRawMaterialStatusesList();
      ViewBag.QualityList = ListValuesHelper.GetProductQualityList();
      ViewBag.Defects = ListValuesHelper.GetDefectsMulitSelect();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/RawMaterial/Index.cshtml");
    }

    public Task<JsonResult> AssignRawMaterialQuality(VM_QualityAssignment rawMaterialQuality)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _materialService.AssignRawMaterialQualityAsync(ModelState, rawMaterialQuality));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetRawMaterialSearchList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _materialService.GetRawMaterialSearchList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetMeasurementSearchList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _materialService.GetMeasurementSearchList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long rawMaterialId)
    {
      return PrepareActionResultFromVm(() => _materialService.GetRawMaterialDetails(ModelState, rawMaterialId),
        "~/Views/Module/PE.Lite/RawMaterial/_RawMaterialBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> GetMaterialDetails(long rawMaterialId)
    {
      return PrepareActionResultFromVm(() => _materialService.GetRawMaterialDetails(ModelState, rawMaterialId),
        "~/Views/Module/PE.Lite/RawMaterial/_RawMaterialDetails.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetailsInFurnace(long rawMaterialId)
    {
      return PrepareActionResultFromVm(() => _materialService.GetRawMaterialDetails(ModelState, rawMaterialId),
        "~/Views/Module/PE.Lite/RawMaterial/_RawMaterialFurnaceBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> MeasurementDetails(long measurementId)
    {
      return PrepareActionResultFromVm(() => _materialService.GetMeasurementDetails(ModelState, measurementId),
        "~/Views/Module/PE.Lite/RawMaterial/_MeasurementsBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> HistoryDetails(long rawMaterialStepId)
    {
      return PrepareActionResultFromVm(() => _materialService.GetHistoryDetails(ModelState, rawMaterialStepId),
        "~/Views/Module/PE.Lite/RawMaterial/_HistoryBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> L3MaterialAssignment(long rawMaterialId)
    {
      return PrepareActionResultFromVm(() => _materialService.GetL3MaterialData(ModelState, rawMaterialId),
        "~/Views/Module/PE.Lite/RawMaterial/_L3MaterialAssignment.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> GetRawMaterialGenealogy(long rawMaterialId)
    {
      return PrepareActionResultFromVm(() => _materialService.GetRawMaterialGenealogy(ModelState, rawMaterialId),
        "~/Views/Module/PE.Lite/RawMaterial/_RawMaterialGenealogy.cshtml");
    }
    
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_Tracking,
      RightLevel.Delete)]
    public async Task<JsonResult> AssignRawMaterial(long rawMaterialId, long l3MaterialId)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _materialService.AssignRawMaterial(ModelState, rawMaterialId, l3MaterialId));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_Tracking,
      RightLevel.Delete)]
    public async Task<JsonResult> UnassignRawMaterial(long rawMaterialId)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _materialService.UnassignRawMaterial(ModelState, rawMaterialId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetMeasurmentsByRawMaterialId([DataSourceRequest] DataSourceRequest request,
      long rawMaterialId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _materialService.GetMeasurmentsByRawMaterialId(ModelState, request, rawMaterialId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetHistoryByRawMaterialId([DataSourceRequest] DataSourceRequest request, long rawMaterialId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _materialService.GetHistoryByRawMaterialId(ModelState, request, rawMaterialId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public Task<ActionResult> AssignDefectsPopup(long rawMaterialId)
    {
      ViewBag.AssetList = _materialService.GetAssets();
      VM_QualityAssignment rawMaterialQuality = new VM_QualityAssignment(rawMaterialId);
      return PreparePopupActionResultFromVm(() => rawMaterialQuality,
        "~/Views/Module/PE.Lite/Defect/_DefectsRawMaterialAssigmentPopup.cshtml");
    }

    public JsonResult GetDefectsList()
    {
      return Json(_materialService.GetDefectsList());
    }

    public Task<JsonResult> GetRawMaterialEvents([DataSourceRequest] DataSourceRequest request, long rawMaterialId)
    {
      return PrepareJsonResultFromDataSourceResult(() => _materialService.GetRawMaterialEvents(ModelState, request, rawMaterialId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Delete)]
    public Task<JsonResult> GetNotAssignedRawMaterials([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>_materialService.GetNotAssignedRawMaterials(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_Tracking,
      RightLevel.Update)]
    public ActionResult GetMaterialReadyView(long rawMaterialId)
    {
      return PartialView("~/Views/Module/PE.Lite/RawMaterial/_RawMaterialReadyBody.cshtml", rawMaterialId);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_Tracking,
      RightLevel.Update)]
    public Task<ActionResult> RawMaterialDivisionHistory(long rawMaterialId)
    {
      return PrepareActionResultFromVm(() => _materialService.GetMaterialDivisionHistory(ModelState, rawMaterialId),
        "~/Views/Module/PE.Lite/RawMaterial/_RawMaterialDivisionHistory.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_Tracking,
      RightLevel.Update)]
    public Task<ActionResult> MaterialReadyPopup(long rawMaterialId)
    {
      ViewBag.ProductTypeList = _materialService.GetProductCatalogueTypes();
      return PreparePopupActionResultFromVm(() => _materialService.GetMaterialForReadyOperation(ModelState, rawMaterialId),
        "~/Views/Module/PE.Lite/RawMaterial/_MaterialReadyPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_Tracking, RightLevel.View)]
    public Task<JsonResult> GetDefectListByRawMaterialId([DataSourceRequest] DataSourceRequest request, long rawMaterialId)
    {
      return PrepareJsonResultFromDataSourceResult(() => _materialService.GetDefectListByRawMaterialId(ModelState, request, rawMaterialId));
    }
  }
}
