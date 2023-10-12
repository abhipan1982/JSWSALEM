using System.Collections.Generic;
using System.Linq;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.DataAnalysis;
using PE.DbEntity.PEContext;
using PE.DbEntity.DWModels;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class DataAnalysisService : BaseService, IDataAnalysisService
  {
    private readonly HmiContext _hmiContext;
    private readonly DWContext _dwContext;

    public DataAnalysisService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, DWContext dwContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _dwContext = dwContext;
    }

    public DataSourceResult GetDelaysDataList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      List<V_AS_Delay> list = _hmiContext.V_AS_Delays.OrderBy(x => x.DimDate).ToList();

      return list.ToDataSourceLocalResult(request, modelState, data => new VM_DataAnalysisDelay(data));
    }

    public DataSourceResult GetWorkOrdersDataList(ModelStateDictionary modelState, DataSourceRequest request)
    {

      List<FactWorkOrder> list = _dwContext.FactWorkOrders.OrderBy(x => x.DimDate).ToList();

      return list.ToDataSourceLocalResult(request, modelState, data => new VM_DataAnalysisWorkOrder(data));
    }

    public DataSourceResult GetDefectsDataList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      List<V_AS_Defect> list = _hmiContext.V_AS_Defects.OrderBy(x => x.DimDate).ToList();

      return list.ToDataSourceLocalResult(request, modelState, data => new VM_DataAnalysisDefect(data));
    }
  }
}
