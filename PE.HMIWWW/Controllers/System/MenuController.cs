using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.ViewModel.System;
using PE.Services.System;

namespace PE.HMIWWW.Controllers.System
{
  public class MenuController : BaseController
  {
    #region services

    private readonly IMainMenuService _mainMenuService;

    #endregion

    #region ctor

    public MenuController(IMainMenuService mainMenuService)
    {
      _mainMenuService = mainMenuService;
    }

    #endregion

    #region interface

    public async Task<ActionResult> Index()
    {
      VM_Menu vMenu = await _mainMenuService.GetMainMenu(ModelState, User.Identity.Name);
      return PartialView("menu/_MainMenu", vMenu);
    }
  }

  #endregion
}
