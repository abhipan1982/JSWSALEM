using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using PE.DbEntity.EFCoreExtensions;
using PE.DbEntity.HmiModels;
using PE.DbEntity.PEContext;
using PE.DbEntity.SPModels;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.System;
using SMF.DbEntity.Models;
using SMF.HMI.Core;

namespace PE.Services.System
{
  public interface IAlarmsService
  {
    DataSourceResult GetAlarmList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request,
      string languageCode);

    DataSourceResult GetShortAlarmList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request,
      string languageCode);

    VM_Base Confirm(ModelStateDictionary modelState, long alarmId, string userId);
    VM_AlarmItem GetAlarmDetails(ModelStateDictionary modelState, long alarmId);
  }

  public class AlarmsService : BaseService, IAlarmsService
  {
    private readonly HmiContext _hmiContext;
    private readonly SMFContext _smfContext;

    public AlarmsService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, SMFContext smfContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _smfContext = smfContext;
    }

    #region interface IAlarmsService

    public DataSourceResult GetAlarmList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request,
      string languageCode)
    {
      DataSourceResult returnValue = null;

      //VALIDATE ENTRY PARAMETERS
      if (languageCode == null || languageCode == "")
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      var language = CultureHelper.GetLanguagesList().FirstOrDefault(x => x.LanguageCode == languageCode) ??
                     CultureHelper.GetLanguagesList().FirstOrDefault(x => x.LanguageCode == CultureHelper.GetDefaultCulture());

      IQueryable<V_Alarm> list = _hmiContext.V_Alarms.Where(w => w.LanguageId == language.LanguageId).AsQueryable();

      request.RenameRequestMember("Message", "MessageTextFilled");
      request.RenameRequestMember("ConfirmedBy", "UserName");
      request.RenameRequestMember("AlarmCategoryName", "CategoryCode");
      request.RenameRequestMember("AlarmType", "EnumAlarmType");
      request.RenameRequestMember("ToConfirm", "IsToConfirm");

      returnValue = list.ToDataSourceLocalResult(request, modelState, data => new VM_AlarmItem(data));

      return returnValue;
    }


    public DataSourceResult GetShortAlarmList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, string languageCode)
    {
      DataSourceResult returnValue = null;
      //VALIDATE ENTRY PARAMETERS
      if (languageCode == null || languageCode == "")
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      var language = CultureHelper.GetLanguagesList().FirstOrDefault(x => x.LanguageCode == languageCode) ??
                     CultureHelper.GetLanguagesList().FirstOrDefault(x => x.LanguageCode == CultureHelper.GetDefaultCulture());

      SqlParameter[] parameters = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@LanguageId",
                            SqlDbType =  SqlDbType.BigInt,
                            Direction = ParameterDirection.Input,
                            Value = language.LanguageId
                        }};

      IList<SPGetAlarmsByLanguage> list = _hmiContext.ExecuteSPGetAlarmsByLanguage(parameters)
        .OrderByDescending(z => z.AlarmDate).ToList();
      returnValue = list.ToDataSourceLocalResult(request, modelState, data => new VM_AlarmItem(data));

      return returnValue;
    }

    public VM_Base Confirm(ModelStateDictionary modelState, long alarmId, String userId)
    {
      VM_Base returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (alarmId <= 0)
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
      //Alarm alarm = uow.Repository<Alarm>().Find(alarmId);
      Alarm alarm = _smfContext.Alarms.Find(alarmId);
      if (alarm == null || alarm.IsConfirmed == true)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("NoConfirmationNeeded"));
        return returnValueVm;
      }

      //alarm.State = ObjectState.Modified;
      alarm.IsConfirmed = true;
      alarm.ConfirmationDate = DateTime.Now;
      alarm.FKUserIdConfirmed = userId;
      //uow.Repository<Alarm>().Update(alarm);
      //uow.SaveChanges();
      _smfContext.SaveChanges();
      returnValueVm = new VM_Base();
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_AlarmItem GetAlarmDetails(ModelStateDictionary modelState, long alarmId)
    {
      VM_AlarmItem returnValueVm = new VM_AlarmItem();

      //VALIDATE ENTRY PARAMETERS
      if (alarmId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      returnValueVm = new VM_AlarmItem(_hmiContext.V_Alarms.First(x => x.AlarmId == alarmId));
      //END OF DB OPERATION

      return returnValueVm;
    }

    #endregion
  }
}
