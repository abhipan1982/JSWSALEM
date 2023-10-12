using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.HMI;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Material;

namespace PE.HMIWWW.Services.Module
{
  public class CoilWeighingMonitorService : BaseService, ICoilWeighingMonitorService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public CoilWeighingMonitorService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public async Task<VM_RawMaterialOverview> GetRawMaterialOnWeightAsync(ModelStateDictionary modelState, long? rawMaterialId)
    {
      if (rawMaterialId == null)
        return new VM_RawMaterialOverview();

      return new VM_RawMaterialOverview(await _hmiContext.V_RawMaterialOverviews
        .FirstOrDefaultAsync(x => x.RawMaterialId == rawMaterialId));
    }

    public async Task<DataSourceResult> GetRawMaterialsBeforeWeightListAsync(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, DCMaterialPosition materialPosition)
    {
      var materialsNumberOnGrid = 4;
      var areasBeforeWeighing = new List<int>
      {
        TrackingArea.GetValue(TrackingArea.STELMOR_AREA),
        TrackingArea.GetValue(TrackingArea.REFORM_AREA),
        TrackingArea.GetValue(TrackingArea.COIL_WEIGHING_AREA)
      };

      var areas = await _peContext.MVHAssets
        .Where(x => areasBeforeWeighing.Contains(x.AssetCode))
        .OrderBy(x => x.OrderSeq)
        .ToListAsync();

      var materialIds = new List<long>();

      if (materialPosition.Areas is not null)
      {
        foreach (var item in areas)
        {
          var area = materialPosition.Areas.FirstOrDefault(x => x.AreaId == item.AssetCode);
          if (area is null)
            continue;

          var numberOfMaterials = area.Materials.Count();
          var isWeighing = area.AreaId == TrackingArea.GetValue(TrackingArea.COIL_WEIGHING_AREA);
          foreach (var material in area.Materials.OrderBy(x => x.Order))
          {
            numberOfMaterials--;
            if (!(isWeighing && numberOfMaterials == 0))
              materialIds.Add(material.RawMaterialId);
          }
        }
      }

      var rawMaterials = await _hmiContext.V_RawMaterialOverviews
        .Where(x => materialIds.Contains(x.RawMaterialId))
        .ToListAsync();

      rawMaterials = rawMaterials
        .OrderBy(x => materialIds.IndexOf(x.RawMaterialId))
        .ToList();

      return rawMaterials
        .OrderBy(x => materialIds.IndexOf(x.RawMaterialId))
        .TakeLast(materialsNumberOnGrid)
        .ToDataSourceLocalResult(request, modelState, x => new VM_RawMaterialOverview(x));
    }

    public async Task<DataSourceResult> GetRawMaterialsAfterWeightListAsync(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, DCMaterialPosition materialPosition)
    {
      var materialsNumberOnGrid = 4;
      var areasBeforeWeighing = new List<int>
      {
        TrackingArea.GetValue(TrackingArea.TRANSPORT_AREA),
      };

      var areas = await _peContext.MVHAssets
        .Where(x => areasBeforeWeighing.Contains(x.AssetCode))
        .OrderBy(x => x.OrderSeq)
        .ToListAsync();

      var materialIds = new List<long>();

      if (materialPosition.Areas is not null)
      {
        foreach (var item in areas)
        {
          var area = materialPosition.Areas.FirstOrDefault(x => x.AreaId == item.AssetCode);
          if (area is null)
            continue;

          foreach (var material in area.Materials.OrderBy(x => x.Order))
          {
            materialIds.Add(material.RawMaterialId);
          }
        }
      }

      var rawMaterials = await _hmiContext.V_RawMaterialOverviews
        .Where(x => materialIds.Contains(x.RawMaterialId))
        .ToListAsync();

      return rawMaterials
        .OrderBy(x => materialIds.IndexOf(x.RawMaterialId))
        .Take(materialsNumberOnGrid)
        .ToDataSourceLocalResult(request, modelState, x => new VM_RawMaterialOverview(x));
    }
  }
}
