using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.Helpers;
using PE.HMIWWW.Core.Parameter;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.Core.DC;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Notification;
using SMF.DbEntity.ExceptionHelpers;
using SMF.Module.Notification;

namespace PE.HMIWWW.Core.Controllers
{
  public class BaseController : Controller
  {
    #region helpers

    public static string GetCultureName()
    {
      return Thread.CurrentThread.CurrentUICulture.Name;
    }

    #endregion

    protected void AddModelStateError(string errorText)
    {
      ModelState.AddModelError("", errorText);
    }

    private bool HandleModelWarningMessage<T>(ref T vmResult) where T : VM_Base
    {
      bool flag = true;
      IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
      foreach (ModelError error in allErrors)
      {
        if (error.Exception is ModuleWarningMessageException)
        {
          ModuleWarningMessageException ex = (ModuleWarningMessageException)error.Exception;
          PrepareWarningMessageFromModuleWarningMessageException(ref ex);

          vmResult.SetModuleWarningMessage(ex.ModuleWarningMessage);
        }
        else
        {
          flag = false;
        }
      }

      return flag;
    }

    private void PrepareWarningMessageFromModuleWarningMessageException(ref ModuleWarningMessageException ex)
    {
      ModuleWarningMessage moduleWarningMessage = ex.ModuleWarningMessage;
      // Validate
      if (moduleWarningMessage == null)
      {
        throw new NullReferenceException();
      }

      // Language
      string cultureName = GetCultureName();
      short loop = 10;
      do
      {
        foreach (ModuleMessage message in moduleWarningMessage.WarningMessage)
        {
          message.SetName(moduleWarningMessage.ModuleName);
          message.SetMessage(NotificarionCore.GetErrorCodeMessage(message.ErrorCode, message.ShortDescription,
            cultureName, message.Args));
        }

        if (moduleWarningMessage.InnerModuleWarningMessage != null)
        {
          moduleWarningMessage = moduleWarningMessage.InnerModuleWarningMessage;
        }
        else
        {
          break;
        }

        --loop;
      } while (loop > 0);
    }

    private void PrepareErrorMessageFromModuleMessageExceptions(ModuleMessageException mEx)
    {
      // get module result and entry validate
      ModuleMessage moduleResult = mEx.ModuleMessage;
      if (moduleResult == null)
      {
        throw new NullReferenceException();
      }

      string cultureName = GetCultureName();

      //----------------end getting culture
      //TODO: Structure moduleResult needs tuning. ModuleName is missplaced with ShortDescription. Other attributs are wrong as well.
      string preparedMessage = String.Format("{0}: {1}, {2}: {3}",
        ResourceController.GetResourceTextByResourceKey("GLOB_Module_Name"), moduleResult.ModuleName,
        ResourceController.GetResourceTextByResourceKey("GLOB_Alarm_Code"), moduleResult.ErrorCode);

      try
      {
        preparedMessage += "\n" + NotificarionCore.GetErrorCodeMessage(moduleResult.ErrorCode,
          moduleResult.ShortDescription, cultureName, moduleResult.Args);
      }
      catch (Exception ex)
      {
        preparedMessage += "\n" +
                           "Wrong alarm configuration, It is possible that alarm parameters are not matching template: " +
                           ex.Message;
      }

      //preparedMessage += "\n" + NotificationController.GetErrorCodeMessage(moduleResult.ModuleName, moduleResult.ErrorCode, cultureName, moduleResult.ShortDescription);
      AddModelStateError(preparedMessage);
    }

    #region members

    private string _hostIp;
    private string _userId;
    protected string UserName;
    private bool _isAuthenticated;

    #endregion

    #region override

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      _hostIp = HttpContext.Request.Host.ToUriComponent();

      ViewBag.User_IsAuthenticated = _isAuthenticated = User.Identity.IsAuthenticated;
      if (_isAuthenticated)
      {
        ViewBag.User_Id = _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        ViewBag.User_Name = UserName = User.Identity.Name;
        if (User.Claims.Count() > 0)
        {
          ViewBag.User_IsSuperuser = User.Claims.Any(x => x.Type == "primetals");

          ViewBag.User_IsAdmin = User.Claims.Any(x => x.Type == "admin");
          ViewBag.User_ScreenOrientation =
            User.Claims.FirstOrDefault(x => x.Type == "HMIViewOrientation")?.Value ?? "LTR";
        }
        else
        {
          ViewBag.User_IsSuperuser = false;
          ViewBag.User_IsAdmin = false;
        }

        TrackingAreaHelpers.InitTrackingAreas();
        ControllerActionDescriptor actionDescriptor =
          ((ControllerBase)ctx.Controller).ControllerContext.ActionDescriptor;

        ViewBag.Page_Title =
          ResourceController.GetPageTitleValue(actionDescriptor.ControllerName, actionDescriptor.ActionName);
        ViewBag.Page_Controller = actionDescriptor.ControllerName;

        string iconName = "header_" + ViewBag.Page_Controller + ".png";
        ViewBag.Page_Icon = "background-image:url(\'/css/ControllerIcon/" + iconName + "\');";

        ViewBag.User_LanguageCode = Thread.CurrentThread.CurrentUICulture.Name;
        ViewBag.NoRecordsInGrid = GetParameter("NoOfRowsInGrid").ValueInt;
        ViewBag.FullGridHeight = GetParameter("FullGridHeight").ValueInt;
        ViewBag.HalfGridHeight = GetParameter("HalfGridHeight").ValueInt;
        ViewBag.ScheduleMoveMode = GetParameter("ScheduleMoveMode").ValueInt;
      }
    }

    #endregion

    #region prepare result

    private JsonResult HandleJsonError()
    {
      string errorText = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

      HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

      return new JsonResult(new { Data = new { Errors = errorText, Url = HttpContext.Request.Path, Code = 500 } });
    }

    private void StoreExceptionsInModelState(Exception ex)
    {
      DbExceptionResult exceptionResult = DbExceptionHelper.ProcessException(ex, "Database operation failed!", null);

      ModelState.AddModelError("error", exceptionResult?.Message ?? "Unexpeted exception");
      if (exceptionResult?.InnerMessages != null)
      {
        foreach (string message in exceptionResult.InnerMessages)
        {
          ModelState.AddModelError("error", message);
        }
      }
    }

    /// <summary>
    ///   Method prepares complete JSON result (including error handling) from DataSourceResult.
    ///   DataSourceResult is main data source for Ttelerik Grids
    /// </summary>
    /// <param name="methodToGetDataFromService">
    ///   Paremeter is method from service injected to current controller returning DataSourceResult
    /// </param>
    /// <returns>
    ///   Return value is JosnResult
    /// </returns>
    protected async Task<JsonResult> PrepareJsonResultFromDataSourceResult<T>(Func<T> methodToGetDataFromService)
      where T : DataSourceResult
    {
      Task task = null;
      try
      {
        DataSourceResult dataSourceResult = null;
        task = new Task(delegate { dataSourceResult = methodToGetDataFromService(); });
        task.Start();
        await task.ConfigureAwait(false);
        if (!ModelState.IsValid)
        {
          return HandleJsonError();
        }

        return Json(dataSourceResult);
      }
      catch (ModuleMessageException mEx)
      {
        NotificationController.LogException(mEx);
        await Task.Factory.StartNew(delegate { PrepareErrorMessageFromModuleMessageExceptions(mEx); })
          .ConfigureAwait(false);
        return HandleJsonError();
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
        task = new Task(delegate { StoreExceptionsInModelState(ex); });
        task.Start();
        await task.ConfigureAwait(false);
        return HandleJsonError();
      }
    }

    /// <summary>
    ///   Method prepares complete JSON result (including error handling) from ViewModel.
    /// </summary>
    /// <param name="methodToGetDataFromService">
    ///   Paremeter is method from service injected to current controller returning ViewModel
    /// </param>
    /// <returns>
    ///   Return value is JosnResult
    /// </returns>
    protected async Task<JsonResult> PrepareJsonResultFromVm<T>(Func<T> methodToGetDataFromService) where T : VM_Base
    {
      Task task = null;
      try
      {
        VM_Base vmResult = null;
        task = new Task(delegate { vmResult = methodToGetDataFromService(); });
        task.Start();
        await task.ConfigureAwait(false);
        if (!ModelState.IsValid)
        {
          return HandleJsonError();
        }

        return Json(vmResult);
      }
      catch (ModuleMessageException mEx)
      {
        NotificationController.LogException(mEx);
        await Task.Factory.StartNew(delegate { PrepareErrorMessageFromModuleMessageExceptions(mEx); })
          .ConfigureAwait(false);
        return HandleJsonError();
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
        task = new Task(delegate { StoreExceptionsInModelState(ex); });
        task.Start();
        await task.ConfigureAwait(false);
        return HandleJsonError();
      }
    }

    /// <summary>
    ///   Method prepares complete JSON result (including error handling) from ViewModel.
    /// </summary>
    /// <param name="methodToGetDataFromService">
    ///   Paremeter is method from service injected to current controller returning ViewModel
    /// </param>
    /// <returns>
    ///   Return value is JosnResult
    /// </returns>
    protected async Task<JsonResult> PrepareJsonResultFromVmAsync<T>(Func<Task<T>> methodToGetDataFromService)
      where T : VM_Base
    {
      Task task = null;
      try
      {
        VM_Base vmResult = await methodToGetDataFromService().ConfigureAwait(false);
        if (!ModelState.IsValid)
        {
          return HandleJsonError();
        }

        return Json(vmResult);
      }
      catch (ModuleMessageException mEx)
      {
        NotificationController.LogException(mEx);
        await Task.Factory.StartNew(delegate { PrepareErrorMessageFromModuleMessageExceptions(mEx); })
          .ConfigureAwait(false);
        return HandleJsonError();
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
        task = new Task(delegate { StoreExceptionsInModelState(ex); });
        task.Start();
        await task.ConfigureAwait(false);
        return HandleJsonError();
      }
    }

    /// <summary>
    ///   Method prepares complete action result from ViewModel. In case error JsonResult is returned.
    /// </summary>
    /// <param name="methodToGetDataFromService">
    ///   Paremeter is method from service injected to current controller returning ViewModel
    /// </param>
    /// <returns>
    ///   Return value is ActionResult
    /// </returns>
    protected async Task<ActionResult> PreparePopupActionResultFromVm<T>(Func<T> methodToGetDataFromService,
      string partialViewModelName) where T : VM_Base
    {
      Task task = null;
      try
      {
        VM_Base vmResult = null;
        task = new Task(delegate { vmResult = methodToGetDataFromService(); });
        task.Start();
        await task.ConfigureAwait(false);
        if (!ModelState.IsValid)
        {
          return HandleJsonError();
        }

        return PartialView(partialViewModelName, vmResult);
      }
      catch (ModuleMessageException mEx)
      {
        NotificationController.LogException(mEx);
        await Task.Factory.StartNew(delegate { PrepareErrorMessageFromModuleMessageExceptions(mEx); })
          .ConfigureAwait(false);
        return HandleJsonError();
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
        task = new Task(delegate { StoreExceptionsInModelState(ex); });
        task.Start();
        await task.ConfigureAwait(false);
        return HandleJsonError();
      }
    }

    /// <summary>
    ///   Method prepares complete action result from ViewModel. .
    /// </summary>
    /// <param name="methodToGetDataFromService">
    ///   Paremeter is method from service injected to current controller returning ViewModel
    /// </param>
    /// <returns>
    ///   Return value is ActionResult
    /// </returns>
    protected async Task<ActionResult> PrepareActionResultFromVm<T>(Func<T> methodToGetDataFromService,
      string partialViewModelName) where T : VM_Base
    {
      Task task = null;
      try
      {
        VM_Base vmResult = null;
        task = new Task(delegate { vmResult = methodToGetDataFromService(); });
        task.Start();
        await task.ConfigureAwait(false);
        if (!ModelState.IsValid)
        {
          return PartialView("Error");
          // return HandleJsonError();
        }

        return PartialView(partialViewModelName, vmResult);
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
        return PartialView("Error");
        //task = new Task(delegate { StoreExceptionsInModelState(ex); });
        //task.Start();
        //await task.ConfigureAwait(false);
        //return HandleJsonError();
      }
    }

    protected async Task<ActionResult> TaskPrepareActionResultFromVm<T>(Func<Task<T>> methodToGetDataFromService,
      string partialViewModelName) where T : VM_Base
    {
      try
      {
        T vmResult = await methodToGetDataFromService().ConfigureAwait(false);
        if (!ModelState.IsValid)
        {
          return PartialView("Error");
          // return HandleJsonError();
        }

        return PartialView(partialViewModelName, vmResult);
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
        return PartialView("Error");
        //task = new Task(delegate { StoreExceptionsInModelState(ex); });
        //task.Start();
        //await task.ConfigureAwait(false);
        //return HandleJsonError();
      }
    }

    protected async Task<ActionResult> PrepareActionResultFromVmList<T>(Func<T> methodToGetDataFromService,
      string partialViewModelName) where T : IEnumerable
    {
      Task task = null;
      try
      {
        IEnumerable vmResult = null;
        task = new Task(delegate { vmResult = methodToGetDataFromService(); });
        task.Start();
        await task.ConfigureAwait(false);
        if (!ModelState.IsValid)
        {
          return PartialView("Error");
          // return HandleJsonError();
        }

        return PartialView(partialViewModelName, vmResult);
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
        return PartialView("Error");
        //task = new Task(delegate { StoreExceptionsInModelState(ex); });
        //task.Start();
        //await task.ConfigureAwait(false);
        //return HandleJsonError();
      }
    }

    protected async Task<JsonResult> TaskPrepareJsonResultFromVm<T, TV>(Func<TV> methodToGetDataFromService)
      where TV : Task<T> where T : VM_Base
    {
      Task task = null;
      try
      {
        T vmResult = await methodToGetDataFromService().ConfigureAwait(false);


        if (!ModelState.IsValid)
        {
          if (!HandleModelWarningMessage(ref vmResult))
          {
            return HandleJsonError();
          }
        }

        return Json(vmResult);
      }
      catch (ModuleMessageException mEx)
      {
        NotificationController.LogException(mEx);
        await Task.Factory.StartNew(delegate { PrepareErrorMessageFromModuleMessageExceptions(mEx); })
          .ConfigureAwait(false);
        return HandleJsonError();
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
        // Type t = ex.InnerException.GetType();
        task = new Task(delegate { StoreExceptionsInModelState(ex); });
        task.Start();
        await task.ConfigureAwait(false);
        return HandleJsonError();
      }
    }

    public SMF.DbEntity.Models.Parameter GetParameter(string parameterName)
    {
      return ParameterController.GetParameter(parameterName);
    }

    #endregion
  }
}
