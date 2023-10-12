using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.LabelPrinter;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Products;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using PE.DbEntity.PEContext;
using SMF.Core.Communication;
using SMF.Core.DC;
using PE.HMIWWW.Core.Communication;
using PE.BaseModels.DataContracts.Internal.ZPC;
using Microsoft.EntityFrameworkCore;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  /// <summary>
  /// Not Used
  /// </summary>
  public class LabelPrinterService : BaseService, ILabelPrinterService
  {
    private readonly PEContext _peContext;
    private readonly HmiContext _hmiContext;

    public LabelPrinterService(IHttpContextAccessor httpContextAccessor, PEContext peContext, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
      _hmiContext = hmiContext;
    }

    public DataSourceResult GetWorkOrderOverviewList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      IQueryable<V_WorkOrderSummary> workOrderList = _hmiContext.V_WorkOrderSummaries.AsQueryable();
      return workOrderList.ToDataSourceLocalResult(request, modelState, data => new VM_WorkOrderSummary(data));
    }

    public VM_PrintRange GetPrintRangeByWorkOrder(long workOrderId)
    {
      IQueryable<V_RawMaterialLabel> materialsList =
        _hmiContext.V_RawMaterialLabels.Where(x => x.WorkOrderId == workOrderId).AsQueryable();
      if (materialsList.Count() > 0)
      {
        short maxSequence = materialsList.Max(x => x.MaterialSeqNo);
        short minSequence = materialsList.Min(x => x.MaterialSeqNo);
        return new VM_PrintRange(workOrderId, minSequence, maxSequence);
      }

      return new VM_PrintRange(workOrderId);
    }

    public async Task<VM_Base> PrintLabels(ModelStateDictionary modelState, VM_PrintRange data)
    {
      // TODO: Send label to printer
      VM_Base result = new VM_Base();
      await Task.CompletedTask;
      return result;
    }

    public async Task<VM_Base> PrintLabel(ModelStateDictionary modelState, long productId)
    {
      VM_Base result = new VM_Base();

      SendOfficeResult<DCZebraPrinterResponse> sendOfficeResult =
        await HmiSendOffice.PrintLabel(new DCZebraRequest { Id = productId });

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_LabelPreview> LabelPreview(ModelStateDictionary modelState, long productId)
    {
      VM_LabelPreview result = new VM_LabelPreview();

      SendOfficeResult<DCZebraImageResponse> sendOfficeResult =
        await HmiSendOffice.RequestLabelPreview(new DCZebraRequest { Id = productId });

      HandleWarnings(sendOfficeResult, ref modelState);

      if (sendOfficeResult.OperationSuccess)
      {
        result.ImageBase64 = sendOfficeResult.DataConctract.ImageBase64;
      }

      return result;
    }

    public DataSourceResult GetProductsListByWorkOrderId(ModelStateDictionary modelState, DataSourceRequest request,
      long workOrderId)
    {
      IQueryable<V_Product> workOrderList = _hmiContext.V_Products.Where(x => x.WorkOrderId == workOrderId).AsQueryable();
      return workOrderList.ToDataSourceLocalResult(request, modelState, data => new VM_ProductsList(data));
    }

    public async Task<VM_LabelRequest> FillLabelRequestForRawMaterialAsync(ModelStateDictionary modelState, VM_LabelRequest request)
    {
      request ??= new VM_LabelRequest();
      var rawMaterial = await _peContext.TRKRawMaterials.FirstAsync(r => r.RawMaterialId == request.RawMaterialId);
      request.ProductId = rawMaterial.FKProductId;
      return request;
    }
  }
}
