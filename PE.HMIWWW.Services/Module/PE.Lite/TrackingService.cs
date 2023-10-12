using System.Linq;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Tracking;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class TrackingService : BaseService, ITrackingService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;
    public TrackingService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public DataSourceResult GetTrackingList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _hmiContext.V_RawMaterialOverviews.Where(x => x.EnumRawMaterialStatus < RawMaterialStatus.Rolled.Value)
        .ToDataSourceLocalResult(request, modelState, x => new VM_TrackingOverview(x));

      return result;
    }

    public VM_TrackingOverview GetTrackingDetails(ModelStateDictionary modelState, long dimRawMaterialKey,
      long? workOrderId, long? heatId)
    {
      VM_TrackingOverview result = null;
      V_RawMaterialOverview data = null;

      if (dimRawMaterialKey <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      data = _hmiContext.V_RawMaterialOverviews
        .Where(x => x.RawMaterialId == dimRawMaterialKey)
        .Single();

      PRMWorkOrder woData = _peContext.PRMWorkOrders
        .Include(i => i.FKMaterialCatalogue)
        .Where(x => x.WorkOrderId == workOrderId)
        .SingleOrDefault();

      PRMHeat heatData = _peContext.PRMHeats
        .Include(i => i.FKHeatSupplier)
        .Include(i => i.FKSteelgrade)
        .Where(x => x.HeatId == heatId)
        .SingleOrDefault();

      result = new VM_TrackingOverview(data, woData, heatData);

      return result;
    }
  }
}
