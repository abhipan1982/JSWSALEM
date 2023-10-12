using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PE.Common;
using PE.HMIWWW.Core.Helpers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.Notification;
using SMF.DbEntity.EnumClasses;
using SMF.DbEntity.ExceptionHelpers;
using SMF.DbEntity.Models;
using Kendo.Mvc.Extensions;

namespace PE.HMIWWW.Services.System
{
  public interface IRoleService
  {
    DataSourceResult GetRolesList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);

    DataSourceResult GetRightsType(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request,
      string roleId, short rightsType);

    VM_Role InsertRole(ModelStateDictionary modelState, VM_Role viewModel);
    VM_StringId UpdateRole(ModelStateDictionary modelState, VM_Role viewModel);
    VM_StringId DeleteRole(ModelStateDictionary modelState, VM_StringId viewModel);
    VM_Role GetRole(ModelStateDictionary modelState, string id);

    VM_StringId UpdateRight(ModelStateDictionary modelState, int roleRightId, long accessUnitId, string roleId,
      bool isAssigned, short permission);

    SelectList GetPermissionTypesController();
    SelectList GetPermissionTypesMenu();
    SelectList GetAccessUnitsMenu();
    SelectList GetAccessUnitsController();
  }

  public class RoleService : BaseService, IRoleService
  {
    private readonly SMFContext _smfContext;

    public RoleService(IHttpContextAccessor httpContextAccessor, SMFContext smfContext) : base(httpContextAccessor)
    {
      _smfContext = smfContext;
    }

    #region interface IRoleService

    public DataSourceResult GetRolesList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult returnValue = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      returnValue = _smfContext.Roles
        .Include(i => i.Users)
        .Include(i => i.RoleRights)
        .ToDataSourceLocalResult(request, modelState, data => new VM_Role(data));

      return returnValue;
    }

    public VM_Role InsertRole(ModelStateDictionary modelState, VM_Role viewModel)
    {
      VM_Role returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (viewModel.Name == null || viewModel.Name == "")
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (IfRoleExists(viewModel.Name))
      {
        AddModelStateError(modelState, VM_Resources.ERROR_MustBeUnique);
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      Role role = new Role();
      role.Name = viewModel.Name;
      role.Id = Guid.NewGuid().ToString();
      role.Description = viewModel.Description;
      _smfContext.Roles.Add(role);
      _smfContext.SaveChanges();
      returnValueVm = viewModel;
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_StringId UpdateRole(ModelStateDictionary modelState, VM_Role viewModel)
    {
      VM_StringId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (viewModel.Id == null || viewModel.Id == "")
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      Role role = _smfContext.Roles.Single(w => w.Id == viewModel.Id);
      role.Name = viewModel.Name;
      role.Description = viewModel.Description;
      _smfContext.SaveChanges();
      returnValueVm = new VM_StringId(viewModel.Id);
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_StringId DeleteRole(ModelStateDictionary modelState, VM_StringId viewModel)
    {
      VM_StringId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (viewModel.Id == null || viewModel.Id == "")
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION

      Role role = _smfContext.Roles.Where(w => w.Id == viewModel.Id).Single();
      if (role != null)
      {
        _smfContext.Roles.Remove(role);
        _smfContext.SaveChanges();
        returnValueVm = viewModel;
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_Role GetRole(ModelStateDictionary modelState, string id)
    {
      VM_Role returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (id == null || id == "")
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION


      //DB OPERATION
      Role user = _smfContext.Roles.Single(w => w.Id == id);
      if (user != null)
      {
        returnValueVm = new VM_Role(user);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_StringId UpdateRight(ModelStateDictionary modelState, int roleRightId, long accessUnitId, string roleId,
      bool isAssigned, short permission)
    {
      VM_StringId returnValueVm = null;

      if (accessUnitId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (roleId == null || roleId == "")
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      RoleRight entityObject = _smfContext.RoleRights.FirstOrDefault(w => w.Id == roleRightId);
      if (isAssigned)
      {
        if (entityObject == null)
        {
          entityObject = new RoleRight
          {
            RoleId = roleId,
            AccessUnitId = accessUnitId
          };

          if (permission != 0)
          {
            entityObject.PermissionType = permission;
          }

          _smfContext.RoleRights.Add(entityObject);
          _smfContext.SaveChanges();
          returnValueVm = new VM_StringId(roleRightId.ToString());
        }
        else
        {
          if (permission != 0)
          {
            entityObject.PermissionType = permission;
          }

          _smfContext.SaveChanges();
          returnValueVm = new VM_StringId(roleRightId.ToString());
        }
      }
      else
      {
        if (entityObject != null)
        {
          _smfContext.RoleRights.Remove(entityObject);
          _smfContext.SaveChanges();
          returnValueVm = new VM_StringId(roleRightId.ToString());
        }
        else
        {
          returnValueVm = new VM_StringId(roleRightId.ToString());
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public DataSourceResult GetRightsType(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, string roleId, short rightsType)
    {
      DataSourceResult returnValue = null;

      //VALIDATE ENTRY PARAMETERS
      if (roleId == null || roleId == "")
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      VM_RightList rightListModel = null;
      VM_Right tmpElement = null;
      IList<AccessUnit> accessUnits =
        _smfContext.AccessUnits.Where(w => w.EnumAccessUnitType == rightsType).OrderBy(o => o.AccessUnitName).ToList();

      rightListModel = new VM_RightList(accessUnits, roleId);

      IList<RoleRight> rights = _smfContext.RoleRights
        .Where(x => x.AccessUnit.EnumAccessUnitType == rightsType && x.RoleId == roleId)
        .Include(i => i.AccessUnit)
        .ToList();

      foreach (RoleRight item in rights)
      {
        tmpElement = rightListModel.FirstOrDefault(x => x.AccessUnitId == item.AccessUnitId);
        if (tmpElement != null)
        {
          tmpElement.Id = item.Id;
          tmpElement.PermissionType = item.PermissionType;
          tmpElement.RoleId = item.RoleId;
          tmpElement.Assigned = true;
        }
      }

      returnValue = rightListModel.ToDataSourceLocalResult(request, (x) => x);

      return returnValue;
    }

    #region helpers

    public SelectList GetPermissionTypesController()
    {
      return SelectListHelpers.GetSelectList<PermissionType, int>();
    }

    public SelectList GetPermissionTypesMenu()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();
      SelectList tmpSelectList = null;
      try
      {
        resultDictionary.Add(1, ResxHelper.GetResxByKey(PermissionType.View));
        tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(CommonAlarmDefs.AlarmCode_ExceptionInViewBagMethod,
          String.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name,
            ex.Message), MethodBase.GetCurrentMethod().Name, ex.Message);
        NotificationController.LogException(ex,
          String.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name,
            ex.Message));
      }

      return tmpSelectList;
    }

    public SelectList GetAccessUnitsMenu()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();
      SelectList tmpSelectList = null;
      try
      {
        IList<AccessUnit> accessUnits = _smfContext.AccessUnits
          .Where(w => w.EnumAccessUnitType == AccessUnitType.HmiMenu.Value)
          .OrderBy(o => o.AccessUnitName)
          .ToList();

        foreach (AccessUnit item in accessUnits)
        {
          resultDictionary.Add((int)item.AccessUnitId,
            string.Format("{0}", VM_Resources.ResourceManager.GetString(item.AccessUnitName)));
        }

        tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(CommonAlarmDefs.AlarmCode_ExceptionInViewBagMethod,
          string.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name,
            ex.Message), MethodBase.GetCurrentMethod().Name, ex.Message);
        NotificationController.LogException(ex,
          string.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name,
            ex.Message));
      }

      return tmpSelectList;
    }

    public SelectList GetAccessUnitsController()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();
      SelectList tmpSelectList = null;
      try
      {
        IList<AccessUnit> accessUnits = _smfContext.AccessUnits
          .Where(w => w.EnumAccessUnitType == AccessUnitType.Controller.Value)
          .OrderBy(o => o.AccessUnitName)
          .ToList();
        foreach (AccessUnit item in accessUnits)
        {
          string translation = VM_Resources.ResourceManager.GetString(item.AccessUnitName);
          resultDictionary.Add((int)item.AccessUnitId,
            string.Format("{0}", translation == null || translation == "" ? item.AccessUnitName : translation));
        }

        tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(CommonAlarmDefs.AlarmCode_ExceptionInViewBagMethod,
          string.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name,
            ex.Message), MethodBase.GetCurrentMethod().Name, ex.Message);
        NotificationController.LogException(ex,
          string.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name,
            ex.Message));
      }

      return tmpSelectList;
    }

    #endregion

    #endregion

    #region public methods

    #endregion

    #region private methods

    /// <summary>
    ///   Get list of all roles in system.
    /// </summary>
    /// <returns>List of Roles object.</returns>
    private List<Role> GetAllRoles()
    {
      List<Role> rolesList = new List<Role>();
      try
      {
        rolesList = _smfContext.Roles.Include(i => i.Users).ToList();
      }
      catch (Exception ex)
      {
        DbExceptionResult result =
          DbExceptionHelper.ProcessException(ex, "GetAllRoles::Database operation failed!", null);
      }

      return rolesList;
    }

    private bool IfRoleExists(string roleName)
    {
      bool retValue;
      try
      {
        Role role = _smfContext.Roles.SingleOrDefault(w => w.Name == roleName);
        if (role != null)
        {
          retValue = true;
        }
        else
        {
          retValue = false;
        }
      }
      catch (Exception ex)
      {
        DbExceptionResult result =
          DbExceptionHelper.ProcessException(ex, "IfRoleExists::Database operation failed!", null);
        retValue = true;
      }

      return retValue;
    }

    #endregion
  }
}
