using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.LabelPrinter;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface ILabelPrinterService
  {
    DataSourceResult GetWorkOrderOverviewList(ModelStateDictionary modelState, DataSourceRequest request);

    DataSourceResult GetProductsListByWorkOrderId(ModelStateDictionary modelState, DataSourceRequest request,
      long workOrderId);

    Task<VM_LabelRequest> FillLabelRequestForRawMaterialAsync(ModelStateDictionary modelState, VM_LabelRequest request);

    VM_PrintRange GetPrintRangeByWorkOrder(long workOrderId);
    Task<VM_Base> PrintLabels(ModelStateDictionary modelState, VM_PrintRange data);

    Task<VM_Base> PrintLabel(ModelStateDictionary modelState, long productId);

    Task<VM_LabelPreview> LabelPreview(ModelStateDictionary modelState, long productId);
  }
}
