using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.ViewModel.System;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace PE.HMIWWW.Controllers
{
  [Authorize]
  public class AccountController : BaseController
  {
    #region services

    private readonly IAccountService _accountService;

    #endregion

    #region ctor

    public AccountController(IAccountService service,
      SignInManager<ApplicationUser> signInManager,
      ILogger<AccountController> logger,
      UserManager<ApplicationUser> userManager)
    {
      _accountService = service;
      _userManager = userManager;
      _signInManager = signInManager;
      _logger = logger;
    }

    #endregion

    #region members

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<AccountController> _logger;

    #endregion

    #region user view interface

    public ActionResult ChangePasswordDialog()
    {
      return View();
    }

    public async Task<JsonResult> ChangePassword(VM_AccountPassword viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_AccountPassword, Task<VM_AccountPassword>>(() => ChangePasswordAsync(viewModel));
    }

    [AllowAnonymous]
    public ActionResult Login(string returnUrl)
    {
      ViewBag.ReturnUrl = returnUrl;
      return View();
    }

    //
    // POST: /Account/Login
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }

      // This doesn't count login failures towards account lockout
      // To enable password failures to trigger account lockout, change to shouldLockout: true
      SignInResult result =
        await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

      if (result.Succeeded)
      {
        return RedirectToLocal(returnUrl);
      }

      ModelState.AddModelError(string.Empty, "Invalid login attempt.");
      return View(model);
    }

    [AllowAnonymous]
    public ActionResult Register()
    {
      return View();
    }

    //
    // POST: /Account/Register
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Register(RegisterViewModel model)
    {
      if (await RegisterUserHelper(model))
      {
        return RedirectToAction("Index", "Home");
      }

      return View();
    }

    // POST: /Account/LogOff
    [HttpGet]
    public async Task<IActionResult> LogOff()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index", "Home");
    }

    #endregion

    #region admin view interface

    [SmfAuthorization(Constants.SmfAuthorization_Controller_UserAccountAdministration,
      Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_UserAccountAdministration,
      Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetAccountData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _accountService.GetAccountList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_UserAccounts, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> GetAccountRoles(string id, [DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _accountService.GetRolesList(ModelState, request, id));
    }

    #region edit account

    [SmfAuthorization(Constants.SmfAuthorization_Controller_UserAccountAdministration,
      Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> EditAccountDialog(string id)
    {
      return PreparePopupActionResultFromVm(() => _accountService.GetAccount(ModelState, id), "EditAccountDialog");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_UserAccountAdministration,
      Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> UpdateAccount(VM_Account viewModel)
    {
      return PrepareJsonResultFromVm(() => _accountService.UpdateAccount(ModelState, viewModel));
    }

    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_UserAccounts, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<ActionResult> Details(string id)
    {
      return PreparePopupActionResultFromVm(() => _accountService.GetAccount(ModelState, id), "EditAccountDialog");
    }

    #region edit roles

    [SmfAuthorization(Constants.SmfAuthorization_Controller_UserAccounts, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<ActionResult> EditAccountRolesDialog(string id)
    {
      return PreparePopupActionResultFromVm(() => PrepareStringId(id), "EditAccountRolesDialog");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_UserAccounts, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<JsonResult> UpdateAccountRoles(string roleId, string userId, string isAssigned)
    {
      return PrepareJsonResultFromVm(() => _accountService.UpdateUserInRole(ModelState, roleId, userId, isAssigned));
    }

    #endregion

    #region delete account

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_UserAccountAdministration,
      Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public Task<JsonResult> Delete([DataSourceRequest] DataSourceRequest request, VM_StringId viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_StringId, Task<VM_StringId>>(() => _accountService.DeleteAccountAsync(ModelState, viewModel));
    }

    #endregion

    #endregion

    #region Helpers

    private async Task<bool> RegisterUserHelper(RegisterViewModel registerData)
    {
      if (ModelState.IsValid)
      {
        if (await _userManager.FindByEmailAsync(registerData.Email) == null)
        {
          ApplicationUser user = new ApplicationUser
          {
            Email = registerData.Email,
            UserName = registerData.Email,
            EmailConfirmed = true,
            NormalizedEmail = registerData.Email,
            NormalizedUserName = registerData.Email,
            LanguageId = registerData.Language
          };
          IdentityResult result = await _userManager.CreateAsync(user, registerData.Password);
          if (result.Succeeded)
          {
            _logger.LogInformation("User created a new account with password.");


            await _signInManager.SignInAsync(user, false);
            // returnValue=new VM_StringId(user.Id);
            //RedirectToLocal("");
            return true;
          }
        }
        else
        {
          ModelState.AddModelError("", "User already exist.");
        }
      }

      return false;
    }

    private VM_StringId PrepareStringId(string id)
    {
      return new VM_StringId(id);
    }

    private async Task<VM_AccountPassword> ChangePasswordAsync(VM_AccountPassword viewModel)
    {
      VM_AccountPassword result = null;

      ApplicationUser user = await _userManager.GetUserAsync(User);

      if (user == null)
      {
        AddModelStateError(VM_Resources.GLOB_Account_UserNotFound);
      }

      if (viewModel.Password != viewModel.ConfirmPassword)
      {
        AddModelStateError(VM_Resources.GLOB_Account_UserNotFound);
      }

      bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, viewModel.OldPassword);

      if (!isPasswordCorrect)
      {
        AddModelStateError(VM_Resources.GLOB_Account_OldPassNotValid);
      }

      if (ModelState.IsValid)
      {
        IdentityResult passwordChangeResult = await _userManager.ChangePasswordAsync(user, viewModel.OldPassword, viewModel.Password);

        if (!passwordChangeResult.Succeeded)
        {
          AddModelStateError(VM_Resources.GLOB_Account_PasswordNotChanged);
        }
        else
        {
          result = viewModel;
        }
      }

      return result;
    }

    // Used for XSRF protection when adding external logins
    private const string XsrfKey = "XsrfId";

    //private IAuthenticationManager AuthenticationManager
    //{
    //  get
    //  {
    //    return HttpContext.GetOwinContext().Authentication;
    //  }
    //}

    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }

      return RedirectToAction("Index", "Home");
    }

    //public class ChallengeResult : HttpUnauthorizedResult
    //{
    //  public ChallengeResult(string provider, string redirectUri)
    //          : this(provider, redirectUri, null)
    //  {
    //  }

    //  public ChallengeResult(string provider, string redirectUri, string userId)
    //  {
    //    LoginProvider = provider;
    //    RedirectUri = redirectUri;
    //    UserId = userId;
    //  }

    //  public string LoginProvider { get; set; }
    //  public string RedirectUri { get; set; }
    //  public string UserId { get; set; }

    //  public override void ExecuteResult(ControllerContext context)
    //  {
    //    AuthenticationProperties properties = new AuthenticationProperties { RedirectUri = RedirectUri };
    //    if (UserId != null)
    //    {
    //      properties.Dictionary[XsrfKey] = UserId;
    //    }
    //    context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
    //  }
    //}

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
    }

    #endregion
  }
}
