using System.Collections.Generic;
using System.Linq;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.ViewModel.System;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.Services.System
{
  public interface IViewsStaticsService
  {
    DataSourceResult GetViewsStatisticsList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request);
  }

  public class ViewsStaticsService : BaseService, IViewsStaticsService
  {
    private readonly SMFContext _smfContext;

    public ViewsStaticsService(IHttpContextAccessor httpContextAccessor, SMFContext smfContext) : base(httpContextAccessor)
    {
      _smfContext = smfContext;
    }

    #region interface

    public DataSourceResult GetViewsStatisticsList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult returnValue = null;

      // VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      //DB OPERATION 
      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      //var list = uow.Repository<ViewsStatistic>().Query().OrderBy(z => z.OrderByDescending(s => s.TimePerRecord)).Get().ToList();
      List<ViewsStatistic> list = _smfContext.ViewsStatistics.OrderByDescending(o => o.TimePerRecord).ToList();
      returnValue = list.ToDataSourceLocalResult(request, modelState, data => new VM_ViewsStatistics(data));
      //END OF DB OPERATION

      return returnValue;
    }

    #endregion
  }
}
