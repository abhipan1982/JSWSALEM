using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.QualityExpert;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class QualityExpertController : BaseController
  {
    private readonly IQualityExpertService _qeService;

    public QualityExpertController(IQualityExpertService qeService)
    {
      _qeService = qeService;
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      ViewBag.RatingValuesTypes = _qeService.GetRatingValuesTypes();
      base.OnActionExecuting(ctx);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_QualityExpert, Constants.SmfAuthorization_Module_QualityExpert, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/QualityExpert/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_QualityExpert, Constants.SmfAuthorization_Module_QualityExpert, RightLevel.View)]
    public async Task<JsonResult> GetRawMaterialSearchList([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() =>
        _qeService.GetRawMaterialSearchList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_QualityExpert, Constants.SmfAuthorization_Module_QualityExpert, RightLevel.View)]
    public async Task<ActionResult> MaterialGradingInformation(VM_QualityExpertRawMaterial selectedMaterialInfo)
    {
      if (selectedMaterialInfo != null)
      {
        VM_QualityExpertOverview overview = await _qeService.GetOverview(ModelState, selectedMaterialInfo);
        // get VM_QualityExpertOverview 
        return PartialView("_MaterialDetails", overview);
      }
      else
      {
        return NotFound();
      }
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_QualityExpert, Constants.SmfAuthorization_Module_QualityExpert, RightLevel.View)]
    public async Task<ActionResult> GetMaterialGrading(long rawMaterialId)
    {
      return PartialView("_MaterialGradingValue", await _qeService.GetMaterialGrading(ModelState, rawMaterialId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_QualityExpert, Constants.SmfAuthorization_Module_QualityExpert, RightLevel.View)]
    public async Task<JsonResult> GetMaterialGradingPerAsset([DataSourceRequest] DataSourceRequest request, long rawMaterialId)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _qeService.GetGradingPerAssetForMaterial(ModelState, request, rawMaterialId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_QualityExpert, Constants.SmfAuthorization_Module_QualityExpert, RightLevel.View)]
    public async Task<JsonResult> GetRatingsForAssetForMaterial([DataSourceRequest] DataSourceRequest request, long rawMaterialId, long assetId)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _qeService.GetRatingsForAssetForMaterial(ModelState, request, rawMaterialId, assetId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_QualityExpert, Constants.SmfAuthorization_Module_QualityExpert, RightLevel.Update)]
    public async Task<ActionResult> ForceValueDialog(long ratingId)
    {
      return await PreparePopupActionResultFromVm(() => _qeService.GetRatingById(ModelState, ratingId), "ForceRatingValueDialog");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_QualityExpert, Constants.SmfAuthorization_Module_QualityExpert, RightLevel.Update)]
    public async Task<JsonResult> ForceRatingValue(VM_ForceRatingValue reqeust)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _qeService.ForceRatingValue(ModelState, reqeust));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_QualityExpert, Constants.SmfAuthorization_Module_QualityExpert, RightLevel.Update)]
    public async Task<ActionResult> RatingDetailsDialog(long ratingId)
    {
      return await PreparePopupActionResultFromVm(() => _qeService.GetRatingDetailsById(ModelState, ratingId), "RatingDetailsDialog");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_QualityExpert, Constants.SmfAuthorization_Module_QualityExpert, RightLevel.View)]
    public async Task<ActionResult> RootCauses([DataSourceRequest] DataSourceRequest request, long ratingId)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _qeService.GetRootCausesByRatingId(ModelState, request, ratingId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_QualityExpert, Constants.SmfAuthorization_Module_QualityExpert, RightLevel.View)]
    public async Task<ActionResult> Compensations([DataSourceRequest] DataSourceRequest request, long ratingId)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _qeService.GetCompensationsByRatingId(ModelState, request, ratingId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_QualityExpert, Constants.SmfAuthorization_Module_QualityExpert, RightLevel.Update)]
    public async Task<JsonResult> ToggleCompensation(long compensationId, long ratingId, bool isChosen)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _qeService.ToggleCompensation(ModelState, compensationId, ratingId, isChosen));
    }
  }
}
