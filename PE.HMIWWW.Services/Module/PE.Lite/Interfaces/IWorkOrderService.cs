using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.KPI;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IWorkOrderService
  {
    DataSourceResult GetWorkOrderOverviewList(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetWorkOrderInRealizationList(ModelStateDictionary modelState, DataSourceRequest request);
    VM_WorkOrderOverview GetWorkOrderDetails(ModelStateDictionary modelState, long workOrderId);

    DataSourceResult GetMaterialsListByWorkOrderId(ModelStateDictionary modelState, DataSourceRequest request,
      long workOrderId);

    DataSourceResult GetNoScheduledWorkOrderList(ModelStateDictionary modelState, DataSourceRequest request);
    Task<VM_Base> CreateWorkOrder(ModelStateDictionary modelState, VM_WorkOrder workOrder);
    Task<VM_Base> EditWorkOrder(ModelStateDictionary modelState, VM_WorkOrder workOrder);

    IList<PRMHeat> GetHeatList();
    IList<PRMProductCatalogue> GetProductList();
    IList<PRMCustomer> GetCustomerList();
    IList<PRMMaterialCatalogue> GetMaterialList();
    Task<VM_Base> DeleteWorkOrder(ModelStateDictionary modelState, VM_WorkOrderOverview workOrder);
    Task<VM_WorkOrder> GetWorkOrder(long? id);
    Task<long?> GetWorkOrderIdByMaterialId(long id);
    Task<VM_BilletCatalogueDetails> GetBilletCatalogueDetails(long billetCatalogId);
    Task<VM_ProductCatalogue> GetProductCatalogueDetails(long productCatalogId);
    Task<VM_WorkOrderMaterials> GetWorkOrderMaterialsDetails(ModelStateDictionary modelState, long workOrderId);
    Task<VM_Base> EditMaterialNumber(ModelStateDictionary modelState, VM_WorkOrderMaterials workOrderMaterials);
    DataSourceResult GetWorkOrderProducts(ModelStateDictionary modelState, DataSourceRequest request, long workOrderId);
    DataSourceResult GetWorkOrderEvents(ModelStateDictionary modelState, DataSourceRequest request, long workOrderId);
    DataSourceResult GetWorkOrderDelays(ModelStateDictionary modelState, DataSourceRequest request, long workOrderId);
    DataSourceResult GetWorkOrderDefects(ModelStateDictionary modelState, DataSourceRequest request, long workOrderId);
    DataSourceResult GetWorkOrderRejects(ModelStateDictionary modelState, DataSourceRequest request, long workOrderId);
    DataSourceResult GetWorkOrderScraps(ModelStateDictionary modelState, DataSourceRequest request, long workOrderId);
    VM_WorkOrderSummary GetWorkOrderSummary(long workOrderId);
    Task<VM_Base> SendWorkOrderReportAsync(ModelStateDictionary modelState, VM_WorkOrderConfirmation workOrderConfirmation);
    Task<VM_Base> CancelWorkOrder(ModelStateDictionary modelState, long workOrderId);
    Task<VM_Base> UnCancelWorkOrder(ModelStateDictionary modelState, long workOrderId);
    Task<VM_Base> BlockWorkOrder(ModelStateDictionary modelState, long workOrderId);
    Task<VM_Base> UnBlockWorkOrder(ModelStateDictionary modelState, long workOrderId);
  }
}
