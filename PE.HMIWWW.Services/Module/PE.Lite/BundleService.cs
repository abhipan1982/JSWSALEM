using System.Linq;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.PEContext;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Material;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class BundleService : BaseService, IBundleService
  {
    private readonly PEContext _peContext;
    private readonly HmiContext _hmiContext;

    public BundleService(IHttpContextAccessor httpContextAccessor, PEContext peContext, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
      _hmiContext = hmiContext;
    }

    public DataSourceResult GetBundleSearchList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _hmiContext.V_BundleSearchGrids.ToDataSourceLocalResult(request, modelState, data => new VM_RawMaterialsTree(data));

      return result;
    }
  }
}
