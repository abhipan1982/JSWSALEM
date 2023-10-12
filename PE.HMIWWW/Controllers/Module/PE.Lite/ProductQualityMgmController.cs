using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class ProductQualityMgmController : BaseController
  {
    private readonly IProductQualityMgmService _service;

    public ProductQualityMgmController(IProductQualityMgmService service)
    {
      _service = service;
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.ProductQuality = ListValuesHelper.GetProductQualityList();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductQualityMgm,
      Constants.SmfAuthorization_Module_Quality, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/ProductQualityMgm/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductQualityMgm,
      Constants.SmfAuthorization_Module_Quality, RightLevel.View)]
    public async Task<ActionResult> GetProductHistoryList([DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = _service.GetProductHistoryList(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductQualityMgm,
      Constants.SmfAuthorization_Module_Quality, RightLevel.View)]
    public async Task<ActionResult> GetDefectsList([DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = _service.GetDefectsList(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    //setQualityAndDefects(long productId, Enum.ProductQiality quality, Array<long> defectIds)
    //przeslac do modulu Quality
    //ustawic quality (enum)
    //stworzyc dla produktu defekty MVHDefects

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductQualityMgm,
      Constants.SmfAuthorization_Module_Quality, RightLevel.Update)]
    [HttpPost]
    public async Task<ActionResult> AssignQualityAsync(long productId, short quality, List<long> defectIds)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.AssignQualityAsync(ModelState, productId, quality, defectIds));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductQualityMgm,
      Constants.SmfAuthorization_Module_Quality, RightLevel.Update)]
    [HttpPost]
    public async Task<ActionResult> AssignProductQualityAsync(long id, short quality, List<long> defectIds)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.AssignQualityAsync(ModelState, id, quality, defectIds));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductQualityMgm,
      Constants.SmfAuthorization_Module_Quality, RightLevel.Update)]
    [HttpPost]
    public async Task<ActionResult> AssignRawMaterialQualityAsync(long rawMaterialId, short quality,
      List<long> defectIds)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.AssignRawMaterialQualityAsync(ModelState, rawMaterialId, quality, defectIds));
    }

    public string GetProductQualities()
    {
      //Dictionary<string, int> qualities = new List<Dictionary<string, int>>;
      return JsonConvert.SerializeObject(_service.GetProductQualityList(), Formatting.Indented);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductQualityMgm,
      Constants.SmfAuthorization_Module_Quality, RightLevel.View)]
    public Task<JsonResult> GetDefectsByProductId([DataSourceRequest] DataSourceRequest request, long productId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _service.GetProductDefectsList(ModelState, request, productId));
    }
  }
}
