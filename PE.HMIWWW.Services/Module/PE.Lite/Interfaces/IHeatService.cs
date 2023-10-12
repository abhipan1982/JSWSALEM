using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IHeatService
  {
    VM_HeatOverview GetHeatDetails(ModelStateDictionary modelState, long heatId);
    VM_HeatOverview GetHeatOverviewByWorkOrderId(ModelStateDictionary modelState, long workOrderId);
    DataSourceResult GetHeatOverviewList(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetMaterialsListByHeatId(ModelStateDictionary modelState, DataSourceRequest request, long heatId);
    DataSourceResult GetWorkOrderListByHeatId(ModelStateDictionary modelState, DataSourceRequest request, long heatId);
    Task<VM_Base> CreateHeat(ModelStateDictionary modelState, VM_Heat heat);
    Task<VM_Base> EditHeat(ModelStateDictionary modelState, VM_Heat heat);
    IList<PRMHeatSupplier> GetSupplierList();
    IList<PRMSteelgrade> GetSteelgradeList();
    IList<PRMMaterialCatalogue> GetMaterialList();
    Task<IList<VM_HeatAutocomplete>> GetHeatsByAnyFeaure(string text, bool isTest);
    Task<VM_Heat> GetHeat(long id);
  }
}
