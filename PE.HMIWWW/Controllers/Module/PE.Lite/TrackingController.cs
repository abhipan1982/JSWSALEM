using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class TrackingController : BaseController
  {
    #region members

    private readonly ITrackingService _service;

    #endregion

    #region ctor

    public TrackingController(ITrackingService service)
    {
      _service = service;
    }

    #endregion

    #region funcs

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Tracking, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Tracking/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetTrackingList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetTrackingList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Tracking, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long dimRawMaterialKey, long? workOrderId, long? heatId)
    {
      return PrepareActionResultFromVm(
        () => _service.GetTrackingDetails(ModelState, dimRawMaterialKey, workOrderId, heatId),
        "~/Views/Module/PE.Lite/Tracking/_TrackingBody.cshtml");
    }

    #endregion
  }
}
