using System;
using System.Collections.Generic;
using System.Linq;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.ViewModel.System;
using SMF.DbEntity.Models;
using Microsoft.AspNetCore.Identity;
using PE.HMIWWW.Core.Authorization;
using System.Threading.Tasks;

namespace PE.HMIWWW.Services.System
{
  public interface IAccountService
  {
    DataSourceResult GetAccountList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);

    DataSourceResult GetRolesList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request,
      string id);

    VM_Account GetAccount(ModelStateDictionary modelState, string id);

    VM_Account UpdateAccount(ModelStateDictionary modelState, VM_Account viewModel);
    Task<VM_StringId> DeleteAccountAsync(ModelStateDictionary modelState, VM_StringId viewModel);

    VM_StringId UpdateUserInRole(ModelStateDictionary modelState, string roleId, string userId, string isAssigned);
  }

  public class AccountService : BaseService, IAccountService
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SMFContext _smfContext;

    public AccountService(IHttpContextAccessor httpContextAccessor,
      UserManager<ApplicationUser> userManager, SMFContext smfContext) : base(httpContextAccessor)
    {
      _userManager = userManager;
      _smfContext = smfContext;
    }

    #region private methods

    private User GetUserById(string id)
    {
      User user = new User();

      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{

      //User = uow.Repository<User>().Query(z => z.Id == Id)
      //                                                                    .Include(z => z.UserLogins)
      //                                                                    .Include(z => z.UserRoles.Select(y => y.Role))
      //                                                                    .Include(z => z.UserClaims)
      //                                                                    .GetSingle();
      user = _smfContext.Users
        .Where(w => w.Id == id)
        .Include(i => i.UserLogins)
        .Include(i => i.UserClaims)
        .Include(i => i.Roles)
        .Single();

      return user;
    }

    #endregion

    #region interface IAccountService

    public DataSourceResult GetAccountList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult returnValue = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      IEnumerable<User> list = _smfContext.Users
        .Include(i => i.UserLogins)
        .Include(j => j.Roles)
        .Include(k => k.UserClaims)
        .ToList();
      returnValue = list.ToDataSourceLocalResult(request, modelState, data => new VM_Account(data));

      return returnValue;
    }

    public DataSourceResult GetRolesList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request,
      string id)
    {
      DataSourceResult returnValue = null;

      //VALIDATE ENTRY PARAMETERS
      if (id == string.Empty)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValue;
      }

      //END OF VALIDATION
      User user = GetUserById(id);
      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      //var list = uow.Repository<Role>().Query().Get();
      IEnumerable<Role> list = _smfContext.Roles.ToList();
      returnValue = list.ToDataSourceLocalResult(request, modelState, data => new VM_UserInRoles(user, data));

      return returnValue;
    }

    public VM_Account GetAccount(ModelStateDictionary modelState, string id)
    {
      VM_Account returnValueVm = new VM_Account();

      //VALIDATE ENTRY PARAMETERS
      if (id == string.Empty)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION


      //DB OPERATION
      // using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      //User user = uow.Repository<User>().Find(id);
      User user = _smfContext.Users.Find(id);
      if (user != null)
      {
        returnValueVm = new VM_Account(user);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_Account UpdateAccount(ModelStateDictionary modelState, VM_Account viewModel)
    {
      VM_Account returnValueVm = viewModel;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (viewModel.Id == string.Empty)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      //User user = uow.Repository<User>().Find(viewModel.Id);
      User user = _smfContext.Users.Find(viewModel.Id);
      user.UserName = viewModel.UserName;
      user.FirstName = viewModel.FirstName;
      user.LastName = viewModel.LastName;
      user.JobPosition = viewModel.JobPosition;
      user.HMIViewOrientation = (Int16)(viewModel.LeftToRight ? 1 : 0);
      //uow.Repository<User>().UpdateGraph(user);
      //uow.SaveChanges();
      _smfContext.SaveChanges();
      //END OF DB OPERATION

      return returnValueVm;
    }

    public async Task<VM_StringId> DeleteAccountAsync(ModelStateDictionary modelState, VM_StringId viewModel)
    {
      VM_StringId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (viewModel.Id == String.Empty)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      var user = await _userManager.FindByIdAsync(viewModel.Id);

      if (user != null)
      {
        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
          returnValueVm = viewModel;
        }
        else
        {
          AddModelStateError(modelState, VM_Resources.GLOB_Account_UserNotDeleted);
        }
      }

      return returnValueVm;
    }

    public VM_StringId UpdateUserInRole(ModelStateDictionary modelState, string roleId, string userId,
      string isAssigned)
    {
      VM_StringId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (roleId == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (userId == string.Empty)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION


      //DB OPERATION
      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      //User user = uow.Repository<User>().Query(z => z.Id == userId).GetSingle();
      //Role role = uow.Repository<Role>().Query(z => z.Id == roleId).GetSingle();
      User user = _smfContext.Users.Include(x => x.Roles).Single(x => x.Id == userId);
      Role role = _smfContext.Roles.Single(x => x.Id == roleId);
      returnValueVm = new VM_StringId(user.Id);
      if (user != null && role != null)
      {
        if (isAssigned == "true")
        {
          user.Roles.Add(role);
          //uow.Repository<UserRole>().InsertOrUpdateGraph(userRole);
          //uow.SaveChanges();
          _smfContext.SaveChanges();
        }
        else
        {
          //UserRole userRole = uow.Repository<UserRole>().Query(z => (z.UserId == user.Id) && (z.RoleId == role.Id)).GetSingle();
          Role userRole = user.Roles.Single(x => x.Id == role.Id);
          user.Roles.Remove(userRole);

          //uow.SaveChanges();
          _smfContext.SaveChanges();
        }
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    #endregion

    #region public methods

    #endregion
  }
}
