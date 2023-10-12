using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PE.DbEntity.EFCoreExtensions;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.Notification;
using SMF.DbEntity.Models;
using SMF.HMI.Core;

namespace PE.Services.System
{
  public interface IMainMenuService // :  IHmiServiceBase
  {
    Task<VM_Menu> GetMainMenu(ModelStateDictionary modelState, string user);
  }

  public class MainMenuService : BaseService, IMainMenuService
  {
    private readonly HmiContext _hmiContext;
    private readonly SMFContext _smfContext;

    public MainMenuService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, SMFContext smfContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _smfContext = smfContext;
    }

    #region interface IMainMenuService

    public async Task<VM_Menu> GetMainMenu(ModelStateDictionary modelState, string userName)
    {
      VM_Menu vMenu = new VM_Menu();
      var isAdmin = false;
      var isPrimetals = false;
      try
      {
        var user = HttpContextAccessor.HttpContext.User;
        if (user.Identity.IsAuthenticated)
        {
          //check whether use is primetals developer or admin and complete menu should be displayed
          isPrimetals = user.HasClaim(x => x.Type == "primetals" && x.Value == "true");
          if (isPrimetals)
            isAdmin = true;
          else
            isAdmin = user.HasClaim(x => x.Type == "admin" && x.Value == "true");
        }

        SqlParameter[] parameters = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@userName",
                            SqlDbType =  SqlDbType.NVarChar,
                            Direction = ParameterDirection.Input,
                            Value = user.Identity.Name
                        },
                        new SqlParameter() {
                            ParameterName = "@isAdmin",
                            SqlDbType =  SqlDbType.Bit,
                            Direction = ParameterDirection.Input,
                            Value = isAdmin? 1 : 0
                        }};

        var data = _hmiContext.ExecuteSPUserMenu(parameters);
        if (isPrimetals)
          data = data.Where(x => x.IsActive.HasValue && x.IsActive.Value).ToList();
        else
          data = data.Where(x => x.IsActive.HasValue && x.IsActive.Value && x.HmiClientMenuName != "Primetals").ToList();

        vMenu.Menuitems = new VM_MenuItemList(data.Where(x => !x.ParentHmiClientMenuId.HasValue).ToList());
        var children = data.Where(x => x.ParentHmiClientMenuId.HasValue).ToList();

        FillChildrenItems(vMenu.Menuitems, children);
        RemoveEmptyItems(vMenu.Menuitems);

        vMenu.Languages = await GetMainMenuLanguageItems(_smfContext);
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
        AddModelStateError(modelState, ex);
      }

      return vMenu;
    }

    private void RemoveEmptyItems(VM_MenuItemList menuItems)
    {
      for (var i = menuItems.Count - 1; i >= 0; i--)
      {
        var item = menuItems[i];
        //RemoveEmptyItems(item.Children);

        if ((item.Children == null || item.Children.Count == 0) && item.Controller == null)
          menuItems.Remove(item);
      }
    }

    #endregion

    #region private methods

    private void FillChildrenItems(VM_MenuItemList menu, List<DbEntity.SPModels.SPUserMenu> data)
    {
      foreach (VM_MenuItem parent in menu)
      {
        BuildChildrenTree(data, parent);
      }
    }

    private void BuildChildrenTree(List<DbEntity.SPModels.SPUserMenu> items, VM_MenuItem parent)
    {
      List<DbEntity.SPModels.SPUserMenu> children = items
        .Where(x => x.ParentHmiClientMenuId == parent.Id)
        .ToList();

      parent.Children = new VM_MenuItemList(children);

      foreach (VM_MenuItem child in parent.Children)
      {
        BuildChildrenTree(items, child);
      }
    }

    /// <summary>
    ///   Return view model with languages items.
    /// </summary>
    /// <returns>View model list object if found or null.</returns>
    private async Task<List<VM_LanguageItem>> GetMainMenuLanguageItems(SMFContext ctxSMF)
    {
      List<VM_LanguageItem> vLanguageItemList =
        new VM_LanguageItemList(CultureHelper.GetLanguagesList()
        .OrderBy(k => k.LanguageName));

      return await Task.FromResult(vLanguageItemList);
    }

    ///// <summary>
    ///// Filter menu items according to user role
    ///// </summary>
    ///// <param name="menuList">list of menu items</param>
    ///// <param name="user">logged in user</param>
    ///// <returns>list of filtered items</returns>
    //[Obsolete]
    //private List<HmiClientMenu> FilterAuthorizerItems(List<HmiClientMenu> menuList, User user)
    //{
    //	List<HmiClientMenu> returnList = new List<HmiClientMenu>();

    //	foreach (HmiClientMenu cm in menuList)
    //	{
    //		if (SmfAuthorization.CheckIfAuthorized(user, cm.Name))
    //		{
    //			returnList.Add(cm);
    //		}
    //	}

    //	return returnList;
    //}

    #endregion
  }
}
