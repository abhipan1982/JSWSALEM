using System.Linq;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Asset;
using PE.HMIWWW.ViewModel.Module.Lite.Feature;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class AssetService : BaseService, IAssetService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public AssetService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public DataSourceResult GetAssetOverList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      IQueryable<V_Asset> list = _hmiContext.V_Assets.AsQueryable();

      return _hmiContext.V_Assets.ToDataSourceLocalResult(request, modelState, data => new VM_Asset(data));
    }

    public DataSourceResult GetFeatureByAssetId(ModelStateDictionary modelState, DataSourceRequest request,
      long assetId)
    {
      IQueryable<MVHFeature> list = _peContext.MVHFeatures.Where(x => x.FKAssetId == assetId).AsQueryable();

      return list.ToDataSourceLocalResult(request, modelState, data => new VM_Feature(data));
    }
  }
}
