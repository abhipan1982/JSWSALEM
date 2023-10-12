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
using PE.HMIWWW.ViewModel.Module.Lite.Inspection;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class BundleInspectionStationController : BaseController
  {
    private readonly IInspectionService _inspectionService;

    public BundleInspectionStationController(IInspectionService service)
    {
      _inspectionService = service;
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.CrashTestList = ListValuesHelper.GetCrashTestList();
      ViewBag.InspectionResultList = ListValuesHelper.GetInspectionResultList();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_InspectionStation,
      Constants.SmfAuthorization_Module_Quality, RightLevel.View)]
    public ActionResult Index()
    {
      ViewBag.RawMaterialStatuses = ListValuesHelper.GetRawMaterialStatusesList();
      return View("~/Views/Module/PE.Lite/BundleInspectionStation/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_InspectionStation,
      Constants.SmfAuthorization_Module_Quality, RightLevel.View)]
    public Task<JsonResult> GetRawMaterialSearchList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _inspectionService.GetBundleInspectionStationRawMaterialSearchList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_InspectionStation, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long rawMaterialId)
    {
      return PrepareActionResultFromVm(() => _inspectionService.GetRawMaterialDetails(ModelState, rawMaterialId),
        "~/Views/Module/PE.Lite/BundleInspectionStation/_InspectionBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_InspectionStation, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public Task<ActionResult> RawMaterialQualityView(long rawMaterialId)
    {
      return PrepareActionResultFromVm(() => _inspectionService.GetQualityByRawMaterial(rawMaterialId),
        "~/Views/Module/PE.Lite/BundleInspectionStation/_Quality.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_InspectionStation, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public ActionResult ScrapByRawMaterialView(long id)
    {
      VM_Scrap scrapData = _inspectionService.GetScrapByRawMaterial(id);
      return PartialView("~/Views/Module/PE.Lite/BundleInspectionStation/_Scrap.cshtml", scrapData);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_InspectionStation,
      Constants.SmfAuthorization_Module_Quality, RightLevel.View)]
    public Task<JsonResult> GetDefectsByRawMaterialId([DataSourceRequest] DataSourceRequest request, long rawMaterialId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _inspectionService.GetDefectsByRawMaterialId(ModelState, request, rawMaterialId));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_InspectionStation,
      Constants.SmfAuthorization_Module_Quality, RightLevel.Delete)]
    public Task<JsonResult> DeleteDefect(long defectId)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _inspectionService.DeleteDefect(ModelState, defectId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_InspectionStation,
      Constants.SmfAuthorization_Module_Quality, RightLevel.View)]
    public Task<ActionResult> DefectEditPopup(VM_LongId viewModel)
    {
      ViewBag.DefectCatalogueList = _inspectionService.GetDefectCatalogues();
      ViewBag.AssetList = _inspectionService.GetAssets();
      return PreparePopupActionResultFromVm(() => _inspectionService.GetDefect(viewModel.Id),
        "~/Views/Module/PE.Lite/Defect/_DefectEditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_InspectionStation,
      Constants.SmfAuthorization_Module_Quality, RightLevel.Update)]
    [HttpPost]
    public Task<JsonResult> UpdateDefect(VM_Defect defect)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _inspectionService.UpdateDefect(ModelState, defect));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_InspectionStation,
      Constants.SmfAuthorization_Module_Quality, RightLevel.View)]
    public Task<ActionResult> QualityView(long rawMaterialId)
    {
      return PrepareActionResultFromVm(() => _inspectionService.GetQualityByRawMaterial(rawMaterialId),
        "~/Views/Module/PE.Lite/BundleInspectionStation/_Inspection.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_InspectionStation, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public Task<JsonResult> GetInspectionByWorkOrder([DataSourceRequest] DataSourceRequest request, long workOrderId)
    {
      return PrepareJsonResultFromDataSourceResult(() => _inspectionService.GetInspectionByWorkOrder(ModelState, request, workOrderId));
    }
  }
}
