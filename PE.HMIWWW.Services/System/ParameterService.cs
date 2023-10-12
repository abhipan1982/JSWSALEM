using System;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.Helpers;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Parameter;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.System;
using SMF.Core;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.Core.Interfaces;
using SMF.Core.Notification;
using SMF.DbEntity.EnumClasses;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.Services.System
{
  public interface IParameterService
  {
    DataSourceResult GetParameters(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    VM_Parameter UpdateParameter(ModelStateDictionary modelState, VM_Parameter viewModel);
    Task<VM_Base> UpdateParameterValueByParameterName(ModelStateDictionary modelState, string parameterName, object value);
  }

  public class ParameterService : BaseService, IParameterService
  {
    private readonly SMFContext _smfContext;

    public ParameterService(IHttpContextAccessor httpContextAccessor, SMFContext smfContext) : base(httpContextAccessor)
    {
      _smfContext = smfContext;
    }

    #region private methods

    private void ParameterUpdateBroadcast(ModelStateDictionary modelState, string groupName)
    {
      ParameterController.ReadParamaters();

      IHmiModule client = null;
      try
      {
        client = InterfaceHelper.GetFactoryChannel<IHmiModule>(Constants.HmiProcessName);
        if (client != null)
        {
          client.ParameterChangeFromWWW(groupName);
        }
      }
      catch (Exception ex)
      {
        SetCommunicationError(modelState);
        NotificationController.LogException(ex);
      }
    }

    #endregion

    #region interface

    public DataSourceResult GetParameters(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult returnValue = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      //DB OPERATION
      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
        IQueryable<Parameter> list = _smfContext.Parameters
          .Include(i => i.ParameterGroup)
          .Include(j => j.Unit);

        returnValue = list.ToDataSourceLocalResult(request, modelState, data => new VM_Parameter(data));
      //END OF DB OPERATION

      return returnValue;
    }

    public VM_Parameter UpdateParameter(ModelStateDictionary modelState, VM_Parameter viewModel)
    {
      VM_Parameter returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
        //Parameter parameter = uow.Repository<Parameter>().Find(viewModel.Id);
        Parameter parameter = _smfContext.Parameters.Where(x => x.ParameterId == viewModel.Id).Single();

        if (parameter != null)
        {
          parameter.Description = viewModel.Description;
          parameter.Name = viewModel.Name;
          parameter.ParameterGroupId = viewModel.GroupId;
          switch (parameter.EnumParameterValueType)
          {
            case var value when value == ParameterValueType.ValueDate.Value:
              {
                parameter.ValueDate = DateTime.Parse(viewModel.Value);
                break;
              }
            case var value when value == ParameterValueType.ValueFloat.Value:
              {
                parameter.ValueFloat = double.Parse(viewModel.Value);
                break;
              }
            case var value when value == ParameterValueType.ValueInt.Value:
              {
                parameter.ValueInt = Int32.Parse(viewModel.Value);
                break;
              }
            case var value when value == ParameterValueType.ValueText.Value:
              {
                parameter.ValueText = viewModel.Value;
                break;
              }
          }

          //uow.Repository<Parameter>().Update(parameter);
          //uow.SaveChanges();
          _smfContext.SaveChanges();
          returnValueVm = viewModel;
          ParameterUpdateBroadcast(modelState, viewModel.GroupName);
        }

      //END OF DB OPERATION      
      return returnValueVm;
    }

    public async Task<VM_Base> UpdateParameterValueByParameterName(ModelStateDictionary modelState, string parameterName, object value)
    {
      VM_Base returnValueVm = null;

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }

      Parameter parameter = await _smfContext.Parameters.Where(x => x.Name.Equals(parameterName)).FirstOrDefaultAsync();

      if (parameter != null)
      {
        ParameterGroup group = await _smfContext.ParameterGroups.Where(x => x.ParameterGroupId == parameter.ParameterGroupId).FirstOrDefaultAsync();

        if (parameter.EnumParameterValueType == ParameterValueType.ValueDate)
        {
          parameter.ValueDate = (DateTime)value;
        }
        else if (parameter.EnumParameterValueType == ParameterValueType.ValueFloat)
        {
          parameter.ValueFloat = (double)value;
        }
        else if (parameter.EnumParameterValueType == ParameterValueType.ValueInt)
        {
          parameter.ValueInt = (int)value;
        }
        else if (parameter.EnumParameterValueType == ParameterValueType.ValueText)
        {
          parameter.ValueText = value.ToString();
        }

        await _smfContext.SaveChangesAsync();

        if (group != null)
          ParameterUpdateBroadcast(modelState, group.Name);
      }

      //TODO create event
      //if (parameterName.Equals("DLS_Mode"))
      //{
      //  DataContractBase dc = new DataContractBase();
      //  InitDataContract(dc);
      //  TaskHelper.FireAndForget(() => HmiSendOffice.AddMillEvent(new DCMillEvent { EventType = (int)value == 0 ? MillEventType.FCR : MillEventType.DCR, UserId = dc.HmiInitiator.UserId }).GetAwaiter().GetResult());
      //}

      return returnValueVm;
    }

    #endregion
  }
}
