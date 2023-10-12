using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PE.HMIWWW.Core.Dtos;
using SMF.Core.Notification;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.Core.Authorization
{
  public enum RightLevel : short { View = 1, Update, Delete }

  public class SmfAuthorizationAttribute : TypeFilterAttribute
  {
    public SmfAuthorizationAttribute(string authObjectName, string moduleName, RightLevel rightLevel) : base(
      typeof(AuthorizationFilter))
    {
      AuthObjectName = authObjectName;
      ModuleName = moduleName;
      Arguments = new object[] { authObjectName, rightLevel };
    }

    public string AuthObjectName { get; }

    public string ModuleName { get; }
  }

  public class AuthorizationFilter : IAuthorizationFilter
  {
    private readonly IMemoryCache _cache;
    private readonly string _authObjectName;
    private readonly RightLevel _rightLevel;

    public AuthorizationFilter(string authObjectName, RightLevel rightLevel, IMemoryCache cache)
    {
      _rightLevel = rightLevel;
      _authObjectName = authObjectName;
      _cache = cache;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
      ClaimsPrincipal userContext = context.HttpContext.User;

      if (!userContext.Identity.IsAuthenticated)
      {
        context.Result = new ForbidResult();
        return;
      }


      bool hasAdminClaim = userContext.Claims.Any(c => c.Type == "admin" && c.Value == "true");

      if (!hasAdminClaim)
      {
        string userId = userContext.FindFirst(ClaimTypes.NameIdentifier).Value;

        if (!_cache.TryGetValue(userId, out List<RoleRightsDto> roles))
        {
          roles = GetUserRoles(userId);
          _cache.Set(userId, roles, DateTimeOffset.Now.AddMinutes(180));
        }

        if (!IsInRole(roles))
        {
          using var ctxSMF = new SMFContext();
          if (ctxSMF.RoleRights.Any(x => x.Role.Users.Any(ur => ur.Id == userId)
            && x.AccessUnit.AccessUnitName == _authObjectName
            && x.PermissionType >= (short)_rightLevel))
            _cache.Remove(userId);
          else
            context.Result = new ForbidResult();
        }
      }
    }

    private bool IsInRole(List<RoleRightsDto> roleRights)
    {
      foreach (RoleRightsDto r in roleRights)
      {
        if (r.AccessUnitName == _authObjectName && r.PermissionType >= (short)_rightLevel)
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    ///   Get Full Info for roles.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>List of Roles.</returns>
    //TODO cache it
    private static List<RoleRightsDto> GetUserRoles(string userId)
    {
      try
      {
        using (SMFContext ctxSMF = new SMFContext())
        {
          return ctxSMF.RoleRights
            .Include(x => x.Role)
            .Include(x => x.AccessUnit)
            .Where(x => x.Role.Users.Any(ur => ur.Id == userId))
            .Select(x => new RoleRightsDto { PermissionType = x.PermissionType, AccessUnitName = x.AccessUnit.AccessUnitName })
            .ToList();
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }

      return new List<RoleRightsDto>();
    }
  }
}
//public override void OnAuthorization(AuthorizationContext filterContext)
//{
//  base.OnAuthorization(filterContext);

//  _authController = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
//}
//protected override bool AuthorizeCore(HttpContextBase httpContext)
//{
//  ApplicationUserManager userManager = httpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
//  ApplicationUser applicationUser = userManager.FindById(httpContext.GetOwinContext().Authentication.User.Identity.GetUserId());

//  if (applicationUser == null)
//    return false;
//  else
//  {
//    return CheckIfRequestAuthorized( applicationUser, _authObjectName, (int)_rightLevel);
//  }
//}
//protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
//{
//  if (filterContext.HttpContext.Request.IsAjaxRequest())
//  {
//    UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
//    filterContext.HttpContext.Response.StatusCode = 403;
//    filterContext.HttpContext.Response.Charset = "utf-8";
//    filterContext.Result = new JsonResult
//    {
//      Data = new
//      {
//        Data = new
//        {
//          Errors = String.Format(ResourceController.GetGlobalResourceTextByResourceKey("UnauthAccessError"), filterContext.HttpContext.Request.Url.AbsolutePath),
//          Url = filterContext.HttpContext.Request.Url.AbsolutePath,
//          Code = 403,
//        }
//      },
//      JsonRequestBehavior = JsonRequestBehavior.AllowGet
//    };
//  }
//  else
//    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login", returnUrl = filterContext.HttpContext.Request.Url.PathAndQuery }));
//}
