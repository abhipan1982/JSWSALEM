using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.ProductYard;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IProductYardService
  {
    DataSourceResult GetWorkOrdersOnYards(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetFinishedWorkOrders(ModelStateDictionary modelState, DataSourceRequest request);
    IList<VM_ProductYard> GetYards();
    Task<VM_ProductYardDetails> GetLocations(long id);
    Task<VM_ProductLocationDetails> GetWorkOrdersInLocation(long locationId);

    DataSourceResult GetWorkOrdersInLocationList(ModelStateDictionary modelState, DataSourceRequest request,
      long locationId);

    DataSourceResult GetLocationsByWorkOrder(ModelStateDictionary modelState, DataSourceRequest request,
      long? workOrderId);

    DataSourceResult GetProductsInLocationByWo(ModelStateDictionary modelState, DataSourceRequest request,
      long workOrderId, long locationId);

    DataSourceResult GetProductsOnYards(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetLocations(ModelStateDictionary modelState, DataSourceRequest request);
    Task<VM_SearchResult> SearchLocationIds(ModelStateDictionary modelState, long workOrderId, long yardId);
    Task<VM_Base> RelocateProduct(ModelStateDictionary modelState, long targetLocationId, List<long> sourceLocations, List<long> products);
    Task<VM_Base> DispatchWorkOrder(ModelStateDictionary modelState, long workOrderId);
    Task<VM_Base> ReorderLocationSeq(ModelStateDictionary modelState, long locationId, short oldIndex, short newIndex);
  }
}
