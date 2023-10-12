using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.Resources;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Interfaces;
using SMF.DbEntity.ExceptionHelpers;

namespace PE.HMIWWW.Core.Service
{
  public abstract class BaseService : IBaseService
  {
    protected readonly IHttpContextAccessor HttpContextAccessor;

    public BaseService(IHttpContextAccessor httpContextAccessor)
    {
      HttpContextAccessor = httpContextAccessor;
    }

    #region IBaseService

    public void SendHmiOperationRequest(HmiInitiator hmiInitiator, String moduleName, int operation)
    {
      IBaseModule client = null;
      try
      {
        client = InterfaceHelper.GetFactoryChannel<IBaseModule>(moduleName);
        DCHmiOperation dc = new DCHmiOperation();
        dc.HmiInitiator = hmiInitiator;
        dc.Operation = operation;
        client.ModuleHmiOperation(dc);
      }
      catch (Exception ex)
      {
      }
    }

    #endregion


    //protected static DataSourceResult HandleError<T>(DataSourceRequest request, ModelStateDictionary modelState, Exception ex)
    //{
    //    DataSourceResult result;
    //    DbExceptionResult exceptionResult = DbExceptionHelper.ProcessException(ex, "GetAllAccounts::Database operation failed!", null);
    //    T list = default(T);

    //    modelState.AddModelError("error", exceptionResult.Message);
    //    foreach (string message in exceptionResult.InnerMessages)
    //    {
    //        modelState.AddModelError("error", message);
    //    }

    //    result = new[] { list }.ToDataSourceLocalResult(request, modelState);
    //    return result;
    //}

    protected void SetCommunicationError(ModelStateDictionary modelState)
    {
      modelState.AddModelError("error", ResourceController.GetErrorText("NoCommunication"));
    }

    protected void AddModelStateError(ModelStateDictionary modelState, string errorText)
    {
      modelState.AddModelError("error", errorText);
    }

    protected void AddModelStateError(ModelStateDictionary modelState, Exception ex)
    {
      DbExceptionResult result = DbExceptionHelper.ProcessException(ex, "DeleteAccount::Database operation failed!",
        GetCurrentLanguageName());
      modelState.AddModelError("error", result.Message);
    }

    protected void HandleModuleWarningMessage<T>(T dataContract, ref ModelStateDictionary modelState)
      where T : DataContractBase
    {
      if (dataContract.ModuleWarningMessage != null && dataContract.ModuleWarningMessage.WarningMessage.Count > 0)
      {
        modelState.TryAddModelException("warning",
          new ModuleWarningMessageException(dataContract.ModuleWarningMessage));
      }
      // Module Result is empty
      else
      {
      }
    }

    #region helpers

    /// <summary>
    ///   Get Current Language.
    /// </summary>
    /// <returns>Return Language Code.</returns>
    protected string GetCurrentLanguageName()
    {
      return HttpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture.Name;
    }

    protected void InitDataContract(DataContractBase dataContract)
    {
      if (dataContract == null)
      {
        dataContract = new DataContractBase();
      }

      ClaimsPrincipal claimsPrincipal = HttpContextAccessor.HttpContext.User;
      string userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;

      dataContract.Sender = "HmiWWW";
      dataContract.TimeStamp = DateTime.Now;
      dataContract.HmiInitiator =
        new HmiInitiator(userId, HttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString());

      if (claimsPrincipal.Claims.Any())
      {
        dataContract.HmiInitiator.SpecialPrivileges = claimsPrincipal.Claims.Any(x => x.Type == "primetals") ? 1 : 0;
      }
    }

    protected void HandleWarnings<T>(SendOfficeResult<T> sendOfficeResult, ref ModelStateDictionary modelState)
      where T : DataContractBase
    {
      if (sendOfficeResult != null)
      {
        if (sendOfficeResult.OperationSuccess)
        {
          if (sendOfficeResult.DataConctract != null)
          {
            HandleModuleWarningMessage(sendOfficeResult.DataConctract, ref modelState);
          }
        }
      }
    }

    #endregion
  }
}
