using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;

namespace PE.HMIWWW.Controllers.System
{
  public class AssetController : BaseController
  {
    private readonly IAssetService _assetService;

    public AssetController(IAssetService service)
    {
      _assetService = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Asset, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Asset/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Asset, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetAssetOverList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _assetService.GetAssetOverList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Asset, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetFeatureByAssetId([DataSourceRequest] DataSourceRequest request, long assetId)
    {
      return PrepareJsonResultFromDataSourceResult(
        () => _assetService.GetFeatureByAssetId(ModelState, request, assetId));
    }
  }
}
