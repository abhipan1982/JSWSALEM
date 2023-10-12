using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class BilletCatalogueController : BaseController
  {
    private readonly IBilletService _service;

    public BilletCatalogueController(IBilletService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Billet/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetMaterialCatalogueList([DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = _service.GetProductCatalogueList(ModelState, request);
      return PrepareJsonResultFromDataSourceResult(() => result);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> EditMaterialCataloguePopup(long id)
    {
      VM_MaterialCatalogue result = _service.GetMaterialCatalogue(id);
      return PreparePopupActionResultFromVm(() => result,
        "~/Views/Module/PE.Lite/Billet/_MaterialCatalogueEditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> UpdateMaterialCatalogue(VM_MaterialCatalogue materialCatalogue)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.UpdateMaterialCatalogue(ModelState, materialCatalogue));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public Task<ActionResult> CreateMaterialCataloguePopup()
    {
      VM_MaterialCatalogue result = new VM_MaterialCatalogue();
      return PreparePopupActionResultFromVm(() => result,
        "~/Views/Module/PE.Lite/Billet/_MaterialCatalogueCreatePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> CreateMaterialCatalogue(VM_MaterialCatalogue materialCatalogue)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.CreateMaterialCatalogue(ModelState, materialCatalogue));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long id)
    {
      return PrepareActionResultFromVm(() => _service.GetBilletDetails(ModelState, id),
        "~/Views/Module/PE.Lite/Billet/_BilletBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> GetBilletCatalogueOverviewByWorkOrderId(long workOrderId)
    {
      return PrepareActionResultFromVm(() => _service.GetBilletCatalogueOverviewByWorkOrderId(ModelState, workOrderId),
        "~/Views/Module/PE.Lite/Billet/_BilletDetails.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Product, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Delete)]
    public Task<JsonResult> DeleteMaterialCatalogue(long itemId)
    {
      VM_MaterialCatalogue materialCat = new VM_MaterialCatalogue {Id = itemId};
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.DeleteMaterialCatalogue(ModelState, materialCat));
    }

    public JsonResult ServerFiltering_GetMaterialCatalogues(string text)
    {
      IList<VM_MaterialCatalogue> materialCatalogues = _service.GetMaterialCataloguesByAnyFeaure(text);

      return Json(materialCatalogues);
    }

    public JsonResult ServerFiltering_GetSteelGrades()
    {
      IList<VM_Steelgrade> list = _service.GetSteelgradeList();

      return Json(list);
    }

    public JsonResult ServerFiltering_GetTypes()
    {
      IList<VM_MaterialCatalogueType> list = _service.GetMaterialCatalogueTypeList();

      return Json(list);
    }

    public JsonResult ServerFiltering_GetShapes()
    {
      IList<VM_Shape> list = _service.GetShapeList();

      return Json(list);
    }


    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    //public async Task<JsonResult> GetWorkOrderListById([DataSourceRequest] DataSourceRequest request, long id)
    //{
    //  return await PrepareJsonResultFromDataSourceResult(() => _service.GetWorkOrderListById(ModelState, request, id));
    //}
  }
}
