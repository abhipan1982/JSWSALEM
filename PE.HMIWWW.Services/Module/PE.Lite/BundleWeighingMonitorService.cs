using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Signers;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.PEContext;
using PE.Core;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Schedule;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class BundleWeighingMonitorService : BaseService, IBundleWeighingMonitorService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public BundleWeighingMonitorService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public async Task<VM_RawMaterialOverview> GetRawMaterialOnWeightAsync(ModelStateDictionary modelState, long? rawMaterialId)
    {
      if (rawMaterialId == null)
        return new VM_RawMaterialOverview();

      return new VM_RawMaterialOverview(await _peContext.TRKRawMaterials
        .Include(x => x.QTYQualityInspection)
        .Include(x => x.FKProduct)
        .Include(x => x.FKProduct.FKWorkOrder)
        .Include(x => x.FKProduct.FKWorkOrder.FKHeat)
        .Include(x => x.FKProduct.FKWorkOrder.FKHeat.FKSteelgrade)
        .FirstOrDefaultAsync(x => x.RawMaterialId == rawMaterialId));
    }

    public async Task<DataSourceResult> GetWorkOrdersBeforeWeightListAsync(ModelStateDictionary modelState, DataSourceRequest request)
    {
      var workOrdersNumberOnGrid = 4;

      var data = await _hmiContext.V_ScheduleSummaries
        .Where(x => x.ProductCatalogueTypeCode.ToUpper().Equals(Constants.Bar.ToUpper()) && x.EnumWorkOrderStatus < WorkOrderStatus.Finished.Value)
        .OrderByDescending(x => x.ScheduleOrderSeq)
        .ToListAsync();

      return data
        .TakeLast(workOrdersNumberOnGrid)
        .ToDataSourceLocalResult(request, modelState, x => new VM_ScheduleSummary(x));
    }

    public async Task<DataSourceResult> GetRawMaterialsAfterWeightListAsync(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, long? rawMaterialId)
    {
      var materialsNumberOnGrid = 4;

      var data = await _peContext.TRKRawMaterials
        .Include(x => x.QTYQualityInspection)
        .Include(x => x.FKProduct)
        .Include(x => x.FKProduct.FKWorkOrder)
        .Include(x => x.FKProduct.FKWorkOrder.FKHeat)
        .Include(x => x.FKProduct.FKWorkOrder.FKHeat.FKSteelgrade)
        .Where(x => x.EnumRawMaterialType == RawMaterialType.Bundle && x.RawMaterialId != rawMaterialId)
        .OrderBy(x => x.RawMaterialCreatedTs)
        .ToListAsync();

      return data
        .TakeLast(materialsNumberOnGrid)
        .OrderByDescending(x => x.RawMaterialCreatedTs)
        .ToDataSourceLocalResult(request, modelState, x => new VM_RawMaterialOverview(x));
    }
  }
}
