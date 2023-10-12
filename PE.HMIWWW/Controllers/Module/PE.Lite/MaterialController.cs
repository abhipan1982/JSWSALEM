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

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class MaterialController : BaseController
  {
    private readonly IMaterialService _materialService;

    public MaterialController(IMaterialService service)
    {
      _materialService = service;
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.RawMaterialStatuses = ListValuesHelper.GetRawMaterialStatusesList();
    }

    // GET: Material
    public ActionResult Index(long? materialId)
    {
      return View("~/Views/Module/PE.Lite/Material/Index.cshtml",
        materialId != null ? _materialService.GetMaterialById(materialId) : null);
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_Material, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetMaterialSearchList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _materialService.GetMaterialSearchList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Material, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long materialId)
    {
      return PrepareActionResultFromVm(() => _materialService.GetMaterialDetails(ModelState, materialId),
        "~/Views/Module/PE.Lite/Material/_MaterialBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Material, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> GetMaterialDetails(long materialId)
    {
      return PrepareActionResultFromVm(() => _materialService.GetMaterialDetails(ModelState, materialId),
        "~/Views/Module/PE.Lite/Material/_MaterialDetails.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Material, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Delete)]
    public Task<JsonResult> GetNotAssignedMaterials([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _materialService.GetNotAssignedMaterials(ModelState, request));
    }
  }
}
