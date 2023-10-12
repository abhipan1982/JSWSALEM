using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PE.Core;
using PE.HMIWWW.Areas.AlarmManagement.Services;
using PE.HMIWWW.Areas.AlarmManagement.ViewModels;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.Areas.AlarmManagement.Controllers
{
  [Area("AlarmManagement")]
  [Authorize]
  public class AlarmCreatorController : BaseController
  {
    private static long ProjectId { get; set; }
    private static string CustomPrefix { get; set; }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ProjectId = 1;
      CustomPrefix = "P_";
      ViewBag.AlarmTypeList = ListValuesHelper.GetAlarmTypes();
      ViewBag.AlarmCategoriesList = GetAlarmCategorySelectList();
      ViewBag.ProjectId = ProjectId;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public IActionResult Index()
    {
      return View();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public async Task<IActionResult> ElementDetails(long alarmDefinitionId)
    {
      using var context = new SMFContext();
      var model = await context.AlarmDefinitions
        .Include(x => x.FKAlarmCategory)
        .Include(x => x.FKProject)
        .FirstAsync(x => x.AlarmDefinitionId == alarmDefinitionId);

      return PartialView("~/Areas/AlarmManagement/Views/AlarmCreator/_AlarmCreatorBody.cshtml",
        new VM_AlarmDefinition(model));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public async Task<IActionResult> GetAlarmCreatorSearchList([DataSourceRequest] DataSourceRequest request)
    {
      using var context = new SMFContext();

      return Ok(await context.AlarmDefinitions
        .Include(x => x.FKAlarmCategory)
        .Include(x => x.FKProject)
        .ToDataSourceResultAsync(request, data => new VM_AlarmDefinition(data)));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public async Task<IActionResult> GetAlarmMessagesSearchGridData([DataSourceRequest] DataSourceRequest request, long alarmDefinitionId)
    {
      using var context = new SMFContext();

      return Ok(await context.AlarmMessages
        .Where(x => x.FKAlarmDefinitionId == alarmDefinitionId)
        .Include(x => x.FKLanguage)
        .Include(x => x.FKAlarmDefinition)
        .ToDataSourceResultAsync(request, data => new VM_AlarmMessage(data)));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public async Task<IActionResult> GetAlarmParametersSearchGridData([DataSourceRequest] DataSourceRequest request, long alarmDefinitionId)
    {
      using var context = new SMFContext();

      return Ok(await context.AlarmDefinitionParams
        .Where(x => x.FKAlarmDefinitionId == alarmDefinitionId)
        .ToDataSourceResultAsync(request, data => new VM_AlarmParameter(data)));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public IActionResult AlarmMessagesView(long alarmDefinitionId)
    {
      return PartialView("~/Areas/AlarmManagement/Views/AlarmCreator/_AlarmMessages.cshtml", alarmDefinitionId);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public IActionResult AlarmDefinitionCreatePopup()
    {
      using var context = new SMFContext();
      ViewBag.AlarmCategoryList = GetAlarmCategoryList(context);
      ViewBag.ModuleList = GetModuleList(context);

      var model = new VM_AlarmCreator
      {
        FKProjectId = ProjectId,
        Messages = new List<VM_AlarmMessage>()
      };

      var languages = context.Languages
        .Where(x => x.Active.HasValue && x.Active.Value)
        .ToList();
      foreach (var item in languages)
      {
        var message = new VM_AlarmMessage
        {
          FKLanguageId = item.LanguageId,
          LanguageCode = item.LanguageCode
        };

        if (message.LanguageCode.ToUpper().Equals("EN-US"))
          model.DefaultMessage = message;
        else
          model.Messages.Add(message);
      }

      return PartialView("~/Areas/AlarmManagement/Views/AlarmCreator/_AlarmDefinitionCreatePopup.cshtml", model);
    }

    public async Task<IActionResult> CreateAlarmDefinition(VM_AlarmCreator alarmDefinition)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        InsertAlarmDefinition(ModelState, alarmDefinition));
    }

    public async Task<IActionResult> AlarmDefinitionEditPopup(long alarmDefinitionId)
    {
      using var context = new SMFContext();
      ViewBag.AlarmCategoryList = GetAlarmCategoryList(context);
      ViewBag.ModuleList = GetModuleList(context);

      var alarmDefinition = await context.AlarmDefinitions
        .Include(x => x.AlarmMessages)
        .Include(x => x.AlarmDefinitionParams)
        .FirstAsync(x => x.AlarmDefinitionId == alarmDefinitionId);
      var model = new VM_AlarmCreator(alarmDefinition)
      {
        Messages = new List<VM_AlarmMessage>()
      };

      var languages = context.Languages
        .Where(x => x.Active.HasValue && x.Active.Value)
        .ToList();
      foreach (var item in languages)
      {
        var message = new VM_AlarmMessage
        {
          FKLanguageId = item.LanguageId,
          LanguageCode = item.LanguageCode
        };

        if (message.LanguageCode.ToUpper().Equals("EN-US"))
          model.DefaultMessage = new VM_AlarmMessage(alarmDefinition.AlarmMessages.First(x => x.FKLanguageId == message.FKLanguageId));
        else
        {
          var currentMessage = alarmDefinition.AlarmMessages.FirstOrDefault(x => x.FKLanguageId == message.FKLanguageId);
          if (currentMessage != null)
            model.Messages.Add(new VM_AlarmMessage(currentMessage));
          else
            model.Messages.Add(message);
        }
      }

      var alarmParameter = alarmDefinition.AlarmDefinitionParams.FirstOrDefault(x => x.ParamKey == 0);
      if (alarmParameter != null)
        model.Parameter0 = new VM_AlarmParameter(alarmParameter);

      alarmParameter = alarmDefinition.AlarmDefinitionParams.FirstOrDefault(x => x.ParamKey == 1);
      if (alarmParameter != null)
        model.Parameter1 = new VM_AlarmParameter(alarmParameter);

      alarmParameter = alarmDefinition.AlarmDefinitionParams.FirstOrDefault(x => x.ParamKey == 2);
      if (alarmParameter != null)
        model.Parameter2 = new VM_AlarmParameter(alarmParameter);

      alarmParameter = alarmDefinition.AlarmDefinitionParams.FirstOrDefault(x => x.ParamKey == 3);
      if (alarmParameter != null)
        model.Parameter3 = new VM_AlarmParameter(alarmParameter);

      return PartialView("~/Areas/AlarmManagement/Views/AlarmCreator/_AlarmDefinitionEditPopup.cshtml", model);
    }

    public async Task<IActionResult> EditAlarmDefinition(VM_AlarmCreator alarmDefinition)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        UpdateAlarmDefinition(ModelState, alarmDefinition));
    }

    public IActionResult AlarmRolesView(long alarmDefinitionId)
    {
      return PartialView("~/Areas/AlarmManagement/Views/AlarmCreator/_AlarmRoles.cshtml", alarmDefinitionId);
    }

    public async Task<IActionResult> GetRoles([DataSourceRequest] DataSourceRequest request, long alarmDefinitionId)
    {
      using var context = new SMFContext();
      var rolesWithAlarms = await context.AlarmDefinitionsToRoles.Where(x => x.FKAlarmDefinitionId == alarmDefinitionId).ToListAsync();
      List<Role> roles = await context.Roles.ToListAsync();
      List<VM_AlarmRole> result = new List<VM_AlarmRole>();

      foreach (var item in roles)
      {
        VM_AlarmRole element = new VM_AlarmRole(item, rolesWithAlarms.FirstOrDefault(x => x.FKRoleId == item.Id));
        result.Add(element);
      }

      return Ok(await result.ToDataSourceResultAsync(request));
    }

    [HttpPost]
    public async Task<IActionResult> UnassignAlarmFromRole(long alarmDefinitionToRoleId)
    {
      using var context = new SMFContext();
      var alarmDefinitionsToRole = await context.AlarmDefinitionsToRoles.FirstOrDefaultAsync(x => x.AlarmDefinitionToRoleId == alarmDefinitionToRoleId);
      context.AlarmDefinitionsToRoles.Remove(alarmDefinitionsToRole);
      await context.SaveChangesAsync();

      return new StatusCodeResult(200);
    }

    [HttpPost]
    public async Task<IActionResult> AssignAlarmToRole(string roleId, long alarmDefinitionId)
    {
      using var context = new SMFContext();
      await context.AlarmDefinitionsToRoles.AddAsync(new AlarmDefinitionsToRole { FKAlarmDefinitionId = alarmDefinitionId, FKRoleId = roleId });
      await context.SaveChangesAsync();

      return new StatusCodeResult(200);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAlarmDefinition(long alarmDefinitionId)
    {
      using var context = new SMFContext();
      AlarmDefinition alarmDefinition = await context.AlarmDefinitions.FirstAsync(x => x.AlarmDefinitionId == alarmDefinitionId);
      context.AlarmDefinitions.Remove(alarmDefinition);
      await context.SaveChangesAsync();

      return new StatusCodeResult(200);
    }

    [HttpPost]
    public async Task<IActionResult> RefreshAlarms()
    {
      await Task.CompletedTask;
      return new StatusCodeResult(200);
    }

    public async Task<IActionResult> GenerateAlarmCodeByModuleCode(string moduleCode, bool isStandard)
    {
      using var context = new SMFContext();
      moduleCode = isStandard ? moduleCode.ToUpper() : $"{CustomPrefix}{moduleCode}".ToUpper();
      var data = await context.AlarmDefinitions
        .OrderByDescending(x => x.DefinitionCode)
        .FirstOrDefaultAsync(x => x.DefinitionCode.ToUpper().StartsWith(moduleCode));

      if (data == null)
        return Json($"{moduleCode}001");
      else
      {
        var definitionNumber = data.DefinitionCode[^3..];
        var newDefinitionNumber = Convert.ToInt32(definitionNumber) + 1;
        return Json($"{moduleCode}{newDefinitionNumber.ToString().PadLeft(3, '0')}");
      }
    }

    public async Task<IActionResult> AlarmDefinitionGeneratePopup()
    {
      await Task.CompletedTask;
      return PartialView("~/Areas/AlarmManagement/Views/AlarmCreator/_AlarmDefinitionGeneratePopup.cshtml");
    }

    public async Task<IActionResult> GenerateAlarmDefinition(bool type)
    {
      using var context = new SMFContext();
      return Json(await new AlarmsGenerator(context).GenerateAsync(type));
    }

    private static IList<AlarmCategory> GetAlarmCategoryList(SMFContext context)
    {
      var result = context.AlarmCategories
        .ToList();

      var systemCategory = result.First(x => x.CategoryCode.ToUpper().Equals("SYSTEM"));
      result = result.Where(x => !x.CategoryCode.ToUpper().Equals("SYSTEM")).ToList();
      result.Add(systemCategory);

      return result;
    }

    private static bool ValidateParameters(VM_AlarmParameter parameter, string parameterKey, List<VM_AlarmMessage> messages, VM_AlarmMessage defaultMessage)
    {
      bool result = true;

      List<VM_AlarmMessage> messagesToCheck = new List<VM_AlarmMessage>();

      foreach (VM_AlarmMessage message in messages)
      {
        messagesToCheck.Add(message);
      }
      messagesToCheck.Add(defaultMessage);

      foreach (var item in messagesToCheck)
      {
        if (string.IsNullOrEmpty(parameter.ParamName) && !string.IsNullOrEmpty(item.MessageText) && item.MessageText.Contains("{" + parameterKey + "}"))
          return false;
        else if (string.IsNullOrEmpty(parameter.ParamName))
          return result;
        else if (string.IsNullOrEmpty(item.MessageText))
          continue;
        else if (!item.MessageText.Contains("{" + parameterKey + "}"))
          return false;
      }

      return result;
    }

    private static async Task<VM_Base> InsertAlarmDefinition(ModelStateDictionary modelState, VM_AlarmCreator alarmDefinition)
    {
      VM_Base result = new VM_Base();

      using var context = new SMFContext();

      if (alarmDefinition.DefaultMessage?.MessageText == null)
        modelState.AddModelError("error", "Default Alarm Message Cannot Be Empty");
      if (alarmDefinition.DefinitionCode.ToUpper().Contains(CustomPrefix) && alarmDefinition.IsStandard.HasValue && alarmDefinition.IsStandard.Value)
        modelState.AddModelError("error", "Cannot Generate Standard Alarm With Custom Code. Select Module Again");
      if (!alarmDefinition.DefinitionCode.ToUpper().Contains(CustomPrefix) && (!alarmDefinition.IsStandard.HasValue || !alarmDefinition.IsStandard.Value))
        modelState.AddModelError("error", "Cannot Generate Custom Alarm With Standard Code. Select Module Again");
      if (await context.AlarmDefinitions.FirstOrDefaultAsync(x => x.DefinitionDescription.ToUpper().Equals(alarmDefinition.DefinitionDescription.ToUpper())) is not null)
        modelState.AddModelError("error", "Definition With This Description Already Exists");

      foreach (var item in alarmDefinition.Messages)
      {
        if (string.IsNullOrEmpty(item.MessageText))
        {
          item.MessageText = alarmDefinition.DefaultMessage?.MessageText;
        }
      }

      if (!ValidateParameters(alarmDefinition.Parameter0, nameof(VM_AlarmCreator.Parameter0)[^1..], alarmDefinition.Messages, alarmDefinition.DefaultMessage)
        || !ValidateParameters(alarmDefinition.Parameter1, nameof(VM_AlarmCreator.Parameter1)[^1..], alarmDefinition.Messages, alarmDefinition.DefaultMessage)
        || !ValidateParameters(alarmDefinition.Parameter2, nameof(VM_AlarmCreator.Parameter2)[^1..], alarmDefinition.Messages, alarmDefinition.DefaultMessage)
        || !ValidateParameters(alarmDefinition.Parameter3, nameof(VM_AlarmCreator.Parameter3)[^1..], alarmDefinition.Messages, alarmDefinition.DefaultMessage))
      {
        modelState.AddModelError("error", "Wrong Alarm Parameters");
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      DateTime now = DateTime.Now;
      now = now.AddTicks(-(now.Ticks % 10000000));

      AlarmDefinition newAlarmDefinition = new AlarmDefinition
      {
        FKAlarmCategoryId = alarmDefinition.FKAlarmCategoryId,
        DefinitionCode = alarmDefinition.DefinitionCode,
        DefinitionDescription = alarmDefinition.DefinitionDescription,
        IsStandard = alarmDefinition.IsStandard is null ? false : alarmDefinition.IsStandard,
        IsPopupShow = alarmDefinition.IsPopupShow,
        IsToConfirm = alarmDefinition.IsToConfirm,
        EnumAlarmType = alarmDefinition.EnumAlarmType,
        DefinitionCreated = now,
        FKProjectId = alarmDefinition.FKProjectId
      };

      List<AlarmMessage> messages = new List<AlarmMessage>
      {
        new AlarmMessage
        {
          FKLanguageId = alarmDefinition.DefaultMessage.FKLanguageId,
          MessageText = alarmDefinition.DefaultMessage.MessageText
        }
      };

      foreach (var item in alarmDefinition.Messages)
      {
        if (item.MessageText != null)
        {
          messages.Add(new AlarmMessage { FKLanguageId = item.FKLanguageId, MessageText = item.MessageText });
        }
      }

      newAlarmDefinition.AlarmMessages = messages;

      List<Role> userRoles = await context.Roles.ToListAsync();
      List<AlarmDefinitionsToRole> roles = new List<AlarmDefinitionsToRole>();
      foreach (var item in userRoles)
      {
        roles.Add(new AlarmDefinitionsToRole() { FKRoleId = item.Id });
      }

      newAlarmDefinition.AlarmDefinitionsToRoles = roles;

      List<AlarmDefinitionParam> parameters = new List<AlarmDefinitionParam>();
      if (alarmDefinition.Parameter0.ParamName != null)
      {
        parameters.Add(new AlarmDefinitionParam()
        {
          ParamName = alarmDefinition.Parameter0.ParamName,
          ParamKey = 0
        });
      }
      if (alarmDefinition.Parameter1.ParamName != null)
      {
        parameters.Add(new AlarmDefinitionParam()
        {
          ParamName = alarmDefinition.Parameter1.ParamName,
          ParamKey = 1
        });
      }
      if (alarmDefinition.Parameter2.ParamName != null)
      {
        parameters.Add(new AlarmDefinitionParam()
        {
          ParamName = alarmDefinition.Parameter2.ParamName,
          ParamKey = 2
        });
      }
      if (alarmDefinition.Parameter3.ParamName != null)
      {
        parameters.Add(new AlarmDefinitionParam()
        {
          ParamName = alarmDefinition.Parameter3.ParamName,
          ParamKey = 3
        });
      }

      newAlarmDefinition.AlarmDefinitionParams = parameters;

      context.AlarmDefinitions.Add(newAlarmDefinition);
      context.SaveChanges();

      //return view model
      return result;
    }

    private static async Task<VM_Base> UpdateAlarmDefinition(ModelStateDictionary modelState, VM_AlarmCreator alarmDefinition)
    {
      VM_Base result = new VM_Base();

      using var context = new SMFContext();

      if (alarmDefinition.DefaultMessage?.MessageText == null)
        modelState.AddModelError("error", "Default Alarm Message Cannot Be Empty");
      if (alarmDefinition.DefinitionCode.ToUpper().Contains(CustomPrefix) && alarmDefinition.IsStandard.HasValue && alarmDefinition.IsStandard.Value)
        modelState.AddModelError("error", "Cannot Generate Standard Alarm With Custom Code. Select Module Again");
      if (!alarmDefinition.DefinitionCode.ToUpper().Contains(CustomPrefix) && (!alarmDefinition.IsStandard.HasValue || !alarmDefinition.IsStandard.Value))
        modelState.AddModelError("error", "Cannot Generate Custom Alarm With Standard Code. Select Module Again");
      if (await context.AlarmDefinitions
        .FirstOrDefaultAsync(x => x.DefinitionDescription.ToUpper().Equals(alarmDefinition.DefinitionDescription.ToUpper()) && x.AlarmDefinitionId != alarmDefinition.AlarmDefinitionId) is not null)
        modelState.AddModelError("error", "Definition With This Description Already Exists");

      foreach (var item in alarmDefinition.Messages)
      {
        if (string.IsNullOrEmpty(item.MessageText))
        {
          item.MessageText = alarmDefinition.DefaultMessage?.MessageText;
        }
      }

      if (!ValidateParameters(alarmDefinition.Parameter0, nameof(VM_AlarmCreator.Parameter0)[^1..], alarmDefinition.Messages, alarmDefinition.DefaultMessage)
      || !ValidateParameters(alarmDefinition.Parameter1, nameof(VM_AlarmCreator.Parameter1)[^1..], alarmDefinition.Messages, alarmDefinition.DefaultMessage)
      || !ValidateParameters(alarmDefinition.Parameter2, nameof(VM_AlarmCreator.Parameter2)[^1..], alarmDefinition.Messages, alarmDefinition.DefaultMessage)
      || !ValidateParameters(alarmDefinition.Parameter3, nameof(VM_AlarmCreator.Parameter3)[^1..], alarmDefinition.Messages, alarmDefinition.DefaultMessage))
      {
        modelState.AddModelError("error", "Wrong Alarm Parameters");
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      var newAlarmDefinition = await context.AlarmDefinitions
        .Include(x => x.AlarmDefinitionParams)
        .FirstAsync(x => x.AlarmDefinitionId == alarmDefinition.AlarmDefinitionId);

      context.AlarmDefinitionParams.RemoveRange(newAlarmDefinition.AlarmDefinitionParams);
      await context.SaveChangesAsync();

      newAlarmDefinition = await context.AlarmDefinitions
        .Include(x => x.AlarmMessages)
        .Include(x => x.AlarmDefinitionParams)
        .FirstAsync(x => x.AlarmDefinitionId == alarmDefinition.AlarmDefinitionId);

      newAlarmDefinition.FKAlarmCategoryId = alarmDefinition.FKAlarmCategoryId;
      newAlarmDefinition.DefinitionCode = alarmDefinition.DefinitionCode;
      newAlarmDefinition.DefinitionDescription = alarmDefinition.DefinitionDescription;
      newAlarmDefinition.IsStandard = alarmDefinition.IsStandard is null ? false : alarmDefinition.IsStandard;
      newAlarmDefinition.IsPopupShow = alarmDefinition.IsPopupShow;
      newAlarmDefinition.IsToConfirm = alarmDefinition.IsToConfirm;
      newAlarmDefinition.EnumAlarmType = alarmDefinition.EnumAlarmType;

      var defaultMessage = await context.AlarmMessages
        .FirstAsync(x => x.FKAlarmDefinitionId == newAlarmDefinition.AlarmDefinitionId && x.FKLanguage.LanguageCode.ToUpper().Equals("EN-US"));
      newAlarmDefinition.AlarmMessages
        .First(x => x.FKLanguageId == defaultMessage.FKLanguageId).MessageText = alarmDefinition.DefaultMessage.MessageText;

      foreach (var item in alarmDefinition.Messages)
      {
        if (newAlarmDefinition.AlarmMessages.FirstOrDefault(x => x.FKLanguageId == item.FKLanguageId) != null)
          newAlarmDefinition.AlarmMessages.First(x => x.FKLanguageId == item.FKLanguageId).MessageText = item.MessageText;
        else if (item.MessageText != null)
          newAlarmDefinition.AlarmMessages.Add(new AlarmMessage
          {
            FKLanguageId = item.FKLanguageId,
            MessageText = item.MessageText
          });
      }

      List<AlarmDefinitionParam> parameters = new List<AlarmDefinitionParam>();
      if (alarmDefinition.Parameter0.ParamName != null)
      {
        parameters.Add(new AlarmDefinitionParam()
        {
          ParamName = alarmDefinition.Parameter0.ParamName,
          ParamKey = 0
        });
      }
      if (alarmDefinition.Parameter1.ParamName != null)
      {
        parameters.Add(new AlarmDefinitionParam()
        {
          ParamName = alarmDefinition.Parameter1.ParamName,
          ParamKey = 1
        });
      }
      if (alarmDefinition.Parameter2.ParamName != null)
      {
        parameters.Add(new AlarmDefinitionParam()
        {
          ParamName = alarmDefinition.Parameter2.ParamName,
          ParamKey = 2
        });
      }

      newAlarmDefinition.AlarmDefinitionParams.AddRange(parameters);

      context.SaveChanges();

      //return view model
      return result;
    }

    private static SelectList GetAlarmCategorySelectList()
    {
      using var context = new SMFContext();
      IList<AlarmCategory> categories = GetAlarmCategoryList(context);

      var categoriesList = new Dictionary<long, string>();
      foreach (var item in categories)
      {
        categoriesList.Add(item.AlarmCategoryId, item.CategoryCode);
      }

      return new SelectList(categoriesList, "Key", "Value");
    }

    private static SelectList GetModuleList(SMFContext context)
    {
      var moduleList = new Dictionary<string, string>();
      var data = context.Modules
        .OrderByDescending(x => x.ModuleId)
        .ToList();

      foreach (var item in data)
      {
        moduleList.Add(item.ModuleCode, item.ModuleName);
      }

      return new SelectList(moduleList, "Key", "Value");
    }
  }
}
