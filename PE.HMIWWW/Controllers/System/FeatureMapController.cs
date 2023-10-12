using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PE.BaseDbEntity.EnumClasses;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.FeatureMap;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;

namespace PE.HMIWWW.Controllers.System

{
  public class FeatureMapController : BaseController
  {
    private readonly IFeatureMap _featureMapService;

    public FeatureMapController(IFeatureMap service)
    {
      _featureMapService = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_FeatureMap, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/FeatureMap/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_FeatureMap, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetFeatureMapOverList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _featureMapService.GetFeatureMapOverList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_FeatureMap, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> FeatureLimitsEditPopup(long featureId)
    {
      return PartialView("~/Views/Module/PE.Lite/FeatureMap/_EditFeatureLimitsPopup.cshtml",
        await _featureMapService.GetFeatureAsync(ModelState, featureId));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_FeatureMap, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<JsonResult> EditFeatureLimits(VM_FeatureMap data)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _featureMapService.EditFeatureLimitsAsync(ModelState, data));
    }
  }
}
