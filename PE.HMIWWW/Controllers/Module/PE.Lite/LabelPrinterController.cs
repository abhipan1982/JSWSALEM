using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.LabelPrinter;


namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class LabelPrinterController : BaseController
  {
    private readonly ILabelPrinterService _service;

    public LabelPrinterController(ILabelPrinterService service)
    {
      _service = service;
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_LabelPrinter, Constants.SmfAuthorization_Module_ZebraPrinterConnector, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/ZebraPrinter/Index.cshtml");
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_LabelPrinter, Constants.SmfAuthorization_Module_ZebraPrinterConnector, RightLevel.View)]
    public ActionResult ElementDetails(long workOrderId)
    {
      return PartialView("~/Views/Module/PE.Lite/ZebraPrinter/_ZebraPrinterBody.cshtml", workOrderId);
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_LabelPrinter, Constants.SmfAuthorization_Module_ZebraPrinterConnector, RightLevel.View)]
    public ActionResult GetLabelPartialView(VM_LabelRequest request)
    {
      return PartialView("~/Views/Module/PE.Lite/ZebraPrinter/_Label.cshtml", request);
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_LabelPrinter, Constants.SmfAuthorization_Module_ZebraPrinterConnector, RightLevel.View)]
    public Task<ActionResult> GetLabePartialViewForRawMaterial(VM_LabelRequest labelRequest)
    {
      return TaskPrepareActionResultFromVm(() => _service.FillLabelRequestForRawMaterialAsync(ModelState, labelRequest),
        "~/Views/Module/PE.Lite/ZebraPrinter/_Label.cshtml");
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_LabelPrinter, Constants.SmfAuthorization_Module_ZebraPrinterConnector, RightLevel.View)]
    public Task<JsonResult> GetWorkOrderOverviewList([DataSourceRequest] DataSourceRequest request)
    {
      ViewBag.WorkOrderStatuses = ListValuesHelper.GetWorkOrderStatusesList();
      return PrepareJsonResultFromDataSourceResult(() => _service.GetWorkOrderOverviewList(ModelState, request));
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_LabelPrinter, Constants.SmfAuthorization_Module_ZebraPrinterConnector, RightLevel.View)]
    public Task<JsonResult> GetProductsListByWorkOrderId([DataSourceRequest] DataSourceRequest request,
      long workOrderId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _service.GetProductsListByWorkOrderId(ModelState, request, workOrderId));
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_LabelPrinter, Constants.SmfAuthorization_Module_ZebraPrinterConnector, RightLevel.Update)]
    public Task<JsonResult> PrintLabel(long productId)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.PrintLabel(ModelState, productId));
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_LabelPrinter, Constants.SmfAuthorization_Module_ZebraPrinterConnector, RightLevel.View)]
    public Task<JsonResult> LabelPreview(long productId)
    {
      return TaskPrepareJsonResultFromVm<VM_LabelPreview, Task<VM_LabelPreview>>(() => _service.LabelPreview(ModelState, productId));
    }
  }
}
