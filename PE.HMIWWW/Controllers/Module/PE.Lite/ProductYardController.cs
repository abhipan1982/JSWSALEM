using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.ProductYard;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class ProductYardController : BaseController
  {
    private readonly IProductYardService _productYardService;

    public ProductYardController(IProductYardService service)
    {
      _productYardService = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public ActionResult GetYardsView()
    {
      IList<VM_ProductYard> yards = _productYardService.GetYards();
      return PartialView("~/Views/Module/PE.Lite/ProductYard/_Yards.cshtml", yards);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public ActionResult GetLocationSchedulePartialView()
    {
      return PartialView("~/Views/Module/PE.Lite/ProductYard/_LocationScheduling.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public ActionResult GetShipmentPartialView()
    {
      return PartialView("~/Views/Module/PE.Lite/ProductYard/_Shipment.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public ActionResult GetPileContenPartialtView(long id)
    {
      return PartialView("~/Views/Module/PE.Lite/ProductYard/_PileContent.cshtml", id);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BilletYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public ActionResult GetRelocationPartialView()
    {
      return PartialView("~/Views/Module/PE.Lite/ProductYard/_Relocation.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public async Task<ActionResult> GetWorkOrdersOnYards([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() =>
        _productYardService.GetWorkOrdersOnYards(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public async Task<ActionResult> GetFinishedWorkOrders([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() =>
        _productYardService.GetFinishedWorkOrders(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public ActionResult GetProductsInLocationView([DataSourceRequest] DataSourceRequest request, VM_ProductOnYard data)
    {
      return PartialView("~/Views/Module/PE.Lite/ProductYard/_ProductsInLocationGrid.cshtml", data);
      //return await PrepareJsonResultFromDataSourceResult(() => _productYardService.GetProductsInLocationByWO(ModelState, request, workOrderId, locationId), "~/Views/Module/PE.Lite/ProductYard/_ProductsInLocationGrid.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public async Task<ActionResult> GetProductsInLocationByWo([DataSourceRequest] DataSourceRequest request,
      long workOrderId, long locationId)
    {
      return await PrepareJsonResultFromDataSourceResult(() =>
        _productYardService.GetProductsInLocationByWo(ModelState, request, workOrderId, locationId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public async Task<ActionResult> GetProductsOnYards([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() =>
        _productYardService.GetProductsOnYards(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public async Task<ActionResult> GetYardMapView(long id)
    {
      VM_ProductYardDetails locations = await _productYardService.GetLocations(id);
      return PartialView("~/Views/Module/PE.Lite/ProductYard/_YardMap.cshtml", locations);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public async Task<ActionResult> GetSearchedLocationsIds(long workOrderId, long yardId)
    {
      return await TaskPrepareJsonResultFromVm<VM_SearchResult, Task<VM_SearchResult>>(() =>
        _productYardService.SearchLocationIds(ModelState, workOrderId, yardId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public async Task<ActionResult> GetLocations([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _productYardService.GetLocations(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public async Task<ActionResult> GetLocationsByWorkOrder([DataSourceRequest] DataSourceRequest request,
      long? workOrderId)
    {
      return await PrepareJsonResultFromDataSourceResult(() =>
        _productYardService.GetLocationsByWorkOrder(ModelState, request, workOrderId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.Update)]
    public async Task<ActionResult> ReorderLocationSeq(long locationId, short oldIndex, short newIndex)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _productYardService.ReorderLocationSeq(ModelState, locationId, oldIndex, newIndex));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public async Task<ActionResult> GetWorkOrdersInLocationList([DataSourceRequest] DataSourceRequest request,
      long locationId)
    {
      return await PrepareJsonResultFromDataSourceResult(() =>
        _productYardService.GetWorkOrdersInLocationList(ModelState, request, locationId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.View)]
    public async Task<ActionResult> GetWorkOrdersInLocation(long locationId)
    {
      return await TaskPrepareJsonResultFromVm<VM_ProductLocationDetails, Task<VM_ProductLocationDetails>>(() =>
        _productYardService.GetWorkOrdersInLocation(locationId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.Update)]
    public async Task<ActionResult> DispatchWorkOrder(long workOrderId)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _productYardService.DispatchWorkOrder(ModelState, workOrderId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ProductYard, Constants.SmfAuthorization_Module_Yards,
      RightLevel.Update)]
    public async Task<ActionResult> ProductsRelocation(long targetLocationId, List<long> sourceLocations, List<long> products)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _productYardService.RelocateProduct(ModelState, targetLocationId, sourceLocations, products));
    }
  }
}
