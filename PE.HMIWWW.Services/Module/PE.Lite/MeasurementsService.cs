using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.Helpers;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Furnace;
using PE.HMIWWW.ViewModel.Module.Lite.Measurements;
using PE.HMIWWW.ViewModel.Module.Lite.MeasurementsSummary;
using PE.HMIWWW.ViewModel.Module.Lite.Visualization; //using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class MeasurementsService : BaseService, IMeasurementsService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public MeasurementsService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public DataSourceResult GetFeatures(ModelStateDictionary modelState, DataSourceRequest request, int areaCode)
    {
      DataSourceResult result = null;

      result = _hmiContext.V_Features
        .Where(x => x.AreaCode == areaCode && x.IsActive)
        .ToDataSourceLocalResult(request, modelState, data => new VM_FeatureMap(data));

      // TODOMN: For all of units not only custom - refresh should be on application start - on production once new unit has come - there should be anyway a restart of application
      // once client would like to have such option - we will prepare an API GET call, which can be called from database - and it will refresh the cache.
      // FeatureUnitConverterHelper.ClearCustomUnitsList();

      return result;
    }

    public DataSourceResult GetMeasurements(ModelStateDictionary modelState, DataSourceRequest request, long featureId,
      long workOrderId)
    {
      DataSourceResult result = null;

      result = _hmiContext.V_RawMaterialMeasurements
        .Where(x => x.FeatureId == featureId && x.WorkOrderId == workOrderId)
        .ToDataSourceLocalResult(request, modelState, data => new VM_RawMaterialMeasurements(data));

      // TODOMN: For all of units not only custom - refresh should be on application start - on production once new unit has come - there should be anyway a restart of application
      // once client would like to have such option - we will prepare an API GET call, which can be called from database - and it will refresh the cache.
      // FeatureUnitConverterHelper.ClearCustomUnitsList();

      return result;
    }

    public IList<VM_RawMaterialInArea> GetRawMaterialWithArea(ModelStateDictionary modelState, long rawMaterialId)
    {
      List<VM_RawMaterialInArea> result = new List<VM_RawMaterialInArea>();
      IQueryable<MVHAsset> dbAreas = _peContext.MVHAssets
        .Where(x => x.IsVisibleOnMVH)
        .AsQueryable();
      foreach (MVHAsset item in dbAreas)
      {
        result.Add(new VM_RawMaterialInArea(rawMaterialId, item));
      }

      return result;
    }

    public DataSourceResult GetMeasurementsByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request,
      long areaCode, long rawMaterialId)
    {
      DataSourceResult result = null;
      TRKRawMaterial material = null;

      var parentRelation = _peContext.TRKRawMaterialRelations
        .FirstOrDefault(x => x.ChildRawMaterialId == rawMaterialId);

      material = _peContext.TRKRawMaterials
        .First(x => x.RawMaterialId == rawMaterialId);

      //RawMaterialId = 82947;
      if (parentRelation != null)
      {
        result = _hmiContext.V_RawMaterialMeasurements
          .Where(x => (x.RawMaterialId == rawMaterialId || x.RawMaterialId == parentRelation.ParentRawMaterialId) &&
          x.AreaCode == areaCode)
          .ToDataSourceLocalResult(request, modelState, data => new VM_RawMaterialMeasurements(data));
      }
      else
      {
        result = _hmiContext.V_RawMaterialMeasurements
          .Where(x => x.RawMaterialId == rawMaterialId && x.AreaCode == areaCode)
          .ToDataSourceLocalResult(request, modelState, data => new VM_RawMaterialMeasurements(data));
      }

      // TODOMN: For all of units not only custom - refresh should be on application start - on production once new unit has come - there should be anyway a restart of application
      // once client would like to have such option - we will prepare an API GET call, which can be called from database - and it will refresh the cache.
      // FeatureUnitConverterHelper.ClearCustomUnitsList();

      return result;
    }


    public async Task<List<VM_AreaRawMaterialMeasurements>> GeMaterialMeasurements(ModelStateDictionary modelState,
      long? rawMaterialId)
    {
      List<VM_AreaRawMaterialMeasurements> result = null;

      if (!rawMaterialId.HasValue || rawMaterialId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      List<V_AreaRawMaterialMeasurement> data = await _hmiContext.V_AreaRawMaterialMeasurements
        .Where(x => x.RawMaterialId == rawMaterialId)
        .OrderBy(x => x.OrderSeq)
        .ToListAsync();

      result = data
        .GroupBy(x => x.AreaName)
        .OrderBy(x => x.Select(o => o.OrderSeq).FirstOrDefault())
        .Select(x => new VM_AreaRawMaterialMeasurements(x.Key)
        {
          AreaMeasurements = x.Select(m => new VM_AreaRawMaterialMeasurement(m)).ToList()
        }).ToList();

      // TODOMN: For all of units not only custom - refresh should be on application start - on production once new unit has come - there should be anyway a restart of application
      // once client would like to have such option - we will prepare an API GET call, which can be called from database - and it will refresh the cache.
      // FeatureUnitConverterHelper.ClearCustomUnitsList();

      return result;
    }

    public IList<VM_Temperature> GetFurnaceTemperatures(ModelStateDictionary modelState)
    {
      List<VM_Temperature> result = new List<VM_Temperature>();

      if (!modelState.IsValid)
      {
        return result;
      }

      IQueryable<V_RawMaterialInFurnace> temperatures =
        _hmiContext.V_RawMaterialInFurnaces.OrderBy(x => x.OrderSeq).AsQueryable();
      foreach (V_RawMaterialInFurnace item in temperatures)
      {
        result.Add(new VM_Temperature(item));
      }

      return result;
    }
  }
}
