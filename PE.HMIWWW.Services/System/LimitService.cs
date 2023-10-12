using System;
using System.Linq;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.System;
using SMF.Core;
using SMF.Core.Communication;
using SMF.Core.Interfaces;
using SMF.Core.Notification;
using SMF.DbEntity.EnumClasses;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.Services.System
{
  public interface ILimitService
  {
    DataSourceResult GetLimits(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    VM_Limit UpdateLimit(ModelStateDictionary modelState, VM_Limit viewModel);
  }

  public class LimitService : BaseService, ILimitService
  {
    private readonly SMFContext _smfContext;

    public LimitService(IHttpContextAccessor httpContextAccessor, SMFContext smfContext) : base(httpContextAccessor)
    {
      _smfContext = smfContext;
    }

    #region private methods

    private VM_Base LimitUpdateBroadcast(ModelStateDictionary modelState, string groupName)
    {
      VM_Base tmpRetValue = new VM_Base();
      IHmiModule client = null;
      try
      {
        client = InterfaceHelper.GetFactoryChannel<IHmiModule>(Constants.HmiProcessName);
        if (client != null)
        {
          client.LimitChangeFromWWW(groupName);
        }
      }
      catch (Exception ex)
      {
        tmpRetValue = new VM_Base();
        SetCommunicationError(modelState);
        NotificationController.LogException(ex);
      }

      return tmpRetValue;
    }

    #endregion

    #region interface ILimitService

    public DataSourceResult GetLimits(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
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
        //var list = uow.Repository<Limit>()
        //                                .Query()
        //                                .Include(q => q.ParameterGroup)
        //                                .Include(q => q.UnitOfMeasure)
        //                                .Get();
        IQueryable<Limit> list = _smfContext.Limits.Include(i => i.ParameterGroup).Include(j => j.Unit);
        returnValue = list.ToDataSourceLocalResult(request, modelState, data => new VM_Limit(data));
      //END OF DB OPERATION

      return returnValue;
    }

    public VM_Limit UpdateLimit(ModelStateDictionary modelState, VM_Limit viewModel)
    {
      VM_Limit returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION

      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
        //Limit limit = uow.Repository<Limit>().Find(viewModel.Id);
        Limit limit = _smfContext.Limits.Where(x => x.LimitId == viewModel.Id).Single();

        switch (limit.EnumParameterValueType)
        {
          case var value when value == LimitValueType.ValueDate.Value:
          {
            limit.LowerValueDate = DateTime.Parse(viewModel.LowerValue);
            limit.UpperValueDate = DateTime.Parse(viewModel.UpperValue);

            if (limit.LowerValueDate > limit.UpperValueDate)
            {
              DateTime? tempDate = limit.LowerValueDate;
              limit.LowerValueDate = limit.UpperValueDate;
              limit.UpperValueDate = tempDate;
            }

            break;
          }
          case var value when value == LimitValueType.ValueFloat.Value:
          {
            limit.LowerValueFloat = double.Parse(viewModel.LowerValue);
            limit.UpperValueFloat = double.Parse(viewModel.UpperValue);

            if (limit.LowerValueFloat > limit.UpperValueFloat)
            {
              double? tempFloat = limit.LowerValueFloat;
              limit.LowerValueFloat = limit.UpperValueFloat;
              limit.UpperValueFloat = tempFloat;
            }

            break;
          }
          case var value when value == LimitValueType.ValueInt.Value:
          {
            limit.LowerValueInt = int.Parse(viewModel.LowerValue);
            limit.UpperValueInt = int.Parse(viewModel.UpperValue);

            if (limit.LowerValueInt > limit.UpperValueInt)
            {
              int? tempInt = limit.LowerValueInt;
              limit.LowerValueInt = limit.UpperValueInt;
              limit.UpperValueInt = tempInt;
            }

            break;
          }
        }

        //uow.Repository<Limit>().Update(limit);
        //uow.SaveChanges();
        _smfContext.SaveChanges();
        //ModuleController.HmiRefresh(HMIRefreshKeys.Limits);
        returnValueVm = viewModel;
        LimitUpdateBroadcast(modelState, viewModel.GroupName);
      //END OF DB OPERATION

      return returnValueVm;
    }

    #endregion
  }
}
