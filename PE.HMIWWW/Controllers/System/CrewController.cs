using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.BaseDbEntity.Models;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.System
{
  public class CrewController : BaseController
  {
    #region services

    private readonly ICrewService _crewService;

    #endregion

    public CrewController(ICrewService service)
    {
      _crewService = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Crew, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Crew, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> GetCrewData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _crewService.GetCrewList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Crew, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<ActionResult> EditCrewDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _crewService.GetCrew(ModelState, id), "EditCrewDialog");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Crew, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public async Task<ActionResult> UpdateCrew(VM_Crew viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _crewService.UpdateCrew(ModelState, viewModel));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Crew, Constants.SmfAuthorization_Module_System,
      RightLevel.Delete)]
    public async Task<ActionResult> Delete([DataSourceRequest] DataSourceRequest request, VM_LongId viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _crewService.DeleteCrew(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Crew, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public ActionResult AddCrewDialog()
    {
      return PartialView("AddCrewDialog");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Crew, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public async Task<ActionResult> InsertCrew(VM_Crew viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _crewService.InsertCrew(ModelState, viewModel));
    }

    public static List<EVTCrew> GetCrewsList()
    {
      return CrewService.GetCrewsList();
    }
  }
}
