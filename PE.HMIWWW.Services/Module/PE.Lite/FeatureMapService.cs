using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.PEContext;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.FeatureMap;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class FeatureMapService : BaseService, IFeatureMap
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public FeatureMapService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public DataSourceResult GetFeatureMapOverList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _hmiContext.V_Features
        .ToDataSourceLocalResult(request, modelState, data => new VM_FeatureMap(data));
      // TODOMN: For all of units not only custom - refresh should be on application start - on production once new unit has come - there should be anyway a restart of application
      // once client would like to have such option - we will prepare an API GET call, which can be called from database - and it will refresh the cache.
      // FeatureUnitConverterHelper.ClearCustomUnitsList();

      return result;
    }

    public async Task<VM_FeatureMap> GetFeatureAsync(ModelStateDictionary modelState, long featureId)
    {
      var result = new VM_FeatureMap();

      if (!modelState.IsValid)
        return result;

      return new VM_FeatureMap(await _hmiContext.V_Features
        .FirstAsync(x => x.FeatureId == featureId));
    }

    public async Task<VM_Base> EditFeatureLimitsAsync(ModelStateDictionary modelState, VM_FeatureMap data)
    {
      var result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      var feature = await _peContext.MVHFeatures
        .IgnoreQueryFilters()
        .FirstAsync(x => x.FeatureId == data.FeatureId);

      feature.MinValue = data.MinValue;
      feature.MaxValue = data.MaxValue;

      await _peContext.SaveChangesAsync();

      //return view model
      return result;
    }
  }
}
