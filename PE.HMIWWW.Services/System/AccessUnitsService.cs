using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.ViewModel.System;
using SMF.DbEntity.EnumClasses;
using SMF.DbEntity.Models;
using Module = SMF.DbEntity.Models.Module;
using Kendo.Mvc.Extensions;

namespace PE.Services.System
{
  public interface IAccessUnitsService
  {
    DataSourceResult UserRightsPopulate(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);

    DataSourceResult GetExistedAccessUnits(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request);
  }

  public class AccessUnitsService : BaseService, IAccessUnitsService
  {
    private readonly SMFContext _smfContext;

    public AccessUnitsService(IHttpContextAccessor httpContextAccessor, SMFContext smfContext) : base(httpContextAccessor)
    {
      _smfContext = smfContext;
    }

    #region interface IMainMenuService

    public DataSourceResult UserRightsPopulate(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;
      List<VM_UserRights> itemsAdded = new List<VM_UserRights>();

      var accessUnitsFromControllers = 
        GetAccessUnitsFromControllers(_smfContext).Select(x => x.AccessName).Distinct().ToList();
      var accessUnitList = _smfContext.AccessUnits.Select(x => x.AccessUnitName).ToList();

      foreach (var accessUnit in accessUnitsFromControllers)
      {
        if (accessUnitList.Contains(accessUnit))
          continue;
        
        //ToDo: wstawianie batchowe

        AccessUnit newRight = new AccessUnit
        {
          AccessUnitName = accessUnit,
          EnumAccessUnitType = AccessUnitType.Controller.Value,
        };
        
        _smfContext.AccessUnits.Add(newRight);
        itemsAdded.Add(new VM_UserRights(newRight));
      }

      _smfContext.SaveChanges();

      result = itemsAdded.ToDataSourceLocalResult(request, modelState, (x) => x);

      return result;
    }

    public DataSourceResult GetExistedAccessUnits(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;
      List<AccessUnit> list;
      list = _smfContext.AccessUnits
        .AsNoTracking()
        .ToList();


      foreach (AccessUnit item in list)
      {
        item.HmiClientMenus = null;
      }

      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_UserRights(data));

      return result;
    }

    #endregion

    #region private methods

    private IList<VM_UserRights> GetAccessUnitsFromControllers(SMFContext ctxSMF)
    {
      List<VM_UserRights> userRights = new List<VM_UserRights>();

      var modules = ctxSMF.Modules.ToDictionary(x => x.ModuleName, x => x);
      
      Assembly assembly = Assembly.Load("PE.HMIWWW");
      Type[] types = assembly.GetTypes();
      foreach (Type controller in types.Where(c =>
                 c.BaseType.FullName == typeof(BaseController).FullName ||
                 c.BaseType.FullName == typeof(Controller).FullName))
      {
        MethodInfo[] methods = controller.GetMethods();
        foreach (MethodInfo method in methods)
        {
          object[] attributes = method.GetCustomAttributes(typeof(SmfAuthorizationAttribute), false);
          foreach (SmfAuthorizationAttribute attribute in attributes)
          {
            if (!modules.TryGetValue(attribute.ModuleName, out var module))
            {
              throw new ArgumentOutOfRangeException(attribute.ModuleName,
                $"Module not found for method {controller.Name}.{method.Name}");
            }

            VM_UserRights urm = new VM_UserRights();
            urm.Name = controller.Name;
            urm.Method = method.Name;
            urm.AccessName = attribute.AuthObjectName;
            urm.Module = module;
              
            userRights.Add(urm);
          }
        }
      }

      return userRights;
    }
    #endregion
  }
}
