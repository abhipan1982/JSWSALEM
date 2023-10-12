using System.Collections.Generic;
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
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.Products;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class ProductsController : BaseController
  {
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService service)
    {
      _productsService = service;
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.ProductQualities = ListValuesHelper.GetProductQualityList();
      ViewBag.Defects = ListValuesHelper.GetDefectsMulitSelect();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Products/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetProductsList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _productsService.GetProductsSearchList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> GetProductDetails(long productId)
    {
      return PrepareActionResultFromVm(() => _productsService.GetProductsDetails(ModelState, productId),
        "~/Views/Module/PE.Lite/Products/_ProductsDetails.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long productId)
    {
      return PrepareActionResultFromVm(() => _productsService.GetProductsDetails(ModelState, productId),
        "~/Views/Module/PE.Lite/Products/_ProductsBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public Task<ActionResult> AssignDefectsPopup(long productId)
    {
      VM_QualityAssignment productQuality = new VM_QualityAssignment(productId);
      return PreparePopupActionResultFromVm(() => productQuality, "~/Views/Module/PE.Lite/Defect/_DefectsProductAssigmentPopup.cshtml");
    }

    [HttpPost]
    public async Task<ActionResult> AssignProductQualityAsync(long id, short quality, List<long> defectIds)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _productsService.AssignProductQualityAsync(ModelState, id, quality, defectIds));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Billet, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public Task<JsonResult> GetMaterialsListByProductId([DataSourceRequest] DataSourceRequest request, long productId)
    {
      return PrepareJsonResultFromDataSourceResult(() => _productsService.GetMaterialsListByProductId(ModelState, request, productId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public ActionResult ProductDetailsView(VM_ProductsOverview product)
    {
      return PartialView("~/Views/Module/PE.Lite/Products/_ProductsDetails.cshtml", product);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public ActionResult ProductCatalogueDetailsView(VM_ProductCatalogue productCatalogue)
    {
      return PartialView("~/Views/Module/PE.Lite/Product/_ProductDetails.cshtml", productCatalogue);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public ActionResult ProductMaterialsListView(long productId)
    {
      return PartialView("~/Views/Module/PE.Lite/Products/_ProductMaterialList.cshtml", productId);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> BundleCreatePopup(long workOrderId)
    {
      var model = await _productsService.GetNewBundleDataAsync(ModelState, workOrderId);

      return await PreparePopupActionResultFromVm(() => model, "~/Views/Module/PE.Lite/Products/_BundleCreatePopup.cshtml");
    }

    [HttpPost]
    public async Task<ActionResult> CreateBundleAsync(VM_Bundle bundle)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _productsService.CreateBundleAsync(ModelState, bundle));
    }
  }
}
