using System;
using System.Collections.Generic;
using System.Linq;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.ViewModel.System;
using SMF.DbEntity.ExceptionHelpers;
using PE.DbEntity.PEContext;
using System.Threading.Tasks;

namespace PE.HMIWWW.Services.System
{
  public interface IWidgetConfigurationService
  {
    DataSourceResult GetWidgetConfigurationsData(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request, string userId);
    VM_WidgetConfigurations GetWidgetConfiguration(ModelStateDictionary modelState, long id);
    VM_LongId UpdateWidgetConfiguration(ModelStateDictionary modelState, VM_WidgetConfigurations viewModel);
    VM_LongId DeleteWidgetConfiguration(ModelStateDictionary modelState, VM_LongId viewModel);
    VM_LongId InsertWidgetConfiguration(ModelStateDictionary modelState, VM_WidgetConfigurations viewModel);
    VM_WidgetConfigurations GetWidgetConfigurationByUser(ModelStateDictionary modelState, long widgetId, string userId);
    DataSourceResult GetWidgetsList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request, string userId);
    VM_LongId UpdateWidgetConfigurationForUser(ModelStateDictionary modelState, VM_WidgetConfigurations viewModel, string userId);
    Task<IList<VM_WidgetConfigurations>> GetVMWidgetConfigurationsList(string userId);
  }

  public class WidgetConfigurationService : BaseService, IWidgetConfigurationService
  {
    private readonly PEContext _peContext;
    private readonly HmiContext _hmiContext;

    public WidgetConfigurationService(IHttpContextAccessor httpContextAccessor, PEContext peContext, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
      _hmiContext = hmiContext;
    }

    #region public methods

    public async Task<IList<VM_WidgetConfigurations>> GetVMWidgetConfigurationsList(string userId)
    {
      return await _peContext.HMIWidgetConfigurations
        .Where(z => z.FKUserId.Equals(userId))
        .Include(x => x.FKWidget)
        .OrderBy(x => x.OrderSeq)
        .Select(x => new VM_WidgetConfigurations(x, false))
        .ToListAsync();
    }

    #endregion

    #region interface IWidgetConfigurationService

    public DataSourceResult GetWidgetConfigurationsData(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, string userId)
    {
      DataSourceResult returnValue = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      //DB OPERATION
      returnValue = _peContext.HMIWidgetConfigurations
        .Include(x => x.FKWidget)
        .ToDataSourceLocalResult(request, modelState, data => new VM_WidgetConfigurations(data));
      //END OF DB OPERATION

      return returnValue;
    }

    public DataSourceResult GetWidgetsList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, string userId)
    {
      DataSourceResult returnValue = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      //DB OPERATION
      returnValue = _hmiContext.V_WidgetConfigurations
        .Where(x => x.UserId.Equals(userId))
        .AsNoTracking()
        .ToDataSourceLocalResult(request, modelState, data => new VM_WidgetConfigurations(data));
      //END OF DB OPERATION

      return returnValue;
    }

    public VM_WidgetConfigurations GetWidgetConfiguration(ModelStateDictionary modelState, long id)
    {
      VM_WidgetConfigurations returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (id == 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION


      //DB OPERATION
      var widgetConfiguration = _peContext.HMIWidgetConfigurations
        .Include(x => x.FKWidget)
        .Single(x => x.WidgetConfigurationId == id);
      //END OF DB OPERATION

      returnValueVm = new VM_WidgetConfigurations(widgetConfiguration);

      return returnValueVm;
    }

    public VM_WidgetConfigurations GetWidgetConfigurationByUser(ModelStateDictionary modelState, long widgetId,
      string userId)
    {
      VM_WidgetConfigurations returnValueVm = null;

      if (widgetId == 0 || string.IsNullOrEmpty(userId))
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }

      var widgetConfigurationForUser = _peContext.HMIWidgetConfigurations
        .Include(x => x.FKWidget)
        .SingleOrDefault(x => x.FKWidgetId == widgetId && x.FKUserId.Equals(userId));

      if (widgetConfigurationForUser is not null)
        returnValueVm = new VM_WidgetConfigurations(widgetConfigurationForUser, true);
      else
      {
        var widget = _peContext.HMIWidgets.Single(x => x.WidgetId == widgetId);
        returnValueVm = new VM_WidgetConfigurations(widget, false);
      }

      return returnValueVm;
    }

    public VM_LongId UpdateWidgetConfiguration(ModelStateDictionary modelState, VM_WidgetConfigurations viewModel)
    {
      VM_LongId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel is null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (viewModel.WidgetConfigurationId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      var widgetConfiguration = _peContext.HMIWidgetConfigurations
        .Include(x => x.FKWidget)
        .Single(x => x.WidgetConfigurationId == viewModel.WidgetConfigurationId.Value);

      widgetConfiguration.WidgetName = viewModel.WidgetName;
      widgetConfiguration.WidgetFileName = viewModel.WidgetFileName;

      _peContext.SaveChanges();
      //END OF DB OPERATION

      returnValueVm = new VM_LongId(viewModel.WidgetConfigurationId ?? 0);

      return returnValueVm;
    }

    public VM_LongId UpdateWidgetConfigurationForUser(ModelStateDictionary modelState,
      VM_WidgetConfigurations viewModel, string userId)
    {
      VM_LongId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel is null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      if (viewModel.WidgetConfigurationId != null)
      {
        var widgetConfiguration = _peContext.HMIWidgetConfigurations
          .Single(x => x.WidgetConfigurationId == viewModel.WidgetConfigurationId.Value);

        if (viewModel.IsCurrentUserAssigned)
          widgetConfiguration.OrderSeq = viewModel.OrderSeq;
        else
          _peContext.HMIWidgetConfigurations.Remove(widgetConfiguration);

        _peContext.SaveChanges();

        returnValueVm = new VM_LongId(viewModel.WidgetConfigurationId ?? 0);
      }
      else
      {
        _peContext.HMIWidgetConfigurations.Add(new HMIWidgetConfiguration
        {
          OrderSeq = viewModel.OrderSeq,
          FKUserId = userId,
          FKWidgetId = viewModel.WidgetId,
          WidgetFileName = viewModel.WidgetFileName,
          WidgetName = viewModel.WidgetName
        });
        _peContext.SaveChanges();

        returnValueVm = new VM_LongId(viewModel.WidgetConfigurationId ?? 0);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_LongId DeleteWidgetConfiguration(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_LongId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel is null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (viewModel.Id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      var widgetConfiguration = _peContext.HMIWidgetConfigurations
        .Include(x => x.FKWidget)
        .Single(x => x.WidgetConfigurationId == viewModel.Id);

      if (widgetConfiguration is not null)
      {
        _peContext.HMIWidgetConfigurations.Remove(widgetConfiguration);
        _peContext.SaveChanges();

        returnValueVm = new VM_LongId(viewModel.Id);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_LongId InsertWidgetConfiguration(ModelStateDictionary modelState, VM_WidgetConfigurations viewModel)
    {
      VM_LongId returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel is null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (string.IsNullOrEmpty(viewModel.WidgetName) || string.IsNullOrEmpty(viewModel.WidgetFileName))
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      _peContext.HMIWidgetConfigurations.Add(new HMIWidgetConfiguration
      {
        WidgetName = viewModel.WidgetName,
        WidgetFileName = viewModel.WidgetFileName
      });
      _peContext.SaveChanges();

      returnValueVm = new VM_LongId(viewModel.WidgetConfigurationId ?? 0);
      //END OF DB OPERATION

      return returnValueVm;
    }

    #endregion
  }
}
