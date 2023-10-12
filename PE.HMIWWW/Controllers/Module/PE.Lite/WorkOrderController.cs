using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using PE.BaseDbEntity.EnumClasses;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class WorkOrderController : BaseController
  {
    private readonly IWorkOrderService _service;

    public WorkOrderController(IWorkOrderService service)
    {
      _service = service;
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.WorkOrderStatuses = ListValuesHelper.GetScheduleStatuses();
      ViewBag.RejectLocation = ListValuesHelper.GetRejectLocationList();
      ViewBag.TypeOfScrap = ListValuesHelper.GetTypeOfScrapList();
      ViewBag.RawMaterialStatus = ListValuesHelper.GetRawMaterialStatusesList();
      ViewBag.CrashTestList = ListValuesHelper.GetCrashTestList();
      ViewBag.InspectionResultList = ListValuesHelper.GetInspectionResultList();
      ViewBag.ProductQualityList = ListValuesHelper.GetProductQualityList();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/WorkOrder/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetWorkOrderOverviewList([DataSourceRequest] DataSourceRequest request)
    {
      ViewBag.WorkOrderStatuses = ListValuesHelper.GetWorkOrderStatusesList();
      return PrepareJsonResultFromDataSourceResult(() => _service.GetWorkOrderOverviewList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
    RightLevel.View)]
    public Task<JsonResult> GetWorkOrderInRealizationList([DataSourceRequest] DataSourceRequest request)
    {
      ViewBag.WorkOrderStatuses = ListValuesHelper.GetWorkOrderStatusesList();
      return PrepareJsonResultFromDataSourceResult(() => _service.GetWorkOrderInRealizationList(ModelState, request));
    }
      

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> GetWorkOrderOverview(long workOrderId)
    {
      return PrepareActionResultFromVm(() => _service.GetWorkOrderDetails(ModelState, workOrderId),
        "~/Views/Module/PE.Lite/WorkOrder/_WorkOrderDetails.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long workOrderId)
    {
      return PrepareActionResultFromVm(() => _service.GetWorkOrderDetails(ModelState, workOrderId),
        "~/Views/Module/PE.Lite/WorkOrder/_WorkOrderBody.cshtml");
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetMaterialsListByWorkOrderId([DataSourceRequest] DataSourceRequest request,
      long workOrderId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _service.GetMaterialsListByWorkOrderId(ModelState, request, workOrderId));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetNoScheduledWorkOrderList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetNoScheduledWorkOrderList(ModelState, request));
    }


    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> CreateWorkOrder(VM_WorkOrder workOrder)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.CreateWorkOrder(ModelState, workOrder));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public ActionResult WorkOrderCreatePopup()
    {
      ViewBag.WorkOrderStatuses =
        new SelectList(ListValuesHelper.GetWorkOrderStatusesList().Where(x => x.Value != "0").ToList(), "Value",
          "Text");
      ViewBag.ProductList = _service.GetProductList();
      ViewBag.MaterialList = _service.GetMaterialList();
      ViewBag.CustomerList = _service.GetCustomerList();
      return PartialView("~/Views/Module/PE.Lite/WorkOrder/_WorkOrderCreatePopup.cshtml");
    }


    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> EditWorkOrder(VM_WorkOrder workOrder)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.EditWorkOrder(ModelState, workOrder));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> WorkOrderEditPopup(long id, bool byMaterial = false)
    {
      long? workOrderId = !byMaterial ? id : await _service.GetWorkOrderIdByMaterialId(id);
      if (workOrderId == null)
      {
        return NotFound();
      }

      var workOrder = await _service.GetWorkOrder(workOrderId);

      if (workOrder.WorkOrderStatus == WorkOrderStatus.New)
      {
        ViewBag.WorkOrderStatuses =
          new SelectList(ListValuesHelper.GetWorkOrderStatusesList().Where(x => x.Value != "0").ToList(), "Value", "Text");
        ViewBag.ProductList = _service.GetProductList();
        ViewBag.MaterialList = _service.GetMaterialList();
        ViewBag.CustomerList = _service.GetCustomerList();
        return PartialView("~/Views/Module/PE.Lite/WorkOrder/_WorkOrderEditPopup.cshtml", workOrder);
      }
      else
      {
        return PartialView("~/Views/Module/PE.Lite/WorkOrder/_EditMaterialNumberPopup.cshtml",
          await _service.GetWorkOrderMaterialsDetails(ModelState, workOrderId.Value));
      }
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Delete)]
    public Task<JsonResult> DeleteWorkOrder(long workOrderId)
    {
      VM_WorkOrderOverview workOrder = _service.GetWorkOrderDetails(ModelState, workOrderId);
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.DeleteWorkOrder(ModelState, workOrder));
    }

    public async Task<JsonResult> GetBilletCatalogueDetails(long billetCatalogId)
    {
      VM_BilletCatalogueDetails result = await _service.GetBilletCatalogueDetails(billetCatalogId);
      return Json(result);
    }

    public async Task<JsonResult> GetProductCatalogDetails(long productCatalogId)
    {
      VM_ProductCatalogue result = await _service.GetProductCatalogueDetails(productCatalogId);
      return Json(result);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdPlanning,
      RightLevel.Update)]
    public async Task<ActionResult> EditMaterialNumberPopup(long workOrderId)
    {
      return PartialView("~/Views/Module/PE.Lite/WorkOrder/_EditMaterialNumberPopup.cshtml",
        await _service.GetWorkOrderMaterialsDetails(ModelState, workOrderId));
    }


    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public async Task<ActionResult> EditMaterialNumber(VM_WorkOrderMaterials workOrderMaterials)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.EditMaterialNumber(ModelState, workOrderMaterials));
    }

    public Task<JsonResult> GetWorkOrderProductsList([DataSourceRequest] DataSourceRequest request, long workOrderId)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetWorkOrderProducts(ModelState, request, workOrderId));
    }

    public Task<JsonResult> GetWorkOrderEventsList([DataSourceRequest] DataSourceRequest request, long workOrderId)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetWorkOrderEvents(ModelState, request, workOrderId));
    }

    public Task<JsonResult> GetWorkOrderDelaysList([DataSourceRequest] DataSourceRequest request, long workOrderId)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetWorkOrderDelays(ModelState, request, workOrderId));
    }

    public Task<JsonResult> GetWorkOrderDefectsList([DataSourceRequest] DataSourceRequest request, long workOrderId)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetWorkOrderDefects(ModelState, request, workOrderId));
    }

    public Task<JsonResult> GetWorkOrderRejectsList([DataSourceRequest] DataSourceRequest request, long workOrderId)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetWorkOrderRejects(ModelState, request, workOrderId));
    }

    public Task<JsonResult> GetWorkOrderScrapsList([DataSourceRequest] DataSourceRequest request, long workOrderId)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetWorkOrderScraps(ModelState, request, workOrderId));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Delete)]
    public async Task<JsonResult> SendWorkOrderReportAsync(VM_WorkOrderConfirmation workOrderConfirmation)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.SendWorkOrderReportAsync(ModelState, workOrderConfirmation));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Delete)]
    public async Task<JsonResult> SendEndOfWorkShop()
    {
      VM_WorkOrderConfirmation workOrderConfirmation = new VM_WorkOrderConfirmation()
      {
        WorkOrderId = null,
        IsEndOfWorkShop = true
      };
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.SendWorkOrderReportAsync(ModelState, workOrderConfirmation));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public ActionResult SendWorkOrderToL3View(long workOrderId)
    {
      return PartialView("~/Views/Module/PE.Lite/WorkOrder/_SendWorkOrderToL3.cshtml", workOrderId);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Update)]
    public Task<ActionResult> SendL3Details(long workOrderId)
    {
      return PrepareActionResultFromVm(() => _service.GetWorkOrderSummary(workOrderId), "~/Views/Module/PE.Lite/WorkOrder/_SendL3Details.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.Delete)]
    public async Task<ActionResult> CancelWorkOrder(long workOrderId)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.CancelWorkOrder(ModelState, workOrderId));
    }

    public async Task<ActionResult> UnCancelWorkOrder(long workOrderId)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.UnCancelWorkOrder(ModelState, workOrderId));
    }    
    public async Task<ActionResult> BlockWorkOrder(long workOrderId)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.BlockWorkOrder(ModelState, workOrderId));
    }

    public async Task<ActionResult> UnBlockWorkOrder(long workOrderId)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.UnBlockWorkOrder(ModelState, workOrderId));
    }
  }
}
