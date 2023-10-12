using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class BundleController : BaseController
  {
    private readonly IBundleService _materialService;

    public BundleController(IBundleService service)
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
      return View("~/Views/Module/PE.Lite/Bundle/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetBundleSearchList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _materialService.GetBundleSearchList(ModelState, request));
    }
  }
}
