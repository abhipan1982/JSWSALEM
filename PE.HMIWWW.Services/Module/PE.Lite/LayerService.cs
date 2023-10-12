using System.Linq;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.UnitConverter;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class LayerService : BaseService, ILayerService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public LayerService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public DataSourceResult GetLayerSearchList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      IQueryable<V_LayerSearchGrid> list = _hmiContext.V_LayerSearchGrids.AsQueryable();

      return list.ToDataSourceLocalResult(request, modelState, data => new VM_RawMaterialsTree(data));
    }

    public VM_RawMaterialOverview GetLayerDetails(ModelStateDictionary modelState, long rawMaterialId)
    {
      VM_RawMaterialOverview result = null;

      if (rawMaterialId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      TRKRawMaterial material = _peContext.TRKRawMaterials
        .Single(x => x.RawMaterialId == rawMaterialId);

      TRKRawMaterialRelation parentRelation = _peContext.TRKRawMaterialRelations
      .Include(x => x.ParentRawMaterial)
      .FirstOrDefault(x => x.ChildRawMaterialId == rawMaterialId);

      result = new VM_RawMaterialOverview(material, parentRelation?.ParentRawMaterial);

      return result;
    }

    public VM_RawMaterialMeasurements GetMeasurementDetails(ModelStateDictionary modelState, long measurementId)
    {
      VM_RawMaterialMeasurements result = null;

      if (measurementId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      V_RawMaterialMeasurement data = _hmiContext.V_RawMaterialMeasurements
        .FirstOrDefault(x => x.MeasurementId == measurementId);

      result = new VM_RawMaterialMeasurements(data);

      return result;
    }

    public VM_RawMaterialHistory GetHistoryDetails(ModelStateDictionary modelState, long rawMaterialStepId)
    {
      VM_RawMaterialHistory result = null;

      if (rawMaterialStepId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      V_RawMaterialHistory data = _hmiContext.V_RawMaterialHistories
        .FirstOrDefault(x => x.RawMaterialStepId == rawMaterialStepId);

      result = new VM_RawMaterialHistory(data);

      return result;
    }

    public DataSourceResult GetMeasurmentsByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request,
      long rawMaterialId)
    {
      DataSourceResult result = null;
      TRKRawMaterial material = _peContext.TRKRawMaterials
        .First(x => x.RawMaterialId == rawMaterialId);

      var parentRelation = _peContext.TRKRawMaterialRelations
        .FirstOrDefault(x => x.ChildRawMaterialId == rawMaterialId);

      IQueryable<V_RawMaterialMeasurement> list;

      if (parentRelation is not null)
      {
        list = _hmiContext.V_RawMaterialMeasurements
          .Where(x => x.RawMaterialId == rawMaterialId || x.RawMaterialId == parentRelation.ParentRawMaterialId)
          .AsQueryable();
      }
      else
      {
        list = _hmiContext.V_RawMaterialMeasurements
          .Where(x => x.RawMaterialId == rawMaterialId)
          .AsQueryable();
      }

      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_RawMaterialMeasurements(data));

      return result;
    }

    public DataSourceResult GetHistoryByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request,
      long rawMaterialId)
    {
      IQueryable<V_RawMaterialHistory> list = _hmiContext.V_RawMaterialHistories
        .Where(x => x.RawMaterialId == rawMaterialId)
        .AsQueryable();

      V_Asset asset = new V_Asset();

      return list.ToDataSourceLocalResult(request, modelState, data => new VM_RawMaterialHistory(data));
    }

    public DataSourceResult GetChildrenByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request,
      long rawMaterialId)
    {
      IQueryable<V_RawMaterialOverview> list = _hmiContext.V_RawMaterialOverviews
        .Where(x => x.ParentLayerId == rawMaterialId)
        .AsQueryable();

      return list.ToDataSourceLocalResult(request, modelState, data => new VM_RawMaterialOverview(data));
    }
  }
}
