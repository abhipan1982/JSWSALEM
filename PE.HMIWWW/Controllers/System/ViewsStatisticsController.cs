using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.System;

namespace PE.HMIWWW.Controllers.System
{
  public class ViewsStatisticsController : BaseController
  {
    #region services

    private readonly IViewsStaticsService _viewsStatisticsService;

    #endregion

    public ViewsStatisticsController(IViewsStaticsService service)
    {
      _viewsStatisticsService = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ViewsStatistics, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ViewsStatistics, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> ViewsStatisticsData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _viewsStatisticsService.GetViewsStatisticsList(ModelState, request));
    }
  }
}
