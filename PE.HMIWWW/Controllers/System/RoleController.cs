using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.System
{
  public class RoleController : BaseController
  {
    #region services

    private readonly IRoleService _roleService;

    #endregion

    #region ctor

    public RoleController(IRoleService service)
    {
      _roleService = service;
    }

    #endregion

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      ViewBag.PermissionTypesController = _roleService.GetPermissionTypesController();
      ViewBag.PermissionTypesMenu = _roleService.GetPermissionTypesMenu();
      ViewBag.AccessUnitsMenu = _roleService.GetAccessUnitsMenu();
      ViewBag.AccessUnitsController = _roleService.GetAccessUnitsController();

      base.OnActionExecuting(filterContext);
    }

    #region view interface

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> GetRoleData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _roleService.GetRolesList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<ActionResult> EditRoleDialog(string id)
    {
      return PreparePopupActionResultFromVm(() => _roleService.GetRole(ModelState, id), "EditRoleDialog");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<JsonResult> UpdateRole(VM_Role viewModel)
    {
      return PrepareJsonResultFromVm(() => _roleService.UpdateRole(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public ActionResult AddRoleDialog()
    {
      return PartialView("AddRoleDialog");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<JsonResult> InsertRole(VM_Role viewModel)
    {
      return PrepareJsonResultFromVm(() => _roleService.InsertRole(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public ActionResult EditAccessUnitsDialog(string id)
    {
      return PartialView("EditAccessUnitsDialog", id);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System,
      RightLevel.Delete)]
    public Task<JsonResult> UpdateAccessUnitPermission(int roleRightId, long accessUnitId, string roleId,
      bool isAssigned, short permission)
    {
      return PrepareJsonResultFromVm(() =>
        _roleService.UpdateRight(ModelState, roleRightId, accessUnitId, roleId, isAssigned, permission));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System,
      RightLevel.Delete)]
    public Task<JsonResult> Delete([DataSourceRequest] DataSourceRequest request, VM_StringId viewModel)
    {
      return PrepareJsonResultFromVm(() => _roleService.DeleteRole(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Role, Constants.SmfAuthorization_Module_System,
      RightLevel.Delete)]
    public Task<JsonResult> GetRightsTypeData(string roleId, short rightsType,
      [DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _roleService.GetRightsType(ModelState, request, roleId, rightsType));
    }

    #endregion
  }
}
