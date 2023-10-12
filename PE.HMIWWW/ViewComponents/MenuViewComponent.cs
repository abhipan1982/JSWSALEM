using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PE.HMIWWW.ViewModel.System;
using PE.Services.System;

namespace PE.HMIWWW.ViewComponents
{
  public class MenuViewComponent : ViewComponent
  {
    private readonly IMainMenuService _mainMenuService;

    public MenuViewComponent(IMainMenuService mainMenuService)
    {
      _mainMenuService = mainMenuService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
      VM_Menu vMenu = await _mainMenuService.GetMainMenu(ModelState, User.Identity.Name);
      return View("_MainMenu", vMenu);
    }
  }
}
