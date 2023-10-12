using System;
using System.Collections.Generic;
using System.Linq;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.ViewModel.System;
using SMF.DbEntity.ExceptionHelpers;
using SMF.Core.Communication;
using SMF.Core.DC;
using PE.HMIWWW.Core.Communication;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.Services.System
{
  public interface ICrewService
  {
    DataSourceResult GetCrewList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    Task<VM_Base> UpdateCrew(ModelStateDictionary modelState, VM_Crew viewModel);
    Task<VM_Base> DeleteCrew(ModelStateDictionary modelState, VM_LongId viewModel);
    VM_Crew GetCrew(ModelStateDictionary modelState, long id);
    Task<VM_Base> InsertCrew(ModelStateDictionary modelState, VM_Crew viewModel);
  }

  public class CrewService : BaseService, ICrewService
  {
    private readonly PEContext _peContext;

    public CrewService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    #region public methods

    public static List<EVTCrew> GetCrewsList()
    {
      List<EVTCrew> crews = null;
      try
      {
        using (PEContext uow = new PEContext())
        {
          crews = uow.EVTCrews.ToList();
        }
      }
      catch
      {
      }

      return crews;
    }

    #endregion

    #region private methods

    private bool IfCrewExists(string crewName)
    {
      bool retValue;
      try
      {
        EVTCrew crew = _peContext.EVTCrews.Where(z => z.CrewName == crewName).Single();
        if (crew != null)
        {
          retValue = true;
        }
        else
        {
          retValue = false;
        }
      }
      catch (Exception ex)
      {
        DbExceptionResult result =
          DbExceptionHelper.ProcessException(ex, "IfCrewExists::Database operation failed!", null);
        retValue = true;
      }

      return retValue;
    }

    #endregion

    #region interface ICrewService

    public async Task<VM_Base> DeleteCrew(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base result = new VM_Base();
      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null || viewModel.Id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return viewModel;
      }
      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendDeleteCrew(new BaseModels.DataContracts.Internal.EVT.DCCrewId() { CrewId = viewModel.Id });

      HandleWarnings(sendOfficeResult, ref modelState);

      //END OF DB OPERATION
      return result;
    }

    public VM_Crew GetCrew(ModelStateDictionary modelState, long id)
    {
      VM_Crew returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      EVTCrew crew = _peContext.EVTCrews.Find(id);
      if (crew != null)
      {
        returnValueVm = new VM_Crew(crew);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public DataSourceResult GetCrewList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult returnValue = null;

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      //DB OPERATION
      List<EVTCrew> list = _peContext.EVTCrews.ToList();
      returnValue = list.ToDataSourceLocalResult(request, modelState, data => new VM_Crew(data));

      //END OF DB OPERATION

      return returnValue;
    }

    public async Task<VM_Base> InsertCrew(ModelStateDictionary modelState, VM_Crew viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (viewModel.CrewId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION
      var crewElement = new DCCrewElement()
      { 
        LeaderName = viewModel.LeaderName, 
        CrewName = viewModel.CrewName, 
        CrewDescription = viewModel.CrewDescription 
      };
      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendInsertCrew(crewElement);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> UpdateCrew(ModelStateDictionary modelState, VM_Crew viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (viewModel.CrewId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendUpdateCrew(new DCCrewElement()
        { CrewId = (long)viewModel.CrewId, LeaderName = viewModel.LeaderName, CrewName = viewModel.CrewName, CrewDescription = viewModel.CrewDescription });
      //END OF DB OPERATION

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    #endregion
  }
}
